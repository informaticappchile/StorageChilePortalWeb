using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using System.IO;

namespace Presentacion
{
    public partial class Site1 : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Charset = "utf-8";
            User_EN user = (User_EN)Session["user_session_data"];
            LogicaOpciones lo = new LogicaOpciones();
            DateTime date1 = DateTime.Now;
            DateTime date2 = lo.getFecha();
            TimeSpan ts = date2.Subtract(date1);
            if (lo.getMantenimiento() && ts.Seconds <= 0)
            {
                DateTime date3 = lo.getFechaTermino();
                ts = date3.Subtract(date1);
                LbBienvenido.Text = "Estamos en mantenimiento. Estaremos disponible dentro de " + ts.Hours + " hrs y " + ts.Minutes + " minutos aprox.";
                LbBienvenido.Visible = true;
                if (user != null && user.IdPerfil != 18)
                {
                    user = null;
                    Session["user_session_data"] = null;
                }
            }
            if (user != null)
            {
                Link_IniciarSesion.Visible = false;
                Link_Who.Visible = false;
                Link_Editar_Perfil.Visible = true;
                LogicaEmpresa le = new LogicaEmpresa();
                Empresa_EN em = le.BuscarEmpresa(user.NombreEmp);
                LogicaServicio ls = new LogicaServicio();
                em.ListaServicio = ls.MostrarServiciosEmpresas(em);
                for (int i = 0; i < em.ListaServicio.Count; i++)
                {
                    if (((Servicio_EN)em.ListaServicio[i]).Verified)
                    {
                        switch (((Servicio_EN)em.ListaServicio[i]).Nombre)
                        {
                            case "Almacen":
                                Link_Almacen.Visible = true;
                                break;
                            case "Bodega":
                                Link_MyFiles.Visible = true;
                                Link_Feed.Visible = true;
                                break;
                            case "Digitalizacion":
                                Link_MyFiles.Visible = true;
                                Link_Feed.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                Link_Cerrar_Sesion.Visible = true;
                Barra_Secundaria.Visible = false;
                LbBienvenido.Text = "Bienvenido: " + user.NombreUsu;
                LbBienvenido.Visible = true;
                LogoEmpresa.Visible = true;
                System.Drawing.Image img = byte_a_Image(em.LogoEmpresa);
                try
                {
                    img.Save(Server.MapPath("~/logEmpresas/") + "logoEmp.png", System.Drawing.Imaging.ImageFormat.Png);
                    LogoEmpresa.ImageUrl = "~/logEmpresas/logoEmp.png";
                }
                catch(Exception ex)
                {
                    //error
                }
            }
            user = (User_EN)Session["user_session_admin"];
            if (user != null)
            {
                Link_IniciarSesion.Visible = false;
                Link_Who.Visible = false;
                Link_Administrar_Empresa.Visible = true;
                Link_Registrar_Empresa.Visible = true;
                Link_Registrar_Usuario.Visible = true;
                Link_Administrar_Usuarios.Visible = true;
                Link_Arcivos_Usuario.Visible = true;
                Link_Cerrar_Sesion.Visible = true;
                Barra_Secundaria.Visible = false;
                LbBienvenido.Text = "Bienvenido: " + user.NombreUsu;
                LbBienvenido.Visible = true;
                LogoEmpresa.Visible = true;
                LogoEmpresa.ImageUrl = "~/logEmpresas/ico-storage.png";
            }
        }

        protected void Busqueda_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            Response.Redirect("~/BusquedaArchivos.aspx?query=" + a.Text);
        }

        private System.Drawing.Image byte_a_Image(byte [] logo)
        {
            if (!(logo == null) || logo.Length > 1)
            {
                try
                {
                    //byte[] arr = File.ReadAllBytes(Server.MapPath("~/logEmpresas/") + "logoEmp.png");
                    MemoryStream ms = new MemoryStream(logo);
                    System.Drawing.Image resultado = System.Drawing.Image.FromStream(ms);
                    return resultado;
                }
                catch(Exception ex)
                {
                    //error
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}