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
using System.Security.Cryptography;
using System.Configuration;

namespace Presentacion
{
 
    public partial class Default : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbMensaje = new StringBuilder();
            //Aperturamos la escritura de Javascript
            sbMensaje.Append("<script type='text/javascript'>");
            //Le indicamos al alert que mensaje va mostrar          
            sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
            //Cerramos el Script
            sbMensaje.Append("</script>");
            //Registramos el Script escrito en el StringBuilder
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
        }
    }
}