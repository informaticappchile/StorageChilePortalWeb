using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Pago_EN
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
        private bool estadoComprobante;

        //Declaramos el nombre del user en public para poder utilizarlo
        public bool EstadoComprobante
        {
            get { return estadoComprobante; }
            set { estadoComprobante = value; }
        }

        //Declaramos la fecha del último ingreso del usuario

        private DateTime fechaComprobante;
        public DateTime FechaComprobante
        {
            get { return fechaComprobante; }
            set { fechaComprobante = value; }
        }

        //Declaramos la contraseña del user en private
        private int numCheque;

        //Declaramos la contraseña del user en public para poder utilizarlo
        public int NumCheque
        {
            get { return numCheque; }
            set { numCheque = value; }
        }

        //Declaramos el verificado del user en private
        private string idMovimiento;

        //Declaramos el verificado del user en public para poder utilizarlo
        public string IdMovimiento
        {
            get { return idMovimiento; }
            set { idMovimiento = value; }
        }

        //Declaramos el nombre de la empresa del user en private
        private string razonSocial;
        //Declaramos el nombre de la empresa del user en public para poder utilizarlo
        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
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
        private int idTipoPago;

        //Declaramos el id del user en public para poder utilizarlo
        public int IdTipoPago
        {
            get { return idTipoPago; }
            set { idTipoPago = value; }
        }

        //Declaramos el id del user en private
        private string tipoPago;

        //Declaramos el id del user en public para poder utilizarlo
        public string TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }


        //Declaramos el constructor de la clase User_EN
        public Pago_EN()
        {
            id = "";
            tipoPago = "";
            razonSocial = "";
            idTipoPago = 0;
            idMovimiento = "";
            idProveedor = 0;
            numCheque = 0;
            fechaComprobante = DateTime.Now;
            estadoComprobante = false;
        }

    }
}
