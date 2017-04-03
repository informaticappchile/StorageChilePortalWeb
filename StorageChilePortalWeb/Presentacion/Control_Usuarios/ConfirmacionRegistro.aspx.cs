using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Entidades;
using Logica;
using System.Text;
using System.Net.Mail;

namespace Presentacion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user_session_data"] = null;
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.Keys[0] == "email")//Si efecticamente la url es correcta
                {
                    try
                    {


                        LogicaUsuario lu = new LogicaUsuario();
                        User_EN u = new User_EN();
                        User_EN en = new User_EN();//Creamos un nuevo usuario
                        string email = Request.QueryString["email"].ToString();//Gracias a la url, podemos ver el usuario que ha recargado la pagina
                        en.Correo = email;//Ahora que ese usuario sea el del email
                        u = lu.BuscarUsuario(en.Correo, "Usuario");
                        if (ValidarConfirmacionCorreo(u))
                        {

                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Su correo ya esta confirmado.");
                            //Cerramos el Script
                            sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                            tbValidacion.Text = "1";
                        }
                        else
                        {
                            try
                            {
                                lu.confirmacionUsuario(en);//Confirmacion 
                                u = lu.BuscarUsuario(en.Correo, "Usuario");
                                Session["user_session_data"] = u;
                                if (ValidarConfirmacionCorreo(u))
                                {
                                    EnviarCorreoConfirmacion(u);
                                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                    StringBuilder sbMensaje = new StringBuilder();
                                    //Aperturamos la escritura de Javascript
                                    sbMensaje.Append("<script type='text/javascript'>");
                                    //Le indicamos al alert que mensaje va mostrar
                                    sbMensaje.AppendFormat("alert('{0}');", "Se ha confirmado el correo correctamente, ahora actualice sus datos en editar perfil.");
                                    //Cerramos el Script
                                    sbMensaje.Append("window.location.href = \"Editar_Perfil.aspx\";");
                                    sbMensaje.Append("</script>");
                                    //Registramos el Script escrito en el StringBuilder
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                                    //Creamos una sesion del usuario
                                    tbValidacion.Text = "1";
                                }
                                else
                                {
                                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                    StringBuilder sbMensaje = new StringBuilder();
                                    //Aperturamos la escritura de Javascript
                                    sbMensaje.Append("<script type='text/javascript'>");
                                    //Le indicamos al alert que mensaje va mostrar
                                    sbMensaje.AppendFormat("alert('{0}');", "Hubo un error al confirmar su correo, inténtelo más tarde o consulte a soporte.");
                                    //Cerramos el Script
                                    sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                                    sbMensaje.Append("</script>");
                                    //Registramos el Script escrito en el StringBuilder
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                                    tbValidacion.Text = "";
                                }
                            }
                            catch
                            {
                                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                StringBuilder sbMensaje = new StringBuilder();
                                //Aperturamos la escritura de Javascript
                                sbMensaje.Append("<script type='text/javascript'>");
                                //Le indicamos al alert que mensaje va mostrar
                                sbMensaje.AppendFormat("alert('{0}');", "Hubo un error Critico 2 al confirmar su correo, inténtelo más tarde o consulte a soporte.");
                                //Cerramos el Script
                                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                                sbMensaje.Append("</script>");
                                //Registramos el Script escrito en el StringBuilder
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                                tbValidacion.Text = "";
                            }
                            

                        }
                    }
                    catch
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "Hubo un error Critico 1 al confirmar su correo, inténtelo más tarde o consulte a soporte.");
                        //Cerramos el Script
                        sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                        tbValidacion.Text = "";

                    }
                }
            }
        }
        private bool ValidarConfirmacionCorreo(User_EN u)
        {
            if (u.Verified=="Verificado")
            {
                return true;
            }
            else {
                return false;
            }

        }

        /*
         * Una vez el usuario se ha introducido con éxito en la base de datos procedemos a 
         * enviarle el email de confirmacion
         */
        protected void EnviarCorreoConfirmacion(User_EN u)
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
                message.Subject = "Activación de la cuenta";//El asunto del email
                
                message.Body = "Estimado, " + u.NombreUsu + "<br> Bienvenido al portal web de Storage Chile. Su cuenta se encuentra"+
                    " activada felicitaciones.<br>Sus datos son: <br> Usuario: " + u.NombreUsu + "<br>Contraseña: " + u.Contraseña +
                    "<br><br>Como recomendación de seguridad guarde sus datos y elimine este correo o cambie su contraseña por favor.";//Donde debe hacer click el nuevo usuario para activarla
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
    }
}