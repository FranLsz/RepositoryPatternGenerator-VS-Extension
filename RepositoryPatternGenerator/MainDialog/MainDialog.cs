using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using RepositoryPatternGenerator.Utils;
using Solution = Microsoft.CodeAnalysis.Solution;
using Thread = System.Threading.Thread;

namespace RepositoryPatternGenerator.MainDialog
{
    public partial class MainDialog : Form
    {
        private IServiceProvider ServiceProvider { get; }

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

        private void NewGenerateBtn_Click(object sender, EventArgs e)
        {
            Solution solution;
            GetCurrentSolution(out solution);
            if (solution.FilePath == null)
            {
                SolutionNotFound.Visible = true;
                return;
            }

            PreProcessPanel.Visible = true;

        }

        private async void NGenerateBtn_Click(object sender, EventArgs e)
        {
            PreProcessPanel.Visible = false;
            SettingsBtn.Visible = false;
            GoBackBtn.Visible = false;
            SettingsPanel.Visible = false;
            ProcessPanel.Visible = true;
            var options = new Dictionary<string, object>
                {
                    {"RepositoryProjectName", NProjectNameTxt.Text},
                    {"EdmxFolderName", "Models"},
                    {"EdmxFileName", NEdmxFileNameTxt.Text}
                };

            var progressIndicator = new Progress<Tuple<int, string, Color>>(ReportProgress);

            await Generate(options, progressIndicator);

            ExitBtn.Visible = true;
        }


        private void ExistingGenerateBtn_Click(object sender, EventArgs e)
        {
            Solution solution;
            GetCurrentSolution(out solution);
            if (solution.FilePath == null)
            {
                SolutionNotFound.Visible = true;
                return;
            }
        }

        void ReportProgress(Tuple<int, string, Color> t)
        {
            ProgressBar.Value = t.Item1;
            LogBox.AppendLine(GetHour() + t.Item2, t.Item3);
        }

        // Send progress with color
        private static void Send(IProgress<Tuple<int, string, Color>> p, int progress, string text, Color color)
        {
            p.Report(new Tuple<int, string, Color>(progress, text, color));
        }

        // Send progress without color
        private static void Send(IProgress<Tuple<int, string, Color>> p, int progress, string text)
        {
            p.Report(new Tuple<int, string, Color>(progress, text, Color.Black));
        }



        private async Task Generate(Dictionary<string, object> options, IProgress<Tuple<int, string, Color>> p)
        {
            await Task.Run(async () =>
            {
                var repositoryName = options["RepositoryProjectName"].ToString();
                var edmxFolderName = options["EdmxFolderName"].ToString();
                var edmxFileName = options["EdmxFileName"].ToString();

                Send(p, 0, " ---RPG process started---");

                // WORKSPACE LOAD
                Send(p, 0, " - Trying to get workspace");
                var workspace = GetWorkspace();
                Send(p, 5, " - Workspace loaded", Color.Green);

                // SOLUTION LOAD
                Send(p, 5, " - Trying to get current solution");
                Solution solution;
                GetCurrentSolution(out solution);
                if (solution.FilePath == null)
                {
                    Send(p, 5, " - Solution not found", Color.Red);
                }
                else
                {
                    Send(p, 10, " - Solution successfully loaded", Color.Green);

                    try
                    {
                        // CREATE PROJECT
                        Send(p, 10, " - Trying to create '" + repositoryName + "'");


                        DTE dte = (DTE)this.ServiceProvider.GetService(typeof(DTE));
                        var solution2 = (Solution2)dte.Solution;
                        Projects dteProjects = solution2.Projects;
                        EnvDTE.Project dteProject = null;
                        var projectExist = false;
                        for (var i = 1; i <= dteProjects.Count; i++)
                        {
                            if (dteProjects.Item(i).Name == repositoryName)
                            {
                                projectExist = true;
                                dteProject = dteProjects.Item(i);
                            }
                        }

                        if (!projectExist)
                        {
                            var pathProject = solution2.GetProjectTemplate("ClassLibrary.zip", "CSharp");

                            if (pathProject.Contains("Store Apps\\Universal Apps"))
                                pathProject = pathProject.Replace("Store Apps\\Universal Apps", "Windows");

                            var solutionName = Path.GetFileNameWithoutExtension(solution2.FullName);
                            solution2.AddFromTemplate(pathProject,
                                solution2.FullName.Replace(solutionName + ".sln", repositoryName), repositoryName);




                            for (var i = 1; i <= dteProjects.Count; i++)
                            {
                                if (dteProjects.Item(i).Name == repositoryName)
                                    dteProject = dteProjects.Item(i);
                            }
                            foreach (ProjectItem pi in dteProject.ProjectItems)
                            {
                                if (pi.Name != "Class1.cs") continue;
                                var filename = pi.FileNames[0];
                                pi.Delete();
                                System.IO.File.Delete(filename);
                            }

                            Send(p, 20, " - Project created", Color.Green);
                        }
                        else
                        {
                            Send(p, 20, " - A project named '" + repositoryName + "' already exists on current solution", Color.Orange);
                        }

                        try
                        {
                            // ADO .NET CREATE
                            Send(p, 20, " - Generating ADO .NET Entity Data Model");
                            var pathAdo = solution2.GetProjectItemTemplate("AdoNetEntityDataModelCSharp.zip", "CSharp");

                            try
                            {
                                dteProject.ProjectItems.AddFolder(edmxFolderName);
                            }
                            catch (Exception)
                            {
                                // Models folder already exist
                            }

                            foreach (ProjectItem pi in dteProject.ProjectItems)
                            {
                                if (pi.Name == edmxFolderName)
                                    pi.ProjectItems.AddFromTemplate(pathAdo, edmxFileName + ".edmx");
                            }

                            Send(p, 30, " - Data Model succesfully generated", Color.Green);
                        }
                        catch (Exception)
                        {

                            Send(p, 30, " - Data Model not generated: ", Color.Red);
                        }
                    }
                    catch (Exception)
                    {
                        Send(p, 30, " - Project not created", Color.Red);
                    }


                    GetCurrentSolution(out solution);
                    Send(p, 30, " - Trying to get the repository project");
                    var project = solution.Projects.FirstOrDefault(o => o.Name == repositoryName);

                    if (project == null)
                    {
                        Send(p, 30, " - Project not found, ensure that you have a project named '" + repositoryName + "' in the current solution", Color.Red);
                    }
                    else
                    {
                        Send(p, 30, " - Project " + repositoryName + " successfully loaded", Color.Green);

                        Send(p, 30, " - Checking project integrity");

                        var docs = project.Documents.Where(d => d.Folders.Contains("ViewModels") || d.Folders.Contains("Repository"));

                        if (docs.Any())
                        {
                            Send(p, 30, " - The project already have a folder named 'Repository' or 'ViewModels', please remove it before generate the repository pattern",
                                Color.Red);
                        }
                        else
                        {

                            try
                            {


                                var documents = solution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                                var modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                                if (!modelsDocument.Any())
                                {
                                    Send(p, 30, " - The '" + edmxFolderName + "' folder doesnt exist or is empty, generate Entity Framework data model before use this tool",
                                        Color.Red);
                                }
                                else {
                                    Send(p, 30, " - Project integrity checked",
                                        Color.Green);
                                    Send(p, 30, " - Trying to generate root folders and interfaces");

                                    CodeSnippets.CodeSnippets.RepositoryName = repositoryName;
                                    CodeSnippets.CodeSnippets.ModelsName = edmxFolderName;


                                    var iRepository = project.AddDocument("IRepository", CodeSnippets.CodeSnippets.GetIRepository(),
                                        new[] { repositoryName, "Repository" });
                                    Send(p, 30, " - File IRepository generated", Color.Green);


                                    var iView = iRepository.Project.AddDocument("IViewModel", CodeSnippets.CodeSnippets.GetIViewModel(), new[] { repositoryName, "ViewModels" });
                                    Send(p, 30, " - File IViewModel generated", Color.Green);


                                    Send(p, 30, " - Trying to generate EntityRepository class");
                                    var entityRepository = iView.Project.AddDocument("EntityRepository", CodeSnippets.CodeSnippets.GetEntityRepository(),
                                        new[] { repositoryName, "Repository" });
                                    Send(p, 30, " - File EntityRepository generated", Color.Green);

                                    // var applied = true;
                                    workspace.TryApplyChanges(entityRepository.Project.Solution);
                                    if (false)
                                    {
                                        //Send(p,worker, 30, " - Files cant be loaded on current solution", Color.Red);
                                    }
                                    else
                                    {

                                        GetCurrentSolution(out solution);

                                        documents = solution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents; ;
                                        modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                                        /*if (!modelsDocument.Any())
                                        {
                                            Send(p,worker, 30, " - The '" + modelsName + "' folder doesnt exist or is empty, generate Entity Framework data model before use this tool",
                                                Color.Red);
                                        }
                                        else
                                        {*/
                                        Send(p, 75, " - Trying to generate ViewModels for each entity");

                                        //PRIMARY KEYS RASTREATOR
                                        var controlPkProp = new Dictionary<string, List<string>>();
                                        foreach (var d in modelsDocument)
                                        {
                                            var sm = await d.GetSemanticModelAsync();
                                            var smtext = sm.SyntaxTree.GetText().ToString();

                                            if (!smtext.Contains($"namespace {repositoryName}.{edmxFolderName}") ||
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
                                            foreach (var pr in props)
                                            {
                                                if (pr.GetText().ToString().Contains("virtual") || pr.GetText().ToString().Contains("Nullable")) continue;
                                                var name = pr.Identifier.Text;

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
                                                foreach (var pr in props)
                                                {
                                                    if (pr.GetText().ToString().Contains("virtual") || pr.GetText().ToString().Contains("Nullable")) continue;
                                                    var name = pr.Identifier.Text;
                                                    if ((name.ToLower().StartsWith("id")
                                                    || name.ToLower().EndsWith("id"))
                                                    &&
                                                    (name.Remove(0, 1).Any(char.IsUpper) || name.Contains("_")))
                                                        controlPkProp[className].Add(name);
                                                }
                                            }

                                        }
                                        //END PRIMARY KEYS RASTREATOR

                                        GetCurrentSolution(out solution);

                                        documents = solution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                                        modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                                        var count = modelsDocument.Count();
                                        foreach (var d in modelsDocument)
                                        {
                                            var data = await d.GetSemanticModelAsync();
                                            var text = data.SyntaxTree.GetText().ToString();

                                            if (text.Contains($"namespace {repositoryName}.{edmxFolderName}") && !text.Contains("DbContext"))
                                            {

                                                var currentClass =
                                                    data.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                                                var props = data.SyntaxTree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>();

                                                var className = currentClass.Identifier.Text;
                                                var propsList = new Dictionary<string, string>();
                                                foreach (var pr in props)
                                                {
                                                    //"    \r\n        public string CustomerID { get; set; }\r\n"
                                                    // if is a relation prop, continue
                                                    if (pr.GetText().ToString().Contains("virtual")) continue;
                                                    var line = pr.GetText().ToString();
                                                    var name = pr.Identifier.Text;
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
                                                Send(p, 75, $" - {className}ViewModel generated, primary key: {controlPkProp[className].Aggregate((a, b) => a + ", " + b)}", Color.Green);
                                            }
                                        }
                                        Send(p, 100, " - Its recommended to check if selected primary keys are correct, the algorithm could fail :). In case of wrong PK, modify method 'GetKeys()' of his respective ViewModel", Color.Orange);
                                        Send(p, 100, " - ---ALL FILES AND FOLDERS GENERATED SUCCESSFULLY---", Color.Green);
                                    }
                                }

                            }
                            catch (Exception)
                            {
                                Send(p, 100, " - Something goes wrong :(, please, check if the project already contains any folder named 'Repository' or 'ViewModels' in windows explorer.", Color.Red);
                            }
                        }
                    }
                }
                Send(p, 100, " ---RPG process ended---");

            });

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
    }
}

