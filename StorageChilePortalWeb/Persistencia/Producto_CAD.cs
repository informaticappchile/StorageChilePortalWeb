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
    public class Producto_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProducto(Producto_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into Producto(IdProducto, Descripcion,CodProducto, CantMinStock,Stock) VALUES ('"
                    + e.ID + "','" + e.Descripcion + "','" + e.CodProducto + "'," + e.CantMinStock + "," + e.Stock + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProductoUnidadMedida(Producto_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into ProductoUnidadMedida(IdProducto, IdUnidadMedida) VALUES ('"
                    + e.ID + "'," + e.IdMedidad + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProductoGrupoProducto(Producto_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into ProductoGrupoProducto(IdProducto,IdGrupoProducto) VALUES ('"
                    + e.ID + "'," + e.IdGrupo + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProductoProveedorEmpresa(Producto_EN e, Proveedor_EN pr, Empresa_EN em)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into ProductoProveedorEmpresa(IdProducto,IdProveedor,IdEmpresa) VALUES ('"
                    + e.ID + "'," + pr.ID + "," + em.ID + ")";
                //POR DEFECTO, VISIBILIDAD Y VERIFICACION SON FALSAS
                nueva_conexion.SetQuery(insert);
                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProductoProveedorEmpresa(Producto_EN e, Empresa_EN em)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into ProductoProveedorEmpresa(IdProducto, IdProveedor, IdEmpresa) VALUES ("
                    + e.ID + "," + e.IdProveedor + "," + em.ID + ")";
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

        public ArrayList MostrarProductoProveedorEmpresa(Producto_EN e, Proveedor_EN p, Empresa_EN em)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *"+
                " from Proveedor pr, Producto p, GrupoProducto g, UnidadMedida um, ProductoProveedorEmpresa pp" +
                " where p.IdProducto=" + e.ID +"AND g.IdGrupoProducto = p.IdGrupoProducto AND pr.IdProveedor = " + p.ID +
                " um.IdUnidadMedida = p.IdUnidadMedida AND pr.IdProveedor = pp.IdProveedor AND pp.IdProducto = p.IdProducto" +
                " AND pp.IdEmpresa = " + em.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                Producto_EN producto = new Producto_EN();
                producto.ID = dt.Rows[0]["IdProducto"].ToString();
                producto.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                producto.CodProducto = dt.Rows[0]["CodProducto"].ToString();
                producto.CantMinStock = Convert.ToInt16(dt.Rows[0]["CantMinStock"].ToString());
                producto.Grupo = dt.Rows[0]["NombreGrupoProducto"].ToString();
                producto.UnidadMedida = dt.Rows[0]["NombreUnidadMedida"].ToString(); ;
                producto.IdGrupo = Convert.ToInt16(dt.Rows[0]["IdGrupoProducto"].ToString());
                producto.IdMedidad = Convert.ToInt16(dt.Rows[0]["IdUnidadMedida"].ToString());
                producto.NombreProveedor = dt.Rows[0]["RazonSocial"].ToString();
                producto.Stock = Convert.ToInt16(dt.Rows[0]["Stock"].ToString());
                lista.Add(producto);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProductos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, GrupoProducto gp, UnidadMedida um" + 
                                    " where p.IdGrupoProducto = gp.IdGrupoProducto AND p.IdUnidadMedida = um.IdUnidadMedida");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Producto_EN producto = new Producto_EN();
                producto.ID = dt.Rows[i]["IdProducto"].ToString();
                producto.Descripcion = dt.Rows[i]["Descripcion"].ToString();
                producto.CodProducto = dt.Rows[i]["CodProducto"].ToString();
                producto.CantMinStock = Convert.ToInt16(dt.Rows[i]["CantMinStock"].ToString());
                producto.Grupo = dt.Rows[i]["NombreGrupoProducto"].ToString();
                producto.UnidadMedida = dt.Rows[i]["NombreUnidadMedida"].ToString(); ;
                producto.IdGrupo = Convert.ToInt16(dt.Rows[i]["IdGrupoProducto"].ToString());
                producto.IdMedidad = Convert.ToInt16(dt.Rows[i]["IdUnidadMedida"].ToString());
                producto.Stock = Convert.ToInt16(dt.Rows[i]["Stock"].ToString());
                lista.Add(producto);

            }

            return lista;
            
        }
        
        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarProducto(string idProducto)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Producto where Producto.IdProducto = '" + idProducto + "'";
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
                    " from Producto p, GrupoProducto gp, UnidadMedida um, ProductoGrupoProducto pgp, ProductoUnidadMedida pum" +
                    " where p.IdProducto ='" + busqueda + "' AND pgp.IdGrupoProducto = gp.IdGrupoProducto AND pum.IdUnidadMedida = um.IdUnidadMedida AND"+
                    " pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    producto.ID = dt.Rows[0]["IdProducto"].ToString();
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

        public Producto_EN BuscarProductoPorCodigo(string busqueda, Empresa_EN en, string razon)
        {
            Producto_EN producto = new Producto_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *" +
                    " from Producto p, GrupoProducto gp, UnidadMedida um, ProductoGrupoProducto pgp, ProductoUnidadMedida pum, ProductoProveedorEmpresa ppe, Proveedor pr " +
                    " where (p.CodProducto ='" + busqueda + "' or p.Descripcion ='" + busqueda + "' ) AND pgp.IdGrupoProducto = gp.IdGrupoProducto AND pum.IdUnidadMedida = um.IdUnidadMedida AND" +
                    " pr.IdProveedor = ppe.IdProveedor AND ppe.IdEmpresa = "+ en.ID +" AND ppe.IdProducto = p.IdProducto AND pr.RazonSocial ='"+ razon + "' AND" +
                    " pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    producto.ID = dt.Rows[0]["IdProducto"].ToString();
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

        public Producto_EN BuscarProductoPorCodigo(string busqueda, Empresa_EN en)
        {
            Producto_EN producto = new Producto_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *" +
                    " from Producto p, GrupoProducto gp, UnidadMedida um, ProductoGrupoProducto pgp, ProductoUnidadMedida pum, ProductoProveedorEmpresa ppe, Proveedor pr " +
                    " where (p.CodProducto ='" + busqueda + "' or p.Descripcion ='" + busqueda + "' ) AND pgp.IdGrupoProducto = gp.IdGrupoProducto AND pum.IdUnidadMedida = um.IdUnidadMedida AND" +
                    " pr.IdProveedor = ppe.IdProveedor AND ppe.IdEmpresa = " + en.ID + " AND ppe.IdProducto = p.IdProducto AND" +
                    " pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    producto.ID = dt.Rows[0]["IdProducto"].ToString();
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
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public Producto_EN BuscarProductoEmpresa(Empresa_EN em, string busqueda)
        {
            Producto_EN producto = new Producto_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *" +
                    " from Producto p, GrupoProducto gp, UnidadMedida um, ProductoGrupoProducto pgp, ProductoUnidadMedida pum, ProductoProveedorEmpresa ppe" +
                    " where p.CodProducto ='" + busqueda + "' AND pgp.IdGrupoProducto = gp.IdGrupoProducto AND pum.IdUnidadMedida = um.IdUnidadMedida AND" +
                    " pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto AND ppe.IdProducto = p.IdProducto AND ppe.IdEmpresa ="+ em.ID;
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    producto.ID = dt.Rows[0]["IdProducto"].ToString();
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
                    ",CodProducto = '" + e.CodProducto + "', Stock = " + e.Stock + 
                    " where Producto.IdProducto = '" + e.ID +"'";
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarProductoGrupoProducto(Producto_EN e, int oldIdGrupo)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update ProductoGrupoProducto set IdGrupoProducto = " + e.IdGrupo +
                    " where ProductoGrupoProducto.IdProducto = '" + e.ID + "' AND ProductoGrupoProducto.IdGrupoProducto = " + oldIdGrupo;
                nueva_conexion.SetQuery(update);


                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarProductoUnidadMedida(Producto_EN e, int oldIdUnidad)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update ProductoUnidadMedida set IdUnidadMedida = " + e.IdMedidad +
                    " where ProductoUnidadMedida.IdProducto = '" + e.ID + "' AND ProductoUnidadMedida.IdUnidadMedida = " + oldIdUnidad;
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

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProductosPorEmpresa(int id)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, ProductoProveedorEmpresa ppe, GrupoProducto gp, UnidadMedida um, ProductoGrupoProducto pgp, ProductoUnidadMedida pum" +
                                    " where p.IdProducto = ppe.IdProducto AND pgp.IdGrupoProducto = gp.IdGrupoProducto AND pum.IdUnidadMedida = um.IdUnidadMedida AND" +
                                    " pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto AND ppe.IdEmpresa = " + id +
                                    " Group by p.IdProducto");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Producto_EN producto = new Producto_EN();
                producto.ID = dt.Rows[i]["IdProducto"].ToString();
                producto.Descripcion = dt.Rows[i]["Descripcion"].ToString();
                producto.CodProducto = dt.Rows[i]["CodProducto"].ToString();
                producto.CantMinStock = Convert.ToInt16(dt.Rows[i]["CantMinStock"].ToString());
                producto.Grupo = dt.Rows[i]["NombreGrupoProducto"].ToString();
                producto.UnidadMedida = dt.Rows[i]["NombreUnidadMedida"].ToString(); ;
                producto.IdGrupo = Convert.ToInt16(dt.Rows[i]["IdGrupoProducto"].ToString());
                producto.IdMedidad = Convert.ToInt16(dt.Rows[i]["IdUnidadMedida"].ToString());
                producto.Stock = Convert.ToInt16(dt.Rows[i]["Stock"].ToString());
                lista.Add(producto);

            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProductosPorProveedorEmpresa(string razonSocial, int id)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Producto p, ProductoProveedorEmpresa pp, Proveedor pr, ProductoGrupoProducto pgp, ProductoUnidadMedida pum" +
                                    " where p.IdProducto = pp.IdProducto AND pgp.IdProducto = p.IdProducto AND pum.IdProducto = p.IdProducto AND pp.IdProveedor = pr.IdProveedor AND pr.RazonSocial ='"+
                                    razonSocial +"' AND pp.IdEmpresa = "+ id);
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Producto_EN producto = new Producto_EN();
                producto.ID = dt.Rows[i]["IdProducto"].ToString();
                producto.Descripcion = dt.Rows[i]["Descripcion"].ToString();
                producto.CodProducto = dt.Rows[i]["CodProducto"].ToString();
                producto.CantMinStock = Convert.ToInt16(dt.Rows[i]["CantMinStock"].ToString());
                producto.IdGrupo = Convert.ToInt16(dt.Rows[i]["IdGrupoProducto"].ToString());
                producto.IdMedidad = Convert.ToInt16(dt.Rows[i]["IdUnidadMedida"].ToString());
                producto.Stock = Convert.ToInt16(dt.Rows[i]["Stock"].ToString());
                lista.Add(producto);

            }

            return lista;

        }

    }
}
