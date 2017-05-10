using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Data.SqlClient;
using System.Web.ClientServices;
using System.IO;
using System.Text;
using Logica;

namespace Presentacion
{
 
    public partial class MenuAlmacen : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            LogicaUsuario lu = new LogicaUsuario();
            User_EN userAutoLog = lu.BuscarUsuario("cvaras", "Usuario");
            Session["user_session_data"] = userAutoLog;*/
            User_EN en = (User_EN)Session["user_session_data"];
            
            if (Session["user_session_data"] == null)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no dispone de estos servicios.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuario/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                return;
            }
            
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
                case "UsuarioMantenimiento":
                    break;
                case "AdministradorAlmacen":
                    break;
                case "UsuarioAlmacen":
                    break;
                case "UsuarioAlmacenMovimientoCompraDevolucion":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    informe_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    crear_proveedor.Visible = false;
                    break;
                case "UsuarioAlmacenMovimientoMermaProduccion":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    informe_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    crear_proveedor.Visible = false;
                    break;
                case "UsuarioAlmacenMovimiento":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    informe_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    crear_proveedor.Visible = false;
                    break;
                case "UsuarioAlmacenCrearProducto":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    informe_proveedor.Visible = false;
                    movimiento.Visible = false;
                    crear_proveedor.Visible = false;
                    break;
                case "UsuarioAlmacenCrearProveedor":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    informe_proveedor.Visible = false;
                    movimiento.Visible = false;
                    crear_producto.Visible = false;
                    break;
                case "UsuarioAlmacenInformeInventario":
                    pago.Visible = false;
                    informe_proveedor.Visible = false;
                    movimiento.Visible = false;
                    crear_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    break;
                case "UsuarioAlmacenInformeProveedor":
                    pago.Visible = false;
                    informe_inventario.Visible = false;
                    movimiento.Visible = false;
                    crear_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    break;
                case "Usuario":
                    break;
                case "BodegaAlmacen":
                    pago.Visible = false;
                    crear_proveedor.Visible = false;
                    crear_producto.Visible = false;
                    informe_proveedor.Visible = false;
                    break;
                case "Caja":
                    pago.Visible = false;
                    informe_proveedor.Visible = false;
                    informe_inventario.Visible = false;
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
        }

        protected void clickCreacionProveedor(object sender, EventArgs e)
        {
            Response.Redirect("RegisterProveedor.aspx");
        }

        protected void clickCreacionProducto(object sender, EventArgs e)
        {
            Response.Redirect("RegisterInventario.aspx");
        }
        protected void clickMovimientosInventario(object sender, EventArgs e)
        {
            Response.Redirect("Movimientos.aspx");
        }
        protected void clickPagoProveedores(object sender, EventArgs e)
        {
            Response.Redirect("Pagos.aspx");
        }

        protected void clickInformeProveedor(object sender, EventArgs e)
        {
            Response.Redirect("InformeProveedor.aspx");
        }
        protected void clickInformeInventario(object sender, EventArgs e)
        {
            Response.Redirect("InformeProducto.aspx");
        }
    }
}