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
 
    public partial class InformeProducto : System.Web.UI.Page
    {

        public static double Porcentage = 0.5;
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
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
            Llenar_GridView();

        }

        /*
         * Esta función sirve para controlar los datos de la tabla y poder acceder
         * a los datos de los archivos para ser descargados o borrados
         */
        protected void GridViewMostrarArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                 * Así buscamos y encontramos un icono de miniatura para el fichero en función de la extensión del archivo
                 */
                Image icono = (Image)e.Row.FindControl("icono_fichero");
                double cantMin = Convert.ToDouble(e.Row.Cells[2].Text);
                double stock = Convert.ToDouble(e.Row.Cells[5].Text);
                if (cantMin*(1+Porcentage) < stock)
                {
                    icono.ImageUrl = "img/Bien.png";
                }
                else if (cantMin * (1 + Porcentage) > stock && cantMin < stock)
                {
                    icono.ImageUrl = "img/regular.png";
                }
                else
                {
                    icono.ImageUrl = "img/Mal.png";
                }
            }
        }

        private void Llenar_GridView()
        {
            LogicaProducto ls = new LogicaProducto();
            Responsive.DataSource = ls.MostrarProductos();
            Responsive.DataBind();
        }
    }
}