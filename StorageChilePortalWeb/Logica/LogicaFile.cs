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
    }
}
