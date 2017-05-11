using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class AnDataUnGanador
    {
        public static void AnalizarDatosDia(DateTime fechaRevisar, SisResultEntities contexto, int cantidadQuitar = 10)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;
            List<int> listaTabIndexDifCero, listaTabIndexDifNoCero;
            List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra;
            ConsultarDatosDiaLvl1(contexto, cantidadQuitar, fechaFormat, diaSemana, out listaTabIndexDifCero, out listaTabIndexDifNoCero, out listaResultadosMuestra);

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

        private static void ConsultarDatosDiaLvl1(SisResultEntities contexto, int cantidadQuitar, string fechaFormat, int diaSemana, out List<int> listaTabIndexDifCero, out List<int> listaTabIndexDifNoCero, out List<AgrupadorTotalTabIndexDTO> listaResultadosMuestra)
        {
            int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto);
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados = ConsultasClass.ConsultarTodosDatosDia(fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaConteoIgualdadDia = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat,0);
            List<int> listaIgualdades = (from x in listaConteoIgualdadDia select x.Tabindex).Take(cantidadQuitar).ToList();
            List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();
            listaResultadosMuestra = listaResultados.Take(25).ToList();
        }
    }
}
