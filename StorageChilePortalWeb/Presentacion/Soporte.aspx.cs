using System;
using Entidades;
using System.Net.Mail;
using System.Net;
using System.Web.Services;
using System.Text;
using System.Configuration;

namespace Presentacion
{
    public partial class Soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_session_data"] == null)
            {//Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Debe iniciar sesión");
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
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
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["correo_master"]);//Gmail, creado para el envio de correos
                MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["correo_soporte"]);//El destinatario
                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = "Consulta de Soporte";//El asunto del email

                //string userActiviation = Request.Url.GetLeftPart(UriPartial.Authority) + "/Control_Usuarios/ConfirmacionRegistro.aspx?email=" + correo_register.Text;//La direccion url que debe ser recargada para la activacion de la cuenta

                message.Body = "Hola haz recibido una consulta de " + correo_register.Text + "<br> " + TextBox1.Text + "</br>";//Donde debe hacer click el nuevo usuario para activarla
                message.IsBodyHtml = true;//El mensaje esta en html
                //smtpClient.UseDefaultCredentials = true;

                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["correo_master"], ConfigurationManager.AppSettings["clave_master"]);//Los credenciales del cliente
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
                  
            EnviarCorreoConfirmacion();//Esto enviara un correo de confirmaacion al usuario
                                       //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
            StringBuilder sbMensaje = new StringBuilder();
            //Aperturamos la escritura de Javascript
            sbMensaje.Append("<script type='text/javascript'>");
            //Le indicamos al alert que mensaje va mostrar
            sbMensaje.AppendFormat("alert('{0}');", "Se ha enviado el correo a nuestro equipo de soporte. En breves momentos nuestro equipo notificará su solicitud. Que tenga un buen día. ");
            sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
            //Cerramos el Script
            sbMensaje.Append("</script>");
            //Registramos el Script escrito en el StringBuilder
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());


        }
    }
}