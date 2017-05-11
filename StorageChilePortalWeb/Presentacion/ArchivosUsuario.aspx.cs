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
using Logica;
using System.Configuration;

namespace Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<Button> botones = new List<Button>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaUsuario lu = new LogicaUsuario();
            User_EN userAutoLog = lu.BuscarUsuario("jbravo", "Usuario");
            Session["user_session_data"] = userAutoLog;
            User_EN en = (User_EN)Session["user_session_data"];
            if (en != null)
            {
                cargaCarpetas(true,null);
            }
            else
            {
                Response.Redirect("Control_Usuarios/Login.aspx");
            }

            if (Session["lista_archivos"] == null)
            {
                CargarListaArchivos();
                Arbol a = (Arbol)Session["lista_archivos"];
                NodoArbol n = a.Raiz;
                a.RecorridoPostOrden(ref n);
                ArbolR.Text = a.Resultado;
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
                Image icono = (Image)e.Row.FindControl("icono_fichero");
                icono.ImageUrl = "~/img/PDF-48.png";
                
                /*Texto_Descarga.NavigateUrl = Server.MapPath(rutaArchivo); //Copiamos la ruta del archivo a la URL para descargar
            */
            }
        }

        protected void Responsive_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DOWNLOAD")
            {
                User_EN en = (User_EN)Session["user_session_data"];
                try
                {
                    DownloadFileFromFtp(e.CommandArgument.ToString());

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
        }

        /// <summary>
        /// Download file with http response
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="localPath"></param>
        public byte[] DownloadFileFromFtp(string fileUrl)
        {
            
            User_EN user = (User_EN)Session["user_session_data"];

            StringBuilder filesstring = new StringBuilder();
            WebResponse webResponse = null;
            try
            {
                string FileSaveUri = ConfigurationManager.AppSettings["ftp"];
                string uri = user.NombreEmp + "/" + Convert.ToString(Session["carpeta"]) + "/" + fileUrl;

                FtpWebRequest ftpRequest = ConnectToFtp(FileSaveUri + "/" + uri, WebRequestMethods.Ftp.DownloadFile);
                ftpRequest.UseBinary = true;
                webResponse = ftpRequest.GetResponse();

                FtpWebResponse response = (FtpWebResponse)webResponse;
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename="+ fileUrl);
                Stream dfileResponseStream = response.GetResponseStream();
                int Length = 1024;

                byte[] buffer = new byte[Length];
                List<byte> filebytes = new List<byte>();
                int bytesRead = dfileResponseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        for (int i = 0; i < bytesRead; i++)
                            filebytes.Add(buffer[i]);

                        Response.OutputStream.Write(buffer, 0, bytesRead);
                        bytesRead = dfileResponseStream.Read(buffer, 0, Length);
                    }else
                    {
                        bytesRead = -1;
                    }
                }

                response.Close();
                if (bytesRead == -1)
                {
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Error, se ha perdido la conexión con el servidor. Intente más tarde.");
                    //Cerramos el Script
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    return null;
                }
                return filebytes.ToArray();

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
                return null;
            }
            finally
            {
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }
        }

        /// <summary>
        /// Connect to ftp on specfic Url and related method
        /// </summary>
        /// <param name="SpecificPathOnFtpUrl"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public FtpWebRequest ConnectToFtp(string SpecificPathOnFtpUrl, string Method)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(SpecificPathOnFtpUrl));
            ftpRequest.UseBinary = true;
            ftpRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftp_user"], ConfigurationManager.AppSettings["ftp_password"]);
            ftpRequest.Method = Method;

            ftpRequest.Proxy = null;
            ftpRequest.KeepAlive = false;
            ftpRequest.UsePassive = false;
            return ftpRequest;
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

        protected void cargaCarpetas(bool tipoCarga, List<string> carpetas)
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
                    Button button = new Button();
                    button.ID = ObtenerNombre(line);
                    button.Text = ObtenerNombre(line);
                    if (botones.Count >= contador && !tipoCarga)
                    {
                        cargaCarpetasFiltradas(carpetas);
                        break;
                    }
                    else
                    {
                        button.CssClass = "button-folder";
                    }
                    if (!isPunto(button.Text))
                    {
                        button.Click += new EventHandler(button_Click);
                        container.Controls.Add(button);
                    }
                    botones.Add(button);
                    line = streamReader.ReadLine();
                    contador++;
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

        protected void cargaCarpetasFiltradas(List<string> carpetas)
        {
            foreach (Button button in botones)
            {
                if (buscarCarpeta(button.ID, carpetas))
                {
                    button.CssClass = "button-folder-filtrado";
                }
                else
                {
                    button.CssClass = "button-folder";
                }
            }
        }

        protected void limpiarCarpetasFiltradas()
        {
            foreach (Button button in botones)
            {
                button.CssClass = "button-folder";
            }
        }

        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Session["carpeta"] = button.Text;
            txtFiltro.Visible = true;
            buscar_Rut.Visible = false;
            Button1.Visible = true;
            container.Visible = false;
            GridViewMostrarArchivos.Visible = true;
            botonesPie.Visible = true;
            cargaArchivos();
        }



        protected void Button_Volver_Click(object sender, EventArgs e)
        {
            txtFiltro.Visible = false;
            buscar_Rut.Visible = true;
            Button1.Visible = true;
            container.Visible = true;
            GridViewMostrarArchivos.Visible = false;
            botonesPie.Visible = false;
            //limpiarCarpetasFiltradas();
        }

        protected void cargaArchivos()
        {
            User_EN en = (User_EN)Session["user_session_data"];
            try
            {
                LogicaFile la = new LogicaFile();
                LogicaEmpresa le = new LogicaEmpresa();
                Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
                ArrayList dt = la.MostrarFIles(Convert.ToString(Session["carpeta"]),em);
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

        protected void filtrarArchivo(bool verificado)
        {
            User_EN en = (User_EN)Session["user_session_data"];
            try
            {
                string rutCompleto = txtFiltro.Text + "-" + digitoVerificador(txtFiltro.Text);
                LogicaPersonal lp = new LogicaPersonal();
                Personal_EN p = lp.BuscarPersonal(rutCompleto);
                LogicaFile la = new LogicaFile();
                LogicaEmpresa le = new LogicaEmpresa();
                Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
                ArrayList dt = la.MostrarArchivosFiltrados(p, Convert.ToString(Session["carpeta"]), em, verificado);
                GridViewMostrarArchivos.DataSource = dt;
                GridViewMostrarArchivos.DataBind();
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


        protected void Button_Buscar_Click(object sender, EventArgs e)
        {
            if (GridViewMostrarArchivos.Visible)
            {
                if (txtFiltro.Text == "")
                {
                    filtrarArchivo(false);
                }
                else
                {
                    filtrarArchivo(true);
                }
            }
            else
            {
                limpiarCarpetasFiltradas();
                User_EN en = (User_EN)Session["user_session_data"];
                LogicaFile lf = new LogicaFile();
                LogicaEmpresa le = new LogicaEmpresa();
                string rutCompleto = buscar_Rut.Text + "-" + digitoVerificador(buscar_Rut.Text);
                List<string> carpetas = lf.MostrarArchivosFiltrados(rutCompleto, le.BuscarEmpresa(en.NombreEmp));
                cargaCarpetas(false, carpetas);
            }
        }

        protected bool buscarCarpeta(string carpeta, List<string> carpetas)
        {
            if (carpetas.Contains(carpeta)) {
                return true;
            }
            return false;
        }

        public string digitoVerificador(string rut)
        {
            int suma = 0;
            for (int x = rut.Length - 1; x >= 0; x--)
                suma += int.Parse(char.IsDigit(rut[x]) ? rut[x].ToString() : "0") * (((rut.Length - (x + 1)) % 6) + 2);
            int numericDigito = (11 - suma % 11);
            string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
            return digito;
        }

        public void CargarListaArchivos()
        {
            User_EN en = (User_EN)Session["user_session_data"];
            Arbol arbol = new Arbol(en.NombreEmp, true);
            try
            {
                NodoArbol padre = arbol.Raiz;
                string dir = padre.Nombre + "/";
                List<NodoArbol> lista = null;
                CargarListaArchivos(ref padre, dir, ref arbol);
                lista = padre.Hijos;
                CargarListaArchivos(ref lista, dir, arbol);
                Session["lista_archivos"] = arbol;
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

        public void CargarListaArchivos(ref List<NodoArbol> lista, string dir, Arbol arbol)
        {

            foreach (NodoArbol aux in lista)
            {
                NodoArbol padre = aux;
                if (padre.EsCarpeta)
                {
                    CargarListaArchivos(ref padre, dir + padre.Nombre + "/", ref arbol);
                    List<NodoArbol> list = aux.Hijos;
                    CargarListaArchivos(ref list, dir + padre.Nombre + "/", arbol);
                }
               
            }

        }

        public void CargarListaArchivos(ref NodoArbol padre, string dir, ref Arbol arbol)
        {
            string FileSaveUri = ConfigurationManager.AppSettings["ftp"] + dir;
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
                bool isCarpeta = esCarpeta(nombre);
                arbol.Insertar(nombre, isCarpeta, ref padre);
                line = streamReader.ReadLine();
                contador++;
            }
            streamReader.Close();
        }

        public bool esCarpeta(string nombre)
        {
            if(System.IO.Path.GetExtension(nombre).ToLower() == ".pdf")
            {
                return false;
            }
            return true;
        }

    }
}