using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;

namespace Presentacion
{
    public partial class Editar_Empresa : System.Web.UI.Page
    {
        
        /**
         * Es necesario que los elementos CheckBox y RadioButton inicialicen sus tipos de clases
         * usando estos métodos, ya que si lo hacemos directamente desde el HTML el checkbox lo envuelve
         * en un <span></span> declarando la clase en esa etiqueta (y, por tanto, inhabilitando el checkbox)
         */
        protected void InitInputClasses()
        {
            Editar_Empresa_ServicioBodega_Switch.InputAttributes.Add("class", "mdl-switch__input");
            Editar_Empresa_ServicioAlmacen_Switch.InputAttributes.Add("class", "mdl-switch__input");
            Editar_Empresa_ServicioDigitalizacion_Switch.InputAttributes.Add("class", "mdl-switch__input");
        }

        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(Empresa_EN em)
        {
            Editar_Nombre_Empresa.Text = em.NombreEmp;
            Editar_Rut_Empresa.Text = em.Rut;
            Editar_Email_Empresa.Text = em.Correo;
            Editar_Perfil_ID.Text = em.ID.ToString();
            Editar_Fecha_Resgistro.Text = em.FechaRegistro.ToString();
            System.Drawing.Image img = byte_a_Image(em.LogoEmpresa);
            img.Save(Server.MapPath("~/logEmpresas/") + "logoEmp.png", System.Drawing.Imaging.ImageFormat.Png);
            preview.ImageUrl = "~/logEmpresas/logoEmp.png";
            LogicaServicio ls = new LogicaServicio();
            List<Servicio_EN> list = ls.MostrarServicioEmpresa(em);
            foreach (Servicio_EN s in list)
            {
                if (s.Nombre == "Almacen")
                {
                    Editar_Empresa_ServicioAlmacen_Switch.Checked = s.Verified;
                    if (s.Verified)
                    {
                        Editar_Empresa_ServicioAlmacen_Label.Text = "Activado";
                    }
                }
                if (s.Nombre == "Bodega")
                {
                    Editar_Empresa_ServicioBodega_Switch.Checked = s.Verified;
                    if (s.Verified)
                    {
                        Editar_Empresa_ServicioBodega_Label.Text = "Activado";
                    }
                }
                if (s.Nombre == "Digitalización")
                {
                    Editar_Empresa_ServicioDigitalizacion_Switch.Checked = s.Verified;
                    if (s.Verified)
                    {
                        Editar_Empresa_ServicioDigitalizacion_Label.Text = "Activado";
                    }
                }
            }
            /*if(en.Verified == "Verificado")
            {
                Editar_Perfil_Visibilidad_Switch.Checked = true;
                Editar_Perfil_Visibilidad_Switch.Enabled = false;
                Editar_Perfil_Visibilidad_Label.Text = "Verificado";
            }
            else
            {
                Editar_Perfil_Visibilidad_Switch.Checked = false;
            }*/

        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            LogicaEmpresa lu = new LogicaEmpresa();
            this.user = Request["ID"].ToString();
            this.en = lu.BuscarEmpresa(user);
            this.en.NombreEmp = Editar_Nombre_Empresa.Text;
            this.en.Rut = Editar_Rut_Empresa.Text;
            this.en.Correo = Editar_Email_Empresa.Text;
            if (FileUpload1.HasFile) {
                this.en.LogoEmpresa = FileUpload1.FileBytes;
            }
            LogicaServicio lse = new LogicaServicio();
            List<Servicio_EN> ls = lse.MostrarServicios();
            foreach (Servicio_EN s in ls)
            {
                if (s.Nombre == "Almacen")
                {
                    s.Verified = Editar_Empresa_ServicioAlmacen_Switch.Checked;
                }
                if (s.Nombre == "Bodega")
                {
                    s.Verified = Editar_Empresa_ServicioBodega_Switch.Checked;
                }
                if (s.Nombre == "Digitalización")
                {
                    s.Verified = Editar_Empresa_ServicioDigitalizacion_Switch.Checked;
                }
            }
            /*if (Editar_Perfil_Visibilidad_Switch.Checked)
            {
                en.Verified = "Verificado";
            }*/

            lu.actualizarEmpresa(en);
            if (ValidarCambios(en))
            {
                lse.actualizarServicioEmpresa(en, ls);
                string FileSaveUri = ConfigurationManager.AppSettings["ftp"];

                string ftpUser = ConfigurationManager.AppSettings["ftp_user"];
                string ftpPassWord = ConfigurationManager.AppSettings["ftp_password"];
                if (Editar_Empresa_ServicioBodega_Switch.Checked)
                {
                    try
                    {
                        modificarCarpeta(en.NombreEmp, FileSaveUri, ftpUser, ftpPassWord);
                    }
                    catch (Exception ex)
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje1 = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje1.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje1.AppendFormat("alert('{0}');", "Ha ocurrido un error al reservar su espacio,comuníquese con el servicio de soporte para poder habilitarlo.");
                        sbMensaje1.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/AdministrarEmpresa.aspx\";");
                        //Cerramos el Script
                        sbMensaje1.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje1.ToString());
                    }
                }
                if (Editar_Empresa_ServicioDigitalizacion_Switch.Checked && !Editar_Empresa_ServicioBodega_Switch.Checked)
                {
                    try
                    {
                        modificarCarpeta(en.NombreEmp, FileSaveUri, ftpUser, ftpPassWord);
                        crearCarpeta("Documentos", FileSaveUri + en.NombreEmp + "/", ftpUser, ftpPassWord);
                    }
                    catch (Exception ex)
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje1 = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje1.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje1.AppendFormat("alert('{0}');", "Ha ocurrido un error al reservar su espacio,comuníquese con el servicio de soporte para poder habilitarlo.");
                        sbMensaje1.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/AdministrarEmpresa.aspx\";");
                        //Cerramos el Script
                        sbMensaje1.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje1.ToString());
                    }
                }
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se actualizaron los datos correctamente");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/AdministrarEmpresa.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());

            }
            else
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error al actualizar los datos. Reintente más tarde "+
                    "o pongase en contacto con el servicio de soporte.");
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/AdministrarEmpresa.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        private string user;
        private Empresa_EN en = new Empresa_EN();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitInputClasses();
            LogicaUsuario lu = new LogicaUsuario();
            LogicaEmpresa le = new LogicaEmpresa();
            User_EN ad = (User_EN)Session["user_session_admin"];
            if (ad != null)
            {
                if (!Page.IsPostBack)
                {
                    if (Request["ID"] != null)
                    {
                        this.user = Request["ID"].ToString();
                        this.en = le.BuscarEmpresa(user);
                        CargarDatos(this.en);
                    }
                }

            }
          else
           {
                //Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Debe iniciar sesión, usted no tiene privilegios para acceder aqui");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        protected bool ValidarCambios(Empresa_EN u)
        {
            LogicaEmpresa lu = new LogicaEmpresa();
            Empresa_EN en = lu.BuscarEmpresa(u.NombreEmp);
            if (en.NombreEmp != u.NombreEmp) { return false; }
            if (en.Rut != u.Rut){return false; }
            if (en.Correo != u.Correo) { return false; }
            return true;
        }

        private System.Drawing.Image byte_a_Image(byte[] logo)
        {
            if (!(logo == null) || logo.Length > 0)
            {
                byte[] arr = File.ReadAllBytes(Server.MapPath("~/logEmpresas/") + "falabella.png");
                MemoryStream ms = new MemoryStream(logo);
                System.Drawing.Image resultado = System.Drawing.Image.FromStream(ms);
                return resultado;
            }
            else
            {
                return null;
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

        protected void modificarCarpeta(string carpeta, string uri, string ftpUser, string ftpPassWord)
        {
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create(uri + this.user);
            request.Method = WebRequestMethods.Ftp.Rename;
            request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
            request.RenameTo = carpeta;
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream respStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(respStream);
                respStream.Close();
                response.Close();
            }catch (Exception e)
            {

            }
        }
    }
}