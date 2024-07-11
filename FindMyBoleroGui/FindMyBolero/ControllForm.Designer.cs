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
            ((System.ComponentModel.ISupportInitialize)dgV1).BeginInit();
            SuspendLayout();
            // 
            // dgV1
            // 
            dgV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgV1.Columns.AddRange(new DataGridViewColumn[] { SetActive });
            dgV1.Location = new Point(12, 12);
            dgV1.Name = "dgV1";
            dgV1.RowTemplate.Height = 25;
            dgV1.Size = new Size(776, 426);
            dgV1.TabIndex = 0;
            dgV1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // SetActive
            // 
            SetActive.HeaderText = "Set Active";
            SetActive.Name = "SetActive";
            SetActive.ReadOnly = true;
            // 
            // pingTimer
            // 
            pingTimer.Interval = 60000;
            pingTimer.Tick += timer1_Tick;
            // 
            // ControllForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgV1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ControllForm";
            Text = "FindMyBolero";
            FormClosing += ControllForm_FormClosing;
            Load += ControllForm_Load;
            Leave += ControllForm_Leave;
            ((System.ComponentModel.ISupportInitialize)dgV1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgV1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn AntennaName;
        private DataGridViewTextBoxColumn Ip;
        private DataGridViewTextBoxColumn State;
        private DataGridViewButtonColumn SetActive;
        private System.Windows.Forms.Timer pingTimer;
    }
}