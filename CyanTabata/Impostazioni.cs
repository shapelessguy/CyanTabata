using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public partial class Impostazioni : Form
    {
        static public bool imp_on = false;
        static public Impostazioni impostazioni;


        int max_char = 50;
        public Impostazioni()
        {
            InitializeComponent();
            imp_on = true;
            impostazioni = this;
            label2.Text = Archivio.video_path;
            if (label2.Text == "" || label2.Text.Length < 2) { label2.Text = @"C:\"; Archivio.video_path=label2.Text; }
            if (label2.Text.Length > max_char) label2.Text = Archivio.video_path.Substring(0, max_char) + "...";
            tooltip.SetToolTip(label2, Archivio.video_path);
            checkBox1.Checked = Archivio.spartano;
            Show();
            FormClosing += ClosingForm;
            trackBar1.Value = Properties.Settings.Default.vol;
        }

        private FolderBrowserDialog folderBrowserDialog1;
        private ToolTip tooltip = new ToolTip();
        private void label2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Archivio.video_path = folderBrowserDialog1.SelectedPath;
                    label2.Text = Archivio.video_path;
                    tooltip.SetToolTip(label2, Archivio.video_path);
                    if (label2.Text.Length > max_char) label2.Text = Archivio.video_path.Substring(0, max_char) + "...";
                }
                catch (Exception) { MessageBox.Show("Non è possibile selezionare questa cartella a causa di mancata autorizzazione"); }
            }
        }

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) Archivio.spartano = true;
            else Archivio.spartano = false;
        }
        private void ClosingForm(object sender, EventArgs e)
        {
            imp_on = false;
        }
        public static void SetVolumeApplication(int NewVolume)
        {
            NewVolume = NewVolume * 650;
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private void trackBar1_DragLeave(object sender, EventArgs e)
        {
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            SetVolumeApplication(trackBar1.Value);
            Properties.Settings.Default.vol = trackBar1.Value;
            Properties.Settings.Default.Save();
        }
    }
}
