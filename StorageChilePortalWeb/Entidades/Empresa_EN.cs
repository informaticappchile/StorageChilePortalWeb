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
    public class Empresa_EN
    {
        //Declaramos el id del user en private
        private int id;

        //Declaramos el id del user en public para poder utilizarlo
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //Declaramos el correo del user en private
        private string correo;

        //DEclaramos el correo en public para poder utilizarlo
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        private string rut;

        public string Rut
        {
            get { return rut; }
            set { rut = value; }
        }

        //Declaramos la fecha de registro del usuario
        private DateTime fechaRegistro;

        
        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }
        
        //Declaramos el nombre de la empresa del user en private
        private string nombreEmp;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string NombreEmp
        {
            get { return nombreEmp; }
            set { nombreEmp = value; }
        }

        //Declaramos el verificado del user en private
        private byte[] logoEmpresa;

        //Declaramos el verificado del user en public para poder utilizarlo
        public byte[] LogoEmpresa
        {
            get { return logoEmpresa; }
            set { logoEmpresa = value; }
        }

        ArrayList listaServicio;
        public ArrayList ListaServicio
        {
            get { return listaServicio; }
            set { listaServicio = value; }
        }


        //Declaramos el constructor de la clase User_EN
        public Empresa_EN()
        {
            id = 0;
            nombreEmp = "";
            rut = "";
            correo = "";
            fechaRegistro = DateTime.Now;
            listaServicio = new ArrayList();
        }

    }
}
