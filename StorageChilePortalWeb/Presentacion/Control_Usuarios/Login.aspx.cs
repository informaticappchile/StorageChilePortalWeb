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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user_session_data"] = null; //Cuando vas a iniciar sesion, si se habia iniciado sesion, ahora se cierra

            Session["user_session_admin"] = null;
        }

        /*
         * Esta funcion esta conectada al boton de iniciar sesion 
         */ 
        protected void Button_Login_Click(object sender, EventArgs e)
        {
            UserNotVerifiedError_Login.Visible = 
            WrongPasswordError_Login.Visible =
            UserNotExistsError_Login.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            User_EN busqueda = new User_EN();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN usuario = lu.BuscarUsuario(username_login_input.Text, "Usuario");//Buscamos el usuario que introducimos para iniciar sesion
            if (usuario != null && usuario.NombreUsu != "")
            {
                ingresoUsuario(usuario, "Usuario");
            }
            else
            {
                usuario = lu.BuscarUsuario(username_login_input.Text, "Administrador");//Buscamos el usuario que introducimos para iniciar sesion
                ingresoUsuario(usuario, "Administrador");
                UserNotExistsError_Login.Visible = true;
            }
        }

        /*
         * Una vez el usuario se ha introducido con éxito en la base de datos procedemos a 
         * enviarle el email de confirmacion
         */
        protected void EnviarCorreoBloqueo(User_EN u)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");//Creamos el cliente
            smtpClient.Port = 587;//El puerto
            MailMessage message = new MailMessage();//Cremos el menaseje que ahora rellenamos
            try
            {
                MailAddress fromAddress = new MailAddress("informaticapp.soporte@gmail.com");//Gmail, creado para el envio de correos
                MailAddress toAddress = new MailAddress(u.Correo);//El destinatario
                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = "Bloqueo de la cuenta";//El asunto del email

                string userActiviation = Request.Url.GetLeftPart(UriPartial.Authority) + "/Control_Usuarios/ConfirmacionRegistro.aspx?email=" + u.Correo;//La direccion url que debe ser recargada para la activacion de la cuenta

                message.Body = "Estimado " + u.NombreUsu + ",<br> Se ha detectado una serie de intento de ingresos erróneos en tu cuenta con fecha " + u.FechaBloqueo + ".</br>"+
                    "<br> Si haz sido tú, haz click en el siguente enlace </br> <a href = "
                    + userActiviation + "> click Aquí para restablecer tu cuenta </a>"+ "<br> En el caso contrario contáctese con nuestro equipo de soporte mediante el siguiente correo: informaticapp.soporte@gmail.com</br>";//Donde debe hacer click el nuevo usuario para activarla
                message.IsBodyHtml = true;//El mensaje esta en html
                //smtpClient.UseDefaultCredentials = true;

                smtpClient.Credentials = new System.Net.NetworkCredential("informaticapp.soporte@gmail.com", "InfoChile2625");//Los credenciales del cliente
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
       
        private void ingresoUsuario(User_EN usuario, string tabla)
        {
            LogicaUsuario lu = new LogicaUsuario();
            LogicaOpciones lo = new LogicaOpciones();
            byte[] salt = lo.getCrypto();
            if (usuario.Verified == "Verificado")
            {
                if (Crypto.ComparePassword(password_login_input.Text,salt, usuario.Contraseña))
                {
                    if (tabla == "Administrador")
                    {
                        Session["user_session_admin"] = usuario; //Creamos una sesion del usuario
                        Response.Redirect("~/Inicio.aspx"); //Vamos a la pagina de nuestros archivos
                    }
                    else
                    {
                        Session["user_session_data"] = usuario; //Creamos una sesion del usuario
                        Response.Redirect("~/Inicio.aspx"); //Vamos a la pagina de nuestros archivos
                    }
                }
                else
                {
                    usuario.Intentos = usuario.Intentos + 1;
                    lu.establecerIntento(usuario);
                    if (usuario.Intentos == 3)
                    {
                        usuario.FechaBloqueo = DateTime.Now;
                        usuario.Intentos = 0;
                        lu.establecerIntento(usuario);
                        BlockUser_Login.Visible = true;
                        username_login_input.Enabled = false;
                        password_login_input.Enabled = false;
                        lu.bloquearUsuario(usuario);
                        EnviarCorreoBloqueo(usuario);
                    }
                    else
                    {
                        WrongPasswordError_Login.Visible = true;
                        WrongPasswordError_Login.Text = "Contraseña Incorrecta haz realizado " + usuario.Intentos
                            + " de 3 intento(s). Si no recuerdas tu contraseña haz click en \"¿Olvidó su contraseña?\".";
                    }
                }
            }
            else UserNotVerifiedError_Login.Visible = true;
        }
    }
}