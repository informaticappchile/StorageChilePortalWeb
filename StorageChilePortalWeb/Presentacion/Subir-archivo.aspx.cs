using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Net;
using System.Data.SqlClient;
using Logica;
using System.ComponentModel;
using System.Text;
using System.Configuration;

namespace Presentacion
{
    public partial class SubirArchivo : System.Web.UI.Page
    {
        public int progresoBar1 = 0;
        public List<string> carpetas = new List<string>();
        public List<string> subCarpetas = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitInputClasses();
            User_EN en = (User_EN)Session["user_session_data"];
            if (en != null)
            {
                //en.LeerUsuario();  //lee todos los datos del usuario de la base de datos, ya que la pagina solo proporciona login y password
                MostrarDirectorio(en); //Muestra todo el directorio
            }
            else
            {
                Response.Redirect("Control_Usuarios/Login.aspx");
            }
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=LinkButton1]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void MostrarDirectorio(User_EN en)
        {

        }

        protected void FileUpload1_DataBinding(object sender, EventArgs e)
        {

        }
        

        /*
        * Este método esta conectado al boton de subir archivo
        */
        protected void Button_Upload_Click(object sender, EventArgs e)
        {
            User_EN en = (User_EN)Session["user_session_data"];
            LogicaUsuario lu = new LogicaUsuario();
            String path = Server.MapPath("Files/"); //Ruta donde subir el archivo (en la carpeta "Files" de nuestro proyecto)

            string FileSaveUri = ConfigurationManager.AppSettings["ftp"];

            string ftpUser = ConfigurationManager.AppSettings["ftp_user"];
            string ftpPassWord = ConfigurationManager.AppSettings["ftp_password"];
            Stream requestStream = null;
            Stream fileStream = null;
            FtpWebResponse uploadResponse = null;
            string carpeta = contenedor_sa_inpu.Text + "/";
            string subCarpeta = subcontenedor_sa_inpu.Text + "/";
            string empresa = lu.BuscarUsuario(en.NombreUsu, "Usuario").NombreEmp + "/";

            if (FileUpload1.PostedFile != null)
            {
                HttpPostedFile uploadFile = FileUpload1.PostedFile;
                if (System.IO.Path.GetExtension(uploadFile.FileName).ToLower() == ".pdf")
                {
                    Personal_EN pe = new Personal_EN();
                    File_EN fe = new File_EN();
                    LogicaPersonal lp = new LogicaPersonal();
                    LogicaFile lf = new LogicaFile();

                    pe.Nombre = nombre_sa_input.Text;
                    pe.Rut = rut_sa_input.Text;
                    fe.Ubicacion = ubicacion_sa_inpu.Text;
                    if (subcontenedor_sa_inpu.Text != "")
                    {
                        fe.CarpetaAsociado = contenedor_sa_inpu.Text + "/" + subcontenedor_sa_inpu.Text;
                    }
                    else
                    {
                        fe.CarpetaAsociado = contenedor_sa_inpu.Text;
                    }
                    fe.ArchivoAsociado = uploadFile.FileName;
                    lp.InsertarPersonal(pe);
                    pe = lp.BuscarPersonal(rut_sa_input.Text);
                    LogicaEmpresa le = new LogicaEmpresa();
                    fe.IDPersonal = pe.ID;
                    fe.IDUsuario = en.ID;
                    lp.InsertarPersonalEmpresa(pe.ID, le.BuscarEmpresa(en.NombreEmp).ID);
                    lf.InsertarArchivo(fe);
                    try
                    {
                        User_EN user = (User_EN)Session["user_session_data"];
                        if (user != null)
                        {
                            string strFileName = Path.GetFileName(uploadFile.FileName);
                            int FileLength = FileUpload1.PostedFile.ContentLength;
                            Uri uri = new Uri(FileSaveUri + empresa + carpeta + Path.GetFileName(FileUpload1.PostedFile.FileName));

                            try
                            {
                                cargaCarpetas();
                                if (!existCarpeta(contenedor_sa_inpu.Text))
                                {
                                    crearCarpeta(carpeta, FileSaveUri + en.NombreEmp + "/", ftpUser, ftpPassWord);
                                }
                                else
                                {
                                    cargaSubCarpetas(carpeta);
                                    if (subCarpetas.Count == 0)
                                    {
                                        throw new System.ArgumentException("Parameter cannot be null", "original");
                                    }
                                }
                                if (subcontenedor_sa_inpu.Text != "")
                                {
                                    crearCarpeta(subCarpeta, FileSaveUri + en.NombreEmp + "/" + carpeta, ftpUser, ftpPassWord);
                                    uri = new Uri(FileSaveUri + empresa + carpeta + subCarpeta + Path.GetFileName(FileUpload1.PostedFile.FileName));
                                }
                                FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uri);
                                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                                uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                                requestStream = uploadRequest.GetRequestStream();
                                fileStream = FileUpload1.PostedFile.InputStream;
                                byte[] buffer = new byte[1024];
                                double total = (double)FileLength;
                                int byteRead = 0;
                                double read = 0;
                                do
                                {
                                    byteRead = FileUpload1.PostedFile.InputStream.Read(buffer, 0, 1024);
                                    requestStream.Write(buffer, 0, byteRead);
                                    read += (double)byteRead;
                                    double percentage = read / total * 100;
                                    progresoBar1 = (int)percentage;
                                } while (byteRead != 0);
                                fileStream.Close();
                                requestStream.Close();
                                uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
                                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                StringBuilder sbMensaje = new StringBuilder();
                                //Aperturamos la escritura de Javascript
                                sbMensaje.Append("<script type='text/javascript'>");
                                //Le indicamos al alert que mensaje va mostrar
                                sbMensaje.AppendFormat("alert('{0}');", "Se ha subido el archivo correctamente");
                                //Cerramos el Script
                                sbMensaje.Append("</script>");
                                //Registramos el Script escrito en el StringBuilder
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                                //uploadFile.SaveAs(MapPath(a));
                            }
                            catch (Exception ex1)
                            {
                                Response.Write("Error de conexión con el servidor, el contenedor ya existe o el contenedor contiene archivos.");
                            }
                        }
                        else
                            Response.Write("Error. usuario no válido");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("El archivo no se puede subir.");
                    }
                        
                }
                else
                {

                }
            }
            else
            {

            }
        }

        protected void crearCarpeta(string carpeta, string uri, string ftpUser, string ftpPassWord)
        {
            WebRequest request = WebRequest.Create(uri + carpeta);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }

        protected string ObtenerNombre(string nombre)
        {
            string n = "";
            char[] c = nombre.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)
            {
                if (c[i] != ' ')
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
        protected bool isPunto(string nombre)
        {
            char[] c = nombre.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)
            {
                if (c[i] == '.')
                {
                    return true;
                }
            }
            return false;
        }

        protected string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        protected void cargaCarpetas()
        {
            User_EN en = (User_EN)Session["user_session_data"];
            try
            {
                string FileSaveUri = ConfigurationManager.AppSettings["ftp"] + en.NombreEmp + "/";
                string ftpUser = ConfigurationManager.AppSettings["ftp_user"];
                string ftpPassWord = ConfigurationManager.AppSettings["ftp_password"];
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(FileSaveUri);
                ftpRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string line = streamReader.ReadLine();
                int contador = 1;
                while (!string.IsNullOrEmpty(line))
                {
                    string nombre = ObtenerNombre(line);
                    if (isPunto(nombre))
                    {
                        break;
                    }
                    else
                    {
                        carpetas.Add(nombre);
                        line = streamReader.ReadLine();
                        contador++;
                    }
                }
                streamReader.Close();
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

        protected void cargaSubCarpetas(string carpeta)
        {
            User_EN en = (User_EN)Session["user_session_data"];
            try
            {
                string FileSaveUri = ConfigurationManager.AppSettings["ftp"] + en.NombreEmp + "/" + carpeta;
                string ftpUser = ConfigurationManager.AppSettings["ftp_user"];
                string ftpPassWord = ConfigurationManager.AppSettings["ftp_password"];
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(FileSaveUri);
                ftpRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string line = streamReader.ReadLine();
                int contador = 1;
                while (!string.IsNullOrEmpty(line))
                {
                    string nombre = ObtenerNombre(line);
                    if (isPunto(nombre))
                    {
                        break;
                    }
                    else
                    {
                        subCarpetas.Add(nombre);
                        line = streamReader.ReadLine();
                        contador++;
                    }
                }
                streamReader.Close();
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

        protected bool existCarpeta(string carpeta)
        {
            foreach (string aux in carpetas)
            {
                if (aux == carpeta)
                {
                    return true;
                }
            }
            return false;
        }
    }
}