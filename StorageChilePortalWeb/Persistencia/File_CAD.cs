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
                    " group by a.Ruta"+
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
                        archivo.Ubicacion = dt.Rows[i]["Ubicacion"].ToString();
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
        public ArrayList MostrarArchivosFiltrados(Personal_EN p, string carpeta, Empresa_EN emp, bool tipoFiltrado)
        {
            List<string> carpetas = new List<string>();
            Conexion con = new Conexion();
            try
            {
                if (!tipoFiltrado)
                {
                    return MostrarFiles(carpeta, emp);
                }
                else
                {
                    con.SetQuery("SELECT a.Ruta, a.IdArchivo, a.Ubicacion " +
                    "from archivo a, personal p, personalempresa pe "+
                    "where a.IdPersonal = p.idPersonal and pe.idEmpresa =" + emp.ID + " and p.RutPersonal = '" +p.Rut+"'"+
                    " and pe.idPersonal = p.idPersonal "+
                    " group by a.Ruta" +
                    " Order by p.NombrePersonal");
                }
                DataTable dt = con.QuerySeleccion();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string folder = ObtenerCarpeta(dt.Rows[i]["Ruta"].ToString());
                    if (folder == carpeta)
                    {
                        string file = ObtenerArchivo(dt.Rows[i]["Ruta"].ToString());
                        File_EN archivo = new File_EN();
                        archivo.IDArchivo = Convert.ToInt16(dt.Rows[i]["IdArchivo"].ToString());
                        archivo.ArchivoAsociado = file;
                        archivo.CarpetaAsociado = folder;
                        archivo.NombreAsociado = p.Nombre;
                        archivo.RutAsociado = p.Rut;
                        archivo.Ubicacion = dt.Rows[i]["Ubicacion"].ToString();
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
            bool v = false;
            char[] c = carpeta.ToCharArray();
            for (int i = c.Length-1; i >= 0; i--)
            {
                if (v)
                {
                    n = n + c[i].ToString();
                }
                else if(c[i] == '/')
                {
                    v = true;
                }
            }
            return Reverse(n);
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
                string insert = "insert into Archivo(Ruta,IdPersonal,IdUsuario,Ubicacion) VALUES ('" + f.CarpetaAsociado + 
                    "/"+f.ArchivoAsociado + "'," +f.IDPersonal + "," +f.IDUsuario + ",'"+ f.Ubicacion + "')";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }


        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarArchivo(int id)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Archivo where Archivo.IdArchivo = " + id + "";
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
        public File_EN BuscarArchivo(string ruta, Personal_EN p)
        {
            File_EN archivo = null;
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select * from Archivo where IdPersonal =" + p.ID + " and Ruta = '" + ruta + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    string folder = ObtenerCarpeta(dt.Rows[0]["Ruta"].ToString());
                    string file = ObtenerArchivo(dt.Rows[0]["Ruta"].ToString());
                    archivo = new File_EN();
                    archivo.IDArchivo = Convert.ToInt16(dt.Rows[0]["IdArchivo"].ToString());
                    archivo.ArchivoAsociado = file;
                    archivo.CarpetaAsociado = folder;
                    archivo.NombreAsociado = p.Nombre;
                    archivo.RutAsociado = p.Rut;
                    archivo.Ubicacion = dt.Rows[0]["Ubicacion"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return archivo;
        }
    }
}
