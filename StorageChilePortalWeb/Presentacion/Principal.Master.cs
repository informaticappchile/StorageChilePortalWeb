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
            if (user != null)
            {
                if (user.IdPerfil == 1)
                {
                    Link_Feed.Visible = false;
                    Link_MyFiles.Visible = false;
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
                else if (user.IdPerfil == 2)
                {
                    Link_Almacen.Visible = true;
                    Link_Cerrar_Sesion.Visible = true;
                    Barra_Secundaria.Visible = false;
                    LbBienvenido.Text = "Bienvenido: " + user.NombreUsu;
                    LbBienvenido.Visible = true;
                    LogoEmpresa.Visible = true;
                    LogicaEmpresa le = new LogicaEmpresa();
                    Empresa_EN em = le.BuscarEmpresa(user.NombreEmp);
                    System.Drawing.Image img = byte_a_Image(em.LogoEmpresa);
                    img.Save(Server.MapPath("~/logEmpresas/") + "logoEmp.png", System.Drawing.Imaging.ImageFormat.Png);
                    LogoEmpresa.ImageUrl = "~/logEmpresas/logoEmp.png";
                }
            }
        }

        protected void Busqueda_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            Response.Redirect("~/BusquedaArchivos.aspx?query=" + a.Text);
        }

        private System.Drawing.Image byte_a_Image(byte [] logo)
        {
            if (!(logo == null) || logo.Length > 0)
            {
                byte[] arr = File.ReadAllBytes(Server.MapPath("~/logEmpresas/") + "falabella.png");
                MemoryStream ms = new MemoryStream(logo);
                System.Drawing.Image resultado = System.Drawing.Image.FromStream(ms);
                return resultado;
            }
            else
            {
                return null;
            }
        }
    }
}