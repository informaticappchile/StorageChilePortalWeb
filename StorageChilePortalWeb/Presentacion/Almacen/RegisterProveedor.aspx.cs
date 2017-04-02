using System;
using Entidades;
using Logica;
using System.Net.Mail;
using System.Web.Services;
using System.Net;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Presentacion
{
    public partial class RegisterProveedor : System.Web.UI.Page
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
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            User_EN en = (User_EN)Session["user_session_data"];
            if (en.IdPerfil != 2)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene privilegios de administrador para acceder aqui.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            if (Page.IsPostBack == false)
            {
                LogicaProveedor lp = new LogicaProveedor();
                ArrayList lista = lp.MostrarCiudades();
                ciudad_register.DataSource = lista;
                ciudad_register.DataTextField = "Nombre";
                ciudad_register.DataValueField = "Nombre";
                ciudad_register.DataBind();
            }
        }

        /* Una vez el usuario ha rellenado todos los campos solicitados en el apartado del registro
         * correctamente, es decir, el email tiene formato de email, las contraseñas coinciden...proceemos a
         * guardar el usuario en la base de datos
         */
        protected void Button_Register_Click(object sender, EventArgs e)
        {
            EmailExistsError_Register.Visible =
            UsernameExistsError_Register.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            Proveedor_EN busqueda = new Proveedor_EN();
            LogicaProveedor lu = new LogicaProveedor();
            if (lu.BuscarProveedor(razon_social_register.Text).RazonSocial != razon_social_register.Text) //Comprobamos que ese nombre de usuario ya este
            {
                Proveedor_EN en = new Proveedor_EN();//Si lo cumple todo, creamos un nuevo usuario
                en.Vendedor = vendedor_name_register.Text.Replace('\'', '´').Trim();
                en.RazonSocial = razon_social_register.Text.Replace('\'', '´').Trim();//Con su nombre de usuario
                en.Rut = rut_empresa_register.Text;//Con su correo
                en.Direccion = direccion_register.Text.Replace('\'', '´').Trim();//Con su contrasenya
                direccion_register.Text = en.Direccion.Replace('\'', '´').Trim();
                en.Ciudad = ciudad_register.Text;
                en.IdCiudad = lu.GetIdCiudad(ciudad_register.Text);
                en.Fono = fono_register.Text;
                lu.InsertarProveedor(en);//Llamamos a InsertarUsuario de la cap EN, que se encaragra de insertarlo
                Proveedor_EN u = lu.BuscarProveedor(en.RazonSocial);
                if (validarRegistroProveedor(u))
                {
                    limpiar(this.Controls);
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Se ha registrado al proveedor: " + en.RazonSocial);
                    //Cerramos el Script
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
                    sbMensaje.AppendFormat("alert('{0}');", "Ha ocurrido un error al registrar al proveedor, reintente mas tarde o comuníquese con el servicio de soporte.");
                    //Cerramos el Script
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
            }
            else UsernameExistsError_Register.Visible = true;

        }

        private bool validarRegistroProveedor(Proveedor_EN u)
        {
            if (u != null || u.RazonSocial != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static string ReCaptcha_Key = "<6LfZ-RUUAAAAAGrnxFF7Z4LCovzUAdbNyLMeboFz>";
        protected static string ReCaptcha_Secret = "<6LfZ-RUUAAAAAPQDIsUqplPc3FGA0Bik4IyQ_dZh>";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        private void limpiar(ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    if (text.ID == fono_register.ID)
                    {
                        text.Text = "+56";
                    }
                    else
                    {
                        text.Text = "";
                    }
                }
                else if (ctrl.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(ctrl.Controls);
            }
        }

    }
}