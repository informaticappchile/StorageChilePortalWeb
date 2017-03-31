using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaPago
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarPago(Pago_EN e)
        {
            Pago_CAD userCad = new Pago_CAD();
            userCad.InsertarPago(e);
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
        public Pago_EN BuscarPago(string producto)
        {
            Pago_CAD busqueda = new Pago_CAD();
            Pago_EN pagoBuscado = busqueda.BuscarPago(producto);
            return pagoBuscado;
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
        public ArrayList MostrarTipoPagos()
        {
            ArrayList a = new ArrayList();
            Pago_CAD c = new Pago_CAD();
            a = c.MostrarTipoPagos();

            return a;
        }

        public int GetIdDocumento(string nombreDocumento)
        {
            int id = 0;
            Movimiento_CAD c = new Movimiento_CAD();
            id = c.GetIdDocumento(nombreDocumento);

            return id;
        }

        public int GetIdTipoPago(string nombreTipoPago)
        {
            int id = 0;
            Pago_CAD c = new Pago_CAD();
            id = c.GetIdTipoPago(nombreTipoPago);

            return id;
        }

    }
}
