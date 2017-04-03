using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;

namespace Presentacion
{
    public partial class Editar_Proveedor : System.Web.UI.Page
    {
        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(Proveedor_EN en)
        {
            vendedor_name_editar.Text = en.Vendedor;
            razon_social_editar.Text = en.RazonSocial;//Con su nombre de usuario
            rut_empresa_editar.Text = en.Rut;//Con su correo
            direccion_editar.Text= en.Direccion;//Con su contrasenya
            ciudad_editar.Text = en.Ciudad;
            fono_editar.Text = en.Fono;
        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            LogicaProveedor lu = new LogicaProveedor();
            this.user = Request["ID"].ToString();
            this.en = lu.BuscarProveedor(user);
            this.en.RazonSocial = razon_social_editar.Text.Replace('\'', '´').Trim();
            this.en.Rut = rut_empresa_editar.Text;
            this.en.Vendedor = vendedor_name_editar.Text.Replace('\'', '´').Trim();
            this.en.Ciudad = ciudad_editar.Text;
            this.en.Direccion = direccion_editar.Text.Replace('\'', '´').Trim();
            this.en.Fono = fono_editar.Text;
            this.en.IdCiudad = lu.GetIdCiudad(ciudad_editar.Text);
            LogicaServicio lse = new LogicaServicio();
            List<Servicio_EN> ls = lse.MostrarServicios();

            lu.actualizarProveedor(en);
            if (ValidarCambios(en))
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se actualizaron los datos correctamente");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/AdministrarProveedor.aspx\";");
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
                sbMensaje.AppendFormat("alert('{0}');", "A ocurrido un error al actualizar los datos. Reintente más tarde "+
                    "o pongase en contacto con el servicio de soporte.");
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/AdministrarProveedor.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        private string user;
        private Proveedor_EN en = new Proveedor_EN();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaUsuario lu = new LogicaUsuario();
            LogicaProveedor le = new LogicaProveedor();
            User_EN ad = (User_EN)Session["user_session_data"];
            if (ad != null)
            {
                if (!Page.IsPostBack)
                {
                    if (Request["ID"] != null)
                    {

                        User_EN en = (User_EN)Session["user_session_data"];
                        LogicaEmpresa l = new LogicaEmpresa();
                        Empresa_EN em = l.BuscarEmpresa(en.NombreEmp);
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
                        ArrayList lista = le.MostrarProveedores();
                        if (lista.Count == 0)
                        {
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene proveedores disponibles en el sistema. Por favor registre uno.");
                            //Cerramos el Script
                            sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/RegisterProveedor.aspx\";");
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                        }
                        this.user = Request["ID"].ToString();
                        this.en = le.BuscarProveedor(user);
                        LogicaProveedor lp = new LogicaProveedor();
                        lista = lp.MostrarCiudades();
                        ciudad_editar.DataSource = lista;
                        ciudad_editar.DataTextField = "Nombre";
                        ciudad_editar.DataValueField = "Nombre";
                        ciudad_editar.DataBind();
                        CargarDatos(this.en);
                    }
                }

            }
          else
           {
                //Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Debe iniciar sesión o usted no tiene privilegios para acceder aqui");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        protected bool ValidarCambios(Proveedor_EN u)
        {
            LogicaProveedor lu = new LogicaProveedor();
            Proveedor_EN en = lu.BuscarProveedor(u.RazonSocial);
            if (en.RazonSocial != u.RazonSocial) { return false; }
            if (en.Rut != u.Rut){return false; }
            if (en.IdCiudad != u.IdCiudad) { return false; }
            return true;
        }
    }
}