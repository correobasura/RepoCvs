using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    class Program
    {
        private static SisResultEntities contexto;
        static DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
        static string rutaBase = @"D:\OneDrive\Estimaciones\FS\";

        static void Main(string[] args)
        {
            contexto = new SisResultEntities();
            //IngresarDatos(rutaBase);

            //for (int i = -5; i < 0; i++)
            //{
            //for (var i = DateTime.Today.AddDays(-16); i < DateTime.Today;)
            //{
            //    AnalizarDiaAnterior(i.AddDays(-1));
            //    i = i.AddDays(1);
            //}
            //var laFecha = DateTime.ParseExact("20170322", "yyyyMMdd", CultureInfo.InvariantCulture);
            //for (var i = DateTime.Today.AddDays(-13); i < DateTime.Today;)
            //{
            //    //AnDataUnGanador.AnalizarDatosDia(i, contexto,80);
            //    AnDataUnGanador.AnalizarDatosPorDiaSeleccionLvl2(i, contexto, 80);
            //    i = i.AddDays(1);
            //}
            //AnDataUnGanador.AnalizarDatosDia(DateTime.Today.AddDays(-1), contexto);
            //AnDataUnGanador.AnalizarDatosDiaLvl2(DateTime.Today.AddDays(-1), contexto);
            //AnDataUnGanador.AnalizarDiaAnteriorUnGanador(DateTime.Today.AddDays(-1), contexto);
            //AnDataMayorUno.AnalizarDiaAnteriorMayorUno(DateTime.Today.AddDays(-1), contexto);
            //for (var i = DateTime.Today.AddDays(-8); i <= DateTime.Today;)
            //{
            //    SeleccionarValoresUnGanador(rutaBase, i.AddDays(-1));
            //    i = i.AddDays(1);
            //}
            //AnalizarDatos(rutaBase, DateTime.Today, 202);

            AnalizarDatosListaDias(rutaBase);

            //SeleccionarValoresAleatorios(rutaBase);
            //AnalizarDatosListaDiasBetween(rutaBase);
            //AnalizarUnGanadorLvl1(rutaBase);
            //AnalizarUnGanadorLvl3(rutaBase);
            //RevisarTimeSpanDatos();
        }

        private static void IngresarDatos(string rutaBase)
        {
            string line;
            List<string> filenames = new List<string>();
            //DateTime fechaMinima = DateTime.ParseExact("20170502", "yyyyMMdd", CultureInfo.InvariantCulture);
            //for (var i = fechaMinima; i < DateTime.Today;)
            //{
            //    filenames.Add(i.ToString("yyyyMMdd"));
            //    i = i.AddDays(1);
            //}
            filenames.Add("20170509");
            StreamReader fileReader;

            for (int i = 0; i < filenames.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                string rutaTemp = rutaBase+ dt.ToString("yyyyMM") + "\\"+ filenames[i] + ".csv";
                fileReader = new StreamReader(rutaTemp);
                while ((line = fileReader.ReadLine()) != null)
                {
                    string[] arreglo = line.Split(';');
                    USERRESULTTABLESFS u = new USERRESULTTABLESFS();
                    u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
                    u.FECHA = dt;
                    u.TABINDEX = Convert.ToInt32(arreglo[0]);
                    u.HORA = arreglo[1];
                    u.MARCADOR = arreglo[4];
                    u.GLOCAL = Convert.ToInt32(arreglo[7]);
                    u.GVISITANTE = Convert.ToInt32(arreglo[8]);
                    u.DIFERENCIAG = Convert.ToInt32(arreglo[9]);
                    u.TOTALG = Convert.ToInt32(arreglo[10]);
                    u.FECHANUM = Convert.ToInt32(dt.ToString("yyyyMMdd"));
                    u.MESNUM = dt.Month;
                    u.DIAMESNUM = dt.Day;
                    u.DIASEMNUM = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;

                    contexto.USERRESULTTABLESFS.Add(u);
                }
                contexto.SaveChanges();
            }
            //AnalizarUnGanador(filenames);
            //AnalizarUnGanadorLvl2(filenames);
        }

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
                sw.WriteLine(item.Key+";"+item.Value.ToString());
            }
            sw.Close();
        }

        public static void AnalizarDatosListaDias(string rutaBase)
        {
            List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
            //Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            //listaAnalizada = new List<AnalisisDatosDTO>();
            for (int k = 4; k < 25; k++)
            {
                //for (int j = 80; j <=92; j++)
                //{
                for (var i = DateTime.Today.AddDays(-15); i < DateTime.Today;)
                {
                    TimeSpan ts = i - minFecha;
                    //int percent = ts.Days * j / 100;
                    //int percent = (ts.Days * 80) / 100;
                    listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccion(i, contexto, 80,k));
                    i = i.AddDays(1);
                }
                //EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizadaConDictU2"+k, rutaBase);
                AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
                a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
                a.Porcentaje = k;
                a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
                listaAnalizada.Clear();
                listaConsolidada.Add(a);
            }
            EscribirDatosArchivo(listaConsolidada, "AnalisisConsolidadoU1", rutaBase);
            //}

        }

        public static void AnalizarDatosListaDiasBetween(string rutaBase)
        {
            List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();

            listaAnalizada = new List<AnalisisDatosDTO>();
                for (var i = DateTime.Today.AddDays(-15); i < DateTime.Today;)
                {
                    TimeSpan ts = i - minFecha;
                    int percentMenor = ts.Days * 60 / 100;
                    int percentMayor = ts.Days * 79 / 100;
                    //int percent = 80;
                    listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionBetween(i, contexto, 5, percentMenor, percentMayor));
                    i = i.AddDays(1);
            }
            EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada", rutaBase);
            AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
                a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
                //a.Porcentaje = j;
                a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
                listaAnalizada.Clear();
                listaConsolidada.Add(a);
            EscribirDatosArchivo(listaConsolidada, "AnalisisDiaBetween" + 5, rutaBase);
        }

        public static void AnalizarUnGanador(List<string> filenames)
        {
            for (int i = 0; i < filenames.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                TimeSpan ts = dt - minFecha;
                int percent = ts.Days * 80 / 100;
                AnDataUnGanador.AnalizarDatosDia(dt, contexto, percent);
            }
        }

        public static void AnalizarUnGanadorLvl2(List<string> filenames)
        {
            for (int i = 0; i < filenames.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                TimeSpan ts = dt - minFecha;
                int percent = ts.Days * 80 / 100;
                AnDataUnGanador.AnalizarDatosPorDiaSeleccionLvl2(dt, contexto, percent);
            }
        }

        public static void AnalizarUnGanadorLvl3(string rutaBase)
        {
            List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            DateTime fechaMinLvl1 = DateTime.ParseExact("20170408", "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (; fechaMinLvl1 < DateTime.Today;)
            {
                TimeSpan ts = fechaMinLvl1 - minFecha;
                int percent = ts.Days * 80 / 100;
                listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionLvl3(fechaMinLvl1, contexto, percent, 5));
                fechaMinLvl1 = fechaMinLvl1.AddDays(1);
            }
            EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada2", rutaBase);
        }

        public static void RevisarTimeSpanDatos()
        {
            Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            var laMin = minFecha;
            for (; laMin < DateTime.Today;)
            {
                RevisionDatos.RevisarTimeSpanDatos(laMin, contexto, dict, 58);
                laMin = laMin.AddDays(1);
            }

            foreach (var item in dict)
            {
                item.Value.ValoresAparicionAcumulada = new List<int>();
                var counter = 0;
                foreach (var itemList in item.Value.ValoresAparicion)
                {
                    if(itemList == 1)
                    {
                        if (counter < 0)
                        {
                            item.Value.ValoresAparicionAcumulada.Add(counter);
                            counter = 0;
                        }
                        counter++;
                    }
                    else
                    {
                        if (counter > 0)
                        {
                            item.Value.ValoresAparicionAcumulada.Add(counter);
                            counter = 0;
                        }
                        counter--;

                    }
                }
                item.Value.ValoresAparicionAcumulada.Add(counter);
                item.Value.UltimoEnRachas = counter;
                item.Value.MaxValue = (from x in item.Value.ValoresAparicionAcumulada select x).Max();
                item.Value.MinValue = (from x in item.Value.ValoresAparicionAcumulada select x).Min();

                item.Value.DictRachasAcumuladas = (from elemento in item.Value.ValoresAparicionAcumulada
                                                 group elemento by elemento into g
                                                 select new
                                                 {
                                                     Valor = g.Key,
                                                     Cantidad = g.Count()
                                                 }).ToList().ToDictionary(x => x.Valor, x => x.Cantidad);
                item.Value.DictRachasAcumuladas = (from entry in item.Value.DictRachasAcumuladas orderby entry.Value ascending select entry).ToDictionary(x => x.Key, x => x.Value);
            }
            EscribirDatosArchivo(dict, "AnalisisDict", rutaBase);
        }
    }
}
