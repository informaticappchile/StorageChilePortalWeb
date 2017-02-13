using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
namespace Persistencia
{
    public class PersistenciaPersona
    {
        public static DataTable ObtenerNombres()
        {
            
            //Se instancia la conexion
            Conexion con = new Conexion();
            con.SetCadenaConexion("conexionStorage");
            //Se realiza la consulta
            string query = "SELECT * From Persona p";
            con.SetQuery(query);
            return con.QuerySeleccion();

          
        }
    }
}
