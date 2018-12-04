namespace LectorCvsResultados.FlashOrdered
{
    public class ConstantesConsultaFO
    {
        public const string QUERY_MAX_ID_ACTUAL =
            "SELECT COALESCE(MAX(" + ConstantesModel.ID + "), 0)"
            + "FROM {0}";

        public const string QUERY_ULTIMOS_SPAN =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} {3}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ") "
            + "SELECT f.{2} AS spantiempo, f." + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + ", f." + ConstantesModel.FECHANUM + " AS " + ConstantesModel.FECHANUM + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON r." + ConstantesModel.TABINDEX + "  = f." + ConstantesModel.TABINDEX
            + " AND r." + ConstantesModel.FECHANUM + " = f." + ConstantesModel.FECHANUM + "";

        public const string QUERY_ULTIMOS_SPAN_TB_LETTER =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND {1} {3} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + "," + ConstantesModel.TABINDEXLETTER + ") "
            + "SELECT f.{2} AS spantiempo, f." + ConstantesModel.FECHANUM + " AS " + ConstantesModel.FECHANUM + ", f." + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + ", f." + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter "
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
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "{1} "
            + "GROUP BY " + ConstantesModel.TABINDEX + " ORDER BY 1";

        public const string QUERY_MAX_FECHA_TABINDEX_GL =
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEXLETTER + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} "
            + "AND " + ConstantesModel.TABINDEXLETTER + " <= {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + " ORDER BY 1";

        public const string QUERY_NEXT_TABINDEX_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXSEQ + "),0) AS Apariciones, " + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "GROUP BY " + ConstantesModel.TABINDEX;

        public const string QUERY_NEXT_TABINDEXLETTER_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXLETTERSEQ + "),0) AS Apariciones, " + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER;

        public const string QUERY_NEXT_TABINDEX_COMP_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXCOMPSEQ + "),0) AS Apariciones, " + ConstantesModel.IDCOMPETITION + " AS Lineindex, " + ConstantesModel.TABINDEXCOMPETITION + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.IDCOMPETITION + " IN ({0}) "
            + "GROUP BY " + ConstantesModel.IDCOMPETITION + ", " + ConstantesModel.TABINDEXCOMPETITION;

        public const string QUERY_MAXFECHAS_TABANDGROUPLETTER =
            "WITH registros AS ( "
            + "SELECT max(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.TABINDEXLETTER + " AS Tabindexletter, " + ConstantesModel.GROUPLETTER + " AS Groupletter "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " ) "
            + "SELECT f.id AS id, r.* "
            + "FROM " + ConstantesModel.FLASHORDERED + " f, registros r "
            + "WHERE r." + ConstantesModel.FECHANUM + "  = f." + ConstantesModel.FECHANUM + " "
            + "AND r." + ConstantesModel.TABINDEXLETTER + " = f." + ConstantesModel.TABINDEXLETTER + " "
            + "AND r." + ConstantesModel.GROUPLETTER + " = f." + ConstantesModel.GROUPLETTER + " ";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX =
            "SELECT a.total/a.Apariciones AS total, a." + ConstantesModel.TABINDEX + ", a.Apariciones, RANK () OVER(ORDER BY a.total/a.Apariciones DESC) AS Rank "
            + "FROM (SELECT COUNT(CASE WHEN " + ConstantesModel.DIFERENCIAG + " != 0 THEN 1 END) AS total, " + ConstantesModel.TABINDEX + ", COUNT(1) AS Apariciones "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND " + ConstantesModel.TABINDEX + " <= {1} {2}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ")a ";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX_GROUPANDTAB =
            "SELECT a.total/a.Apariciones AS total, "
            + "a." + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + ", "
            + "a." + ConstantesModel.TABINDEXLETTER + " AS " + ConstantesModel.TABINDEX + ", "
            + "a.Apariciones AS Apariciones, "
            + "RANK () OVER(ORDER BY a.total/a.Apariciones DESC) AS Rank "
            + "FROM (SELECT COUNT(CASE WHEN " + ConstantesModel.DIFERENCIAG + " != 0 THEN 1 END) AS total, "
            + "" + ConstantesModel.GROUPLETTER + ", "
            + "" + ConstantesModel.TABINDEXLETTER + ", "
            + "COUNT(1) AS Apariciones "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND {1} {2} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", "
            + ConstantesModel.TABINDEXLETTER + ")a";

        public const string QUERY_MAX_TABINDEXSEQ_GEN =
            "SELECT MAX(" + ConstantesModel.TABINDEXSEQ + ") AS total, " + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " <= {1} "
            + "GROUP BY " + ConstantesModel.TABINDEX + "";

        public const string QUERY_MAX_TABINDEXSEQ_GL =
            "SELECT MAX(" + ConstantesModel.TABINDEXLETTERSEQ + ") AS total, " + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " AS Tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " <= {0} AND {1} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + "";

        public const string QUERY_MAX_FECHANUMTABINDEX =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} {2}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ") "
            + "SELECT f.{3} AS spantiempo,f." + ConstantesModel.FECHANUM + " AS " + ConstantesModel.FECHANUM + ", f." + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON f." + ConstantesModel.FECHANUM + "  = r." + ConstantesModel.FECHANUM
                + " AND f." + ConstantesModel.TABINDEX + " = r." + ConstantesModel.TABINDEX + " ";

        public const string QUERY_MAX_FECHANUMTABINDEX_GL =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter, " + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND ({1}) {2}"
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + ") "
            + "SELECT f.{3} AS spantiempo,f." + ConstantesModel.FECHANUM + " AS " + ConstantesModel.FECHANUM + ", f." + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter, f. " + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON f." + ConstantesModel.FECHANUM + "  = r." + ConstantesModel.FECHANUM
                + " AND f." + ConstantesModel.TABINDEXLETTER + " = r." + ConstantesModel.TABINDEXLETTER + " "
                + " AND f." + ConstantesModel.GROUPLETTER + " = r." + ConstantesModel.GROUPLETTER + " ";

        public const string QUERY_RANK_SPANS_BY_TABINDEX =
            "SELECT COUNT(1) AS total, {0} AS spanTiempo, " + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + ", "
            + "RANK () OVER (PARTITION BY " + ConstantesModel.TABINDEX + " ORDER BY COUNT(1) DESC) AS rank "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE ({1}) "
            + "AND {0} IS NOT NULL {2} "
            + "GROUP BY {0}, " + ConstantesModel.TABINDEX;

        public const string QUERY_RANK_SPANS_BY_GL_TABINDEX =
            "SELECT COUNT(1) AS total, {0} AS spanTiempo, " + ConstantesModel.TABINDEXLETTER + " AS " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " AS " + ConstantesModel.GROUPLETTER + ", "
            + "RANK () OVER (PARTITION BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " ORDER BY COUNT(1) DESC) AS rank "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE ({1}) "
            + "AND {0} IS NOT NULL {2} "
            + "GROUP BY {0}, " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + "";

        public const string QUERY_RANK_PERCENT =
            "SELECT b.total/a.total AS Prob, b.percent AS Percent, RANK () OVER (order by b.total/a.total) AS rank, b.total AS TotalGen, a.total AS TotalDif "
            + "FROM (SELECT COUNT(1) AS total, percent "
            + "FROM andatapercentung "
            + "WHERE {3} " + ConstantesModel.FECHANUM + " < {0} {1} "
            + "AND tipoorden = {2} "
            + "GROUP BY percent "
            + ") a, "
            + "(SELECT COUNT(1) AS total, percent "
            + "FROM andatapercentung "
            + "WHERE {3} " + ConstantesModel.FECHANUM + " < {0} {1} "
            + "AND tipoorden = {2} "
            + "AND " + ConstantesModel.DIFERENCIAG + " != 0 "
            + "GROUP BY percent "
            + ") b "
            + "WHERE a.percent = b.percent ";

        public const string QUERY_COUNT_PROB_SELINF =
            "SELECT a.total/b.total AS Prob, a.{1} AS Spantiempo, a.{2} AS Rank, b.total AS Total "
            + "FROM ( "
            + "SELECT COUNT(1) AS total, {1}, {2}  "
            + "FROM andataselectedinfo  "
            + "WHERE " + ConstantesModel.DIFERENCIAG + " != 0 "
            + "AND {0} " + ConstantesModel.FECHANUM + " < {3} "
            + "GROUP BY {1}, {2}) a, ( "
            + "SELECT COUNT(1) AS total, {1}, {2}  "
            + "FROM andataselectedinfo  "
            + "WHERE {0} " + ConstantesModel.FECHANUM + " < {3} "
            + "GROUP BY {1}, {2}) b "
            + "WHERE a.{1} = b.{1} "
            + "AND a.{2} = b.{2} ";

        //public const string QUERY_COUNT_PROB_DIADIAMES =
        //    "SELECT b.total/a.total AS Total, a." + ConstantesModel.TABINDEX + " AS Tabindex "
        //    + "FROM "
        //    + "(SELECT COUNT(1) AS total, " + ConstantesModel.TABINDEX + " "
        //    + "FROM " + ConstantesModel.FLASHORDERED + " "
        //    + "WHERE " + ConstantesModel.MESNUM + "     = {0} "
        //    + "AND " + ConstantesModel.FECHANUM + "     < {1} "
        //    + "AND " + ConstantesModel.TABINDEX + "     <= {2} "
        //    + "AND " + ConstantesModel.DIFERENCIAG + " != 0 "
        //    + "GROUP BY " + ConstantesModel.TABINDEX + ")B, "
        //    + "(SELECT COUNT(1) AS total, " + ConstantesModel.TABINDEX + " "
        //    + "FROM " + ConstantesModel.FLASHORDERED + " "
        //    + "WHERE " + ConstantesModel.MESNUM + " = {0} "
        //    + "AND " + ConstantesModel.FECHANUM + "   < {1} "
        //    + "AND " + ConstantesModel.TABINDEX + "   <= {2} "
        //    + "GROUP BY " + ConstantesModel.TABINDEX + " )A "
        //    + "WHERE a." + ConstantesModel.TABINDEX + " = b." + ConstantesModel.TABINDEX + " "
        //    + "AND b.total/a.total >= 0.85 "
        //    + "ORDER BY 1 DESC";

        //public const string QUERY_COUNT_PROB_DIADIAMES_GL =
        //    "SELECT b.total/a.total AS Total, a." + ConstantesModel.TABINDEXLETTER + " AS Tabindex, b." + ConstantesModel.GROUPLETTER + " AS Groupletter "
        //    + "FROM "
        //    + "(SELECT COUNT(1) AS total, " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " "
        //    + "FROM " + ConstantesModel.FLASHORDERED + " "
        //    + "WHERE " + ConstantesModel.MESNUM + "     = {0} "
        //    + "AND " + ConstantesModel.FECHANUM + "     < {1} AND {2} "
        //    + "AND " + ConstantesModel.DIFERENCIAG + " != 0 "
        //    + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + ")B, "
        //    + "(SELECT COUNT(1) AS total, " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " "
        //    + "FROM " + ConstantesModel.FLASHORDERED + " "
        //    + "WHERE " + ConstantesModel.MESNUM + " = {0} "
        //    + "AND " + ConstantesModel.FECHANUM + " < {1} AND {2} "
        //    + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + ")A "
        //    + "WHERE a." + ConstantesModel.TABINDEXLETTER + " = b." + ConstantesModel.TABINDEXLETTER + " "
        //    + "AND a." + ConstantesModel.GROUPLETTER + "      = b." + ConstantesModel.GROUPLETTER + " "
        //    + "AND b.total/a.total >= 0.85 "
        //    + "ORDER BY 1 DESC";

        public const string QUERY_COUNT_PROB_DIADIAMES =
            "SELECT a.total, a.Tabindex "
            + "FROM ( "
            + "SELECT COUNT(CASE WHEN " + ConstantesModel.DIFERENCIAG + " != 0 THEN 1 END)/COUNT(1) AS total, " + ConstantesModel.TABINDEX + " AS Tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.MESNUM + "     = {0} "
            + "AND " + ConstantesModel.FECHANUM + "     < {1} "
            + "AND " + ConstantesModel.TABINDEX + "     <= {2} "
            + "GROUP BY " + ConstantesModel.TABINDEX + ")a "
            + "WHERE a.total >= 0.85";

        public const string QUERY_COUNT_PROB_DIADIAMES_GL =
            "SELECT a.Total, a." + ConstantesModel.TABINDEXLETTER + " AS Tabindex, a." + ConstantesModel.GROUPLETTER + " AS Groupletter "
            + "FROM "
            + "(SELECT COUNT(CASE WHEN " + ConstantesModel.DIFERENCIAG + " != 0 THEN 1 END)/COUNT(1) AS total, " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.MESNUM + "     = {0} "
            + "AND " + ConstantesModel.FECHANUM + "     < {1} AND {2} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + ")a "
            + "WHERE a.total >= 0.85";

        public const string QUERY_COUNT_PROB_RANK =
            "SELECT a.total AS Prob, a." + ConstantesModel.KEYRANKGRAL + " AS KEY "
            + "FROM ( "
            + "SELECT COUNT(CASE WHEN " + ConstantesModel.DIFERENCIAG + " != 0 THEN 1 END)/COUNT(1) AS total, " + ConstantesModel.KEYRANKGRAL + " "
            + "FROM andatabininfo "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} AND tipo IS NULL "
            + "{1} "
            + "GROUP BY " + ConstantesModel.KEYRANKGRAL + " "
            + "HAVING COUNT(1)>2) a "
            + "WHERE a.total > 0.85";

        public const string QUERY_DIAS_BY_TABINDEX =
            "SELECT id "
            + "FROM totalesdia "
            + "WHERE total = {0} "
            + "ORDER BY id";

        public const string QUERY_PIVOT_COUNT_RESULTS =
            "SELECT * FROM "
            + "(SELECT tabindex, fechanum "
            + "FROM flashordered "
            + "WHERE fechanum IN (SELECT id FROM totalesdia WHERE total = {0})"
            + "AND diferenciag = 0) "
            + "pivot (COUNT(1) FOR fechanum IN ({1}))";

        public const string QUERY_COUNT_GRP_TAB_TOTAL =
            "SELECT COUNT(CASE WHEN f.diferenciag = 0 THEN 1 END)/COUNT(1) AS PROB, "
            + "COUNT(CASE WHEN f.diferenciag = 0 THEN 1 END) AS CERO, "
            + "COUNT(1) AS TOTAL, f.groupletter AS groupletter, f.tabindexletter AS tabindexletter "
            + "FROM flashordered f "
            + "INNER JOIN totalesdiagroptab t ON t.total = {1} "
            + "AND t.fechanum < {2} "
            + "AND t.fechanum = f.fechanum "
            + "AND f.groupletter = t.groupletter "
            + "AND t.groupletter = '{0}' "
            + "GROUP BY f.groupletter, f.tabindexletter";
    }
}