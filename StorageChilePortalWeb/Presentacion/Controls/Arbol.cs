using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion
{
    public class Arbol
    {
        NodoArbol raiz;
        string nombre;
        string resultado;
        NodoArbol buscado;

        public NodoArbol Raiz
        {
            get { return raiz; }
            set { raiz = value; }
        }
        public NodoArbol Buscado
        {
            get { return buscado; }
            set { buscado = value; }
        }


        public string Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }

        public Arbol()
        {
            resultado = "";
            this.raiz = new NodoArbol();
            buscado = null;
        }

        public Arbol(string nombre, bool esCarpeta)
        {
            resultado = "";
            this.raiz = new NodoArbol(nombre,esCarpeta);
            buscado = null;
        }

        public bool Insertar(string nombre, bool esCarpeta, ref NodoArbol padre)
        {
            NodoArbol a = new NodoArbol(nombre, esCarpeta, ref padre);
            if (this.raiz == a.Padre)
            {
                this.raiz.Hijos.Add(a);
            }
            else if (this.raiz.Hijos.Contains(a.Padre))
            {
                int pos = this.raiz.Hijos.LastIndexOf(a.Padre);
                this.raiz.Hijos[pos].Hijos.Add(a);
            }
            else
            {
                NodoArbol b = a.Padre;
                b.Hijos.Add(a);
                
            }
            return false;
        }

        public bool EsHoja(NodoArbol entidad)
        {
            if (entidad.Hijos == null || entidad.Hijos.Count < 1)
                return true;
            return false;
        }

        public int ObtenerProfunidad(NodoArbol entidad)
        {
            if (entidad.Padre == null)
                return 0;
            else
                return this.ObtenerProfunidad(entidad.Padre) + 1;
        }

        public int ObtenerAltura(NodoArbol entidad)
        {
            if (entidad.Hijos == null || entidad.Hijos.Count < 1)
                return 0;
            else
            {
                int maximaAltura = -1;
                NodoArbol auxiliar = null;
                entidad.Hijos.ToList().ForEach(actualEntidad =>
                {
                    int auxAltura = this.ObtenerAltura(actualEntidad);
                    if (auxAltura > maximaAltura)
                    {
                        maximaAltura = auxAltura;
                        auxiliar = actualEntidad;
                    }
                });

                return this.ObtenerAltura(auxiliar) + 1;
            }
        }

        public void RecorridoPostOrden(ref NodoArbol entidad)
        {
            
            if (entidad == null)
                return;
            entidad.Hijos.ToList().ForEach(actualNodo =>
            {
                RecorridoPostOrden(ref actualNodo);
                resultado += actualNodo.Nombre + " ";
            }
            );

        }

        public NodoArbol BuscarPostOrden(ref NodoArbol entidad, string buscado)
        {
            if (entidad == null)
                return null;
            foreach (NodoArbol actualNodo in entidad.Hijos)
            {
                NodoArbol a = actualNodo;
                BuscarPostOrden(ref a, buscado);
                if (a.Nombre == buscado)
                {
                    this.buscado = actualNodo;
                }
            }
            return null;

        }

        public bool AplicarFiltro(ref NodoArbol entidad)
        {
            try
            {
                entidad.Filtro = true;
                NodoArbol aux = entidad.Padre;
                while (aux != null)
                {
                    aux.Filtro = true;
                    aux = aux.Padre;
                }
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }

        public bool FiltrarHijo(ref NodoArbol entidad, string hijo)
        {
            try
            {
                foreach (NodoArbol hijos in entidad.Hijos)
                {
                    if (hijos.Nombre == hijo)
                    {
                        hijos.Filtro = true;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}