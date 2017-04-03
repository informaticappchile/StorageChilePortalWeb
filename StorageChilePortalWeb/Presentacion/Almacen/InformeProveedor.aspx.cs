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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

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
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
            LogicaServicio ls = new LogicaServicio();
            em.ListaServicio = ls.MostrarServiciosEmpresas(em);
            for (int i = 0; i < em.ListaServicio.Count; i++)
            {
                if (!((Servicio_EN)em.ListaServicio[i]).Verified && ((Servicio_EN)em.ListaServicio[i]).Nombre == "Almacen")
                {
                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                    StringBuilder sbMensaje = new StringBuilder();
                    //Aperturamos la escritura de Javascript
                    sbMensaje.Append("<script type='text/javascript'>");
                    //Le indicamos al alert que mensaje va mostrar
                    sbMensaje.AppendFormat("alert('{0}');", "Usted no dispone de estos servicios.");
                    //Cerramos el Script
                    sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Inicio.aspx\";");
                    sbMensaje.Append("</script>");
                    //Registramos el Script escrito en el StringBuilder
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                }
            }
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
            if (!IsPostBack)
            {
                Session["dataPago"] = null;
                Llenar_GridView();
                try
                {
                    LogicaProveedor lpr = new LogicaProveedor();
                    lista = lpr.MostrarProveedores();
                    ArrayList aux = new ArrayList();
                    aux.Add("Mostrar Todos");
                    for (int i = 0; i < lista.Count; i++)
                    {
                        aux.Add(((Proveedor_EN)lista[i]).RazonSocial);
                    }
                    buscar_razon_social.DataSource = aux;
                    buscar_razon_social.DataBind();
                }
                catch (Exception ex)
                {
                    //No hay producto o proveedor
                }
            }
            
        }

        private void Llenar_GridView()
        {
            LogicaMovimiento lm = new LogicaMovimiento();
            ArrayList lista = new ArrayList();
            lista = lm.MostrarMovimientosPorProveedor();
            DataTable dt = new DataTable();
            dt.Columns.Add("RazonSocial");
            dt.Columns.Add("TipoDoc");
            dt.Columns.Add("NumDoc");
            dt.Columns.Add("FechaDocumento");
            dt.Columns.Add("Total");
            dt.Columns.Add("Observaciones");
            dt.Columns.Add("EstadoPago");
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
                    observaciones += "' " + (string)obs[j] + "' ";
                }
                row["Observaciones"] = observaciones;
                if (((Movimiento_EN)lista[i]).IdPago == "0")
                {
                    row["EstadoPago"] = "No Pagado";
                }
                else
                {
                    row["EstadoPago"] = "Pagado";

                }
                dt.Rows.Add(row);
            }
            Session["dataPago"] = dt;
            //enlazas datatable a griedview
            Responsive.DataSource = dt;
            Responsive.DataBind();
        }

        private void Llenar_GridView(string razon)
        {
            LogicaMovimiento lm = new LogicaMovimiento();
            ArrayList lista = new ArrayList();
            lista = lm.MostrarMovimientosPorProveedor(razon);
            DataTable dt = new DataTable();
            dt.Columns.Add("RazonSocial");
            dt.Columns.Add("TipoDoc");
            dt.Columns.Add("NumDoc");
            dt.Columns.Add("FechaDocumento");
            dt.Columns.Add("Total");
            dt.Columns.Add("Observaciones");
            dt.Columns.Add("EstadoPago");

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
                    observaciones += "' " + (string)obs[j] + "' ";
                }
                row["Observaciones"] = observaciones;
                if (((Movimiento_EN)lista[i]).IdPago == "0")
                {
                    row["EstadoPago"] = "No Pagado";
                }
                else
                {
                    row["EstadoPago"] = "Pagado";

                }
                dt.Rows.Add(row);
            }
            Session["dataPago"] = dt;
            //enlazas datatable a griedview
            Responsive.DataSource = dt;
            Responsive.DataBind();
        }


        protected void ClickExportToExcel(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dataPago"];
            string attachment = "attachment; filename=listadoProveedoresPagos.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        public void ClickExportToPdf(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dataPago"];

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=listadoProveedoresPagos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        protected void Button_Buscar_Click(object sender, EventArgs e)
        {
            if (buscar_razon_social.Text == "Mostrar Todos")
            {
                Llenar_GridView();
            }
            else
            {
                Llenar_GridView(buscar_razon_social.Text);
            }
        }
    }
}