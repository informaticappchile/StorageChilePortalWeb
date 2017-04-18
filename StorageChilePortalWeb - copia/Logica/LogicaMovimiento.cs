using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaMovimiento
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarMovimiento(Movimiento_EN e)
        {
            Movimiento_CAD userCad = new Movimiento_CAD();
            userCad.InsertarMovimiento(e);
        }
        public void InsertarMovimientoProductoProveedor(List<Movimiento_EN> e, Empresa_EN em)
        {
            Movimiento_CAD userCad = new Movimiento_CAD();
            userCad.InsertarMovimientoProductoProveedorEmpresa(e, em);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarMovimientosProductosProveedor(int IdEmpresa)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarMovimientosProductosProveedorEmpresa(IdEmpresa);

            return a;
        }

        public ArrayList MostrarMovimientosProductosProveedor(int IdEmpresa, string tipo1, string tipo2)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarMovimientosProductosProveedorEmpresa(IdEmpresa, tipo1, tipo2);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public bool BorrarMovimiento(Movimiento_EN p)
        {
            Movimiento_CAD productoDelete = new Movimiento_CAD();
            return productoDelete.BorrarMovimiento(p.ID);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Movimiento_EN BuscarMovimiento(string producto)
        {
            Movimiento_CAD busqueda = new Movimiento_CAD();
            Movimiento_EN movimientoBuscado = busqueda.BuscarMovimiento(producto);
            return movimientoBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public bool actualizarMovimiento(Movimiento_EN e)
        {
            Movimiento_CAD actUser = new Movimiento_CAD();
            return actUser.actualizarMovimiento(e);
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarMovimientoProductoProveedor(List<Movimiento_EN> e, Empresa_EN em)
        {
            Movimiento_CAD actUser = new Movimiento_CAD();
            actUser.actualizarMovimientoProductoProveedorEmpresa(e,em);
        }

        public ArrayList MostrarDocumentos()
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarDocumentos();

            return a;
        }
        public ArrayList MostrarTipoMovimientos()
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarTipoMovimientos();

            return a;
        }

        public ArrayList MostrarTipoMovimientos(int p)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarTipoMovimientos(p);

            return a;
        }

        public int GetIdDocumento(string nombreDocumento)
        {
            int id = 0;
            Movimiento_CAD c = new Movimiento_CAD();
            id = c.GetIdDocumento(nombreDocumento);

            return id;
        }

        public int GetIdTipoMovimiento(string nombreTipoMovimiento)
        {
            int id = 0;
            Movimiento_CAD c = new Movimiento_CAD();
            id = c.GetIdTipoMovimiento(nombreTipoMovimiento);

            return id;
        }
        public ArrayList MostrarMovimientosPorProveedor(string razon, int idEmpresa)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarMovimientosPorProveedorEmpresa(razon, idEmpresa);

            return a;
        }
        public ArrayList MostrarMovimientosPorProveedor(int idEmpresa)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarMovimientosPorProveedorEmpresa(idEmpresa);

            return a;
        }
        public ArrayList MostrarObservaciones(string razon, string ID, int idEmpresa)
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarObservaciones(razon, ID, idEmpresa);

            return a;
        }

    }
}
