using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Movimiento_EN
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
        private string responsable;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string Responsable
        {
            get { return responsable; }
            set { responsable = value; }
        }

        //Declaramos el nombre del user en private
        private string razonSocial;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        //Declaramos el nombre del user en private
        private string observaciones;

        //Declaramos el nombre del user en public para poder utilizarlo
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        //Declaramos el nombre de usuario del user en private
        private string area;
        //Declaramos el nombre de usuario del user en public para poder utilizarlo
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        //Declaramos el verificado del user en private
        private string documento;

        //Declaramos el verificado del user en public para poder utilizarlo
        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private string tipoMovimiento;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string TipoMovimiento
        {
            get { return tipoMovimiento; }
            set { tipoMovimiento = value; }
        }
        //Declaramos el id del user en private
        private int idTipoMovimiento;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdTipoMovimiento
        {
            get { return idTipoMovimiento; }
            set { idTipoMovimiento = value; }
        }

        //Declaramos el id del user en private
        private int idDocumento;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdDocumento
        {
            get { return idDocumento; }
            set { idDocumento = value; }
        }

        //Declaramos el id del user en private
        private string idPago;

        //Declaramos el id del user en public para poder utilizarlo
        public string IdPago
        {
            get { return idPago; }
            set { idPago = value; }
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
        private int idProducto;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        //Declaramos el id del user en private
        private int numDocumento;

        //Declaramos el id del user en public para poder utilizarlo
        public int NumDocumento
        {
            get { return numDocumento; }
            set { numDocumento = value; }
        }

        //Declaramos el id del user en private
        private int cantidad;

        //Declaramos el id del user en public para poder utilizarlo
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        //Declaramos el id del user en private
        private double precioUnitario;

        //Declaramos el id del user en public para poder utilizarlo
        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        //Declaramos el id del user en private
        private double total;

        //Declaramos el id del user en public para poder utilizarlo
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        //Declaramos la fecha del último ingreso del usuario

        private DateTime fechaMovimiento;
        public DateTime FechaMovimiento
        {
            get { return fechaMovimiento; }
            set { fechaMovimiento = value; }
        }

        //Declaramos la fecha de registro del usuario
        private DateTime fechaDocumento;


        public DateTime FechaDocumento
        {
            get { return fechaDocumento; }
            set { fechaDocumento = value; }
        }

        //Declaramos el constructor de la clase User_EN
        public Movimiento_EN()
        {
            id = "";
            responsable = "";
            razonSocial = "";
            observaciones = "";
            documento = "";
            cantidad = 0;
            numDocumento = 0;
            tipoMovimiento= "";
            idTipoMovimiento = 0;
            idDocumento = 0;
            idProveedor = 0;
            precioUnitario = 0;
            total = 0;
            idPago = "0";
            FechaDocumento = DateTime.Now;
            fechaMovimiento = DateTime.Now;
        }

    }
}
