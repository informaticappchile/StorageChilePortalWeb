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

    public partial class Movimientos : System.Web.UI.Page
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
                try
                {
                    fecha_actual_register.Text = DateTime.Now.ToShortDateString();
                    LogicaProveedor lpr = new LogicaProveedor();
                    LogicaMovimiento lm = new LogicaMovimiento();
                    lista = lm.MostrarDocumentos();
                    tipo_doc_register.DataSource = lista;
                    tipo_doc_register.DataBind();
                    lista = lpr.MostrarProveedoresConProductos(em.ID);
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
                catch (Exception ex)
                {
                    //No hay producto o proveedor
                }
            }

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            fecha_doc_register.Text = Calendar1.SelectedDate.ToShortDateString();
        }

        protected void clickConvertir(object sender, EventArgs e)
        {
            double conversion = 0;
            double precio = 0;
            if (Convert.ToDouble(unidadestxt.Text) > 0 && Convert.ToDouble(equivalenciatxt.Text) > 0)
            {
                conversion = (Convert.ToDouble(equivalenciatxt.Text) * Convert.ToDouble(unidadestxt.Text));
                precio = (Convert.ToDouble(preciotxt.Text) / Convert.ToDouble(equivalenciatxt.Text));
                cant_register.Text = ((int)conversion).ToString();
                precio_register.Text = ((int)precio).ToString();
                unidadestxt.Text = "0";
                equivalenciatxt.Text = "0";
                preciotxt.Text = "0";
            }
        }

        protected void clickCalcular(object sender, EventArgs e)
        {
            double totalSuma = 0;
            if (Session["dataMovimiento"] != null)
            {
                DataTable dt = (DataTable)Session["dataMovimiento"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalSuma += Convert.ToDouble(dt.Rows[i]["Precio"]) * Convert.ToDouble(dt.Rows[i]["Cantidad"]);
                }
                neto_register.Text = totalSuma.ToString();
                iva_register.Text = (totalSuma * 0.19).ToString();
                totalSuma = totalSuma * 1.19;
                totalSuma += Convert.ToDouble(ila_register.Text);
                totalSuma += Convert.ToDouble(flete_register.Text);
                total_register.Text =  (Convert.ToInt32(totalSuma).ToString());
            }
        }

        protected void clickIngresarMovimiento(object sender, EventArgs e)
        {
            NotPositiveNumError.Visible = false;
            Llenar_GridView();
        }

        protected void clickGuardar(object sender, EventArgs e)
        {
            NotFechaDocError_Register.Visible =
            NotNumDocError_Register.Visible = false;

            
            try
            {
                LogicaProducto lp = new LogicaProducto();
                LogicaProveedor lpr = new LogicaProveedor();
                LogicaMovimiento lm = new LogicaMovimiento();
                Movimiento_EN m = new Movimiento_EN();
                Producto_EN p = new Producto_EN();
                Proveedor_EN pr = new Proveedor_EN();

                m.IdTipoMovimiento = lm.GetIdTipoMovimiento(tipo_mov_register.Text);
                m.Area = "Sin Area";
                p = lp.BuscarProducto(cod_prod_register.Text);

               
                switch (tipo_mov_register.Text)
                {
                       

                    case "Devolución Proveedor":
                        if (fecha_doc_register.Text == "")
                        {
                            NotFechaDocError_Register.Visible = true;
                            return;
                        }
                        if (num_doc_register.Text == "")
                        {
                            NotNumDocError_Register.Visible = true;
                            return;
                        }
                        if (p.Stock == 0)
                        {
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene stock disponible con este producto para realizar una devolución");
                            //Cerramos el Script                          
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                            return;
                        }
                            m.FechaMovimiento = Convert.ToDateTime(fecha_actual_register.Text);
                            m.FechaDocumento = Convert.ToDateTime(fecha_doc_register.Text);
                            m.IdDocumento = lm.GetIdDocumento(tipo_doc_register.Text);
                            m.NumDocumento = Convert.ToInt32(num_doc_register.Text);
                            m.Total = Convert.ToInt32(total_register.Text);
                            
   
                        break;

                    case "Compra":
                        if (fecha_doc_register.Text == "")
                        {
                            NotFechaDocError_Register.Visible = true;
                            return;
                        }
                        if (num_doc_register.Text == "")
                        {
                            NotNumDocError_Register.Visible = true;
                            return;
                        }

                        m.FechaMovimiento = Convert.ToDateTime(fecha_actual_register.Text);
                            m.FechaDocumento = Convert.ToDateTime(fecha_doc_register.Text);
                            m.IdDocumento = lm.GetIdDocumento(tipo_doc_register.Text);
                            m.NumDocumento = Convert.ToInt32(num_doc_register.Text);
                            m.Total = Convert.ToInt32(total_register.Text);

                        break;

                    case "Merma":
                        if (p.Stock == 0)
                        {
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene stock disponible con este producto para realizar una merma");
                            //Cerramos el Script                          
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                            return;
                        }
                        m.Responsable = responsable_register.Text;

                        break;

                    case "Producción":
                        if (p.Stock == 0)
                        {
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene stock disponible con este producto para realizar una producción");
                            //Cerramos el Script                          
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                            return;
                        }
                        m.Responsable = responsable_register.Text;
                          
                        break;
                    default:
                        break;
                }
               
                string Id = GenerarPass(5, 15);

                Movimiento_EN mov = lm.BuscarMovimiento(Id);

                while (mov.ID != "")
                {
                    Id = GenerarPass(5, 15);
                    mov = lm.BuscarMovimiento(Id);
                }

                m.ID = Id;

                lm.InsertarMovimiento(m);

                mov = lm.BuscarMovimiento(Id);

                if (mov.ID != "")
                {
                    List<Movimiento_EN> listaMovimientos = new List<Movimiento_EN>();
                    DataTable dt = (DataTable)Session["dataMovimiento"];
                    User_EN en = (User_EN)Session["user_session_data"];
                    LogicaEmpresa le = new LogicaEmpresa();
                    Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
                    pr = lpr.BuscarProveedorVendedorEmpresa(em,razon_social_register.Text);
                    m.IdProveedor = pr.ID;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Movimiento_EN aux = new Movimiento_EN();
                        p = lp.BuscarProductoPorCodigo(dt.Rows[i]["CodProducto"].ToString(), em);

                        switch (tipo_mov_register.Text)
                        {
                            case "Compra":
                                p.Stock += Convert.ToInt32(dt.Rows[i]["Cantidad"].ToString());
                                break;

                            case "Devolución Proveedor":
                                p.Stock -= Convert.ToInt32(dt.Rows[i]["Cantidad"].ToString());
                                break;

                            case "Merma":
                                p.Stock -= Convert.ToInt32(dt.Rows[i]["Cantidad"].ToString());
                                break;

                            case "Producción":
                                p.Stock -= Convert.ToInt32(dt.Rows[i]["Cantidad"].ToString());
                                break;

                            default:
                                break;
                        }

                        lp.actualizarProducto(p);
                        aux.ID = m.ID;
                        aux.IdProveedor = pr.ID;
                        aux.IdProducto = p.ID;
                        aux.PrecioUnitario = Convert.ToInt32(dt.Rows[i]["Precio"].ToString());
                        aux.Observaciones = dt.Rows[i]["Observaciones"].ToString();
                        aux.Cantidad = Convert.ToInt32(dt.Rows[i]["Cantidad"].ToString());

                        listaMovimientos.Add(aux);
                    }

                    lm.InsertarMovimientoProductoProveedor(listaMovimientos, em);
                    limpiar(this.Controls);
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
            catch (Exception ex)
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error critico al ingresar los datos. Reintente más tarde " +
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
            User_EN u = (User_EN)Session["user_session_data"];
            Session["dataMovimiento"] = null;
            Responsive.DataSource = null;
            Responsive.DataBind();
            LogicaProducto lp = new LogicaProducto();
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(u.NombreEmp);
            ArrayList lista = new ArrayList();
            if (!IsPostBack)
            {
                lista = (ArrayList)razon_social_register.DataSource;
                string razon = ((Proveedor_EN)lista[0]).RazonSocial;
                lista = lp.MostrarProductosPorProveedor(razon, em.ID);
                Session["EstadoCod"] = true;
            }
            else
            {
                lista = lp.MostrarProductosPorProveedor(razon_social_register.Text, em.ID);
            }
            descripcion_register.DataSource = lista;
            descripcion_register.DataTextField = "Descripcion";
            descripcion_register.DataValueField = "Descripcion";
            descripcion_register.DataBind();

        }

        protected void descripcionChangeIndex(object sender, EventArgs e)
        {
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            User_EN u = (User_EN)Session["user_session_data"];
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(u.NombreEmp);
            Producto_EN producto = new Producto_EN();
            lista = (ArrayList)descripcion_register.DataSource;
            if ((bool)Session["EstadoCod"])
            {
                string codigo = ((Producto_EN)lista[0]).CodProducto;
                lista = (ArrayList)razon_social_register.DataSource;
                string razon = ((Proveedor_EN)lista[0]).RazonSocial;
                producto = lp.BuscarProductoPorCodigo(codigo, em, razon);
            }
            else if ((bool)Session["EstadoCodPM"])
            {
                producto = lp.BuscarProductoPorCodigo(descripcion_register.Text, em);
            }
            else if (lista != null && lista.Count > 0)
            {
                producto = lp.BuscarProductoPorCodigo(((Producto_EN)lista[0]).Descripcion, em, razon_social_register.Text);
            }
            else
            {
                producto = lp.BuscarProductoPorCodigo(descripcion_register.Text, em, razon_social_register.Text);
            }
            grupo_register.Text = producto.Grupo;
            unidad_register.Text = producto.UnidadMedida;
            cod_prod_register.Text = producto.CodProducto;
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

        private void Llenar_GridView()
        {
            int pos = 0;
            DataTable dt = new DataTable();
            if (Session["dataMovimiento"] == null)
            {
                dt.Columns.Add("N");
                dt.Columns.Add("CodProducto");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Grupo");
                dt.Columns.Add("UnidadMedida");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Observaciones");
                pos++;
            }
            else
            {
                dt = (DataTable)Session["dataMovimiento"];
                pos = Convert.ToInt32(dt.Rows[dt.Rows.Count-1]["N"]) + 1;
            }

            //Agregar Datos    
            DataRow row = dt.NewRow();
            row["N"] = pos;
            row["CodProducto"] = cod_prod_register.Text;
            row["Descripcion"] = descripcion_register.Text;
            row["Grupo"] = grupo_register.Text;
            row["UnidadMedida"] = unidad_register.Text;
            row["Cantidad"] = cant_register.Text;
            row["Precio"] = precio_register.Text;
            row["Observaciones"] = obs_register.Text;
            dt.Rows.Add(row);
            if (Convert.ToInt32(precio_register.Text) >= 0 && Convert.ToInt32(cant_register.Text) > 0)
            {
                Session["dataMovimiento"] = dt;
                //enlazas datatable a griedview
                Responsive.DataSource = dt;
                Responsive.DataBind();
            }
            else
            {
                NotPositiveNumError.Visible = true;
            }
        }

        protected void activarDesactivarControles(string caso)
        {
            User_EN u = (User_EN)Session["user_session_data"];
            LogicaProducto lp = new LogicaProducto();
            ArrayList lista = new ArrayList();
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(u.NombreEmp);
            switch (caso)
            {
                case "Compra":
                    responsable_register.ReadOnly = true;
                    razon_social_register.Enabled = true;
                    tipo_doc_register.Enabled = true;
                    num_doc_register.Text = "";
                    num_doc_register.ReadOnly = false;
                    area_register.Enabled = false;
                    fecha_doc_register.Text = "";
                    responsable_register.Text = "";
                    Calendar1.Visible = true;
                    Session["EstadoCodPM"] = false;
                    lista = lp.MostrarProductosPorProveedor(razon_social_register.Text, em.ID);
                    descripcion_register.DataSource = lista;
                    descripcion_register.DataTextField = "Descripcion";
                    descripcion_register.DataValueField = "Descripcion";
                    descripcion_register.DataBind();
                    break;

                case "Devolución Proveedor":
                    responsable_register.ReadOnly = true;
                    num_doc_register.Text = "";
                    razon_social_register.Enabled = true;
                    tipo_doc_register.Enabled = true;
                    responsable_register.Text = "";
                    num_doc_register.ReadOnly = false;
                    fecha_doc_register.Text = "";
                    area_register.Enabled = false;
                    Calendar1.Visible = true;
                    Session["EstadoCodPM"] = false;
                    lista = lp.MostrarProductosPorProveedor(razon_social_register.Text, em.ID);
                    descripcion_register.DataSource = lista;
                    descripcion_register.DataTextField = "Descripcion";
                    descripcion_register.DataValueField = "Descripcion";
                    descripcion_register.DataBind();
                    break;

                case "Merma":
                    responsable_register.ReadOnly = false;
                    razon_social_register.Enabled = false;
                    tipo_doc_register.Enabled = false;
                    num_doc_register.Text = "";
                    num_doc_register.ReadOnly = true;
                    responsable_register.Text = "";
                    fecha_doc_register.ReadOnly = true;
                    fecha_doc_register.Text = "";
                    Calendar1.Visible = false;
                    Session["EstadoCodPM"] = true;
                    lista = lp.MostrarProductosPorEmpresa(em);
                    descripcion_register.DataSource = lista;
                    descripcion_register.DataTextField = "Descripcion";
                    descripcion_register.DataValueField = "Descripcion";
                    descripcion_register.DataBind();
                    break;

                case "Producción":
                    responsable_register.ReadOnly = false;
                    razon_social_register.Enabled = false;
                    tipo_doc_register.Enabled = false;
                    num_doc_register.ReadOnly = true;
                    responsable_register.Text = "";
                    num_doc_register.Text = "";
                    fecha_doc_register.ReadOnly = true;
                    fecha_doc_register.Text = "";
                    Calendar1.Visible = false;
                    area_register.Enabled = true;
                    Session["EstadoCodPM"] = true;
                    lp = new LogicaProducto();
                    lista = new ArrayList();
                    lista = lp.MostrarProductosPorEmpresa(em);
                    descripcion_register.DataSource = lista;
                    descripcion_register.DataTextField = "Descripcion";
                    descripcion_register.DataValueField = "Descripcion";
                    descripcion_register.DataBind();
                    break;

                default:
                    break;
            }
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

        /// <summary>
        /// Metodo para eliminar un usuario
        /// No elimina cuando estan vinculados a un servicio,
        /// a un ticket o a una respuesta a un ticket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Responsive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DEL")
            {
                DataTable dt = (DataTable)Session["dataMovimiento"];
                int a = Convert.ToInt32(e.CommandArgument.ToString()) - 1;
                dt.Rows.RemoveAt(a);
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    dt.Rows[i-1]["N"] = i;
                }
                if (dt.Rows.Count == 0)
                {
                    Session["dataMovimiento"] = null;
                    Responsive.DataSource = null;
                    Responsive.DataBind();
                }
                else
                {
                    Session["dataMovimiento"] = dt;
                    Responsive.DataSource = dt;
                    Responsive.DataBind();
                }
            }
        }

        private void limpiar(ControlCollection controles)
        {
            Responsive.DataSource = null;
            Responsive.DataBind();
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    if (text.ID == responsable_register.ID)
                    {
                        text.Text = "";
                    }
                    else if (text.ID == num_doc_register.ID)
                    {
                        text.Text = "";
                    }
                    else if (text.ID == obs_register.ID)
                    {
                        text.Text = "";
                    }
                    else if (text.ID == fecha_doc_register.ID)
                    {
                        text.Text = "";
                    }
                    else if (text.ID == fecha_actual_register.ID || text.ID == cod_prod_register.ID
                        || text.ID == unidad_register.ID || text.ID == "Busqueda" || text.ID == grupo_register.ID)
                    {
                    }
                    else if (text.ID == unidadtxt.ID)
                    {
                        text.Text = "1";
                    }
                    else
                    {
                        text.Text = "0";
                    }
                }
                else if (ctrl.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(ctrl.Controls);
            }
        }
    }
}