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
    
    public partial class Pagos : System.Web.UI.Page
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
                try
                {
                    fecha_pago_register.Text = DateTime.Now.ToShortDateString();
                    LogicaProveedor lpr = new LogicaProveedor();
                    LogicaPago lpa = new LogicaPago();
                    lista = lpa.MostrarTipoPagos();
                    tipo_pago_register.DataSource = lista;
                    tipo_pago_register.DataBind();
                    lista = lpr.MostrarProveedoresVendedorEmpresa(em);
                    razon_social_register.DataSource = lista;
                    razon_social_register.DataTextField = "RazonSocial";
                    razon_social_register.DataValueField = "RazonSocial";
                    razon_social_register.DataBind();
                }
                catch (Exception ex)
                {
                }
            }
            
        }

        protected void clickGuardar(object sender, EventArgs e)
        {
            NotNumChequeError_Register.Visible = false;
            if (num_cheque_register.Text == "0" && tipo_pago_register.Text == "Cheque")
            {
                NotNumChequeError_Register.Visible = true;
                return;
            }
            LogicaPago lp = new LogicaPago();
            LogicaProveedor lpr = new LogicaProveedor();
            LogicaMovimiento lm = new LogicaMovimiento();
            Movimiento_EN m = new Movimiento_EN();
            Pago_EN p = new Pago_EN();
            Proveedor_EN pr = new Proveedor_EN();
            string Id = GenerarPass(5, 15);

            Pago_EN pag = lp.BuscarPago(Id);

            while (pag.ID != "")
            {
                Id = GenerarPass(5, 15);
                pag = lp.BuscarPago(Id);
            }

            p.ID = Id;
            pr = lpr.BuscarProveedor(razon_social_register.Text);
            int idTipoPago = lp.GetIdTipoPago(tipo_pago_register.Text);
            p.IdProveedor = pr.ID;
            p.IdTipoPago = idTipoPago;
            p.NumCheque = Convert.ToInt32(num_cheque_register.Text);
            p.EstadoComprobante = true;
            p.FechaComprobante = DateTime.Now;

            lp.InsertarPago(p);

            pag = lp.BuscarPago(Id);
            if(pag.ID != "")
            {
                DataTable dt = (DataTable)Session["dataPago"];
                int contador = 0;
                foreach (GridViewRow row in Responsive.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                        {
                            m = lm.BuscarMovimiento(dt.Rows[contador]["IdMovimiento"].ToString());
                            m.IdPago = Id;
                            lm.actualizarMovimiento(m);
                        }
                        contador++;
                    }
                }
                ClickExportToPdf();
                Session["dataPago"] = null;
                Llenar_GridView(razon_social_register.Text);
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se han ingresado los datos exitosamente");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());

            }
            else
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error al ingresar los datos. Reintente más tarde " +
                    "o pongase en contacto con el servicio de soporte.");
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/MenuAlmacen.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }

        }

        protected void razonSocialChangeIndex(object sender, EventArgs e)
        {

            Session["dataPago"] = null;
            Responsive.DataSource = null;
            Responsive.DataBind();
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            if (!IsPostBack)
            {
                lista = (ArrayList)razon_social_register.DataSource;
                string razon = ((Proveedor_EN)lista[0]).RazonSocial;
                Llenar_GridView(razon);
            }
            else
            {
                Llenar_GridView(razon_social_register.Text);
            }

        }

        protected void tipoPagoChangeIndex(object sender, EventArgs e)
        {
            ArrayList lista = new ArrayList();
            if (!IsPostBack)
            {
                num_cheque_register.Text = "0";
                lista = (ArrayList)tipo_pago_register.DataSource;
                string caso = (string)lista[0];
                activarDesactivarControles(caso);
            }
            else
            {
                num_cheque_register.Text = "0";
                activarDesactivarControles(tipo_pago_register.Text);
            }
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
            if(Session["dataPago"] == null)
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
                if (((Movimiento_EN)lista[i]).IdPago == "0")
                {
                    //Agregar Datos    
                    DataRow row = dt.NewRow();
                    row["RazonSocial"] = ((Movimiento_EN)lista[i]).RazonSocial;
                    row["TipoDoc"] = ((Movimiento_EN)lista[i]).Documento;
                    row["NumDoc"] = ((Movimiento_EN)lista[i]).NumDocumento;
                    row["FechaDocumento"] = ((Movimiento_EN)lista[i]).FechaDocumento;
                    row["Total"] = ((Movimiento_EN)lista[i]).Total;
                    ArrayList obs = new ArrayList();
                    obs = lm.MostrarObservaciones(razon, ((Movimiento_EN)lista[i]).ID, us.IdEmpresa);
                    string observaciones = "";
                    for (int j = 0; j < obs.Count; j++)
                    {
                        observaciones += "- " + (string)obs[j] + "\n";
                    }
                    row["Observaciones"] = observaciones;
                    row["IdMovimiento"] = ((Movimiento_EN)lista[i]).ID;
                    dt.Rows.Add(row);
                }
            }
            Session["dataPago"] = dt;
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
            DataTable dt = (DataTable)Session["dataPago"];

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
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
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

        protected void limpiar()
        {

        }
    }
}