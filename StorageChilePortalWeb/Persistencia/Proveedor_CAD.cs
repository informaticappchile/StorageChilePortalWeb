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
    public class Proveedor_CAD
    {
        public ArrayList lista = new ArrayList();

        /**
         * Se encarga de introducir un usuario en la base de datos 
         * 
         */
        public void InsertarProveedor(Proveedor_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into Proveedor(RutProveedor,RazonSocial) VALUES ('"
                    + e.Rut + "','" + e.RazonSocial + "')";
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
        public void InsertarVendedorEmpresa(Proveedor_EN e, Empresa_EN em)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into VendedorEmpresa(IdVendedor,IdEmpresa) VALUES ('"
                    + e.IdVendedor + "'," + em.ID + ")";
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
        public void InsertarVendedor(Proveedor_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into Vendedor(IdVendedor, Direccion, IdCiudad, Fono, NombreVendedor, IdProveedor) VALUES ('"
                 + e.IdVendedor + "','" +  e.Direccion + "'," + e.IdCiudad + ",'" + e.Fono + "','" + e.Vendedor + "'," + e.ID +")";
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

        public ArrayList MostrarProveedor(Proveedor_EN p)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *"+
                " from Proveedor pr, Ciudad c" +
                " where pr.IdProveedor = " + p.ID + "AND p.IdCiudad = c.IdCiudad");
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                Proveedor_EN proveedor = new Proveedor_EN();
                proveedor.ID = Convert.ToInt16(dt.Rows[0]["IdPoveedor"]);
                proveedor.Direccion = dt.Rows[0]["Direccion"].ToString();
                proveedor.Ciudad = dt.Rows[0]["Ciudad"].ToString();
                proveedor.Fono = dt.Rows[0]["Fono"].ToString();
                proveedor.Rut = dt.Rows[0]["RutProveedor"].ToString();
                proveedor.RazonSocial = dt.Rows[0]["RazonSocial"].ToString();
                proveedor.Vendedor = dt.Rows[0]["Vendedor"].ToString();
                lista.Add(proveedor);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar el usuario que se quiere mostrar a través de su ID
         */

        public ArrayList MostrarProveedorVendedorEmpresa(Proveedor_EN p,Empresa_EN em)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                " from Proveedor pr, Ciudad c, Vendedor v, VendedorEmpresa ve" +
                " where pr.IdProveedor = " + p.ID + " AND v.IdCiudad = c.IdCiudad AND pr.IdProveedor = v.IdProveedor AND ve.IdVendedor = v.IdVendedor" +
                " AND ve.IdEmpresa =" + em.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();


            if (dt != null)
            {
                Proveedor_EN proveedor = new Proveedor_EN();
                proveedor.ID = Convert.ToInt16(dt.Rows[0]["IdProveedor"]);
                proveedor.Direccion = dt.Rows[0]["Direccion"].ToString();
                proveedor.Ciudad = dt.Rows[0]["NombreCiudad"].ToString();
                proveedor.Fono = dt.Rows[0]["Fono"].ToString();
                proveedor.Rut = dt.Rows[0]["RutProveedor"].ToString();
                proveedor.RazonSocial = dt.Rows[0]["RazonSocial"].ToString();
                proveedor.Vendedor = dt.Rows[0]["NombreVendedor"].ToString();
                proveedor.Vendedor = dt.Rows[0]["IdVendedor"].ToString();
                lista.Add(proveedor);
            }

            return lista;
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProveedoresVendedorEmpresa(Empresa_EN em)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                " from Proveedor pr, Ciudad c, Vendedor v, VendedorEmpresa ve" +
                " where v.IdCiudad = c.IdCiudad AND pr.IdProveedor = v.IdProveedor AND ve.IdVendedor = v.IdVendedor" +
                " AND ve.IdEmpresa =" + em.ID);
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Proveedor_EN proveedor = new Proveedor_EN();
                proveedor.ID = Convert.ToInt16(dt.Rows[i]["IdProveedor"]);
                proveedor.IdCiudad = Convert.ToInt16(dt.Rows[i]["IdCiudad"]);
                proveedor.Direccion = dt.Rows[i]["Direccion"].ToString();
                proveedor.Ciudad = dt.Rows[i]["NombreCiudad"].ToString();
                proveedor.Fono = dt.Rows[i]["Fono"].ToString();
                proveedor.Rut = dt.Rows[i]["RutProveedor"].ToString();
                proveedor.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                proveedor.Vendedor = dt.Rows[i]["NombreVendedor"].ToString();
                proveedor.IdVendedor = dt.Rows[i]["IdVendedor"].ToString();
                lista.Add(proveedor);

            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProveedores()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Proveedor p" +
                                    " ");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Proveedor_EN proveedor = new Proveedor_EN();
                proveedor.ID = Convert.ToInt16(dt.Rows[i]["IdProveedor"]);
                proveedor.Rut = dt.Rows[i]["RutProveedor"].ToString();
                proveedor.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                lista.Add(proveedor);

            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProveedoresConProductosEmpresa(int IdEmpresa)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Proveedor p, Ciudad c, ProductoProveedorEmpresa pp" +
                                    " where p.IdCiudad = c.IdCiudad AND pp.IdProveedor = p.IdProveedor AND pp.IdEmpresa = " + IdEmpresa +
                                    " Group by p.IdProveedor");
            DataTable dt = nueva_conexion.QuerySeleccion();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Proveedor_EN proveedor = new Proveedor_EN();
                proveedor.ID = Convert.ToInt16(dt.Rows[i]["IdProveedor"]);
                proveedor.IdCiudad = Convert.ToInt16(dt.Rows[i]["IdCiudad"]);
                proveedor.Direccion = dt.Rows[i]["Direccion"].ToString();
                proveedor.Ciudad = dt.Rows[i]["NombreCiudad"].ToString();
                proveedor.Fono = dt.Rows[i]["Fono"].ToString();
                proveedor.Rut = dt.Rows[i]["RutProveedor"].ToString();
                proveedor.RazonSocial = dt.Rows[i]["RazonSocial"].ToString();
                proveedor.Vendedor = dt.Rows[i]["Vendedor"].ToString();
                lista.Add(proveedor);

            }

            return lista;

        }

        /**
         * Se encarga de borrar el usuario, si existe en la base de datos, a través de su ID
         **/
        public bool BorrarProveedor(int idProveedor)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string delete = "";
                delete = "Delete from Proveedor where Proveedor.IdProveedor = " + idProveedor;
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
        public Proveedor_EN BuscarProveedor(string busqueda)
        {
            Proveedor_EN proveedor = new Proveedor_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *"+
                    " from Proveedor p" +
                    " where (RazonSocial ='" + busqueda + "' OR RutProveedor ='" + busqueda + "')";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    proveedor.ID = Convert.ToInt16(dt.Rows[0]["IdProveedor"]);
                    proveedor.Rut = dt.Rows[0]["RutProveedor"].ToString();
                    proveedor.RazonSocial = dt.Rows[0]["RazonSocial"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return proveedor;
        }

        /**
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public Proveedor_EN BuscarVendedor(Proveedor_EN busqueda, string IdVendedor)
        {
            Proveedor_EN proveedor = new Proveedor_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *" +
                    " from Proveedor p, Ciudad c, Vendedor v" +
                    " where (RazonSocial ='" + busqueda.RazonSocial + "' OR RutProveedor ='" + busqueda.Rut + "') AND  p.IdCiudad = c.IdCiudad AND v.IdProveedor = p.IdProveedor"+
                    " AND v.idVendedor ='" + IdVendedor +"'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    proveedor.ID = Convert.ToInt16(dt.Rows[0]["IdProveedor"]);
                    proveedor.IdCiudad = Convert.ToInt16(dt.Rows[0]["IdCiudad"]);
                    proveedor.Direccion = dt.Rows[0]["Direccion"].ToString();
                    proveedor.Ciudad = dt.Rows[0]["NombreCiudad"].ToString();
                    proveedor.Fono = dt.Rows[0]["Fono"].ToString();
                    proveedor.Rut = dt.Rows[0]["RutProveedor"].ToString();
                    proveedor.RazonSocial = dt.Rows[0]["RazonSocial"].ToString();
                    proveedor.Vendedor = dt.Rows[0]["NombreVendedor"].ToString();
                    proveedor.IdVendedor = dt.Rows[0]["IdVendedor"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return proveedor;
        }

        /**
         * Recibe un nombre de usuario o un correo electrónico y devuelve los datos del usuario al que pertenecen.
         * En caso de que no exista tal usuario/correo, devuelve NULL
         */
        public Proveedor_EN BuscarProveedorVendedorEmpresa(Empresa_EN busqueda, string idVendedor)
        {
            Proveedor_EN proveedor = new Proveedor_EN();
            Conexion nueva_conexion = new Conexion();
            try
            {
                string select = "Select *" +
                    " from Proveedor p, Ciudad c, Vendedor v, VendedorEmpresa ve" +
                    " where v.IdCiudad = c.IdCiudad AND v.IdProveedor = p.IdProveedor AND v.IdVendedor = ve.IdVendedor" +
                    " AND ve.IdEmpresa ='" + busqueda.ID + "' AND v.IdVendedor ='" + idVendedor + "'";
                nueva_conexion.SetQuery(select);
                DataTable dt = nueva_conexion.QuerySeleccion();
                if (dt != null) //Teóricamente solo debe de devolver una sola fila debido a que tanto el usuario como el email son claves alternativas (no nulos y no repetidos)
                {
                    proveedor.ID = Convert.ToInt16(dt.Rows[0]["IdProveedor"]);
                    proveedor.IdCiudad = Convert.ToInt16(dt.Rows[0]["IdCiudad"]);
                    proveedor.Direccion = dt.Rows[0]["Direccion"].ToString();
                    proveedor.Ciudad = dt.Rows[0]["NombreCiudad"].ToString();
                    proveedor.Fono = dt.Rows[0]["Fono"].ToString();
                    proveedor.Rut = dt.Rows[0]["RutProveedor"].ToString();
                    proveedor.RazonSocial = dt.Rows[0]["RazonSocial"].ToString();
                    proveedor.Vendedor = dt.Rows[0]["NombreVendedor"].ToString();
                    proveedor.IdVendedor = dt.Rows[0]["IdVendedor"].ToString();
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }

            return proveedor;
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarProveedor(Proveedor_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";
                

                update = "Update Proveedor set RazonSocial='" + e.RazonSocial + 
                    "' where Proveedor.IdProveedor =" + e.ID;
                nueva_conexion.SetQuery(update);

                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de actualizar el usuario si sufre alguna modificacion en alguno de sus campos
         **/

        public void actualizarVendedor(Proveedor_EN e)
        {
            Conexion nueva_conexion = new Conexion();

            try
            {
                string update = "";


                update = "Update Vendedor set Direccion = '" + e.Direccion + "',IdCiudad  = " + e.IdCiudad +
                    ",NombreVendedor='" + e.Vendedor + "',Fono='" + e.Fono +
                    "' where Vendedor.IdVendedor ='" + e.IdVendedor + "'";
                nueva_conexion.SetQuery(update);

                nueva_conexion.EjecutarQuery();
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally { nueva_conexion.Cerrar_Conexion(); }
        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarCiudades()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Ciudad" +
                                    " order by Ciudad.NombreCiudad");
            DataTable dt = nueva_conexion.QuerySeleccion();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Ciudad_EN c = new Ciudad_EN();
                c.ID = Convert.ToInt32(dt.Rows[i]["IdCiudad"].ToString());
                c.Nombre = dt.Rows[i]["NombreCiudad"].ToString();

                lista.Add(c);
            }

            return lista;

        }

        /**
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public int GetIdCiudad(string nombreCiudad)
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Ciudad u" +
                                    " where u.NombreCiudad ='" + nombreCiudad + "'");
            DataTable dt = nueva_conexion.QuerySeleccion();
            int id = 0;
            if (dt != null)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdCiudad"].ToString());
            }

            return id;

        }

    }
}
