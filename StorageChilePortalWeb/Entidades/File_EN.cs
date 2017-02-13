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
using File_CAD_Class;


namespace File_EN_Class
{
    public class File_EN
    {
        //Declaramos el id del archivo en private
        private int id;
        //Declaramos el id del archivo en public para poder utilizarlo
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        //Declaramos el nombre del archivo en private
        private string nombre;
        //Declaramos el nombre del archivo en public para poder utilizarlo
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        //Declaramos la descripcion del archivo en private
        private string descripcion;
        //Declaramos la descripcion del archivo en public para poder utilizarlo
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
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
        //Declaramos la funcion mostrar archivo donde llama al cad correspondiente
        public ArrayList MostrarArchivo()
        {
            ArrayList a = new ArrayList();
            File_CAD c = new File_CAD();
            a = c.MostrarFile(this);

            return a;
        }

        public int SubirArchivo()
        {
            File_CAD upFile = new File_CAD();
            return upFile.SubirFile(this);
        }
        //Declaramos la funcion borrar archivo donde llama al cad correspondiente
        public void BorrarArchivo()
        {
            File_CAD deleteFile = new File_CAD();
            deleteFile.BorrarFile(this);
        }
        //Declaramos la funcion mostrar todos los archivos donde llama al cad correspondiente
        public ArrayList MostrarAllFiles()
        {
            File_CAD file = new File_CAD();
            ArrayList a = new ArrayList();
            a = file.MostrarTodosArchivos(this);
            return a;
        }

        public ArrayList BuscarFiles(string busqueda)
        {
            Nombre = busqueda;
            File_CAD cad = new File_CAD();
            ArrayList a = new ArrayList();
            a = cad.BuscarArchivos(this);
            return a;
        }

        //Declaramos la funcion mostrar datos de un archivo donde llama al cad correspondiente
        public ArrayList MostrarDatosUser()
        {
            File_CAD file = new File_CAD();
            ArrayList a = new ArrayList();
            a = file.MostrarDatosArchivo(this);
            return a;
        }
        //Declaramos la funcion mostrar archivos con usuario donde llama al cad correspondiente
        public ArrayList MostrarFilesUsuarioNombreEn()
        {
            File_CAD ListaArchivos = new File_CAD();
            ArrayList a = new ArrayList();
            a = ListaArchivos.MostrarFilesUsuarioNombre(this.Propietario);
            return a;
        }
        //Declaramos la funcion mostrar likes de un archivo donde llama al cad correspondiente
        public ArrayList MostrarLikes()
        {
            ArrayList a = new ArrayList();
            File_CAD showFile = new File_CAD();
            a=showFile.showLikes(this);
            return a;
        }
        //Declaramos el constructor de la clase File_EN
        public File_EN()
        {   
            id = 0;
            nombre = "";
            descripcion = "";
            fecha_creacion = new DateTime();
            fecha_modificacion = new DateTime();
        }

    }
}
