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
        public ArrayList MostrarFiles(string carpeta, Empresa_EN emp)
        {
            Conexion con = new Conexion();
            try
            {
                con.SetQuery("SELECT * from archivo a, personal p where archivo.IdPersonal = personal.idPersonal and personal.idEmpresa =" + emp.ID + 
                    "OrderBy p.NombrePersonal");
                DataTable dt = con.QuerySeleccion();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string folder = ObtenerCarpeta(dt.Rows[i]["Ruta"].ToString());
                    if(folder == carpeta)
                    {
                        string file = ObtenerArchivo(dt.Rows[i]["Ruta"].ToString());
                        File_EN archivo = new File_EN();
                        archivo.IDArchivo = Convert.ToInt16(dt.Rows[i]["IdArchivo"].ToString());
                        archivo.ArchivoAsociado = file;
                        archivo.CarpetaAsociado = folder;
                        archivo.NombreAsociado = dt.Rows[i]["NombrePersonal"].ToString();
                        archivo.RutAsociado = dt.Rows[i]["RutPersonal"].ToString();
                        lista.Add(archivo);
                    }
                    
                }

            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { con.Cerrar_Conexion(); }
            return lista;

        }

        protected string ObtenerCarpeta(string carpeta)
        {
            string n = "";
            char[] c = carpeta.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != '/')
                {
                    n = n + c[i].ToString();
                }
                else
                {

                    return n;
                }
            }
            return n;
        }

        protected string ObtenerArchivo(string archivo)
        {
            string n = "";
            char[] c = archivo.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)
            {
                if (c[i] != '/')
                {
                    n = n + c[i].ToString();
                }
                else
                {

                    return Reverse(n);
                }
            }
            return n;
        }

        protected string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
