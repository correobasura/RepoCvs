using HtmlAgilityPack;
using LectorCvsResultados.FlashOrdered;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
            int indexer = 0;
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
                    var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;", "&").Replace("\n", "").Replace("\r", "").Trim();
                    if (i.Equals(indexInicio + 1) && data.ToLower().Contains(strAfter.ToLower()))
                    {
                        data = strFinal;
                        esAfter = true;
                    }
                    if (i.Equals(indexInicio + 3) && esAfter)
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
                infoFs.Home = datos[2].ToUpper();
                infoFs.RESULT = datos[3];
                infoFs.Away = datos[4].ToUpper();
                infoFs.Half = datos[5].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Trim();

                infoFs.TABINDEX = ++indexer;
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

            string letter = lista.ElementAt(0).Home[0].ToString().ToUpper();
            int indexLetter = 1;
            foreach (var item in lista)
            {
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
            lista = lista.OrderBy(x => x.TABINDEX).ToList();
            return lista;
        }

        public static List<FLASHORDERED> LeerInfoHtmlResetAll(
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
            List<FLASHORDERED> listaHtmlTemp = new List<FLASHORDERED>();
            if (!File.Exists(pathTemp))
            {
                listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, path + ".html");
            }
            else
            {
                listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, pathTemp);
            }
            foreach (var item in listaHtmlTemp)
            {
                item.FECHA = fecha;
            }

            return listaHtmlTemp;
        }

        public static string ObtenerJoinElementos(List<FLASHORDERED> listaElementos)
        {
            List<string> listaDistinctChar = listaElementos.Select(x => x.GROUPLETTER).Distinct().ToList();
            Dictionary<string, int> keyValuePairMaxIndexChar = new Dictionary<string, int>();
            foreach (var item in listaDistinctChar)
            {
                int maxValueIndexChar = (from x in listaElementos
                                         where x.GROUPLETTER.Equals(item)
                                         select x.TABINDEXLETTER).Max();
                keyValuePairMaxIndexChar.Add(item, maxValueIndexChar);
            }
            string agrupador = "({0})";
            string temp = "(" + ConstantesModel.GROUPLETTER + "  = '{0}' AND " + ConstantesModel.TABINDEXLETTER + " <= {1})";
            List<string> listaJoins = new List<string>();
            foreach (var item in keyValuePairMaxIndexChar)
            {
                listaJoins.Add(string.Format(temp, item.Key, item.Value));
            }
            string strJoin = string.Join(" OR ", listaJoins);
            strJoin = string.Format(agrupador, strJoin);
            return strJoin;
        }

        public static string GetAsync(DateTime fecha, string pathTemplate)
        {
            string anio = fecha.ToString("yyyy");
            string mes = fecha.ToString("MM");
            string dia = fecha.ToString("dd");
            string html = string.Empty;
            string url = @"https://es.soccerway.com/matches/{0}/{1}/{2}/";
            //string urlIn = @"https://es.soccerway.com/a/block_date_matches?block_id=page_matches_1_block_date_matches_1&callback_params={"block_service_id":"matches_index_block_datematches","date":"2010-01-01"}&action=showMatches&params={"competition_id":583}"

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url, anio, mes, dia));
            //request.AutomaticDecompression = DecompressionMethods.GZip;

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    html = reader.ReadToEnd();
            //}
            var idCompetition = "";
            var doc = GetAsyncIn(fecha, idCompetition, 0);
            //var htmlWeb = new HtmlWeb();
            //htmlWeb.OverrideEncoding = Encoding.UTF8;
            //var doc = htmlWeb.Load(string.Format(url, anio, mes, dia));
            //var bodyInfo = doc.DocumentNode.SelectNodes("//tbody")[0];
            var bodyInfo = doc.DocumentNode.SelectNodes("//tbody")[0];
            var htmlBodyContent = new HtmlDocument();
            htmlBodyContent.LoadHtml(bodyInfo.InnerHtml);
            var htmltrNodes = htmlBodyContent.DocumentNode.SelectNodes("//tr").Where(x => x.Id != string.Empty);

            var expanded = false;
            StringBuilder sb = new StringBuilder();
            Regex rgxNumbers = new Regex(@"[0-9]");
            Regex rgxLetters = new Regex(@"[a-zA-Z]");

            foreach (var item in htmltrNodes)
            {
                var atributtes = item.Attributes.ToDictionary(x => x.Name, x => x.Value);
                expanded = atributtes["class"].IndexOf("expanded") != -1;
                if (item.Id.Equals(string.Empty)) continue;
                if (atributtes.ContainsKey("stage-value"))
                {
                    idCompetition = item.Id.Split('-')[1];
                    if (expanded)
                        continue;
                }
                if (expanded)
                {
                    sb.Append(CreateTrNode(GetMatchData(rgxNumbers, rgxLetters, item)));
                }
                else
                {
                    var dataNodes = GetAsyncIn(fecha, idCompetition, 1).DocumentNode.SelectNodes("//tr");
                    foreach (var dataNode in dataNodes)
                    {
                        if (dataNode.Id.Equals(string.Empty)) continue;
                        sb.Append(CreateTrNode(GetMatchData(rgxNumbers, rgxLetters, dataNode)));
                    }
                }

            }
            html = GetTemplate(pathTemplate) + sb.ToString() + "</table></body></html>";
            return html;
        }

        private static string GetMatchData(Regex rgxNumbers, Regex rgxLetters, HtmlNode item)
        {
            List<string> lstInfo = new List<string>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(item.InnerHtml);

            var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
            lstInfo.Add(htmltdNodes[0].InnerText);
            lstInfo.Add(getAnchorInfo(htmltdNodes[1].InnerHtml, "title"));
            string result = getAnchorInfo(htmltdNodes[2].InnerHtml, "");
            if (rgxNumbers.IsMatch(result))
            {
                if (rgxLetters.IsMatch(result))
                {
                    htmlDoc = GetAsyncIn(DateTime.Today, getAnchorInfo(htmltdNodes[2].InnerHtml, "href"), 2);
                    var info = htmlDoc.DocumentNode.SelectNodes("//div[@id='page_match_1_block_match_info_4-wrapper']");
                    htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(info[0].InnerHtml);
                    var infoDetails = htmlDoc.DocumentNode.SelectNodes("//div[@class='details clearfix']");
                    htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(infoDetails[1].InnerHtml);
                    var ddNodes = htmlDoc.DocumentNode.SelectNodes("//dd");
                    result = ddNodes[0].InnerText;
                }
            }
            lstInfo.Add(result);
            lstInfo.Add(getAnchorInfo(htmltdNodes[3].InnerHtml, "title"));
            return CreateTdNotes(lstInfo);
        }

        private static string getAnchorInfo(string innerText, string property)
        {
            var document = new HtmlDocument();
            document.LoadHtml(innerText);
            var anchorNodes = document.DocumentNode.SelectNodes("//a");
            return property != string.Empty ? anchorNodes[0].Attributes.ToDictionary(x => x.Name, x => x.Value)[property]
                : anchorNodes[0].InnerText.Replace("\n", "").Trim();
        }

        private static string GetTemplate(string pathTemplate)
        {
            return File.ReadAllText(pathTemplate);
        }

        private static string CreateTrNode(string info)
        {
            return "<tr>" + info + "</tr>";
        }

        private static string CreateTdNotes(List<string> lstInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td></td><td></td>");
            foreach (var item in lstInfo)
            {
                sb.Append("<td>" + item + "</td>");
            }
            return sb.ToString();
        }

        public static HtmlDocument GetAsyncIn(DateTime fecha, string info, int caso)
        {
            string anio = fecha.ToString("yyyy");
            string mes = fecha.ToString("MM");
            string dia = fecha.ToString("dd");
            string html = string.Empty;
            string url = "";
            var htmlDocument = new HtmlDocument();
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            switch (caso)
            {
                case 1:
                    url = @"https://es.soccerway.com/a/block_date_matches?block_id=page_matches_1_block_date_matches_1&callback_params=%7B%22block_service_id%22%3A%22matches_index_block_datematches%22%2C%22date%22%3A%22{0}-{1}-{2}%22%2C%22stage-value%22%3A%221%22%7D&action=showMatches&params=%7B%22competition_id%22%3A{3}%7D";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url, anio, mes, dia, info));
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                    JObject json = JObject.Parse(html);
                    htmlDocument.LoadHtml(json.SelectToken("commands[0].parameters.content").ToString().Replace("\\/", "/"));
                    break;
                case 2:
                    url = @"https://es.soccerway.com" + info;
                    htmlDocument = htmlWeb.Load(string.Format(url, anio, mes, dia));
                    break;
                default:
                    url = @"https://es.soccerway.com/matches/{0}/{1}/{2}/";
                    htmlDocument = htmlWeb.Load(string.Format(url, anio, mes, dia));
                    break;
            }
            return htmlDocument;
        }
    }
}