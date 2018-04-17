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
            return contexto.Database.SqlQuery<int>(query).FirstOrDefault() + 1;
        }

        /// <summary>
        /// Obtiene el valor del ultimo timespan relacionado a una columna
        /// </summary>
        /// <param name="contexto"> instancia para la consulta</param>
        /// <param name="tabIndex"> Máximo valor de tabindex al que se consultan los datos</param>
        /// <param name="fechaNum">Fecha para validar la información</param>
        /// <param name="valor">Información a asignar</param>
        /// <param name="caso">caso para crear el query</param>
        /// <returns>Lista de elementos asociados</returns>
        public static List<AgrupadorFechaNumValor> ConsultarUltimoTimeSpan(SisResultEntities contexto, int tabIndex,
            int fechaNum, int valor = 0, int caso = 0)
        {
            string columna;
            string filtroAnd;
            switch (caso)
            {
                case 1:
                    columna = ConstantesModel.SPANTISEMHIST;
                    filtroAnd = "AND " + ConstantesModel.DIASEM + " = " + valor;
                    break;
                case 2:
                    columna = ConstantesModel.SPANTIMESHIST;
                    filtroAnd = "AND " + ConstantesModel.DIAMES + " = " + valor;
                    break;
                case 3:
                    columna = ConstantesModel.SPANTIANIHIST;
                    filtroAnd = "AND " + ConstantesModel.DIAANIO + " = " + valor;
                    break;
                default:
                    columna = ConstantesModel.SPANTIDIAHIST;
                    filtroAnd = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMOS_SPAN, tabIndex, fechaNum, columna, filtroAnd);
            return contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).ToList();
        }

        /// <summary>
        /// Obtiene el valor del ultimo timespan relacionado a una columna y un groupletter
        /// </summary>
        /// <param name="contexto"> instancia para la consulta</param>
        /// <param name="tabIndex"> tabindex al que se consultan los datos</param>
        /// <param name="fechaNum">Fecha para validar la información</param>
        /// <param name="diaSem"> diasem para la consulta</param>
        /// <param name="diaMes">diames para la consulta</param>
        /// <param name="diaAnio">diaanio para la consulta</param>
        /// <param name="caso">caso para crear el query</param>
        /// <returns>Elemento asociado</returns>
        public static List<AgrupadorFechaNumValor> ConsultarUltimoTimeSpan(SisResultEntities contexto, int fechaNum, string strJoin, int valor = 0, int caso = 0)
        {
            string columna;
            string filtroAnd;
            switch (caso)
            {
                case 1:
                    columna = ConstantesModel.SPANTIGLSEMHIST;
                    filtroAnd = string.Format(" AND " + ConstantesModel.DIASEM + " = {0}", valor);
                    break;
                case 2:
                    columna = ConstantesModel.SPANTIGLMESHIST;
                    filtroAnd = string.Format("AND " + ConstantesModel.DIAMES + " = {0}", valor);
                    break;
                case 3:
                    columna = ConstantesModel.SPANTIGLANIHIST;
                    filtroAnd = string.Format("AND " + ConstantesModel.DIAANIO + " = {0}", valor);
                    break;
                default:
                    columna = ConstantesModel.SPANTIGLDIAHIST;
                    filtroAnd = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMOS_SPAN_TB_LETTER, fechaNum, strJoin, columna, filtroAnd);
            return contexto.Database.SqlQuery<AgrupadorFechaNumValor>(query).ToList();
        }

        /// <summary>
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxTabIndexResultados(SisResultEntities contexto)
        {
            return contexto.Database.SqlQuery<int>(ConstantesConsultaFO.QUERY_MAX_INDEX_RESULTADOS).Single();
        }

        /// <summary>
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados para un groupletter
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="groupLetter">Group para validar el maxtabindex</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxTabIndexLetterResultados(SisResultEntities contexto, string groupLetter)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_INDEX_RESULTADOS_GROUP_LETTER, groupLetter);
            return contexto.Database.SqlQuery<int>(query).Single();
        }

        /// <summary>
        /// Consulta el valor máximo de la fecha para un tabindex menor o igual al recibido
        /// </summary>
        /// <param name="contexto"> instancia para la consulta</param>
        /// <param name="maxTabIndex">valor del maximo tabindex a consultar</param>
        /// <param name="caso">caso para consultar y armar el query</param>
        /// <param name="valor">valor a concatenar</param>
        /// <returns>Valor máximo de la fecha</returns>
        public static int ConsultarMaxFechaTabindex(SisResultEntities contexto, int maxTabIndex, int caso, int valor)
        {
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = "AND " + ConstantesModel.DIASEM + " = " + valor;
                    break;
                case 2:
                    filtro = "AND " + ConstantesModel.DIAMES + " = " + valor;
                    break;
                case 3:
                    filtro = "AND " + ConstantesModel.DIAANIO + " = " + valor;
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
        /// Consulta el siguiente valor para un tabindexletter y un groupletter
        /// </summary>
        /// <param name="contexto">instancia para la consulta</param>
        /// <param name="groupLetter">grupo para la consulta</param>
        /// <param name="tabIndexLetter">tabindex letter</param>
        /// <returns>valor de la siguiente secuencia</returns>
        public static int ConsultarNextTabindexLetterSeq(SisResultEntities contexto, string groupLetter, decimal? tabIndexLetter)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEXLETTER_SEQ, groupLetter, tabIndexLetter);
            return contexto.Database.SqlQuery<int>(query).Single() + 1;
        }

        /// <summary>
        /// Consulta los valores de las fechas relacionadas a los groupletters recibidos en el querybody
        /// </summary>
        /// <param name="contexto">isntancia para la consulta</param>
        /// <param name="caso">caso para concatenar al query</param>
        /// <param name="valor">valor a concatenar en el query</param>
        /// <param name="queryBody">cadena con el contenido de la consulta</param>
        /// <returns>Lista de objetos con la información</returns>
        public static List<AgrupadorMaxFechasTGDTO> ConsultarMaxFechasTabGroup(SisResultEntities contexto, int caso, int valor, string queryBody)
        {
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = "AND " + ConstantesModel.DIASEM + " = {0}";
                    break;
                case 2:
                    filtro = "AND " + ConstantesModel.DIAMES + " = {0}";
                    break;
                case 3:
                    filtro = "AND " + ConstantesModel.DIAANIO + " = {0}";
                    break;
                default:
                    filtro = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_MAXFECHAS_TABANDGROUPLETTER, queryBody, string.Format(filtro, valor));
            return contexto.Database.SqlQuery<AgrupadorMaxFechasTGDTO>(query).ToList();
        }

        /// <summary>
        /// Método que retorna los datos con mayores probabilidades de aparecer de acuerdo a como se han registrado los resultados
        /// </summary>
        /// <param name="maxListIndex">Máximo tabindex para realizar la consulta, se devulven los valores menores o iguales
        /// al tabindex recibido</param>
        /// <param name="fechaFormat">Fecha formateada</param>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="caso">caso para adicionar filtro a la consulta</param>
        /// <param name="valor">Valor para el filtro</param>
        /// <returns></returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarPromResultadosMaxTabindex(int maxListIndex, string fechaFormat, SisResultEntities contexto, int caso = 1, int valor = 0)
        {
            string filtro;
            switch (caso)
            {
                case 2:
                    filtro = string.Format("AND " + ConstantesModel.DIASEM + " = {0} ", valor);
                    break;
                case 3:
                    filtro = string.Format("AND " + ConstantesModel.DIAMES + " = {0} ", valor);
                    break;
                case 4:
                    filtro = string.Format("AND " + ConstantesModel.DIAANIO + " = {0} ", valor);
                    break;
                default:
                    filtro = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_PROM_RESULTS_INTO_TOTALTABINDEX, fechaFormat, maxListIndex, filtro);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que obtiene el promedio para los resultados de un grupo y tabindex
        /// </summary>
        /// <param name="queryBody">Consulta con datos</param>
        /// <param name="fechaFormat">fechaMaxima</param>
        /// <param name="contexto">Instancia para la consulta de datos</param>
        /// <param name="caso">caso para evaluar la columna</param>
        /// <param name="valor">Valor a asignar</param>
        /// <returns>Lista de datos obtenida</returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarPromResultadosGroupTab(string queryBody, string fechaFormat, SisResultEntities contexto, int caso = 1, int valor = 0)
        {
            string filtro;
            switch (caso)
            {
                case 2:
                    filtro = string.Format("AND " + ConstantesModel.DIASEM + " = {0} ", valor);
                    break;
                case 3:
                    filtro = string.Format("AND " + ConstantesModel.DIAMES + " = {0} ", valor);
                    break;
                case 4:
                    filtro = string.Format("AND " + ConstantesModel.DIAANIO + " = {0} ", valor);
                    break;
                default:
                    filtro = "";
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_PROM_RESULTS_INTO_TOTALTABINDEX_GROUPANDTAB, fechaFormat, queryBody, filtro);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Retorna los máximos valores de secuencia para los tabindex
        /// </summary>
        /// <param name="maxListIndex">Máximo valor a revisar</param>
        /// <param name="fechaFormat">Fecha máxima a validar</param>
        /// <param name="contexto">instancia para la consulta</param>
        /// <returns>Lista de datos relacionados</returns>
        public static List<AgrupadorMaxTabIndex> ConsultarMaxSeqTabindex(int maxListIndex, string fechaFormat, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_TABINDEXSEQ_GEN, maxListIndex, fechaFormat);
            DbRawSqlQuery<AgrupadorMaxTabIndex> data = contexto.Database.SqlQuery<AgrupadorMaxTabIndex>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Retorna los máximos valores de secuencia para los tabindex
        /// </summary>
        /// <param name="maxListIndex">Máximo valor a revisar</param>
        /// <param name="fechaFormat">Fecha máxima a validar</param>
        /// <param name="contexto">instancia para la consulta</param>
        /// <returns>Lista de datos relacionados</returns>
        public static List<AgrupadorMaxTabIndex> ConsultarMaxSeqTabindex(int maxListIndex, string fechaFormat, SisResultEntities contexto, string queryBody)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_TABINDEXSEQ_GEN, maxListIndex, fechaFormat);
            DbRawSqlQuery<AgrupadorMaxTabIndex> data = contexto.Database.SqlQuery<AgrupadorMaxTabIndex>(query);
            return data.AsEnumerable().ToList();
        }
    }
}
