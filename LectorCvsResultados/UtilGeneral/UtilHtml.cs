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

        public static List<HtmlDTO> LeerHtml(int indexInicio, string strAfter, string strFinal, string path)
        {
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            var doc = htmlWeb.Load(path);

            var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
            List<HtmlDTO> lista = new List<HtmlDTO>();
            foreach (HtmlNode item in htmlNodes)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(item.InnerHtml);

                var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
                HtmlDTO htmlDTO = new HtmlDTO();
                var theNodes = htmltdNodes.ToString();
                StringBuilder sb = new StringBuilder();
                bool esAfter = false;
                for (int i = indexInicio; i < htmltdNodes.Count; i++)
                {
                    var item2 = htmltdNodes.ElementAt(i);
                    var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;", "&");
                    if (i.Equals(1) && data.ToLower().Contains(strAfter))
                    {
                        data = strFinal;
                        esAfter = true;
                    }
                    if (i.Equals(3) && esAfter)
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
                htmlDTO.Hora = datos[0];
                htmlDTO.Estado = datos[1];
                htmlDTO.Home = datos[2];
                htmlDTO.Result = datos[3];
                htmlDTO.Away = datos[4];
                htmlDTO.Half = datos[5].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                lista.Add(htmlDTO);
            }
            lista = lista.OrderBy(x => x.Home).ThenBy(x => x.Away).ToList();
            int indexer = 0;
            char letter = lista.ElementAt(0).Home[0];
            int indexLetter = 1;
            foreach (var item in lista)
            {
                item.IndexOrdered = ++indexer;
                if (!item.Home[0].Equals(letter))
                {
                    indexLetter = 1;
                    letter = item.Home[0];
                }
                if (item.Estado.ToLower().Equals(strFinal))
                {
                    string[] score = item.Result.Split('-');
                    item.GHome = Convert.ToInt32(score[0]);
                    item.GAway = Convert.ToInt32(score[1]);
                    item.DiferenciaG = item.GHome - item.GAway;
                    item.TotalG = item.GHome + item.GAway;
                }
                item.GroupLetter = letter;
                item.GroupIndexLetter = indexLetter++;
            }
            return lista;
        }

        public static List<HtmlDTO> LeerInfoHtml(
            DateTime fecha, int caso, out string pathWriteFile, out string pathWrite)
        {
            List<HtmlDTO> listaHtmlFinal = new List<HtmlDTO>();
            string fechaMes = fecha.ToString("yyyyMM");
            string fechaDia = fecha.ToString("yyyyMMdd");
            string strAfter = "";
            string strFinal = "";
            string path = "";
            int indexInicio;
            if (caso == 1)
            {
                path = pathBase + fs + fechaMes + "\\" + fechaDia;
                pathWriteFile = pathBase + fso + fechaMes + "\\" + fechaDia + ".csv";
                pathWrite = pathBase + fso + fechaMes;
                indexInicio = 1;
                strAfter = "after";
                strFinal = "finished";
            }
            else
            {
                path = pathBase + rc + fechaMes + "\\" + fechaDia;
                pathWriteFile = pathBase + rco + fechaMes + "\\" + fechaDia + ".csv";
                pathWrite = pathBase + rco + fechaMes;
                indexInicio = 0;
                strAfter = "tras";
                strFinal = "finalizado";
            }

            if (!File.Exists(path+".html"))
            {
                var pathTemp = path + "T" + ".html";
                var pathFinal = path + "F" + ".html";
                List<HtmlDTO> listaHtmlTemp = LeerHtml(indexInicio, strAfter, strFinal, pathTemp);

                List<HtmlDTO> listaFinal = LeerHtml(indexInicio, strAfter, strFinal, pathFinal);
                foreach (var item in listaHtmlTemp)
                {
                    HtmlDTO dto = (from x in listaFinal
                                   where x.Home == item.Home
                                   && x.Away == item.Away
                                   select x).FirstOrDefault();
                    if (dto != null)
                    {
                        dto.IndexOrdered = item.IndexOrdered;
                        dto.GroupIndexLetter = item.GroupIndexLetter;
                        listaHtmlFinal.Add(dto);
                    }
                }
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            else
            {
                listaHtmlFinal = LeerHtml(indexInicio, strAfter, strFinal, path + ".html");
                listaHtmlFinal = listaHtmlFinal.Where(x => x.Estado.ToLower().Equals(strFinal)).ToList();
            }
            return listaHtmlFinal;
        }
    }
}
