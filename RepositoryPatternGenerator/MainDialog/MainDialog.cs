using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell.Interop;
using NuGet;
using NuGet.VisualStudio;
using RepositoryPatternGenerator.Helpers;
using Project = Microsoft.CodeAnalysis.Project;
using Solution = Microsoft.CodeAnalysis.Solution;
using Thread = System.Threading.Thread;
using VsShellUtilities = Microsoft.VisualStudio.Shell.VsShellUtilities;

namespace RepositoryPatternGenerator.MainDialog
{
    public partial class MainDialog : Form
    {
        private IServiceProvider ServiceProvider { get; }

        private VisualStudioWorkspace CurrentWorkspace { get; set; }

        private Solution CurrentSolution { get; set; }


        public MainDialog(IServiceProvider svc)
        {
            ServiceProvider = svc;
            GetWorkspace();
            RefreshCurrentSolution();
            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            MinimizeBox = false;
            LabelVersion.Text = "Version " + Helpers.Utils.GetManifestAttribute("Version");
        }

        private void NewGenerateBtn_Click(object sender, EventArgs e)
        {
            if (CurrentSolution.FilePath == null)
            {
                SolutionNotFound.Visible = true;
                return;
            }

            PreNProcessPanel.Visible = true;
        }

        private async void NGenerateBtn_Click(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (NProjectNameTxt.Text == "" || NEdmxFileNameTxt.Text == "" || !regex.IsMatch(NProjectNameTxt.Text) || !regex.IsMatch(NEdmxFileNameTxt.Text))
            {
                NFieldsRequired.Visible = true;
                return;
            }
            NFieldsRequired.Visible = false;



            if (CurrentSolution.Projects.Any(o => o.Name == NProjectNameTxt.Text))
            {
                ProjectExist.Visible = true;
                return;
            }
            ProjectExist.Visible = false;

            PreNProcessPanel.Visible = false;
            SettingsBtn.Visible = false;
            GoBackBtn.Visible = false;
            SettingsPanel.Visible = false;
            ProcessPanel.Visible = true;

            var options = new Dictionary<string, object>
                {
                    {"ProcessType", "Create" },
                    {"RepositoryProjectName", NProjectNameTxt.Text},
                    {"EdmxFolderName", SettingsModelsName.Text},
                    {"EdmxFileName", NEdmxFileNameTxt.Text}
                };

            var progressIndicator = new Progress<Tuple<int, string, Color>>(ReportProgress);

            await GenerateV2(options, progressIndicator);

            ExitBtn.Visible = true;
        }


        private void ExistingGenerateBtn_Click(object sender, EventArgs e)
        {
            // Provisional
            return;

            if (CurrentSolution.FilePath == null)
            {
                SolutionNotFound.Visible = true;
                return;
            }
            SolutionNotFound.Visible = false;

            if (!CurrentSolution.Projects.Any())
            {
                SolutionEmpty.Visible = true;
                return;
            }
            SolutionEmpty.Visible = false;

            PreEProcessPanel.Visible = true;
            EProjectNameCbx.Items.Clear();
            foreach (var pr in CurrentSolution.Projects)
            {
                EProjectNameCbx.Items.Add(pr.Name);
            }

            EProjectNameCbx.SelectedIndex = 0;

        }

        private async void EGenerateBtn_Click(object sender, EventArgs e)
        {
            return;
            var regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (EModelFolder.Text == "" || !regex.IsMatch(EModelFolder.Text))
            {
                EFieldRequired.Visible = true;
                return;
            }
            EFieldRequired.Visible = false;

            PreEProcessPanel.Visible = false;
            SettingsBtn.Visible = false;
            GoBackBtn.Visible = false;
            SettingsPanel.Visible = false;
            ProcessPanel.Visible = true;

            var options = new Dictionary<string, object>
                {
                    {"ProcessType", "Add" },
                    {"RepositoryProjectName", EProjectNameCbx.Text},
                    {"EdmxFolderName", EModelFolder.Text},
                    {"EdmxFileName", NEdmxFileNameTxt.Text},
                    {"EdmxFolderEmpty", EdmxFolderEmpty.Checked}
                };

            var progressIndicator = new Progress<Tuple<int, string, Color>>(ReportProgress);

            await Generate(options, progressIndicator);

            ExitBtn.Visible = true;



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

        private bool ApplyChanges(Solution solution)
        {
            var applied = CurrentWorkspace.TryApplyChanges(solution);

            if (!applied) return false;

            CurrentSolution = CurrentWorkspace.CurrentSolution;
            return true;
        }

        private async Task GenerateV2(Dictionary<string, object> options, IProgress<Tuple<int, string, Color>> p)
        {
            await Task.Run(async () =>
          {

              var repositoryName = options["RepositoryProjectName"].ToString();
              var edmxFileName = "Model";
              var edmxFolderName = "Model";
              var dataModelProjectName = "DataModel";
              var _onlineNugetPackageLocation = "https://packages.nuget.org/api/v2";
              Send(p, 0, " ---RPG process started---");
              // WORKSPACE LOAD
              Send(p, 0, " - Trying to get workspace");
              GetWorkspace();
              Send(p, 5, " - Workspace loaded", Color.Green);

              // SOLUTION LOAD
              Send(p, 5, " - Trying to get current solution");


              if (CurrentSolution.FilePath == null)
              {
                  Send(p, 5, " - Solution not found", Color.Red);
              }
              else
              {
                  Send(p, 10, " - Solution successfully loaded", Color.Green);
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
                      // delete default Class1.cs
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
                      Send(p, 20, " - A project named '" + repositoryName + "' already exists on current solution",
                          Color.Orange);
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
                              pi.ProjectItems.AddFromTemplate(pathAdo, edmxFileName);
                      }

                      RefreshCurrentSolution();
                      var xmlRepDoc = new XmlDocument();
                      var apiRepPath =
                          Path.Combine(
                              CurrentSolution.Projects.First(o => o.Name == repositoryName)
                                  .FilePath.Replace(repositoryName + ".csproj", ""), "App.Config");
                      xmlRepDoc.Load(apiRepPath);
                      var repConnectionStrings =
                          xmlRepDoc.DocumentElement.ChildNodes.Cast<XmlElement>()
                              .First(x => x.Name == "connectionStrings");
                      if (repConnectionStrings != null)
                      {
                          var repConnectionStringsNew = repConnectionStrings;

                          var csdataOld =
                              repConnectionStringsNew.ChildNodes.Cast<XmlElement>().First(x => x.Name == "add");

                          var csdataNew = csdataOld;

                          var csLine = csdataNew.GetAttribute("connectionString");
                          csLine = csLine.Replace(";App=", ";application name=");

                          string newLine =
                              $"res://*/{edmxFolderName}.{edmxFileName}.csdl|res://*/{edmxFolderName}.{edmxFileName}.ssdl|res://*/{edmxFolderName}.{edmxFileName}.msl";
                          var startIndex = csLine.IndexOf("metadata=") + "metadata=".Length;
                          var endIndex = csLine.IndexOf(";provider=") - ";provider=".Length + 1;
                          var oldLine = csLine.Substring(startIndex, endIndex);
                          csLine = csLine.Replace(oldLine, newLine);

                          csdataNew.SetAttribute("connectionString", csLine);
                          repConnectionStringsNew.ReplaceChild(csdataNew, csdataOld);
                          xmlRepDoc.DocumentElement.ReplaceChild(repConnectionStringsNew, repConnectionStrings);

                          xmlRepDoc.Save(apiRepPath);
                      }


                      Send(p, 30, " - Data Model succesfully generated", Color.Green);
                  }
                  catch (Exception)
                  {
                      Send(p, 30, " - Data Model not generated: ", Color.Red);
                  }

                  Send(p, 30, " - Trying to get the '" + repositoryName + "' project");
                  RefreshCurrentSolution();
                  var project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);
                  Send(p, 30, " - Project " + repositoryName + " successfully loaded", Color.Green);
                  Send(p, 30, " - Checking project integrity");
                  if (project == null)
                  {
                      Send(p, 30,
                          " - Project not found, ensure that you have a project named '" + repositoryName +
                          "' in the current solution", Color.Red);
                  }
                  else
                  {
                      try
                      {
                          Send(p, 30, " - Project integrity checked", Color.Green);
                          RefreshCurrentSolution();
                          project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);
                          CodeSnippets.CodeSnippetsV2.RepositoryName = repositoryName;

                          Send(p, 30, " - Trying to generate root folders interfaces and implementations");
                          var iRepository = project.AddDocument("IRepository",
                              CodeSnippets.CodeSnippetsV2.GetIRepository(),
                              new[] { repositoryName, "Repository" });
                          Send(p, 30, " - File IRepository generated", Color.Green);

                          var iAdapter = iRepository.Project.AddDocument("IAdapter",
                              CodeSnippets.CodeSnippetsV2.GetIAdapter(),
                              new[] { repositoryName, "Adapter" });
                          Send(p, 30, " - File IAdapter generated", Color.Green);

                          var entityFrameworkRepository = iAdapter.Project.AddDocument("EntityFrameworkRepository",
                              CodeSnippets.CodeSnippetsV2.GetEntityFrameworkRepository(),
                              new[] { repositoryName, "Repository" });
                          Send(p, 30, " - File EntityFrameworkRepository generated", Color.Green);

                          var adapter = entityFrameworkRepository.Project.AddDocument("Adapter",
                              CodeSnippets.CodeSnippetsV2.GetAdapter(),
                              new[] { repositoryName, "Adapter" });
                          Send(p, 30, " - File Adapter generated", Color.Green);
                          ApplyChanges(adapter.Project.Solution);




                          // CREATE PORTABLE CLASS

                          Send(p, 50, " - Trying to create '" + dataModelProjectName + "' Portable Class Library");
                          var projectPortableExist = false;
                          for (var i = 1; i <= dteProjects.Count; i++)
                          {
                              if (dteProjects.Item(i).Name == dataModelProjectName)
                              {
                                  projectPortableExist = true;
                              }
                          }

                          if (!projectPortableExist)
                          {
                              var pathProject = solution2.GetProjectTemplate("PortableClassLibrary.zip", "CSharp");

                              //if (pathProject.Contains("Store Apps\\Universal Apps"))
                              //pathProject = pathProject.Replace("Store Apps\\Universal Apps", "Windows");

                              var solutionName = Path.GetFileNameWithoutExtension(solution2.FullName);
                              var dest = solution2.FullName.Replace(solutionName + ".sln", dataModelProjectName);
                              solution2.AddFromTemplate(pathProject, dest, dataModelProjectName);

                              for (var i = 1; i <= dteProjects.Count; i++)
                              {
                                  if (dteProjects.Item(i).Name == dataModelProjectName)
                                      dteProject = dteProjects.Item(i);
                              }
                              // delete default Class1.cs
                              foreach (ProjectItem pi in dteProject.ProjectItems)
                              {
                                  if (pi.Name != "Class1.cs") continue;
                                  var filename = pi.FileNames[0];
                                  pi.Delete();
                                  System.IO.File.Delete(filename);
                              }

                              Send(p, 60, " - Portable Class Library created", Color.Green);

                          }
                          else
                          {
                              Send(p, 100,
                                  " - A project named '" + dataModelProjectName + "' already exists on current solution",
                                  Color.Red);
                          }

                          if (!projectPortableExist)
                          {
                              RefreshCurrentSolution();

                              var documents =
                                  CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                              var modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                              Project portableProject = null;
                              Project mainProject = null;
                              Send(p, 75, " - Trying to generate Adapters and ViewModels from Models");

                              var mainProjectName =
                                  CurrentSolution.Projects.FirstOrDefault(
                                      o => o.Name != repositoryName && o.Name != dataModelProjectName).Name;

                              foreach (var d in modelsDocument)
                              {
                                  var data = await d.GetSemanticModelAsync();
                                  var text = data.SyntaxTree.GetText().ToString();

                                  if (text.Contains($"namespace {repositoryName}.{edmxFolderName}") &&
                                      !text.Contains("DbContext"))
                                  {
                                      var currentClass =
                                          data.SyntaxTree.GetRoot()
                                              .DescendantNodes()
                                              .OfType<ClassDeclarationSyntax>()
                                              .First();
                                      var props =
                                          data.SyntaxTree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>();
                                      var modelName = currentClass.Identifier.Text;
                                      var modelProps = new List<string>();
                                      var modelPropsType = new Dictionary<string, string>();

                                      foreach (var pr in props)
                                      {
                                          if (pr.GetText().ToString().Contains("virtual")) continue;
                                          var line = pr.GetText().ToString();
                                          var name = pr.Identifier.Text;
                                          var type = line.Substring(line.IndexOf("public ") + 7,
                                              line.IndexOf(" " + name) - line.IndexOf("public ") - 7);
                                          modelProps.Add(name);
                                          modelPropsType.Add(name, type);
                                      }
                                      CodeSnippets.CodeSnippetsV2.MainProjectName = mainProjectName;

                                      var code = CodeSnippets.CodeSnippetsV2.GetModelAdapter(modelName, modelProps);
                                      var portableCode = CodeSnippets.CodeSnippetsV2.GetViewModel(modelName, modelPropsType);
                                      var mainProjectCode = CodeSnippets.CodeSnippetsV2.GetModelRepository(modelName);


                                      RefreshCurrentSolution();
                                      mainProject = CurrentSolution.Projects.FirstOrDefault(o => o.Name == mainProjectName);
                                      var md = mainProject.AddDocument(modelName + "Repository", mainProjectCode, new[] { mainProjectName, "Repository" });
                                      ApplyChanges(md.Project.Solution);
                                      Send(p, 75, $" - {modelName}Repository generated", Color.Green);


                                      RefreshCurrentSolution();
                                      project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);
                                      var ad = project.AddDocument(modelName + "Adapter", code, new[] { repositoryName, "Adapter" });
                                      ApplyChanges(ad.Project.Solution);
                                      Send(p, 75, $" - {modelName}Adapter generated", Color.Green);


                                      RefreshCurrentSolution();
                                      portableProject = CurrentSolution.Projects.FirstOrDefault(o => o.Name == dataModelProjectName);
                                      var vm = portableProject.AddDocument(modelName + "ViewModel", portableCode, new[] { dataModelProjectName, "ViewModel" });
                                      ApplyChanges(vm.Project.Solution);
                                      Send(p, 75, $" - {modelName}ViewModel generated", Color.Green);

                                  }
                              }

                              Send(p, 85, " - Trying to reference '" + dataModelProjectName + "' on '" + repositoryName + "'");
                              RefreshCurrentSolution();
                              project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);
                              portableProject = CurrentSolution.Projects.FirstOrDefault(o => o.Name == dataModelProjectName);
                              var projectWithReference = project.AddProjectReference(new ProjectReference(portableProject.Id));
                              if (ApplyChanges(projectWithReference.Solution))
                                  Send(p, 85, " - Reference added successfully", Color.Green);
                              else
                                  Send(p, 85, " - Can't add the reference, you must add it manually after process end", Color.Red);


                              Send(p, 90, " - Trying to reference '" + dataModelProjectName + "' and '" + repositoryName + "' on '" + mainProjectName + "'");
                              RefreshCurrentSolution();
                              project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);
                              portableProject = CurrentSolution.Projects.FirstOrDefault(o => o.Name == dataModelProjectName);
                              mainProject = CurrentSolution.Projects.FirstOrDefault(o => o.Name == mainProjectName);
                              var mainProjectWithReference = mainProject.AddProjectReference(new ProjectReference(portableProject.Id));
                              var mainProjectWithOtherReference = mainProjectWithReference.AddProjectReference(new ProjectReference(project.Id));
                              if (ApplyChanges(mainProjectWithOtherReference.Solution))
                                  Send(p, 85, " - References added successfully", Color.Green);
                              else
                                  Send(p, 85, " - Can't add the reference, you must add it manually after process end", Color.Red);

                              // INSTALL NUGET PACKAGE
                              Send(p, 90, " - Installing Entity Framework NuGet package on '" + mainProjectName + "'");

                              for (int i = 1; i <= dteProjects.Count; i++)
                              {
                                  if (dteProjects.Item(i).Name == mainProjectName)
                                      dteProject = dteProjects.Item(i);
                              }

                              string packageID = "EntityFramework";

                              IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

                              var componentModel = (IComponentModel)ServiceProvider.GetService(typeof(SComponentModel));
                              IVsPackageInstaller pckInstaller = componentModel.GetService<IVsPackageInstaller>();

                              List<IPackage> package = repo.FindPackagesById(packageID).ToList();
                              var lastVersion = package.Where(o => o.IsLatestVersion).Select(o => o.Version).FirstOrDefault();

                              try
                              {
                                  pckInstaller.InstallPackage(_onlineNugetPackageLocation, dteProject, packageID, lastVersion.Version, false);
                                  Send(p, 90, " - " + packageID + " " + lastVersion.Version + " installed", Color.Green);
                              }
                              catch (Exception)
                              {
                                  Send(p, 100, " - Error on installing " + packageID + " " + lastVersion.Version, Color.Red);
                              }

                              Send(p, 100, " - REPOSITORY PATTERN SUCCESSFULLY GENERATED", Color.Green);
                          }

                      }
                      catch (Exception)
                      {
                          Send(p, 100,
                          " - Joder, algo ha petado", Color.Red);
                      }
                  }

              }
              Send(p, 100, " ---RPG process ended---");
          });
        }

        private async Task Generate(Dictionary<string, object> options, IProgress<Tuple<int, string, Color>> p)
        {
            await Task.Run(async () =>
            {

                var processType = options["ProcessType"].ToString();
                var repositoryName = options["RepositoryProjectName"].ToString();
                var edmxFolderName = options["EdmxFolderName"].ToString();
                var edmxFileName = options["EdmxFileName"].ToString();
                var edmxFolderEmpty = false;

                if (options.ContainsKey("EdmxFolderEmpty"))
                    edmxFolderEmpty = (bool)options["EdmxFolderEmpty"];

                Send(p, 0, " ---RPG process started---");

                // WORKSPACE LOAD
                Send(p, 0, " - Trying to get workspace");
                GetWorkspace();
                Send(p, 5, " - Workspace loaded", Color.Green);

                // SOLUTION LOAD
                Send(p, 5, " - Trying to get current solution");


                if (CurrentSolution.FilePath == null)
                {
                    Send(p, 5, " - Solution not found", Color.Red);
                }
                else
                {
                    Send(p, 10, " - Solution successfully loaded", Color.Green);
                    if (processType == "Create")
                    {
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
                                // delete default Class1.cs
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
                                Send(p, 20, " - A project named '" + repositoryName + "' already exists on current solution",
                                    Color.Orange);
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
                                        pi.ProjectItems.AddFromTemplate(pathAdo, edmxFileName);
                                }

                                RefreshCurrentSolution();
                                var xmlRepDoc = new XmlDocument();
                                var apiRepPath =
                                    Path.Combine(
                                        CurrentSolution.Projects.First(o => o.Name == repositoryName)
                                            .FilePath.Replace(repositoryName + ".csproj", ""), "App.Config");
                                xmlRepDoc.Load(apiRepPath);
                                var repConnectionStrings =
                                    xmlRepDoc.DocumentElement.ChildNodes.Cast<XmlElement>()
                                        .First(x => x.Name == "connectionStrings");
                                if (repConnectionStrings != null)
                                {
                                    var repConnectionStringsNew = repConnectionStrings;

                                    var csdataOld =
                                        repConnectionStringsNew.ChildNodes.Cast<XmlElement>().First(x => x.Name == "add");

                                    var csdataNew = csdataOld;

                                    var csLine = csdataNew.GetAttribute("connectionString");
                                    csLine = csLine.Replace(";App=", ";application name=");

                                    string newLine = $"res://*/{edmxFolderName}.{edmxFileName}.csdl|res://*/{edmxFolderName}.{edmxFileName}.ssdl|res://*/{edmxFolderName}.{edmxFileName}.msl";
                                    var startIndex = csLine.IndexOf("metadata=") + "metadata=".Length;
                                    var endIndex = csLine.IndexOf(";provider=") - ";provider=".Length + 1;
                                    var oldLine = csLine.Substring(startIndex, endIndex);
                                    csLine = csLine.Replace(oldLine, newLine);

                                    csdataNew.SetAttribute("connectionString", csLine);
                                    repConnectionStringsNew.ReplaceChild(csdataNew, csdataOld);
                                    xmlRepDoc.DocumentElement.ReplaceChild(repConnectionStringsNew, repConnectionStrings);

                                    xmlRepDoc.Save(apiRepPath);
                                }


                                Send(p, 30, " - Data Model succesfully generated", Color.Green);
                            }
                            catch (Exception ex)
                            {

                                Send(p, 30, " - Data Model not generated: ", Color.Red);
                            }
                        }
                        catch (Exception)
                        {
                            Send(p, 30, " - Project not created", Color.Red);
                        }
                    }
                    Send(p, 30, " - Trying to get the '" + repositoryName + "' project");
                    RefreshCurrentSolution();
                    var project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);

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
                                var documents = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                                var modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                                var modelOk = true;

                                //if (!modelsDocument.Any(o => o.Name.Contains(edmxFileName.Replace(".edmx", ""))))
                                if (!modelsDocument.Any())
                                {
                                    var res = 6;
                                    if (!edmxFolderEmpty)
                                    {
                                        Send(p, 30,
                                            " - The '" + edmxFolderName +
                                            "' folder doesnt exist or content .EDMX model, generate Entity Framework data model before use this tool",
                                            Color.Orange);

                                        res = VsShellUtilities.ShowMessageBox(ServiceProvider, "",
                                            "The .EDMX model was not found, do you want to generate it?",
                                            OLEMSGICON.OLEMSGICON_WARNING,
                                            OLEMSGBUTTON.OLEMSGBUTTON_YESNO, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                                    }
                                    // if press YES
                                    if (res == 6)
                                    {
                                        DTE dte = (DTE)this.ServiceProvider.GetService(typeof(DTE));
                                        var solution2 = (Solution2)dte.Solution;
                                        Projects dteProjects = solution2.Projects;
                                        EnvDTE.Project dteProject = null;
                                        for (var i = 1; i <= dteProjects.Count; i++)
                                        {
                                            if (dteProjects.Item(i).Name == repositoryName)
                                            {
                                                dteProject = dteProjects.Item(i);
                                            }
                                        }

                                        try
                                        {
                                            // ADO .NET CREATE
                                            Send(p, 20, " - Generating ADO .NET Entity Data Model");
                                            var pathAdo =
                                                solution2.GetProjectItemTemplate("AdoNetEntityDataModelCSharp.zip",
                                                    "CSharp");

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
                                                    pi.ProjectItems.AddFromTemplate(pathAdo, edmxFileName);
                                            }

                                            RefreshCurrentSolution();

                                            var xmlRepDoc = new XmlDocument();
                                            var confFile = "";
                                            if (processType == "Create")
                                            {
                                                confFile = "App.Confing";
                                            }
                                            else if (processType == "Add")
                                            {
                                                confFile = "Web.Config";
                                            }

                                            var apiRepPath =
                                                Path.Combine(
                                                    CurrentSolution.Projects.First(o => o.Name == repositoryName)
                                                        .FilePath.Replace(repositoryName + ".csproj", ""), confFile);
                                            xmlRepDoc.Load(apiRepPath);
                                            var repConnectionStrings =
                                                xmlRepDoc.DocumentElement.ChildNodes.Cast<XmlElement>()
                                                    .First(x => x.Name == "connectionStrings");
                                            if (repConnectionStrings != null)
                                            {
                                                var repConnectionStringsNew = repConnectionStrings;

                                                var csdataOld =
                                                    repConnectionStringsNew.ChildNodes.Cast<XmlElement>().First(x => x.Name == "add");

                                                var csdataNew = csdataOld;

                                                var csLine = csdataNew.GetAttribute("connectionString");
                                                csLine = csLine.Replace(";App=", ";application name=");

                                                string newLine = $"res://*/{edmxFolderName}.{edmxFileName}.csdl|res://*/{edmxFolderName}.{edmxFileName}.ssdl|res://*/{edmxFolderName}.{edmxFileName}.msl";
                                                var startIndex = csLine.IndexOf("metadata=") + "metadata=".Length;
                                                var endIndex = csLine.IndexOf(";provider=");
                                                var oldLine = csLine.Substring(startIndex, endIndex);
                                                csLine = csLine.Replace(oldLine, newLine);

                                                csdataNew.SetAttribute("connectionString", csLine);
                                                repConnectionStringsNew.ReplaceChild(csdataNew, csdataOld);
                                                xmlRepDoc.DocumentElement.ReplaceChild(repConnectionStringsNew, repConnectionStrings);

                                                xmlRepDoc.Save(apiRepPath);
                                            }

                                            Send(p, 30, " - Data Model succesfully generated", Color.Green);
                                        }
                                        catch (Exception)
                                        {

                                            Send(p, 30, " - Data Model not generated", Color.Red);
                                            modelOk = false;
                                        }

                                    }
                                    else
                                    {
                                        Send(p, 30, " - Data Model not generated", Color.Red);
                                        modelOk = false;
                                    }

                                }
                                if (modelOk)
                                {
                                    RefreshCurrentSolution();
                                    project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);

                                    Send(p, 30, " - Project integrity checked",
                                        Color.Green);
                                    Send(p, 30, " - Trying to generate root folders and interfaces");

                                    CodeSnippets.CodeSnippets.RepositoryName = repositoryName;
                                    CodeSnippets.CodeSnippets.ModelsName = edmxFolderName;


                                    var iRepository = project.AddDocument("IRepository",
                                        CodeSnippets.CodeSnippets.GetIRepository(),
                                        new[] { repositoryName, "Repository" });
                                    Send(p, 30, " - File IRepository generated", Color.Green);


                                    var iView = iRepository.Project.AddDocument("IViewModel",
                                        CodeSnippets.CodeSnippets.GetIViewModel(), new[] { repositoryName, "ViewModels" });
                                    Send(p, 30, " - File IViewModel generated", Color.Green);


                                    Send(p, 30, " - Trying to generate EntityRepository class");
                                    var entityRepository = iView.Project.AddDocument("EntityRepository",
                                        CodeSnippets.CodeSnippets.GetEntityRepository(),
                                        new[] { repositoryName, "Repository" });
                                    Send(p, 30, " - File EntityRepository generated", Color.Green);

                                    Send(p, 30, " - Trying to generate ExpressionHelper class");
                                    var helperRepository = entityRepository.Project.AddDocument("ExpressionHelper",
                                        CodeSnippets.CodeSnippets.GetExpressionHelper(),
                                        new[] { repositoryName, "Helpers" });
                                    Send(p, 30, " - File ExpressionHelper generated", Color.Green);


                                    // var applied = true;
                                    ApplyChanges(helperRepository.Project.Solution);
                                    if (false)
                                    {
                                        //Send(p,worker, 30, " - Files cant be loaded on current solution", Color.Red);
                                    }
                                    else
                                    {

                                        RefreshCurrentSolution();

                                        documents =
                                            CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                                        ;
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
                                                sm.SyntaxTree.GetRoot()
                                                    .DescendantNodes()
                                                    .OfType<PropertyDeclarationSyntax>();

                                            controlPkProp.Add(className, new List<string>());

                                            // first round
                                            foreach (var pr in props)
                                            {
                                                if (pr.GetText().ToString().Contains("virtual") ||
                                                    pr.GetText().ToString().Contains("Nullable")) continue;
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
                                                            ||
                                                            (name.ToLower()).StartsWith("id") &&
                                                            (name.ToLower()).Contains(singleClassName.ToLower())
                                                            ||
                                                            (name.ToLower()).StartsWith("id_") &&
                                                            (name.ToLower()).Contains(singleClassName.ToLower())
                                                            ||
                                                            (name.ToLower()).EndsWith("id") &&
                                                            (name.ToLower()).Contains(singleClassName.ToLower())
                                                            ||
                                                            (name.ToLower()).EndsWith("_id") &&
                                                            (name.ToLower()).Contains(singleClassName.ToLower()))
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
                                                    if (pr.GetText().ToString().Contains("virtual") ||
                                                        pr.GetText().ToString().Contains("Nullable")) continue;
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

                                        RefreshCurrentSolution();
                                        documents =
                                            CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName).Documents;
                                        modelsDocument = documents.Where(d => d.Folders.Contains(edmxFolderName));
                                        var count = modelsDocument.Count();
                                        foreach (var d in modelsDocument)
                                        {
                                            var data = await d.GetSemanticModelAsync();
                                            var text = data.SyntaxTree.GetText().ToString();

                                            if (text.Contains($"namespace {repositoryName}.{edmxFolderName}") &&
                                                !text.Contains("DbContext"))
                                            {

                                                var currentClass =
                                                    data.SyntaxTree.GetRoot()
                                                        .DescendantNodes()
                                                        .OfType<ClassDeclarationSyntax>()
                                                        .First();
                                                var props =
                                                    data.SyntaxTree.GetRoot()
                                                        .DescendantNodes()
                                                        .OfType<PropertyDeclarationSyntax>();

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

                                                    type = line.Substring(line.IndexOf("public ") + 7,
                                                        line.IndexOf(" " + name) - line.IndexOf("public ") - 7);

                                                    //add the name and type of the prop
                                                    propsList.Add(name, type);
                                                }

                                                var code = CodeSnippets.CodeSnippets.GenerateClassViewModel(className,
                                                    propsList, controlPkProp[className]);

                                                RefreshCurrentSolution();
                                                project = CurrentSolution.Projects.FirstOrDefault(o => o.Name == repositoryName);

                                                var vm = project.AddDocument(className + "ViewModel", code,
                                                    new[] { repositoryName, "ViewModels" });
                                                ApplyChanges(vm.Project.Solution);
                                                Send(p, 75,
                                                    $" - {className}ViewModel generated, primary key: {controlPkProp[className].Aggregate((a, b) => a + ", " + b)}",
                                                    Color.Green);
                                            }
                                        }
                                        Send(p, 100,
                                            " - Its recommended to check if selected primary keys are correct, the algorithm could fail :). In case of wrong PK, modify method 'GetKeys()' of his respective ViewModel",
                                            Color.Orange);
                                        Send(p, 100, " - ---ALL FILES AND FOLDERS GENERATED SUCCESSFULLY---",
                                            Color.Green);
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


        private void GetWorkspace()
        {
            IComponentModel componentModel = this.ServiceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            CurrentWorkspace = componentModel.GetService<VisualStudioWorkspace>();
        }

        private string GetHour()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private void RefreshCurrentSolution()
        {
            if (CurrentWorkspace == null)
                GetWorkspace();
            else
                CurrentSolution = CurrentWorkspace.CurrentSolution;
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
            System.Diagnostics.Process.Start("https://twitter.com/fran_lsz");
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
            SettingsPanel.BringToFront();
            RepositoryTree.ExpandAll();
        }

        private void GoBackBtn_Click(object sender, EventArgs e)
        {

            var regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (SettingsModelsName.Text == "" || !regex.IsMatch(SettingsModelsName.Text))
            {
                SettingsRequiredField.Visible = true;
                return;
            }

            SettingsRequiredField.Visible = false;

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

        private void NBackToMenu_Click(object sender, EventArgs e)
        {
            PreNProcessPanel.Visible = false;
        }

        private void EBackToMenu_Click(object sender, EventArgs e)
        {
            PreEProcessPanel.Visible = false;
        }
    }
}

