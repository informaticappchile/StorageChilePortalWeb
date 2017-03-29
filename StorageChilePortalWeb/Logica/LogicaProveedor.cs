using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaProveedor
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarProveedor(Proveedor_EN e)
        {
            Proveedor_CAD userCad = new Proveedor_CAD();
            userCad.InsertarProducto(e);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProveedor(Proveedor_EN p)
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedor(p);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public bool BorrarProveedor(Proveedor_EN p)
        {
            Proveedor_CAD proveedorDelete = new Proveedor_CAD();
            return proveedorDelete.BorrarProveedor(p.ID);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Proveedor_EN BuscarProveedor(string proveedor)
        {
            Proveedor_CAD busqueda = new Proveedor_CAD();
            Proveedor_EN proveedorBuscado = busqueda.BuscarProveedor(proveedor);
            return proveedorBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProveedor(Proveedor_EN e)
        {
            Proveedor_CAD actUser = new Proveedor_CAD();
            actUser.actualizarProveedor(e);
        }

        public ArrayList MostrarProveedores()
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedores();

            return a;
        }
        public ArrayList MostrarCiudades()
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarCiudades();

            return a;
        }

        public int GetIdCiudad(string nombreCiudad)
        {
            int id = 0;
            Proveedor_CAD c = new Proveedor_CAD();
            id = c.GetIdCiudad(nombreCiudad);

            return id;
        }
    }
}
