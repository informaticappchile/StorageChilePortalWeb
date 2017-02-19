﻿using System;
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

namespace Presentacion
{
 
    public partial class Contactos : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void clickContacto(object sender, EventArgs e)
        {
            Response.Redirect("Contactanos.aspx");
        }
        protected void clickSoporte(object sender, EventArgs e)
        {
            Response.Redirect("Soporte.aspx");
        }
    }
}