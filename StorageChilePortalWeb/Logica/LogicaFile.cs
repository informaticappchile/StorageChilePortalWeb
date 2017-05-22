using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaFile
    {

        public ArrayList MostrarFIles(string carpeta, Empresa_EN emp)
        {
            File_CAD fc = new File_CAD();
            return fc.MostrarFiles(carpeta, emp);
        }

        public void InsertarArchivo(File_EN f)
        {
            File_CAD filCad = new File_CAD();
            filCad.InsertarArchivo(f);
        }

        public List<string> MostrarArchivosFiltrados(string rut, Empresa_EN emp)
        {
            File_CAD filCad = new File_CAD();
            return filCad.MostrarArchivosFiltrados(rut, emp);
        }

        public ArrayList MostrarArchivosFiltrados(Personal_EN p, string carpeta, Empresa_EN emp, bool tipoFiltrado)
        {
            File_CAD filCad = new File_CAD();
            return filCad.MostrarArchivosFiltrados(p, carpeta, emp, tipoFiltrado);
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public bool BorrarArchivo(int idArchivo)
        {
            File_CAD archivoDelete = new File_CAD();
            return archivoDelete.BorrarArchivo(idArchivo);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public File_EN BuscarArchivo(Personal_EN p, string ruta)
        {
            File_CAD busqueda = new File_CAD();
            File_EN archivoBuscado = busqueda.BuscarArchivo(ruta, p);
            return archivoBuscado;
        }
    }
}
