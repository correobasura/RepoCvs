namespace LectorCvsResultados.FlashOrdered
{
    public class ConstantesConsultaFO
    {
        public const string QUERY_MAX_ID_ACTUAL =
            "SELECT COALESCE(MAX(" + ConstantesModel.ID + "), 0)"
            + "FROM {0}";

        public const string QUERY_ULTIMOS_SPAN =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} {3}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ") "
            + "SELECT f.{2} AS spantiempo, f." + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + ", f." + ConstantesModel.FECHANUM + " AS fechanum "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON r." + ConstantesModel.TABINDEX + "  = f." + ConstantesModel.TABINDEX
            + " AND r.fechanum = f." + ConstantesModel.FECHANUM + "";

        public const string QUERY_ULTIMOS_SPAN_TB_LETTER =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND {1} {3} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + "," + ConstantesModel.TABINDEXLETTER + ") "
            + "SELECT f.{2} AS spantiempo, f." + ConstantesModel.FECHANUM + " AS fechanum, f." + ConstantesModel.GROUPLETTER + " AS GroupLetter, f." + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON r." + ConstantesModel.GROUPLETTER + " = f." + ConstantesModel.GROUPLETTER + " "
            + "AND r." + ConstantesModel.TABINDEXLETTER + " = f." + ConstantesModel.TABINDEXLETTER + " "
            + "AND r." + ConstantesModel.FECHANUM + " = f." + ConstantesModel.FECHANUM + "";

        public const string QUERY_MAX_INDEX_RESULTADOS =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEX + "), 0)"
            + "FROM " + ConstantesModel.FLASHORDERED + " ";

        public const string QUERY_MAX_INDEX_RESULTADOS_GROUP_LETTER =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXLETTER + "), 0)"
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.GROUPLETTER + " = '{0}'";

        public const string QUERY_MAX_FECHA_TABINDEX =
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEX + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "{1} "
            + "GROUP BY " + ConstantesModel.TABINDEX + " ORDER BY 1";

        public const string QUERY_MAX_FECHA_TABINDEX_GL =
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEXLETTER + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} "
            + "AND " + ConstantesModel.TABINDEXLETTER + " <= {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + " ORDER BY 1";

        public const string QUERY_NEXT_TABINDEX_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXSEQ + "),0) "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " = {0}";

        public const string QUERY_NEXT_TABINDEXLETTER_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXLETTERSEQ + "),0) "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.GROUPLETTER + " = '{0}' "
            + "AND " + ConstantesModel.TABINDEXLETTER + " = {1}";

        public const string QUERY_MAXFECHAS_TABANDGROUPLETTER =
            "WITH registros AS ( "
            + "SELECT max(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEXLETTER + " AS Tabindexletter, " + ConstantesModel.GROUPLETTER + " AS Groupletter "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " ) "
            + "SELECT f.id AS id, r.* "
            + "FROM " + ConstantesModel.FLASHORDERED + " f, registros r "
            + "WHERE r.fechanum  = f.fechanum "
            + "AND r.tabindexletter = f.tabindexletter "
            + "AND r.groupletter = f.groupletter ";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX =
            "SELECT a.cuenta/b.cuenta AS total, a.tabindex, b.cuenta AS Apariciones, RANK () OVER(ORDER BY a.cuenta/b.cuenta DESC) AS Rank "
            + "FROM (SELECT COUNT(1) AS cuenta, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.DIFERENCIAG + " != 0 "
            + "AND " + ConstantesModel.FECHANUM + " < {0} "
            + "AND " + ConstantesModel.TABINDEX + " < = {1} {2} "
            + "GROUP BY " + ConstantesModel.TABINDEX + " "
            + ") a, "
            + "(SELECT COUNT(1) AS cuenta, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND " + ConstantesModel.TABINDEX + " < = {1} {2} "
            + "GROUP BY " + ConstantesModel.TABINDEX + ") b "
            + "WHERE a.tabindex = b.tabindex";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX_GROUPANDTAB =
            "WITH registros AS ( "
            + "SELECT " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.DIFERENCIAG + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND {1} {2}"
            + ") "
            + "SELECT a.cuenta/b.cuenta AS total, a.groupletter AS groupletter, a.tabindexletter AS tabindex, b.cuenta AS Apariciones, RANK () OVER(ORDER BY a.cuenta/b.cuenta DESC) AS Rank "
            + "FROM ( "
            + "SELECT count(1) AS cuenta, " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM registros "
            + "WHERE diferenciag != 0 "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ")a, "
            + "(SELECT count(1) AS cuenta, " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM registros "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ")b "
            + "WHERE a.groupletter = b.groupletter "
            + "AND a.tabindexletter = b.tabindexletter";

        public const string QUERY_MAX_TABINDEXSEQ_GEN =
            "SELECT MAX(" + ConstantesModel.TABINDEXSEQ + ") AS total, tabindex AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " <= {1} "
            + "GROUP BY tabindex";

        public const string QUERY_MAX_TABINDEXSEQ_GL =
            "SELECT MAX(" + ConstantesModel.TABINDEXLETTERSEQ + ") AS total, groupletter AS GroupLetter, tabindexletter AS Tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " <= {0} AND {1} "
            + "GROUP BY groupletter, tabindexletter";

        public const string QUERY_MAX_FECHANUMTABINDEX =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEX + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} {2}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ") "
            + "SELECT f.{3} AS spantiempo,f." + ConstantesModel.FECHANUM + " AS fechanum, f." + ConstantesModel.TABINDEX + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON f." + ConstantesModel.FECHANUM + "  = r." + ConstantesModel.FECHANUM
                + " AND f." + ConstantesModel.TABINDEX + " = r." + ConstantesModel.TABINDEX + " ";

        public const string QUERY_MAX_FECHANUMTABINDEX_GL =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter, " + ConstantesModel.GROUPLETTER + " AS GroupLetter "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND ({1}) {2}"
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + ") "
            + "SELECT f.{3} AS spantiempo,f." + ConstantesModel.FECHANUM + " AS fechanum, f." + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter, f. " + ConstantesModel.GROUPLETTER + " AS GroupLetter "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON f." + ConstantesModel.FECHANUM + "  = r." + ConstantesModel.FECHANUM
                + " AND f." + ConstantesModel.TABINDEXLETTER + " = r." + ConstantesModel.TABINDEXLETTER + " "
                + " AND f." + ConstantesModel.GROUPLETTER + " = r." + ConstantesModel.GROUPLETTER + " ";

        public const string QUERY_RANK_SPANS_BY_TABINDEX =
            "SELECT COUNT(1) AS total, {0} AS spanTiempo, " + ConstantesModel.TABINDEX + " AS tabindex, "
            + "RANK () OVER (PARTITION BY " + ConstantesModel.TABINDEX + " ORDER BY COUNT(1) DESC) AS rank "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE ({1}) "
            + "AND {0} IS NOT NULL {2} "
            + "GROUP BY {0}, " + ConstantesModel.TABINDEX;

        public const string QUERY_RANK_SPANS_BY_GL_TABINDEX =
            "SELECT COUNT(1) AS total, {0} AS spanTiempo, " + ConstantesModel.TABINDEXLETTER + " AS tabindexletter, " + ConstantesModel.GROUPLETTER + " AS groupletter, "
            + "RANK () OVER (PARTITION BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " ORDER BY COUNT(1) DESC) AS rank "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE ({1}) "
            + "AND {0} IS NOT NULL {2} "
            + "GROUP BY {0}, " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + "";

        public const string QUERY_RANK_PERCENT =
            "SELECT b.total/a.total AS Prob, b.percent AS Percent, RANK () OVER (order by b.total/a.total) AS rank, b.total AS TotalGen, a.total AS TotalDif "
            + "FROM (SELECT COUNT(1) AS total, percent "
            + "FROM andatapercentung "
            + "WHERE {3} fechanum < {0} {1} "
            + "AND tipoorden = {2} "
            + "GROUP BY percent "
            + ") a, "
            + "(SELECT COUNT(1) AS total, percent "
            + "FROM andatapercentung "
            + "WHERE {3} fechanum < {0} {1} "
            + "AND tipoorden = {2} "
            + "AND diferenciag != 0 "
            + "GROUP BY percent "
            + ") b "
            + "WHERE a.percent = b.percent ";

        public const string QUERY_COUNT_PROB_SELINF =
            "SELECT a.total/b.total AS Prob, a.{1} AS Spantiempo, a.{2} AS Rank, b.total AS Total "
            + "FROM ( "
            + "SELECT COUNT(1) AS total, {1}, {2}  "
            + "FROM andataselectedinfo  "
            + "WHERE diferenciag != 0 "
            + "AND {0} fechanum < {3} "
            + "GROUP BY {1}, {2}) a, ( "
            + "SELECT COUNT(1) AS total, {1}, {2}  "
            + "FROM andataselectedinfo  "
            + "WHERE {0} fechanum < {3} "
            + "GROUP BY {1}, {2}) b "
            + "WHERE a.{1} = b.{1} "
            + "AND a.{2} = b.{2} ";
    }
}