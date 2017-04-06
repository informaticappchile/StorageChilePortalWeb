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
    }
}
