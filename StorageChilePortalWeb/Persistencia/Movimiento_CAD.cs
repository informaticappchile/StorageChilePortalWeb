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

                string insert = "insert into Movimiento(Total,Responsable, FechaMovimiento, Area, FechaDocumento, NumeroDocumento, IdDocumento, IdTipoMovimento) VALUES ("
                    + e.Total + ",'" + e.Responsable + "'," + parametro1 + ",'" + e.Area + "'," + parametro2 + "," + e.NumDocumento + "," + e.IdDocumento + "," + e.IdTipoMovimento + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.addParameter(parametro1,e.FechaMovimiento);
                nueva_conexion.addParameter(parametro2, e.FechaDocumento);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarMovimientoProductoProveedor(Movimiento_EN m)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into MovimientoProductoProveedor(IdMovimiento,IdProducto, IdProveedor,PrecioUnitario,Observaciones,CantidadSolicitada) VALUES ("
                    + m.ID + "," + m.IdProducto + "," + m.IdProveedor + "," + m.PrecioUnitario + ",'" + m.Observaciones + "'," + m.Cantidad + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
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
                                    " mpp.IdMovimiento =" + p.ID + "AND pr.IdProveedor =" + p.IdProveedor);
            DataTable dt = nueva_conexion.QuerySeleccion();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = Convert.ToInt16(dt.Rows[i]["IdMovimiento"]);
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
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, Movimiento m, MovimientoProductoProveedor mpp, Proveedor pr" + 
                                    " where m.IdProducto = p.IdProducto AND p.IdProducto = mpp.IdProducto AND" + 
                                    " mpp.IdMovimiento = m.IdMovimiento AND pr.IdProveedor = m.IdProveedor" + 
                                    " group by pr.IdProveedor");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movimiento_EN movimiento = new Movimiento_EN();
                movimiento.ID = Convert.ToInt16(dt.Rows[i]["IdMovimiento"]);
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
        public bool BorrarProducto(int idProducto)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Producto where Producto.IdProducto = " + idProducto;
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
        public Producto_EN BuscarProducto(string busqueda)
        {
            Producto_EN producto = new Producto_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *"+
                    " from Producto p, GrupoProducto gp, UnidadMedida um" +
                    " where codProducto ='" + busqueda + "' AND p.IdGrupoProducto = gp.IdGrupoProducto AND p.IdUnidadMedida = um.IdUnidadMedida";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    producto.ID = Convert.ToInt16(dt.Rows[0]["IdProducto"]);
                    producto.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    producto.CodProducto = dt.Rows[0]["CodProducto"].ToString();
                    producto.CantMinStock = Convert.ToInt16(dt.Rows[0]["CantMinStock"].ToString());
                    producto.Grupo = dt.Rows[0]["NombreGrupoProducto"].ToString();
                    producto.UnidadMedida = dt.Rows[0]["NombreUnidadMedida"].ToString(); ;
                    producto.IdGrupo = Convert.ToInt16(dt.Rows[0]["IdGrupoProducto"].ToString());
                    producto.IdMedidad = Convert.ToInt16(dt.Rows[0]["IdUnidadMedida"].ToString());
                    producto.Stock = Convert.ToInt16(dt.Rows[0]["Stock"].ToString());
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return producto;
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/ 
         
        public void actualizarProducto(Producto_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                

                update = "Update Producto set Descripcion = '" + e.Descripcion + "',CantMinStock  = " + e.CantMinStock +
                    ",IdGrupoProducto = "+e.IdGrupo +",IdUnidadMedida = " + e.IdMedidad + ",CodProducto = '" + e.CodProducto + "'"+ 
                    " where Producto.IdProducto = " + e.ID;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarGrupos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from GrupoProducto");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["NombreGrupoProducto"].ToString());
            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarUnidades()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from UnidadMedida");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(dt.Rows[i]["NombreUnidadMedida"].ToString());
            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public int GetIdGrupo(string nombreGrupo)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from GrupoProducto u" +
                                    " where u.NombreGrupoProducto ='" + nombreGrupo+"'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdGrupoProducto"].ToString());
            }

            return id;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public int GetIdUnidad(string nombreUnidad)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from UnidadMedida u" +
                                    " where u.NombreUnidadMedida ='" + nombreUnidad + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdUnidadMedida"].ToString());
            }

            return id;

        }

    }
}
