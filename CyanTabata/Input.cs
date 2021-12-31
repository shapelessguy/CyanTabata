using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Input
    {
        static public int[] data_attuale = Date.GetActualDate();
        static public int[] data_utile = new int[6];
        static public Size Schermo = new Size(3840, 2160);
        static public int prova = 0;
        static public bool end_input = false;
        static Point ora_minuto = new Point(5, 0);
        public Input()
        {
            RefreshData();
            end_input = true;
        }
        static public void RefreshData()
        {
            data_attuale = Date.GetActualDate();
            RefreshDataUtile();
        }
        static public void RefreshDataUtile()
        {
            if (data_attuale[2] < ora_minuto.X) { data_utile = Date.DataIncrement(data_attuale, -60 * 60 * 24); data_utile[0] = 0; data_utile[1] = 59; data_utile[2] = 23; }
            else if (data_attuale[2] == ora_minuto.X && data_attuale[1] < ora_minuto.Y) { data_utile = Date.DataIncrement(data_attuale, -60 * 60 * 24); data_utile[0] = 0; data_utile[1] = 59; data_utile[2] = 23; }
            else data_utile = data_attuale;
        }
        
    }
}
