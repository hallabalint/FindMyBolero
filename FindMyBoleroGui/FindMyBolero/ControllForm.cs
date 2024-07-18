

using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FindMyBolero
{
    public partial class ControllForm : Form
    {
        private event EventHandler quitProgram;
        public ControllForm()
        {
            InitializeComponent();
            quitProgram += delegate (object sender, EventArgs e)
            {
                Caller.cf.Dispose();
            };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                Caller.Active = Caller.antennas[e.RowIndex];
                dgV1.Refresh();
            }
        }

        private void ControllForm_Load(object sender, EventArgs e)
        {
            NotifyIcon icon = new NotifyIcon();
            var resources = new ResourceManager(typeof(ControllForm));
            icon.Icon = (Icon)resources.GetObject("$this.Icon");
            icon.DoubleClick += (e, o) => this.Show();
            icon.Visible = true;
            icon.ContextMenuStrip = new ContextMenuStrip();
            icon.ContextMenuStrip.Items.Add("Quit", null, quitProgram);
            dgV1.DataSource = Caller.antennas;
            DataRefreh();
            Task.Run(() => Caller.PingAntennas());

        }
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        private void ControllForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Task.Run(() => Caller.PingAntennas());
        }
        public void DataRefreh()
        {

            for (int i = 0; i < Caller.antennas.Count; i++)
            {
                var row = dgV1.Rows[i];
                if (Caller.antennas[i].IsOnline)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = DefaultBackColor;
                }

            }
            dgV1.Refresh();

        }

        private static void openUrl(string url) => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });

        private void label1_Click(object sender, EventArgs e)
        {
            openUrl("https://github.com/hallabalint/findmybolero");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            openUrl("https://github.com/hallabalint/findmybolero/issues");
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() => Caller.PingAntennas());
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsActiveAntenna_Click(object sender, EventArgs e)
        {
            openUrl("http://" + Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("FindMyBolero").GetValue("bind").ToString() + "/");
         }
    }
}
