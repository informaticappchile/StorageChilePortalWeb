using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace Entidades
{
    public class Opciones_EN
    {

        //Declaramos la contraseña del user en private
        private byte[] contraseña;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public byte[] Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        public Opciones_EN()
        {
            contraseña = new byte[0];
        }
    }
}
