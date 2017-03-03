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
    public class File_CAD
    {
        ArrayList lista = new ArrayList();

        /**
         * Se encarga de mostrar los datos de un archivo al que le pasamos el id de ese archivo
         **/
        public ArrayList MostrarFiles()
        {
            Conexion con = new Conexion();
            try
            {
                con.SetQuery("SELECT * from archivo a, personal p where archivo.IdPersonal = personal.idPersonal");
                DataTable dt = con.QuerySeleccion();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    File_EN archivo = new File_EN();
                    archivo.IDArchivo = Convert.ToInt16(dt.Rows[i]["IdArchivo"].ToString());
                    archivo.Ruta = dt.Rows[i]["Ruta"].ToString();
                    archivo.NombreAsociado = dt.Rows[i]["NombrePersonal"].ToString();
                    archivo.RutAsociado = dt.Rows[i]["RutPersonal"].ToString();
                    lista.Add(archivo);

                }

                

            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { con.Cerrar_Conexion(); }
            return lista;

        }

    }
}
