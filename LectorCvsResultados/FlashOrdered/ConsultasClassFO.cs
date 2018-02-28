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
                    columna = "spantisemhist";
                    filtroAnd = "AND diasem = " + diaSem;
                    break;
                case 2:
                    columna = "spantimeshist";
                    filtroAnd = "AND diames = " + diaMes;
                    break;
                case 3:
                    columna = "spantianihist";
                    filtroAnd = "AND diaanio = " + diaAnio;
                    break;
                default:
                    columna = "spantidiahist";
                    filtroAnd = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMO_SPAN, tabIndex, fechaNum, columna, filtroAnd);
            AgrupadorFechaNumValor a = contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).FirstOrDefault();
            return a != null ? a : new AgrupadorFechaNumValor();
        }

        public static AgrupadorFechaNumValor ConsultarUltimoTimeSpan(SisResultEntities contexto, int tabIndexLetter,
            int fechaNum, int diaSem, int diaMes, int diaAnio, int caso, string groupletter)
        {
            string columna;
            string filtroAnd;
            switch (caso)
            {
                case 1:
                    columna = "spantiglsemhist";
                    filtroAnd = string.Format("AND groupletter = '{0}' AND diasem = {1}", groupletter, diaSem);
                    break;
                case 2:
                    columna = "spantiglmeshist";
                    filtroAnd = string.Format("AND groupletter = '{0}' AND diames = {1}", groupletter, diaMes);
                    break;
                case 3:
                    columna = "spantiglanihist";
                    filtroAnd = string.Format("AND groupletter = '{0}' AND diaanio = {1}", groupletter, diaAnio);
                    break;
                default:
                    columna = "spantigldiahist";
                    filtroAnd = string.Format(filtroAnd = "AND groupletter = '{0}'", groupletter);
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMO_SPAN_TB_LETTER, tabIndexLetter, fechaNum, columna, filtroAnd);
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
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="groupLetter">Group para validar el maxtabindex</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxIndexResultados(SisResultEntities contexto, string groupLetter)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_INDEX_RESULTADOS_GROUP_LETTER, groupLetter);
            return contexto.Database.SqlQuery<int>(query).Single();
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
            if (data == null)
            {
                return 0;
            }
            return data.FechaNum;
        }

        /// <summary>
        /// Método que consulta ls lista de los máximos tabindex registrados para los elementos con tabindex
        /// menor o igual al maxindex recibido
        /// </summary>
        /// <param name="contexto">instancia para la consulta de los datos</param>
        /// <param name="maxTabIndex">valor relacionada al máximo tabindex a validar</param>
        /// <param name="caso">caso para validar los tabindex, 0 para el diario, 1 para el sem, 2 para mes, 3 para el anio</param>
        /// <param name="valor">valor que se concatena para la consulta de acuerdo al caso</param>
        /// <param name="groupLetter">Group letter validar</param>
        /// <returns>Mínimo valor de la fecha, dentro de los máximos valores asociados a los tabindex</returns>
        public static int ConsultarMaxFechaTabindex(SisResultEntities contexto, int maxTabIndex, int caso, int valor, string groupLetter)
        {
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = "groupletter = '{0}' AND diasem = {1}";
                    break;
                case 2:
                    filtro = "groupletter = '{0}' AND diames = {1}";
                    break;
                case 3:
                    filtro = "groupletter = '{0}' AND diaanio = {1}";
                    break;
                default:
                    filtro = "groupletter = '{0}' ";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHA_TABINDEX_GL, string.Format(filtro, groupLetter, valor), maxTabIndex);
            var data = contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).AsEnumerable().FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            return data.FechaNum;
        }

        /// <summary>
        /// Método que retorna el máximo valor de la secuencia de un tabindex
        /// </summary>
        /// <param name="contexto">Instancia para realizar la consulta</param>
        /// <param name="tabIndex">Tabindex sobre el que se realiza la validación</param>
        /// <returns></returns>
        public static int ConsultarNextTabindexSeq(SisResultEntities contexto, decimal? tabIndex)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEX_SEQ, tabIndex);
            return contexto.Database.SqlQuery<int>(query).Single() + 1;
        }

        /// <summary>
        /// Método que retorna el máximo valor de la secuencia de un tabindex
        /// </summary>
        /// <param name="contexto">Instancia para realizar la consulta</param>
        /// <param name="tabIndex">Tabindex sobre el que se realiza la validación</param>
        /// <returns></returns>
        public static int ConsultarNextTabindexLetterSeq(SisResultEntities contexto, string groupLetter, decimal? tabIndexLetter)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEXLETTER_SEQ, groupLetter , tabIndexLetter);
            return contexto.Database.SqlQuery<int>(query).Single() + 1;
        }

        public static List<AgrupadorMaxFechasTGDTO> ConsultarMaxFechasTabGroup(SisResultEntities contexto, int caso, int valor, string queryBody)
        {
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = "AND diasem = {0}";
                    break;
                case 2:
                    filtro = "AND diames = {0}";
                    break;
                case 3:
                    filtro = "AND diaanio = {0}";
                    break;
                default:
                    filtro = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_MAXFECHAS_TABANDGROUPLETTER, queryBody, string.Format(filtro,valor));
            return contexto.Database.SqlQuery<AgrupadorMaxFechasTGDTO>(query).ToList();
        }
    }
}
