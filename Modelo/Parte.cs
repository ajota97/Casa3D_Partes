using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;


namespace Casa3D_Partes.Modelo
{
    class Parte:Objeto
    {
        //private float[] vertices;
        //private uint[] indices;
        //private Color4 color;
        public  Parte(float[] vertices, uint[] indices, Color4 color)
        {
            //this.vertices = vertices;
            //this.indices = indices;
            //this.color = color;

            //base.cargar(this.vertices, this.indices, this.color);
            base.cargar(vertices, indices, color);
        }



    }
}
