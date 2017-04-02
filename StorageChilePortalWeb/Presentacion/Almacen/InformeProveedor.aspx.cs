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
using System.Collections;
using System.Data;

namespace Presentacion
{
 
    public partial class InformeProveedor : System.Web.UI.Page
    {
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
            User_EN en = (User_EN)Session["user_session_data"];
            LogicaMovimiento lm = new LogicaMovimiento();
            ArrayList lista = lm.MostrarMovimientosProductosProveedor();
            if (lista.Count == 0)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene movimientos realizados en el sistema. Por favor registre uno.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/Movimientos.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            Llenar_GridView();
            
        }

        private void Llenar_GridView()
        {
            LogicaMovimiento lm = new LogicaMovimiento();
            ArrayList lista = new ArrayList();
            lista = lm.MostrarMovimientosPorProveedor();
            DataTable dt = new DataTable();
            if (Session["dataPago"] == null)
            {
                dt.Columns.Add("RazonSocial");
                dt.Columns.Add("TipoDoc");
                dt.Columns.Add("NumDoc");
                dt.Columns.Add("FechaDocumento");
                dt.Columns.Add("Total");
                dt.Columns.Add("Observaciones");
                dt.Columns.Add("IdMovimiento");
            }
            else
            {
                dt = (DataTable)Session["dataPago"];
            }

            if (lista.Count == 0)
            {
                Session["dataPago"] = null;
                Responsive.DataSource = null;
                Responsive.DataBind();
            }

            for (int i = 0; i < lista.Count; i++)
            {
                //Agregar Datos    
                DataRow row = dt.NewRow();
                row["RazonSocial"] = ((Movimiento_EN)lista[i]).RazonSocial;
                row["TipoDoc"] = ((Movimiento_EN)lista[i]).Documento;
                row["NumDoc"] = ((Movimiento_EN)lista[i]).NumDocumento;
                row["FechaDocumento"] = ((Movimiento_EN)lista[i]).FechaDocumento;
                row["Total"] = ((Movimiento_EN)lista[i]).Total;
                ArrayList obs = new ArrayList();
                obs = lm.MostrarObservaciones(((Movimiento_EN)lista[i]).RazonSocial, ((Movimiento_EN)lista[i]).ID);
                string observaciones = "";
                for (int j = 0; j < obs.Count; j++)
                {
                    observaciones += "- " + (string)obs[j] + "\n";
                }
                row["Observaciones"] = observaciones;
                row["IdMovimiento"] = ((Movimiento_EN)lista[i]).ID;
                dt.Rows.Add(row);
            }
            Session["dataPago"] = dt;
            //enlazas datatable a griedview
            Responsive.DataSource = dt;
            Responsive.DataBind();
        }
    }
}