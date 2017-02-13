using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using Admin_CAD_Class;

namespace Admin_EN_Class
{
    public class Admin_EN
    {
        public ArrayList MostrarUsuarios()
        {
            ArrayList a = new ArrayList();
            Admin_CAD c = new Admin_CAD();
            a = c.MostrarUsers();

            return a;
        }
    }
}
