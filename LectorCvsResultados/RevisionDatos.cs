using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class RevisionDatos
    {
        public static AnalisisDatosDTO AnalizarDatosPorDiaSeleccion(DateTime fecha, SisResultEntities contexto, int minimoRegistrosExistentes, int cantidadQuitar = 0)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int diaSemana = fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto, diaSemana, fecha.Day);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaConteoIgualdadDia = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            List<int> listaIgualdades = (from x in listaConteoIgualdadDia select x.Tabindex).Take(cantidadQuitar).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            List<int> listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            List<int> listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();

            AnalisisDatosDTO a = new AnalisisDatosDTO();
            a.TotalDatos = listaResultados.Count();
            a.Fecha = fechaFormat;
            int cantidadTomar = 25;
            //List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultados.Take(cantidadTomar).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultadosTemp.Take(cantidadTomar).ToList();
            //Dictionary<int, AgrupadorTimeSpanDTO> dict = RevisarTimeSpanDatos(fecha, contexto, maxIndex);
            for (int i = 0; i < listaResultadosMuestra.Count(); i++)
            {
                if(listaTabIndexDifCero.IndexOf(listaResultadosMuestra.ElementAt(i).Tabindex) != -1)
                {
                    a.ResultadosNegativos++;
                    //var ultimo = dict[listaResultadosMuestra.ElementAt(i).Tabindex].UltimoEnRachas;
                    //var itemDic = dict[listaResultadosMuestra.ElementAt(i).Tabindex].DictRachasAcumuladas[ultimo];
                    //a.UltimoParecido +=  + ",\t";
                }
                else if(listaTabIndexDifNoCero.IndexOf(listaResultadosMuestra.ElementAt(i).Tabindex) != -1)
                {
                    a.ResultadosPositivos++;
                }
            }
            a.PromedioPositivo = (double)a.ResultadosPositivos / cantidadTomar;
            a.PromedioNegativo = (double)a.ResultadosNegativos / cantidadTomar;

            return a;
        }

        public static AnalisisDatosDTO AnalizarDatosPorDiaSeleccionBetween(DateTime fecha, SisResultEntities contexto, int cantidadIgualdfades, int percentMenor, int percentMayor)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int diaSemana = fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccionBetween(maxIndex, fechaFormat, contexto, percentMenor, percentMayor);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            //List<AgrupadorTotalTabIndexDTO> listaResultadosIgualdad = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            //List<int> listaIgualdades = (from x in listaResultadosIgualdad select x.Tabindex).Take(cantidadIgualdfades).ToList();
            //List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            List<int> listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            List<int> listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();


            AnalisisDatosDTO a = new AnalisisDatosDTO();
            a.TotalDatos = listaResultados.Count();
            a.Fecha = fechaFormat;
            int cantidadTomar = 15;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultados.Take(cantidadTomar).ToList();
            //List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultadosTemp.Take(cantidadTomar).ToList();
            Dictionary<int, AgrupadorTimeSpanDTO> dict = RevisarTimeSpanDatos(fecha, contexto, maxIndex);
            for (int i = 0; i < listaResultadosMuestra.Count(); i++)
            {
                if (listaTabIndexDifCero.IndexOf(listaResultadosMuestra.ElementAt(i).Tabindex) != -1)
                {
                    a.ResultadosNegativos++;
                    a.UltimoParecido += dict[listaResultadosMuestra.ElementAt(i).Tabindex].UltimoEnRachas+"\t";
                }
                else if (listaTabIndexDifNoCero.IndexOf(listaResultadosMuestra.ElementAt(i).Tabindex) != -1)
                {
                    a.ResultadosPositivos++;
                }
            }
            a.PromedioPositivo = (double)a.ResultadosPositivos / cantidadTomar;
            a.PromedioNegativo = (double)a.ResultadosNegativos / cantidadTomar;
            return a;
        }

        public static AnalisisDatosDTO AnalizarDatosPorDiaSeleccionLvl2(DateTime fecha, SisResultEntities contexto, int minimoRegistrosExistentes, int cantidadIgualdfades)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int diaSemana = fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto, minimoRegistrosExistentes);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultadosIgualdad = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            List<int> listaIgualdades = (from x in listaResultadosIgualdad select x.Tabindex).Take(cantidadIgualdfades).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            List<int> listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            List<int> listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();


            AnalisisDatosDTO a = new AnalisisDatosDTO();
            a.TotalDatos = listaResultados.Count();
            a.MinimoApariciones = minimoRegistrosExistentes;
            a.Fecha = fechaFormat;
            int cantidadTomar = 30;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultadosTemp.Take(cantidadTomar).ToList();

            cantidadTomar = 25;
            List<AgrupadorTotalTabIndexDTO> listaResultadosLvl1 = ConsultasClass.ConsultarDatosParaDiaSeleccionLvl1(contexto, fechaFormat).Take(25).ToList();
            for (int i = 0; i < listaResultadosLvl1.Count; i++)
            {
                var elemento = listaResultadosMuestra.ElementAt(listaResultadosLvl1.ElementAt(i).Lineindex-1);
                if (listaTabIndexDifCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.ResultadosNegativos++;
                }
                else if (listaTabIndexDifNoCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.ResultadosPositivos++;
                }
            }
            a.PromedioPositivo = (double)a.ResultadosPositivos / cantidadTomar;
            a.PromedioNegativo = (double)a.ResultadosNegativos / cantidadTomar;
            return a;
        }

        public static AnalisisDatosDTO AnalizarDatosPorDiaSeleccionLvl3(DateTime fecha, SisResultEntities contexto, int minimoRegistrosExistentes, int cantidadIgualdfades)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int diaSemana = fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto, minimoRegistrosExistentes);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultadosIgualdad = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            List<int> listaIgualdades = (from x in listaResultadosIgualdad select x.Tabindex).Take(cantidadIgualdfades).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            List<int> listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            List<int> listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();


            AnalisisDatosDTO a = new AnalisisDatosDTO();
            a.TotalDatos = listaResultados.Count();
            a.MinimoApariciones = minimoRegistrosExistentes;
            a.Fecha = fechaFormat;
            int cantidadTomar = 30;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra = listaResultadosTemp.Take(cantidadTomar).ToList();

            cantidadTomar = 25;
            List<AgrupadorTotalTabIndexDTO> listaResultadosLvl2 = ConsultasClass.ConsultarDatosParaDiaSeleccionLvl1(contexto, fechaFormat).Take(25).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosLvl3 = ConsultasClass.ConsultarDatosParaDiaSeleccionLvl2(contexto, fechaFormat).Take(20).ToList();
            for (int i = 0; i < listaResultadosLvl3.Count; i++)
            {
                var elementoLvl2 = listaResultadosLvl2.ElementAt(listaResultadosLvl3.ElementAt(i).Lineindex - 1);
                var elemento = listaResultadosMuestra.ElementAt(elementoLvl2.Lineindex - 1);
                if (listaTabIndexDifCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.ResultadosNegativos++;
                }
                else if (listaTabIndexDifNoCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.ResultadosPositivos++;
                }
            }
            a.PromedioPositivo = (double)a.ResultadosPositivos / cantidadTomar;
            a.PromedioNegativo = (double)a.ResultadosNegativos / cantidadTomar;
            return a;
        }

        public static void RevisarTimeSpanDatos(DateTime fecha, SisResultEntities contexto, Dictionary<int, AgrupadorTimeSpanDTO> dict, int maxIndexAnalizar)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            foreach (var item in listaTodosResultados)
            {
                if (!dict.ContainsKey(item.Tabindex))
                {
                    dict.Add(item.Tabindex, new AgrupadorTimeSpanDTO());
                    dict[item.Tabindex].ValoresAparicion = new List<int>();
                }
                if (item.Diferencia == 0)
                {
                    dict[item.Tabindex].ValoresAparicion.Add(0);
                }
                else
                {
                    dict[item.Tabindex].ValoresAparicion.Add(1);
                }
            }            
        }

        public static Dictionary<int, AgrupadorTimeSpanDTO> RevisarTimeSpanDatos(DateTime FechaMax, SisResultEntities contexto, int maxIndex)
        {
            Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            var laMin = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture); ;
            for (; laMin < FechaMax;)
            {
                RevisionDatos.RevisarTimeSpanDatos(laMin, contexto, dict, maxIndex);
                laMin = laMin.AddDays(1);
            }

            foreach (var item in dict)
            {
                item.Value.ValoresAparicionAcumulada = new List<int>();
                var counter = 0;
                foreach (var itemList in item.Value.ValoresAparicion)
                {
                    if (itemList == 1)
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
            return dict;
        }
    }
}
