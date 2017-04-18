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

        //Declaramos la contraseña del user en private
        private DateTime fechaMantenimiento;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public DateTime FechaMantenimiento
        {
            get { return fechaMantenimiento; }
            set { fechaMantenimiento = value; }
        }

        //Declaramos la contraseña del user en private
        private DateTime fechaTerminoMantenimiento;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public DateTime FechaTerminoMantenimiento
        {
            get { return fechaTerminoMantenimiento; }
            set { fechaTerminoMantenimiento = value; }
        }

        //Declaramos la contraseña del user en private
        private bool estadoMantenimiento;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public bool EstadoMantenimiento
        {
            get { return estadoMantenimiento; }
            set { estadoMantenimiento = value; }
        }

        //Declaramos la contraseña del user en private
        private string mensaje;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        public Opciones_EN()
        {
            contraseña = new byte[0];
            mensaje = "";
            fechaMantenimiento = DateTime.Now;
            estadoMantenimiento = false;
            fechaTerminoMantenimiento = DateTime.Now;
        }
    }
}
