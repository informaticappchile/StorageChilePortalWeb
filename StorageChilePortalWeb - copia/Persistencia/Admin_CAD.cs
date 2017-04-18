using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;
using Entidades;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace Persistencia
{
    public class Admin_CAD
    {
        public User_EN logAdmin(string userName, string password)
        {
            User_EN usuario = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Usuario where UserName ='" + userName + "' and Password = '" + password + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    usuario = new User_EN();
                    usuario.ID = Convert.ToInt16(dt.Rows[0]["IdUsuario"]);
                    usuario.Correo = dt.Rows[0]["Email"].ToString();
                    usuario.Nombre = dt.Rows[0]["NombreCompleto"].ToString();
                    usuario.NombreUsu = dt.Rows[0]["UserName"].ToString();
                    usuario.Contraseña = (byte[])dt.Rows[0]["Password"];
                    usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["IdPerfil"].ToString());
                    if (Convert.ToBoolean(dt.Rows[0]["Verificado"]))
                    {
                        usuario.Verified = "Verificado";
                    }
                    else
                    {
                        usuario.Verified = "No Verificado";
                    }
                    usuario.Intentos = Convert.ToInt16(dt.Rows[0]["Intentos"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return usuario;
        }
        /*
        ArrayList lista = new ArrayList();

        public ArrayList MostrarUsers()
        {
            //SqlConnection c = new SqlConnection(Constants.nombreConexion);
            c.Open();
            SqlCommand com = new SqlCommand("Select * from Users", c);
            SqlDataReader dr = com.ExecuteReader();


            while (dr.Read())
            {
                User_EN usuario = new User_EN();
                usuario.ID = (short)dr["ID"];
                usuario.Correo = dr["email"].ToString();
                usuario.Nombre = dr["nombre"].ToString();
                usuario.NombreUsu = dr["username"].ToString();
                usuario.Contraseña = dr["password"].ToString();
                usuario.Edad = (short)dr["age"];
                usuario.Genero = (bool?)dr["gender"];
                usuario.Localidad = dr["locality"].ToString();
                usuario.Visibilidad_perfil = (bool)dr["profile_visibility"];
                usuario.Verified = (bool)dr["verified"];

                lista.Add(usuario);
            }
            dr.Close();
            c.Close();

            return lista;
        }*/
    }
}
