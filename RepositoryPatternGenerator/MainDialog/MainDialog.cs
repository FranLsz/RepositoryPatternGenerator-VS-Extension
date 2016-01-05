using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using RepositoryPatternGenerator.Utils;

namespace RepositoryPatternGenerator.MainDialog
{
    public partial class MainDialog : Form
    {
        private IServiceProvider ServiceProvider { get; set; }
        public MainDialog(IServiceProvider svc)
        {
            ServiceProvider = svc;

            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            MinimizeBox = false;

            LabelVersion.Text = "Version " + Utils.Utils.GetManifestAttribute("Version");
        }

        // Send progress
        private void Send(BackgroundWorker worker, int progress)
        {
            worker.ReportProgress(progress);
        }
        // Send with text
        private void Send(BackgroundWorker worker, int progress, string text)
        {
            worker.ReportProgress(progress, new Tuple<string>(text));
        }
        // Send with text color
        private void Send(BackgroundWorker worker, int progress, string text, Color color)
        {
            worker.ReportProgress(progress, new Tuple<string, Color>(text, color));
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;

            if (e.UserState == null) return;

            if (e.UserState.GetType() == typeof(Tuple<string, Color>))
            {
                var tuple = (Tuple<string, Color>)e.UserState;
                LogBox.AppendLine(GetHour() + tuple.Item1, tuple.Item2);
            }
            else if (e.UserState.GetType() == typeof(Tuple<string>))
            {
                var tuple = (Tuple<string>)e.UserState;
                LogBox.AppendLine(GetHour() + tuple.Item1);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                LogBox.AppendLine("Canceled!");

            else if (e.Error != null)
                LogBox.AppendLine(("Error: " + e.Error.Message), Color.Red);

            else
                LogBox.AppendLine("Done!");

            ExitBtn.Visible = true;
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            GenerateBtn.Visible = false;
            SettingsBtn.Visible = false;
            GoBackBtn.Visible = false;
            LogBox.Visible = true;
            ProgressBar.Visible = true;

            BackgroundWorker bw = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            var param = new Dictionary<string, object>
             {
                 {"RepositoryProjectName", SettingsRepositoryName.Text},
                 {"ModelsFolderName", SettingsModelsName.Text}
             };

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync(param);
            }
        }

        private async void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            // PARAMS
            var param = (Dictionary<string, object>)e.Argument;

            var repositoryName = param["RepositoryProjectName"].ToString();
            var modelsName = param["ModelsFolderName"].ToString();

            Send(worker, 0, "---RPG process start---");

            Send(worker, 0, " - Trying to get workspace");
            var workspace = GetWorkspace();
            Send(worker, 5, " - Workspace loaded", Color.Green);

            Send(worker, 5, " - Trying to get current solution");
            Solution solution;
            GetCurrentSolution(out solution);
            if (solution.FilePath == null)
            {
                Send(worker, 5, " - Solution not found", Color.Red);
            }
            else
            {
                Send(worker, 10, " - Solution successfully loaded", Color.Green);
                Send(worker, 10, " - Trying to get the repository project");
                var project = solution.Projects.FirstOrDefault(o => o.Name == repositoryName);

                if (project == null)
                {
                    Send(worker, 10, " - Project not found, ensure that you have a project named '" + repositoryName + "' in the current solution", Color.Red);
                }
                else
                {
                    Send(worker, 15, " - Project 'Repository' successfully loaded", Color.Green);

                    var docs = project.Documents.Where(d => d.Folders.Contains("ViewModels") || d.Folders.Contains("Repository"));

                    if (docs.Any())
                    {
                        Send(worker, 15, " - The project already have a folder named 'Repository' or 'ViewModels', please remove it before generate the repository pattern",
                            Color.Red);
                    }
                    else
                    {

                        try
                        {
                            Send(worker, 15, " - Trying to generate root folders and interfaces");

                            CodeSnippets.CodeSnippets.RepositoryName = repositoryName;
                            CodeSnippets.CodeSnippets.ModelsName = modelsName;


                            var iRepository = project.AddDocument("IRepository", CodeSnippets.CodeSnippets.GetIRepository(),
                                new[] { repositoryName, "Repository" });
                            Send(worker, 20, " - File IRepository generated", Color.Green);


                            var iView = iRepository.Project.AddDocument("IViewModel", CodeSnippets.CodeSnippets.GetIViewModel(), new[] { repositoryName, "ViewModels" });
                            Send(worker, 25, " - File IViewModel generated", Color.Green);


                            Send(worker, 25, " - Trying to generate EntityRepository class");
                            var entityRepository = iView.Project.AddDocument("EntityRepository", CodeSnippets.CodeSnippets.GetEntityRepository(),
                                new[] { repositoryName, "Repository" });
                            Send(worker, 30, " - File EntityRepository generated", Color.Green);

                            var applied = true;
                            workspace.TryApplyChanges(entityRepository.Project.Solution);
                            if (!applied)
                            {
                                Send(worker, 30, " - Files cant be loaded on current solution", Color.Red);

                            }
                            else
                            {

                                GetCurrentSolution(out solution);

                                var documents = solution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents; ;
                                var modelsDocument = documents.Where(d => d.Folders.Contains(modelsName));
                                if (!modelsDocument.Any())
                                {
                                    Send(worker, 30, " - The '" + modelsName + "' folder doesnt exist or is empty, generate Entity Framework data model before use this tool",
                                        Color.Red);
                                }
                                else
                                {
                                    Send(worker, 75, " - Trying to generate ViewModels for each entity");

                                    //PRIMARY KEYS RASTREATOR
                                    var controlPkProp = new Dictionary<string, List<string>>();
                                    foreach (var d in modelsDocument)
                                    {
                                        var sm = await d.GetSemanticModelAsync();
                                        var smtext = sm.SyntaxTree.GetText().ToString();

                                        if (!smtext.Contains($"namespace {repositoryName}.{modelsName}") ||
                                            smtext.Contains("DbContext"))
                                            continue;

                                        var className =
                                            sm.SyntaxTree.GetRoot()
                                                .DescendantNodes()
                                                .OfType<ClassDeclarationSyntax>()
                                                .First()
                                                .Identifier.Text;
                                        var props =
                                            sm.SyntaxTree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>();

                                        controlPkProp.Add(className, new List<string>());

                                        // first round
                                        foreach (var p in props)
                                        {
                                            if (p.GetText().ToString().Contains("virtual") || p.GetText().ToString().Contains("Nullable")) continue;
                                            var name = p.Identifier.Text;

                                            // class user
                                            // if prop is id, iduser or userid or user_id
                                            if (name.ToLower().Equals("id")
                                                || name.ToLower().Equals("id_" + className.ToLower())
                                                || name.ToLower().Equals("id" + className.ToLower())
                                                || name.ToLower().Equals(className.ToLower() + "id")
                                                || name.ToLower().Equals(className.ToLower() + "_id"))
                                            {
                                                controlPkProp[className].Add(name);
                                            }
                                            else
                                            {
                                                if (className.Length < 4) continue;
                                                var noExit = true;
                                                var i = 1;
                                                while (noExit)
                                                {
                                                    // remove plural chars (-i last letters)
                                                    var singleClassName = className.Remove(className.Length - i, i);
                                                    if (name.ToLower().Equals("id")
                                                        || (name.ToLower()).StartsWith("id") && (name.ToLower()).Contains(singleClassName.ToLower())
                                                        || (name.ToLower()).StartsWith("id_") && (name.ToLower()).Contains(singleClassName.ToLower())
                                                        || (name.ToLower()).EndsWith("id") && (name.ToLower()).Contains(singleClassName.ToLower())
                                                        || (name.ToLower()).EndsWith("_id") && (name.ToLower()).Contains(singleClassName.ToLower()))
                                                    {
                                                        controlPkProp[className].Add(name);
                                                        noExit = false;
                                                    }
                                                    if (i == 3)
                                                        noExit = false;
                                                    i++;
                                                }
                                            }
                                        }

                                        // if no found any PK -> second round
                                        if (!controlPkProp[className].Any())
                                        {
                                            // less stricted than first
                                            foreach (var p in props)
                                            {
                                                if (p.GetText().ToString().Contains("virtual") || p.GetText().ToString().Contains("Nullable")) continue;
                                                var name = p.Identifier.Text;
                                                if ((name.ToLower().StartsWith("id")
                                                || name.ToLower().EndsWith("id"))
                                                &&
                                                (name.Remove(0, 1).Any(char.IsUpper) || name.Contains("_")))
                                                    controlPkProp[className].Add(name);
                                            }
                                        }

                                    }
                                    //END PRIMARY KEYS RASTREATOR



                                    foreach (var d in modelsDocument)
                                    {
                                        var data = await d.GetSemanticModelAsync();
                                        var text = data.SyntaxTree.GetText().ToString();

                                        if (text.Contains($"namespace {repositoryName}.{modelsName}") && !text.Contains("DbContext"))
                                        {

                                            var currentClass =
                                                data.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                                            var props = data.SyntaxTree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>();

                                            var className = currentClass.Identifier.Text;
                                            var propsList = new Dictionary<string, string>();
                                            foreach (var p in props)
                                            {
                                                //"    \r\n        public string CustomerID { get; set; }\r\n"
                                                // if is a relation prop, continue
                                                if (p.GetText().ToString().Contains("virtual")) continue;
                                                var line = p.GetText().ToString();
                                                var name = p.Identifier.Text;
                                                string type = "";

                                                /* if (!p.GetText().ToString().Contains("Nullable"))
                                                 {
                                                     type = data.GetDeclaredSymbol(p).Type.Name;
                                                 }
                                                 else
                                                 {
                                                     var realType = line.Substring(line.IndexOf("<"), line.IndexOf(">") + 1 - line.IndexOf("<"));
                                                     type = data.GetDeclaredSymbol(p).Type.Name + realType;
                                                 }*/

                                                type = line.Substring(line.IndexOf("public ") + 7, line.IndexOf(" " + name) - line.IndexOf("public ") - 7);

                                                //add the name and type of the prop
                                                propsList.Add(name, type);
                                            }

                                            var code = CodeSnippets.CodeSnippets.GenerateClassViewModel(className, propsList, controlPkProp[className]);

                                            GetCurrentSolution(out solution);
                                            project = solution.Projects.FirstOrDefault(o => o.Name == repositoryName);

                                            var vm = project.AddDocument(className + "ViewModel", code, new[] { repositoryName, "ViewModels" });
                                            workspace.TryApplyChanges(vm.Project.Solution);
                                            Send(worker, 75, $" - {className}ViewModel generated, primary key: {controlPkProp[className].Aggregate((a, b) => a + ", " + b)}", Color.Green);
                                        }
                                    }
                                    Send(worker, 100, " - Its recommended to check if selected primary keys are correct, the algorithm could fail :). In case of wrong PK, modify method 'GetKeys()' of his respective ViewModel", Color.Orange);
                                    Send(worker, 100, " - ---ALL FILES AND FOLDERS GENERATED SUCCESSFULLY---", Color.Green);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Send(worker, 100, " - Something goes wrong :(, please, check if the project already contains any folder named 'Repository' or 'ViewModels' in windows explorer.",
                                       Color.Red);
                        }
                    }
                }
            }
            Send(worker, 100, "---RPG process end---");
            
        }



        private VisualStudioWorkspace GetWorkspace()
        {
            IComponentModel componentModel = this.ServiceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            return componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
        }

        private string GetHour()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private void GetCurrentSolution(out Solution solution)
        {
            solution = GetWorkspace().CurrentSolution;
        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TwitterLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/franlsz95");
        }

        private void LinkedInLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://es.linkedin.com/in/francisco-lópez-sánchez-326907100");
        }

        private void GitHubLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FranLsz/RepositoryPatternGenerator-VS-Extension");
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            SettingsBtn.Visible = false;
            SettingsPanel.Visible = true;
            RepositoryTree.ExpandAll();
        }

        private void GoBackBtn_Click(object sender, EventArgs e)
        {
            SettingsBtn.Visible = true;
            SettingsPanel.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void SelectParents(TreeNode node, Boolean isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectParents(parent, isChecked);
        }

        private bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }
        private void RepositoryTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.SelectParents(e.Node, e.Node.Checked);
                }
            }
        }

        private void Header2_Click(object sender, EventArgs e)
        {

        }

    }
}
