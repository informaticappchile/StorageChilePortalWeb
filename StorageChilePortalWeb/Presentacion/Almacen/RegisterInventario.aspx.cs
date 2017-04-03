using System;
using Entidades;
using Logica;
using System.Net.Mail;
using System.Web.Services;
using System.Net;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            LogicaProveedor l = new LogicaProveedor();
            ArrayList lista = l.MostrarProveedores();
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
                lista = lp.MostrarProductos();
                if (lista.Count > 0)
                {
                    ArrayList aux = new ArrayList();
                    aux.Add("Limpiar");
                    for (int i = 0; i < lista.Count; i++)
                    {
                        aux.Add(((Producto_EN)lista[i]).Descripcion);
                    }
                    productos_registrados.DataSource = aux;
                    productos_registrados.DataBind();
                }
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
                en.Descripcion = descripcion_register.Text.Replace('\'', '´').Trim();//Con su correo
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
                    limpiar(this.Controls);
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

        protected void clickRellenar(object sender, EventArgs e)
        {
            if (productos_registrados.Text == "Limpiar")
            {
                grupo_register.Enabled = true;
                codigo_producto_register.Enabled = true;
                descripcion_register.Enabled = true;
                unidad_register.Enabled = true;
                cant_min_stock_register.Enabled = true;
                limpiar(this.Controls);
            }
            else
            {
                LogicaProducto lp = new LogicaProducto();
                Producto_EN p = lp.BuscarProducto(productos_registrados.Text);
                grupo_register.Text = p.Grupo;
                codigo_producto_register.Text = p.CodProducto;
                descripcion_register.Text = p.Descripcion;
                unidad_register.Text = p.UnidadMedida;
                cant_min_stock_register.Text = p.CantMinStock.ToString();
            }
        }

        private void limpiar(ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    text.Text = "";
                }
                else if (ctrl.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(ctrl.Controls);
            }
        }
    }
}