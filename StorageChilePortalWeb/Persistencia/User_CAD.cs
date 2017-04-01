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
using MySql.Data.MySqlClient;

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
                
                string insert = "insert into Usuario(Email,NombreCompleto,UserName,Password,IdPerfil,Verificado,IdEmpresa) VALUES ('"
                    + u.Correo + "','" + u.Nombre + "','" + u.NombreUsu + "','" + u.Contraseña + "'," + 
                    u.IdPerfil + "," + 0 + "," + u.IdEmpresa + ")";
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
            nueva_conexion.SetQuery("Select * from Usuario where IdUsuario=" + u.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                User_EN usuario = new User_EN();
                usuario.ID = Convert.ToInt16(dt.Rows[0]["IdUsuario"]);
                usuario.Correo = dt.Rows[0]["Email"].ToString();
                usuario.Nombre = dt.Rows[0]["NombreCompleto"].ToString();
                usuario.NombreUsu = dt.Rows[0]["UserName"].ToString();
                usuario.Contraseña = dt.Rows[0]["Password"].ToString();
                usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["IdPerfil"].ToString());
                if (Convert.ToBoolean(dt.Rows[0]["Verificado"]))
                {
                    usuario.Verified = "Verificado";
                }
                else
                {
                    usuario.Verified = "No Verificado";
                }
                lista.Add(usuario);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarUsuarios()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select u.UserName, u.NombreCompleto, u.Email, u.Verificado, u.FechaRegistro, u.FechaUltimoIngreso "+
                                    "from Usuario u, Perfil p "+
                                    "where u.IdPerfil = p.IdPerfil and p.NombrePerfil = 'Usuario' ");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User_EN usuario = new User_EN();
                usuario.Correo = dt.Rows[i]["Email"].ToString();
                usuario.Nombre = dt.Rows[i]["NombreCompleto"].ToString();
                usuario.NombreUsu = dt.Rows[i]["UserName"].ToString();
                if (Convert.ToBoolean(dt.Rows[i]["Verificado"]))
                {
                    usuario.Verified = "Verificado";
                }
                else
                {
                    usuario.Verified = "No Verificado";
                }              
                usuario.FechaRegistro = DateTime.Parse(dt.Rows[i]["FechaRegistro"].ToString());
                usuario.UltimoIngreso = DateTime.Parse(dt.Rows[i]["FechaUltimoIngreso"].ToString());
                lista.Add(usuario);

            }

            return lista;
            
        }




        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarUser(string userName)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Usuario where Usuario.UserName = '" + userName +"'";
                nueva_conexion.SetQuery(delete);


                nueva_conexion.EjecutarQuery();
                return true;
            }
            catch (Exception ex) { ex.Message.ToString(); return false; }
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
                string select = "Select * from Usuario, Empresa where (UserName ='" + busqueda + "' or Email = '" + busqueda + 
                    "') and Empresa.IdEmpresa = Usuario.IdEmpresa";
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
                    if (Convert.ToBoolean(dt.Rows[0]["Verificado"]))
                    {
                        usuario.Verified = "Verificado";
                    }
                    else
                    {
                        usuario.Verified = "No Verificado";
                    }
                    usuario.Intentos = Convert.ToInt16(dt.Rows[0]["Intentos"]);
                    usuario.NombreEmp = dt.Rows[0]["NombreEmpresa"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return usuario;
        }

        /**
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public User_EN BuscarUserAdmin(string busqueda)
        {
            User_EN usuario = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Usuario where UserName ='" + busqueda + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    usuario = new User_EN();
                    usuario.ID = Convert.ToInt16(dt.Rows[0]["IdUsuario"]);
                    usuario.Correo = dt.Rows[0]["Email"].ToString();
                    usuario.Nombre = dt.Rows[0]["NombreCompleto"].ToString();
                    usuario.NombreUsu = dt.Rows[0]["UserName"].ToString();
                    usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["IdPerfil"].ToString());
                    if (Convert.ToBoolean(dt.Rows[0]["Verificado"]))
                    {
                        usuario.Verified = "Verificado";
                    }
                    else
                    {
                        usuario.Verified = "No Verificado";
                    }
                    usuario.FechaRegistro = DateTime.Parse(dt.Rows[0]["FechaRegistro"].ToString());
                    usuario.UltimoIngreso = DateTime.Parse(dt.Rows[0]["FechaUltimoIngreso"].ToString());
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
                select = "Select * from Usuario where UserName ='" + u.NombreUsu + "' and Password = '" + u.Contraseña + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();

                if (dt != null)
                {

                    usuario.ID = Convert.ToInt16(dt.Rows[0]["IdUsuario"]);
                    usuario.Correo = dt.Rows[0]["Email"].ToString();
                    usuario.Nombre = dt.Rows[0]["NombreCompleto"].ToString();
                    usuario.NombreUsu = dt.Rows[0]["UserName"].ToString();
                    usuario.Contraseña = dt.Rows[0]["Password"].ToString();
                    usuario.IdPerfil = Convert.ToInt16(dt.Rows[0]["IdPerfil"].ToString());
                    if (Convert.ToBoolean(dt.Rows[0]["Verificado"]))
                    {
                        usuario.Verified = "Verificado";
                    }
                    else
                    {
                        usuario.Verified = "No Verificado";
                    }
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
                update = "Update Usuario set Verificado = '1' where Usuario.Email = '" + u.Correo + "'";
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
                

                update = "Update Usuario set Email = '" + u.Correo + "',NombreCompleto  = '" + u.Nombre + 
                    "',UserName = '" + u.NombreUsu + "',Password = '" + u.Contraseña + "' where Usuario.IdUsuario ="+u.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }
        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarUserAdmin(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                bool verifid = false;

                if (u.Verified == "Verificado")
                {
                    verifid = true;
                }

                update = "Update Usuario set Email = '" + u.Correo + "',NombreCompleto  = '" + u.Nombre +
                    "',UserName = '" + u.NombreUsu + "' ,Verificado = " + verifid + " where Usuario.IdUsuario =" + u.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de actualizar el usuario si sufre algun intento fallido de iniciar sesion
         **/

        public void establecerIntentos(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update Usuario set Intentos = " + u.Intentos + " where Usuario.IdUsuario =" + u.ID;
                nueva_conexion.SetQuery(update);

                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }
        /**
         * Se encarga de actualizar el usuario con el fin de bloquer a este.
         **/

        public void bloquearUsuario(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                string nombreParam = "@fechaBloqueo";

                update = "Update Usuario set Verificado = " + false + ",FechaIntentoIngreso = "+ nombreParam +" where Usuario.IdUsuario =" + u.ID;
                nueva_conexion.SetQuery(update);
                nueva_conexion.addParameter(nombreParam, u.FechaBloqueo);

                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }
        /**
         * Se encarga de actualizar el usuario con el fin de bloquer a este.
         **/

        public void RestablecerContraseña(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update Usuario set Password = '" + u.Contraseña + "' where Usuario.IdUsuario =" + u.ID;
                nueva_conexion.SetQuery(update);

                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public string miPerfil(User_EN u)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select NombrePerfil from Perfil where IdPerfil=" + u.IdPerfil);
            DataTable dt = nueva_conexion.QuerySeleccion();

            string nomPerfil = "";
            if (dt != null)
            {
                nomPerfil = dt.Rows[0]["NombrePerfil"].ToString();
            }

            return nomPerfil;
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public ArrayList MostrarPerfiles()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select * from Perfil");
            DataTable dt = nueva_conexion.QuerySeleccion();

            string nomPerfil = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                nomPerfil = dt.Rows[i]["NombrePerfil"].ToString();

                lista.Add(nomPerfil);
            }

            return lista;
        }


        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public int getIdPerfil(string u)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select IdPerfil from Perfil where NombrePerfil='" + u + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();

            int idPerfil = 0;
            if (dt != null)
            {
                idPerfil = Convert.ToInt32(dt.Rows[0]["IdPerfil"].ToString());
            }

            return idPerfil;
        }


    }
}
