using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Data.SqlClient;
using System.Web.ClientServices;

namespace Presentacion
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Buscra_Click(object sender, EventArgs e)
        {
            /*User_EN usuarioABuscar = new User_EN();
            User_EN usuario = usuarioABuscar.BuscarUsuario(usuario_buscar.Text);
            if (usuario != null)
            {
                File_EN fi = new File_EN();
                fi.Propietario = usuario.ID;//Para identificar al usuario
                //EL griedView, mostrara un tabla con todos los datos que nos devuelva MostrarFilesUsuarioNombreEn
                GridViewMostrarArchivosUsuario.DataSource = fi.MostrarFilesUsuarioNombreEn();
                GridViewMostrarArchivosUsuario.DataBind();
            }*/
        }
    }
}