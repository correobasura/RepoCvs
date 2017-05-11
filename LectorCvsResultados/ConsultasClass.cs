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
            else if (entidad is ANALISTINDEXUNGLV2)
            {
                secuencia = "sq_AnaListIndexUnGLv2";
            }
            //else if (entidad is ANALISISLISTINDEXLVLDOS)
            //{
            //    secuencia = "sq_AnalisisListIndexLvlDos";
            //}
            string consultaSecuencia = string.Format("SELECT {0}.NEXTVAL FROM dual", secuencia);
            valSecuencia = (long)contexto.Database.SqlQuery<decimal>(consultaSecuencia).ToList().Single();
            return valSecuencia;
        }

        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosParaDiaSeleccion(int maxListIndex, string fechaFormat, SisResultEntities contexto, int minimoRegistrosExistentes, int diaMes = 0)
        {
            string query = string.Format(ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM, fechaFormat, maxListIndex, minimoRegistrosExistentes, diaMes);
            DbRawSqlQuery<AgrupadorTotalTabIndexDTO> data = contexto.Database.SqlQuery<AgrupadorTotalTabIndexDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static int ConsultarMaxIndex(string fecha, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsulta.QUERY_MAX_INDEX_FECHA, fecha);
            return contexto.Database.SqlQuery<int>(query).Single();
        }

        public static List<AgrupadorTabIndexDiferenciaDTO> ConsultarTodosDatosDia(string fechaFormat, SisResultEntities contexto)
        {
            string query = string.Format(ConstantesConsulta.QUERY_TODOS_RESULTADOS_DIA, fechaFormat);
            DbRawSqlQuery<AgrupadorTabIndexDiferenciaDTO> data = contexto.Database.SqlQuery<AgrupadorTabIndexDiferenciaDTO>(query);
            return data.AsEnumerable().ToList();
        }

        public static List<AgrupadorTotalTabIndexDTO> ConsultarDatosIgualdadDia(SisResultEntities contexto, int diaSemana, int maxTabIndex, string fechaNum)
        {
            string query = string.Format(ConstantesConsulta.QUERY_DATOS_IGUALDAD_DIA, diaSemana, maxTabIndex, fechaNum);
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
    }
}
