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
    public partial class Editar_Producto : System.Web.UI.Page
    {

        /*
         * Esta funcion carga los datos del usuario en los TextBox para que el usuario pueda ver sus datos personales
         */
        protected void CargarDatos(Producto_EN en)
        {
            codigo_producto_editar.Text = en.CodProducto;//Con su nombre de usuario
            descripcion_editar.Text = en.Descripcion;//Con su correo
            cant_min_stock_editar.Text = ""+en.CantMinStock;//Con su contrasenya
            grupo_editar.Text = en.Grupo;
            unidad_editar.Text = en.UnidadMedida;
        }

        /*
         * Esta funcion esta conectada al boton de guardar cambios por si el usuario quiere cambiar algun
         * dato de su perfil
         */
        protected void Editar_Perfil_Guardar_Click(object sender, EventArgs e)
        {
            LogicaProducto lu = new LogicaProducto();
            this.user = Request["ID"].ToString();
            this.en = lu.BuscarProducto(user);
            this.en.CodProducto = codigo_producto_editar.Text;
            this.en.CantMinStock = Convert.ToInt32(cant_min_stock_editar.Text);
            this.en.Descripcion = descripcion_editar.Text.Replace('\'', '´').Trim();
            this.en.IdGrupo = lu.GetIdGrupo(grupo_editar.Text);
            this.en.IdMedidad = lu.GetIdUnidad(unidad_editar.Text);

            lu.actualizarProducto(en);
            if (ValidarCambios(en))
            {
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Se actualizaron los datos correctamente");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/AdministrarProducto.aspx\";");
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
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Almacen/AdministrarProducto.aspx\";");
                //Cerramos el Script
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
        }

        private string user;
        private Producto_EN en = new Producto_EN();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogicaUsuario lu = new LogicaUsuario();
            LogicaProducto le = new LogicaProducto();
            User_EN ad = (User_EN)Session["user_session_data"];
            if (ad != null && ad.IdPerfil != 1)
            {
                if (!Page.IsPostBack)
                {
                    if (Request["ID"] != null)
                    {
                        ArrayList lista = le.MostrarProductos(); if (lista.Count == 0)
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
                        this.user = Request["ID"].ToString();
                        this.en = le.BuscarProducto(user);
                        lista = le.MostrarGrupos();
                        grupo_editar.DataSource = lista;
                        grupo_editar.DataBind();
                        lista = le.MostrarUnidades();
                        unidad_editar.DataSource = lista;
                        unidad_editar.DataBind();
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

        protected bool ValidarCambios(Producto_EN u)
        {
            LogicaProducto lu = new LogicaProducto();
            Producto_EN en = lu.BuscarProducto(u.CodProducto);
            if (en.CodProducto != u.CodProducto) { return false; }
            if (en.CantMinStock != u.CantMinStock){return false; }
            if (en.Descripcion != u.Descripcion) { return false; }
            return true;
        }
    }
}