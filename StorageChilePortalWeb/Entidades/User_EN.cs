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
    public class User_EN
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

        //Declaramos el nombre del user en private
        private string nombre;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        //Declaramos el nombre de usuario del user en private
        private string nombreUsu;
        //Declaramos el nombre de usuario del user en public para poder utilizarlo
        public string NombreUsu
        {
            get { return nombreUsu; }
            set { nombreUsu = value; }
        }

        //Declaramos la contraseña del user en private
        private string contraseña;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        //Declaramos la visibilidad del perfil del user en private
        private int idPerfil;

        //Declaramos la visibilidad de perfil del user en public para poder utilizarlo
        public int IdPerfil
        {
            get { return idPerfil; }
            set { idPerfil = value; }
        }

        //Declaramos el verificado del user en private
        private string verified;

        //Declaramos el verificado del user en public para poder utilizarlo
        public string Verified
        {
            get { return verified; }
            set { verified = value; }
        }

        //Declaramos la fecha de registro del usuario
        private DateTime fechaRegistro;

        
        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }



        //Declaramos la fecha del último ingreso del usuario

        private DateTime ultimoIngreso;
        public DateTime UltimoIngreso
        {
            get { return ultimoIngreso; }
            set { ultimoIngreso = value; }
        }

        //Declaramos la cantidad de ingresos del usuario al sistema

        private DateTime cantIngreso;
        public DateTime CantIngreso
        {
            get { return cantIngreso; }
            set { cantIngreso = value; }
        }

        //Declaramos el constructor de la clase User_EN
        public User_EN()
        {
            id = 0;
            nombre = "";
            nombreUsu = "";
            correo = "";
            contraseña = "";
            idPerfil = 0;
            verified = "";
        }

    }
}
