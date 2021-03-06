﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using System.Collections;
using System.Data;

namespace Logica
{
    public class LogicaServicio
    {
        //Declaramos la funcion insertar usuario donde llama al cad correspondiente
        
        public void InsertarServicio(Servicio_EN s)
        {
            Servicio_CAD userCad = new Servicio_CAD();
            userCad.InsertarServicio(s);
        }

        //Declaramos la funcion insertar usuario donde llama al cad correspondiente

        public void InsertarServicioEmpresa(Empresa_EN e, List<Servicio_EN> ls)
        {
            Servicio_CAD userCad = new Servicio_CAD();
            userCad.InsertarServicioEmpresa(e, ls);
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
        public void actualizarServicioEmpresa(Empresa_EN e, List<Servicio_EN> ls)
        {
            Servicio_CAD actUser = new Servicio_CAD();
            actUser.actualizarServicioEmpresa(e, ls);
        }

        public ArrayList MostrarServiciosEmpresas(Empresa_EN e)
        {
            ArrayList a = new ArrayList();
            Servicio_CAD c = new Servicio_CAD();
            a = c.MostrarServiciosEmpresas(e);

            return a;
        }

        public List<Servicio_EN> MostrarServicios()
        {
            List<Servicio_EN> a = new List<Servicio_EN>();
            Servicio_CAD c = new Servicio_CAD();
            a = c.MostrarServicios();

            return a;
        }

        public DataTable MostrarServiciosEmpresas()
        {
            DataTable a = new DataTable();
            Servicio_CAD c = new Servicio_CAD();
            a = c.MostrarServiciosEmpresas();

            return a;
        }

        //Declaramos la funcion actualizar usuario donde llama al cad correspondiente
        public bool bajarServiciosEmpresa(Empresa_EN e)
        {
            Servicio_CAD actUser = new Servicio_CAD();
            return actUser.bajarServiciosEmpresa(e);
        }

        public List<Servicio_EN> MostrarServicioEmpresa(Empresa_EN e)
        {
            List<Servicio_EN> a = new List<Servicio_EN>();
            Servicio_CAD c = new Servicio_CAD();
            a = c.MostrarServicioEmpresa(e);

            return a;
        }

    }
}
