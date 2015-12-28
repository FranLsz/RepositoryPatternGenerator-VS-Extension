using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using RepositoryPatternGenerator.Utils;

namespace RepositoryPatternGenerator
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

        }

        private async void GenerateBtn_Click(object sender, EventArgs e)
        {
            GenerateBtn.Visible = false;
            LogBox.Visible = true;
            ProgressBar.Visible = true;
            LogBox.Text = "";
            LogBox.AppendLine("---RPG process start---");

            LogBox.AppendLine(GetHour() + " - Trying to get workspace");
            var workspace = GetWorkspace();
            LogBox.AppendLine(GetHour() + " - Workspace loaded", Color.Green);

            LogBox.AppendLine(GetHour() + " - Trying to get current solution");
            Solution solution;
            GetCurrentSolution(out solution);
            if (solution.FilePath == null)
            {
                LogBox.AppendLine(GetHour() + " - Solution not found", Color.Red);
                ProgressBar.Value = 100;
            }
            else
            {
                LogBox.AppendLine(GetHour() + " - Solution successfully loaded", Color.Green);
                LogBox.AppendLine(GetHour() + " - Trying to get the repository project");
                var project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

                if (project == null)
                    LogBox.AppendLine(GetHour() + " - Project not found, ensure that you have a project named 'Repository' in the current solution", Color.Red);
                else
                {
                    LogBox.AppendLine(GetHour() + " - Project 'Repository' successfully loaded", Color.Green);

                    var docs = project.Documents.Where(d => d.Folders.Contains("ViewModel") || d.Folders.Contains("Repository"));

                    if (docs.Any())
                    {
                        LogBox.AppendLine(
                            GetHour() +
                            " - The project already have a folder named 'Repository' or 'ViewModel', please remove it before generate the repository pattern",
                            Color.Red);
                        ProgressBar.Value = 100;
                    }
                    else
                    {



                        LogBox.AppendLine(GetHour() + " - Trying to generate root folders and interfaces");


                        var iRepository = project.AddDocument("IRepository", CodeSnippets.IRepository,
                            new[] { "Repository", "Repository" });
                        LogBox.AppendLine(GetHour() + " - File IRepository generated", Color.Green);


                        var iView = iRepository.Project.AddDocument("IViewModel", CodeSnippets.IViewModel, new[] { "Repository", "ViewModel" });
                        LogBox.AppendLine(GetHour() + " - File IViewModel generated", Color.Green);


                        LogBox.AppendLine(GetHour() + " - Trying to generate EntityRepository class");
                        var entityRepository = iView.Project.AddDocument("EntityRepository", CodeSnippets.EntityRepository,
                            new[] { "Repository", "Repository" });
                        LogBox.AppendLine(GetHour() + " - File EntityRepository generated", Color.Green);


                        var applied = workspace.TryApplyChanges(entityRepository.Project.Solution);
                        ProgressBar.Value = 50;
                        if (!applied)
                        {
                            LogBox.AppendLine(GetHour() + " - Files cant be loaded on current solution", Color.Red);
                            ProgressBar.Value = 100;

                        }
                        else
                        {
                            //LogBox.AppendLine(GetHour() + " - Changes loaded on current solution", Color.Green);

                            GetCurrentSolution(out solution);

                            var documents = solution.Projects.FirstOrDefault(o => o.Name == "Repository").Documents; ;
                            var modelsDocument = documents.Where(d => d.Folders.Contains("Models"));
                            if (!modelsDocument.Any())
                            {
                                LogBox.AppendLine(
                                    GetHour() +
                                    " - The 'Models' folder doesnt exist or is empty, generate Entity Framework data model before use this tool",
                                    Color.Orange);
                                ProgressBar.Value = 100;
                            }
                            else
                            {
                                ProgressBar.Value = 75;
                                LogBox.AppendLine(GetHour() + " - Trying to generate ViewModels for each entity");
                                foreach (var d in modelsDocument)
                                {
                                    var data = await d.GetSemanticModelAsync();
                                    var text = data.SyntaxTree.GetText().ToString();

                                    if (text.Contains("namespace Repository.Models") && !text.Contains("DbContext"))
                                    {

                                        var currentClass =
                                            data.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                                        var props = data.SyntaxTree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>();

                                        var className = currentClass.Identifier.Text;
                                        var propsList = new Dictionary<string, string>();
                                        var primaryKeys = new List<string>();
                                        foreach (var p in props)
                                        {
                                            // if inst a relation prop
                                            if (p.GetText().ToString().Contains("virtual")) continue;
                                            var name = p.Identifier.Text;
                                            var type = data.GetDeclaredSymbol(p).Type.Name;

                                            //add the name and type of the prop
                                            propsList.Add(name, type);

                                            //if is an id prop...
                                            if (name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                                name.Equals("id" + className, StringComparison.OrdinalIgnoreCase))
                                                primaryKeys.Add(name);
                                        }

                                        // if the previus foreach doesnt add any key, the next loop add any key which contains the id keyword
                                        if (primaryKeys.Count.Equals(0))
                                        {
                                            foreach (var p in props)
                                            {
                                                if (p.GetText().ToString().Contains("virtual")) continue;
                                                var name = p.Identifier.Text;

                                                if (name.ToLower().StartsWith("id", StringComparison.OrdinalIgnoreCase) &&
                                                    (name.Any(char.IsUpper) || name.Contains("_")))
                                                    primaryKeys.Add(name);
                                            }
                                        }

                                        var code = CodeSnippets.GenerateClassViewModel(className, propsList, primaryKeys);

                                        GetCurrentSolution(out solution);
                                        project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

                                        var vm = project.AddDocument(className + "ViewModel", code, new[] { "Repository", "ViewModel" });
                                        workspace.TryApplyChanges(vm.Project.Solution);
                                        LogBox.AppendLine(GetHour() + $" - {className}ViewModel generated, primary key: {primaryKeys.Aggregate((a, b) => a + ", " + b)}", Color.Green);
                                    }
                                }
                                ProgressBar.Value = 100;
                                LogBox.AppendLine(GetHour() + " - All files and folders generated successfully", Color.Green);
                                LogBox.AppendLine(GetHour() + " - Its recommended to check if selected primary keys are correct, the algorithm can fail :)", Color.Orange);
                                LogBox.AppendLine("---RPG process end---");
                            }

                        }
                    }
                }
            }

            ExitBtn.Visible = true;
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
    }
}
