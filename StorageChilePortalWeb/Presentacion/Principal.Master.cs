using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

namespace Presentacion
{
    public partial class Site1 : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Charset = "utf-8";
            User_EN user = (User_EN)Session["user_session_data"];
            if (user != null)
            {
                Link_Cerrar_Sesion.Visible = true;
                Barra_Secundaria.Visible = false;
            }
        }

        protected void Busqueda_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            Response.Redirect("~/BusquedaArchivos.aspx?query=" + a.Text);
        }
    }
}