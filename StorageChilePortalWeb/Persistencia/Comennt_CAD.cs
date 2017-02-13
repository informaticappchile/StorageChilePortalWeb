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
using Comennt_EN_Class;

namespace Comennt_CAD_Class
{
    public class Comennt_CAD
    {
        private const string s = "data source=.\\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\\BBDD.mdf;User Instance=true";
        ArrayList lista = new ArrayList();

        public void InsertComennt(Comentario_EN c)
        {
            SqlConnection nueva_conexion = new SqlConnection(Constants.nombreConexion);

            try
            {
                nueva_conexion.Open();
                string insert = "";
                insert = "Insert Into CommentsContent(User,File,Content) VALUES (" + c.User + "," + c.File + ",'" + c.Content + "')";
                SqlCommand com = new SqlCommand(insert, nueva_conexion);

                com.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { nueva_conexion.Close(); }
        }

        public void DeleteComennt(Comentario_EN c)
        {
            SqlConnection nueva_conexion = new SqlConnection(Constants.nombreConexion);

            try
            {
                nueva_conexion.Open();
                string delete = "";
                delete = "Delete from Files where CommentsContent.User = " + c.User + "AND CommentsContent.File" + c.File + "AND CommentsContent.Content" + c.Content;
                SqlCommand com = new SqlCommand(delete, nueva_conexion);


                com.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { nueva_conexion.Close(); }
        }

        public ArrayList ShowComennts(Comentario_EN comentario)
        {
            SqlConnection c = new SqlConnection(Constants.nombreConexion);
            c.Open();
            SqlCommand com = new SqlCommand("Select Content from CommentsContent WHERE User=" + comentario.User + " AND File=" + comentario.File, c);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["Content"].ToString());
            }
            dr.Close();
            c.Close();

            return lista;
        }

    }
}
