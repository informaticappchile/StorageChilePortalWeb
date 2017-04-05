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
            LogicaUsuario lu = new LogicaUsuario();
            User_EN userAutoLog = lu.BuscarUsuario("cvaras", "Usuario");
            Session["user_session_data"] = userAutoLog;
            User_EN en = (User_EN)Session["user_session_data"];
            if (en == null)
            {
                Response.Redirect("Control_Usuarios/Login.aspx");
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