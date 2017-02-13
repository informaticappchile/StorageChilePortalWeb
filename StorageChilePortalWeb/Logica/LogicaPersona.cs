using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Logica
{
    public class LogicaPersona
    {
        public static DataTable ObtenerNombres()
        {
            return Persistencia.PersistenciaPersona.ObtenerNombres();
        }
    }
}
