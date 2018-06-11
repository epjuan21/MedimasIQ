using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MedimasIQ
{
    public class Consulta
    {
        DataAccessClass oAccesDatos = new DataAccessClass();

        // Obtener Datos Registro de Control
        public String RegistroControl(String numeroFactura)
        {
            String Registros = null;
            IDataReader DataReader;
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @FACTURA VARCHAR(6) = '" + numeroFactura + "'");
            sb.Append("DECLARE @NIT_PRESTADOR VARCHAR(15) = '890981494' ");
            sb.Append("DECLARE @NOMBRE_PRESTADOR VARCHAR(50)= 'ESE HOSPITAL SAN ANTONIO' ");
            sb.Append("DECLARE @NIT_PAGADOR VARCHAR(15) = '901097473' ");
            sb.Append("DECLARE @NOMBRE_PAGADOR VARCHAR(50) = 'MEDIMAS' ");

            sb.Append("SELECT TOP 1 ");
            sb.Append("'0' AS TIPO_REGISTRO ");
            sb.Append(",CONVERT(VARCHAR,REPLICATE('0', 6 - LEN(dbo.FUC_MED_01(@FACTURA)))) + CONVERT(VARCHAR,dbo.FUC_MED_01(@FACTURA)) AS NUMERO_REGISTROS ");
            sb.Append(",REPLICATE('0',5) + '1' AS NUMERO_FACTURAS ");
            sb.Append(",CONVERT(varchar, GETDATE(), 103) AS FECHA_ENVIO ");
            sb.Append(",REPLICATE('0',5) + '1' AS CODIGO_SUCURSAL ");
            sb.Append(",'NI' AS TIPO_DOCUMENTO_PRESTADOR ");
            sb.Append(",REPLICATE('0', 16 - LEN(@NIT_PRESTADOR)) + @NIT_PRESTADOR AS NIT_PRESTADOR ");
            sb.Append(",'2' AS DIGITO_VERIFICACION_PRESTADOR ");
            sb.Append(",@NOMBRE_PRESTADOR + SPACE(50 - LEN(@NOMBRE_PRESTADOR)) AS NOMBRE_PRESTADOR ");
            sb.Append(",'NI' AS TIPO_DOCUMENTO_PAGADOR ");
            sb.Append(",REPLICATE('0', 16 - LEN(@NIT_PAGADOR)) + @NIT_PAGADOR AS NIT_PAGADOR ");
            sb.Append(",'5' AS DIGITO_VERIFICACION_PAGADOR ");
            sb.Append(",@NOMBRE_PAGADOR + SPACE(50 - LEN(@NOMBRE_PAGADOR)) AS NOMBRE_PAGADOR ");
            sb.Append("FROM VIEW_IQ ");
            sb.Append("WHERE NUMERO_FACTURA = @FACTURA");

            try
            {
                oAccesDatos.Open();
                DataReader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (DataReader.Read())
                {
                    Registros = String.Join("",
                    DataReader["TIPO_REGISTRO"].ToString() +
                    DataReader["NUMERO_REGISTROS"].ToString() +
                    DataReader["NUMERO_FACTURAS"].ToString() +
                    DataReader["FECHA_ENVIO"].ToString() +
                    DataReader["CODIGO_SUCURSAL"].ToString() +
                    DataReader["TIPO_DOCUMENTO_PRESTADOR"].ToString() +
                    DataReader["NIT_PRESTADOR"].ToString() +
                    DataReader["DIGITO_VERIFICACION_PRESTADOR"].ToString() +
                    DataReader["NOMBRE_PRESTADOR"].ToString() +
                    DataReader["TIPO_DOCUMENTO_PAGADOR"].ToString() +
                    DataReader["NIT_PAGADOR"].ToString() +
                    DataReader["DIGITO_VERIFICACION_PAGADOR"].ToString() +
                    DataReader["NOMBRE_PAGADOR"].ToString()
                        );
                }
                DataReader.Close();
                return Registros;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                
            }


        }

        public String RegistroCabecera(String numeroFactura)
        {
            String Registros = null;
            IDataReader DataReader;
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @FACTURA VARCHAR(6) = '" + numeroFactura + "'");

            sb.Append("SELECT TOP 1 ");
            sb.Append("'1' AS TIPO_REGISTRO ");
            sb.Append(",'1' AS CAMPO_FIJO ");
            sb.Append(",FECHA_FACT AS FECHA_FACURA ");
            sb.Append(",REPLICATE(' ',6) AS LETRAS_FACTURA ");
            sb.Append(",REPLICATE('0', 10 - LEN(@FACTURA)) + @FACTURA AS DIGITOS_FACTURA ");
            sb.Append(",VALOR_BRUTO AS VALOR_BRUTO ");
            sb.Append(",VALOR_NETO AS VALOR_NETO ");
            sb.Append(",dbo.FUC_CAN_LET(VALOR_NETO) AS VALOR_NETO_EN_LETRAS ");
            sb.Append(",'0000000000' AS CAMPO_FIJO_2 ");
            sb.Append(",REPLICATE('0',10 - LEN(VALOR_MODERADORA)) +  CONVERT(VARCHAR,VALOR_MODERADORA) AS VALOR_MODERADORA ");
            sb.Append(",REPLICATE('0',10 - LEN(VALOR_COPAGO)) + CONVERT(VARCHAR,VALOR_COPAGO) AS VALOR_COPAGO ");
            sb.Append(",REPLICATE(' ',10 - LEN('02')) + '02' AS TIPO_CUENTA ");
            sb.Append(",REPLICATE(' ',10 - LEN('')) + '' AS VALOR_IVA ");
            sb.Append(",'0' AS CAMPO_FIJO_3 ");
            sb.Append("FROM VIEW_IQ ");
            sb.Append("WHERE NUMERO_FACTURA = @FACTURA");

            try
            {
                oAccesDatos.Open();
                DataReader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (DataReader.Read())
                {
                    Registros = String.Join("",
                       DataReader["TIPO_REGISTRO"].ToString() +
                       DataReader["CAMPO_FIJO"].ToString() +
                       DataReader["FECHA_FACURA"].ToString() +
                       DataReader["LETRAS_FACTURA"].ToString() +
                       DataReader["DIGITOS_FACTURA"].ToString() +
                       DataReader["VALOR_BRUTO"].ToString() +
                       DataReader["VALOR_NETO"].ToString() +
                       DataReader["VALOR_NETO_EN_LETRAS"].ToString() +
                       DataReader["CAMPO_FIJO_2"].ToString() +
                       DataReader["VALOR_MODERADORA"].ToString() +
                       DataReader["VALOR_COPAGO"].ToString() +
                       DataReader["TIPO_CUENTA"].ToString() +
                       DataReader["VALOR_IVA"].ToString() +
                       DataReader["CAMPO_FIJO_3"].ToString()
                        );
                }
                DataReader.Close();
                return Registros;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }

        }

        public DataTable dt(String FechaInicial, String FechaFinal)
        {
            DataTable Tabla = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [GRUPOS ETAREOS QUINQUENAL] ");
            sb.Append(",COUNT([GRUPOS ETAREOS QUINQUENAL]) AS CANTIDAD ");
            sb.Append("FROM VIM_RIPS_CONSULTA_S02 ");
            sb.Append("WHERE FECHA BETWEEN " + FechaInicial + " AND " + FechaFinal + " ");
            sb.Append("AND DIAGNOSTICO LIKE 'A09X%' AND DOCUMENTO NOT LIKE 'X01%' GROUP BY [GRUPOS ETAREOS QUINQUENAL] ORDER BY [GRUPOS ETAREOS QUINQUENAL]");

            try
            {
                oAccesDatos.Open();
                Tabla = oAccesDatos.Dt(CommandType.Text, sb.ToString());

                return Tabla;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }
        }

        public String GetArticulos(String Tarifa)
        {
            IDataReader reader = null;
            String valor = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TRF_TARIFA, TRF_ARTIC, TRF_FCH ");
            sb.Append("FROM TMTARIFAARTICULO ");
            sb.Append("WHERE TRF_TARIFA = " + Tarifa + "");

            try
            {
                oAccesDatos.Open();
                reader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (reader.Read())
                {
                    valor = reader.GetValue(0).ToString(); ;
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }

        }

        public String GetDato(String FechaInicial, String FechaFinal, String Condicion)
        {
            IDataReader reader = null;
            String valor = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(*) ");
            sb.Append("AS CANTIDAD FROM VIM_RIPS_CONSULTA_S02 ");
            sb.Append("WHERE FECHA BETWEEN " + FechaInicial + " AND " + FechaFinal + "");
            sb.Append("AND DIAGNOSTICO LIKE 'A09X%' AND DOCUMENTO NOT LIKE 'X01%' AND [GRUPOS ETAREOS QUINQUENAL] = '" + Condicion + "' ");

            try
            {
                oAccesDatos.Open();
                reader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (reader.Read())
                {
                    valor = reader["CANTIDAD"].ToString();
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }

        }

        public String GetTotal(String FechaInicial, String FechaFinal)
        {
            IDataReader reader = null;
            String valor = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(*) ");
            sb.Append("AS CANTIDAD FROM VIM_RIPS_CONSULTA_S02 ");
            sb.Append("WHERE FECHA BETWEEN " + FechaInicial + " AND " + FechaFinal + "");
            sb.Append("AND DIAGNOSTICO LIKE 'A09X%' AND DOCUMENTO NOT LIKE 'X01%'");

            try
            {
                oAccesDatos.Open();
                reader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (reader.Read())
                {
                    valor = reader["CANTIDAD"].ToString();
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }
        }

        public String GetTotalSexo(String FechaInicial, String FechaFinal, String Sexo)
        {
            IDataReader reader = null;
            String valor = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(SEXO) ");
            sb.Append("AS CANTIDAD FROM VIM_RIPS_CONSULTA_S02 ");
            sb.Append("WHERE FECHA BETWEEN " + FechaInicial + " AND " + FechaFinal + "");
            sb.Append("AND DIAGNOSTICO LIKE 'A09X%' AND DOCUMENTO NOT LIKE 'X01%' ");
            sb.Append("AND SEXO = '" + Sexo + "'");

            try
            {
                oAccesDatos.Open();
                reader = oAccesDatos.ExecuteReader(CommandType.Text, sb.ToString());

                while (reader.Read())
                {
                    valor = reader["CANTIDAD"].ToString();
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }


        }

        public String GetTotalSP(String FechaInicial, String FechaFinal)
        {
            IDataReader reader = null;
            String valor = null;

            String Procedimiento = "SP_TOTAL_EDA";

            try
            {
                oAccesDatos.Open();

                reader = oAccesDatos.ExecuteReader(CommandType.StoredProcedure, Procedimiento, FechaInicial, FechaFinal);

                while (reader.Read())
                {
                    valor = reader["CANTIDAD"].ToString();
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }
        }

        public String GetEDAGrupo(String FechaInicial, String FechaFinal, String Grupo)
        {

            IDataReader reader = null;
            String valor = null;

            String Procedimiento = "SP_EDA_GRUPOS";

            try
            {
                oAccesDatos.Open();
                reader = oAccesDatos.ExecuteReader(CommandType.StoredProcedure, Procedimiento, FechaInicial, FechaFinal, Grupo);


                while (reader.Read())
                {
                    valor = reader["CANTIDAD"].ToString();
                }
                reader.Close();
                return valor;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();
            }
        }
    }
}
