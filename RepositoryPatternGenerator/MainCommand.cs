//------------------------------------------------------------------------------
// <copyright file="MainCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

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
            IComponentModel componentModel = this.ServiceProvider.GetService(typeof(SComponentModel)) as IComponentModel;
            var workspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();

            var solution = workspace.CurrentSolution;
            var project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

            
            if (project != null)
            {
                var iRepository = project.AddDocument("IRepository", CodeSnippets.IRepository);
                var res = workspace.TryApplyChanges(iRepository.Project.Solution);

                solution = workspace.CurrentSolution;
                project = solution.Projects.FirstOrDefault(o => o.Name == "Repository");

                var iView = project.AddDocument("IViewModel", CodeSnippets.IViewModel);
                var res2 =  workspace.TryApplyChanges(iView.Project.Solution);


                var documents = project.Documents;

                foreach (var d in documents.Where(d => d.Folders.Contains("Models")))
                {
                    var data = await d.GetSemanticModelAsync();
                    var text = data.SyntaxTree.GetText();
                }
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
    }
}
