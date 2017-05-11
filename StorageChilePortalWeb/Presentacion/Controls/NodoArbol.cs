using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion
{
    public class NodoArbol
    {
        NodoArbol padre;
        List<NodoArbol> hijos;
        string nombre;
        bool esCarpeta;
        bool filtro;

        public NodoArbol()
        {
            this.hijos = new List<NodoArbol>();
            this.nombre = "";
            this.esCarpeta = false;
            this.padre = null;
            this.filtro = false;
        }

        public NodoArbol(string nombre, bool esCarpeta)
        {
            this.hijos = new List<NodoArbol>();
            this.nombre = nombre;
            this.esCarpeta = esCarpeta;
            this.padre = null;
            this.filtro = false;
        }

        public NodoArbol(string nombre, bool esCarpeta, ref NodoArbol padre)
        {
            this.hijos = new List<NodoArbol>();
            this.nombre = nombre;
            this.esCarpeta = esCarpeta;
            this.padre = padre;
        }

        public List<NodoArbol> Hijos
        {
            get { return hijos; }
            set { hijos = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public bool EsCarpeta
        {
            get { return esCarpeta; }
            set { esCarpeta = value; }
        }
        public NodoArbol Padre
        {

            get { return padre; }
            set { padre = value; }
        }
    }
}