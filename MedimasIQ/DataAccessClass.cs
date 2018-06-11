using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MedimasIQ
{
    class DataAccessClass
    {
        #region Variables
        private string cadena = string.Empty;

        public SqlConnection oConnection { get; set; }

        public SqlCommand oCommand { get; set; }

        public IDataReader Lector { get; set; }

        public DataTable Tabla { get; set; }

        #endregion

        #region Constructores
        public DataAccessClass()
        {
            cadena = "Data Source=SERVIDOR01;Initial Catalog=BETANIA;Integrated Security=True";
            oConnection = new SqlConnection();
            oCommand = new SqlCommand();
        }

        #endregion

        #region Metodos
        public void Open()
        {
            if (oConnection.State == ConnectionState.Open)
            {
                return;
            }

            oConnection.ConnectionString = cadena;

            try
            {
                oConnection.Open();
            }
            catch
            {
                throw;
            }

        }

        public void Close()
        {
            if (oConnection.State == ConnectionState.Closed)
            {
                return;
            }

            oConnection.Close();
        }


        // Metodo para retornar Data Table

        public DataTable Dt(CommandType tipoComando, String consulta)
        {
            Tabla = null;
            oCommand.Connection = oConnection;
            oCommand.CommandType = tipoComando;
            oCommand.CommandText = consulta;

            try
            {
                SqlCommand cmd = new SqlCommand(consulta, oCommand.Connection);
                cmd.CommandType = oCommand.CommandType;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable Tabla = new DataTable();

                adapter.Fill(Tabla);
                return Tabla;
            }
            catch (Exception)
            {

                throw;
            }


        }

        // Metodo para seleccionar Datos
        public IDataReader ExecuteReader(CommandType tipoComando, String consulta)
        {
            Lector = null;
            oCommand.Connection = oConnection;
            oCommand.CommandType = tipoComando;
            oCommand.CommandText = consulta;

            try
            {
                Lector = oCommand.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }

            return Lector;
        }

        // Ejecuta Procedimiento Almacenado
        public IDataReader ExecuteReader(CommandType tipoComando, String Procedimiento, String Value1, String Value2)
        {
            Lector = null;
            oCommand.Connection = oConnection;
            oCommand.CommandType = tipoComando;
            oCommand.CommandText = Procedimiento;

            oCommand.Parameters.Clear();
            oCommand.Parameters.AddWithValue("FechaInicial", Value1);
            oCommand.Parameters.AddWithValue("FechaFinal", Value2);

            try
            {
                Lector = oCommand.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }

            return Lector;
        }

        public IDataReader ExecuteReader(CommandType tipoComando, String Procedimiento, String FechaInicial, String FechaFinal, String Grupo)
        {
            Lector = null;
            oCommand.Connection = oConnection;
            oCommand.CommandType = tipoComando;
            oCommand.CommandText = Procedimiento;

            oCommand.Parameters.Clear();
            oCommand.Parameters.AddWithValue("FechaInicial", FechaInicial);
            oCommand.Parameters.AddWithValue("FechaFinal", FechaFinal);
            oCommand.Parameters.AddWithValue("Grupo", Grupo);

            try
            {
                Lector = oCommand.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }

            return Lector;
        }
    }


    #endregion

}
