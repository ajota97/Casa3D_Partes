using System;
using System.Collections.Generic;
using System.Linq;

//agregando libreria openTK
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;
using Casa3D_Partes.Shaders;
using Casa3D_Partes.Modelo;


using System.Text;
using System.Threading.Tasks;



namespace Casa3D_Partes
{
    class Game : GameWindow
    {

        private Matrix4 view;
        private Matrix4 projection;
        private Matrix4 model;
        private Vector3 cameraPosition;
        private Vector3 target;
        private Vector3 up;


        List<Objeto> casa = new List<Objeto>();
      
        

        

        public Game(int ancho, int alto, String title) : base(ancho, alto, GraphicsMode.Default, title)
        {
            float[] vertRoof = {
            0.4f,0.3f,0.4f,  //p0
           -0.4f,0.3f,0.4f,  //p1
            0.0f,0.6f,0.4f,  //p2
            0.4f,0.3f,-0.4f, //p3
            0.0f,0.6f,-0.4f, //p4
           -0.4f,0.3f,-0.4f, //p5
        };

            uint[] indRoof = {
            //techo  caida der
            0, 2, 4,
            0, 3, 4,
            //techo caida izq
            1, 2, 4,
            1, 4, 5,
        };

            float[] vertWall = {
             0.4f,-0.3f,0.4f, //p0
             0.4f,0.3f,0.4f,  //p1
            -0.4f,0.3f,0.4f, //p2
            -0.4f,-0.3f,0.4f, //p3
             0.0f,0.6f,0.4f, //p4
             0.4f,-0.3f,-0.4f, //p5
             0.4f,0.3f,-0.4f, //p6
             0.0f,0.6f,-0.4f,//p7
            -0.4f,0.3f,-0.4f, //p8
            -0.4f,-0.3f,-0.4f,//p9

             0.1f,-0.3f,0.41f, //p10
             0.1f,0.0f,0.41f,  //p11
            -0.1f,0.0f,0.41f, //p12
            -0.1f,-0.3f,0.41f, //p13

            -0.4f,0.0f,0.4f, //p14
            0.4f,0.0f,0.4f,//p15

        };

            uint[] indWall = {
             //pared frontal
               0,15,10,
               10,11,15,
               1,14,15,
               1,2,14,
               3,12,14,
               3,12,13,
               1,2,4,
            //parel der
                0, 1, 6,
                0, 5, 6,
            //pared izq
                2, 3, 8,
                3, 8, 9,
            //pared del fondo
                5, 8, 9,
                5, 6, 8,
                6, 7, 8,
        };

            
                float[] vertDoor = {
             0.1f,-0.3f,0.4f, //p0
             0.1f,0.0f,0.4f,  //p1
            -0.1f,0.0f,0.4f, //p2
            -0.1f,-0.3f,0.4f, //p3
           
        };

                uint[] indDoor = {
             //puerta
                0, 1, 3,
                1, 2, 3,

        };
            casa.Add(new Parte(vertRoof, indRoof, Color4.Black));
            casa.Add(new Parte(vertWall, indWall, Color4.Red));
            casa.Add(new Parte(vertDoor, indDoor,Color4.Blue));
          

          
        }

       

        //metodo que se realiza al abrir la ventana por primer vez
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f)
                , Width / Height, 0.1f, 200.0f);

            model = Matrix4.Identity;

            setPosCam(2.0f, 1.0f, 3.0f);

            view = Matrix4.LookAt(cameraPosition, target, up);

            foreach (Objeto parte in casa)
            {
                parte.setMatrices(model, view, projection);
            }



            base.OnLoad(e);
        }

        //metodo que se ejecuta varias veces por segundo
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            view = Matrix4.LookAt(cameraPosition, target, up);



            foreach (Objeto parte in casa)
            {
                parte.dibujarObjeto();
            }



            Context.SwapBuffers(); //esto es el doble buffer
            base.OnRenderFrame(e);
        }

        //metodo que se ejecuta cada vez que se cambia el tamaño de la ventana
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }


        //metodo para limpiar nuestros buferes
        protected override void OnUnload(EventArgs e)
        {

            foreach (Objeto parte in casa)
            {
                parte.dispose();
            }

          

            base.OnUnload(e);
        }

        public void setPosCam(float x, float y, float z)
        {
            foreach (Objeto parte in casa)
            {
                parte.setMatrices(model, view, projection);
            }

            cameraPosition = new Vector3(x, y, z);
            target = new Vector3(0.0f, 0.0f, 0.0f); //punto de vision de la cam
            up = new Vector3(0.0f, 1.0f, 0.0f);
        }
       
    }
}
