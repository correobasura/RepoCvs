using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace LectorCvsResultados.FlashOrdered
{
    public class ConsultasClassFO
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Consulta el máximo id almacenado
        /// </summary>
        /// <param name="contexto">instancia para las consultas</param>
        /// <returns>valor del máximo id incrementado en 1</returns>
        public static int ConsultarMaxIdActual(SisResultEntities contexto, int tabla)
        {
            string tbl = "";
            switch (tabla)
            {
                case ConstantesGenerales.TBL_FLASH:
                    tbl = ConstantesModel.FLASHORDERED;
                    break;

                case ConstantesGenerales.TBL_PERCENT:
                    tbl = ConstantesModel.ANDATAPERCENTUNG;
                    break;

                case ConstantesGenerales.TBL_SELECTINFO:
                    tbl = ConstantesModel.ANDATASELECTEDINFO;
                    break;
                case ConstantesGenerales.TBL_MINRANK:
                    tbl = ConstantesModel.ANDATAMINRANK;
                    break;
                case ConstantesGenerales.TBL_BININFO:
                    tbl = ConstantesModel.ANDATABININFO;
                    break;
                case ConstantesGenerales.TBL_POSRANK:
                    tbl = ConstantesModel.ANDATAINFOPOSRANK;
                    break;
            }
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_ID_ACTUAL, tbl);
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
        public static List<AgrupadorInfoQuery> ConsultarUltimoTimeSpan(SisResultEntities contexto, int tabIndex,
            int fechaNum, int caso, params int[] valores)
        {
            string columna = GetColumnaTIHist(caso);
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMOS_SPAN, tabIndex, fechaNum, columna, filtro);
            return contexto.Database.SqlQuery<AgrupadorInfoQuery>(query).ToList();
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
        public static List<AgrupadorInfoQuery> ConsultarUltimoTimeSpan(SisResultEntities contexto, int fechaNum, string strJoin, int caso, params int[] valores )
        {
            string columna = GetColumnaGlTIHist(caso);
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_ULTIMOS_SPAN_TB_LETTER, fechaNum, strJoin, columna, filtro);
            return contexto.Database.SqlQuery<AgrupadorInfoQuery>(query).ToList();
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
        public static int ConsultarMaxFechaTabindex(SisResultEntities contexto, int maxTabIndex, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHA_TABINDEX, maxTabIndex, filtro);
            var data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query).AsEnumerable().FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            return data.Fechanum;
        }

        /// <summary>
        /// Método que retorna el máximo valor de la secuencia de un tabindex
        /// </summary>
        /// <param name="contexto">Instancia para realizar la consulta</param>
        /// <param name="tabIndex">Tabindex sobre el que se realiza la validación</param>
        /// <returns></returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarNextTabindexSeq(SisResultEntities contexto, decimal? tabIndex)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEX_SEQ, tabIndex);
            return contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query).ToList();
        }

        /// <summary>
        /// Consulta el siguiente valor para un tabindexletter y un groupletter
        /// </summary>
        /// <param name="contexto">instancia para la consulta</param>
        /// <param name="groupLetter">grupo para la consulta</param>
        /// <param name="tabIndexLetter">tabindex letter</param>
        /// <returns>valor de la siguiente secuencia</returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarNextTabindexLetterSeq(SisResultEntities contexto, string srtJoin)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEXLETTER_SEQ, srtJoin);
            return contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query).ToList();
        }

        /// <summary>
        /// Consulta el siguiente valor para un tabindexletter y un groupletter
        /// </summary>
        /// <param name="contexto">instancia para la consulta</param>
        /// <param name="groupLetter">grupo para la consulta</param>
        /// <param name="tabIndexLetter">tabindex letter</param>
        /// <returns>valor de la siguiente secuencia</returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarNextTabCompSeq(SisResultEntities contexto, List<int> lstCompt)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_NEXT_TABINDEX_COMP_SEQ, string.Join(",", lstCompt));
            return contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query).ToList();
        }

        /// <summary>
        /// Consulta los valores de las fechas relacionadas a los groupletters recibidos en el querybody
        /// </summary>
        /// <param name="contexto">isntancia para la consulta</param>
        /// <param name="caso">caso para concatenar al query</param>
        /// <param name="valor">valor a concatenar en el query</param>
        /// <param name="queryBody">cadena con el contenido de la consulta</param>
        /// <returns>Lista de objetos con la información</returns>
        public static List<AgrupadorMaxFechasTGDTO> ConsultarMaxFechasTabGroup(SisResultEntities contexto, string queryBody, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAXFECHAS_TABANDGROUPLETTER, queryBody, string.Format(filtro, valores));
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
        public static List<AgrupadorTotalTabIndexDTO> ConsultarPromResultadosMaxTabindex(int maxListIndex, string fechaFormat, SisResultEntities contexto, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
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
        public static List<AgrupadorTotalTabIndexDTO> ConsultarPromResultadosGroupTab(string queryBody, string fechaFormat, SisResultEntities contexto, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
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
        /// Consulta los máximos valores para los tabindexletter y groupletter
        /// </summary>
        /// <param name="fechaFormat">Fecha máxima para validar la información</param>
        /// <param name="contexto">Instancia para realizar las consultas</param>
        /// <param name="queryBody">Cadena con filtros de la cosnulta</param>
        /// <returns>Lista de datos obtenidos</returns>
        public static List<AgrupadorMaxTabIndex> ConsultarMaxSeqTabindexGl(string fechaFormat, SisResultEntities contexto, string queryBody)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_TABINDEXSEQ_GL, fechaFormat, queryBody);
            DbRawSqlQuery<AgrupadorMaxTabIndex> data = contexto.Database.SqlQuery<AgrupadorMaxTabIndex>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Consulta los máximos valores de fechanum para un tabindex
        /// </summary>
        /// <param name="contexto">Instancia para realizar las consultas</param>
        /// <param name="maxTabindex">Valor máximo del tabindex</param>
        /// <param name="fechaFormat">Valor para la fecha</param>
        /// <param name="caso">caso para adicionar el filtro</param>
        /// <param name="valor">valor a adicionar al filtro</param>
        /// <returns></returns>
        public static List<AgrupadorInfoQuery> ConsultarMaxFechaAndSpanTabindex(SisResultEntities contexto, int maxTabindex, string fechaFormat, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string columna = GetColumnaTIHist(caso);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEX, maxTabindex, fechaFormat, filtro, columna);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarMaxFechaAndValueDiffTabindex(SisResultEntities contexto, int maxTabindex, string fechaFormat, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string columna = GetColumnaTIHist(caso);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEXVALUEDIFF, maxTabindex, fechaFormat, filtro, columna);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarMaxFechaAndValueDiffTabindexTotalesDia(SisResultEntities contexto, int maxTabindex, string fechaFormat)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEXVALUEDIFFTOTALDIA, maxTabindex, fechaFormat);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Metodo que consulta los máximos valores para los tabindex groupletter
        /// </summary>
        /// <param name="contexto">Instancia para las consultas</param>
        /// <param name="fechaFormat">Fecha máxima para las consultas</param>
        /// <param name="queryBody">Filtros adicionales de las consultas</param>
        /// <param name="caso">Caso para adicionar el filtro</param>
        /// <param name="valor">Valor para adicionar al filtro</param>
        /// <returns>Lista de elementos encontrados</returns>
        public static List<AgrupadorInfoQuery> ConsultarMaxFechaTabindexAndSpanGl(SisResultEntities contexto, string fechaFormat, string queryBody, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string columna = GetColumnaGlTIHist(caso);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEX_GL, fechaFormat, queryBody, filtro, columna);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarMaxFechaAndValueDiffTabGl(SisResultEntities contexto, string fechaFormat, string queryBody, int caso, params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string columna = GetColumnaTIHist(caso);
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEX_GL_VALUEDIFF, fechaFormat, queryBody, filtro, columna);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarMaxFechaAndValueDiffTabGlTotalesDia(SisResultEntities contexto, string querybody)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_MAX_FECHANUMTABINDEX_GL_VALUEDIFF_TOTALGL, querybody);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que realiza el rankeo de los spans para cada tabindex
        /// </summary>
        /// <param name="contexto">Instancia para las consultas</param>
        /// <param name="queryBody">info de consulta</param>
        /// <param name="caso">Caso para evaluar</param>
        /// <param name="valor">Valor a asignar al filtro</param>
        /// <returns>Listado de elementos</returns>
        public static List<AgrupadorInfoQuery> ConsultarRanksTabindex(SisResultEntities contexto, string queryBody, int caso, params int[] valores)
        {
            string columna = GetColumnaTIAct(caso);
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_RANK_SPANS_BY_TABINDEX, columna, queryBody, filtro);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que realiza el rankeo de los spans para cada tabindex
        /// </summary>
        /// <param name="contexto">Instancia para las consultas</param>
        /// <param name="queryBody">info de consulta</param>
        /// <param name="caso">Caso para evaluar</param>
        /// <param name="valor">Valor a asignar al filtro</param>
        /// <returns>Listado de elementos</returns>
        public static List<AgrupadorInfoQuery> ConsultarRanksGlTabindex(SisResultEntities contexto, string queryBody, int caso, params int[] valores)
        {
            string columna = GetColumnaGlTIAct(caso);
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_RANK_SPANS_BY_GL_TABINDEX, columna, queryBody, filtro);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static string GetFiltro(int caso, params int[] valores)
        {
            string filtro;
            switch (caso)
            {
                case ConstantesGenerales.CASO_DIASEM:
                    filtro = string.Format("AND " + ConstantesModel.DIASEM + " = {0} ", valores[0]);
                    break;

                case ConstantesGenerales.CASO_DIAMES:
                    filtro = string.Format("AND " + ConstantesModel.DIAMES + " = {0} ", valores[0]);
                    break;

                case ConstantesGenerales.CASO_DIAANIO:
                    filtro = string.Format("AND " + ConstantesModel.DIAANIO + " = {0} ", valores[0]);
                    break;
                case ConstantesGenerales.CASO_MESNUM:
                    filtro = string.Format("AND " + ConstantesModel.MESNUM + " = {0} ", valores[0]);
                    break;
                case ConstantesGenerales.CASO_DIASEM_MESNUM:
                    filtro = string.Format("AND " + ConstantesModel.DIASEM + " = {0} AND " + ConstantesModel.MESNUM + " = {1} ", valores[0], valores[1]);
                    break;
                case ConstantesGenerales.CASO_DIAMES_MESNUM:
                    filtro = string.Format("AND " + ConstantesModel.DIAMES + " = {0} AND " + ConstantesModel.MESNUM + " = {1} ", valores[0], valores[1]);
                    break;
                default:
                    filtro = "";
                    break;
            }
            return filtro;
        }

        public static string GetColumnaTIHist(int caso)
        {
            string columna;
            switch (caso)
            {
                case ConstantesGenerales.CASO_DIASEM:
                    columna = ConstantesModel.SPANTISEMHIST;
                    break;

                case ConstantesGenerales.CASO_DIAMES:
                    columna = ConstantesModel.SPANTIMESHIST;
                    break;

                case ConstantesGenerales.CASO_DIAANIO:
                    columna = ConstantesModel.SPANTIANIHIST;
                    break;

                default:
                    columna = ConstantesModel.SPANTIDIAHIST;
                    break;
            }
            return columna;
        }

        public static string GetColumnaTIAct(int caso)
        {
            string columna;
            switch (caso)
            {
                case ConstantesGenerales.CASO_DIASEM:
                    columna = ConstantesModel.SPANTISEMACT;
                    break;

                case ConstantesGenerales.CASO_DIAMES:
                    columna = ConstantesModel.SPANTIMESACT;
                    break;

                case ConstantesGenerales.CASO_DIAANIO:
                    columna = ConstantesModel.SPANTIANIACT;
                    break;

                default:
                    columna = ConstantesModel.SPANTIDIAACT;
                    break;
            }
            return columna;
        }

        public static string GetColumnaGlTIHist(int caso)
        {
            string columna;
            switch (caso)
            {
                case ConstantesGenerales.CASO_DIASEM:
                    columna = ConstantesModel.SPANTIGLSEMHIST;
                    break;

                case ConstantesGenerales.CASO_DIAMES:
                    columna = ConstantesModel.SPANTIGLMESHIST;
                    break;

                case ConstantesGenerales.CASO_DIAANIO:
                    columna = ConstantesModel.SPANTIGLANIHIST;
                    break;

                default:
                    columna = ConstantesModel.SPANTIGLDIAHIST;
                    break;
            }
            return columna;
        }

        public static string GetColumnaGlTIAct(int caso)
        {
            string columna;
            switch (caso)
            {
                case ConstantesGenerales.CASO_DIASEM:
                    columna = ConstantesModel.SPANTIGLSEMACT;
                    break;

                case ConstantesGenerales.CASO_DIAMES:
                    columna = ConstantesModel.SPANTIGLMESACT;
                    break;

                case ConstantesGenerales.CASO_DIAANIO:
                    columna = ConstantesModel.SPANTIGLANIACT;
                    break;

                default:
                    columna = ConstantesModel.SPANTIGLDIAACT;
                    break;
            }
            return columna;
        }

        public static string GetColumnaRank(int caso)
        {
            string columna;
            switch (caso)
            {
                case ConstantesGenerales.KEY_RANK:
                    columna = ConstantesModel.KEYRANK;
                    break;

                default:
                    columna = ConstantesModel.KEYRANKGRAL;
                    break;
            }
            return columna;
        }

        /// <summary>
        /// Método que realiza el rankeo de los spans para cada tabindex
        /// </summary>
        /// <param name="contexto">Instancia para las consultas</param>
        /// <param name="queryBody">info de consulta</param>
        /// <param name="caso">Caso para evaluar</param>
        /// <param name="valor">Valor a asignar al filtro</param>
        /// <returns>Listado de elementos</returns>
        public static List<AgrupadorInfoQuery> ConsultarPercent(SisResultEntities contexto, string fechanum, int tipoOrden, int caso, string fechaMin = "", params int[] valores)
        {
            string filtro = GetFiltro(caso, valores);
            string query = string.Format(ConstantesConsultaFO.QUERY_RANK_PERCENT, fechanum, filtro, tipoOrden, fechaMin);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarProbSelectedInfo(SisResultEntities contexto, string spanCol, string rankCol, string fechaMax, string fechaMin="")
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_SELINF, fechaMin, spanCol, rankCol, fechaMax);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        //internal static List<AgrupadorTotalTabIndexDTO> ConsultarAgrupadorDiaMesDia(SisResultEntities contexto, string fechaformat, int dayofweek, int day, int maxTabindex)
        //{
        //    string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_DIADIAMES, dayofweek, day, fechaformat, maxTabindex);
        //    DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
        //    return data.AsEnumerable().ToList();
        //}

        //internal static List<AgrupadorTotalTabIndexDTO> ConsultarAgrupadorDiaMesDiaGl(SisResultEntities contexto, string fechaformat, int dayofweek, int day, string queryBody)
        //{
        //    string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_DIADIAMES_GL, dayofweek, day, fechaformat, queryBody);
        //    DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
        //    return data.AsEnumerable().ToList();
        //}

        internal static List<AgrupadorTotalTabIndexDTO> ConsultarAgrupadorDiaMesDia(SisResultEntities contexto, string fechaformat, int dayofweek, int day, int maxTabindex)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_DIADIAMES, dayofweek, fechaformat, maxTabindex);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        internal static List<AgrupadorTotalTabIndexDTO> ConsultarAgrupadorDiaMesDiaGl(SisResultEntities contexto, string fechaformat, int dayofweek, int day, string queryBody)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_DIADIAMES_GL, dayofweek, fechaformat, queryBody);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery> ConsultarProbRankInfo(SisResultEntities contexto, int casoCol, int casoRange, string fechaMax, int valor, string fechaFormatMin)
        {
            string col = GetColumnaRank(casoCol);
            string filtro = GetFiltro(casoRange, valor)+ (fechaFormatMin != "" ? "AND fechanum >= " + fechaFormatMin+" ":"");
            string query = string.Format(ConstantesConsultaFO.QUERY_COUNT_PROB_RANK, fechaMax, filtro, col);
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        internal static void ConsultarValoresPivotTab(SisResultEntities contexto, int maxTabindex)
        {
            string query = string.Format(ConstantesConsultaFO.QUERY_DIAS_BY_TABINDEX, maxTabindex);
            DbRawSqlQuery<int> data = contexto.Database.SqlQuery<int>(query);
            List<int> lstDias = data.AsEnumerable().ToList();
            query = string.Format(ConstantesConsultaFO.QUERY_PIVOT_COUNT_RESULTS, maxTabindex, string.Join(",", lstDias));
            _log.Info(query);

            using (System.Data.IDbCommand command = contexto.Database.Connection.CreateCommand())
            {
                try
                {
                    contexto.Database.Connection.Open();
                    command.CommandText = query;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;

                    using (System.Data.IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<int> lstFlags = new List<int>();
                            int tabindex = reader.GetInt32(0);
                            for (int i = 1; i < reader.FieldCount; i++)
                            {
                                lstFlags.Add(reader.GetInt32(i));
                            }
                        }
                    }
                }
                finally
                {
                    contexto.Database.Connection.Close();
                    command.Parameters.Clear();
                }
            }
            
        }

        internal static List<AgrupadorInfoQuery> ConsultarValoresProbGroupTabTotal(SisResultEntities contexto, string query)
        {
            DbRawSqlQuery<AgrupadorInfoQuery> data = contexto.Database.SqlQuery<AgrupadorInfoQuery>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<InfoAnalisisDTO4> getInfoRanks(SisResultEntities contexto, string query)
        {
            DbRawSqlQuery<InfoAnalisisDTO4> data = contexto.Database.SqlQuery<InfoAnalisisDTO4>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorInfoQuery2> ConsultarValoresReferencia(SisResultEntities contexto, string query)
        {
            DbRawSqlQuery<AgrupadorInfoQuery2> data = contexto.Database.SqlQuery<AgrupadorInfoQuery2>(query);
            return data.AsEnumerable().ToList();
        }
    }
}