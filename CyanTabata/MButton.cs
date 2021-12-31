using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class MButton : Button
    {
        int indice;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem modifica;
        Button Up, Down, Erase;
        public MButton(int indice, int modo)
        {
            this.indice = indice;

            if (modo == 2)
            {
                Up = new Button() { Visible = true, Text = "\u25b2", };
                Controls.Add(Up);
                Up.Click += Up_Click;
                Down = new Button() { Visible = true, Text = "\u25bc", };
                Controls.Add(Down);
                Down.Click += Down_Click;
                Erase = new Button() { Visible = true, Text = "X", };
                Controls.Add(Erase);
                Erase.Click += Erase_Click;
            }
            contextMenuStrip = new ContextMenuStrip
            {
                ImageScalingSize = new System.Drawing.Size(24, 24),
                Name = "contextMenuStrip1",
                Size = new System.Drawing.Size(141, 34)
            };
            modifica = new ToolStripMenuItem
            {
                Size = new System.Drawing.Size(140, 30),
                Text = "Modifica"
            };
            modifica.Click += Click_Modifica;
            contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { modifica });
            if(modo==2) ContextMenuStrip = contextMenuStrip;
        }
        private void Click_Modifica(object sender, EventArgs e)
        {
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Show();
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Text = Program.principale.panel_schede.panel_schede.bottoni[indice].Text;
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Location = Program.principale.panel_schede.panel_schede.bottoni[indice].Location;
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Size = Program.principale.panel_schede.panel_schede.bottoni[indice].Size;
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Font = Program.principale.panel_schede.panel_schede.bottoni[indice].Font;
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].BringToFront();
        }
        public void SemiSaveModifica()
        {
            Program.principale.panel_schede.panel_schede.bottoni_text[indice].Hide();
            string name = Program.principale.panel_schede.panel_schede.bottoni_text[indice].Text;
            name = name.Replace("'", "^").Replace("\n", ", ");
            bool same = false;
            foreach(Scheda scheda in Pannello1.arc.Schede)
            {
                if (scheda.nome_scheda == name) same = true;
            }
            if (same) return;
            foreach (Scheda scheda in Pannello1.arc.Schede)
            {
                if (scheda.nome_scheda == Text) scheda.nome_scheda = name;
            }
            Text = name;
        }
        private void Up_Click(object sender, EventArgs e)
        {
            if (indice == 0) return;
            Scheda scheda = Pannello1.arc.Schede[indice];
            Pannello1.arc.Schede.RemoveAt(indice);
            Pannello1.arc.Schede.Insert(indice - 1, scheda);
            Program.principale.panel_schede.Aggiorna();
            Program.principale.panel_principale.Aggiorna_PannelloSchede();
        }
        private void Down_Click(object sender, EventArgs e)
        {
            if (indice == Program.principale.panel_schede.panel_schede.bottoni.Count - 1) return;
            Scheda scheda = Pannello1.arc.Schede[indice];
            Pannello1.arc.Schede.RemoveAt(indice);
            Pannello1.arc.Schede.Insert(indice + 1, scheda);
            Program.principale.panel_schede.Aggiorna();
            Program.principale.panel_principale.Aggiorna_PannelloSchede();
        }
        private void Erase_Click(object sender, EventArgs e)
        {
            Pannello1.arc.Schede.RemoveAt(indice);
            Program.principale.panel_schede.Aggiorna();
            Program.principale.panel_principale.Aggiorna_PannelloSchede();
        }
        public void ResizeElements()
        {
            Up.Height = (int)(Height*0.8);
            Up.Width = Up.Height;
            Down.Size = Up.Size;
            Erase.Size = Up.Size;
            Up.Location = new System.Drawing.Point(Width - Up.Width * 2, (int)(Height - Up.Height) / 2);
            Down.Location = new System.Drawing.Point((int)(Width - Up.Width * 3), (int)(Height - Up.Height) / 2);
            Erase.Location = new System.Drawing.Point((int)(Up.Width * 1), (int)(Height - Up.Height) / 2);
        }
    }
}
