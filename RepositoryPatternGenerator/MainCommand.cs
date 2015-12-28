//------------------------------------------------------------------------------
// <copyright file="MainCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using RepositoryPatternGenerator.Utils;

namespace RepositoryPatternGenerator
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class MainCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("481c8da0-264a-4e10-af21-64a81ccb6dde");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private MainCommand(Package package)
        {



            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MainCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new MainCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void MenuItemCallback(object sender, EventArgs e)
        {
            var workspace = GetWorkspace();
            Solution solution;

            GetCurrentSolution(out solution);
            var project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

            if (project != null)
            {
                try
                {
                    //si se llama igual que el nombre del proyecto y el de una carpeta, se debe especificar la raiz
                    var iRepository = project.AddDocument("IRepository", CodeSnippets.IRepository,
                        new[] { "Repository", "Repository" });
                    workspace.TryApplyChanges(iRepository.Project.Solution);

                    GetCurrentSolution(out solution);
                    project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

                    var entityRepository = project.AddDocument("EntityRepository", CodeSnippets.EntityRepository,
                        new[] { "Repository", "Repository" });
                    workspace.TryApplyChanges(entityRepository.Project.Solution);

                    GetCurrentSolution(out solution);
                    project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

                    var iView = project.AddDocument("IViewModel", CodeSnippets.IViewModel, new[] { "Repository", "ViewModel" });
                    workspace.TryApplyChanges(iView.Project.Solution);
                }
                catch (Exception exception)
                {
                    VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    "Error on create files, check if you have already a folder named 'Repository' or 'ViewModel' and if they are empty",
                    "Error",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                }


                var documents = project.Documents;

                foreach (var d in documents.Where(d => d.Folders.Contains("Models")))
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
                    }
                }
            }
            else
            {
                VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    "Project named 'Repository' not found on the current solution",
                    "Info",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }


            string title = "MainCommand";

            string message = workspace.CurrentSolution.FilePath;

            if (message != null)
            {
                // Show a message box to prove we were here
                VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    message,
                    title,
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
            else
            {
                message = "First load a solution";
                // Show a message box to prove we were here
                VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    message,
                    title,
                    OLEMSGICON.OLEMSGICON_WARNING,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }


        }

        private VisualStudioWorkspace GetWorkspace()
        {
            IComponentModel componentModel = this.ServiceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            return componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
        }

        private void GetCurrentSolution(out Solution solution)
        {
            solution = GetWorkspace().CurrentSolution;
        }
    }
}
