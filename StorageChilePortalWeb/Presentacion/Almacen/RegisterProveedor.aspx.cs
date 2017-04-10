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
using System.Configuration;

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
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
            LogicaServicio ls = new LogicaServicio();
            em.ListaServicio = ls.MostrarServiciosEmpresas(em);
            for (int i = 0; i < em.ListaServicio.Count; i++)
            {
                if (!((Servicio_EN)em.ListaServicio[i]).Verified && ((Servicio_EN)em.ListaServicio[i]).Nombre == "Almacen")
                {
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Usted no dispone de estos servicios.");
                    //Cerramos el Script
                    sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
            }
            switch (en.NombrePerfil)
            {
                case "Administrador":
                    break;
                case "AdministradorAlmacen":
                    break;
                case "UsuarioAlmacen":
                    break;
                case "UsuarioAlmacenCrearProveedor":
                    break;
                case "Usuario":
                    break;
                default:
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene permisos para ingresar aquí.");
                    //Cerramos el Script
                    sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/MenuAlmacen.aspx\";");
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    break;

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
            DigiVerifacadorInValidError_Register.Visible =
            UsernameExistsError_Register.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            Proveedor_EN busqueda = new Proveedor_EN();
            LogicaProveedor lu = new LogicaProveedor();
            if (!validarRut(rut_empresa_register.Text))
            {
                DigiVerifacadorInValidError_Register.Visible = true;
                return;
            }
            if (lu.BuscarProveedor(razon_social_register.Text).RazonSocial != razon_social_register.Text) //Comprobamos que ese nombre de usuario ya este
            {
                if (lu.BuscarProveedor(rut_empresa_register.Text).Rut != rut_empresa_register.Text)
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
                        LogicaEmpresa lem =  new LogicaEmpresa();
                        Empresa_EN emp = lem.BuscarEmpresa(((User_EN)Session["user_session_data"]).NombreEmp);
                        en.ID = u.ID;

                        string Id = GenerarPass(5, 15);

                        Proveedor_EN pag = lu.BuscarVendedor(en, Id);

                        while (pag.IdVendedor != "")
                        {
                            Id = GenerarPass(5, 15);
                            pag = lu.BuscarVendedor(en, Id);
                        }

                        en.IdVendedor = Id;

                        lu.InsertarVendedor(en);
                        lu.InsertarVendedorEmpresa(en,emp);
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
                else EmailExistsError_Register.Visible = true;
            }
            else
            {
                if (lu.BuscarProveedor(rut_empresa_register.Text).Rut != rut_empresa_register.Text)
                {
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Este Rut no correponde con la razón social asociada.");
                    //Cerramos el Script
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
                else
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
                    Proveedor_EN u = lu.BuscarProveedor(en.RazonSocial);
                    LogicaEmpresa lem = new LogicaEmpresa();
                    Empresa_EN emp = lem.BuscarEmpresa(((User_EN)Session["user_session_data"]).NombreEmp);
                    en.ID = u.ID;
                    ArrayList list = lu.MostrarProveedorVendedorEmpresa(en, emp);
                    if (list.Count > 0)
                    {
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "Usted ya tiene a este proveedor registrado con un vendedor.");
                        //Cerramos el Script
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    }
                    else
                    {

                        string Id = GenerarPass(5, 15);

                        Proveedor_EN pag = lu.BuscarVendedor(en, Id);

                        while (pag.IdVendedor != "")
                        {
                            Id = GenerarPass(5, 15);
                            pag = lu.BuscarVendedor(en, Id);
                        }

                        en.IdVendedor = Id;

                        lu.InsertarVendedor(en);
                        lu.InsertarVendedorEmpresa(en, emp);
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
                }
            }

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

        protected static string ReCaptcha_Key = "<"+ ConfigurationManager.AppSettings["ReCaptcha_Key"] + ">";
        protected static string ReCaptcha_Secret = "<" + ConfigurationManager.AppSettings["ReCaptcha_Secret"] + ">";

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
        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LongPassMin"></param>
        /// <param name="LongPassMax"></param>
        /// <returns></returns>
        protected string GenerarPass(int LongPassMin, int LongPassMax)
        {
            char[] ValueAfanumeric = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '!', '#', '$', '%', '&', '?', '¿' };
            Random ram = new Random();
            int LogitudPass = ram.Next(LongPassMin, LongPassMax);
            string Password = String.Empty;

            for (int i = 0; i < LogitudPass; i++)
            {
                int rm = ram.Next(0, 2);

                if (rm == 0)
                {
                    Password += ram.Next(0, 10);
                }
                else
                {
                    Password += ValueAfanumeric[ram.Next(0, 59)];
                }
            }

            return Password;
        }

    }
}