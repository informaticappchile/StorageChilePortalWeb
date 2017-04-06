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
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Presentacion
{
    
    public partial class PagosComprobante : System.Web.UI.Page
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
            ArrayList lista = lm.MostrarMovimientosProductosProveedor(em.ID);
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
                fecha_pago_register.Text = Request["fecha"].ToString();
                razon_social_register.Text = Request["Proveedor"].ToString();
                tipo_pago_register.Text = Request["TipoPago"].ToString();
                if (Request["numCheque"] != null)
                {
                    num_cheque_register.Visible = true;
                    num_cheque_register.Text = Request["numCheque"];
                }
                Llenar_GridView(razon_social_register.Text);
            }
        }

        protected void clickGuardar(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["imprimirPago"];
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=listadoproveedores.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            //Obtiene imagen y lo inserta en el PDF
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/logEmpresas/logoEmp.png"));
            logo.SetAbsolutePosition(430, 700);
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(140);
            pdfDoc.Add(logo);
            //fin
            //Agrega Titulo

            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            pdfDoc.Add(new iTextSharp.text.Paragraph(" \n \n \n \n"));
            pdfDoc.Add(new iTextSharp.text.Paragraph("Comprobante de Pago", titleFont));
            pdfDoc.Add(new iTextSharp.text.Paragraph(" \n"));
            pdfDoc.Add(new Paragraph("Información del pago:", subTitleFont));
            //fin
            //Parte 2
            var orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 10;
            orderInfoTable.SpacingAfter = 10;
            orderInfoTable.DefaultCell.Border = 0;
            if(tipo_pago_register.Text == "Cheque")
            {
                orderInfoTable.SetWidths(new int[] { 1, 8 });

                orderInfoTable.AddCell(new Phrase("Fecha:", boldTableFont));
                orderInfoTable.AddCell(fecha_pago_register.Text);
                orderInfoTable.AddCell(new Phrase("Forma de Pago:", boldTableFont));
                orderInfoTable.AddCell(tipo_pago_register.Text);
                orderInfoTable.AddCell(new Phrase("N° Cheque:", boldTableFont));
                orderInfoTable.AddCell(num_cheque_register.Text);
                orderInfoTable.AddCell(new Phrase("Total:", boldTableFont));
                orderInfoTable.AddCell(CalcularTotal(dt) + "");

            }
            else
            {
                orderInfoTable.SetWidths(new int[] { 1, 6 });

                orderInfoTable.AddCell(new Phrase("Fecha:", boldTableFont));
                orderInfoTable.AddCell(fecha_pago_register.Text);
                orderInfoTable.AddCell(new Phrase("Forma de Pago:", boldTableFont));
                orderInfoTable.AddCell(tipo_pago_register.Text);
                orderInfoTable.AddCell(new Phrase("Total:", boldTableFont));
                orderInfoTable.AddCell(CalcularTotal(dt) + "");

            }

            pdfDoc.Add(orderInfoTable);

            //AgregaTabla
            htmlparser.Parse(sr);
            //fin
            pdfDoc.Close();
            //
            Response.Write(pdfDoc);
            Response.End();

        }

        private void Llenar_GridView(string razon)
        {
            User_EN us = (User_EN)Session["user_session_data"];
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(us.NombreEmp);
            LogicaServicio ls = new LogicaServicio();
            LogicaMovimiento lm = new LogicaMovimiento();
            ArrayList lista = new ArrayList();
            lista = lm.MostrarMovimientosPorProveedor(razon, em.ID);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["imprimirPago"];
            //enlazas datatable a griedview
            Responsive.DataSource = dt;
            Responsive.DataBind();
        }

        protected void activarDesactivarControles(string caso)
        {
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            switch (caso){
                case "Cheque":
                    num_cheque_register.ReadOnly = false;
                    break;

                default:
                    num_cheque_register.ReadOnly = true;
                    break;
            }
        }

        public void ClickExportToPdf()
        {
            DataTable dt = (DataTable)Session["imprimirPago"];
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=listadoproveedores.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            Session["imprimirPago"] = null;
            pdfDoc.Open();
            //Obtiene imagen y lo inserta en el PDF
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/logo.png"));
            logo.SetAbsolutePosition(430, 700);
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(140);
            pdfDoc.Add(logo);
            //fin
            //Agrega Titulo

            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            pdfDoc.Add(new iTextSharp.text.Paragraph("Comprobante de Pago", titleFont));
            //fin
            //Parte 2
            var orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 10;
            orderInfoTable.SpacingAfter = 10;
            orderInfoTable.DefaultCell.Border = 0;
            orderInfoTable.SetWidths(new int[] { 1, 2 });

            orderInfoTable.AddCell(new Phrase("Total:", boldTableFont));
            orderInfoTable.AddCell(CalcularTotal(dt)+"");

            pdfDoc.Add(orderInfoTable);

            //AgregaTabla
            htmlparser.Parse(sr);
            //fin
            pdfDoc.Close();
            //
            Response.Write(pdfDoc);
            Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LongPassMin"></param>
        /// <param name="LongPassMax"></param>
        /// <returns></returns>
        protected string GenerarPass(int LongPassMin, int LongPassMax)
        {
            char[] ValueAfanumeric = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '!', '#', '$', '%', '&', '?', '¿' };
            Random ram = new Random();
            int LogitudPass = ram.Next(LongPassMin, LongPassMax);
            string Password = String.Empty;

            for (int i = 0; i < LogitudPass; i++)
            {
                int rm = ram.Next(0, 2);

                if (rm == 0)
                {
                    Password += ram.Next(0, 10);
                }
                else
                {
                    Password += ValueAfanumeric[ram.Next(0, 59)];
                }
            }

            return Password;
        }

        protected int CalcularTotal(DataTable dt)
        {
            int Total = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Total += Convert.ToInt32(dt.Rows[i]["Total"]);
            }
            return Total;
        }
    }
}