using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Set_pulsanti : Panel
    {
        public int indexx;
        Label Su;
        Label Giu;
        Label Piu;
        Label Elimina;
        public Set_pulsanti(int index)
        {
            indexx = index;
            Su = new Label()
            {
                Visible = true,
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(Pannello_Schede.altezzatext, Pannello_Schede.altezzatext),
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Capture = false,
                Text = "\u25b2",
            };
            Controls.Add(Su);
            Giu = new Label()
            {
                Visible = true,
                Location = new System.Drawing.Point(Pannello_Schede.altezzatext, 0),
                Size = new System.Drawing.Size(Pannello_Schede.altezzatext, Pannello_Schede.altezzatext),
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "\u25bc",
            };
            Controls.Add(Giu);
            Piu = new Label()
            {
                Visible = true,
                Location = new System.Drawing.Point(Pannello_Schede.altezzatext * 2, 0),
                Size = new System.Drawing.Size(Pannello_Schede.altezzatext, Pannello_Schede.altezzatext),
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "+",
            };
            Controls.Add(Piu);
            Elimina = new Label()
            {
                Visible = true,
                Location = new System.Drawing.Point(Pannello_Schede.altezzatext*3, 0),
                Size = new System.Drawing.Size(Pannello_Schede.altezzatext, Pannello_Schede.altezzatext),
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "X",
            };
            Controls.Add(Elimina);
        }
    }
}
