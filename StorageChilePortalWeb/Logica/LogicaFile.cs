using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaFile
    {
        
        public ArrayList MostrarFIles()
        {
            File_CAD fc = new File_CAD ();
            return fc.MostrarFiles();
        }

    }
}
