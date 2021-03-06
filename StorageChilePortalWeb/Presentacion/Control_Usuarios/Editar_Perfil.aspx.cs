﻿using System;
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
    public partial class Editar_Perfil : System.Web.UI.Page
    {
        /**
         * Es necesario que los elementos CheckBox y RadioButton inicialicen sus tipos de clases
         * usando estos métodos, ya que si lo hacemos directamente desde el HTML el checkbox lo envuelve
         * en un <span></span> declarando la clase en esa etiqueta (y, por tanto, inhabilitando el checkbox)
         */
        protected void InitInputClasses()
        {
            Editar_Perfil_Contraseña.Attributes["type"] = "password"; //Engañar al servidor para que pueda recibir valores
        }

        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(User_EN en)
        {
            LogicaOpciones lo = new LogicaOpciones();
            byte[] salt = lo.getCrypto();
            Editar_Perfil_Usuario.Text = en.NombreUsu;
            Editar_Perfil_Nombre.Text = en.Nombre;
            Editar_Perfil_Email.Text = en.Correo;
            Editar_Perfil_Contraseña.Text = Crypto.DecrytedPassword(salt, en.Contraseña);
            Editar_Perfil_ID.Text = en.ID.ToString();

        }

        /*
         * Cuando se dé click al botón Editar Datos, todas las entradas pasarán a ser editables
         * También el botón Editar Datos se esconderá para dar paso a Guardar Datos
         */
        protected void Editar_Perfil_Editar_Click(object sender, EventArgs e)
        {
            Editar_Perfil_Contraseña.Text = ""; //Vaciamos la contraseña para que no la puedan copiar



            Editar_Perfil_Nombre.ReadOnly =
            Editar_Perfil_Email.ReadOnly =
            Editar_Perfil_Contraseña.ReadOnly =

            Editar_Perfil_Editar.Visible = false;
            Editar_Perfil_Guardar.Visible = true;
        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            LogicaOpciones lo = new LogicaOpciones();
            byte[] salt = lo.getCrypto();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN en = new User_EN();
            en.ID = Convert.ToInt16(Editar_Perfil_ID.Text);
            en.NombreUsu = Editar_Perfil_Usuario.Text;
            en.Nombre = Editar_Perfil_Nombre.Text;
            en.Correo = Editar_Perfil_Email.Text;
            string password = Editar_Perfil_Contraseña.Text;
            en.Contraseña = Crypto.Hash(salt, password);


            lu.actualizarUsuario(en);
            if (ValidarCambios(en))
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se actualizaron los datos correctamente");
                //Redirige a página de inicio.
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
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
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error al actualizar los datos. Reintente más tarde " +
                    "o pongase en contacto con el servicio de soporte.");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitInputClasses();
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
            User_EN en = (User_EN)Session["user_session_data"];
            if (en != null)
            {
                if (!Page.IsPostBack)
                {
                    CargarDatos(en);
                }

            }
            else
            {
                Response.Redirect("Login.aspx"); //Si no se ha iniciado sesion, no podras ver tu pefil y se redireccionara a la pagina de iniciar sesion
            }
        }

        protected bool ValidarCambios(User_EN u)
        {
            LogicaUsuario lu = new LogicaUsuario();
            User_EN en = lu.BuscarUsuario(u.NombreUsu, "Usuario");
            if (en.Nombre != u.Nombre) { return false; }
            if (en.Correo != u.Correo) { return false; }
            if (!Equality(en.Contraseña,u.Contraseña)) { return false; }
            return true;
        }

        public bool Equality(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;
                while (i < a1.Length && (a1[i] == b1[i])) //Earlier it was a1[i]!=b1[i]
                {
                    i++;
                }
                if (i == a1.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}