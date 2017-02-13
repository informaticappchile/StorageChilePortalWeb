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

using User_CAD_Class;

namespace User_EN_Class
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

        //Declaramos la edad del user en private
        private short edad;

        //Declaramos la edad del user en public para poder utilizarlo
        public short Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        //Declaramos el genero del user en private
        private bool? genero;

        //Declaramos el genero del user en public para poder utilizarlo
        public bool? Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        //Declaramos la localidad del user en private
        private string localidad;

        //Declaramos la localidad del user en public para poder utilizarlo
        public string Localidad
        {
            get { return localidad; }
            set { localidad = value; }
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

        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        public void InsertarUsuario()
        {
            User_CAD userCad = new User_CAD();
            userCad.InsertarUser(this);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarUsuario()
        {
            ArrayList a = new ArrayList();
            User_CAD c = new User_CAD();
            a = c.MostrarUser(this);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public void BorrarUsuario()
        {
            User_CAD userDelete = new User_CAD();
            userDelete.BorrarUser(this);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public User_EN BuscarUsuario(string clave)
        {
            User_CAD busqueda = new User_CAD();
            User_EN usuarioBuscado = busqueda.BuscarUser(clave);
            return usuarioBuscado;
        }

        //Declaramos la funcion listar amigos donde llama al cad correspondiente
        public ArrayList ListarAmigos()
        {
            ArrayList a = new ArrayList();
            User_CAD c = new User_CAD();
            a = c.ListarAmigos();

            return a;
        }
        //Declaramos la funcion leer usuario donde llama al cad correspondiente
        public void LeerUsuario()
        {
            User_CAD cad = new User_CAD();
            User_EN en = new User_EN();
            en = cad.LeerUser(this);
            if (en != null)
            {
                id = en.id;
                nombre = en.nombre;
                nombreUsu = en.nombreUsu;
                correo = en.correo;
                edad = en.edad;
                genero = en.genero;
                localidad = en.localidad;
                visibilidad_perfil = en.visibilidad_perfil;
                verified = en.verified;
            }
        }

        //Declaramos la funcion confirmar usuario donde llama al cad correspondiente
        public bool confirmacionUsuario()
        {
            User_CAD confirmUser = new User_CAD();
            confirmUser.confirmacionUser(this);
            return true;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarUsuario()
        {
            User_CAD actUser = new User_CAD();
            actUser.actualizarUser(this);
        }

        
        //Declaramos el constructor de la clase User_EN
        public User_EN()
        {
            id = 0;
            nombre = "";
            nombreUsu = "";
            correo = "";
            contraseña = "";
            edad = 0;
            genero = null;
            localidad = "";
            visibilidad_perfil = false;
            verified = false;
        }
    }
}
