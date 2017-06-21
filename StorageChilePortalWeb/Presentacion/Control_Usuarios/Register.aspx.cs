using System;
using Entidades;
using Logica;
using System.Net.Mail;
using System.Web.Services;
using System.Net;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_session_admin"] == null)
            {//Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene los privilegios para acceder aquí. Debe iniciar sesión");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            User_EN en = (User_EN)Session["user_session_admin"];

            LogicaUsuario lu = new LogicaUsuario();
            LogicaEmpresa le = new LogicaEmpresa();
            ArrayList lista = le.MostrarEmpresas();
            if (lista.Count == 0)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene empresas disponibles en el sistema. Por favor registre una.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Register_Empresa.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            if (!IsPostBack)
            {
                DropDownList1.DataSource = lista;
                DropDownList1.DataTextField = "NombreEmp";
                DropDownList1.DataValueField = "NombreEmp";
                DropDownList1.DataBind();
                lista = lu.MostrarPerfiles();
                DropDownList2.DataSource = lista;
                DropDownList2.DataBind();
            }
            
        }

        /*
         * Una vez el usuario se ha introducido con éxito en la base de datos procedemos a 
         * enviarle el email de confirmacion
         */
        protected void EnviarCorreoConfirmacion()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");//Creamos el cliente
            smtpClient.Port = 587;//El puerto
            MailMessage message = new MailMessage();//Cremos el menaseje que ahora rellenamos
            try
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["correo_soporte"]);//Gmail, creado para el envio de correos
                MailAddress toAddress = new MailAddress(correo_register.Text);//El destinatario
                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = "Activación de la cuenta";//El asunto del email

                string userActiviation = Request.Url.GetLeftPart(UriPartial.Authority) + "/Control_Usuarios/ConfirmacionRegistro.aspx?email=" + correo_register.Text;//La direccion url que debe ser recargada para la activacion de la cuenta

                message.Body = "Estimado, " + user_name_register.Text + "<br> Bienvenido al portal web de Storage Chile.<br>Haga click aquí para confirmar tu cuenta</br> <a href = " +
                    userActiviation + "> click Here </a> <br>. Una vez confirmada su cuenta, se le enviará un correo" +
                    " con sus datos.<br>En caso de presentar algún problema, póngase en contacto con nuestro equipo de soporte: informaticapp.soporte@gmail.com";//Donde debe hacer click el nuevo usuario para activarla
                message.IsBodyHtml = true;//El mensaje esta en html
                //smtpClient.UseDefaultCredentials = true;

                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["correo_soporte"], ConfigurationManager.AppSettings["clave_soporte"]);//Los credenciales del cliente
                smtpClient.EnableSsl = true;//necesario para el envio
                smtpClient.Send(message);//Lo enviamos
                //Response.Write("Correcto email");
            }
            catch (Exception ex)
            {
                //Response.Write("Incorrecto email");
                Response.Write(ex.GetBaseException());
                //Label1.Text = "No se pudo enviar el mensaje!";
                //e.GetBaseExceptio();
            }
        }

        /* Una vez el usuario ha rellenado todos los campos solicitados en el apartado del registro
         * correctamente, es decir, el email tiene formato de email, las contraseñas coinciden...proceemos a
         * guardar el usuario en la base de datos
         */
        protected void Button_Register_Click(object sender, EventArgs e)
        {
            LogicaOpciones lo = new LogicaOpciones();
            byte[] salt = lo.getCrypto();
            EmailExistsError_Register.Visible = 
            UsernameExistsError_Register.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            User_EN busqueda = new User_EN();
            LogicaUsuario lu = new LogicaUsuario();
            if (lu.BuscarUsuario(user_name_register.Text, "Usuario").NombreUsu != user_name_register.Text ) //Comprobamos que ese nombre de usuario ya este
            {
                if (lu.BuscarUsuario(correo_register.Text, "Usuario").Correo != correo_register.Text) //Comprobamos que ese correo ya este
                {
                    LogicaEmpresa le = new LogicaEmpresa();
                    Empresa_EN em = le.BuscarEmpresa(DropDownList1.Text);
                    User_EN en = new User_EN();//Si lo cumple todo, creamos un nuevo usuario
                    en.NombreUsu = user_name_register.Text;//Con su nombre de usuario
                    en.Correo = correo_register.Text;//Con su correo
                    string password = password_register1.Text;
                    en.Contraseña = Crypto.Hash(salt, password);
                    en.IdEmpresa = em.ID;
                    en.IdPerfil = lu.getIdPerfil(DropDownList2.Text);
                    lu.InsertarUsuario(en);//Llamamos a InsertarUsuario de la cap EN, que se encaragra de insertarlo
                    EnviarCorreoConfirmacion();//Esto enviara un correo de confirmaacion al usuario
                    User_EN u = lu.BuscarUsuario(en.NombreUsu, "Usuario");
                    if (validarRegistroUsuario(u))
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "Se ha registrado al usuario: "+ en.NombreUsu);
                        //Cerramos el Script
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                        limpiar(this.Controls);
                    }
                    else
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "Ha ocurrido un error al registrar el usuario, reintente mas tarde o comuníquese con el servicio de soporte.");
                        //Cerramos el Script
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    }
                }
                else EmailExistsError_Register.Visible = true;
            }
            else UsernameExistsError_Register.Visible = true;

        }

        private bool validarRegistroUsuario(User_EN u)
        {
            if (u != null || u.NombreUsu != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static string ReCaptcha_Key = "<" + ConfigurationManager.AppSettings["ReCaptcha_Key"] + ">";
        protected static string ReCaptcha_Secret = "<" + ConfigurationManager.AppSettings["ReCaptcha_Secret"] + ">";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        private void mensajePermisoUsuarioError(bool verificado)
        {
            if (verificado)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene privilegios de super administrador para acceder aqui.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());

            }
        }

        private void limpiar(ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    text.Text = "";
                }
                else if (ctrl.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(ctrl.Controls);
            }
        }
    }
}