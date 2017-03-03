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

namespace Presentacion
{
    public partial class SubirArchivo : System.Web.UI.Page
    {
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

            if (IsPostBack)
            {
                User_EN en = (User_EN)Session["user_session_data"];
                LogicaUsuario lu = new LogicaUsuario();
                String path = Server.MapPath("Files/"); //Ruta donde subir el archivo (en la carpeta "Files" de nuestro proyecto)

                string FileSaveUri = @"ftp://ftp.Smarterasp.net/";

                string ftpUser = "cvaras";
                string ftpPassWord = "cvaras1234";
                Stream requestStream = null;
                Stream fileStream = null;
                FtpWebResponse uploadResponse = null;
                string carpeta = contenedor_sa_inpu.Text + "/";
                string empresa = lu.BuscarUsuario(en.NombreUsu).NombreEmp + "/";

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
                        fe.CarpetaAsociado = contenedor_sa_inpu.Text;
                        fe.ArchivoAsociado = uploadFile.FileName;
                        lp.InsertarPersonal(pe);
                        pe = lp.BuscarPersonal(rut_sa_input.Text);

                        fe.IDPersonal = pe.ID;
                        fe.IDUsuario = en.ID;

                        lf.InsertarArchivo(fe);

                        try
                        {
                            User_EN user = (User_EN)Session["user_session_data"];
                            if (user != null)
                            {
                                string strFileName = Path.GetFileName(uploadFile.FileName);
                                string a = ("h:/root/home/infochile-001/www/Storage/uploadpdf/" + strFileName);
                                int FileLength = FileUpload1.PostedFile.ContentLength;
                                Uri uri = new Uri(FileSaveUri + empresa + carpeta + Path.GetFileName(FileUpload1.PostedFile.FileName));
                                
                                try
                                {
                                    FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uri);
                                    uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                                    uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                                    requestStream = uploadRequest.GetRequestStream();
                                    byte[] buffer = new byte[FileLength];
                                    fileStream = FileUpload1.PostedFile.InputStream;
                                    fileStream.Read(buffer, 0, FileLength);
                                    requestStream.Write(buffer, 0, FileLength);
                                    requestStream.Close();
                                    uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
                                    //uploadFile.SaveAs(MapPath(a));
                                
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        crearCarpeta(carpeta, FileSaveUri+en.NombreEmp+"/", ftpUser, ftpPassWord);
                                        FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uri);
                                        uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                                        uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                                        requestStream = uploadRequest.GetRequestStream();
                                        byte[] buffer = new byte[FileLength];
                                        fileStream = FileUpload1.PostedFile.InputStream;
                                        fileStream.Read(buffer, 0, FileLength);
                                        requestStream.Write(buffer, 0, FileLength);
                                        requestStream.Close();
                                        uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
                                        //uploadFile.SaveAs(MapPath(a));
                                    }
                                    catch (Exception ex1)
                                    {
                                        Response.Write("Error de conexión con el servidor.");
                                    }
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
    }
}