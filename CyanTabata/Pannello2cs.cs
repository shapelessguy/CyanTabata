using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Pannello2 : Panel
    {
        public Pannello_Schede panel_schede;
        public Pannello2()
        {
            Aggiorna();
            Click += ClickNull;
        }

        public void Aggiorna()
        {
            Controls.Remove(panel_schede);
            panel_schede = new Pannello_Schede(Pannello1.arc.Schede, 2);
            Controls.Add(panel_schede);
            LocationChanging();
        }


        public void LocationChanging()
        {
            panel_schede.Location = new Point(0, 33 + (int)(Height* 0.02));
            panel_schede.Size = new Size((int)(Width * 0.97), (int)(Height * 0.9));
            panel_schede.Resize_Elements();
        }

        public void ClickNull(object sender, EventArgs e)
        {
            panel_schede.Modificato(null, null);
        }
    }
}
