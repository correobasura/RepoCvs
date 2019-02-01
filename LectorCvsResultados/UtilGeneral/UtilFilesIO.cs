using LectorCvsResultados.FlashOrdered;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LectorCvsResultados.UtilGeneral
{
    public class UtilFilesIO
    {
        public static void EscribirDatosArchivo(List<AgrupadorConsolidadoDTO> listaSeleccionados, string cad, string rutabase)
        {
            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            var i = 0;
            foreach (var item in listaSeleccionados)
            {
                sw.WriteLine(item.ToString());
                i++;
            }
            sw.Close();
        }

        public static void EscribirDatosArchivo(Dictionary<int, AnalizedTabIndexDTO> dict, string cad, string rutabase)
        {
            //var data = CalcularNumeros();
            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Value.ToString());
            }
            sw.WriteLine("\n");
            //foreach (var item in data)
            //{
            //    sw.WriteLine(item);
            //}
            sw.Close();
        }

        public static void EscribirDatosArchivo(List<AnalisisDatosDTO> listaSeleccionados, string cad, string rutabase)
        {
            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in listaSeleccionados)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        public static void EscribirDatosArchivo(Dictionary<int, AgrupadorTimeSpanDTO> dict, string cad, string rutabase)
        {
            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + ";" + item.Value.ToString());
            }
            sw.Close();
        }

        public static List<InfoAnalisisDTO3> ReadLinesFiles(string path, string rutaWriteReview)
        {
            List<InfoAnalisisDTO3> lista = new List<InfoAnalisisDTO3>();
            var lines = File.ReadAllLines(rutaWriteReview + path + ".txt");
            //StreamWriter sw = new StreamWriter(rutaWriteReview + path + ".txt");
            foreach (var line in lines)
            {
                //string strLine = line.Replace("Id:", "Id\":");
                InfoAnalisisDTO3 obj = JsonConvert.DeserializeObject<InfoAnalisisDTO3>(line);
                lista.Add(obj);
                //string item = line.Replace("\"[", "[");
                //sw.WriteLine(item.ToString());
            }
            //sw.Close();
            return lista;
        }

        public static List<AgrupadorInfoGeneralDTO> ReadLinesFilesQueried(string path, string rutaWriteReview)
        {
            List<AgrupadorInfoGeneralDTO> lista = new List<AgrupadorInfoGeneralDTO>();
            var lines = File.ReadAllLines(rutaWriteReview + path);
            foreach (var line in lines)
            {
                AgrupadorInfoGeneralDTO obj = JsonConvert.DeserializeObject<AgrupadorInfoGeneralDTO>(line);
                lista.Add(obj);
            }
            return lista;
        }


        public static void EscribirArchivoCsv(List<AgrupadorInfoGeneralDTO> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite);
            foreach (var item in lista)
            {
                sw.WriteLine(JsonConvert.SerializeObject(item));
            }
            sw.Close();
        }

        public static void EscribirArchivoCsv(Dictionary<int, Dictionary<string, InfoAnalisisDTO>> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite);
            foreach (var item in lista)
            {
                sw.WriteLine(JsonConvert.SerializeObject(item));
            }
            sw.Close();
        }

        public static void EscribirArchivoCsv(Dictionary<int, Dictionary<int, InfoAnalisisDTO>> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite);
            foreach (var item in lista)
            {
                sw.WriteLine(JsonConvert.SerializeObject(item));
            }
            sw.Close();
        }

        public static void EscribirArchivoCsv(Dictionary<string, InfoAnalisisDTO> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite);
            foreach (var item in lista)
            {
                sw.WriteLine(JsonConvert.SerializeObject(item));
            }
            sw.Close();
        }

        public static void EscribirArchivoCsv(Dictionary<int, InfoAnalisisDTO> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite);
            foreach (var item in lista)
            {
                sw.WriteLine(JsonConvert.SerializeObject(item));
            }
            sw.Close();
        }

        //public static Task ProcessWrite(List<InfoAnalisisDTO3> lista, string pathWrite)
        //{
        //    return WriteTextToFile(lista, pathWrite);
        //}

        //public static async Task WriteTextToFile()
        //{
        //    using (var streamWriter = new StreamWriter(rutaWriteReview + key + ".txt"))
        //    {
        //        foreach (var line in lista)
        //            await streamWriter.WriteLineAsync(line.ToString());
        //    }
        //}


        public static void EscribirArchivoCsv(List<InfoAnalisisDTO3> lista, string pathWrite, string rutaWriteReview)
        {
            if (!Directory.Exists(rutaWriteReview))
            {
                Directory.CreateDirectory(rutaWriteReview);
            }
            StreamWriter sw = new StreamWriter(rutaWriteReview + pathWrite + ".txt");
            foreach (var item in lista)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }


        public static void EscribirArchivoHtml(string data, DateTime fecha, string pathConcat, string rutaWriteTemp)
        {
            string path = rutaWriteTemp + fecha.ToString("yyyy") + "\\" + fecha.ToString("MM") + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += fecha.ToString("dd") + pathConcat + ".html";
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(data);
            sw.Close();
        }



        public static void EscribirArchivoCsv(List<FLASHORDERED> lista, string pathWriteFile, string pathWrite)
        {
            if (!Directory.Exists(pathWrite))
            {
                Directory.CreateDirectory(pathWrite);
            }
            StreamWriter sw = new StreamWriter(pathWriteFile);
            foreach (var item in lista)
            {
                var json = JsonConvert.SerializeObject(item);
                sw.WriteLine(json);
            }
            sw.Close();
        }
    }
}