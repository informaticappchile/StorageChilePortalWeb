using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Producto_EN
    {
        //Declaramos el id del user en private
        private string id;

        //Declaramos el id del user en public para poder utilizarlo
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        //Declaramos el nombre del user en private
        private string nombreProveedor;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string NombreProveedor
        {
            get { return nombreProveedor; }
            set { nombreProveedor = value; }
        }

        //Declaramos el nombre del user en private
        private string descripcion;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        //Declaramos el nombre de usuario del user en private
        private string codProducto;
        //Declaramos el nombre de usuario del user en public para poder utilizarlo
        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }

        //Declaramos la contraseña del user en private
        private int cantMinStock;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public int CantMinStock
        {
            get { return cantMinStock; }
            set { cantMinStock = value; }
        }

        //Declaramos el verificado del user en private
        private string grupo;

        //Declaramos el verificado del user en public para poder utilizarlo
        public string Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private string unidadMedida;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        //Declaramos el id del user en private
        private int idGrupo;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }
        //Declaramos el id del user en private
        private int idMedidad;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdMedidad
        {
            get { return idMedidad; }
            set { idMedidad = value; }
        }

        //Declaramos el id del user en private
        private int idProveedor;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }

        //Declaramos el id del user en private
        private int stock;

        //Declaramos el id del user en public para poder utilizarlo
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }


        //Declaramos el constructor de la clase User_EN
        public Producto_EN()
        {
            id = "";
            descripcion = "";
            codProducto = "";
            grupo = "";
            cantMinStock = 0;
            unidadMedida = "";
            nombreProveedor = "";
            idMedidad = 0;
            idGrupo = 0;
            idProveedor = 0;
            Stock = 0;
        }

    }
}
