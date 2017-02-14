using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Persistencia
{
    public class Conexion
    {
        MySqlConnection conexion;
        DataTable DT;
        MySqlDataReader DR;
        MySqlCommand ds;
        //Instancia la conexion a la base de datos SQL-Server
        public Conexion()
        {
            conexion = new MySqlConnection(); 
            conexion.ConnectionString = "server=sql10.freemysqlhosting.net;user id=sql10158821;database=sql10158821;port=3306;password=iZ2FPqvKdH";
            //conexion.Open();
            //Registra la cadena de conexión SQL-Server
            ds = new MySqlCommand();
            ds.Connection = conexion;
        }

        /// <summary>
        /// Define la cadena de conexión
        /// </summary>
        /// <param name="nombre">recibe el nombre de la variable de conexión</param>
        public void SetCadenaConexion(string nombre)
        {
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings[nombre].ConnectionString;
        }


        //Abre la conexión de la base de datos
        public void Abrir_Conexion()
        {
            conexion.Open();
        }
        //Metodo que cierra la conexión con la base de datos
        public void Cerrar_Conexion()
        {
            conexion.Close();
        }
        //Metodo que libera los recursos utilizados por la conexión
        public void Liberar()
        {
            conexion.Dispose();
        }
        public void addParameter(string nombre, object dato)
        {
            //Define un parametro asociada a la query
            SqlParameter param = new SqlParameter(nombre, dato);
            ds.Parameters.Add(param);
        }
        public void clearParameter()
        {
            //Elimina los parametros existentes
            ds.Parameters.Clear();
        }
        public void SetQuery(string query)
        {
            ds.CommandType = CommandType.Text;
            ds.CommandText = query;
        }
        private void SetQuerySinVariables(string query)
        {
            ds.CommandType = CommandType.Text;
            ds.CommandText = query;
        }
        //Metodo que ejecuta querys, con la conexion activa
        public DataTable QuerySeleccion()
        {
            //crea el adaptador para ejecutar la query

            Abrir_Conexion();
            DT = new DataTable();
            //Quita el limite de tiempo de espera de una consulta
            ds.CommandTimeout = 0;
            using (DR = ds.ExecuteReader())
            {
                //Carga el datatable con el datareader
                DT.Load(DR);
                Cerrar_Conexion();
                return DT;
            }
        }
        public int EjecutarQuery()
        {
            //Se ejecuta la query
            //Quita el limite de tiempo de espera de una consulta
            Abrir_Conexion();
            ds.CommandTimeout = 0;
            int ok = ds.ExecuteNonQuery();
            Cerrar_Conexion();
            //
            return ok;
        }

        public int EjecutarQueryIdentity()
        {
            //Se ejecuta la query
            //Quita el limite de tiempo de espera de una consulta
            //Obtiene el valor del identity ingresado
            Abrir_Conexion();
            ds.CommandTimeout = 0;
            string consulta = ds.CommandText;
            if (!consulta.Substring(consulta.Length - 2, 1).Equals(";"))
            {
                consulta += ";  SELECT CAST (SCOPE_IDENTITY () AS int); ";
            }
            else
            {
                consulta += "  SELECT CAST (SCOPE_IDENTITY () AS int); ";
            }
            ds.CommandText = consulta;
            int id = (int)ds.ExecuteScalar();
            Cerrar_Conexion();
            //
            return id;
        }
    }
}
