using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using User_EN_Class;

namespace Prensentacion
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        /**
         * Es necesario que los elementos CheckBox y RadioButton inicialicen sus tipos de clases
         * usando estos métodos, ya que si lo hacemos directamente desde el HTML el checkbox lo envuelve
         * en un <span></span> declarando la clase en esa etiqueta (y, por tanto, inhabilitando el checkbox)
         */
        protected void InitInputClasses()
        {
            /*Editar_Perfil_Contraseña.Attributes["type"] = "password"; //Engañar al servidor para que pueda recibir valores
            Editar_Perfil_Visibilidad_Switch.InputAttributes.Add("class", "mdl-switch__input");
            Editar_Perfil_Hombre.InputAttributes.Add("class", "mdl-radio__button");
            Editar_Perfil_Mujer.InputAttributes.Add("class", "mdl-radio__button");
            Editar_Perfil_NoMostrar.InputAttributes.Add("class", "mdl-radio__button");*/
        }

        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(User_EN en)
        {
            /*Editar_Perfil_Usuario.Text = en.NombreUsu;
            Editar_Perfil_Nombre.Text = en.Nombre;
            Editar_Perfil_Email.Text = en.Correo;
            Editar_Perfil_Contraseña.Text = en.Contraseña;
            Editar_Perfil_Localidad.Text = en.Localidad;
            if(en.Edad > 0) Editar_Perfil_Edad.Text = en.Edad.ToString();
            if (en.Genero == null) Editar_Perfil_NoMostrar.Checked = true;
            else if (en.Genero.Value == true) Editar_Perfil_Hombre.Checked = true;
            else Editar_Perfil_Mujer.Checked = true;
            
            if (en.Visibilidad_perfil == true)
            {
                Editar_Perfil_Visibilidad_Switch.Checked = true;
                Editar_Perfil_Visibilidad_Label.Text = "Público";
            }
            Editar_Perfil_ID.Text = en.ID.ToString();*/
        }

        /*
         * Cuando se dé click al botón Editar Datos, todas las entradas pasarán a ser editables
         * También el botón Editar Datos se esconderá para dar paso a Guardar Datos
         */
        protected void Editar_Perfil_Editar_Click(object sender, EventArgs e)
        {
            /*Editar_Perfil_Contraseña.Text = ""; //Vaciamos la contraseña para que no la puedan copiar

            Editar_Perfil_Usuario.ReadOnly =
            Editar_Perfil_Nombre.ReadOnly =
            Editar_Perfil_Email.ReadOnly =
            Editar_Perfil_Contraseña.ReadOnly =
            Editar_Perfil_Edad.ReadOnly =
            Editar_Perfil_Localidad.ReadOnly = false;

            Editar_Perfil_Hombre.Enabled =
            Editar_Perfil_Mujer.Enabled =
            Editar_Perfil_NoMostrar.Enabled =
            Editar_Perfil_Visibilidad_Switch.Enabled = true;

            Editar_Perfil_Editar.Visible = false;
            Editar_Perfil_Guardar.Visible = true;*/
        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            /*if(Editar_Perfil_Visibilidad_Switch.Checked)
            {
                Response.Write("ON");
            } else
            {
                Response.Write("OFF");
            }

            User_EN en = new User_EN();
            en.ID = Convert.ToInt16(Editar_Perfil_ID.Text);
            en.NombreUsu = Editar_Perfil_Usuario.Text;
            en.Nombre = Editar_Perfil_Nombre.Text;
            en.Correo = Editar_Perfil_Email.Text;
            en.Contraseña = Editar_Perfil_Contraseña.Text;
            en.Edad = Convert.ToInt16(Editar_Perfil_Edad.Text);
            en.Localidad = Editar_Perfil_Localidad.Text;
            en.Visibilidad_perfil = Editar_Perfil_Visibilidad_Switch.Checked;
            if (Editar_Perfil_Hombre.Checked)
                en.Genero = true;
            else if (Editar_Perfil_Mujer.Checked)
                en.Genero = false;
            else
                en.Genero = null;

            en.actualizarUsuario();*/
 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitInputClasses();
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
    }
}