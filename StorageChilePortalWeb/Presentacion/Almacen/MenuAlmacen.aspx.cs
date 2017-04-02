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
            User_EN userAutoLog = lu.BuscarUsuario("cvaras","Usuario");
            Session["user_session_data"] = userAutoLog;
            User_EN en = (User_EN)Session["user_session_data"];
            if (en == null)
            {
                Response.Redirect("Control_Usuarios/Login.aspx");
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