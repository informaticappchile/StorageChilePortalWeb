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
 
    public partial class InformeProducto : System.Web.UI.Page
    {

        public static double Porcentage = 0.5;
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
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = lp.MostrarProductosPorEmpresa(em);
            if (lista.Count == 0)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene productos disponibles en el sistema. Por favor registre uno.");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/RegisterInventario.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            if (!IsPostBack)
            {
                Session["dataStock"] = null;
                Llenar_GridView();
            }

        }

        /*
         * Esta función sirve para controlar los datos de la tabla y poder acceder
         * a los datos de los archivos para ser descargados o borrados
         */
        protected void GridViewMostrarArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                 * Así buscamos y encontramos un icono de miniatura para el fichero en función de la extensión del archivo
                 */
                System.Web.UI.WebControls.Image icono = (System.Web.UI.WebControls.Image)e.Row.FindControl("icono_fichero");
                double cantMin = Convert.ToDouble(e.Row.Cells[2].Text);
                double stock = Convert.ToDouble(e.Row.Cells[5].Text);
                if (cantMin*(1+Porcentage) < stock)
                {
                    icono.ImageUrl = "img/Bien.png";
                }
                else if (cantMin * (1 + Porcentage) >= stock && cantMin <= stock)
                {
                    icono.ImageUrl = "img/regular.png";
                }
                else
                {
                    icono.ImageUrl = "img/Mal.png";
                }
            }
        }

        private void Llenar_GridView()
        {
            User_EN en = (User_EN)Session["user_session_data"];
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
            LogicaProducto ls = new LogicaProducto();
            ArrayList lista = ls.MostrarProductosPorEmpresa(em);
            DataTable dt = new DataTable();
            if (Session["dataStock"] == null)
            {
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("CodProducto");
                dt.Columns.Add("CantMinStock");
                dt.Columns.Add("Grupo");
                dt.Columns.Add("UnidadMedida");
                dt.Columns.Add("Stock");
                dt.Columns.Add("NStock");
            }
            else
            {
                dt = (DataTable)Session["dataStock"];
            }

            if (lista.Count == 0)
            {
                Session["dataStock"] = null;
                Responsive.DataSource = null;
                Responsive.DataBind();
            }

            for (int i = 0; i < lista.Count; i++)
            {
                //Agregar Datos    
                DataRow row = dt.NewRow();
                double cantMin = Convert.ToDouble(((Producto_EN)lista[i]).CantMinStock);
                double stock = Convert.ToDouble(((Producto_EN)lista[i]).Stock);
                row["Descripcion"] = ((Producto_EN)lista[i]).Descripcion;
                row["CodProducto"] = ((Producto_EN)lista[i]).CodProducto;
                row["CantMinStock"] = ((Producto_EN)lista[i]).CantMinStock;
                row["Grupo"] = ((Producto_EN)lista[i]).Grupo;
                row["UnidadMedida"] = ((Producto_EN)lista[i]).UnidadMedida;
                row["Stock"] = ((Producto_EN)lista[i]).Stock;
                if (cantMin * (1 + Porcentage) < stock)
                {
                    row["NStock"] = "Excelente";
                }
                else if (cantMin * (1 + Porcentage) >= stock && cantMin <= stock)
                {
                    row["NStock"] = "Estable";
                }
                else
                {
                    row["NStock"] = "Mal";
                }
                dt.Rows.Add(row);
            }
            Session["dataStock"] = dt;
            //enlazas datatable a griedview
            Responsive.DataSource = dt;
            Responsive.DataBind();

            Responsive.DataSource = ls.MostrarProductosPorEmpresa(em);
            Responsive.DataBind();
        }

        protected void ClickExportToExcel(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dataStock"];
            string attachment = "attachment; filename=listadoStockProductos.xls";
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
            DataTable dt = (DataTable)Session["dataStock"];

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=listadoStockProductos.pdf");
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
    }
}