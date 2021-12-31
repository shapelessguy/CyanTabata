using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyanTabata
{
    class Date
    {
        public static int[] DataIncrement(int[] data, int secondsincrement)
        {
            int codifica = (int)Codifica(data);
            codifica += secondsincrement;
            data = Decodifica((uint)codifica);
            return (data);
        }

        public static int[] GetActualDate()
        {
            return new int[] { DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year };
        }
        public static long Codifica(int[] data)
        {
            long codifica = 0;
            int secondo = data[0];
            int minuto = data[1];
            int ora = data[2];
            int giorno = data[3];
            int mese = data[4];
            int anno = data[5];
            int nmese = 0, ngiorno = 0, nora = 0, nminuto = 0, nsecondo = 0;
            long nanno = 0;
            int a = 0, b = 0, c = 0;
            switch (mese)
            {
                case 2:
                    b = 31;
                    break;
                case 3:
                    b = 59;
                    break;
                case 4:
                    b = 90;
                    break;
                case 5:
                    b = 120;
                    break;
                case 6:
                    b = 151;
                    break;
                case 7:
                    b = 181;
                    break;
                case 8:
                    b = 212;
                    break;
                case 9:
                    b = 243;
                    break;
                case 10:
                    b = 273;
                    break;
                case 11:
                    b = 304;
                    break;
                case 12:
                    b = 334;
                    break;
                default:
                    b = 0;
                    break;
            }
            if (anno >= 2000)
            {
                if (mese <= 2) { if (Math.Floor((double)(anno - 2000) / 4) == (double)(anno - 2000) / 4) { a = -1; } }
                nmese = (1 + (int)Math.Floor((double)(anno - 2000) / 4) + a + b) * 24 * 60 * 60;
                nanno = (anno - 2000) * 365 * 24 * 60 * 60;
                ngiorno = (giorno - 1) * 24 * 60 * 60;
                nora = ora * 60 * 60;
                nminuto = minuto * 60;
                nsecondo = secondo;
            }
            else
            {
                nsecondo = -86400 + (secondo + minuto + ora);
                nanno = (1999 - anno) * 365 * 86400;
                if (Math.Floor((double)((2000 - anno) / 4)) == (double)((2000 - anno) / 4)) { a = 1; }
                if (mese > 2) { c = 1; }
                nmese = (-b - giorno - a * c - 365 - a + 1) * 86400;
                nanno = -(365 + a - 1);
            }
            codifica = (long)(nsecondo + nminuto + nora + ngiorno + nmese + nanno);
            return (codifica);
        }
        public static int ContaGiorni(int mese, int anno)
        {

            if (mese == 1) { return 31; }
            if (mese == 2) { return IsBisestile(anno) ? 29 : 28; }
            if (mese == 3) { return 31; }
            if (mese == 4) { return 30; }
            if (mese == 5) { return 31; }
            if (mese == 6) { return 30; }
            if (mese == 7) { return 31; }
            if (mese == 8) { return 31; }
            if (mese == 9) { return 30; }
            if (mese == 10) { return 31; }
            if (mese == 11) { return 30; }
            if (mese == 12) { return 31; }
            return 0;
        }
        public static bool IsBisestile(int anno)
        {
            bool result = false;
            if (anno % 4 == 0) { result = true; }
            return result;

        }
        public static string GetGiornosettimanatxt(int[] data)
        {
            string giornosettimanatxt = "";
            uint valorenumerico = (uint)Codifica(data);
            double totgiorni = Math.Floor((double)valorenumerico / 60 / 60 / 24);
            double giornosettimana = Math.Round(((totgiorni / 7) - Math.Floor(totgiorni / 7)) * 7);
            if (giornosettimana == 0) { giornosettimanatxt = "Sabato"; }
            if (giornosettimana == 1) { giornosettimanatxt = "Domenica"; }
            if (giornosettimana == 2) { giornosettimanatxt = "Lunedì"; }
            if (giornosettimana == 3) { giornosettimanatxt = "Martedì"; }
            if (giornosettimana == 4) { giornosettimanatxt = "Mercoledì"; }
            if (giornosettimana == 5) { giornosettimanatxt = "Giovedì"; }
            if (giornosettimana == 6) { giornosettimanatxt = "Venerdì"; }
            //BFS.Dialog.ShowMessageInfo(Convert.ToString(giornosettimanatxt));
            return (giornosettimanatxt);
        }

        public static int GetIntfromGiornoSettimana(string giornosettimana)
        {
            int valore = 0;
            if (giornosettimana == "Lunedì") valore = 0;
            if (giornosettimana == "Martedì") valore = 1;
            if (giornosettimana == "Mercoledì") valore = 2;
            if (giornosettimana == "Giovedì") valore = 3;
            if (giornosettimana == "Venerdì") valore = 4;
            if (giornosettimana == "Sabato") valore = 5;
            if (giornosettimana == "Domenica") valore = 6;
            return valore;
        }
        public static int[] Decodifica(uint decodifica)
        {
            double valorenumerico = (double)decodifica;
            int[] valoridata = new int[6];
            int totgiorni = (int)Math.Floor(valorenumerico / 60 / 60 / 24);
            int feb29 = 0;
            if (Math.Floor((double)(totgiorni - 59) / 1461) == (double)(totgiorni - 59) / 1461) { feb29 = 1; }
            int passati29feb = (int)Math.Floor((double)(totgiorni - 59) / 1461) + 1 - feb29;
            int inquadrianno = totgiorni - (passati29feb - 1) * 1461;
            int inanno = inquadrianno - (int)Math.Floor((double)(inquadrianno - 1) / 365) * 365;
            int anno = (passati29feb - 1) * 4 + (int)Math.Floor((double)(inquadrianno - 1) / 365) + 2000;
            int mese = 0, meselisten = 0;
            if (feb29 == 1) { mese = 2; meselisten = 1; }
            if (meselisten == 1) {; } else { if (inanno > 334) { mese = 12; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 304) { mese = 11; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 273) { mese = 10; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 243) { mese = 9; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 212) { mese = 8; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 181) { mese = 7; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 151) { mese = 6; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 120) { mese = 5; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 90) { mese = 4; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 59) { mese = 3; meselisten = 1; } }
            if (meselisten == 1) {; } else { if (inanno > 31) { mese = 2; meselisten = 1; } }
            if (meselisten == 1) {; } else { mese = 1; meselisten = 1; }
            int giorno = 0, giornolisten = 0;
            if (feb29 == 1) { giorno = 29; giornolisten = 1; }
            if (giornolisten == 1) {; } else { if (inanno > 334) { giorno = inanno - 334; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 304) { giorno = inanno - 304; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 273) { giorno = inanno - 273; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 243) { giorno = inanno - 243; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 212) { giorno = inanno - 212; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 181) { giorno = inanno - 181; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 151) { giorno = inanno - 151; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 120) { giorno = inanno - 120; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 90) { giorno = inanno - 90; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 59) { giorno = inanno - 59; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { if (inanno > 31) { giorno = inanno - 31; giornolisten = 1; } }
            if (giornolisten == 1) {; } else { giorno = inanno; giornolisten = 1; }
            int ora = (int)Math.Floor((double)(valorenumerico - (long)totgiorni * 24 * 3600) / 3600);
            int minuto = (int)Math.Floor((double)(valorenumerico - ora * 60 * 60 - (long)(totgiorni) * 24 * 60 * 60) / 60);
            int secondo = (int)(valorenumerico - minuto * 60 - ora * 60 * 60 - (long)(totgiorni) * 24 * 60 * 60);
            valoridata[0] = secondo;
            valoridata[1] = minuto;
            valoridata[2] = ora;
            valoridata[3] = giorno;
            valoridata[4] = mese;
            valoridata[5] = anno;
            return (valoridata);
        }
        public static string GetMesetxt(int mese)
        {
            string mesetxt = "";
            if (mese == 1) { mesetxt = "Gennaio"; }
            if (mese == 2) { mesetxt = "Febbraio"; }
            if (mese == 3) { mesetxt = "Marzo"; }
            if (mese == 4) { mesetxt = "Aprile"; }
            if (mese == 5) { mesetxt = "Maggio"; }
            if (mese == 6) { mesetxt = "Giugno"; }
            if (mese == 7) { mesetxt = "Luglio"; }
            if (mese == 8) { mesetxt = "Agosto"; }
            if (mese == 9) { mesetxt = "Settembre"; }
            if (mese == 10) { mesetxt = "Ottobre"; }
            if (mese == 11) { mesetxt = "Novembre"; }
            if (mese == 12) { mesetxt = "Dicembre"; }
            return (mesetxt);

        }
        public static string Periodo_DataData(int[] data1, int[] data2)
        {
            long differenza = 0;
            differenza = Codifica(data2) - Codifica(data1);
            int[] valore = new int[] { 0, 0, 0 };
            if (differenza >= 60 * 60 * 24 * 365) { valore[0] = 0; valore[1] = (int)Math.Floor((decimal)differenza / (60 * 60 * 24 * 365)); valore[2] = (int)Math.Floor((decimal)(differenza - valore[1] * 60 * 60 * 24 * 365) / (60 * 60 * 24 * 30)); }
            else if (differenza >= 60 * 60 * 24 * 30) { valore[0] = 1; valore[1] = (int)Math.Floor((decimal)differenza / (60 * 60 * 24 * 30)); valore[2] = (int)Math.Floor((decimal)(differenza - valore[1] * 60 * 60 * 24 * 30) / (60 * 60 * 24)); }
            else if (differenza >= 60 * 60 * 24) { valore[0] = 2; valore[1] = (int)Math.Floor((decimal)differenza / (60 * 60 * 24)); valore[2] = (int)Math.Floor((decimal)(differenza - valore[1] * 60 * 60 * 24) / (60 * 60)); }
            else if (differenza >= 60 * 60) { valore[0] = 3; valore[1] = (int)Math.Floor((decimal)differenza / (60 * 60)); valore[2] = (int)Math.Floor((decimal)(differenza - valore[1] * 60 * 60) / 60); }
            else { valore[0] = 4; valore[1] = (int)Math.Floor((decimal)differenza / 60); valore[2] = (int)Math.Floor((decimal)(differenza - valore[1] * 60)); }

            string stringa1 = "", stringa2 = "";
            if (valore[0] == 0) { if (valore[1] == 1) stringa1 = "anno"; else if (valore[1] > 1) stringa1 = "anni"; if (valore[2] == 1) stringa2 = "mese"; else if (valore[2] > 1) stringa2 = "mesi"; }
            else if (valore[0] == 1) { if (valore[1] == 1) stringa1 = "mese"; else if (valore[1] > 1) stringa1 = "mesi"; if (valore[2] == 1) stringa2 = "giorno"; else if (valore[2] > 1) stringa2 = "giorni"; }
            else if (valore[0] == 2) { if (valore[1] == 1) stringa1 = "giorno"; else if (valore[1] > 1) stringa1 = "giorni"; if (valore[2] == 1) stringa2 = "ora"; else if (valore[2] > 1) stringa2 = "ore"; }
            else if (valore[0] == 3) { if (valore[1] == 1) stringa1 = "ora"; else if (valore[1] > 1) stringa1 = "ore"; if (valore[2] == 1) stringa2 = "minuto"; else if (valore[2] > 1) stringa2 = "minuti"; }
            else if (valore[0] == 4) { if (valore[1] == 1) stringa1 = "minuto"; else if (valore[1] > 1) stringa1 = "minuti"; if (valore[2] == 1) stringa2 = "secondo"; else if (valore[2] > 1) stringa2 = "secondi"; }

            if (stringa1 != "") stringa1 = valore[1] + " " + stringa1;
            if (stringa2 != "") stringa2 = valore[2] + " " + stringa2;
            if (stringa1 != "" && stringa2 != "" && stringa2.Substring(stringa2.Length - 7, 7) == "secondi" && stringa2.Substring(stringa2.Length - 7, 7) == "secondo") stringa1 += " e ";

            if (stringa1 == "") return "< 1 minuto";
            if(stringa2.Length>7) if (stringa2.Substring(stringa2.Length - 7, 7) == "secondi" || stringa2.Substring(stringa2.Length - 7, 7) == "secondo") { return stringa1; }
                else return stringa1 +" " + stringa2;
            else return stringa1 + " " + stringa2;
        }
    }
}
