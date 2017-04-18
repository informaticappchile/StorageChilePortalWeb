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

    }
}
