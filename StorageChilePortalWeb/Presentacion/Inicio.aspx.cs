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
 
    public partial class Inicio : System.Web.UI.Page
    {
        /*
         * AL cargar la pagina de inicio, se mostraran todos los archivos de la base de datos que sean publicos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaOpciones lo = new LogicaOpciones();
            LogicaUsuario lu = new LogicaUsuario();
            User_EN nilo = lu.BuscarUsuario("naraya", "Usuario");
            clave.Text = Crypto.DecrytedPassword(lo.getCrypto(), nilo.Contraseña);
            //File_EN fi = new File_EN();
            //GridViewMostrarTodo.DataSource = fi.MostrarAllFiles();
            //GridViewMostrarTodo.DataBind();
        }
    }
}