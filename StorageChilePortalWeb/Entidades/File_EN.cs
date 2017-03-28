using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Net.Mail;


namespace Entidades
{
    public class File_EN
    {
        //Declaramos el id del archivo en private
        private int idArchivo;
        //Declaramos el id del archivo en public para poder utilizarlo
        public int IDArchivo
        {
            get { return idArchivo; }
            set { idArchivo = value; }
        }

        //Declaramos el id del archivo en private
        private int idPersonal;
        //Declaramos el id del archivo en public para poder utilizarlo
        public int IDPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        //Declaramos el id del archivo en private
        private int idUsuario;
        //Declaramos el id del archivo en public para poder utilizarlo
        public int IDUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        //Declaramos el id del archivo en private

        //Declaramos el nombre del archivo en private
        private string ruta;
        //Declaramos el nombre del archivo en public para poder utilizarlo
        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
        }

        //Declaramos el nombre asociado del archivo en private
        private string nombreAsociado;
        //Declaramos el nombre asociado del archivo en public para poder utilizarlo
        public string NombreAsociado
        {
            get { return nombreAsociado; }
            set { nombreAsociado = value; }
        }

        //Declaramos el rut asociado del archivo en private
        private string rutAsociado;
        //Declaramos el rut asociado del archivo en public para poder utilizarlo
        public string RutAsociado
        {
            get { return rutAsociado; }
            set { rutAsociado = value; }
        }

        //Declaramos el rut asociado del archivo en private
        private string carpetaAsociado;
        //Declaramos el rut asociado del archivo en public para poder utilizarlo
        public string CarpetaAsociado
        {
            get { return carpetaAsociado; }
            set { carpetaAsociado = value; }
        }

        //Declaramos el rut asociado del archivo en private
        private string archivoAsociado;
        //Declaramos el rut asociado del archivo en public para poder utilizarlo
        public string ArchivoAsociado
        {
            get { return archivoAsociado; }
            set { archivoAsociado = value; }
        }

        //Declaramos la fecha de creacion del archivo en private
        private DateTime fecha_creacion;
        //Declaramos la fecha de creacion del archivo en public para poder utilizarlo
        public DateTime Fecha_creacion
        {
            get { return fecha_creacion; }
            set { fecha_creacion = value; }
        }

        //Declaramos la fecha de modificacion del archivo en private
        private DateTime fecha_modificacion;
        //Declaramos la fecha de modificacion del archivo en public para poder utilizarlo
        public DateTime Fecha_modificacion
        {
            get { return fecha_creacion; }
            set { fecha_creacion = value; }
        }

        //Declaramos el propietario del archivo en private
        private int propietario;
        //Declaramos el propietario del archivo en public para poder utilizarlo
        public int Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }
        //Declaramos la version del archivo en private
        private int version;
        //Declaramos la version del archivo en public para poder utilizarlo
        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        //Declaramos el rut asociado del archivo en private
        private string ubicacion;
        //Declaramos el rut asociado del archivo en public para poder utilizarlo
        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
        }
    }
}
