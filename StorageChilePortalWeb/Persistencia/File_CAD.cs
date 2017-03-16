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
                con.SetQuery("SELECT * from archivo a, personal p, personalempresa pe where a.IdPersonal = p.idPersonal and pe.idEmpresa =" + emp.ID + 
                    " and pe.idPersonal = p.idPersonal"+ 
                    " Order by p.NombrePersonal");
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

        /**
         * Se encarga de mostrar los datos de un archivo al que le pasamos el id de ese archivo
         **/
        public List<string> MostrarArchivosFiltrados(string rut, Empresa_EN emp)
        {
            List<string> carpetas = new List<string>();
            Conexion con = new Conexion();
            try
            {
                con.SetQuery("SELECT a.Ruta from archivo a, personal p, personalempresa pe where a.IdPersonal = p.idPersonal and pe.idEmpresa =" + emp.ID +
                    " and pe.idPersonal = p.idPersonal and p.RutPersonal = '" + rut + "'");
                DataTable dt = con.QuerySeleccion();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string folder = ObtenerCarpeta(dt.Rows[i]["Ruta"].ToString());
                    carpetas.Add(folder);
                }

            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { con.Cerrar_Conexion(); }
            return carpetas;

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

        public void InsertarArchivo(File_EN f)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {

                string insert = "insert into Archivo(Ruta,IdPersonal,IdUsuario) VALUES ('" + f.CarpetaAsociado + "/"+f.ArchivoAsociado + "'," +f.IDPersonal + "," +f.IDUsuario + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

    }
}
