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
    public class Movimiento_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarMovimiento(Movimiento_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string parametro1 = "@fechaMovimiento";
                string parametro2 = "@fechaDocumento";
                string parametro3 = "@EstadoMovimiento";

                string insert = "insert into Movimiento(IdMovimiento,Total,Responsable, FechaMovimiento, Area, FechaDocumento, NumeroDocumento, IdDocumento, IdTipoMovimiento, EstadoMovimiento) VALUES ('"
                    + e.ID + "'," + e.Total + ",'" + e.Responsable + "'," + parametro1 + ",'" + e.Area + "'," + parametro2 + "," + e.NumDocumento + "," + e.IdDocumento + "," + e.IdTipoMovimiento + "," + parametro3 + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.addParameter(parametro1,e.FechaMovimiento);
                nueva_conexion.addParameter(parametro2, e.FechaDocumento);
                nueva_conexion.addParameter(parametro3, e.Estado);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarMovimientoProductoProveedor(List<Movimiento_EN> lm)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                foreach (Movimiento_EN m in lm) {
                    string insert = "insert into MovimientoProductoProveedor(IdMovimiento,IdProducto, IdProveedor,PrecioUnitario,Observaciones,CantidadSolicitada) VALUES ('"
                        + m.ID + "'," + m.IdProducto + "," + m.IdProveedor + "," + m.PrecioUnitario + ",'" + m.Observaciones + "'," + m.Cantidad + ")";
                    //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                    nueva_conexion.SetQuery(insert);
                    nueva_conexion.EjecutarQuery();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public ArrayList MostrarMovimientoProductoProveedor(Movimiento_EN p)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr" +
                                    " where m.IdProducto = p.IdProducto AND p.IdProducto = mpp.IdProducto AND" +
                                    " mpp.IdMovimiento ='" + p.ID + "' AND pr.IdProveedor =" + p.IdProveedor);
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
                movimiento.IdProducto = Convert.ToInt32(dt.Rows[i]["CantidadSolicitada"].ToString());
                lista.Add(movimiento);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarMovimientosProductosProveedor()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery(" select mpp.IdMovimiento, pr.IdProveedor, m.IdTipoMovimiento, tm.TipoMovimiento, pr.RazonSocial, d.TipoDocumento, m.NumeroDocumento, m.IdDocumento, m.Total" +
                                    " from movimiento m, movimientoproductoproveedor mpp, proveedor pr, producto p, proveedorproducto pp, documento d, tipomovimiento tm "+
                                    " where m.IdMovimiento = mpp.IdMovimiento and mpp.IdProducto = pp.IdProducto and mpp.IdProveedor = pp.IdProveedor "+
                                    " and pp.IdProveedor = pr.IdProveedor and pp.IdProducto = p.IdProducto and m.IdDocumento = d.IdDocumento AND "+
                                    " m.IdTipoMovimiento = tm.IdTipoMovimiento and m.EstadoMovimiento = 1"+
                                    " ");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = dt.Rows[i]["IdMovimiento"].ToString();
                movimiento.IdProveedor = Convert.ToInt16(dt.Rows[i]["IdProveedor"].ToString());
                movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[i]["IdTipoMovimiento"].ToString());
                movimiento.TipoMovimiento = dt.Rows[i]["TipoMovimiento"].ToString();
                movimiento.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                movimiento.Documento = dt.Rows[i]["TipoDocumento"].ToString();
                movimiento.NumDocumento = Convert.ToInt32(dt.Rows[i]["NumeroDocumento"].ToString());
                movimiento.IdDocumento = Convert.ToInt16(dt.Rows[i]["IdDocumento"].ToString());
                movimiento.Total = Convert.ToInt32(dt.Rows[i]["Total"].ToString());
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
        public Movimiento_EN BuscarMovimiento(string busqueda)
        {
            Movimiento_EN movimiento = new Movimiento_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *"+
                    " from Movimiento" +
                    " where IdMovimiento ='" + busqueda + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    movimiento.ID = dt.Rows[0]["IdMovimiento"].ToString();
                    movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[0]["IdTipoMovimiento"].ToString());
                    movimiento.IdDocumento = Convert.ToInt16(dt.Rows[0]["IdDocumento"].ToString());
                    movimiento.Total = Convert.ToInt32(dt.Rows[0]["Total"].ToString());
                    movimiento.NumDocumento = Convert.ToInt32(dt.Rows[0]["NumeroDocumento"].ToString());
                    movimiento.Area = dt.Rows[0]["Area"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return movimiento;
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/ 
         
        public bool actualizarMovimiento(Movimiento_EN e)
        {
            Conexion nueva_conexion = new Conexion();
            bool estado = false;
            try
            {
                string update = "";
                string parametro = "@EstadoMovimiento";

                update = "Update Movimiento set Total = " + e.Total + ",NumeroDocumento  = " + e.NumDocumento +
                    ",FechaDocumento = "+e.NumDocumento +",IdDocumento = " + e.IdDocumento + ", IdPago='" + e.IdPago 
                    + "', EstadoMovimiento = " + parametro +
                    " where Movimiento.IdMovimiento = '" + e.ID + "'" ;
                nueva_conexion.SetQuery(update);
                nueva_conexion.addParameter(parametro,e.Estado);


                nueva_conexion.EjecutarQuery();
                estado = true;
                return estado;
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
            return estado;
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

        public ArrayList MostrarTipoMovimientos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from TipoMovimiento");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["TipoMovimiento"].ToString());
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

        public int GetIdTipoMovimiento(string tipoMovimiento)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from TipoMovimiento u" +
                                    " where u.TipoMovimiento ='" + tipoMovimiento + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdTipoMovimiento"].ToString());
            }

            return id;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarMovimientosPorProveedor(string razon)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr, TipoMovimiento tm, Documento d" +
                                    " where p.IdProducto = mpp.IdProducto AND" +
                                    " mpp.IdMovimiento = m.IdMovimiento AND pr.IdProveedor = mpp.IdProveedor AND" +
                                    " pr.RazonSocial ='" + razon + "' AND m.IdPago ='" + 0 + "' AND tm.IdTipoMovimiento = m.IdTipoMovimiento" +
                                    " AND tm.TipoMovimiento ='Compra' AND d.IdDocumento = m.IdDocumento" +
                                    " group by m.IdMovimiento");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = dt.Rows[i]["IdMovimiento"].ToString();
                movimiento.IdProveedor = Convert.ToInt16(dt.Rows[i]["IdProveedor"].ToString());
                movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[i]["IdTipoMovimiento"].ToString());
                movimiento.TipoMovimiento = dt.Rows[i]["TipoMovimiento"].ToString();
                movimiento.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                movimiento.Documento = dt.Rows[i]["TipoDocumento"].ToString();
                movimiento.NumDocumento = Convert.ToInt32(dt.Rows[i]["NumeroDocumento"].ToString());
                movimiento.IdDocumento = Convert.ToInt16(dt.Rows[i]["IdDocumento"].ToString());
                movimiento.Total = Convert.ToInt32(dt.Rows[i]["Total"].ToString());
                movimiento.Observaciones = dt.Rows[i]["Observaciones"].ToString();
                movimiento.FechaDocumento = Convert.ToDateTime(dt.Rows[i]["FechaDocumento"].ToString());
                lista.Add(movimiento);

            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarMovimientosPorProveedor()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr, TipoMovimiento tm, Documento d" +
                                    " where p.IdProducto = mpp.IdProducto AND" +
                                    " mpp.IdMovimiento = m.IdMovimiento AND pr.IdProveedor = mpp.IdProveedor" +
                                    " AND tm.IdTipoMovimiento = m.IdTipoMovimiento" +
                                    " AND (tm.TipoMovimiento ='Compra' OR tm.TipoMovimiento ='Devolucion') AND d.IdDocumento = m.IdDocumento" +
                                    " group by m.IdMovimiento");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = dt.Rows[i]["IdMovimiento"].ToString();
                movimiento.IdProveedor = Convert.ToInt16(dt.Rows[i]["IdProveedor"].ToString());
                movimiento.IdTipoMovimiento = Convert.ToInt16(dt.Rows[i]["IdTipoMovimiento"].ToString());
                movimiento.TipoMovimiento = dt.Rows[i]["TipoMovimiento"].ToString();
                movimiento.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                movimiento.Documento = dt.Rows[i]["TipoDocumento"].ToString();
                movimiento.NumDocumento = Convert.ToInt32(dt.Rows[i]["NumeroDocumento"].ToString());
                movimiento.IdDocumento = Convert.ToInt16(dt.Rows[i]["IdDocumento"].ToString());
                movimiento.Total = Convert.ToInt32(dt.Rows[i]["Total"].ToString());
                movimiento.Observaciones = dt.Rows[i]["Observaciones"].ToString();
                movimiento.FechaDocumento = Convert.ToDateTime(dt.Rows[i]["FechaDocumento"].ToString());
                lista.Add(movimiento);

            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarObservaciones(string razon, string idMovimiento)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr, TipoMovimiento tm, Documento d" +
                                    " where p.IdProducto = mpp.IdProducto AND" +
                                    " mpp.IdMovimiento = m.IdMovimiento AND pr.IdProveedor = mpp.IdProveedor AND" +
                                    " pr.RazonSocial ='" + razon + "' AND m.IdPago ='" + 0 + "' AND tm.IdTipoMovimiento = m.IdTipoMovimiento" +
                                    " AND tm.TipoMovimiento ='Compra' AND d.IdDocumento = m.IdDocumento AND m.IdMovimiento = '" + idMovimiento + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["Observaciones"].ToString());
            }

            return lista;

        }

    }
}
