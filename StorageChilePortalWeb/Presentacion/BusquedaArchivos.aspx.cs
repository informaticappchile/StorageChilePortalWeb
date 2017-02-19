using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Entidades;

namespace Presentacion
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys() && Request.QueryString.Keys[0] == "query")
            {
                File_EN fi = new File_EN();
                //GridViewBusqueda.DataSource = fi.BuscarFiles(Request.QueryString["query"].ToString());
                GridViewBusqueda.DataBind();
            }
            else Response.Redirect("~/Inicio.aspx");
        }

        protected void Descarga_Boton_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            HyperLink h = (HyperLink)lb.FindControl("Descarga");
            string rutaDescarga = h.NavigateUrl;
            FileInfo fi = new FileInfo(rutaDescarga);
            if (fi.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                Response.AddHeader("Content-Length", fi.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.TransmitFile(fi.FullName);
                Response.End();
            }
        }

        protected void GridViewBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Texto_Descarga = (HyperLink)e.Row.FindControl("Descarga"); //Creamos el link para la descargar
                File_EN f = (File_EN) e.Row.DataItem;
                string rutaArchivo = "Files/" + f.Propietario + "/" + HttpUtility.HtmlDecode(f.Nombre); //Guardamos la ruta del archivo

                Image icono = (Image)e.Row.FindControl("icono_fichero");
                string extensionArchivo = Path.GetExtension(rutaArchivo);
                extensionArchivo = extensionArchivo.Substring(1, extensionArchivo.Length - 1); //quitar punto (carácter 0 del string)
                string rutaIcono = Server.MapPath("/styles/format-icons/" + extensionArchivo + ".svg");
                icono.ImageUrl = File.Exists(rutaIcono) ? "~/styles/format-icons/" + extensionArchivo + ".svg" : "~/styles/format-icons/file.svg";

                Texto_Descarga.NavigateUrl = Server.MapPath(rutaArchivo); //Copiamos la ruta del archivo a la URL para descargar
            }
        }
    }
}