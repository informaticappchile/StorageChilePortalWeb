using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using System.Text;

namespace Presentacion
{
    public partial class Administrador_Editar_Perfil : System.Web.UI.Page
    {
        /**
         * Es necesario que los elementos CheckBox y RadioButton inicialicen sus tipos de clases
         * usando estos métodos, ya que si lo hacemos directamente desde el HTML el checkbox lo envuelve
         * en un <span></span> declarando la clase en esa etiqueta (y, por tanto, inhabilitando el checkbox)
         */
        protected void InitInputClasses()
        {
            Editar_Perfil_Visibilidad_Switch.InputAttributes.Add("class", "mdl-switch__input");
        }

        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(User_EN en)
        {
            Editar_Perfil_Usuario.Text = en.NombreUsu;
            Editar_Perfil_Nombre.Text = en.Nombre;
            Editar_Perfil_Email.Text = en.Correo;
            Editar_Perfil_ID.Text = en.ID.ToString();
            Editar_Perfil_Fecha_Resgistro.Text = en.FechaRegistro.ToString();
            Editar_Perfil_Fecha_Ingreso.Text = en.UltimoIngreso.ToString();
            if(en.Verified == "Verificado")
            {
                Editar_Perfil_Visibilidad_Switch.Checked = true;
                Editar_Perfil_Visibilidad_Switch.Enabled = false;
                Editar_Perfil_Visibilidad_Label.Text = "Verificado";
            }
            else
            {
                Editar_Perfil_Visibilidad_Switch.Checked = false;
            }

        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            LogicaUsuario lu = new LogicaUsuario();
            this.user = Request["ID"].ToString();
            this.en = lu.BuscarUsuarioAdmin(user);
            this.en.NombreUsu = Editar_Perfil_Usuario.Text;
            this.en.Nombre = Editar_Perfil_Nombre.Text;
            this.en.Correo = Editar_Perfil_Email.Text;
            if (Editar_Perfil_Visibilidad_Switch.Checked)
            {
                en.Verified = "Verificado";
            }

            lu.actualizarUsuarioAdmin(en);
            if (ValidarCambios(en))
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se actualizaron los datos correctamente");
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
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error al actualizar los datos. Reintente más tarde "+
                    "o pongase en contacto con el servicio de soporte.");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            Response.AddHeader("REFRESH", "2;URL=AdministrarUsuario.aspx");
            //Response.Redirect("~/AdministarUsuario.aspx");
        }

        private string user;
        private User_EN en = new User_EN();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitInputClasses();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN ad = (User_EN)Session["user_session_admin"];
            if (ad != null)
            {
                if (!Page.IsPostBack)
                {
                    if (Request["ID"] != null)
                    {
                        this.user = Request["ID"].ToString();
                        this.en = lu.BuscarUsuarioAdmin(user);
                        CargarDatos(this.en);
                    }
                }
            }
          else
           {
                //Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Debe iniciar sesión, usted no tiene privilegios para acceder aqui");
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        protected bool ValidarCambios(User_EN u)
        {
            LogicaUsuario lu = new LogicaUsuario();
            User_EN en = lu.BuscarUsuarioAdmin(u.NombreUsu);
            if (en.NombreUsu != u.NombreUsu) { return false; }
            if (en.Nombre != u.Nombre){return false; }
            if (en.Correo != u.Correo) { return false; }
            if (en.Verified != u.Verified) { return false; }
            return true;
        }
    }
}