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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("IRepository (Interface)");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("EntityRepository (Class)");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Repository (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("IViewModel (Interface)");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Model1ViewModel (Class)");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Model2ViewModel (Class)");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ViewModels (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Model1");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Model2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Models (Folder)", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Repository (Project)", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode7,
            treeNode10});
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
            this.LabelVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.SettingsModelsName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SettingsRepositoryName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RepositoryTree = new System.Windows.Forms.TreeView();
            this.SettingsLabel = new System.Windows.Forms.Label();
            this.GoBackBtn = new System.Windows.Forms.PictureBox();
            this.SolutionNotFound = new System.Windows.Forms.Label();
            this.ExistingGenerateBtn = new System.Windows.Forms.Button();
            this.ProcessPanel = new System.Windows.Forms.Panel();
            this.PreProcessPanel = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ProjectNameLbl = new System.Windows.Forms.Label();
            this.NProjectNameTxt = new System.Windows.Forms.TextBox();
            this.EdmxFileNameLbl = new System.Windows.Forms.Label();
            this.NEdmxFileNameTxt = new System.Windows.Forms.TextBox();
            this.NGenerateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwitterLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkedInLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GitHubLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsBtn)).BeginInit();
            this.SettingsPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoBackBtn)).BeginInit();
            this.ProcessPanel.SuspendLayout();
            this.PreProcessPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.NewGenerateBtn.Location = new System.Drawing.Point(80, 327);
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
            this.SettingsPanel.Controls.Add(this.LabelVersion);
            this.SettingsPanel.Controls.Add(this.tableLayoutPanel1);
            this.SettingsPanel.Controls.Add(this.label7);
            this.SettingsPanel.Controls.Add(this.RepositoryTree);
            this.SettingsPanel.Controls.Add(this.SettingsLabel);
            this.SettingsPanel.Location = new System.Drawing.Point(12, 223);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(803, 293);
            this.SettingsPanel.TabIndex = 12;
            this.SettingsPanel.Visible = false;
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
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SettingsModelsName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SettingsRepositoryName, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(69, 109);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(287, 56);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository project name";
            // 
            // SettingsModelsName
            // 
            this.SettingsModelsName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SettingsModelsName.Location = new System.Drawing.Point(181, 31);
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
            this.label4.Location = new System.Drawing.Point(3, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Entity Models folder name";
            // 
            // SettingsRepositoryName
            // 
            this.SettingsRepositoryName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SettingsRepositoryName.Location = new System.Drawing.Point(181, 3);
            this.SettingsRepositoryName.Name = "SettingsRepositoryName";
            this.SettingsRepositoryName.Size = new System.Drawing.Size(100, 22);
            this.SettingsRepositoryName.TabIndex = 5;
            this.SettingsRepositoryName.Text = "Repository";
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
            treeNode1.ForeColor = System.Drawing.Color.Green;
            treeNode1.Name = "IRepository";
            treeNode1.Text = "IRepository (Interface)";
            treeNode2.ForeColor = System.Drawing.Color.Green;
            treeNode2.Name = "EntityRepository";
            treeNode2.Text = "EntityRepository (Class)";
            treeNode3.ForeColor = System.Drawing.Color.Green;
            treeNode3.Name = "Repository";
            treeNode3.Text = "Repository (Folder)";
            treeNode4.ForeColor = System.Drawing.Color.Green;
            treeNode4.Name = "IViewModel";
            treeNode4.Text = "IViewModel (Interface)";
            treeNode5.ForeColor = System.Drawing.Color.Green;
            treeNode5.Name = "ModelViewModel";
            treeNode5.Text = "Model1ViewModel (Class)";
            treeNode6.ForeColor = System.Drawing.Color.Green;
            treeNode6.Name = "Node7";
            treeNode6.Text = "Model2ViewModel (Class)";
            treeNode7.ForeColor = System.Drawing.Color.Green;
            treeNode7.Name = "ViewModel";
            treeNode7.Text = "ViewModels (Folder)";
            treeNode8.Name = "Model1";
            treeNode8.Text = "Model1";
            treeNode9.Name = "Model2";
            treeNode9.Text = "Model2";
            treeNode10.Name = "Models";
            treeNode10.Text = "Models (Folder)";
            treeNode11.Name = "Repository_project";
            treeNode11.Text = "Repository (Project)";
            this.RepositoryTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
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
            this.SolutionNotFound.Location = new System.Drawing.Point(12, 471);
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
            this.ExistingGenerateBtn.Location = new System.Drawing.Point(545, 327);
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
            this.ProcessPanel.Location = new System.Drawing.Point(12, 223);
            this.ProcessPanel.Name = "ProcessPanel";
            this.ProcessPanel.Size = new System.Drawing.Size(800, 293);
            this.ProcessPanel.TabIndex = 19;
            this.ProcessPanel.Visible = false;
            // 
            // PreProcessPanel
            // 
            this.PreProcessPanel.Controls.Add(this.tableLayoutPanel2);
            this.PreProcessPanel.Controls.Add(this.Title);
            this.PreProcessPanel.Controls.Add(this.NGenerateBtn);
            this.PreProcessPanel.Location = new System.Drawing.Point(80, 230);
            this.PreProcessPanel.Name = "PreProcessPanel";
            this.PreProcessPanel.Size = new System.Drawing.Size(665, 286);
            this.PreProcessPanel.TabIndex = 20;
            this.PreProcessPanel.Visible = false;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Title.Location = new System.Drawing.Point(253, 11);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(190, 19);
            this.Title.TabIndex = 0;
            this.Title.Text = "Create new repository project";
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(218, 47);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(285, 56);
            this.tableLayoutPanel2.TabIndex = 1;
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
            // NEdmxFileNameTxt
            // 
            this.NEdmxFileNameTxt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NEdmxFileNameTxt.Location = new System.Drawing.Point(105, 31);
            this.NEdmxFileNameTxt.Name = "NEdmxFileNameTxt";
            this.NEdmxFileNameTxt.Size = new System.Drawing.Size(167, 22);
            this.NEdmxFileNameTxt.TabIndex = 3;
            this.NEdmxFileNameTxt.Text = "Model";
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
            this.NGenerateBtn.Location = new System.Drawing.Point(243, 241);
            this.NGenerateBtn.Name = "NGenerateBtn";
            this.NGenerateBtn.Size = new System.Drawing.Size(174, 39);
            this.NGenerateBtn.TabIndex = 21;
            this.NGenerateBtn.Text = "Generate";
            this.NGenerateBtn.UseVisualStyleBackColor = false;
            this.NGenerateBtn.Click += new System.EventHandler(this.NGenerateBtn_Click);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 586);
            this.Controls.Add(this.SettingsPanel);
            this.Controls.Add(this.ProcessPanel);
            this.Controls.Add(this.PreProcessPanel);
            this.Controls.Add(this.ExistingGenerateBtn);
            this.Controls.Add(this.SettingsBtn);
            this.Controls.Add(this.GoBackBtn);
            this.Controls.Add(this.GitHubLink);
            this.Controls.Add(this.LinkedInLink);
            this.Controls.Add(this.TwitterLink);
            this.Controls.Add(this.Header1);
            this.Controls.Add(this.Header2);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.NewGenerateBtn);
            this.Controls.Add(this.SolutionNotFound);
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
            this.PreProcessPanel.ResumeLayout(false);
            this.PreProcessPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SettingsRepositoryName;
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
        private System.Windows.Forms.Panel PreProcessPanel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label ProjectNameLbl;
        private System.Windows.Forms.TextBox NProjectNameTxt;
        private System.Windows.Forms.TextBox NEdmxFileNameTxt;
        private System.Windows.Forms.Label EdmxFileNameLbl;
        private System.Windows.Forms.Button NGenerateBtn;
    }
}