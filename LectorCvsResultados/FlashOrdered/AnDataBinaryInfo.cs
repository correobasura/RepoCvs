using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataBinaryInfo
    {
        public static void AddInfoBinary(SisResultEntities contexto, int VAL_TOTAL, DateTime laFecha)
        {
            //var laFechaMax = DateTime.ParseExact(DateTime.Now.AddHours(3).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            //int idInicio = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_BININFO);
            //List<AgrupadorInfoGeneralDTO> listaTemp;
            //List<FLASHORDERED> listaDia;
            //List<FLASHORDERED> listaHtmlTemp;
            //int fecha;
            //int dayofweek;
            //ANDATABININFO a;
            //FLASHORDERED data;
            //for (var i = laFecha; i < laFechaMax; i = i.AddDays(1))
            //{
            //    fecha = Convert.ToInt32(i.ToString("yyyyMMdd"));
            //    listaHtmlTemp = AnDataFlashOrdered.GetListaTemp(i, 1, contexto, VAL_TOTAL);
            //    listaTemp = AnDataFlashOrdered.ValidarElementosDia(i, 1, contexto, listaHtmlTemp,0);
            //    listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
            //    dayofweek = (int)i.DayOfWeek == 0 ? 7 : (int)i.DayOfWeek;
            //    foreach (var item in listaTemp)
            //    {
            //        data = (from x in listaDia where x.TABINDEX == item.Tabindex select x).FirstOrDefault();
            //        if (data == null) continue;
            //        a = new ANDATABININFO();
            //        a.ID = idInicio++;
            //        a.KEYRANKGRAL = string.Join("", item.ListData);
            //        a.FECHANUM = fecha;
            //        a.DIASEM = dayofweek;
            //        a.DIAMES = i.Day;
            //        a.DIAANIO = i.DayOfYear;
            //        a.DIFERENCIAG = data.DIFERENCIAG;
            //        contexto.ANDATABININFO.Add(a);
            //    }
            //    contexto.SaveChanges();
            //}
        }
    }
}
