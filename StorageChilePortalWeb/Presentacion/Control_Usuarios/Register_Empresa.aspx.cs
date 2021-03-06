﻿using System;
using Entidades;
using Logica;
using System.Net.Mail;
using System.Web.Services;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Register_Empresa : System.Web.UI.Page
    {

        protected void InitInputClasses()
        {
            Registro_Empresa_ServicioBodega_Switch.InputAttributes.Add("class", "mdl-switch__input");
            Registro_Empresa_ServicioAlmacen_Switch.InputAttributes.Add("class", "mdl-switch__input");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            /*LogicaUsuario lu = new LogicaUsuario();
            User_EN userAutoLog = lu.BuscarUsuario("admin");
            Session["user_session_data"] = userAutoLog;*/
            InitInputClasses();
            if (Session["user_session_admin"] == null)
            {//Valida que existe usuario logueado.
                //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                StringBuilder sbMensaje = new StringBuilder();
                //Aperturamos la escritura de Javascript
                sbMensaje.Append("<script type='text/javascript'>");
                //Le indicamos al alert que mensaje va mostrar
                sbMensaje.AppendFormat("alert('{0}');", "Usted no tiene los privilegios para acceder aquí.Debe iniciar sesión");
                //Cerramos el Script
                sbMensaje.Append("window.location.href = window.location.protocol + '//' + window.location.hostname + ':'+ window.location.port + \"/Control_Usuarios/Login.aspx\";");
                sbMensaje.Append("</script>");
                //Registramos el Script escrito en el StringBuilder
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
            }
            User_EN en = (User_EN)Session["user_session_admin"];
        }

        

        /* Una vez el usuario ha rellenado todos los campos solicitados en el apartado del registro
         * correctamente, es decir, el email tiene formato de email, las contraseñas coinciden...proceemos a
         * guardar el usuario en la base de datos
         */
        protected void Button_Register_Click(object sender, EventArgs e)
        {
            EmailExistsError_Register.Visible = 
            RutExistsError_Register.Visible =
            DigiVerifacadorInValidError_Register.Visible =
            UsernameExistsError_Register.Visible = false; //Reiniciamos los errores para que si a la proxima le salen bien no les vuelva a salir
            Empresa_EN busqueda = new Empresa_EN();
            LogicaEmpresa le = new LogicaEmpresa();
            if (le.BuscarEmpresa(nombre_empresa_register.Text).NombreEmp != nombre_empresa_register.Text ) //Comprobamos que ese nombre de usuario ya este
            {
                if (le.BuscarEmpresa(correo_empresa_register.Text).Correo != correo_empresa_register.Text) //Comprobamos que ese correo ya este
                {
                    if (!validarRut(rut_empresa_register.Text))
                    {
                        DigiVerifacadorInValidError_Register.Visible = true;
                    }
                    if (le.BuscarEmpresa(rut_empresa_register.Text).Rut != rut_empresa_register.Text) //Comprobamos que ese correo ya este
                    {
                        Empresa_EN en = new Empresa_EN();//Si lo cumple todo, creamos un nuevo usuario
                        LogicaServicio lse = new LogicaServicio();
                        List<Servicio_EN> ls = lse.MostrarServicios();
                        en.NombreEmp = nombre_empresa_register.Text;//Con su nombre de usuario
                        en.Correo = correo_empresa_register.Text;//Con su correo
                        en.Rut = rut_empresa_register.Text;//Con su contrasenya
                        foreach (Servicio_EN s in ls)
                        {
                            if (s.Nombre == "Almacen")
                            {
                                s.Verified = Registro_Empresa_ServicioAlmacen_Switch.Checked;
                            }
                            if (s.Nombre == "Bodega")
                            {
                                s.Verified = Registro_Empresa_ServicioBodega_Switch.Checked;
                            }
                        }
                        en.LogoEmpresa = FileUpload1.FileBytes;
                        le.InsertarEmpresa(en);//Llamamos a InsertarUsuario de la cap EN, que se encaragra de insertarlo
                        Empresa_EN em = le.BuscarEmpresa(en.NombreEmp);
                        if (validarRegistroEmpresa(em))
                        {
                            lse.InsertarServicioEmpresa(em, ls);
                            string FileSaveUri = ConfigurationManager.AppSettings["ftp"];

                            string ftpUser = ConfigurationManager.AppSettings["ftp_user"];
                            string ftpPassWord = ConfigurationManager.AppSettings["ftp_password"];
                            if (Registro_Empresa_ServicioBodega_Switch.Checked)
                            {
                                try
                                {

                                    crearCarpeta(em.NombreEmp, FileSaveUri, ftpUser, ftpPassWord);
                                }
                                catch (Exception ex)
                                {
                                    //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                                    StringBuilder sbMensaje1 = new StringBuilder();
                                    //Aperturamos la escritura de Javascript
                                    sbMensaje1.Append("<script type='text/javascript'>");
                                    //Le indicamos al alert que mensaje va mostrar
                                    sbMensaje1.AppendFormat("alert('{0}');", "Ha ocurrido un error al reservar su espacio,comuníquese con el servicio de soporte para poder habilitarlo.");
                                    //Cerramos el Script
                                    sbMensaje1.Append("</script>");
                                    //Registramos el Script escrito en el StringBuilder
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje1.ToString());
                                }
                            }
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Se ha registrado a la empresa: " + en.NombreEmp);
                            //Cerramos el Script
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                            limpiar(this.Controls);
                        }
                        else
                        {
                            //Declaramos un StringBuilder para almacenar el alert que queremos mostrar
                            StringBuilder sbMensaje = new StringBuilder();
                            //Aperturamos la escritura de Javascript
                            sbMensaje.Append("<script type='text/javascript'>");
                            //Le indicamos al alert que mensaje va mostrar
                            sbMensaje.AppendFormat("alert('{0}');", "Ha ocurrido un error al registrar a la empresa, reintente mas tarde o comuníquese con el servicio de soporte.");
                            //Cerramos el Script
                            sbMensaje.Append("</script>");
                            //Registramos el Script escrito en el StringBuilder
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mensaje", sbMensaje.ToString());
                        }
                    }else RutExistsError_Register.Visible = true;
                }
                else EmailExistsError_Register.Visible = true;
            }
            else UsernameExistsError_Register.Visible = true;

        }

        private bool validarRegistroEmpresa(Empresa_EN e)
        {
            if (e == null)
            {
                return false;
            }
            else if (e.NombreEmp != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static string ReCaptcha_Key = "<" + ConfigurationManager.AppSettings["ReCaptcha_Key"] + ">";
        protected static string ReCaptcha_Secret = "<" + ConfigurationManager.AppSettings["ReCaptcha_Secret"] + ">";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        protected void crearCarpeta(string carpeta, string uri, string ftpUser, string ftpPassWord)
        {
            WebRequest request = WebRequest.Create(uri + carpeta);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
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

        private void limpiar(ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    text.Text = "";
                }
                else if (ctrl.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(ctrl.Controls);
            }
        }
    }
}