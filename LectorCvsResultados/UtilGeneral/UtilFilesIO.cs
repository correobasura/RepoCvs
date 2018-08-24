using System.Collections.Generic;
using System.IO;

namespace LectorCvsResultados.UtilGeneral
{
    public class UtilFilesIO
    {
        private static string rutaBase = @"D:\OneDrive\Estimaciones\FS\";
        private static string rutaBaseAn = @"D:\temp2\";

        public static void EscribirArchivoCsv(List<FLASHORDERED> lista)
        {
            string fic = rutaBase + "\\Analisis\\DiaTemp" + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in lista)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        public static void EscribirArchivoCsv(Dictionary<int, InfoAnalisisDTO> dict)
        {
            string fic = rutaBaseAn + "AnDataMinReg" + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + ";" + item.Value.ToString());
            }
            sw.Close();
        }
    }
}