using LectorCvsResultados.FlashOrdered;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class ConsultasClassFO
    {

        /// <summary>
        /// Consulta el máximo id almacenado
        /// </summary>
        /// <param name="contexto">instancia para las consultas</param>
        /// <returns>valor del máximo id incrementado en 1</returns>
        public static int ConsultarMaxIdActual(SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_ID_ACTUAL);
            return contexto.Database.SqlQuery<int>(query).Single() + 1;
        }

        /// <summary>
        /// Método que retorna el máximo valor de la secuencia de un tabindex
        /// </summary>
        /// <param name="contexto">Instancia para realizar la consulta</param>
        /// <param name="tabIndex">Tabindex sobre el que se realiza la validación</param>
        /// <returns></returns>
        public static AgrupadorFechaNumValor ConsultarUltimoTimeSpan(SisResultEntities contexto, int tabIndex, 
            int fechaNum, int diaSem, int diaMes, int diaAnio, int caso = 0)
        {
            string columna;
            string filtroAnd;
            switch (caso)
            {
                case 1:
                    columna = "spansemanahistorico";
                    filtroAnd = "AND diasem = " + diaSem;
                    break;
                case 2:
                    columna = "spanmeshistorico";
                    filtroAnd = "AND diames = " + diaMes;
                    break;
                case 3:
                    columna = "spananiohistorico";
                    filtroAnd = "AND diaanio = " + diaAnio;
                    break;
                default:
                    columna = "spandiariohistorico";
                    filtroAnd = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMO_SPAN, tabIndex, fechaNum, columna, filtroAnd);
            AgrupadorFechaNumValor a = contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).FirstOrDefault();
            return a != null ? a : new AgrupadorFechaNumValor();
        }

        /// <summary>
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxIndexResultados(SisResultEntities contexto)
        {
            return contexto.Database.SqlQuery<int>(ConstantesConsultaFO.QUERY_MAX_INDEX_RESULTADOS).Single();
        }

        /// <summary>
        /// Método que consulta ls lista de los máximos tabindex registrados para los elementos con tabindex
        /// menor o igual al maxindex recibido
        /// </summary>
        /// <param name="contexto">instancia para la consulta de los datos</param>
        /// <param name="maxTabIndex">valor relacionada al máximo tabindex a validar</param>
        /// <param name="caso">caso para validar los tabindex, 0 para el diario, 1 para el sem, 2 para mes, 3 para el anio</param>
        /// <param name="valor">valor que se concatena para la consulta de acuerdo al caso</param>
        /// <returns>Mínimo valor de la fecha, dentro de los máximos valores asociados a los tabindex</returns>
        public static int ConsultarMaxFechaTabindex(SisResultEntities contexto, int maxTabIndex, int caso, int valor)
        {
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = "AND diasem = " + valor;
                    break;
                case 2:
                    filtro = "AND diames = " + valor;
                    break;
                case 3:
                    filtro = "AND diaanio = " + valor;
                    break;
                default:
                    filtro = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHA_TABINDEX, maxTabIndex, filtro);
            var data = contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).AsEnumerable().FirstOrDefault();
            if(data == null)
            {
                return 0;
            }
            return data.FechaNum;
        }
    }
}
