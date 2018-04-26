using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.UtilGeneral
{
    public class UtilHtml
    {
        private const string pathBase = @"D:\OneDrive\Estimaciones\";
        private const string rc = @"RC\";
        private const string rco = @"RCO\";
        private const string fs = @"FS\";
        private const string fso = @"FSO\";

        public static List<FLASHORDERED> LeerHtml(int indexInicio, string strAfter, string strFinal, string path)
        {
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            var doc = htmlWeb.Load(path);

            var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
            List<FLASHORDERED> lista = new List<FLASHORDERED>();
            foreach (HtmlNode item in htmlNodes)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(item.InnerHtml);

                var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
                FLASHORDERED infoFs = new FLASHORDERED();
                var theNodes = htmltdNodes.ToString();
                StringBuilder sb = new StringBuilder();
                bool esAfter = false;
                for (int i = indexInicio; i < htmltdNodes.Count; i++)
                {
                    var item2 = htmltdNodes.ElementAt(i);
                    var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;", "&").Replace("\n","").Replace("\r", "").Trim();
                    if (i.Equals(indexInicio+1) && data.ToLower().Contains(strAfter.ToLower()))
                    {
                        data = strFinal;
                        esAfter = true;
                    }
                    if (i.Equals(indexInicio+3) && esAfter)
                    {
                        var htmlDocTd = new HtmlDocument();
                        htmlDocTd.LoadHtml(item2.InnerHtml);
                        var data2 = htmlDocTd.DocumentNode.SelectNodes("//span");
                        var result = data2.ElementAt(0).InnerText.Replace("&nbsp;", "").Replace("(", "").Replace(")", "");
                        sb.Append(result);
                        sb.Append(";");
                    }
                    else
                    {
                        sb.Append(data);
                        sb.Append(";");
                    }
                }
                string[] datos = sb.ToString().Split(';');
                infoFs.Hora = datos[0];
                infoFs.Estado = datos[1];
                infoFs.Home = datos[2];
                infoFs.RESULT = datos[3];
                infoFs.Away = datos[4];
                infoFs.Half = datos[5].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Trim();
                lista.Add(infoFs);
            }
            List<FLASHORDERED> listaTemp = new List<FLASHORDERED>();
            listaTemp.AddRange(lista);
            lista.Clear();
            foreach (var item in listaTemp)
            {
                FLASHORDERED added = (from x in lista
                                      where x.Away == item.Away
                                      && x.Home == item.Home
                                      && x.Hora.Trim().Substring(0, 2).Equals(item.Hora.Trim().Substring(0, 2))
                                      select x)
                                   .FirstOrDefault();
                if (added == null)
                {
                    item.intChar = Convert.ToInt32(item.Home[0]);
                    lista.Add(item);
                }

            }

            lista = lista.OrderBy(x => x.intChar).ThenBy(x => x.Home).ThenBy(x => x.Away).ToList();
            
            int indexer = 0;
            string letter = lista.ElementAt(0).Home[0].ToString();
            int indexLetter = 1;
            foreach (var item in lista)
            {
                item.TABINDEX = ++indexer;
                if (!item.Home[0].ToString().Equals(letter))
                {
                    indexLetter = 1;
                    letter = item.Home[0].ToString();
                }
                if (item.Estado.ToLower().Equals(strFinal))
                {
                    string[] score = item.RESULT.Split('-');
                    item.GHOME = Convert.ToInt32(score[0]);
                    item.GAWAY = Convert.ToInt32(score[1]);
                    item.DIFERENCIAG = item.GHOME - item.GAWAY;
                    item.TOTALG = item.GHOME + item.GAWAY;
                }
                item.GROUPLETTER = letter;
                item.TABINDEXLETTER = indexLetter++;
            }
            return lista;
        }

        public static List<FLASHORDERED> LeerInfoHtml(
            DateTime fecha, int caso, decimal idInicio)
        {
            List<FLASHORDERED> listaHtmlFinal = new List<FLASHORDERED>();
            string fechaMes = fecha.ToString("yyyyMM");
            string fechaDia = fecha.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaDia);
            string strAfter = "";
            string strFinal = "";
            string path = "";
            int indexInicio;
            int diaSem = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int diaMes = fecha.Day;
            int diaAnio = fecha.DayOfYear;
            if (caso == 1)
            {
                path = pathBase + fs + fechaMes + "\\" + fechaDia;
                indexInicio = 1;
                strAfter = "after";
                strFinal = "finished";
            }
            else
            {
                path = pathBase + rc + fechaMes + "\\" + fechaDia;
                indexInicio = 0;
                strAfter = "tras";
                strFinal = "finalizado";
            }

            if (!File.Exists(path + ".html"))
            {
                var pathTemp = path + "T" + ".html";
                var pathFinal = path + "F" + ".html";
                List<FLASHORDERED> listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, pathTemp);

                List<FLASHORDERED> listaFinal = LeerHtml(indexInicio, strAfter, strFinal, pathFinal);
                foreach (var item in listaHtmlTemp)
                {
                    List<FLASHORDERED> listaElementos = (from x in listaFinal
                                                         where x.Home == item.Home
                                                         && x.Away == item.Away
                                                         select x).ToList();
                    if (listaElementos.Any())
                    {
                        if (listaElementos.Count > 1)
                        {
                            FLASHORDERED elementoAdd = (from x in listaElementos
                                                        where x.Hora.Trim().Substring(0, 2).Equals(item.Hora.Trim().Substring(0, 2))
                                                        select x)
                                               .FirstOrDefault();

                            if (elementoAdd != null)
                            {
                                elementoAdd.TABINDEX = item.TABINDEX;
                                elementoAdd.TABINDEXLETTER = item.TABINDEXLETTER;
                                listaHtmlFinal.Add(elementoAdd);
                            }
                        }
                        else
                        {
                            listaElementos[0].TABINDEX = item.TABINDEX;
                            listaElementos[0].TABINDEXLETTER = item.TABINDEXLETTER;
                            listaHtmlFinal.Add(listaElementos[0]);
                        }
                    }
                }
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            else
            {
                listaHtmlFinal = LeerHtml(indexInicio, strAfter, strFinal, path + ".html");
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            foreach (var item in listaHtmlFinal)
            {
                item.DIASEM = diaSem;
                item.DIAMES = diaMes;
                item.DIAANIO = diaAnio;
                item.FECHA = fecha;
                item.ID = idInicio++;
                item.SPANTIANIHIST = item.DIFERENCIAG == 0 ? -1 : 1;
                item.SPANTIGLANIHIST = item.DIFERENCIAG == 0 ? -1 : 1;
                item.SPANTIANIACT = item.SPANTIANIHIST;
                item.SPANTIGLANIACT = item.SPANTIGLANIHIST;
                item.FECHANUM = fechaNum;
            }
            return listaHtmlFinal;
        }

        public static List<FLASHORDERED> LeerInfoHtml(
            DateTime fecha, decimal idInicio, int caso = 1)
        {
            List<FLASHORDERED> listaHtmlFinal = new List<FLASHORDERED>();
            string fechaMes = fecha.ToString("yyyyMM");
            string fechaDia = fecha.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaDia);
            string strAfter = "";
            string strFinal = "";
            string path = "";
            int indexInicio;
            int diaSem = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int diaMes = fecha.Day;
            int diaAnio = fecha.DayOfYear;
            if (caso == 1)
            {
                path = pathBase + fs + fechaMes + "\\" + fechaDia;
                indexInicio = 1;
                strAfter = "after";
                strFinal = "finished";
            }
            else
            {
                path = pathBase + rc + fechaMes + "\\" + fechaDia;
                indexInicio = 0;
                strAfter = "tras";
                strFinal = "finalizado";
            }

            if (!File.Exists(path + ".html"))
            {
                var pathTemp = path + "T" + ".html";
                var pathFinal = path + "F" + ".html";
                List<FLASHORDERED> listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, pathTemp);

                List<FLASHORDERED> listaFinal = LeerHtml(indexInicio, strAfter, strFinal, pathFinal);
                foreach (var item in listaHtmlTemp)
                {
                    List<FLASHORDERED> listaElementos = (from x in listaFinal
                                                         where x.Home == item.Home
                                                         && x.Away == item.Away
                                                         select x).ToList();
                    if (listaElementos.Any())
                    {
                        if (listaElementos.Count > 1)
                        {
                            FLASHORDERED elementoAdd = (from x in listaElementos
                                                        where x.Hora.Trim().Substring(0, 2).Equals(item.Hora.Trim().Substring(0, 2))
                                                        select x)
                                               .FirstOrDefault();

                            if (elementoAdd != null)
                            {
                                elementoAdd.TABINDEX = item.TABINDEX;
                                elementoAdd.TABINDEXLETTER = item.TABINDEXLETTER;
                                listaHtmlFinal.Add(elementoAdd);
                            }
                        }
                        else
                        {
                            listaElementos[0].TABINDEX = item.TABINDEX;
                            listaElementos[0].TABINDEXLETTER = item.TABINDEXLETTER;
                            listaHtmlFinal.Add(listaElementos[0]);
                        }
                    }
                }
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            else
            {
                listaHtmlFinal = LeerHtml(indexInicio, strAfter, strFinal, path + ".html");
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            foreach (var item in listaHtmlFinal)
            {
                item.DIASEM = diaSem;
                item.DIAMES = diaMes;
                item.DIAANIO = diaAnio;
                item.FECHA = fecha;
                item.ID = idInicio++;
                item.FECHANUM = fechaNum;
            }
            return listaHtmlFinal;
        }

        public static List<FLASHORDERED> LeerInfoHtmlTempActual(
            DateTime fecha, int caso)
        {
            string path = "";
            string fechaMes = fecha.ToString("yyyyMM");
            string fechaDia = fecha.ToString("yyyyMMdd");
            string strAfter = "";
            string strFinal = "";
            int indexInicio;
            if (caso == 1)
            {
                path = pathBase + fs + fechaMes + "\\" + fechaDia;
                indexInicio = 1;
                strAfter = "after";
                strFinal = "finished";
            }
            else
            {
                path = pathBase + rc + fechaMes + "\\" + fechaDia;
                indexInicio = 0;
                strAfter = "tras";
                strFinal = "finalizado";
            }
            var pathTemp = path + "T" + ".html";
            List<FLASHORDERED> listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, pathTemp);
            return listaHtmlTemp;
        }
    }
}
