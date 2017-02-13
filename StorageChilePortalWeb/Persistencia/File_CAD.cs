using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using Constants_namespace;
using File_EN_Class;

namespace File_CAD_Class
{
    public class File_CAD
    {
        ArrayList lista = new ArrayList();

        /**
         * Se encarga de mostrar los datos de un archivo al que le pasamos el id de ese archivo
         **/ 
        public ArrayList MostrarFile(File_EN f)
        {   
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            c.Open();
            SqlCommand com = new SqlCommand("Select * from Files where ID=" + f.ID, c);
            SqlDataReader dr = com.ExecuteReader();


            if (dr.Read())
            {
                File_EN archivo = new File_EN();
                archivo.ID = dr.GetInt32(0);
                archivo.Nombre = (dr["name"].ToString());
                archivo.Descripcion = (dr["description"].ToString());
                archivo.Fecha_creacion = Convert.ToDateTime(dr["creation_date"]);
                archivo.Propietario = Convert.ToByte(dr["owner"]);

                lista.Add(archivo);

            }
            dr.Close();
            c.Close();

            return lista;
        }

        /**
         * Se encarga de buscar en la base de datos,a través del id del archivo,si se encuentra o no en la base de datos
         **/ 
        public bool BuscarFile(File_EN f)
        {
            bool encontrado = false;

            SqlConnection nueva_conexion = new SqlConnection(Constants.nombreConexion);

            try
            {
                nueva_conexion.Open();
                string select = "";
                select = "Select id from Files where Files.ID =" + f.ID;
                SqlCommand com = new SqlCommand(select, nueva_conexion);
                SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    encontrado = true;
                }

                dr.Close();


            }

            catch (Exception ex) { }
            finally { nueva_conexion.Close(); }

            return encontrado;
        }

        /**
         * Se encarga de subir el archivo y meterlo en la base de datos
         **/ 
        public int SubirFile(File_EN f)
        {
            SqlConnection nueva_conexion = new SqlConnection(Constants.nombreConexion);
            int id=0;
            try
            {
                nueva_conexion.Open();
                f.Fecha_creacion= DateTime.Now;
                string ahora = f.Fecha_creacion.ToString("yyyy/MM/dd HH:mm:ss");
                string insert = "";
                insert = "Insert Into Files(name,description,creation_date,owner) VALUES ('";
                insert += f.Nombre + "','" + f.Descripcion + "','"+ahora+"','" + f.Propietario + "')";
                
                SqlCommand com = new SqlCommand(insert, nueva_conexion);
                
                com.ExecuteNonQuery();
                string select = "select ID from Files where name='" + f.Nombre + "' and owner='" +
                    f.Propietario + "' order by ID desc";
                com = new SqlCommand(select, nueva_conexion);
                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                    f.ID = id;
                    newVersion(f);
                }
            }
            catch (Exception ex) { }
            finally { nueva_conexion.Close(); }
            return id;
        }

        /**
         * Se encarga de mostrar todos los archivos de un usuario en el que tenga el profile_visibility a 1
         **/
        public ArrayList MostrarTodosArchivos(File_EN f)
        {
            
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "Select * from Files inner join Users on Files.owner = Users.ID where  profile_visibility = '1' order by creation_date ASC";
                SqlCommand com = new SqlCommand(select, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    File_EN archivo = new File_EN();
                    archivo.Nombre = dr["name"].ToString();
                    archivo.Descripcion = dr["description"].ToString();
                    archivo.Fecha_creacion = Convert.ToDateTime(dr["creation_date"]);
                    archivo.Propietario = Convert.ToByte(dr["owner"]);
                    lista.Add(archivo);
                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally { c.Close(); }

            return lista;

        }

        public ArrayList BuscarArchivos(File_EN f)
        {
            lista = new ArrayList();
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "Select * from files where name like '%" + f.Nombre + "%'";
                SqlCommand com = new SqlCommand(select, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    File_EN archivo = new File_EN();
                    archivo.Nombre = dr["name"].ToString();
                    archivo.Descripcion = dr["description"].ToString();
                    archivo.Fecha_creacion = Convert.ToDateTime(dr["creation_date"]);
                    archivo.Propietario = Convert.ToByte(dr["owner"]);
                    lista.Add(archivo);
                }
            }
            catch (Exception ex) { }
            finally { c.Close(); }

            return lista;
        }

        /**
         * Se encarga de mostrar los datos de un archivo a traves del id del owner
         **/ 
        public ArrayList MostrarDatosArchivo(File_EN f)
        {     
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "Select * from Files where Files.owner=" + f.Propietario;
                SqlCommand com = new SqlCommand(select, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    File_EN archivo = new File_EN();
                    archivo.Nombre = dr["name"].ToString();
                    archivo.Descripcion = dr["description"].ToString();
                    archivo.Fecha_creacion = Convert.ToDateTime(dr["creation_date"]);
                    archivo.Propietario = Convert.ToByte(dr["owner"]);
                    lista.Add(archivo);
                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally { c.Close(); }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los archivos de un usuario
         **/ 
        public ArrayList MostrarFilesUsuarioNombre(int propietario)
        {
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "Select * from Files,Versions where owner = " + propietario + " and Files.ID=Versions.Files1 order by ID,num_version";
                SqlCommand com = new SqlCommand(select, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    File_EN archivo = new File_EN();
                    archivo.Version = Convert.ToInt16(dr["num_version"]);
                    archivo.ID = Convert.ToInt16(dr["ID"]);
                    archivo.Nombre = dr["name"].ToString();
                    archivo.Descripcion = dr["description"].ToString();
                    archivo.Fecha_creacion = Convert.ToDateTime(dr["creation_date"]);
                    archivo.Propietario = Convert.ToByte(dr["owner"]);
                    lista.Add(archivo);
                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally { c.Close(); }

            return lista;

        }

        /**
         * Se encarga de borrar el archivo de la base de datos
         **/ 
        public void BorrarFile(File_EN f)
        {
            SqlConnection nueva_conexion = new SqlConnection(Constants.nombreConexion);

            try
            {
                nueva_conexion.Open();
                string delete = "";
                delete = "Delete from Files where Files.ID = " + f.ID;
                SqlCommand com = new SqlCommand(delete, nueva_conexion);


                com.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { nueva_conexion.Close(); }
        }

        /**
         * Se encarga de mostrar las versiones que tiene un archivo
         **/ 

        public int newVersion(File_EN fe)
        {
            int Version=1;
            int ID_Principal;
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "Select ID,num_version from Files,Versions where name = '" + fe.Nombre +
                    "' and files1=ID order by num_version desc";
                SqlCommand com = new SqlCommand(select, c); //se ordena del reves
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())  // solamente queremos el primero para saber la ultima version subida
                {
                    Version+= Convert.ToInt16(dr["num_version"]);
                    ID_Principal= Convert.ToInt16(dr["ID"]);
                }
                else
                    ID_Principal = fe.ID; //version se mantiene a 1 y file1=file2=ID_Principal en la primera version
                
                dr.Close();
                string insert = "";
                insert = "Insert Into Versions(files1,files2,num_version) VALUES ('";
                insert += ID_Principal + "','" + fe.ID + "','" + Version + "')";
                com = new SqlCommand(insert, c);
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { c.Close(); }


            return Version;
        }

        /**
         * Se encarga de mostrar los likes que tiene asociada cada archivo
         **/ 
        public ArrayList showLikes(File_EN f)
        {
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            try
            {
                c.Open();
                string select = "select nombre,name, count(*) as lik from Likes,Files,Users where " +
                    "Users.ID=Files.owner and Files.ID=Likes.files group by Files.name, Files.ID, nombre order by lik desc;";
                SqlCommand com = new SqlCommand(select, c);
                SqlDataReader dr = com.ExecuteReader();
                int cont = 0;
                while (cont++<10 && dr.Read())
                {
                    File_EN archivo = new File_EN();                
                    archivo.ID = Convert.ToInt16(dr["Lik"]);
                    archivo.Nombre = dr["name"].ToString();
                    archivo.Descripcion = dr["nombre"].ToString();
                    lista.Add(archivo);
                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally { c.Close(); }

            return lista;
        }

    }
}
