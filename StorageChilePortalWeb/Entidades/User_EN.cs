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
        private bool visibilidad_perfil;

        //Declaramos la visibilidad de perfil del user en public para poder utilizarlo
        public bool Visibilidad_perfil
        {
            get { return visibilidad_perfil; }
            set { visibilidad_perfil = value; }
        }

        //Declaramos el verificado del user en private
        private bool verified;

        //Declaramos el verificado del user en public para poder utilizarlo
        public bool Verified
        {
            get { return verified; }
            set { verified = value; }
        }

        //Declaramos el constructor de la clase User_EN
        public User_EN()
        {
            id = 0;
            nombre = "";
            nombreUsu = "";
            correo = "";
            contraseña = "";
            visibilidad_perfil = false;
            verified = false;
        }
    }
}
