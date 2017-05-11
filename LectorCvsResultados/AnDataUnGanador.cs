using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class AnDataUnGanador
    {
        public static void AnalizarDatosDia(DateTime fechaRevisar, SisResultEntities contexto, int minimoRegistrosExistentes)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;
            List<int> listaTabIndexDifCero, listaTabIndexDifNoCero;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra;
            ConsultarDatosLvl1(contexto, minimoRegistrosExistentes, fechaFormat, diaSemana, out listaTabIndexDifCero, out listaTabIndexDifNoCero, out listaResultadosMuestra);

            for (int j = 0; j < listaResultadosMuestra.Count; j++)
            {
                var elemento = listaResultadosMuestra.ElementAt(j);
                ANALISTINDEXUNG a = new ANALISTINDEXUNG();
                a.ID = ConsultasClass.ObtenerValorSecuencia(a, contexto);
                a.LINEINDEX = j + 1;
                a.FECHA = fechaRevisar;
                a.FECHANUM = fechaNum;
                a.DIASEMNUM = diaSemana;
                a.DIAMESNUM = diaMes;
                if (listaTabIndexDifCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.RESULT = -1;
                }
                else if (listaTabIndexDifNoCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.RESULT = 1;
                }
                else
                {
                    a.RESULT = 0;
                }
                contexto.ANALISTINDEXUNG.Add(a);
            }
            contexto.SaveChanges();
        }

        private static void ConsultarDatosLvl1(SisResultEntities contexto, int minimoRegistrosExistentes, string fechaFormat, int diaSemana, out List<int> listaTabIndexDifCero, out List<int> listaTabIndexDifNoCero, out List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra)
        {
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto, minimoRegistrosExistentes);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            //List<AgrupadorTotalTabIndexDTO> listaResultadosIgualdad = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            //List<int> listaIgualdades = (from x in listaResultadosIgualdad select x.Tabindex).Take(5).ToList();
            //List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();
            listaResultadosMuestra = listaResultados.Take(30).ToList();
        }

        public static void AnalizarDatosPorDiaSeleccionLvl2(DateTime fechaRevisar, SisResultEntities contexto, int minimoRegistrosExistentes)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;
            List<int> listaTabIndexDifCero, listaTabIndexDifNoCero;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra;
            ConsultarDatosLvl1(contexto, minimoRegistrosExistentes, fechaFormat, diaSemana, out listaTabIndexDifCero, out listaTabIndexDifNoCero, out listaResultadosMuestra);

            List<AgrupadorTotalTabIndexDTO> listaResultadosLvl1 = ConsultasClass.ConsultarDatosParaDiaSeleccionLvl1(contexto, fechaFormat).Take(25).ToList();

            for (int i = 0; i < listaResultadosLvl1.Count; i++)
            {
                var elemento = listaResultadosMuestra.ElementAt(listaResultadosLvl1.ElementAt(i).Lineindex - 1);
                ANALISTINDEXUNGLV2 a = new ANALISTINDEXUNGLV2();
                a.ID = ConsultasClass.ObtenerValorSecuencia(a, contexto);
                a.LINEINDEX = i + 1;
                a.FECHA = fechaRevisar;
                a.FECHANUM = fechaNum;
                a.DIASEMNUM = diaSemana;
                a.DIAMESNUM = diaMes;
                if (listaTabIndexDifCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.RESULT = -1;
                }
                else if (listaTabIndexDifNoCero.IndexOf(elemento.Tabindex) != -1)
                {
                    a.RESULT = 1;
                }
                else
                {
                    a.RESULT = 0;
                }
                contexto.ANALISTINDEXUNGLV2.Add(a);
            }
            contexto.SaveChanges();
        }
    }
}
