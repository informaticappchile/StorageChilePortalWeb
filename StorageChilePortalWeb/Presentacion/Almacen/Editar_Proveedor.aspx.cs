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
            UsernameExistsError_Register.Visible = false;
            
            LogicaProveedor lu = new LogicaProveedor();
            if (lu.BuscarProveedor(razon_social_editar.Text.Replace('\'', '´').Trim()).RazonSocial == razon_social_editar.Text.Replace('\'', '´').Trim() &&
                lu.BuscarProveedor(rut_empresa_editar.Text).RazonSocial != razon_social_editar.Text.Replace('\'', '´').Trim())
            {
                UsernameExistsError_Register.Visible = true;
                return;
            }
            this.user = Request["ID"].ToString();
            User_EN en = (User_EN)Session["user_session_data"];
            LogicaEmpresa le = new LogicaEmpresa();
            Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
            this.en = lu.BuscarProveedorVendedorEmpresa(em,user);
            this.en.RazonSocial = razon_social_editar.Text.Replace('\'', '´').Trim();
            this.en.Rut = rut_empresa_editar.Text;
            this.en.Vendedor = vendedor_name_editar.Text.Replace('\'', '´').Trim();
            this.en.Ciudad = ciudad_editar.Text;
            this.en.Direccion = direccion_editar.Text.Replace('\'', '´').Trim();
            this.en.Fono = fono_editar.Text;
            this.en.IdCiudad = lu.GetIdCiudad(ciudad_editar.Text);
            LogicaServicio lse = new LogicaServicio();
            List<Servicio_EN> ls = lse.MostrarServicios();

            lu.actualizarProveedor(this.en);
            lu.actualizarVendedor(this.en);
            if (ValidarCambios(this.en))
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
                        ArrayList lista = le.MostrarProveedoresVendedorEmpresa(em);
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

                        switch (en.NombrePerfil)
                        {
                            case "Administrador":
                                break;
                            case "AdministradorAlmacen":
                                break;
                            case "UsuarioAlmacen":
                                break;
                            case "UsuarioAlmacenCrearProveedor":
                                break;
                            case "Usuario":
                                break;
                            default:
                                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                StringBuilder sbMensaje = new StringBuilder();
                                //Aperturamos la escritura de Javascript
                                sbMensaje.Append("<script type='text/javascript'>");
                                //Le indicamos al alert que mensaje va mostrar
                                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene permisos para ingresar aquí.");
                                //Cerramos el Script
                                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/MenuAlmacen.aspx\";");
                                sbMensaje.Append("</script>");
                                //Registramos el Script escrito en el StringBuilder
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                                break;

                        }
                        this.user = Request["ID"].ToString();
                        this.en = le.BuscarProveedorVendedorEmpresa(em,user);
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
            return true;
        }

        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}