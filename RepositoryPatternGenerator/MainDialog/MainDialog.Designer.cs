namespace RepositoryPatternGenerator.MainDialog
{
    partial class MainDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("IRepository (Interface)");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("EntityRepository (Class)");
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("Repository (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode45,
            treeNode46});
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("IViewModel (Interface)");
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("Model1ViewModel (Class)");
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("Model2ViewModel (Class)");
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("ViewModels (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode48,
            treeNode49,
            treeNode50});
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Model1");
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("Model2");
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("Models (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode52,
            treeNode53});
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Repository (Project)", new System.Windows.Forms.TreeNode[] {
            treeNode47,
            treeNode51,
            treeNode54});
            this.Logo = new System.Windows.Forms.PictureBox();
            this.Header1 = new System.Windows.Forms.Label();
            this.Header2 = new System.Windows.Forms.Label();
            this.NewGenerateBtn = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.TwitterLink = new System.Windows.Forms.PictureBox();
            this.LinkedInLink = new System.Windows.Forms.PictureBox();
            this.GitHubLink = new System.Windows.Forms.PictureBox();
            this.SettingsBtn = new System.Windows.Forms.PictureBox();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.SettingsRequiredField = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SettingsModelsName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RepositoryTree = new System.Windows.Forms.TreeView();
            this.SettingsLabel = new System.Windows.Forms.Label();
            this.GoBackBtn = new System.Windows.Forms.PictureBox();
            this.SolutionNotFound = new System.Windows.Forms.Label();
            this.ExistingGenerateBtn = new System.Windows.Forms.Button();
            this.ProcessPanel = new System.Windows.Forms.Panel();
            this.PreNProcessPanel = new System.Windows.Forms.Panel();
            this.ProjectExist = new System.Windows.Forms.Label();
            this.NFieldsRequired = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.NEdmxFileNameTxt = new System.Windows.Forms.TextBox();
            this.EdmxFileNameLbl = new System.Windows.Forms.Label();
            this.ProjectNameLbl = new System.Windows.Forms.Label();
            this.NProjectNameTxt = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.NGenerateBtn = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SolutionEmpty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PreEProcessPanel = new System.Windows.Forms.Panel();
            this.EFieldRequired = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.EModelFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.EProjectNameCbx = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EGenerateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwitterLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkedInLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GitHubLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsBtn)).BeginInit();
            this.SettingsPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoBackBtn)).BeginInit();
            this.ProcessPanel.SuspendLayout();
            this.PreNProcessPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.PreEProcessPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(323, -20);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(200, 200);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 0;
            this.Logo.TabStop = false;
            // 
            // Header1
            // 
            this.Header1.AutoSize = true;
            this.Header1.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header1.Location = new System.Drawing.Point(244, 154);
            this.Header1.Name = "Header1";
            this.Header1.Size = new System.Drawing.Size(376, 38);
            this.Header1.TabIndex = 1;
            this.Header1.Text = "Repository Pattern Generator";
            // 
            // Header2
            // 
            this.Header2.AutoSize = true;
            this.Header2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.Header2.Location = new System.Drawing.Point(207, 192);
            this.Header2.Name = "Header2";
            this.Header2.Size = new System.Drawing.Size(435, 25);
            this.Header2.TabIndex = 2;
            this.Header2.Text = "Developed and designed by Francisco López Sánchez";
            // 
            // NewGenerateBtn
            // 
            this.NewGenerateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.NewGenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewGenerateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.NewGenerateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewGenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewGenerateBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.NewGenerateBtn.ForeColor = System.Drawing.Color.White;
            this.NewGenerateBtn.Location = new System.Drawing.Point(81, 101);
            this.NewGenerateBtn.Name = "NewGenerateBtn";
            this.NewGenerateBtn.Size = new System.Drawing.Size(200, 86);
            this.NewGenerateBtn.TabIndex = 3;
            this.NewGenerateBtn.Text = "Create new repository project";
            this.NewGenerateBtn.UseVisualStyleBackColor = false;
            this.NewGenerateBtn.Click += new System.EventHandler(this.NewGenerateBtn_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(311, 214);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(175, 23);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 4;
            // 
            // LogBox
            // 
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LogBox.Location = new System.Drawing.Point(68, 3);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(665, 193);
            this.LogBox.TabIndex = 6;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ExitBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.ExitBtn.ForeColor = System.Drawing.Color.White;
            this.ExitBtn.Location = new System.Drawing.Point(327, 247);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(140, 38);
            this.ExitBtn.TabIndex = 7;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Visible = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // TwitterLink
            // 
            this.TwitterLink.AccessibleDescription = "Twitter";
            this.TwitterLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TwitterLink.Image = ((System.Drawing.Image)(resources.GetObject("TwitterLink.Image")));
            this.TwitterLink.Location = new System.Drawing.Point(298, 522);
            this.TwitterLink.Name = "TwitterLink";
            this.TwitterLink.Size = new System.Drawing.Size(71, 57);
            this.TwitterLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TwitterLink.TabIndex = 8;
            this.TwitterLink.TabStop = false;
            this.TwitterLink.Click += new System.EventHandler(this.TwitterLink_Click);
            // 
            // LinkedInLink
            // 
            this.LinkedInLink.AccessibleDescription = "LinkedIn";
            this.LinkedInLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LinkedInLink.Image = ((System.Drawing.Image)(resources.GetObject("LinkedInLink.Image")));
            this.LinkedInLink.Location = new System.Drawing.Point(375, 522);
            this.LinkedInLink.Name = "LinkedInLink";
            this.LinkedInLink.Size = new System.Drawing.Size(72, 57);
            this.LinkedInLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LinkedInLink.TabIndex = 9;
            this.LinkedInLink.TabStop = false;
            this.LinkedInLink.Click += new System.EventHandler(this.LinkedInLink_Click);
            // 
            // GitHubLink
            // 
            this.GitHubLink.AccessibleDescription = "GitHub";
            this.GitHubLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GitHubLink.Image = ((System.Drawing.Image)(resources.GetObject("GitHubLink.Image")));
            this.GitHubLink.Location = new System.Drawing.Point(453, 522);
            this.GitHubLink.Name = "GitHubLink";
            this.GitHubLink.Size = new System.Drawing.Size(72, 57);
            this.GitHubLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GitHubLink.TabIndex = 10;
            this.GitHubLink.TabStop = false;
            this.GitHubLink.Click += new System.EventHandler(this.GitHubLink_Click);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.AccessibleDescription = "Settings";
            this.SettingsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("SettingsBtn.Image")));
            this.SettingsBtn.Location = new System.Drawing.Point(769, 12);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(46, 38);
            this.SettingsBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SettingsBtn.TabIndex = 11;
            this.SettingsBtn.TabStop = false;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SettingsPanel.AutoSize = true;
            this.SettingsPanel.Controls.Add(this.SettingsRequiredField);
            this.SettingsPanel.Controls.Add(this.LabelVersion);
            this.SettingsPanel.Controls.Add(this.tableLayoutPanel1);
            this.SettingsPanel.Controls.Add(this.label7);
            this.SettingsPanel.Controls.Add(this.RepositoryTree);
            this.SettingsPanel.Controls.Add(this.SettingsLabel);
            this.SettingsPanel.Location = new System.Drawing.Point(9, 220);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(811, 302);
            this.SettingsPanel.TabIndex = 18;
            this.SettingsPanel.Visible = false;
            // 
            // SettingsRequiredField
            // 
            this.SettingsRequiredField.AutoSize = true;
            this.SettingsRequiredField.ForeColor = System.Drawing.Color.Red;
            this.SettingsRequiredField.Location = new System.Drawing.Point(123, 87);
            this.SettingsRequiredField.Name = "SettingsRequiredField";
            this.SettingsRequiredField.Size = new System.Drawing.Size(72, 13);
            this.SettingsRequiredField.TabIndex = 17;
            this.SettingsRequiredField.Text = "Invalid input";
            this.SettingsRequiredField.Visible = false;
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(4, 280);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(0, 13);
            this.LabelVersion.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.SettingsModelsName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(39, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(287, 28);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // SettingsModelsName
            // 
            this.SettingsModelsName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SettingsModelsName.Location = new System.Drawing.Point(181, 3);
            this.SettingsModelsName.Name = "SettingsModelsName";
            this.SettingsModelsName.Size = new System.Drawing.Size(100, 22);
            this.SettingsModelsName.TabIndex = 7;
            this.SettingsModelsName.Text = "Models";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Entity models folder name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(579, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 19);
            this.label7.TabIndex = 14;
            this.label7.Text = "Final structure";
            // 
            // RepositoryTree
            // 
            this.RepositoryTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RepositoryTree.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.RepositoryTree.Location = new System.Drawing.Point(507, 32);
            this.RepositoryTree.Name = "RepositoryTree";
            treeNode45.ForeColor = System.Drawing.Color.Green;
            treeNode45.Name = "IRepository";
            treeNode45.Text = "IRepository (Interface)";
            treeNode46.ForeColor = System.Drawing.Color.Green;
            treeNode46.Name = "EntityRepository";
            treeNode46.Text = "EntityRepository (Class)";
            treeNode47.ForeColor = System.Drawing.Color.Green;
            treeNode47.Name = "Repository";
            treeNode47.Text = "Repository (Folder)";
            treeNode48.ForeColor = System.Drawing.Color.Green;
            treeNode48.Name = "IViewModel";
            treeNode48.Text = "IViewModel (Interface)";
            treeNode49.ForeColor = System.Drawing.Color.Green;
            treeNode49.Name = "ModelViewModel";
            treeNode49.Text = "Model1ViewModel (Class)";
            treeNode50.ForeColor = System.Drawing.Color.Green;
            treeNode50.Name = "Node7";
            treeNode50.Text = "Model2ViewModel (Class)";
            treeNode51.ForeColor = System.Drawing.Color.Green;
            treeNode51.Name = "ViewModel";
            treeNode51.Text = "ViewModels (Folder)";
            treeNode52.Name = "Model1";
            treeNode52.Text = "Model1";
            treeNode53.Name = "Model2";
            treeNode53.Text = "Model2";
            treeNode54.Name = "Models";
            treeNode54.Text = "Models (Folder)";
            treeNode55.Name = "Repository_project";
            treeNode55.Text = "Repository (Project)";
            this.RepositoryTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode55});
            this.RepositoryTree.ShowPlusMinus = false;
            this.RepositoryTree.Size = new System.Drawing.Size(253, 246);
            this.RepositoryTree.TabIndex = 9;
            this.RepositoryTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.RepositoryTree_AfterCheck);
            // 
            // SettingsLabel
            // 
            this.SettingsLabel.AutoSize = true;
            this.SettingsLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.SettingsLabel.Location = new System.Drawing.Point(349, 13);
            this.SettingsLabel.Name = "SettingsLabel";
            this.SettingsLabel.Size = new System.Drawing.Size(76, 25);
            this.SettingsLabel.TabIndex = 4;
            this.SettingsLabel.Text = "Settings";
            // 
            // GoBackBtn
            // 
            this.GoBackBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GoBackBtn.Image = ((System.Drawing.Image)(resources.GetObject("GoBackBtn.Image")));
            this.GoBackBtn.Location = new System.Drawing.Point(769, 12);
            this.GoBackBtn.Name = "GoBackBtn";
            this.GoBackBtn.Size = new System.Drawing.Size(46, 38);
            this.GoBackBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GoBackBtn.TabIndex = 13;
            this.GoBackBtn.TabStop = false;
            this.GoBackBtn.Click += new System.EventHandler(this.GoBackBtn_Click);
            // 
            // SolutionNotFound
            // 
            this.SolutionNotFound.ForeColor = System.Drawing.Color.Red;
            this.SolutionNotFound.Location = new System.Drawing.Point(3, 250);
            this.SolutionNotFound.Name = "SolutionNotFound";
            this.SolutionNotFound.Size = new System.Drawing.Size(800, 13);
            this.SolutionNotFound.TabIndex = 17;
            this.SolutionNotFound.Text = "Solution not found";
            this.SolutionNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SolutionNotFound.Visible = false;
            // 
            // ExistingGenerateBtn
            // 
            this.ExistingGenerateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ExistingGenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExistingGenerateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ExistingGenerateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ExistingGenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExistingGenerateBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.ExistingGenerateBtn.ForeColor = System.Drawing.Color.White;
            this.ExistingGenerateBtn.Location = new System.Drawing.Point(530, 101);
            this.ExistingGenerateBtn.Name = "ExistingGenerateBtn";
            this.ExistingGenerateBtn.Size = new System.Drawing.Size(200, 86);
            this.ExistingGenerateBtn.TabIndex = 18;
            this.ExistingGenerateBtn.Text = "Add repository to existing project";
            this.ExistingGenerateBtn.UseVisualStyleBackColor = false;
            this.ExistingGenerateBtn.Click += new System.EventHandler(this.ExistingGenerateBtn_Click);
            // 
            // ProcessPanel
            // 
            this.ProcessPanel.Controls.Add(this.LogBox);
            this.ProcessPanel.Controls.Add(this.ExitBtn);
            this.ProcessPanel.Controls.Add(this.ProgressBar);
            this.ProcessPanel.Location = new System.Drawing.Point(9, 220);
            this.ProcessPanel.Name = "ProcessPanel";
            this.ProcessPanel.Size = new System.Drawing.Size(800, 302);
            this.ProcessPanel.TabIndex = 19;
            this.ProcessPanel.Visible = false;
            // 
            // PreNProcessPanel
            // 
            this.PreNProcessPanel.Controls.Add(this.ProjectExist);
            this.PreNProcessPanel.Controls.Add(this.NFieldsRequired);
            this.PreNProcessPanel.Controls.Add(this.tableLayoutPanel2);
            this.PreNProcessPanel.Controls.Add(this.Title);
            this.PreNProcessPanel.Controls.Add(this.NGenerateBtn);
            this.PreNProcessPanel.Location = new System.Drawing.Point(9, 220);
            this.PreNProcessPanel.Name = "PreNProcessPanel";
            this.PreNProcessPanel.Size = new System.Drawing.Size(800, 302);
            this.PreNProcessPanel.TabIndex = 20;
            this.PreNProcessPanel.Visible = false;
            // 
            // ProjectExist
            // 
            this.ProjectExist.AutoSize = true;
            this.ProjectExist.ForeColor = System.Drawing.Color.Red;
            this.ProjectExist.Location = new System.Drawing.Point(246, 198);
            this.ProjectExist.Name = "ProjectExist";
            this.ProjectExist.Size = new System.Drawing.Size(287, 13);
            this.ProjectExist.TabIndex = 23;
            this.ProjectExist.Text = "The solution already contains a project with that name";
            this.ProjectExist.Visible = false;
            // 
            // NFieldsRequired
            // 
            this.NFieldsRequired.AutoSize = true;
            this.NFieldsRequired.ForeColor = System.Drawing.Color.Red;
            this.NFieldsRequired.Location = new System.Drawing.Point(308, 198);
            this.NFieldsRequired.Name = "NFieldsRequired";
            this.NFieldsRequired.Size = new System.Drawing.Size(170, 13);
            this.NFieldsRequired.TabIndex = 22;
            this.NFieldsRequired.Text = "Both fields require a valid input";
            this.NFieldsRequired.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.NEdmxFileNameTxt, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.EdmxFileNameLbl, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ProjectNameLbl, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.NProjectNameTxt, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(248, 104);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(285, 56);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // NEdmxFileNameTxt
            // 
            this.NEdmxFileNameTxt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NEdmxFileNameTxt.Location = new System.Drawing.Point(105, 31);
            this.NEdmxFileNameTxt.Name = "NEdmxFileNameTxt";
            this.NEdmxFileNameTxt.Size = new System.Drawing.Size(167, 22);
            this.NEdmxFileNameTxt.TabIndex = 3;
            this.NEdmxFileNameTxt.Text = "Model.edmx";
            // 
            // EdmxFileNameLbl
            // 
            this.EdmxFileNameLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EdmxFileNameLbl.AutoSize = true;
            this.EdmxFileNameLbl.Location = new System.Drawing.Point(3, 35);
            this.EdmxFileNameLbl.Name = "EdmxFileNameLbl";
            this.EdmxFileNameLbl.Size = new System.Drawing.Size(87, 13);
            this.EdmxFileNameLbl.TabIndex = 2;
            this.EdmxFileNameLbl.Text = "EDMX file name";
            // 
            // ProjectNameLbl
            // 
            this.ProjectNameLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProjectNameLbl.AutoSize = true;
            this.ProjectNameLbl.Location = new System.Drawing.Point(10, 7);
            this.ProjectNameLbl.Name = "ProjectNameLbl";
            this.ProjectNameLbl.Size = new System.Drawing.Size(73, 13);
            this.ProjectNameLbl.TabIndex = 0;
            this.ProjectNameLbl.Text = "Project name";
            // 
            // NProjectNameTxt
            // 
            this.NProjectNameTxt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NProjectNameTxt.Location = new System.Drawing.Point(105, 3);
            this.NProjectNameTxt.Name = "NProjectNameTxt";
            this.NProjectNameTxt.Size = new System.Drawing.Size(167, 22);
            this.NProjectNameTxt.TabIndex = 1;
            this.NProjectNameTxt.Text = "Repository";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Title.Location = new System.Drawing.Point(307, 34);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(190, 19);
            this.Title.TabIndex = 0;
            this.Title.Text = "Create new repository project";
            // 
            // NGenerateBtn
            // 
            this.NGenerateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.NGenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NGenerateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.NGenerateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NGenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NGenerateBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.NGenerateBtn.ForeColor = System.Drawing.Color.White;
            this.NGenerateBtn.Location = new System.Drawing.Point(311, 243);
            this.NGenerateBtn.Name = "NGenerateBtn";
            this.NGenerateBtn.Size = new System.Drawing.Size(174, 46);
            this.NGenerateBtn.TabIndex = 21;
            this.NGenerateBtn.Text = "Generate";
            this.NGenerateBtn.UseVisualStyleBackColor = false;
            this.NGenerateBtn.Click += new System.EventHandler(this.NGenerateBtn_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.SolutionEmpty);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.NewGenerateBtn);
            this.MainPanel.Controls.Add(this.ExistingGenerateBtn);
            this.MainPanel.Controls.Add(this.SolutionNotFound);
            this.MainPanel.Location = new System.Drawing.Point(9, 220);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(803, 302);
            this.MainPanel.TabIndex = 21;
            // 
            // SolutionEmpty
            // 
            this.SolutionEmpty.ForeColor = System.Drawing.Color.Red;
            this.SolutionEmpty.Location = new System.Drawing.Point(0, 250);
            this.SolutionEmpty.Name = "SolutionEmpty";
            this.SolutionEmpty.Size = new System.Drawing.Size(800, 13);
            this.SolutionEmpty.TabIndex = 20;
            this.SolutionEmpty.Text = "The current solution doesn\'t contain any project";
            this.SolutionEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SolutionEmpty.Visible = false;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Solution not found";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // PreEProcessPanel
            // 
            this.PreEProcessPanel.Controls.Add(this.EFieldRequired);
            this.PreEProcessPanel.Controls.Add(this.tableLayoutPanel3);
            this.PreEProcessPanel.Controls.Add(this.label6);
            this.PreEProcessPanel.Controls.Add(this.EGenerateBtn);
            this.PreEProcessPanel.Location = new System.Drawing.Point(9, 220);
            this.PreEProcessPanel.Name = "PreEProcessPanel";
            this.PreEProcessPanel.Size = new System.Drawing.Size(811, 302);
            this.PreEProcessPanel.TabIndex = 24;
            this.PreEProcessPanel.Visible = false;
            // 
            // EFieldRequired
            // 
            this.EFieldRequired.AutoSize = true;
            this.EFieldRequired.ForeColor = System.Drawing.Color.Red;
            this.EFieldRequired.Location = new System.Drawing.Point(295, 198);
            this.EFieldRequired.Name = "EFieldRequired";
            this.EFieldRequired.Size = new System.Drawing.Size(206, 13);
            this.EFieldRequired.TabIndex = 22;
            this.EFieldRequired.Text = "Containing folder require a valid input";
            this.EFieldRequired.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.EModelFolder, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.EProjectNameCbx, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(253, 104);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(302, 56);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // EModelFolder
            // 
            this.EModelFolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EModelFolder.Location = new System.Drawing.Point(132, 30);
            this.EModelFolder.Name = "EModelFolder";
            this.EModelFolder.Size = new System.Drawing.Size(167, 22);
            this.EModelFolder.TabIndex = 3;
            this.EModelFolder.Text = "Models";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "EDMX containig folder";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Project name";
            // 
            // EProjectNameCbx
            // 
            this.EProjectNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EProjectNameCbx.FormattingEnabled = true;
            this.EProjectNameCbx.Location = new System.Drawing.Point(132, 3);
            this.EProjectNameCbx.Name = "EProjectNameCbx";
            this.EProjectNameCbx.Size = new System.Drawing.Size(167, 21);
            this.EProjectNameCbx.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(307, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Add repository to existing project";
            // 
            // EGenerateBtn
            // 
            this.EGenerateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.EGenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EGenerateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.EGenerateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EGenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EGenerateBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.EGenerateBtn.ForeColor = System.Drawing.Color.White;
            this.EGenerateBtn.Location = new System.Drawing.Point(311, 243);
            this.EGenerateBtn.Name = "EGenerateBtn";
            this.EGenerateBtn.Size = new System.Drawing.Size(174, 46);
            this.EGenerateBtn.TabIndex = 21;
            this.EGenerateBtn.Text = "Generate";
            this.EGenerateBtn.UseVisualStyleBackColor = false;
            this.EGenerateBtn.Click += new System.EventHandler(this.EGenerateBtn_Click);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 586);
            this.Controls.Add(this.SettingsBtn);
            this.Controls.Add(this.GoBackBtn);
            this.Controls.Add(this.SettingsPanel);
            this.Controls.Add(this.PreNProcessPanel);
            this.Controls.Add(this.PreEProcessPanel);
            this.Controls.Add(this.ProcessPanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Header1);
            this.Controls.Add(this.Header2);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.LinkedInLink);
            this.Controls.Add(this.TwitterLink);
            this.Controls.Add(this.GitHubLink);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(840, 625);
            this.MinimumSize = new System.Drawing.Size(840, 625);
            this.Name = "MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RPG";
            this.Load += new System.EventHandler(this.MainDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwitterLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkedInLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GitHubLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsBtn)).EndInit();
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoBackBtn)).EndInit();
            this.ProcessPanel.ResumeLayout(false);
            this.PreNProcessPanel.ResumeLayout(false);
            this.PreNProcessPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.PreEProcessPanel.ResumeLayout(false);
            this.PreEProcessPanel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label Header1;
        private System.Windows.Forms.Label Header2;
        private System.Windows.Forms.Button NewGenerateBtn;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.PictureBox TwitterLink;
        private System.Windows.Forms.PictureBox LinkedInLink;
        private System.Windows.Forms.PictureBox GitHubLink;
        private System.Windows.Forms.PictureBox SettingsBtn;
        private System.Windows.Forms.PictureBox GoBackBtn;
        private System.Windows.Forms.Label SettingsLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SettingsModelsName;
        private System.Windows.Forms.TreeView RepositoryTree;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label SolutionNotFound;
        private System.Windows.Forms.Button ExistingGenerateBtn;
        private System.Windows.Forms.Panel ProcessPanel;
        private System.Windows.Forms.Panel PreNProcessPanel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label ProjectNameLbl;
        private System.Windows.Forms.TextBox NProjectNameTxt;
        private System.Windows.Forms.TextBox NEdmxFileNameTxt;
        private System.Windows.Forms.Label EdmxFileNameLbl;
        private System.Windows.Forms.Button NGenerateBtn;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label NFieldsRequired;
        private System.Windows.Forms.Label SettingsRequiredField;
        private System.Windows.Forms.Label ProjectExist;
        private System.Windows.Forms.Panel PreEProcessPanel;
        private System.Windows.Forms.Label EFieldRequired;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox EModelFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button EGenerateBtn;
        private System.Windows.Forms.ComboBox EProjectNameCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SolutionEmpty;
    }
}