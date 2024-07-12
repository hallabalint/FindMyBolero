namespace FindMyBolero
{
    partial class ControllForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllForm));
            dgV1 = new DataGridView();
            SetActive = new DataGridViewButtonColumn();
            pingTimer = new System.Windows.Forms.Timer(components);
            bottomPanel = new Panel();
            label2 = new Label();
            label1 = new Label();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)dgV1).BeginInit();
            bottomPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgV1
            // 
            dgV1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgV1.Columns.AddRange(new DataGridViewColumn[] { SetActive });
            dgV1.Location = new Point(12, 27);
            dgV1.Name = "dgV1";
            dgV1.RowTemplate.Height = 25;
            dgV1.Size = new Size(776, 398);
            dgV1.TabIndex = 0;
            dgV1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // SetActive
            // 
            SetActive.HeaderText = "Set Active";
            SetActive.Name = "SetActive";
            SetActive.ReadOnly = true;
            SetActive.Text = "Set Active";
            SetActive.UseColumnTextForButtonValue = true;
            // 
            // pingTimer
            // 
            pingTimer.Enabled = true;
            pingTimer.Interval = 60000;
            pingTimer.Tick += timer1_Tick;
            // 
            // bottomPanel
            // 
            bottomPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bottomPanel.Controls.Add(label2);
            bottomPanel.Controls.Add(label1);
            bottomPanel.Location = new Point(2, 431);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(796, 23);
            bottomPanel.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(702, 1);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 1;
            label2.Text = "Report issue";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(10, 3);
            label1.Name = "label1";
            label1.Size = new Size(208, 15);
            label1.TabIndex = 0;
            label1.Text = "github.com/hallabalint/FindMyBolero";
            label1.Click += label1_Click;
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(58, 20);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(42, 20);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, quitToolStripMenuItem });
            menuStrip1.Location = new Point(2, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(108, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // ControllForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 454);
            Controls.Add(bottomPanel);
            Controls.Add(dgV1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "ControllForm";
            Text = "FindMyBolero";
            FormClosing += ControllForm_FormClosing;
            Load += ControllForm_Load;
            Leave += ControllForm_Leave;
            ((System.ComponentModel.ISupportInitialize)dgV1).EndInit();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgV1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn AntennaName;
        private DataGridViewTextBoxColumn Ip;
        private DataGridViewTextBoxColumn State;
        private System.Windows.Forms.Timer pingTimer;
        private Panel bottomPanel;
        private Label label2;
        private Label label1;
        private DataGridViewButtonColumn SetActive;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private MenuStrip menuStrip1;
    }
}