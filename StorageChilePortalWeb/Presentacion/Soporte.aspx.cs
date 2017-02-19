using System;
using Entidades;
using System.Net.Mail;
using System.Net;
using System.Web.Services;
using System.Text;

namespace Presentacion
{
    public partial class Soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user_session_data"] = null; //Cuando vas a registrarte, si se habia iniciado sesion, ahora se cierra
            /*if(Session["user_session_data"] == null){//Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Debe iniciar sesión");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                Response.Redirect("Control_Usuarios/Login.aspx");
            }*/


        }

        protected static string ReCaptcha_Key = "<6LfZ-RUUAAAAAGrnxFF7Z4LCovzUAdbNyLMeboFz>";
        protected static string ReCaptcha_Secret = "<6LfZ-RUUAAAAAPQDIsUqplPc3FGA0Bik4IyQ_dZh>";

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
                MailAddress fromAddress = new MailAddress("informaticapp.chile@gmail.com");//Gmail, creado para el envio de correos
                //MailAddress toAddress = new MailAddress(correo_register.Text);//El destinatario
                message.From = fromAddress;
                //message.To.Add(toAddress);
                message.Subject = "Activacion de la cuenta";//El asunto del email

                //string userActiviation = Request.Url.GetLeftPart(UriPartial.Authority) + "/Control_Usuarios/ConfirmacionRegistro.aspx?email=" + correo_register.Text;//La direccion url que debe ser recargada para la activacion de la cuenta

               // message.Body = "Hi " + user_name_register.Text + "<br> click here to confirm your account</br> <a href = " + userActiviation + "> click Here </a>";//Donde debe hacer click el nuevo usuario para activarla
                message.IsBodyHtml = true;//El mensaje esta en html
                //smtpClient.UseDefaultCredentials = true;

                smtpClient.Credentials = new System.Net.NetworkCredential("informaticapp.chile@gmail.com", "3#rtG&00$1MnPdj!");//Los credenciales del cliente
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
            /*EmailExistsError_Register.Visible = 
            UsernameExistsError_Register.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            User_EN busqueda = new User_EN();
            if (busqueda.BuscarUsuario(user_name_register.Text) == null) //Comprobamos que ese nombre de usuario ya este
            {
                if (busqueda.BuscarUsuario(correo_register.Text) == null) //Comprobamos que ese correo ya este
                {
                    User_EN en = new User_EN();//Si lo cumple todo, creamos un nuevo usuario
                    en.NombreUsu = user_name_register.Text;//Con su nombre de usuario
                    en.Correo = correo_register.Text;//Con su correo
                    en.Contraseña = password_register1.Text;//Con su contrasenya
                    en.InsertarUsuario();//Llamamos a InsertarUsuario de la cap EN, que se encaragra de insertarlo
                    EnviarCorreoConfirmacion();//Esto enviara un correo de confirmaacion al usuario
                }
                else EmailExistsError_Register.Visible = true;
            }
            else UsernameExistsError_Register.Visible = true;*/
            Response.Redirect("Inicio.aspx");

        }
    }
}