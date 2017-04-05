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
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente

        public void InsertarProductoGrupoProducto(Producto_EN e)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProductoGrupoProducto(e);
        }
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente

        public void InsertarProductoUnidadMedida(Producto_EN e)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProductoUnidadMedida(e);
        }
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente

        public void InsertarProductoProveedorEmpresa(Producto_EN e, Proveedor_EN pr, Empresa_EN em)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProductoProveedorEmpresa(e,pr,em);
        }

        public void InsertarProductoProveedor(Producto_EN e, Empresa_EN em)
        {
            Producto_CAD userCad = new Producto_CAD();
            userCad.InsertarProductoProveedorEmpresa(e, em);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProductoProveedo(Producto_EN e, Proveedor_EN p, Empresa_EN em)
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductoProveedorEmpresa(e,p,em);

            return a;
        }
        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarProductosPorProveedor(string busqueda, int id)
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductosPorProveedorEmpresa(busqueda, id);

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

        public Producto_EN BuscarProductoPorCodigo(string producto, Empresa_EN en, string razon)
        {
            Producto_CAD busqueda = new Producto_CAD();
            Producto_EN productoBuscado = busqueda.BuscarProductoPorCodigo(producto, en, razon);
            return productoBuscado;
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Producto_EN BuscarProductoEmpresa(Empresa_EN em, string producto)
        {
            Producto_CAD busqueda = new Producto_CAD();
            Producto_EN productoBuscado = busqueda.BuscarProductoEmpresa(em,producto);
            return productoBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProducto(Producto_EN e)
        {
            Producto_CAD actUser = new Producto_CAD();
            actUser.actualizarProducto(e);
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProductoGrupoProducto(Producto_EN e, int oldIdGrupo)
        {
            Producto_CAD actUser = new Producto_CAD();
            actUser.actualizarProductoGrupoProducto(e, oldIdGrupo);
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarProductoUnidadMedida(Producto_EN e, int oldIdUnidad)
        {
            Producto_CAD actUser = new Producto_CAD();
            actUser.actualizarProductoUnidadMedida(e, oldIdUnidad);
        }

        public ArrayList MostrarProductos()
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductos();

            return a;
        }

        public ArrayList MostrarProductosPorEmpresa(Empresa_EN em)
        {
            ArrayList a = new ArrayList();
            Producto_CAD c = new Producto_CAD();
            a = c.MostrarProductosPorEmpresa(em.ID);

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
