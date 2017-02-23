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
            Session["user_session_data"] = null;
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.Keys[0] == "email")//Si efecticamente la url es correcta
                {
                    LogicaUsuario lu = new LogicaUsuario();
                    User_EN u = new User_EN();
                    User_EN en = new User_EN();//Creamos un nuevo usuario
                    string email = Request.QueryString["email"].ToString();//Gracias a la url, podemos ver el usuario que ha recargado la pagina
                    en.Correo = email;//Ahora que ese usuario sea el del email
                    u = lu.BuscarUsuario(en.Correo);
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
                        lu.confirmacionUsuario(en);//Confirmacion 
                        u = lu.BuscarUsuario(en.Correo);
                        if (ValidarConfirmacionCorreo(u))
                        {
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
                            tbValidacion.Text = "1";
                            Session["user_session_data"] = u; //Creamos una sesion del usuario
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
    }
}