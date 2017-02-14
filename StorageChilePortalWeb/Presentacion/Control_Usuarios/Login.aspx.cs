using System;
using User_EN_Class;

namespace Prensentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user_session_data"] = null; //Cuando vas a iniciar sesion, si se habia iniciado sesion, ahora se cierra
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
            User_EN usuario = busqueda.BuscarUsuario(username_login_input.Text); //Buscamos el usuario que introducimos para iniciar sesion
            if (usuario != null)
            {
                if (usuario.Contraseña == password_login_input.Text)
                {
                   if (usuario.Verified)
                   {
                        Session["user_session_data"] = usuario; //Creamos una sesion del usuario
                        Response.Redirect("~/ArchivosUsuario.aspx"); //Vamos a la pagina de nuestros archivos
                   }
                   else UserNotVerifiedError_Login.Visible = true;
                }
                else WrongPasswordError_Login.Visible = true;
            }
            else UserNotExistsError_Login.Visible = true;
        }
    }
}