using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace AplicacionCasa3D
{
    class Camara
    {
        private Vector3 camRigth = Vector3.UnitX;
        private Vector3 camUp    = Vector3.UnitY;
        private Vector3 camFront = Vector3.UnitZ;

        //rotacion sobre el eje X (radianes)
        private float picth;
        //rotacion sobre el eje Y (radianes)
        private float yaw = -MathHelper.PiOver2;

        //Campo de vision de la camara (radianes)
        private float fov = MathHelper.PiOver2;


        public Camara(Vector3 position, float aspecRatio)
        {
            Position = position;
            AspectRatio = aspecRatio;
        }

        //posicion de la camara
        public Vector3 Position { get; set; }

        //aspect ratio
        public float AspectRatio { private get; set; }

        public Vector3 Front => camFront;

        public Vector3 Up => camUp;

        public Vector3 Rigth => camRigth;

        //para actualizar los vectores
        private void UpdateVectors()
        {
            camFront.X = (float)Math.Cos(picth) * (float)Math.Cos(yaw);
            camFront.Y = (float)Math.Sin(picth);
            camFront.Z = (float)Math.Cos(picth) * (float)Math.Sin(yaw);

            //normalizamos
            camFront = Vector3.Normalize(camFront);

            camRigth = Vector3.Normalize(Vector3.Cross(camFront, Vector3.UnitY));
            camUp = Vector3.Normalize(Vector3.Cross(camRigth, camFront));
        }

        //de grados a radianes
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(picth);
            set
            {
                var angulo = MathHelper.Clamp(value, -89f, 89f);
                picth = MathHelper.DegreesToRadians(angulo);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(yaw);
            set
            {
                yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        //zoom
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(fov);
            set
            {
                var angulo = MathHelper.Clamp(value, 1f, 45f);
                fov = MathHelper.DegreesToRadians(angulo);
            }
        }

        //matrix4 para la vista
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + camFront, camUp);
        }

        //matriz de proyeccion
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio,0.01f, 100F );
        }










      
        
    }
}
