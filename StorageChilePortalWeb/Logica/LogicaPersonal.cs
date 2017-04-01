using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaPersonal
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarPersonal(Personal_EN p)
        {
            Personal_CAD perCad = new Personal_CAD();
            perCad.InsertarPersonal(p);
        }
        public void InsertarPersonalEmpresa(int idPer, int idEmp)
        {
            Personal_CAD perCad = new Personal_CAD();
            perCad.InsertarPersonalEmpresa(idPer,idEmp);
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
        public bool BorrarUsuario(string userName)
        { 
            User_CAD userDelete = new User_CAD();
            return userDelete.BorrarUser(userName);
        }

        public Personal_EN BuscarPersonal(string rut)
        {
            Personal_CAD busqueda = new Personal_CAD();
            Personal_EN usuarioBuscado = busqueda.BuscarPersonal(rut);
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

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarUsuarioAdmin(User_EN u)
        {
            User_CAD actUser = new User_CAD();
            actUser.actualizarUserAdmin(u);
        }

        //Declaramos la funcion establecer intentos del usuario donde llama al cad correspondiente
        public void establecerIntento(User_EN u)
        {
            User_CAD actUser = new User_CAD();
            actUser.establecerIntentos(u);
        }

        //Declaramos la funcion bloquear usuario donde llama al cad correspondiente
        public void bloquearUsuario(User_EN u)
        {
            User_CAD actUser = new User_CAD();
            actUser.bloquearUsuario(u);
        }
        //Declaramos la funcion restablecer la contraseña del usuario donde llama al cad correspondiente
        public void RestableserPassword(User_EN u)
        {
            User_CAD actUser = new User_CAD();
            actUser.RestablecerContraseña(u);
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
