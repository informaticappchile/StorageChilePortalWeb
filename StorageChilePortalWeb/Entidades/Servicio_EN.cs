using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Mail;


namespace Entidades
{
    public class Servicio_EN
    {
        //Declaramos el id del user en private
        private int id;

        //Declaramos el id del user en public para poder utilizarlo
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //Declaramos el nombre del user en private
        private string nombre;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        //Declaramos el verificado del user en private
        private bool verified;

        //Declaramos el verificado del user en public para poder utilizarlo
        public bool Verified
        {
            get { return verified; }
            set { verified = value; }
        }

        //Declaramos la fecha de registro del usuario
        private DateTime fechaInicio;

        
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }



        //Declaramos la fecha del último ingreso del usuario

        private DateTime fechaTermino;
        public DateTime FechaTermino
        {
            get { return fechaTermino; }
            set { fechaTermino = value; }
        }

        //Declaramos el constructor de la clase User_EN
        public Servicio_EN()
        {
            id = 0;
            nombre = "";
            verified = false;
        }

    }
}
