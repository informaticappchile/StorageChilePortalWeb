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
        public void InsertarMovimientoProductoProveedor(List<Movimiento_EN> e)
        {
            Movimiento_CAD userCad = new Movimiento_CAD();
            userCad.InsertarMovimientoProductoProveedor(e);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarMovimientosProductosProveedor()
        {
            ArrayList a = new ArrayList();
            Movimiento_CAD c = new Movimiento_CAD();
            a = c.MostrarMovimientosProductosProveedor();

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
        public void actualizarMovimiento(Movimiento_EN e)
        {
            Movimiento_CAD actUser = new Movimiento_CAD();
            actUser.actualizarMovimiento(e);
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarMovimientoProductoProveedor(List<Movimiento_EN> e)
        {
            Movimiento_CAD actUser = new Movimiento_CAD();
            actUser.actualizarMovimientoProductoProveedor(e);
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

    }
}
