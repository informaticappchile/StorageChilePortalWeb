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
using Telerik.Web.UI;

namespace Presentacion
{
 
    public partial class Inicio : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaOpciones lo = new LogicaOpciones();
            DateTime date1 = DateTime.Now;
            DateTime date2 = lo.getFecha();
            TimeSpan ts = date2.Subtract(date1);
            if (Session["user_session_data"] != null && lo.getMantenimiento() && !IsPostBack && (ts.Days < 4 && ts.Days >= 0))
            {
                RadNotification1.Visible = true;
                RadNotification1.Text = lo.getMensaje() + lo.getFecha().ToString() + " hasta " + lo.getFechaTermino().ToString();
            }
            /*LogicaOpciones lo = new LogicaOpciones();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN nilo = lu.BuscarUsuario("naraya", "Usuario");
            clave.Text = Crypto.DecrytedPassword(lo.getCrypto(), nilo.Contraseña);*/
            //File_EN fi = new File_EN();
            //GridViewMostrarTodo.DataSource = fi.MostrarAllFiles();
            //GridViewMostrarTodo.DataBind();
        }
    }
}