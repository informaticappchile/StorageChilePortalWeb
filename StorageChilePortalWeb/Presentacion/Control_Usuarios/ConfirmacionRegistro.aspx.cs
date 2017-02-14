using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using User_EN_Class;

namespace Prensentacion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString.Keys[0] == "email")//Si efecticamente la url es correcta
                {
                    User_EN en = new User_EN();//Creamos un nuevo usuario
                    string email = Request.QueryString["email"].ToString();//Gracias a la url, podemos ver el usuario que ha recargado la pagina
                    en.Correo = email;//Ahora que ese usuario sea el del email
                    //en.confirmacionUsuario();//Confirmacion 

                }
            }
        }
    }
}