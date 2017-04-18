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
            userCad.InsertarProveedor(e);
        }
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarVendedorEmpresa(Proveedor_EN e,Empresa_EN em)
        {
            Proveedor_CAD userCad = new Proveedor_CAD();
            userCad.InsertarVendedorEmpresa(e,em);
        }

        //Declaramos la funcion insertar usuario donde llama al cad correspondiente

        public void InsertarVendedor(Proveedor_EN e)
        {
            Proveedor_CAD userCad = new Proveedor_CAD();
            userCad.InsertarVendedor(e);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProveedor(Proveedor_EN p)
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedor(p);

            return a;
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProveedoresConProductos(int idEmpresa)
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedoresConProductosEmpresa(idEmpresa);

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

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Proveedor_EN BuscarVendedor(Proveedor_EN p, string proveedor)
        {
            Proveedor_CAD busqueda = new Proveedor_CAD();
            Proveedor_EN proveedorBuscado = busqueda.BuscarVendedor(p, proveedor);
            return proveedorBuscado;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Proveedor_EN BuscarProveedorVendedorEmpresa(Empresa_EN p, string proveedor)
        {
            Proveedor_CAD busqueda = new Proveedor_CAD();
            Proveedor_EN proveedorBuscado = busqueda.BuscarProveedorVendedorEmpresa(p, proveedor);
            return proveedorBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProveedor(Proveedor_EN e)
        {
            Proveedor_CAD actUser = new Proveedor_CAD();
            actUser.actualizarProveedor(e);
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarVendedor(Proveedor_EN e)
        {
            Proveedor_CAD actUser = new Proveedor_CAD();
            actUser.actualizarVendedor(e);
        }

        public ArrayList MostrarProveedores()
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedores();

            return a;
        }

        public ArrayList MostrarProveedorVendedorEmpresa(Proveedor_EN p, Empresa_EN em)
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedorVendedorEmpresa(p,em);

            return a;
        }

        public ArrayList MostrarProveedoresVendedorEmpresa(Empresa_EN em)
        {
            ArrayList a = new ArrayList();
            Proveedor_CAD c = new Proveedor_CAD();
            a = c.MostrarProveedoresVendedorEmpresa(em);

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
