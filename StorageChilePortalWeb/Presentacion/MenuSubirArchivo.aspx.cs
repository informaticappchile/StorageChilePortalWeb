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
using Logica;

namespace Presentacion
{
 
    public partial class MenuSubirArchivo : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            /*LogicaUsuario lu = new LogicaUsuario();
            User_EN userAutoLog = lu.BuscarUsuario("jbravo", "Usuario");
            Session["user_session_data"] = userAutoLog;*/
        }

        protected void clickContacto(object sender, EventArgs e)
        {
            Response.Redirect("Subir-Archivo.aspx");
        }
        protected void clickSoporte(object sender, EventArgs e)
        {
            Response.Redirect("Subir-Archivo-Contenedor.aspx");
        }
    }
}