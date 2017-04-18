using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Mail;
using Entidades;
using MySql.Data.MySqlClient;

namespace Persistencia
{
    public class Pago_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarPago(Pago_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string parametro1 = "@fechaPago";

                string insert = "insert into Pago(IdPago,EstadoComprobante,FechaComprobante, NumeroCheque, IdProveedor, IdTipoPago) VALUES ('"
                    + e.ID + "'," + e.EstadoComprobante + "," + parametro1 + "," + e.NumCheque + "," + e.IdProveedor + "," + e.IdTipoPago  + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.addParameter(parametro1,e.FechaComprobante);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public ArrayList MostrarPagoProveedor(Proveedor_EN p)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr" +
                                    " where m.IdProducto = p.IdProducto AND p.IdProducto = mpp.IdProducto AND" +
                                    " mpp.IdMovimiento ='" + p.ID + "' AND pr.IdProveedor =" + p.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = dt.Rows[i]["IdMovimiento"].ToString();
                movimiento.IdProveedor = Convert.ToInt16(dt.Rows[i]["IdProveedor"].ToString());
                movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[i]["IdTipoMovimiento"].ToString());
                movimiento.TipoMovimiento = dt.Rows[i]["TipoMovimiento"].ToString();
                movimiento.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                movimiento.Documento = dt.Rows[i]["Documento"].ToString();
                movimiento.NumDocumento = Convert.ToInt32(dt.Rows[i]["NombreUnidadMedida"].ToString());
                movimiento.IdDocumento = Convert.ToInt16(dt.Rows[i]["IdDocumento"].ToString());
                movimiento.Total = Convert.ToInt32(dt.Rows[i]["Total"].ToString());
                movimiento.Cantidad = Convert.ToInt32(dt.Rows[i]["CantidadSolicitada"].ToString());
                movimiento.IdProducto = dt.Rows[i]["CantidadSolicitada"].ToString();
                lista.Add(movimiento);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarPagos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr" + 
                                    " where m.IdProducto = p.IdProducto AND p.IdProducto = mpp.IdProducto AND" + 
                                    " mpp.IdMovimiento = m.IdMovimiento AND pr.IdProveedor = m.IdProveedor" + 
                                    " group by pr.IdProveedor");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = dt.Rows[i]["IdMovimiento"].ToString();
                movimiento.IdProveedor = Convert.ToInt16(dt.Rows[i]["IdProveedor"].ToString());
                movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[i]["IdTipoMovimiento"].ToString());
                movimiento.TipoMovimiento = dt.Rows[i]["TipoMovimiento"].ToString();
                movimiento.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                movimiento.Documento = dt.Rows[i]["Documento"].ToString();
                movimiento.NumDocumento = Convert.ToInt32(dt.Rows[i]["NumeroDocumento"].ToString());
                movimiento.IdDocumento = Convert.ToInt16(dt.Rows[i]["IdDocumento"].ToString());
                movimiento.Total = Convert.ToInt32(dt.Rows[i]["Total"].ToString());
                movimiento.Cantidad = Convert.ToInt32(dt.Rows[i]["CantidadSolicitada"].ToString());
                lista.Add(movimiento);

            }

            return lista;
            
        }
        
        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarMovimiento(string idMovimiento)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Movimiento where Movimiento.IdMovimiento = '" + idMovimiento + "'";
                nueva_conexion.SetQuery(delete);

                nueva_conexion.EjecutarQuery();
                return true;
            }
            catch (Exception ex) { ex.Message.ToString(); return false; }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public Pago_EN BuscarPago(string busqueda)
        {
            Pago_EN movimiento = new Pago_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *"+
                    " from Pago" +
                    " where IdPago ='" + busqueda + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    movimiento.ID = dt.Rows[0]["IdPago"].ToString();
                    movimiento.FechaComprobante = Convert.ToDateTime(dt.Rows[0]["FechaComprobante"].ToString());
                    movimiento.IdTipoPago = Convert.ToInt16(dt.Rows[0]["IdTipoPago"].ToString());
                    movimiento.IdProveedor = Convert.ToInt32(dt.Rows[0]["IdProveedor"].ToString());
                    movimiento.NumCheque = Convert.ToInt32(dt.Rows[0]["NumeroCheque"].ToString());
                    movimiento.EstadoComprobante = Convert.ToBoolean(dt.Rows[0]["EstadoComprobante"].ToString());
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return movimiento;
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/ 
         
        public void actualizarMovimiento(Movimiento_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                

                update = "Update Movimiento set Total = " + e.Total + ",NumeroDocumento  = " + e.NumDocumento +
                    ",FechaDocumento = "+e.NumDocumento +",IdDocumento = " + e.IdDocumento +
                    " where Movimiento.IdMovimiento = '" + e.ID + "'";
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarMovimientoProductoProveedor(List<Movimiento_EN> lm)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";

                foreach (Movimiento_EN e in lm) {
                    update = "Update MovimientoProductoProveedor set Observaciones = '" + e.Observaciones + "',PrecioUnitario  = " + e.PrecioUnitario +
                        ",CantidadSolicitada = " + e.Cantidad +
                        " where MovimientoProductoProveedor.IdMovimiento = '" + e.ID + "' AND MovimientoProductoProveedor.IdProveedor = " + e.IdProveedor +
                        " And MovimientoProductoProveedor.IdProducto = " + e.IdProducto;
                    nueva_conexion.SetQuery(update);

                    nueva_conexion.EjecutarQuery();
                }

            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarDocumentos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Documento");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["TipoDocumento"].ToString());
            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarTipoPagos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from TipoPago");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["TipoPago"].ToString());
            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public int GetIdDocumento(string tipoDocumento)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Documento u" +
                                    " where u.TipoDocumento ='" + tipoDocumento+"'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdDocumento"].ToString());
            }

            return id;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public int GetIdTipoPago(string tipoPago)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from TipoPago u" +
                                    " where u.TipoPago ='" + tipoPago + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdTipoPago"].ToString());
            }

            return id;

        }

    }
}
