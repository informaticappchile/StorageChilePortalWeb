using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Mail;
using Entidades;

namespace Persistencia
{
    public class User_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarUser(User_EN u)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                nueva_conexion.Abrir_Conexion();
                string insert = "insert into users(email,nombre,username,password,age,gender,locality,profile_visibility,verified) VALUES ('"
                    + u.Correo + "','" + u.Nombre + "','" + u.NombreUsu + "','" + u.Contraseña + "'," + 
                    0 + "," + 0 + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */ 
         
        public ArrayList MostrarUser(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.Abrir_Conexion();
            nueva_conexion.SetQuery("Select * from Users where ID=" + u.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                User_EN usuario = new User_EN();
                usuario.ID = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                usuario.Correo = dt.Rows[0]["email"].ToString();
                usuario.Nombre = dt.Rows[0]["nombre"].ToString();
                usuario.NombreUsu = dt.Rows[0]["username"].ToString();
                usuario.Contraseña = dt.Rows[0]["password"].ToString();
                usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                usuario.Verified = Convert.ToBoolean(dt.Rows[0]["verified"]);
                lista.Add(usuario);
            }

            return lista;
        }

        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public void BorrarUser(User_EN u)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Users where Users.ID = '" + u.ID;
                nueva_conexion.SetQuery(delete);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public User_EN BuscarUser(string busqueda)
        {
            User_EN usuario = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Usuario where UserName ='" + busqueda + "' or Email = '" + busqueda + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    usuario = new User_EN();
                    usuario.ID = Convert.ToInt16(dt.Rows[0]["IdUsuario"]);
                    usuario.Correo = dt.Rows[0]["Email"].ToString();
                    usuario.Nombre = dt.Rows[0]["NombreCompleto"].ToString();
                    usuario.NombreUsu = dt.Rows[0]["UserName"].ToString();
                    usuario.Contraseña = dt.Rows[0]["Password"].ToString();
                    usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["IdPerfil"].ToString());
                    usuario.Verified = Convert.ToBoolean(dt.Rows[0]["Verificado"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return usuario;
        }

        /**
         * Se encarga de listar todos los amigos que tiene un usuario
         **/ 
        public ArrayList ListarAmigos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select * from Friends where Friends.User1 = ");
            DataTable dt = nueva_conexion.QuerySeleccion();

            while (dt != null)
            {
                lista.Add(dt.Rows[0]["Users2"].ToString());
            }

            return lista;
        }

        /**
         * Se encarga de mostrarnos todos los datos del usuario y contraseña que le pasamos
         **/ 
        public User_EN LeerUser(User_EN u)
        {

            Conexion nueva_conexion = new Conexion();
            User_EN usuario = new User_EN();

            try
            {
                string select = "";
                select = "Select * from Users where username ='" + u.NombreUsu + "' and password = '" + u.Contraseña + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();

                if (dt != null)
                {

                    usuario.ID = Convert.ToInt16(dt.Rows[0]["ID"]);
                    usuario.Correo = dt.Rows[0]["email"].ToString();
                    usuario.Nombre = dt.Rows[0]["nombre"].ToString();
                    usuario.NombreUsu = dt.Rows[0]["username"].ToString();
                    usuario.Contraseña = dt.Rows[0]["password"].ToString();
                    usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                    usuario.Verified = Convert.ToBoolean(dt.Rows[0]["verified"]);
                }

                
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return usuario;
        }

        /**
         * Se encarga de una vez recibido el email y darle al link, poner al que le perteneza ese usuario el verified a 1
         **/ 
        public void confirmacionUser(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                update = "Update Users set verified = '1' where Users.email = '" + u.Correo + "'";
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }
        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/ 
         
        public void actualizarUser(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                

                update = "Update Users set email = '" + u.Correo + "',nombre  = '" + u.Nombre + 
                    "',username = '" + u.NombreUsu + "',password = '" + u.Contraseña + "',profile_visibility = '" 
                    + u.IdPerfil + "' where Users.ID ="+u.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }


    }
}
