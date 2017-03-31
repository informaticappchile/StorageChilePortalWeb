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
    
    public partial class EditarMovimientos : System.Web.UI.Page
    {
        ArrayList areas = new ArrayList();
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
            if (!IsPostBack)
            {
                fecha_actual_register.Text = DateTime.Now.ToShortDateString();
                LogicaProveedor lpr = new LogicaProveedor();
                LogicaMovimiento lm = new LogicaMovimiento();
                ArrayList lista = lm.MostrarDocumentos();
                tipo_doc_register.DataSource = lista;
                tipo_doc_register.DataBind();
                lista = lpr.MostrarProveedores();
                razon_social_register.DataSource = lista;
                razon_social_register.DataTextField = "RazonSocial";
                razon_social_register.DataValueField = "RazonSocial";
                razon_social_register.DataBind();
                areas.Add("Cocina");
                areas.Add("Bar");
                area_register.DataSource = areas;
                area_register.DataBind();
                lista = lm.MostrarTipoMovimientos();
                tipo_mov_register.DataSource = lista;
                tipo_mov_register.DataBind();
            }
            
        }

        protected void clickCalcular(object sender, EventArgs e)
        {
            double totalSuma = 0;
            if (Session["dataMovimiento"] != null)
            {
                DataTable dt = (DataTable)Session["dataMovimiento"];
                for (int i =0; i < dt.Rows.Count; i++)
                {
                    totalSuma += Convert.ToDouble(dt.Rows[i]["Precio"]) * Convert.ToDouble(dt.Rows[i]["Cantidad"]);
                }
                neto_register.Text = totalSuma.ToString();
                iva_register.Text = (totalSuma * 0.19).ToString();
                totalSuma = totalSuma*1.19;
                totalSuma += Convert.ToDouble(ila_register.Text);
                totalSuma += Convert.ToDouble(flete_register.Text);
                total_register.Text = totalSuma.ToString();
            }
        }

        protected void clickIngresarMovimiento(object sender, EventArgs e)
        {
            NotNumDocError_Register.Visible = false;
            Llenar_GridView();
        }

        protected void clickGuardar(object sender, EventArgs e)
        {
            
        }

        protected void razonSocialChangeIndex(object sender, EventArgs e)
        {
            Session["dataMovimiento"] = null;
            Responsive.DataSource = null;
            Responsive.DataBind();
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            if (!IsPostBack) {
                lista = (ArrayList)razon_social_register.DataSource;
                string razon = ((Proveedor_EN)lista[0]).RazonSocial;
                lista = lp.MostrarProductosPorProveedor(razon);
            }else
            {
                lista = lp.MostrarProductosPorProveedor(razon_social_register.Text);
            }
            Session["EstadoCod"] = true;
            cod_prod_register.DataSource = lista;
            cod_prod_register.DataTextField = "CodProducto";
            cod_prod_register.DataValueField = "CodProducto";
            cod_prod_register.DataBind();

        }

        protected void codProductoChangeIndex(object sender, EventArgs e)
        {
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            Producto_EN producto = new Producto_EN();
            if ((bool)Session["EstadoCod"])
            {
                lista = (ArrayList)cod_prod_register.DataSource;
                string codigo = ((Producto_EN)lista[0]).CodProducto;
                producto = lp.BuscarProducto(codigo);
            }
            else
            {
                producto = lp.BuscarProducto(cod_prod_register.Text);
            }
            grupo_register.Text = producto.Grupo;
            unidad_register.Text = producto.UnidadMedida;
            descripcion_register.Text = producto.Descripcion;
            Session["EstadoCod"] = false;
        }

        protected void tipoMovChangeIndex(object sender, EventArgs e)
        {
            Session["dataMovimiento"] = null;
            Responsive.DataSource = null;
            Responsive.DataBind();
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            if (!IsPostBack)
            {
                lista = (ArrayList)tipo_mov_register.DataSource;
                string tipoMov = (string)lista[0];
                activarDesactivarControles(tipoMov);
            }
            else
            {
                activarDesactivarControles(tipo_mov_register.Text);
            }

        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void Llenar_GridView()
        {
            DataTable dt = new DataTable();
            if(Session["dataMovimiento"] == null)
            {
                dt.Columns.Add("CodProducto");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Grupo");
                dt.Columns.Add("UnidadMedida");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Observaciones");
            }
            else
            {
                dt = (DataTable)Session["dataMovimiento"];
            }

            //Agregar Datos    
            DataRow row = dt.NewRow();
            row["CodProducto"] = cod_prod_register.Text;
            row["Descripcion"] = descripcion_register.Text;
            row["Grupo"] = grupo_register.Text;
            row["UnidadMedida"] = unidad_register.Text;
            row["Cantidad"] = cant_register.Text;
            row["Precio"] = precio_register.Text;
            row["Observaciones"] = obs_register.Text;
            dt.Rows.Add(row);
            if (Convert.ToInt32(precio_register.Text) > 0 && Convert.ToInt32(cant_register.Text) > 0)
            {
                Session["dataMovimiento"] = dt;
                //enlazas datatable a griedview
                Responsive.DataSource = dt;
                Responsive.DataBind();
            }else
            {
                NotNumDocError_Register.Visible = true;
            }
        }

        protected void activarDesactivarControles(string caso)
        {
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            switch (caso){
                case "Compra":
                    responsable_register.ReadOnly = true;
                    razon_social_register.Enabled = true;
                    tipo_doc_register.Enabled = true;
                    num_doc_register.ReadOnly = false;
                    fecha_doc_register.ReadOnly = false;
                    lista = lp.MostrarProductosPorProveedor(razon_social_register.Text);
                    cod_prod_register.DataSource = lista;
                    cod_prod_register.DataTextField = "CodProducto";
                    cod_prod_register.DataValueField = "CodProducto";
                    cod_prod_register.DataBind();
                    break;

                case "Devolución Proveedor":
                    responsable_register.ReadOnly = true;
                    razon_social_register.Enabled = true;
                    tipo_doc_register.Enabled = true;
                    num_doc_register.ReadOnly = false;
                    fecha_doc_register.ReadOnly = false;
                    lista = lp.MostrarProductosPorProveedor(razon_social_register.Text);
                    cod_prod_register.DataSource = lista;
                    cod_prod_register.DataTextField = "CodProducto";
                    cod_prod_register.DataValueField = "CodProducto";
                    cod_prod_register.DataBind();
                    break;

                case "Merma":
                    responsable_register.ReadOnly = false;
                    razon_social_register.Enabled = false;
                    tipo_doc_register.Enabled = false;
                    num_doc_register.ReadOnly = true;
                    fecha_doc_register.ReadOnly = true;
                    Session["EstadoCod"] = true;
                    lista = lp.MostrarProductos();
                    cod_prod_register.DataSource = lista;
                    cod_prod_register.DataTextField = "CodProducto";
                    cod_prod_register.DataValueField = "CodProducto";
                    cod_prod_register.DataBind();
                    break;

                case "Producción":
                    responsable_register.ReadOnly = false;
                    razon_social_register.Enabled = false;
                    tipo_doc_register.Enabled = false;
                    num_doc_register.ReadOnly = true;
                    fecha_doc_register.ReadOnly = true;
                    Session["EstadoCod"] = true;
                    lp = new LogicaProducto();
                    lista = new ArrayList();
                    lista = lp.MostrarProductos();
                    cod_prod_register.DataSource = lista;
                    cod_prod_register.DataTextField = "CodProducto";
                    cod_prod_register.DataValueField = "CodProducto";
                    cod_prod_register.DataBind();
                    break;

                default:
                    break;
            }
        }

        protected void limpiar()
        {

        }
    }
}