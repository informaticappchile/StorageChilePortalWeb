using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaUsuario
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarUsuario(User_EN u)
        {
            User_CAD userCad = new User_CAD();
            userCad.InsertarUser(u);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarUsuario()
        {
            User_EN u = new User_EN();
            ArrayList a = new ArrayList();
            User_CAD c = new User_CAD();
            a = c.MostrarUser(u);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public void BorrarUsuario()
        {
            User_EN u = new User_EN();
            User_CAD userDelete = new User_CAD();
            userDelete.BorrarUser(u);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public User_EN BuscarUsuario(string usuario)
        {
            User_CAD busqueda = new User_CAD();
            User_EN usuarioBuscado = busqueda.BuscarUser(usuario);
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
            User_EN u = new User_EN();
            User_CAD cad = new User_CAD();
            User_EN en = new User_EN();
            en = cad.LeerUser(u);
            if (en != null)
            {
                /*id = en.id;
                nombre = en.nombre;
                nombreUsu = en.nombreUsu;
                correo = en.correo;
                visibilidad_perfil = en.visibilidad_perfil;
                verified = en.verified;*/
            }
        }

        //Declaramos la funcion confirmar usuario donde llama al cad correspondiente
        public bool confirmacionUsuario(User_EN u)
        {
            User_CAD confirmUser = new User_CAD();
            confirmUser.confirmacionUser(u);
            return true;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarUsuario(User_EN u)
        {
            User_CAD actUser = new User_CAD();
            actUser.actualizarUser(u);
        }


        public ArrayList MostrarUsuarios()
        {
            ArrayList a = new ArrayList();
            User_CAD c = new User_CAD();
            a = c.MostrarUsuarios();

            return a;
        }

    }
}
