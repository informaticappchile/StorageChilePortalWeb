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
        public void InsertarProducto(Proveedor_EN e)
        {

            Conexion nueva_conexion = new Conexion();

            try
            {
                string insert = "insert into Proveedor(RutProveedor,RazonSocial, Direccion, IdCiudad, Fono, Vendedor) VALUES ('"
                    + e.Rut + "','" + e.RazonSocial + "','" + e.Direccion + "'," + e.IdCiudad + ",'" + e.Fono + "','" + e.Vendedor + "')";
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
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProveedores()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Proveedor p, Ciudad c"+
                                    " where p.IdCiudad = c.IdCiudad");
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
         * Se encarga de mostrar todos los usuarios del sistema.
         */

        public ArrayList MostrarProveedoresConProductos()
        {
            Conexion nueva_conexion = new Conexion();
            nueva_conexion.SetQuery("Select *" +
                                    " from Proveedor p, Ciudad c, ProveedorProducto pp" +
                                    " where p.IdCiudad = c.IdCiudad AND pp.IdProveedor = p.IdProveedor Group by p.IdProveedor");
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
                    " from Proveedor p, Ciudad c" +
                    " where (RazonSocial ='" + busqueda + "' OR RutProveedor ='" + busqueda + "') AND  p.IdCiudad = c.IdCiudad";
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
                    proveedor.Vendedor = dt.Rows[0]["Vendedor"].ToString();
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
                

                update = "Update Proveedor set Direccion = '" + e.Direccion + "',IdCiudad  = " + e.IdCiudad +
                    ",RutProveedor='" + e.Rut + "',RazonSocial='" + e.RazonSocial +"',Vendedor='" + e.Vendedor + "',Fono='" + e.Fono + 
                    "' where Proveedor.IdProveedor =" + e.ID;
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
