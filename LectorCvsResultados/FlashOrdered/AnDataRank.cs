﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataRank
    {
        public static void AddInfoRank(SisResultEntities contexto, int VAL_TOTAL, DateTime laFecha)
        {
            var laFechaMax = DateTime.ParseExact(DateTime.Now.AddHours(3).ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
            int idInicio = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_MINRANK);
            List<AgrupadorInfoGeneralDTO> listaTemp;
            List<FLASHORDERED> listaDia;
            List<FLASHORDERED> listaHtmlTemp;
            int fecha;
            int dayofweek;
            ANDATAMINRANK a;
            FLASHORDERED data;
            for (var i = laFecha; i < laFechaMax; i = i.AddDays(1))
            {
                fecha = Convert.ToInt32(i.ToString("yyyyMMdd"));
                listaHtmlTemp = AnDataFlashOrdered.GetListaTemp(i, 1, contexto, VAL_TOTAL);
                listaTemp = AnDataFlashOrdered.ValidarElementosDia(i, 1, contexto, listaHtmlTemp);
                listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
                dayofweek = (int)i.DayOfWeek == 0 ? 7 : (int)i.DayOfWeek;
                foreach (var item in listaTemp)
                {
                    data = (from x in listaDia where x.TABINDEX == item.Tabindex select x).FirstOrDefault();
                    if (data == null) continue;
                    a = new ANDATAMINRANK();
                    a.ID = idInicio++;
                    a.INFOAGRUPDIASEMMESTI = item.ListData.ElementAt(0);
                    a.INFOAGRUPDIASEMMESGL = item.ListData.ElementAt(1);
                    a.RANKSPANACTUALGEN = item.ListData.ElementAt(2);
                    a.RANKSPANACTUALDIASEM = item.ListData.ElementAt(3);
                    a.RANKSPANACTUALDIAMES = item.ListData.ElementAt(4);
                    a.RANKSPANACTUALGLGEN = item.ListData.ElementAt(5);
                    a.RANKSPANACTUALGLDIASEM = item.ListData.ElementAt(6);
                    a.RANKSPANACTUALGLDIAMES = item.ListData.ElementAt(7);
                    a.RANKSPANACTUALGLDIAANIO = item.ListData.ElementAt(8);
                    a.RANKSPANACTUALDIAANIO = item.ListData.ElementAt(9);
                    a.KEYRANKGRAL = string.Join("", item.ListData);
                    a.KEYRANK = string.Join("", item.ListData.GetRange(2, 8));
                    a.FECHANUM = fecha;
                    a.DIASEM = dayofweek;
                    a.DIAMES = i.Day;
                    a.DIAANIO = i.DayOfYear;
                    a.DIFERENCIAG = data.DIFERENCIAG;
                    contexto.ANDATAMINRANK.Add(a);
                }
                contexto.SaveChanges();
            }
        }
    }
}
