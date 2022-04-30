using Casa3D_Partes.Shaders;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace Casa3D_Partes.Modelo
{
    class Objeto
    {
        public float[] vertices;
        public uint[] indices;
        Color4 color;

        public Matrix4 model;
        public Matrix4 projection;
        public Matrix4 view;

        public int VertexBufferObject; //VBO
        public int VertexArrayObject;  //VAO
        public int IndexBufferObject;  //IBO

        public Shader shader;

        public Objeto() { }

        public void cargar(float[] vertice, uint[] indice, Color4 color)
        {
            this.vertices = vertice;
            this.indices = indice;
            this.color = color;

            VertexArrayObject = GL.GenVertexArray();
            VertexBufferObject = GL.GenBuffer();
            IndexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

            setColor();
        }



        private void setColor()
        {
            setShader();

            int locationColorUniform = GL.GetUniformLocation(shader.Handle, "u_color");
            GL.Uniform4(locationColorUniform, color);
        }

        private void setShader()
        {
            shader = new Shader("shader.vert", "shader.frag");
            shader.Use();
        }

        public void setMatrices(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            this.model = model;
            this.view = view;
            this.projection = projection;
        }

        private void setUniforms()
        {
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
        }

        public void dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
        }

        public void dibujarObjeto()
        {
            shader.Use();
            setUniforms(); GL.Rotate(10, 1, 1, 0);
             GL.BindVertexArray(VertexArrayObject);
            
             GL.DrawElements(PrimitiveType.Polygon, vertices.Length, DrawElementsType.UnsignedInt, 0);
           
        }

     

      

     

        


    }
}
