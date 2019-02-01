using LectorCvsResultados.UtilGeneral;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataBinaryInfo
    {
        public static void AddInfoBinary(SisResultEntities contexto, DateTime laFecha)
        {
            var laFechaMax = DateTime.ParseExact(DateTime.Now.AddHours(3).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            int idInicio = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_BININFO);
            List<AgrupadorInfoGeneralDTO> listaTemp;
            List<FLASHORDERED> listaDia;
            List<FLASHORDERED> listaHtmlTemp;
            int fecha;
            int dayofweek;
            ANDATABININFO a;
            FLASHORDERED data;
            string keyFile;
            for (var i = laFecha; i < laFechaMax; i = i.AddDays(1))
            {
                fecha = Convert.ToInt32(i.ToString("yyyyMMdd"));
                keyFile = "Dict" + i.ToString("yyyyMMdd") + ".txt";
                listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(i, 1);
                if (!File.Exists(ConstantesGenerales.rutaWriteReview + keyFile))
                {
                    listaTemp = AnDataFlashOrdered.ValidarElementosDia(i, 1, contexto, listaHtmlTemp);
                    UtilFilesIO.EscribirArchivoCsv(listaTemp, keyFile, ConstantesGenerales.rutaWriteReview);
                }
                else
                {
                    listaTemp = UtilFilesIO.ReadLinesFilesQueried(keyFile, ConstantesGenerales.rutaWriteReview);
                }
                listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
                dayofweek = (int)i.DayOfWeek == 0 ? 7 : (int)i.DayOfWeek;
                foreach (var item in listaTemp)
                {
                    data = (from x in listaDia where x.TABINDEX == item.Tabindex select x).FirstOrDefault();
                    if (data == null) continue;
                    a = new ANDATABININFO();
                    a.ID = idInicio++;
                    a.KEYRANKGRAL = item.ValRefGen + "_" + item.ValRefGlGen;
                    a.FECHANUM = fecha;
                    a.DIASEM = dayofweek;
                    a.DIAMES = i.Day;
                    a.DIAANIO = i.DayOfYear;
                    a.DIFERENCIAG = data.DIFERENCIAG;
                    a.TABINDEX = item.Tabindex;
                    a.TABINDEXLETTER = item.Tabindexletter;
                    a.GROUPLETTER = item.GroupLetter;
                    contexto.ANDATABININFO.Add(a);
                }
                contexto.SaveChanges();
            }
        }

        public static List<AgrupadorInfoQuery> AnDataKeyRank(SisResultEntities contexto, string fechanum, string fechanummin,
            int dayofweek)
        {
            string query = "select count(1) as Total, count(case when diferenciag != 0 then 1 end)/count(1) as Prob, keyrankgral as key, tabindex as tabindex"
+ " from ANDATABININFO"
+ " where fechanum < {0} and fechanum >= {1} and diasem = {2}"
+ " group by keyrankgral, tabindex"
+ " having count(1)>=5";
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(string.Format(query, fechanum, fechanummin, dayofweek));
            return data.ToList();
        }

        internal static List<AgrupadorInfoGeneralDTO> AnDataBinInfoGral(DateTime laFechaActual, SisResultEntities contexto, List<AgrupadorInfoGeneralDTO> listaTemp, int casoFiltro, int days = -500)
        {
            List<AgrupadorInfoQuery> listaBinInfo = new List<AgrupadorInfoQuery>();
            int dayofweek = (int)laFechaActual.DayOfWeek == 0 ? 7 : (int)laFechaActual.DayOfWeek;
            string filtro = GetFiltroBinInfo(laFechaActual, casoFiltro, dayofweek, days);
            string query = "SELECT COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) AS prob, "
+ "COUNT(1) AS total, "
+ "keyrankgral AS key, "
+ "RANK () OVER (ORDER BY COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) DESC) AS RANK "
+ "FROM andatabininfo "
+ filtro
+ "GROUP BY keyrankgral HAVING COUNT(1) > 1";
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            listaBinInfo = data.ToList();
            foreach (var item in listaTemp)
            {
                var key = string.Join("", item.ListData);
                item.BinInfoGral = (from x in listaBinInfo where x.Key.Equals(key) select x).FirstOrDefault();
                if (item.BinInfoGral == null)
                {
                    item.BinInfoGral = new AgrupadorInfoQuery();
                    item.BinInfoGral.Rank = 10000;
                }
            }
            return listaTemp;
        }

        private static string GetFiltroBinInfo(DateTime laFechaActual, int casoFiltro, int dayofweek, int days)
        {
            string filtro = "";
            switch (casoFiltro)
            {
                case 1:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diasem = " + dayofweek + " ";
                    break;
                case 2:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " ";
                    break;
                case 3:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " AND diasem = " + dayofweek + " ";
                    break;
                default:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND fechanum >= " + laFechaActual.AddDays(days).ToString("yyyyMMdd") + " ";
                    break;
            }

            return filtro;
        }

        internal static List<AgrupadorInfoGeneralDTO> AnDataBinInfoTab(DateTime laFechaActual, SisResultEntities contexto, List<AgrupadorInfoGeneralDTO> listaTemp, int casoFiltro)
        {
            List<AgrupadorInfoQuery> listaBinInfo = new List<AgrupadorInfoQuery>();
            int dayofweek = (int)laFechaActual.DayOfWeek == 0 ? 7 : (int)laFechaActual.DayOfWeek;
            string filtro = "";
            switch (casoFiltro)
            {
                case 1:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diasem = " + dayofweek + " ";
                    break;
                case 2:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " ";
                    break;
                case 3:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " AND diasem = " + dayofweek + " ";
                    break;
                default:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " ";
                    break;
            }
            string query = "SELECT COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) AS prob, "
+ "COUNT(1) AS total, "
+ "tabindex As tabindex, "
+ "keyrankgral AS key, "
+ "RANK () OVER (ORDER BY COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) DESC) AS RANK "
+ "FROM andatabininfo "
+ filtro
+ "GROUP BY tabindex, keyrankgral HAVING COUNT(1) > 1";
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            listaBinInfo = data.ToList();
            foreach (var item in listaTemp)
            {
                var key = string.Join("", item.ListData);
                item.BinInfoTabindex = (from x in listaBinInfo where x.Key.Equals(key) && x.TabIndex.Equals(item.Tabindex) select x).FirstOrDefault();
                if (item.BinInfoTabindex == null)
                {
                    item.BinInfoTabindex = new AgrupadorInfoQuery();
                    item.BinInfoTabindex.Rank = 10000;
                }
            }
            return listaTemp;
        }

        internal static List<AgrupadorInfoGeneralDTO> AnDataBinInfoGrLet(DateTime laFechaActual, SisResultEntities contexto, List<AgrupadorInfoGeneralDTO> listaTemp, int casoFiltro)
        {
            List<AgrupadorInfoQuery> listaBinInfo = new List<AgrupadorInfoQuery>();
            int dayofweek = (int)laFechaActual.DayOfWeek == 0 ? 7 : (int)laFechaActual.DayOfWeek;
            string filtro = "";
            switch (casoFiltro)
            {
                case 1:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diasem = " + dayofweek + " ";
                    break;
                case 2:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " ";
                    break;
                case 3:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " AND diames = " + laFechaActual.Day + " AND diasem = " + dayofweek + " ";
                    break;
                default:
                    filtro = "WHERE fechanum < " + laFechaActual.ToString("yyyyMMdd") + " ";
                    break;
            }
            string query = "SELECT COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) AS prob, "
+ "COUNT(1) AS total, "
+ "tabindexletter As tabindexletter, "
+ "groupletter As groupletter, "
+ "keyrankgral AS key, "
+ "RANK () OVER (ORDER BY COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) DESC) AS RANK "
+ "FROM andatabininfo "
+ filtro
+ "GROUP BY tabindexletter, groupletter, keyrankgral HAVING COUNT(1) > 1";
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            listaBinInfo = data.ToList();
            foreach (var item in listaTemp)
            {
                var key = string.Join("", item.ListData);
                item.BinInfoGrLet = (from x in listaBinInfo
                                     where x.Key.Equals(key) && x.TabIndexLetter.Equals(item.Tabindexletter)
              && x.GroupLetter.Equals(item.GroupLetter)
                                     select x).FirstOrDefault();
                if (item.BinInfoGrLet == null)
                {
                    item.BinInfoGrLet = new AgrupadorInfoQuery();
                    item.BinInfoGrLet.Rank = 10000;
                }
            }
            return listaTemp;
        }
    }
}
