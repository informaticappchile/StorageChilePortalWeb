using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaOpciones { 
        
        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public byte[] getCrypto()
        {
            Opciones_CAD busqueda = new Opciones_CAD();
            byte[] pass = busqueda.getCrypto();
            return pass;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public string getMensaje()
        {
            Opciones_CAD busqueda = new Opciones_CAD();
            string pass = busqueda.getMensaje();
            return pass;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public DateTime getFecha()
        {
            Opciones_CAD busqueda = new Opciones_CAD();
            DateTime pass = busqueda.getFecha();
            return pass;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public DateTime getFechaTermino()
        {
            Opciones_CAD busqueda = new Opciones_CAD();
            DateTime pass = busqueda.getFechaTermino();
            return pass;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public bool getMantenimiento()
        {
            Opciones_CAD busqueda = new Opciones_CAD();
            bool pass = busqueda.getMantenimiento();
            return pass;
        }

    }
}
