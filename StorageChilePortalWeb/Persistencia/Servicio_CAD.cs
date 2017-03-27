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
    public class Servicio_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarServicio(Servicio_EN s)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into Servicio(NombreServicio) VALUES ('"
                    + s.Nombre + "')";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarServicioEmpresa(Empresa_EN e, List<Servicio_EN> ls)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {

                foreach (Servicio_EN s in ls)
                {
                    string nombreParam1 = "@fechaInicio";
                    string nombreParam2 = "@fechaTermino";
                    string insert = "insert into ServicioEmpresa(IdEmpresa,IdServicio,EstadoServicio, FechaInicio, FechaTermino) VALUES ('"
                        + e.ID + "','" + s.ID + "'," + s.Verified + "," + nombreParam1 + "," + nombreParam2 + ")";
                    //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                    nueva_conexion.SetQuery(insert);
                    nueva_conexion.addParameter(nombreParam1, s.FechaInicio);
                    nueva_conexion.addParameter(nombreParam2, s.FechaTermino);
                    nueva_conexion.EjecutarQuery();
                    nueva_conexion.clearParameter();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public ArrayList MostrarServicioEmpresa(Empresa_EN e)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select * from Empresa where IdEmpresa=" + e.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                Empresa_EN empresa = new Empresa_EN();
                empresa.ID = Convert.ToInt16(dt.Rows[0]["IdEmpresa"]);
                empresa.Correo = dt.Rows[0]["CorreoEmpresa"].ToString();
                empresa.NombreEmp = dt.Rows[0]["NombreEmpresa"].ToString();
                empresa.Rut = dt.Rows[0]["RutEmpresa"].ToString();
                empresa.LogoEmpresa = (byte[])dt.Rows[0]["Foto"];
                lista.Add(empresa);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarServiciosEmpresas(Empresa_EN e)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    "from Servicio s, ServicioEmpresa se"+
                                    "where se.IdEmpresa =" + e.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Servicio_EN servicio = new Servicio_EN();
                servicio.ID = Convert.ToInt16(dt.Rows[i]["IdServicio"]);
                servicio.Nombre = dt.Rows[i]["NombreServicio"].ToString();
                servicio.Verified = Convert.ToBoolean(dt.Rows[i]["EstadoServicio"]);
                servicio.FechaInicio = DateTime.Parse(dt.Rows[i]["FechaInicio"].ToString());
                servicio.FechaTermino = DateTime.Parse(dt.Rows[i]["FechaTermino"].ToString());
                lista.Add(servicio);

            }

            return lista;
            
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public List<Servicio_EN> MostrarServicios()
        {
            List<Servicio_EN> ls = new List<Servicio_EN>();
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    "from Servicio");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Servicio_EN servicio = new Servicio_EN();
                servicio.ID = Convert.ToInt16(dt.Rows[i]["IdServicio"]);
                servicio.Nombre = dt.Rows[i]["NombreServicio"].ToString();
                ls.Add(servicio);
            }

            return ls;

        }

        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarEmpresa(string nombreServicio)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Servicio where Servicio.NombreServicio = '" + nombreServicio + "'";
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
        public Empresa_EN BuscarEmpresa(string busqueda)
        {
            Empresa_EN empresa = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Empresa where NombreEmpresa ='" + busqueda + "' or CorreoEmpresa = '" + busqueda + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    empresa = new Empresa_EN();
                    empresa.ID = Convert.ToInt16(dt.Rows[0]["IdEmpresa"]);
                    empresa.Correo = dt.Rows[0]["CorreoEmpresa"].ToString();
                    empresa.NombreEmp = dt.Rows[0]["NombreEmpresa"].ToString();
                    empresa.Rut = dt.Rows[0]["RutEmpresa"].ToString();
                    empresa.LogoEmpresa = (byte[])dt.Rows[0]["Foto"];
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return empresa;
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/ 
         
        public void actualizarEmpresa(Empresa_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                

                update = "Update Empresa set CorreoEmpresa = '" + e.Correo + "',NombreEmpresa  = '" + e.NombreEmp +
                    "',Foto = '" + e.LogoEmpresa + "'' where Empresa.IdEmpresa =" + e.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public DataTable MostrarServiciosEmpresas()
        {
            Conexion nueva_conexion = new Conexion();

            nueva_conexion.SetQuery("Select e.NombreEmpresa, e.RutEmpresa, e.CorreoEmpresa, e.Foto,"+
                                    "(SELECT CASE WHEN servicioempresa.EstadoServicio <> 0 THEN \"Activado\" ELSE \"No Activado\" END FROM servicioempresa, servicio WHERE servicio.IdServicio = servicioempresa.IdServicio AND servicioempresa.IdEmpresa = e.IdEmpresa AND servicio.NombreServicio = 'Bodega') AS 'Bodega'," +
                                    "(SELECT CASE WHEN servicioempresa.EstadoServicio <> 0 THEN \"Activado\" ELSE \"No Activado\" END FROM servicioempresa, servicio WHERE servicio.IdServicio = servicioempresa.IdServicio AND servicioempresa.IdEmpresa = e.IdEmpresa AND servicio.NombreServicio = 'Almacen') AS 'Almacén'," +
                                    "(SELECT CASE WHEN servicioempresa.EstadoServicio <> 0 THEN \"Activado\" ELSE \"No Activado\" END FROM servicioempresa, servicio WHERE servicio.IdServicio = servicioempresa.IdServicio AND servicioempresa.IdEmpresa = e.IdEmpresa AND servicio.NombreServicio = 'Digitalizacion') AS 'Digitalización'" +
                                    "from Empresa e, Servicio s , ServicioEmpresa se "+
                                    "where e.idEmpresa = se.IdEmpresa and s.IdServicio = se.IdServicio "+
                                    "GROUP by e.NombreEmpresa");
            DataTable dt = nueva_conexion.QuerySeleccion();
            return dt;
        }



        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public bool bajarServiciosEmpresa(Empresa_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update ServicioEmpresa set EstadoServicio = " + false +
                    " where ServicioEmpresa.IdEmpresa =" + e.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
                return true;
            }
            catch (Exception ex) { ex.Message.ToString(); return false; }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

    }
}
