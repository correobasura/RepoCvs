using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataFlashOrdered
    {
        public static List<FLASHORDERED> AnalizarGeneral(List<FLASHORDERED> lista)
        {
            List<decimal?> listaDistinctIndex = lista.Select(x => x.TABINDEX).Distinct().ToList();
            foreach (var item in listaDistinctIndex)
            {
                int indexActual = 0;
                var listaItems = (from x in lista where x.TABINDEX.Equals(item) select x).OrderBy(x => x.FECHA);
                foreach (var item2 in listaItems)
                {
                    if (item2.DIFERENCIAG == 0)
                    {
                        if (indexActual < 0)
                        {
                            indexActual--;
                        }
                        else
                        {
                            indexActual = -1;
                        }
                    }
                    else
                    {
                        if (indexActual > 0)
                        {
                            indexActual++;
                        }
                        else
                        {
                            indexActual = 1;
                        }
                    }
                    item2.SPANDIARIOHISTORICO = indexActual;
                }
                for (decimal i = 1; i <= 7; i++)
                {
                    indexActual = 0;
                    var listaItemsDia = (from x in listaItems where x.DIASEM.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (itemDia.DIFERENCIAG == 0)
                        {
                            if (indexActual < 0)
                            {
                                indexActual--;
                            }
                            else
                            {
                                indexActual = -1;
                            }
                        }
                        else
                        {
                            if (indexActual > 0)
                            {
                                indexActual++;
                            }
                            else
                            {
                                indexActual = 1;
                            }
                        }
                        itemDia.SPANSEMANAHISTORICO = indexActual;
                    }
                }
                for (decimal i = 1; i <= 31; i++)
                {
                    indexActual = 0;
                    var listaItemsDiaMes = (from x in listaItems where x.DIAMES.Equals(i) select x);
                    foreach (var itemDia in listaItemsDiaMes)
                    {
                        if (itemDia.DIFERENCIAG == 0)
                        {
                            if (indexActual < 0)
                            {
                                indexActual--;
                            }
                            else
                            {
                                indexActual = -1;
                            }
                        }
                        else
                        {
                            if (indexActual > 0)
                            {
                                indexActual++;
                            }
                            else
                            {
                                indexActual = 1;
                            }
                        }
                        itemDia.SPANMESHISTORICO = indexActual;
                    }
                }

                listaItems = (from x in lista where x.TABINDEX.Equals(item) select x).OrderByDescending(x => x.FECHA);
                bool asignar = true;
                foreach (var item2 in listaItems)
                {
                    if (asignar)
                    {
                        item2.SPANDIARIOACTUAL = item2.SPANDIARIOHISTORICO;
                    }
                    else
                    {
                        item2.SPANDIARIOACTUAL = null;
                    }
                    asignar = item2.SPANDIARIOHISTORICO == 1 || item2.SPANDIARIOHISTORICO == -1;
                }
                asignar = true;
                for (decimal i = 1; i <= 7; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIASEM.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANSEMANAACTUAL = itemDia.SPANSEMANAHISTORICO;
                        }
                        else
                        {
                            itemDia.SPANSEMANAACTUAL = null;
                        }
                        asignar = itemDia.SPANSEMANAHISTORICO == 1 || itemDia.SPANSEMANAHISTORICO == -1;
                    }
                }
                asignar = true;
                for (decimal i = 1; i <= 31; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIAMES.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANMESACTUAL = itemDia.SPANMESHISTORICO;
                        }
                        else
                        {
                            itemDia.SPANMESACTUAL = null;
                        }
                        asignar = itemDia.SPANMESHISTORICO == 1 || itemDia.SPANMESHISTORICO == -1;
                    }
                }
            }
            lista = lista.OrderBy(x => x.ID).ToList();
            //GuardarElementos(contexto, lista);
            return lista;
        }

        public static void GuardarElementos(SisResultEntities contexto, int maxIdFile)
        {
            List<FLASHORDERED> lista = new List<FLASHORDERED>();
            for (int i = 1; i < maxIdFile; i++)
            {
                string filePath = @"D:\temp" + i + ".csv";
                StreamReader fileReader = new StreamReader(filePath);
                String line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    FLASHORDERED fs = JsonConvert.DeserializeObject<FLASHORDERED>(line);
                    //if (fs.GROUPLETTER.Length > 1)
                    //{
                    //    var data3 = "";
                    //}
                    lista.Add(fs);
                }
                //var data = (from x in LT where x.GROUPLETTER.Length != 1 select x);
                //var data2 = "";
                contexto.FLASHORDERED.AddRange(lista);
                contexto.SaveChanges();
                lista.Clear();
            }
           //var groups = lista.GroupBy(info => info.ID)
           //     .Select(group => new {
           //         Metric = group.Key,
           //         Count = group.Count()
           //     })
           //     .Where(x => x.Count > 1);
           // if (groups.Count() != 0)
           // {
           //     var stop = "";
           // }


        }
    }
}
