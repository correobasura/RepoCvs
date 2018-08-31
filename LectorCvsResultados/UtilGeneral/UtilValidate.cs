using LectorCvsResultados.FlashOrdered;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LectorCvsResultados.UtilGeneral
{
    public class UtilValidate
    {
        public static void TestValidateMinReg(SisResultEntities contexto)
        {
            List<AgrupadorInfoGeneralDTO> listaTemp;
            List<FLASHORDERED> listaHtmlTemp;
            List<FLASHORDERED> listaDia;
            int fecha;
            Dictionary<int, InfoAnalisisDTO> dictTotalesDias = new Dictionary<int, InfoAnalisisDTO>();
            Dictionary<int, InfoAnalisisDTO> dictGen = new Dictionary<int, InfoAnalisisDTO>();
            for (int j = 50; j < 450; j++)
            {
                dictGen.Add(j, new InfoAnalisisDTO());
                dictTotalesDias.Clear();
                for (var i = DateTime.Today.AddDays(-30); i < DateTime.Today; i = i.AddDays(1))
                {
                    fecha = Convert.ToInt32(i.ToString("yyyyMMdd"));
                    dictTotalesDias.Add(fecha, new InfoAnalisisDTO());

                    listaHtmlTemp = AnDataFlashOrdered.GetListaTemp(i, 1, contexto, j);
                    listaTemp = AnDataFlashOrdered.ValidarElementosDia(i, 1, contexto, listaHtmlTemp);
                    listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
                    foreach (var item in listaTemp)
                    {
                        var data = (from x in listaDia where x.TABINDEX == item.Tabindex select x).FirstOrDefault();
                        if (data == null) continue;
                        if (data.DIFERENCIAG == 0)
                        {
                            dictTotalesDias[fecha].Negativos++;
                        }
                        else
                        {
                            dictTotalesDias[fecha].Positivos++;
                        }
                    }
                }
                dictGen[j].Positivos = (from entry in dictTotalesDias select entry.Value.Positivos).Sum();
                dictGen[j].Negativos = (from entry in dictTotalesDias select entry.Value.Negativos).Sum();
            }
            dictGen = (from entry in dictGen orderby entry.Value.Positivos descending, entry.Value.Negativos select entry).ToDictionary(x => x.Key, x => x.Value);
            var dataIn = "";
        }
    }
}