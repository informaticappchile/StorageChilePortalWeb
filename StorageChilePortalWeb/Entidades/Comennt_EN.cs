using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using Comennt_CAD_Class;

namespace Comennt_EN_Class
{
    public class Comentario_EN
    {
        private int user;

        public int User
        {
            get { return user; }
            set { user = value; }
        }

        private int file;

        public int File
        {
            get { return file; }
            set { file = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public void InsertarComentario()
        {
            Comennt_CAD insertComennt = new Comennt_CAD();
            insertComennt.InsertComennt(this);
        }

        public void BorrarComentario()
        {
            Comennt_CAD deleteComennt = new Comennt_CAD();
            deleteComennt.DeleteComennt(this);
        }

        public void MostrarComentarios()
        {
            ArrayList a = new ArrayList();

            Comennt_CAD showComennt = new Comennt_CAD();
            showComennt.ShowComennts(this);
        }
    }
}
