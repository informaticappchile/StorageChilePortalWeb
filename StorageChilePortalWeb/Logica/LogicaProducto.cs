using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaProducto
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarProducto(Producto_EN e)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProducto(e);
        }
        public void InsertarProductoProveedor(Producto_EN e)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProductoProveedor(e);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProductoProveedo(Producto_EN e, Proveedor_EN p)
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductoProveedor(e,p);

            return a;
        }
        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProductosPorProveedor(string busqueda)
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductosPorProveedor(busqueda);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public bool BorrarProducto(Producto_EN p)
        {
            Producto_CAD productoDelete = new Producto_CAD();
            return productoDelete.BorrarProducto(p.ID);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Producto_EN BuscarProducto(string producto)
        {
            Producto_CAD busqueda = new Producto_CAD();
            Producto_EN productoBuscado = busqueda.BuscarProducto(producto);
            return productoBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProducto(Producto_EN e)
        {
            Producto_CAD actUser = new Producto_CAD();
            actUser.actualizarProducto(e);
        }

        public ArrayList MostrarProductos()
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductos();

            return a;
        }
        public ArrayList MostrarGrupos()
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarGrupos();

            return a;
        }
        public ArrayList MostrarUnidades()
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarUnidades();

            return a;
        }

        public int GetIdGrupo(string nombreGrupo)
        {
            int id = 0;
            Producto_CAD c = new Producto_CAD();
            id = c.GetIdGrupo(nombreGrupo);

            return id;
        }

        public int GetIdUnidad(string nombreUnidad)
        {
            int id = 0;
            Producto_CAD c = new Producto_CAD();
            id = c.GetIdUnidad(nombreUnidad);

            return id;
        }

    }
}
