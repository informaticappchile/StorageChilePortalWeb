using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;

namespace Logica
{
    public class LogicaEmpresa
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarEmpresa(Empresa_EN e)
        {
            Empresa_CAD userCad = new Empresa_CAD();
            userCad.InsertarEmpresa(e);
        }

        //Declaramos la funcion mostrar usuario donde llama al cad correspondiente
        public ArrayList MostrarEmpresa()
        {
            Empresa_EN e = new Empresa_EN();
            ArrayList a = new ArrayList();
            Empresa_CAD c = new Empresa_CAD();
            a = c.MostrarEmpresa(e);

            return a;
        }

        //Declaramos la funcion borrar usuario donde llama al cad correspondiente
        public bool BorrarEmpresa(string nombreEmpresa)
        {
            Empresa_CAD empresaDelete = new Empresa_CAD();
            return empresaDelete.BorrarEmpresa(nombreEmpresa);
        }

        //Declaramos la funcion buscar usuario donde llama al cad correspondiente
        public Empresa_EN BuscarEmpresa(string empresa)
        {
            Empresa_CAD busqueda = new Empresa_CAD();
            Empresa_EN empresaBuscado = busqueda.BuscarEmpresa(empresa);
            return empresaBuscado;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public void actualizarEmpresa(Empresa_EN e)
        {
            Empresa_CAD actUser = new Empresa_CAD();
            actUser.actualizarEmpresa(e);
        }

        public ArrayList MostrarEmpresas()
        {
            ArrayList a = new ArrayList();
            Empresa_CAD c = new Empresa_CAD();
            a = c.MostrarEmpresas();

            return a;
        }

    }
}
