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
    public class Personal_EN
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string rut;

        public string Rut
        {
            get { return rut; }
            set { rut = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public Personal_EN()
        {
            id = 0;
            nombre = "";
            rut = "";
        }
    }
}
