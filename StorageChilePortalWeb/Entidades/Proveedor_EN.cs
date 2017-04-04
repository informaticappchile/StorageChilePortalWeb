using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Proveedor_EN
    {
        //Declaramos el id del user en private
        private int id;

        //Declaramos el id del user en public para poder utilizarlo
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //Declaramos el nombre del user en private
        private string razonSocial;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        //Declaramos el nombre de usuario del user en private
        private string rut;
        //Declaramos el nombre de usuario del user en public para poder utilizarlo
        public string Rut
        {
            get { return rut; }
            set { rut = value; }
        }

        //Declaramos la contraseña del user en private
        private string fono;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public string Fono
        {
            get { return fono; }
            set { fono = value; }
        }

        //Declaramos el verificado del user en private
        private string direccion;

        //Declaramos el verificado del user en public para poder utilizarlo
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private string ciudad;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private int idCiudad;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public int IdCiudad
        {
            get { return idCiudad; }
            set { idCiudad = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private string vendedor;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string Vendedor
        {
            get { return vendedor; }
            set { vendedor = value; }
        }

        //Declaramos el constructor de la clase User_EN
        public Proveedor_EN()
        {
            id = 0;
            razonSocial = "";
            direccion = "";
            rut = "";
            fono = "";
            ciudad = "";
            vendedor = "";
            idCiudad = 0;
        }

    }
}
