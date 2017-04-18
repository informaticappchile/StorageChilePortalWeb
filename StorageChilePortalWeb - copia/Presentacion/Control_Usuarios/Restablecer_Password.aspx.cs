using System;
using Entidades;
using Logica;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace Presentacion
{
    public partial class Restablecer_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user_session_data"] = null; //Cuando vas a iniciar sesion, si se habia iniciado sesion, ahora se cierra
        }

        /*
         * Esta funcion esta conectada al boton de iniciar sesion 
         */ 
        protected void Button_RP_Click(object sender, EventArgs e)
        {
            WrongUserError_RP.Visible = false;
            User_EN busqueda = new User_EN();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN usuario = null;
            if (username_rp_input.Text != "") 
            {
                usuario = lu.BuscarUsuario(username_rp_input.Text, "Usuario");//Buscamos el usuario que introducimos para restableser contraseña
            }
            else if (correo_rp_input.Text != "")
            {
                usuario = lu.BuscarUsuario(correo_rp_input.Text, "Usuario");
            }

            if (usuario != null)
            {
                LogicaOpciones lo = new LogicaOpciones();
                byte[] salt = lo.getCrypto();
                string password = GenerarPass(8, 12);
                usuario.Contraseña = Crypto.Hash(salt, password);
                lu.RestableserPassword(usuario);
                EnviarCorreoRestablecer(usuario);
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se le ha enviado un correo con los pasos para restablecer su contraseña.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            else
            {
                WrongUserError_RP.Visible = true;
            }
            
        }

        /*
         * Una vez el usuario se ha introducido con éxito en la base de datos procedemos a 
         * enviarle el email de confirmacion
         */
        protected void EnviarCorreoRestablecer(User_EN u)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");//Creamos el cliente
            smtpClient.Port = 587;//El puerto
            MailMessage message = new MailMessage();//Cremos el menaseje que ahora rellenamos
            try
            {
                LogicaOpciones lo = new LogicaOpciones();
                byte[] salt = lo.getCrypto();
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["correo_soporte"]);//Gmail, creado para el envio de correos
                MailAddress toAddress = new MailAddress(u.Correo);//El destinatario
                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = "Restablecer Contraseña";//El asunto del email

                string userActiviation = Request.Url.GetLeftPart(UriPartial.Authority) + "/Control_Usuarios/Login.aspx";//La direccion url que debe ser recargada para la activacion de la cuenta

                message.Body = "Estimado " + u.NombreUsu + ",<br> Se ha generado una nueva contraseña para que pueda iniciar sesión. Su nueva contraseña es: " + Crypto.DecrytedPassword(salt, u.Contraseña) + "</br>"+
                    "<br> Se recomienda que despues de que inicie sesión cambie esta contraseña por una propia en \"Editar Perfil\".</br> <a href = "
                    + userActiviation + "> click Aquí para iniciar sesión </a>";//Donde debe hacer click el nuevo usuario para activarla
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LongPassMin"></param>
        /// <param name="LongPassMax"></param>
        /// <returns></returns>
        protected string GenerarPass(int LongPassMin, int LongPassMax)
        {
            char[] ValueAfanumeric = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '!', '#', '$', '%', '&', '?', '¿' };
            Random ram = new Random();
            int LogitudPass = ram.Next(LongPassMin, LongPassMax);
            string Password = String.Empty;

            for (int i = 0; i < LogitudPass; i++)
            {
                int rm = ram.Next(0, 2);

                if (rm == 0)
                {
                    Password += ram.Next(0, 10);
                }
                else
                {
                    Password += ValueAfanumeric[ram.Next(0, 59)];
                }
            }

            return Password;
        }

        protected static string ReCaptcha_Key = "<6LfZ-RUUAAAAAGrnxFF7Z4LCovzUAdbNyLMeboFz>";
        protected static string ReCaptcha_Secret = "<6LfZ-RUUAAAAAPQDIsUqplPc3FGA0Bik4IyQ_dZh>";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }
    }
}