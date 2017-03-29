using System;
using Entidades;
using Logica;
using System.Net.Mail;
using System.Web.Services;
using System.Net;
using System.Text;
using System.Data;
using System.Collections;

namespace Presentacion
{
    public partial class RegisterInventario : System.Web.UI.Page
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
            if(en.IdPerfil != 2)
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
            LogicaProveedor le = new LogicaProveedor();
            ArrayList lista = le.MostrarProveedores();
            if (lista.Count == 0)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene proveedores disponibles en el sistema. Por favor registre uno.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/RegisterProveedor.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            if (Page.IsPostBack == false)
            {
                proveedor_register.DataSource = lista;
                proveedor_register.DataTextField = "RazonSocial";
                proveedor_register.DataValueField = "RazonSocial";
                proveedor_register.DataBind();
                LogicaProducto lp = new LogicaProducto();
                lista = lp.MostrarGrupos();
                grupo_register.DataSource = lista;
                grupo_register.DataBind();
                lista = lp.MostrarUnidades();
                unidad_register.DataSource = lista;
                unidad_register.DataBind();
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
            Producto_EN busqueda = new Producto_EN();
            LogicaProducto lu = new LogicaProducto();
            if (lu.BuscarProducto(codigo_producto_register.Text).CodProducto != codigo_producto_register.Text ) //Comprobamos que ese nombre de usuario ya este
            {
                Producto_EN en = new Producto_EN();//Si lo cumple todo, creamos un nuevo usuario
                en.CodProducto = codigo_producto_register.Text;//Con su nombre de usuario
                en.Descripcion = descripcion_register.Text;//Con su correo
                en.CantMinStock = Convert.ToInt32(cant_min_stock_register.Text);//Con su contrasenya
                en.IdGrupo = lu.GetIdGrupo(grupo_register.Text);
                en.IdMedidad = lu.GetIdUnidad(unidad_register.Text);
                lu.InsertarProducto(en);//Llamamos a InsertarUsuario de la cap EN, que se encaragra de insertarlo
                Producto_EN u = lu.BuscarProducto(en.CodProducto);
                if (validarRegistroProducto(u))
                {
                    LogicaProveedor lp = new LogicaProveedor();
                    u.IdProveedor = lp.BuscarProveedor(proveedor_register.Text).ID;
                    lu.InsertarProductoProveedor(u);
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Se ha registrado el producto: " + en.Descripcion);
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
                    sbMensaje.AppendFormat("alert('{0}');", "Ha ocurrido un error al registrar el producto, reintente mas tarde o comuníquese con el servicio de soporte.");
                    //Cerramos el Script
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
            }
            else UsernameExistsError_Register.Visible = true;

        }

        private bool validarRegistroProducto(Producto_EN u)
        {
            if (u != null || u.CodProducto != "")
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
    }
}