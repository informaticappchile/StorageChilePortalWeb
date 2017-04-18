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
    public class Opciones_CAD
    {
        public byte[] getCrypto()
        {
            Opciones_EN opcion = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Opciones";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    opcion = new Opciones_EN();
                    opcion.Contraseña = (byte[])dt.Rows[0]["Clave"];
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return opcion.Contraseña;
        }
        public string getMensaje()
        {
            Opciones_EN opcion = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Opciones";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    opcion = new Opciones_EN();
                    opcion.Mensaje = Convert.ToString(dt.Rows[0]["Mensaje"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return opcion.Mensaje;
        }
        public DateTime getFecha()
        {
            Opciones_EN opcion = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Opciones";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    opcion = new Opciones_EN();
                    opcion.FechaMantenimiento = Convert.ToDateTime(dt.Rows[0]["Fecha_Mantenimiento"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return opcion.FechaMantenimiento;
        }
        public DateTime getFechaTermino()
        {
            Opciones_EN opcion = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Opciones";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    opcion = new Opciones_EN();
                    opcion.FechaTerminoMantenimiento = Convert.ToDateTime(dt.Rows[0]["Fecha_Termino"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return opcion.FechaTerminoMantenimiento;
        }
        public bool getMantenimiento()
        {
            Opciones_EN opcion = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Opciones";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    opcion = new Opciones_EN();
                    opcion.EstadoMantenimiento = Convert.ToBoolean(dt.Rows[0]["Estado_Mantenimiento"]);
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return opcion.EstadoMantenimiento;
        }
    }
}
