using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class ConsultasClass
    {
        /// <summary>
        /// Método que realiza el conteo de los spandatos agrupados hasta la fecha
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="indexConsultar">Tabindex para realizar la consulta</param>
        /// <param name="fechaNumMaxima">Fecha númerica máxima para realizar la consulta</param>
        /// <returns></returns>
        public static List<AgrupadorConteosTimeSpanDTO> ConsultarConteoSpanTiempo(SisResultEntities contexto, int indexConsultar, string fechaNumMaxima)
        {
            string query = string.Format(ConstantesConsulta.QUERY_COUNT_SPANTIEMPOS, indexConsultar, fechaNumMaxima);
            DbRawSqlQuery<AgrupadorConteosTimeSpanDTO> data = contexto.Database.SqlQuery<AgrupadorConteosTimeSpanDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que realiza la consulta de acumulada de los datos para el día de la semana y del mes
        /// Retorna las igualdades sumadas para el día semana y mes
        /// </summary>
        /// <param name="contexto">Instancia del contexto</param>
        /// <param name="diaSemana">Número del día de la semana</param>
        /// <param name="maxTabIndex">Máximo tabindex para realizar la comparación</param>
        /// <param name="fechaNum">Valor Número para la fecha</param>
        /// <param name="diaMes">Día del més a realizar la consulta</param>
        /// <returns></returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosAcumuladosIgualdadDiaSemanaMes(SisResultEntities contexto, int diaSemana, int maxTabIndex, string fechaNum, int diaMes)
        {
            string query = string.Format(ConstantesConsulta.QUERY_ACUMULADO_DATOS_IGUALDAD_DIA_MES, diaSemana, maxTabIndex, fechaNum, diaMes);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que retorna los datos con mayores probabilidades de aparecer de acuerdo a como se han registrado los resultados
        /// </summary>
        /// <param name="maxListIndex">Máximo tabindex para realizar la consulta, se devulven los valores menores o iguales
        /// al tabindex recibido</param>
        /// <param name="fechaFormat">Fecha formateada</param>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <returns></returns>
        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosParaDiaSeleccion(int maxListIndex, string fechaFormat, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM, fechaFormat, maxListIndex);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosParaDiaSeleccionBetween(int maxListIndex, string fechaFormat, SisResultEntities contexto, int percentMayor, int percentMenor)
        {
            string query = string.Format(ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_BETWEEN, fechaFormat, maxListIndex, percentMayor, percentMenor);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosParaDiaSeleccionLvl1(SisResultEntities contexto, string fechaNum)
        {
            string query = string.Format(ConstantesConsulta.QUERY_DATOS_MAS_VALORES_LVL1, fechaNum);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosParaDiaSeleccionLvl2(SisResultEntities contexto, string fechaNum)
        {
            string query = string.Format(ConstantesConsulta.QUERY_DATOS_MAS_VALORES_LVL2, fechaNum);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxFechaTabindex(SisResultEntities contexto, int maxindex)
        {
            string query = string.Format(ConstantesConsulta.QUERY_MAX_FECHA_TABINDEX, maxindex);
            return contexto.Database.SqlQuery<AgrupadorFechaNumTabindex>(query).AsEnumerable().First().FechaNum;
        }

        /// <summary>
        /// Método que retorna el máximo tabindex para la fecha ingresada
        /// </summary>
        /// <param name="fecha">Fecha para realizar la consulta</param>
        /// <param name="contexto">Contexto para realizar la consulta</param>
        /// <returns></returns>
        public static int ConsultarMaxIndexFecha(string fecha, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsulta.QUERY_MAX_INDEX_FECHA, fecha);
            return contexto.Database.SqlQuery<int>(query).Single();
        }

        /// <summary>
        /// Método que realiza la consulta del máximo tabindex registrado dentro de los resultados
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <returns>Valor del máximo tabindex</returns>
        public static int ConsultarMaxIndexResultados(SisResultEntities contexto)
        {
            return contexto.Database.SqlQuery<int>(ConstantesConsulta.QUERY_MAX_INDEX_RESULTADOS).Single();
        }

        /// <summary>
        /// Método que retorna los valores de los resultados para un día específico
        /// </summary>
        /// <param name="fechaFormat">Fecha formateada para realizar la obtención de datos</param>
        /// <param name="contexto">Instancia del contexto para hacer la consulta</param>
        /// <returns></returns>
        public static List<AgrupadorTabIndexDiferenciaDTO> ConsultarResultadosDia(string fechaFormat, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsulta.QUERY_RESULTADOS_DIA, fechaFormat);
            DbRawSqlQuery<AgrupadorTabIndexDiferenciaDTO> data = contexto.Database.SqlQuery<AgrupadorTabIndexDiferenciaDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que realiza la consulta del último span de datos para un tabindex y una fecha máxima para realizar la consulta
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="indexConsultar">Tabindex para realizar la consulta</param>
        /// <param name="fechaNumMaxima">Máxima fecha numérica para la consulta de datos anteriores</param>
        /// <returns></returns>
        public static List<AgrupadorConteosTimeSpanDTO> ConsultarUltimoSpanTiempo(SisResultEntities contexto, int indexConsultar, string fechaNumMaxima)
        {
            string query = string.Format(ConstantesConsulta.QUERY_ULTIMO_SPAN_TIEMPO, indexConsultar, fechaNumMaxima);
            DbRawSqlQuery<AgrupadorConteosTimeSpanDTO> data = contexto.Database.SqlQuery<AgrupadorConteosTimeSpanDTO>(query);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método que retorna el valor de la secuencia de la entidad recibida
        /// </summary>
        /// <param name="entidad">Entidad para verificar el valor de la secuencia</param>
        /// <param name="contexto">Contexto del modelo</param>
        /// <returns>Valor de la secuencia de la entidad recibida como parámetro</returns>
        public static long ObtenerValorSecuencia(object entidad, SisResultEntities contexto)
        {
            long valSecuencia = 0;
            string secuencia = "";
            if (entidad is USERRESULTTABLESFS)
            {
                secuencia = "sq_userresulttablesFs";
            }
            else if (entidad is ANALISTINDEXUNG)
            {
                secuencia = "sq_AnaListIndexUnG";
            }
            //else if (entidad is ANALISISSPANIGUALDADES)
            //{
            //    secuencia = "sq_analisisSpanIgualdades";
            //}
            //else if (entidad is ANALISISLISTINDEXLVLDOS)
            //{
            //    secuencia = "sq_AnalisisListIndexLvlDos";
            //}
            string consultaSecuencia = string.Format("SELECT {0}.NEXTVAL FROM dual", secuencia);
            valSecuencia = (long)contexto.Database.SqlQuery<decimal>(consultaSecuencia).ToList().Single();
            return valSecuencia;
        }
    }
}
