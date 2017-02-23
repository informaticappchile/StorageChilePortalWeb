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
 
    public partial class AdministrarUsuario : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            //File_EN fi = new File_EN();
            //GridViewMostrarTodo.DataSource = fi.MostrarAllFiles();
            Llenar_GridView();
            
        }

        /// <summary>
        /// Metodo para eliminar un usuario
        /// No elimina cuando estan vinculados a un servicio,
        /// a un ticket o a una respuesta a un ticket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Responsive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            {
                if (e.CommandName == "DEL")
                {
                    LogicaUsuario lu = new LogicaUsuario();
                    if (lu.BorrarUsuario(e.CommandArgument.ToString()))
                    {
                        Llenar_GridView();
                        //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                        StringBuilder sbMensaje = new StringBuilder();
                        //Aperturamos la escritura de Javascript
                        sbMensaje.Append("<script type='text/javascript'>");
                        //Le indicamos al alert que mensaje va mostrar
                        sbMensaje.AppendFormat("alert('{0}');", "El usuario ha sido eliminado correctamente");
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
                        sbMensaje.AppendFormat("alert('{0}');", "No se ha podido eliminar al usuario, intente más tarde o consulte a soporte técnico");
                        //Cerramos el Script
                        sbMensaje.Append("</script>");
                        //Registramos el Script escrito en el StringBuilder
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                    }
                }
            }
        }

        private void Llenar_GridView()
        {
            LogicaUsuario lu = new LogicaUsuario();
            Responsive.DataSource = lu.MostrarUsuarios();
            Responsive.DataBind();
        }
    }
}