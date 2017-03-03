using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaAdmin
    {
        
        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public User_EN logAdmin(string usuario, string password)
        {
            Admin_CAD busqueda = new Admin_CAD();
            User_EN usuarioBuscado = busqueda.logAdmin(usuario,password);
            return usuarioBuscado;
        }

    }
}
