using LectorCvsResultados.FlashOrdered;
using LectorCvsResultados.UtilGeneral;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    internal class Program
    {
        private static SisResultEntities contexto;
        private static DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private static string key = "";
        //private static List<InfoAnalisisDTO3> lista = new List<InfoAnalisisDTO3>();

        public static void AnalizarDatosListaDiaActual(string rutaBase, DateTime fecha, int maxTabindex = 0)
        {
            //bool historial = false;
            //if (maxTabindex == 0)
            //{
            //    maxTabindex = ConsultasClass.ConsultarMaxIndexFecha(fecha.ToString("yyyyMMdd"), contexto);
            //    historial = true;
            //}
            //TimeSpan ts = fecha.AddDays(-1) - minFecha;
            ////53 % encontrado en consolidado
            //int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            //int percent = ts.Days * AnDataUnGanador.RetornarPercent(dayofweek) / 100;
            //List<int> lista = AnDataUnGanador.AnalizarDatosDiaActual(fecha, contexto, maxTabindex, percent);
            //string rutaFileTemp = rutaBase + fecha.ToString("yyyyMM") + "\\" + fecha.ToString("yyyyMMdd") + "T.csv";
            //IEnumerable<string> lines = File.ReadAllLines(rutaFileTemp);
            //Dictionary<int, AnalizedTabIndexDTO> dict = new Dictionary<int, AnalizedTabIndexDTO>();
            //List<AgrupadorTotalPercentSpanDTO> listaSpanGral = ConsultasClass.ConsultarPercentTimeSpan(contexto, fecha.ToString("yyyyMMdd"));
            //List<AgrupadorTotalPercentSpanDTO> listaSpanDia = ConsultasClass.ConsultarPercentTimeSpan(contexto, fecha.ToString("yyyyMMdd"), 1);
            //List<ANALISTINDEXUNG> listaAnalisis = new List<ANALISTINDEXUNG>();
            //if (historial)
            //{
            //    int fechanum = Convert.ToInt32(fecha.ToString("yyyyMMdd"));
            //    listaAnalisis = (from x in contexto.ANALISTINDEXUNG
            //                     where x.FECHANUM == fechanum
            //                     select x).OrderBy(x => x.LINEINDEX).ToList();
            //}
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    AnalizedTabIndexDTO a = new AnalizedTabIndexDTO();
            //    a.Lineindex = i + 1;
            //    if (historial)
            //    {
            //        a.Result = (int)(from x in listaAnalisis where x.LINEINDEX == a.Lineindex select x.RESULT).FirstOrDefault();
            //    }
            //    a.Tabindex = lista.ElementAt(i);
            //    a.UltimoSpan = ConsultasClass.ConsultarUltimoTimeSpan(contexto, a.Tabindex, fecha.ToString("yyyyMMdd")).Spantiempo;
            //    var allLines = lines.Where(x => x.IndexOf(a.Tabindex.ToString(), StringComparison.OrdinalIgnoreCase) >= 0);
            //    for (int j = 0; j < allLines.Count(); j++)
            //    {
            //        string lineTemp = allLines.ElementAt(j);
            //        if (Convert.ToInt32(lineTemp.Split(';')[0]).CompareTo(a.Tabindex) == 0)
            //        {
            //            a.TMatch = lineTemp;
            //            break;
            //        }
            //    }
            //    a.RankUltimoSpanGral = (from x in listaSpanGral where x.Span == a.UltimoSpan select x.Rank).FirstOrDefault();
            //    a.RankUltimoSpanDia = (from x in listaSpanDia where x.Span == a.UltimoSpan select x.Rank).FirstOrDefault();
            //    dict.Add(i + 1, a);
            //}
            //var dictvalues = (from entry in dict
            //                  orderby entry.Value.RankUltimoSpanDia descending,
            //                  entry.Value.RankUltimoSpanGral descending
            //                  select entry).ToDictionary(x => x.Key, x => x.Value);
            ////if (historial)
            ////{
            ////    var lineIndex = 1;
            ////    foreach (var item in dictvalues)
            ////    {
            ////        var varData = dictionaryHist[lineIndex];
            ////        varData.Add(item.Value.Result);
            ////        dictionaryHist[lineIndex] = varData;
            ////        lineIndex++;
            ////    }
            ////}
            //EscribirDatosArchivo(dictvalues, "Actual\\Analisis" + fecha.ToString("yyyyMMdd"), rutaBase);
        }

        public static void AnalizarDatosListaDias(string rutaBase)
        {
            //Dictionary<string, string> dictValues = new Dictionary<string, string>();
            //dictValues.Add("AnConProm", ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM);
            //dictValues.Add("AnConDiaSem", ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_DIASEM);
            //dictValues.Add("AnConDiaSemDIAMES", ConstantesConsulta.QUERY_CONTEO_VALORES_DIA_SEMANA_DIAMES);
            //DateTime fechaMinima = DateTime.Today.AddDays(-7);
            ////DateTime fechaMinima = DateTime.ParseExact("20170727", "yyyyMMdd", CultureInfo.InvariantCulture);
            //List<int> indexDias = new List<int>();
            //indexDias.Add(1);
            //indexDias.Add(2);
            //indexDias.Add(3);
            //indexDias.Add(4);
            //indexDias.Add(5);
            //indexDias.Add(6);
            //indexDias.Add(7);
            //foreach (var itemDict in dictValues)
            //{
            //    List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            //    List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
            //    Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            //    for (int m = 0; m < indexDias.Count; m++)
            //    {
            //        var elementoIndex = indexDias.ElementAt(m);
            //        List<DateTime> listaFechas = new List<DateTime>();
            //        for (var i = fechaMinima; i < DateTime.Today;)
            //        {
            //            int dayOfWeek = (int)i.DayOfWeek == 0 ? 7 : (int)i.DayOfWeek;
            //            if (dayOfWeek == elementoIndex)
            //            {
            //                listaFechas.Add(i);
            //            }
            //            i = i.AddDays(1);
            //        }
            //        if (listaFechas.Count > 0)
            //        {
            //            for (int k = 5; k < 93; k++)
            //            {
            //                for (var i = 0; i < listaFechas.Count(); i++)
            //                {
            //                    var fecha = listaFechas.ElementAt(i);
            //                    TimeSpan ts = fecha.AddDays(-1) - minFecha;
            //                    int percent = ts.Days * k / 100;
            //                    listaAnalizada.Add(AnDataUnGanador.AnalizarDatosDiaTemp(fecha, contexto, itemDict.Value, percent));
            //                }
            //                //EscribirDatosArchivo(listaAnalizada, "AnalisisDatosDepuradosD1", rutaBase);
            //                AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
            //                a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
            //                a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
            //                a.Porcentaje = k;
            //                a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
            //                listaAnalizada.Clear();
            //                listaConsolidada.Add(a);
            //            }
            //            listaConsolidada = (from x in listaConsolidada
            //                                orderby x.TotalPositivosMuestras descending, x.MaxValue descending,
            //                                x.MinValue descending
            //                                select x).ToList();
            //            EscribirDatosArchivo(listaConsolidada, itemDict.Key + elementoIndex, rutaBase);
            //            listaConsolidada.Clear();
            //        }
            //    }
            //}

            //ConsolidarResultados();
        }

        private static void ConsolidarResultados()
        {
            List<string> stringBase = new List<string>();
            stringBase.Add("AnConProm");
            stringBase.Add("AnConDiaSem");
            stringBase.Add("AnConDiaSemDIAMES");
            foreach (var item in stringBase)
            {
                for (int i = 1; i <= 7; i++)
                {
                    string rutaActual = ConstantesGenerales.rutaBase + "\\" + item + i + ".csv";
                    string rutaConsolidado = ConstantesGenerales.rutaBase + "\\Consolidados\\" + item + i + ".csv";
                    IEnumerable<string> linesActual = File.ReadAllLines(rutaActual);
                    IEnumerable<string> linesConsolidado = File.ReadAllLines(rutaConsolidado);
                    List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
                    List<AgrupadorConsolidadoDTO> listaActual = new List<AgrupadorConsolidadoDTO>();
                    List<AgrupadorConsolidadoDTO> listaFinal = new List<AgrupadorConsolidadoDTO>();
                    foreach (var itemConsolidado in linesConsolidado)
                    {
                        AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                        string[] arreglo = itemConsolidado.Split(';');
                        a.TotalPositivosMuestras = Convert.ToInt32(arreglo[0]);
                        a.MaxValue = Convert.ToInt32(arreglo[1]);
                        a.MinValue = Convert.ToInt32(arreglo[2]);
                        a.Porcentaje = Convert.ToInt32(arreglo[3]);
                        listaConsolidada.Add(a);
                    }
                    foreach (var itemActual in linesActual)
                    {
                        AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                        string[] arreglo = itemActual.Split(';');
                        a.TotalPositivosMuestras = Convert.ToInt32(arreglo[0]);
                        a.MaxValue = Convert.ToInt32(arreglo[1]);
                        a.MinValue = Convert.ToInt32(arreglo[2]);
                        a.Porcentaje = Convert.ToInt32(arreglo[3]);
                        listaActual.Add(a);
                    }
                    foreach (var itemConsolidado in listaConsolidada)
                    {
                        var itemActual = (from x in listaActual where x.Porcentaje == itemConsolidado.Porcentaje select x).FirstOrDefault();
                        itemConsolidado.TotalPositivosMuestras += itemActual.TotalPositivosMuestras;
                        itemConsolidado.MaxValue = itemConsolidado.MaxValue >= itemActual.MaxValue ? itemConsolidado.MaxValue : itemActual.MaxValue;
                        itemConsolidado.MinValue = itemConsolidado.MinValue <= itemActual.MinValue ? itemConsolidado.MinValue : itemActual.MinValue;
                    }
                    listaConsolidada = listaConsolidada
                        .OrderByDescending(b => b.TotalPositivosMuestras)
                        .ThenByDescending(x => x.MinValue)
                        .ThenBy(b => b.Porcentaje)
                        .ThenByDescending(x => x.MaxValue)
                        .AsEnumerable().ToList();
                    string rutaFinal = "\\Consolidados\\" + item + i;
                    UtilFilesIO.EscribirDatosArchivo(listaConsolidada, rutaFinal, ConstantesGenerales.rutaBase);
                }
            }
        }

        public static void AnalizarUnGanador(string fechaFormat)
        {
            //DateTime dt = DateTime.ParseExact(fechaFormat, "yyyyMMdd", CultureInfo.InvariantCulture);
            //TimeSpan ts = dt.AddDays(-1) - minFecha;
            //int dayofweek = (int)dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            ////53 % encontrado en consolidado
            //int percent = ts.Days * AnDataUnGanador.RetornarPercent(dayofweek) / 100;
            //AnDataUnGanador.AnalizarDatosDia(dt, contexto, percent);
        }

        private static void IngresarDatosAllReload()
        {
            //string line;
            //List<string> filenames = new List<string>();
            ////DateTime fechaMinima = DateTime.Today.AddDays(-36);
            //DateTime fechaMax = DateTime.ParseExact("20170727", "yyyyMMdd", CultureInfo.InvariantCulture);
            //for (var i = minFecha; i < fechaMax;)
            //{
            //    filenames.Add(i.ToString("yyyyMMdd"));
            //    i = i.AddDays(1);
            //}
            //Dictionary<int, AgrupadorTimeSpanDTO> dict = AnDataUnGanador.ObtenerDiccionarioInicial();
            ////filenames.Add("20170621");
            //StreamReader fileReader;
            //List<decimal?> listTabindex = new List<decimal?>();
            //List<USERRESULTTABLESFS> listElementosAgregados = new List<USERRESULTTABLESFS>();

            //for (int i = 0; i < filenames.Count; i++)
            //{
            //    DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
            //    string rutaTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + filenames[i] + ".csv";
            //    fileReader = new StreamReader(rutaTemp);
            //    while ((line = fileReader.ReadLine()) != null)
            //    {
            //        string[] arreglo = line.Split(';');
            //        USERRESULTTABLESFS u = new USERRESULTTABLESFS();
            //        u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
            //        u.FECHA = dt;
            //        int tabindex = Convert.ToInt32(arreglo[0]);
            //        int DIFERENCIAG = Convert.ToInt32(arreglo[9]);
            //        int laFechaNum = Convert.ToInt32(dt.ToString("yyyyMMdd"));
            //        u.TABINDEX = tabindex;
            //        u.HORA = arreglo[1];
            //        u.MARCADOR = arreglo[4];
            //        u.GLOCAL = Convert.ToInt32(arreglo[7]);
            //        u.GVISITANTE = Convert.ToInt32(arreglo[8]);
            //        u.DIFERENCIAG = DIFERENCIAG;
            //        u.TOTALG = Convert.ToInt32(arreglo[10]);
            //        u.FECHANUM = laFechaNum;
            //        u.MESNUM = dt.Month;
            //        u.DIAMESNUM = dt.Day;
            //        u.DIASEMNUM = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            //        //Cuando se reinician todos los datos es necesario ejecutar la actualización de los tabindexseq y spantiempo hist
            //        //por que se hacen consultas a la base de datos y no se han confirmado los cambios, por lo que siempre va a retornar 1 0 -1
            //        //u.TABINDEXSEQ = ConsultasClass.ConsultarNextTabindexSeq(contexto, tabindex);
            //        //u.SPANTIEMPOHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, DIFERENCIAG, contexto, dt.ToString("yyyyMMdd"));
            //        AnDataUnGanador.ValidarSpanDatos(dict, u, tabindex, DIFERENCIAG, contexto);
            //        listTabindex.Add(tabindex);
            //        listElementosAgregados.Add(u);
            //    }
            //}
            ////AnDataUnGanador.ValidarSpanDatosDiaAnterior(contexto, listTabindex, listElementosAgregados);
            //contexto.SaveChanges();
            //AnalizarUnGanador(filenames);
        }

        private static void IngresarDatos(string dateFileName)
        {
            //string line;
            //StreamReader fileReader;
            //List<decimal?> listTabindex = new List<decimal?>();
            //List<USERRESULTTABLESFS> listElementosAgregados = new List<USERRESULTTABLESFS>();
            //DateTime dt = DateTime.ParseExact(dateFileName, "yyyyMMdd", CultureInfo.InvariantCulture);
            //string rutaTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + dateFileName + ".csv";
            //fileReader = new StreamReader(rutaTemp);
            //int DIAMESnum = dt.Day;
            //int diasemnum = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            //while ((line = fileReader.ReadLine()) != null)
            //{
            //    string[] arreglo = line.Split(';');
            //    USERRESULTTABLESFS u = new USERRESULTTABLESFS();
            //    u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
            //    u.FECHA = dt;
            //    int tabindex = Convert.ToInt32(arreglo[0]);
            //    int DIFERENCIAG = Convert.ToInt32(arreglo[9]);
            //    int laFechaNum = Convert.ToInt32(dt.ToString("yyyyMMdd"));
            //    u.TABINDEX = tabindex;
            //    u.HORA = arreglo[1];
            //    u.MARCADOR = arreglo[4];
            //    u.GLOCAL = Convert.ToInt32(arreglo[7]);
            //    u.GVISITANTE = Convert.ToInt32(arreglo[8]);
            //    u.DIFERENCIAG = DIFERENCIAG;
            //    u.TOTALG = Convert.ToInt32(arreglo[10]);
            //    u.FECHANUM = laFechaNum;
            //    u.MESNUM = dt.Month;
            //    u.DIAMESNUM = DIAMESnum;
            //    u.DIASEMNUM = diasemnum;
            //    u.TABINDEXSEQ = ConsultasClass.ConsultarNextTabindexSeq(contexto, tabindex);
            //    u.SPANTIEMPOHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, DIFERENCIAG, contexto, dt.ToString("yyyyMMdd"), 0);
            //    u.SPANTIEMPOSEMHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, DIFERENCIAG, contexto, dt.ToString("yyyyMMdd"), 1);
            //    u.SPANTIEMPOMESHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, DIFERENCIAG, contexto, dt.ToString("yyyyMMdd"), 2);
            //    listTabindex.Add(tabindex);
            //    listElementosAgregados.Add(u);|
            //}
            //AnDataUnGanador.ValidarSpanDatosDiaAnterior(contexto, listTabindex, listElementosAgregados, diasemnum, DIAMESnum);
            //contexto.SaveChanges();
        }

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            contexto = new SisResultEntities();
            var logger = new UtilGeneral.CustomLogger();
            contexto.Database.Log = s => logger.Log("EFApp", s);
            var laFechaNum = (from b in contexto.TOTALESDIA
                              select b.ID).Max();

            var laFecha = DateTime.ParseExact(laFechaNum.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            var laFechaMax = DateTime.ParseExact(DateTime.Now.AddHours(3).AddMinutes(30).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            Dictionary<DateTime, string> lstFechas = new Dictionary<DateTime, string>();
            string infoHtml;
            for (var i = laFecha; i <= laFechaMax; i = i.AddDays(1))
            {
                string path = ConstantesGenerales.rutaWriteTemp + i.ToString("yyyy") + "\\" + i.ToString("MM") + "\\" + i.ToString("dd") + ".html";
                string pathT = ConstantesGenerales.rutaWriteTemp + i.ToString("yyyy") + "\\" + i.ToString("MM") + "\\" + i.ToString("dd") + "T.html";
                string pathF = ConstantesGenerales.rutaWriteTemp + i.ToString("yyyy") + "\\" + i.ToString("MM") + "\\" + i.ToString("dd") + "F.html";
                if (i.Equals(laFechaMax))
                {
                    if (!File.Exists(pathT))
                    {
                        lstFechas.Add(i, "T");
                    }
                }
                else
                {
                    if (File.Exists(pathT) && File.Exists(pathF))
                    {
                        continue;
                    }
                    else if (File.Exists(pathT) && !File.Exists(pathF))
                    {
                        lstFechas.Add(i, "F");

                    }
                    else
                    {
                        if (!File.Exists(path))
                            lstFechas.Add(i, "");
                    }
                }
            }
            //foreach (var i in lstFechas)
            //{
            //    infoHtml = UtilHtml.GetAsync(i.Key, rutaBaseSw);
            //    EscribirArchivoHtml(infoHtml, i.Key, i.Value);
            //}
            bool esActual = false;
            foreach (var i in lstFechas)
            {
                esActual = "T".Equals(i.Value);
                infoHtml = UtilHtml.GetAsyncM(i.Key, ConstantesGenerales.rutaBaseSw, esActual);
                UtilFilesIO.EscribirArchivoHtml(infoHtml, i.Key, i.Value, ConstantesGenerales.rutaWriteTemp);
            }
            InsertarFORecientes();

            List<AgrupadorInfoGeneralDTO> listaTemp;
            List<FLASHORDERED> listaDia;
            List<FLASHORDERED> listaHtmlTemp;
            string fecha;
            int dayofweek;
            string keyFile;
            string keyInfo;
            List<AgrupadorInfoQuery> listProbRanks;
            Dictionary<int, InfoAnalisisDTO> dictTotales = new Dictionary<int, InfoAnalisisDTO>();
            Dictionary<int, InfoAnalisisDTO> dictTotalesFin;
            Dictionary<int, Dictionary<int, InfoAnalisisDTO>> dictGen = new Dictionary<int, Dictionary<int, InfoAnalisisDTO>>();


            Dictionary<int, InfoAnalisisDTO> dicTAnalisis;
            Dictionary<int, int> dicTSelected = new Dictionary<int, int>();
            Dictionary<int, int> dictTotalesInfo = new Dictionary<int, int>();
            List<int> listaDias;
            List<FLASHORDERED> listaDatos;
            List<FLASHORDERED> dataLstDia;
            List<FLASHORDERED> dataActual;
            List<int> listaTempSelected;
            int maxTabIndexDia;
            int fechaNum;
            //for (var i = laFechaMax.AddDays(-45); i < laFechaMax.AddDays(-15); i = i.AddDays(1))
            //{
            //    fechaNum = Convert.ToInt32(i.ToString("yyyyMMdd"));
            //    dictTotales.Add(fechaNum, new InfoAnalisisDTO());
            //    listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(i, 1);
            //    maxTabIndexDia = (from x in listaHtmlTemp select x.TABINDEX).Max();
            //    listaDias = contexto.TOTALESDIA.Where(x => x.TOTAL == maxTabIndexDia && x.ID < fechaNum).Select(x => (int)x.ID).ToList();
            //    dictTotalesInfo.Add(fechaNum, listaDias.Count);
            //    if (listaDias.Count <= 2) continue;
            //    dicTSelected.Clear();
            //    listaDatos = contexto.FLASHORDERED.Where(x => listaDias.Contains(x.FECHANUM)).ToList();
            //    dictGen.Clear();
            //    for (int j = 1; j < listaDias.Count - 1; j++)
            //    {
            //        dataLstDia = (from x in listaDatos where x.FECHANUM == listaDias.ElementAt(j) select x).ToList();
            //        dataActual = (from x in listaDatos where x.FECHANUM == listaDias.ElementAt(j - 1) select x).ToList();
            //        foreach (var itemHist in dataActual)
            //        {
            //            if (!dictGen.ContainsKey(itemHist.TABINDEX))
            //            {
            //                dicTAnalisis = new Dictionary<int, InfoAnalisisDTO>();
            //                dictGen.Add(itemHist.TABINDEX, dicTAnalisis);
            //            }
            //            else
            //            {
            //                dicTAnalisis = dictGen[itemHist.TABINDEX];
            //            }
            //            foreach (var itemActual in dataLstDia)
            //            {
            //                if (!dicTAnalisis.ContainsKey(itemActual.TABINDEX))
            //                {
            //                    dicTAnalisis.Add(itemActual.TABINDEX, new InfoAnalisisDTO());
            //                }
            //                if (itemHist.DIFERENCIAG == 0)
            //                {
            //                    if (itemActual.DIFERENCIAG == 0)
            //                    {
            //                        dicTAnalisis[itemActual.TABINDEX].EqNegativos++;
            //                    }
            //                    else
            //                    {
            //                        dicTAnalisis[itemActual.TABINDEX].EqPositivos++;
            //                    }
            //                }
            //                else
            //                {
            //                    if (itemActual.DIFERENCIAG == 0)
            //                    {
            //                        dicTAnalisis[itemActual.TABINDEX].Negativos++;
            //                    }
            //                    else
            //                    {
            //                        dicTAnalisis[itemActual.TABINDEX].Positivos++;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    dataActual = (from x in listaDatos where x.FECHANUM == listaDias.ElementAt(listaDias.Count - 1) select x).ToList();
            //    foreach (var item in dataActual)
            //    {
            //        dicTAnalisis = dictGen[item.TABINDEX];
            //        if(item.DIFERENCIAG == 0)
            //        {
            //            listaTempSelected = (from x in dicTAnalisis where x.Value.EqAvgPos >= 90000 select x.Key).ToList();
            //        }
            //        else
            //        {
            //            listaTempSelected = (from x in dicTAnalisis where x.Value.AvgPos >= 90000 select x.Key).ToList();
            //        }
            //        foreach (var itemSelected in listaTempSelected)
            //        {
            //            if (!dicTSelected.ContainsKey(itemSelected))
            //            {
            //                dicTSelected.Add(itemSelected, 0);
            //            }
            //            dicTSelected[itemSelected]++;
            //        }
            //    }
            //    dicTSelected = (from x in dicTSelected orderby x.Value select x).ToDictionary(x => x.Key, x => x.Value);
            //    int counter = 0;
            //    listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
            //    foreach (var item in dicTSelected)
            //    {
            //        var data = (from x in listaDia where x.TABINDEX == item.Key select x).FirstOrDefault();
            //        if(data != null)
            //        {
            //            if(data.DIFERENCIAG == 0)
            //            {
            //                dictTotales[fechaNum].Negativos++;
            //            }
            //            else
            //            {
            //                dictTotales[fechaNum].Positivos++;
            //            }
            //            counter++;
            //            if (counter > 0) break;
            //        }
            //    }



            //    var stop = "";
            //}

            var final = "";
        }




        private static List<InfoAnalisisDTO3> ReviewInfo(DateTime laFechaMax, int fromDays,
            Dictionary<DateTime, InfoReviewDTO> dictInfo)
        {
            List<InfoAnalisisDTO3> infoList = new List<InfoAnalisisDTO3>();
            Dictionary<int, InfoAnalisisDTO> dictTotalesDias = new Dictionary<int, InfoAnalisisDTO>();
            Dictionary<int, Dictionary<int, InfoAnalisisDTO>> dictTotalesTabindex = new Dictionary<int, Dictionary<int, InfoAnalisisDTO>>();

            var laFecha = DateTime.ParseExact(laFechaMax.AddDays(-fromDays).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var j = laFecha; j < laFechaMax.AddDays(-2); j = j.AddDays(1))
            {
                var infoResultsSig = dictInfo[j.AddDays(1)].LstResults;
                var infoResultsHist = dictInfo[j].LstResults;
                foreach (var item in infoResultsHist)
                {
                    if (!dictTotalesTabindex.ContainsKey(item.TABINDEX))
                    {
                        dictTotalesDias = new Dictionary<int, InfoAnalisisDTO>();
                        dictTotalesTabindex.Add(item.TABINDEX, dictTotalesDias);
                    }
                    else
                    {
                        dictTotalesDias = dictTotalesTabindex[item.TABINDEX];
                    }
                    foreach (var itemSig in infoResultsSig)
                    {
                        if (!dictTotalesDias.ContainsKey(itemSig.TABINDEX))
                        {
                            dictTotalesDias.Add(itemSig.TABINDEX, new InfoAnalisisDTO());
                        }
                        if (item.DIFERENCIAG == 0)
                        {
                            if (itemSig.DIFERENCIAG == 0)
                            {
                                dictTotalesDias[itemSig.TABINDEX].EqNegativos++;
                            }
                            else
                            {
                                dictTotalesDias[itemSig.TABINDEX].EqPositivos++;
                            }
                        }
                        else
                        {
                            if (itemSig.DIFERENCIAG == 0)
                            {
                                dictTotalesDias[itemSig.TABINDEX].Negativos++;
                            }
                            else
                            {
                                dictTotalesDias[itemSig.TABINDEX].Positivos++;
                            }
                        }
                    }
                }
            }
            foreach (var item in dictTotalesTabindex)
            {
                var data = new InfoAnalisisDTO3();
                data.Id = item.Key;
                data.Lst = new List<InfoAnalisisDTO2>();
                foreach (var itemValue in item.Value)
                {
                    var dataItemList = new InfoAnalisisDTO2();
                    dataItemList.Id = itemValue.Key;
                    dataItemList.P = itemValue.Value.Positivos;
                    dataItemList.N = itemValue.Value.Negativos;
                    dataItemList.EqP = itemValue.Value.EqPositivos;
                    dataItemList.EqN = itemValue.Value.EqNegativos;
                    data.Lst.Add(dataItemList);
                }
                infoList.Add(data);
            }
            return infoList;
        }

        private static Dictionary<int, InfoAnalisisDTO2> GetInfoByProb(DateTime laFechaMax, int minProb, List<InfoAnalisisDTO3> dictTotalesTabindex)
        {
            Dictionary<int, InfoAnalisisDTO2> dictTotalesDias = new Dictionary<int, InfoAnalisisDTO2>();
            Dictionary<int, List<InfoAnalisisDTO2>> dictTotalesTabindexSel = new Dictionary<int, List<InfoAnalisisDTO2>>();
            List<FLASHORDERED> listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(laFechaMax, 1);
            List<FLASHORDERED> listaHist = UtilGeneral.UtilHtml.LeerInfoHtml(laFechaMax.AddDays(-1));
            int maxTabindexTemp = (from x in listaHtmlTemp select x.TABINDEX).Max();
            int maxTabindexHist = (from x in listaHist select x.TABINDEX).Max();
            listaHist = listaHist.Where(x => x.Estado.Equals("FP")).ToList();
            foreach (var itemHist in listaHist)
            {
                var data = (from x in dictTotalesTabindex where x.Id.Equals(itemHist.TABINDEX) select x.Lst).FirstOrDefault();
                if (itemHist.DIFERENCIAG == 0)
                {
                    var dataCollectionSelected = (from x in data
                                                  where x.Id <= maxTabindexTemp && x.EqAP >= minProb
                                                  orderby x.EqAP descending, x.EqP descending, x.EqN
                                                  select x).ToList();
                    dictTotalesTabindexSel.Add(itemHist.TABINDEX, dataCollectionSelected);
                }
                else
                {
                    var dataCollectionSelected = (from x in data
                                                  where x.Id <= maxTabindexTemp && x.P >= minProb
                                                  orderby x.P descending, x.EqP descending, x.N
                                                  select x).ToList();
                    dictTotalesTabindexSel.Add(itemHist.TABINDEX, dataCollectionSelected);
                }

            }
            dictTotalesDias.Clear();
            foreach (var item in dictTotalesTabindexSel)
            {
                foreach (var itemValue in item.Value)
                {
                    if (!dictTotalesDias.ContainsKey(itemValue.Id))
                    {
                        dictTotalesDias.Add(itemValue.Id, itemValue);
                    }
                }
            }
            return dictTotalesDias;
        }


        private static void InsertarFORecientes()
        {
            List<FLASHORDERED> lista = new List<FLASHORDERED>();
            List<FLASHORDERED> listaSelected = new List<FLASHORDERED>();
            List<FLASHORDERED> listaInfoPercent = new List<FLASHORDERED>();
            //var laFecha = DateTime.ParseExact("20181017", "yyyyMMdd", CultureInfo.InvariantCulture);
            //var laFechaMax = DateTime.ParseExact("20180426", "yyyyMMdd", CultureInfo.InvariantCulture);
            var laFechaNum = (from b in contexto.TOTALESDIA
                              select b.ID).Max();
            var laFecha = DateTime.ParseExact(laFechaNum.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).AddDays(1);
            var laFechaMax = DateTime.ParseExact(DateTime.Now.AddHours(3).AddMinutes(30).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            //int idPrueba = ConsultasClassFO.ConsultarMaxIdActual(contexto);
            //int idInicio = 1;
            for (var i = laFecha; i < laFechaMax;)
            {
                //lista.AddRange(UtilGeneral.UtilHtml.LeerInfoHtml(i, idInicio, 0));
                listaSelected = UtilGeneral.UtilHtml.LeerInfoHtml(i);
                int value = (from x in listaSelected select x.TABINDEX).Max();

                lista.AddRange(listaSelected);
                //idPrueba = idPrueba + lista.Count + 1;
                //AnDataSelectedInfo.GuardarElementosSelectedInfo(contexto, i, VAL_TOTAL, listaSelected);
                i = i.AddDays(1);
            }
            listaInfoPercent.AddRange(lista);

            //TimeSpan start = TimeSpan.Parse("23:30"); // 10 PM
            //TimeSpan end = TimeSpan.Parse("23:59");
            //Dictionary<TimeSpan, int> dictTotales = new Dictionary<TimeSpan, int>();
            //foreach (var item in lista)
            //{
            //    TimeSpan now = item.HoraTime.TimeOfDay;
            //    if (now >= start && now <= end)
            //    {
            //        if (!dictTotales.ContainsKey(now))
            //        {
            //            dictTotales.Add(now, 0);
            //        }
            //        dictTotales[now]++;
            //    }
            //}
            //var strop = "";

            AnDataFlashOrdered.InsertarElementosActuales(lista, contexto);
            AnDataInfoPosRank.AddInfoPosRank(contexto, laFecha, laFechaMax);
            //AnDataFlashOrdered.InsertarElementosPercent(listaInfoPercent, contexto, VAL_TOTAL);

            //AnDataRank.AddInfoRank(contexto, VAL_TOTAL, laFecha);
            AnDataBinaryInfo.AddInfoBinary(contexto, laFecha);
        }

        private static void ReiniciarDatosFlashOrdered()
        {
            //    List<FLASHORDERED> lista = new List<FLASHORDERED>();
            //    var laFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
            //    var laFechaMax = DateTime.ParseExact("20180202", "yyyyMMdd", CultureInfo.InvariantCulture);
            //    for (var i = laFecha; i < laFechaMax;)
            //    {
            //        var date = i.ToString("yyyyMMdd");
            //        lista.AddRange(UtilGeneral.UtilHtml.LeerInfoHtml(i, 1, (decimal)lista.Count + 1));
            //        i = i.AddDays(1);
            //    }
            //    ////List<FLASHORDERED> lt = new List<FLASHORDERED>();
            //    ////List<decimal> repetidos = new List<decimal>();
            //    ////var groups = lista.GroupBy(info => info.ID)
            //    ////       .Select(group => new {
            //    ////           Metric = group.Key,
            //    ////           Count = group.Count()
            //    ////       })
            //    ////       .Where(x => x.Count > 1);
            //    ////List<FLASHORDERED> ltFls = new List<FLASHORDERED>();
            //    ////List<decimal> ltDeci = (from y in groups select y.Metric).ToList();
            //    ////ltFls = (from x in lista where ltDeci.IndexOf(x.ID) != -1 select x).ToList();
            //    ////if (groups.Count() != 0)
            //    ////{
            //    ////    var stop2 = "";
            //    ////}
            //    lista = AnDataFlashOrdered.AnalizarGeneral(lista);
            //    List<FLASHORDERED> listawrite = new List<FLASHORDERED>();
            //    ////List<FLASHORDERED> listaAll = new List<FLASHORDERED>();
            //    ////listaAll.AddRange(lista);
            //    int subLenght = lista.Count() / 10;
            //    int indexInit = 0;
            //    int part = 1;
            //    do
            //    {
            //        if (indexInit + subLenght > lista.Count())
            //        {
            //            listawrite = lista.GetRange(indexInit, lista.Count());
            //            lista.RemoveRange(indexInit, lista.Count());
            //        }
            //        else
            //        {
            //            listawrite = lista.GetRange(indexInit, indexInit + subLenght);
            //            lista.RemoveRange(indexInit, indexInit + subLenght);
            //        }
            //        EscribirArchivoCsv(listawrite, @"D:\temp2\file" + (part++) + ".csv", @"D:\temp2");
            //        //indexInit = indexInit + subLenght + 1;
            //    } while (lista.Count() > 0);
            //    ////laFecha = DateTime.ParseExact("20171025", "yyyyMMdd", CultureInfo.InvariantCulture);
            //    ////for (var i = laFecha; i < DateTime.Today;)
            //    ////{
            //    ////    string pathWriteFile = "";
            //    ////    string pathWrite = "";
            //    ////    List<HtmlDTO> lista = UtilGeneral.UtilHtml.LeerInfoHtml(i, 2, out pathWriteFile, out pathWrite);
            //    ////EscribirArchivoCsv(lista, @"D:\temp.csv", @"D:\");
            //    ////    i = i.AddDays(1);
            //    ////}
            //    var stop = "";
            AnDataFlashOrdered.GuardarElementosGeneral(contexto, 12);
        }

        //public static void LeerHtmlRC(String fechaMes, String fechaDia)
        //{
        //    string path = @"D:\OneDrive\Estimaciones\RC\" + fechaMes + @"\" + fechaDia + ".html";
        //    string pathWriteFile = @"D:\OneDrive\Estimaciones\RCO\" + fechaMes + @"\" + fechaDia + ".csv";
        //    string pathWrite = @"D:\OneDrive\Estimaciones\RCO\" + fechaMes;
        //    var htmlWeb = new HtmlWeb();
        //    htmlWeb.OverrideEncoding = Encoding.UTF8;
        //    var doc = htmlWeb.Load(path);

        //    var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
        //    List<HtmlDTO> lista = new List<HtmlDTO>();
        //    foreach (HtmlNode  item in htmlNodes)
        //    {
        //        var htmlDoc = new HtmlDocument();
        //        htmlDoc.LoadHtml(item.InnerHtml);

        //        var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
        //        HtmlDTO htmlDTO = new HtmlDTO();
        //        var theNodes = htmltdNodes.ToString();
        //        StringBuilder sb = new StringBuilder();
        //        bool esAfter = false;
        //        for (int i = 0; i < htmltdNodes.Count; i++)
        //        {
        //            var item2 = htmltdNodes.ElementAt(i);
        //            var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;","&");
        //            if(i.Equals(1) && data.ToLower().Contains("tras"))
        //            {
        //                data = "Finalizado";
        //                esAfter = true;
        //            }
        //            if(i.Equals(3) && esAfter)
        //            {
        //                var htmlDocTd = new HtmlDocument();
        //                htmlDocTd.LoadHtml(item2.InnerHtml);
        //                var data2 = htmlDocTd.DocumentNode.SelectNodes("//span");
        //                var result = data2.ElementAt(0).InnerText.Replace("&nbsp;", "").Replace("(", "").Replace(")", "");
        //                sb.Append(result);
        //                sb.Append(";");
        //            }
        //            else {
        //                sb.Append(data);
        //                sb.Append(";");
        //            }
        //        }
        //        string[] datos = sb.ToString().Split(';');
        //        htmlDTO.Hora = datos[0];
        //        htmlDTO.Estado = datos[1];
        //        htmlDTO.Home = datos[2];
        //        htmlDTO.Result = datos[3];
        //        htmlDTO.Away = datos[4];
        //        htmlDTO.Half = datos[5].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Replace(" ","");
        //        lista.Add(htmlDTO);
        //    }
        //    lista = lista.OrderBy(x => x.Home).ThenBy(x => x.Away).ToList();
        //    int indexer = 0;
        //    char letter = lista.ElementAt(0).Home[0];
        //    int indexLetter = 1;
        //    foreach (var item in lista)
        //    {
        //        item.IndexOrdered = ++indexer;
        //        if (!item.Home[0].Equals(letter))
        //        {
        //            indexLetter = 1;
        //            letter = item.Home[0];
        //        }
        //        if (item.Estado.ToLower().Equals("finalizado"))
        //        {
        //            string[] score = item.Result.Split('-');
        //            item.GHome = Convert.ToInt32(score[0]);
        //            item.GAway = Convert.ToInt32(score[1]);
        //            item.DIFERENCIAG = item.GHome - item.GAway;
        //            item.TotalG = item.GHome + item.GAway;
        //        }
        //        item.GroupLetter = letter;
        //        item.GroupIndexLetter = indexLetter++;
        //    }
        //    lista = lista.Where(x => x.Estado.ToLower().Equals("finalizado")).ToList();
        //    EscribirArchivoCsv(lista, pathWriteFile, pathWrite);
        //}

        //public static void LeerHtml(String fechaMes, String fechaDia)
        //{
        //    string path = @"D:\OneDrive\Estimaciones\FS\" + fechaMes + @"\" + fechaDia + ".html";
        //    string pathWriteFile = @"D:\OneDrive\Estimaciones\FSO\" + fechaMes + @"\" + fechaDia + ".csv";
        //    string pathWrite = @"D:\OneDrive\Estimaciones\FSO\" + fechaMes;
        //    var htmlWeb = new HtmlWeb();
        //    htmlWeb.OverrideEncoding = Encoding.UTF8;
        //    var doc = htmlWeb.Load(path);

        //    var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
        //    List<HtmlDTO> lista = new List<HtmlDTO>();
        //    foreach (HtmlNode item in htmlNodes)
        //    {
        //        var htmlDoc = new HtmlDocument();
        //        htmlDoc.LoadHtml(item.InnerHtml);

        //        var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
        //        HtmlDTO htmlDTO = new HtmlDTO();
        //        var theNodes = htmltdNodes.ToString();
        //        StringBuilder sb = new StringBuilder();
        //        bool esAfter = false;
        //        for (int i = 0; i < htmltdNodes.Count; i++)
        //        {
        //            var item2 = htmltdNodes.ElementAt(i);
        //            var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;", "&");
        //            if (i.Equals(2) && data.ToLower().Contains("after"))
        //            {
        //                data = "Finished";
        //                esAfter = true;
        //            }
        //            if (i.Equals(4) && esAfter)
        //            {
        //                var htmlDocTd = new HtmlDocument();
        //                htmlDocTd.LoadHtml(item2.InnerHtml);
        //                var data2 = htmlDocTd.DocumentNode.SelectNodes("//span");
        //                var result = data2.ElementAt(0).InnerText.Replace("&nbsp;", "").Replace("(", "").Replace(")", "");
        //                sb.Append(result);
        //                sb.Append(";");
        //            }
        //            else
        //            {
        //                sb.Append(data);
        //                sb.Append(";");
        //            }
        //        }
        //        string[] datos = sb.ToString().Split(';');
        //        htmlDTO.Hora = datos[1];
        //        htmlDTO.Estado = datos[2];
        //        htmlDTO.Home = datos[3];
        //        htmlDTO.Result = datos[4];
        //        htmlDTO.Away = datos[5];
        //        htmlDTO.Half = datos[6].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Replace(" ", "");
        //        lista.Add(htmlDTO);
        //    }
        //    lista = lista.OrderBy(x => x.Home).ThenBy(x => x.Away).ToList();
        //    int indexer = 0;
        //    char letter = lista.ElementAt(0).Home[0];
        //    int indexLetter = 1;
        //    foreach (var item in lista)
        //    {
        //        item.IndexOrdered = ++indexer;
        //        if (!item.Home[0].Equals(letter))
        //        {
        //            indexLetter = 1;
        //            letter = item.Home[0];
        //        }
        //        if (item.Estado.ToLower().Equals("finished"))
        //        {
        //            string[] score = item.Result.Split('-');
        //            item.GHome = Convert.ToInt32(score[0]);
        //            item.GAway = Convert.ToInt32(score[1]);
        //            item.DIFERENCIAG = item.GHome - item.GAway;
        //            item.TotalG = item.GHome + item.GAway;
        //        }
        //        item.GroupLetter = letter;
        //        item.GroupIndexLetter = indexLetter++;
        //    }
        //    lista = lista.Where(x => x.Estado.ToLower().Equals("finished")).ToList();
        //    EscribirArchivoCsv(lista, pathWriteFile, pathWrite);
        //}

        private static void AnalizarTabindexResultados(string filename)
        {
            StreamReader fileReader;
            DateTime dt = DateTime.ParseExact(filename, "yyyyMMdd", CultureInfo.InvariantCulture);
            string rutaFileTemp = ConstantesGenerales.rutaBase + dt.ToString("yyyyMM") + "\\" + filename + "T.csv";
            string rutaFileFinal = ConstantesGenerales.rutaBase + dt.ToString("yyyyMM") + "\\" + filename + "F.csv";
            string rutaFinal = ConstantesGenerales.rutaBase + dt.ToString("yyyyMM") + "\\" + filename + ".csv";
            IEnumerable<string> lines = File.ReadAllLines(rutaFileFinal);
            List<string> linesFinal = new List<string>();
            fileReader = new StreamReader(rutaFileTemp);
            String line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] arreglo = line.Split(';');
                string eHome = arreglo[3].Trim();
                string eVisitor = arreglo[5].Trim();
                var matchHome = lines.Where(x => x.IndexOf(eHome, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                var matchVisitor = lines.Where(x => x.IndexOf(eHome, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                if (matchHome != null && matchVisitor != null && matchHome.Equals(matchVisitor))
                {
                    var arrayFinal = matchHome.Split(';');
                    var arrayPush = new string[11];
                    arrayPush[0] = arreglo[0];
                    arrayPush[1] = arreglo[1];
                    arrayPush[3] = arreglo[3];
                    arrayPush[4] = arrayFinal[4];
                    arrayPush[5] = arreglo[5];
                    arrayPush[7] = arrayFinal[7];
                    arrayPush[8] = arrayFinal[8];
                    arrayPush[9] = arrayFinal[9];
                    arrayPush[10] = arrayFinal[10];

                    linesFinal.Add(String.Join(";", arrayPush));
                }
            }
            File.WriteAllLines(rutaFinal, linesFinal, Encoding.UTF8);
        }

        public static int[] CalcularNumeros()
        {
            int[] numeros = new int[7];
            Random r = new Random();

            int auxiliar = 0;
            int contador = 0;

            for (int i = 0; i < 7; i++)
            {
                auxiliar = r.Next(1, 30);
                bool continuar = false;

                while (!continuar)
                {
                    for (int j = 0; j <= contador; j++)
                    {
                        if (auxiliar == numeros[j])
                        {
                            continuar = true;
                            j = contador;
                        }
                    }

                    if (continuar)
                    {
                        auxiliar = r.Next(1, 30);
                        continuar = false;
                    }
                    else
                    {
                        continuar = true;
                        numeros[contador] = auxiliar;
                        contador++;
                    }
                }
            }

            return numeros;
        }

        //public static void AnalizarUnGanadorLvl2(List<string> filenames)
        //{
        //    for (int i = 0; i < filenames.Count; i++)
        //    {
        //        DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
        //        TimeSpan ts = dt - minFecha;
        //        int percent = ts.Days * PercentAnalisis / 100;
        //        AnDataUnGanador.AnalizarDatosPorDiaSeleccionLvl2(dt, contexto, percent);
        //    }
        //}

        //public static void AnalizarUnGanadorLvl3(string rutaBase)
        //{
        //    List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
        //    DateTime fechaMinLvl1 = DateTime.ParseExact("20170408", "yyyyMMdd", CultureInfo.InvariantCulture);
        //    DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
        //    for (; fechaMinLvl1 < DateTime.Today;)
        //    {
        //        TimeSpan ts = fechaMinLvl1 - minFecha;
        //        int percent = ts.Days * PercentAnalisis / 100;
        //        listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionLvl3(fechaMinLvl1, contexto, percent, 5));
        //        fechaMinLvl1 = fechaMinLvl1.AddDays(1);
        //    }
        //    EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada2", rutaBase);
        //}
    }
}