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
    public class Empresa_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarEmpresa(Empresa_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string nombreParam1 = "@foto";
                string nombreParam2 = "@fechaRegistro";
                string insert = "insert into Empresa(CorreoEmpresa,NombreEmpresa,RutEmpresa, Foto, FechaRegistro) VALUES ('"
                    + e.Correo + "','" + e.NombreEmp + "','" + e.Rut + "'," + nombreParam1 + "," + nombreParam2 + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.addParameter(nombreParam1, e.LogoEmpresa);
                nueva_conexion.addParameter(nombreParam2, e.FechaRegistro);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */ 
         
        public ArrayList MostrarEmpresa(Empresa_EN e)
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
                empresa.FechaRegistro = DateTime.Parse(dt.Rows[0]["FechaRegistro"].ToString());
                lista.Add(empresa);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarEmpresas()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    "from Empresa e");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Empresa_EN empresa = new Empresa_EN();
                empresa.NombreEmp = dt.Rows[i]["NombreEmpresa"].ToString();
                empresa.Rut = dt.Rows[i]["RutEmpresa"].ToString();
                empresa.Correo = dt.Rows[i]["CorreoEmpresa"].ToString();
                empresa.LogoEmpresa = (byte[])dt.Rows[0]["Foto"];
                empresa.FechaRegistro = DateTime.Parse(dt.Rows[0]["FechaRegistro"].ToString());
                lista.Add(empresa);

            }

            return lista;
            
        }
        
        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarEmpresa(string nombreEmpresa)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Empresa where Empresa.NombreEmpresa = '" + nombreEmpresa + "'";
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
                    empresa.FechaRegistro = DateTime.Parse(dt.Rows[0]["FechaRegistro"].ToString());
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
                string nombreParam1 = "@foto";
                string update = "";
                

                update = "Update Empresa set CorreoEmpresa = '" + e.Correo + "',NombreEmpresa  = '" + e.NombreEmp +
                    "',Foto = " + nombreParam1 + " where Empresa.IdEmpresa =" + e.ID;
                nueva_conexion.SetQuery(update);
                nueva_conexion.addParameter(nombreParam1, e.LogoEmpresa);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

    }
}
