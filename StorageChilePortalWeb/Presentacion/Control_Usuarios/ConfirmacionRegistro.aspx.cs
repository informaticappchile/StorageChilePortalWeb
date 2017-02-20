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

namespace Presentacion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.Keys[0] == "email")//Si efecticamente la url es correcta
                {
                    User_EN en = new User_EN();//Creamos un nuevo usuario
                    string email = Request.QueryString["email"].ToString();//Gracias a la url, podemos ver el usuario que ha recargado la pagina
                    en.Correo = email;//Ahora que ese usuario sea el del email
                    LogicaUsuario lu = new LogicaUsuario();
                    lu.confirmacionUsuario(en);//Confirmacion 
                    User_EN u = new User_EN();
                    u = lu.BuscarUsuario(en.Correo);

                    if (ValidarConfirmacionCorreo(u))
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "Se ha confirmado el correo correctamente, ahora actualice sus datos.");
                        //Cerramos el Script
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());


                        Session["user_session_data"] = u; //Creamos una sesion del usuario
                        Response.Redirect("~/Control_Usuarios/Editar_Perfil.aspx");
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
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    }

                    

                }
            }
        }


        private bool ValidarConfirmacionCorreo(User_EN u)
        {
            if (u.Verified)
            {
                return true;
            }
            else {
                return false;
            }
            
        }
    }
}