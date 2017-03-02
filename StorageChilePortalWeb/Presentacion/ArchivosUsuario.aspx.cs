using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Data.SqlClient;
using System.Web.ClientServices;
using System.IO;
using System.Text;
using System.Collections;
using System.Net;
using System.Data;

namespace Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User_EN en = (User_EN)Session["user_session_data"];
            if (en != null)
            {
                try
                {
                    string FileSaveUri = @"ftp://ftp.Smarterasp.net/"+en.NombreEmp+"/";
                    string ftpUser = "cvaras";
                    string ftpPassWord = "cvaras1234";
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(FileSaveUri);
                    ftpRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    DataTable dt = new DataTable("Archivos");
                    dt.Columns.Add(new DataColumn("Archivo", typeof(string)));
                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        DataRow row = dt.NewRow();
                        row["Archivo"] = ObtenerNombre(line);
                        //Añadimos la fila a la tabla
                        dt.Rows.Add(row);
                        line = streamReader.ReadLine();
                    }
                    streamReader.Close();
                    // if (Request.QueryString.Count > 0)
                    // {
                    //     if (Request.QueryString.Keys[0] == "ID")

                    //en.LeerUsuario();  //lee todos los datos del usuario de la base de datos, ya que la pagina solo proporciona login y password

                    File_EN fi = new File_EN();
                    fi.Propietario = en.ID;//Para identificar al usuario
                                           //EL griedView, mostrara un tabla con todos los datos que nos devuelva MostrarFilesUsuarioNombreEn
                                           //GridViewMostrarArchivos.DataSource = fi.MostrarFilesUsuarioNombreEn();

                    GridViewMostrarArchivos.DataSource = dt;
                    GridViewMostrarArchivos.DataBind();


                    //}
                }
                catch (Exception ex)
                {
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Error de conexión con el servidor, intente más tarde.");
                    //Cerramos el Script
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
            }
            else
            {
                Response.Redirect("Control_Usuarios/Login.aspx");
            }
        }

        /*
         * Esta función sirve para controlar los datos de la tabla y poder acceder
         * a los datos de los archivos para ser descargados o borrados
         */
        protected void GridViewMostrarArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Texto_Descarga = (HyperLink)e.Row.FindControl("Descarga"); //Creamos el link para la descargar
                HyperLink Texto_Borra = (HyperLink)e.Row.FindControl("Borra"); //Creamos el link para borrar el archivo
                User_EN en = (User_EN)Session["user_session_data"];
                string rutaArchivo = "Files/" + en.ID + "/" + HttpUtility.HtmlDecode(e.Row.Cells[1].Text); /*Guardamos la ruta del archivo
                utilizando HttpUtility.HtmlDecode  que sirve para decodificar carácteres especiales (&, ñ, etc.)*/
                string idArchivo = e.Row.Cells[1].Text; //Guardamos el id del archivo
                /*
                 * Así buscamos y encontramos un icono de miniatura para el fichero en función de la extensión del archivo
                 */
                Image icono = (Image)e.Row.FindControl("icono_fichero");
                string extensionArchivo = Path.GetExtension(rutaArchivo);
                if (extensionArchivo.Length != 0)
                {
                    extensionArchivo = extensionArchivo.Substring(1, extensionArchivo.Length - 1); //quitar punto (carácter 0 del string)
                    string rutaIcono = Server.MapPath("/styles/format-icons/" + extensionArchivo + ".svg");
                    icono.ImageUrl = File.Exists(rutaIcono) ? "~/styles/format-icons/" + extensionArchivo + ".svg" : "~/styles/format-icons/file.svg";
                }
                else
                {
                    extensionArchivo = "folder"; 
                    string rutaIcono = Server.MapPath("/styles/format-icons/" + extensionArchivo + ".svg");
                    icono.ImageUrl = File.Exists(rutaIcono) ? "~/styles/format-icons/" + extensionArchivo + ".svg" : "~/styles/format-icons/file.svg";
                }
                

                /*Texto_Descarga.NavigateUrl = Server.MapPath(rutaArchivo); //Copiamos la ruta del archivo a la URL para descargar
                Texto_Borra.NavigateUrl = Server.MapPath(rutaArchivo); //Lo mismo para borrar
                Texto_Borra.Text = idArchivo; //Guardamos el id asociado al archivo para cuando llamemos al manejador de la funcion podamos extraerlo del objeto emisor de la señal
            */
            }
        }
        /*
         * Esta funcion esta conectada al boton para borrar el archivo
         */
        protected void Borrar_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender; //Creamos un linkButton con el objeto emisor(sender)
            HyperLink h = (HyperLink)lb.FindControl("Borra"); //Conectamos el linkButton al link de borrar
            string rutaborra = h.NavigateUrl;
            File_EN f_bbdd = new File_EN();
            f_bbdd.ID = Convert.ToInt32(h.Text);
            /*
             * Creamos una variable de tipo FileInfo con la ruta del archivo a borrar, 
             * que se utiliza para proporcionar métodos
             * y propiedades para borrar entre otros
             */
            FileInfo file = new FileInfo(rutaborra); 

            if (file.Exists)
            {
                file.Delete(); //Borramos el archivo de la carpeta de nuestro proyecto
                //f_bbdd.BorrarArchivo(); //Borramos el archivo de la base de datos
                Response.Redirect(Request.Url.AbsoluteUri); //Recarga página para refrescar los datos
            }
        }
        /*
         * Esta funcion esta conectada al boton de descargar
         */
        protected void Descarga_Boton_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            HyperLink h = (HyperLink)lb.FindControl("Descarga");
            string rutaDescarga = h.NavigateUrl;
            FileInfo fi = new FileInfo(rutaDescarga);
            if (fi.Exists)
            {
                Response.Clear(); //Limpiamos la salida
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fi.Name + "/"); 
                Response.AddHeader("Content-Length", fi.Length.ToString());
                Response.ContentType = "application/octet-stream"; // Con esto le decimos al browser que la salida sera descargable
                Response.Flush(); // volcamos el stream 
                Response.TransmitFile(fi.FullName);
                Response.End(); // Enviamos todo el encabezado ahora
            }
        }

        protected string ObtenerNombre(string nombre)
        {
            string n = "";
            char[] c = nombre.ToCharArray();
            for(int i = c.Length-1; i >= 0; i--)
            {
                if (c[i] != ' ')
                {
                    n = n + c[i].ToString();
                }else
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