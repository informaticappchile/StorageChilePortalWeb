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
using System.Security.Cryptography;
using System.Configuration;

namespace Presentacion
{
 
    public partial class Inicio : System.Web.UI.Page
    {
        public int option = 0;
        public string msg = "";
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaOpciones lo = new LogicaOpciones();
            DateTime date1 = DateTime.Now;
            DateTime date2 = lo.getFecha();
            TimeSpan ts = date2.Subtract(date1);
            if (Session["user_session_data"] != null && lo.getMantenimiento() && !IsPostBack && (ts.Seconds < 345600 && ts.Seconds >= 0))
            {
                option = 1;
                msg = lo.getMensaje();
            }
            
            /*LogicaUsuario lu = new LogicaUsuario();
            User_EN nilo = lu.BuscarUsuario("naraya", "Usuario");
            clave.Text = Crypto.DecrytedPassword(lo.getCrypto(), nilo.Contraseña);*/
            
            //File_EN fi = new File_EN();
            //GridViewMostrarTodo.DataSource = fi.MostrarAllFiles();
            //GridViewMostrarTodo.DataBind();
        }
    }
}