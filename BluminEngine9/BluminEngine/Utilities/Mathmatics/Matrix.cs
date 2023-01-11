using BluminEngine9.Objects.Componants;
using BluminEngine9.Utilities.Mathmatics.Vectors;
using BluminEngine9.Objects.Componants;
using OpenTK.Mathematics;
using Vector2 = BluminEngine9.Utilities.Mathmatics.Vectors.Vector2;
using Vector3 = BluminEngine9.Utilities.Mathmatics.Vectors.Vector3;

namespace BluminEngine9.Utilities.Mathmatics
{
    public class Matrix
    {
        public const int SIZE = 4;
        public float[] elements { get; private set; } = new float[SIZE * SIZE];

        public static Matrix identity()
        {
            Matrix result = new Matrix();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    result.Set(i, j, 0);
                }
            }
            result.Set(0, 0, 1);
            result.Set(1, 1, 1);
            result.Set(2, 2, 1);
            result.Set(3, 3, 1);
            return result;
        }


        public static Matrix translate(Vector3 translate)
        {
            Matrix result = Matrix.identity();
            result.Set(3, 0, translate.x);
            result.Set(3, 1, translate.y);
            result.Set(3, 2, translate.z);
            return result;
        }
        public static Matrix translate(Vector2 translate)
        {
            Matrix result = Matrix.identity();
            result.Set(3, 0, translate.x);
            result.Set(3, 1, translate.y);
            return result;
        }
        public static Matrix rotate(float angle, Vector3 axis)
        {
            Matrix result = Matrix.identity();

            float cos = (float)Math.Cos(Mathf.toRadians(angle));
            float sin = (float)Math.Sin(Mathf.toRadians(angle));
            float C = 1 - cos;

            result.Set(0, 0, cos + axis.x * axis.y * C);
            result.Set(0, 1, axis.x * axis.y * C - axis.z * sin);
            result.Set(0, 2, axis.x * axis.z * C + axis.y * sin);
            result.Set(1, 0, axis.y * axis.x * C + axis.z * sin);
            result.Set(1, 1, cos + axis.y * axis.y * C);
            result.Set(1, 2, axis.y * axis.z * C - axis.x * sin);
            result.Set(2, 0, axis.z * axis.x * C - axis.y * sin);
            result.Set(2, 1, axis.z * axis.y * C + axis.x * sin);
            result.Set(2, 2, cos + axis.z * axis.z * C);

            return result;
        }
        public static Matrix scale(Vector3 scalar)
        {
            Matrix result = Matrix.identity();

            result.Set(0, 0, scalar.x);
            result.Set(1, 1, scalar.y);
            result.Set(2, 2, scalar.z);

            return result;
        }
        public static Matrix scale(Vector2 scalar)
        {
            Matrix result = Matrix.identity();

            result.Set(0, 0, scalar.x);
            result.Set(1, 1, scalar.y);
            result.Set(2, 2, scalar.y);

            return result;
        }
        public static Matrix transform(Transform transform)
        {
            Matrix result = Matrix.identity();
            Vector3 position = transform.Position;
            Vector3 rotation = transform.Rotation;
            Vector3 scale = transform.Scale;

            Matrix translationMatrix = Matrix.translate(position);

            Matrix rotXMatrix = Matrix.rotate(rotation.x, new Vector3(1, 0, 0));
            Matrix rotYMatrix = Matrix.rotate(rotation.y, new Vector3(0, 1, 0));
            Matrix rotZMatrix = Matrix.rotate(rotation.z, new Vector3(0, 0, 1));

            Matrix scaleMatrix = Matrix.scale(scale);

            Matrix rotationMatrix = (rotXMatrix * (rotYMatrix* rotZMatrix));

            result = (translationMatrix* (rotationMatrix* scaleMatrix));
            return result;
        }
        public static Matrix transform(Vector2 pos, Vector3 rotatio, Vector2 scal)
        {
            Matrix result = Matrix.identity();
            Vector2 position = pos;
            Vector3 rotation = rotatio;
            Vector2 scale = scal;

            Matrix translationMatrix = Matrix.translate(position);

            Matrix rotXMatrix = Matrix.rotate(rotation.x, new Vector3(1, 0, 0));
            Matrix rotYMatrix = Matrix.rotate(rotation.y, new Vector3(0, 1, 0));
            Matrix rotZMatrix = Matrix.rotate(rotation.z, new Vector3(0, 0, 1));

            Matrix scaleMatrix = Matrix.scale(scale);

            Matrix rotationMatrix = (rotXMatrix* (rotYMatrix * rotZMatrix));

            result = (translationMatrix* (rotationMatrix * scaleMatrix));
            return result;
        }


        public static Matrix OrthoMatrix(float left, float right, float bottom, float top, float Zfar, float Znear)
        {
            Matrix orthomatrix = Matrix.identity();
            orthomatrix.Set(0, 0, 2 / (right - left));
            orthomatrix.Set(0, 1, 0);
            orthomatrix.Set(0, 2, 0);
            orthomatrix.Set(0, 3, 0);

            orthomatrix.Set(1, 0, 0);
            orthomatrix.Set(1, 1, 2 / (top - bottom));
            orthomatrix.Set(1, 2, 0);
            orthomatrix.Set(1, 3, 0);

            orthomatrix.Set(2, 0, 0);
            orthomatrix.Set(2, 1, 0);
            orthomatrix.Set(2, 2, 2 / (Zfar - Znear));
            orthomatrix.Set(2, 3, 0);

            orthomatrix.Set(3, 0, -(right + left) / (right - left));
            orthomatrix.Set(3, 1, -(top + bottom) / (top - bottom));
            orthomatrix.Set(3, 2, -(Zfar + Znear) / (Zfar - Znear));
            orthomatrix.Set(3, 3, 1);

            return orthomatrix;
        }
        public static Matrix projection(float fov, float aspect, float near, float far)
        {
            Matrix result = Matrix.identity();

            float tanFOV = (float)Math.Tan(Mathf.toRadians(fov / 2));
            float range = far - near;

            result.Set(0, 0, 1.0f / (aspect * tanFOV));
            result.Set(1, 1, 1.0f / tanFOV);
            result.Set(2, 2, -((far + near) / range));
            result.Set(2, 3, -1.0f);
            result.Set(3, 2, -((2 * far * near) / range));
            result.Set(3, 3, 0.0f);

            return result;
        }
        public static Matrix view(Vector3 position, Vector3 rotation)
        {
            Matrix result = Matrix.identity();

            Vector3 negative = new Vector3(-position.x, -position.y, -position.z);
            Matrix translationMatrix = Matrix.translate(negative);
            Matrix rotXMatrix = Matrix.rotate(rotation.x, new Vector3(1, 0, 0));
            Matrix rotYMatrix = Matrix.rotate(rotation.y, new Vector3(0, 1, 0));
            Matrix rotZMatrix = Matrix.rotate(rotation.z, new Vector3(0, 0, 1));

            Matrix rotationMatrix = (rotZMatrix * (rotYMatrix* rotXMatrix));

            result = (translationMatrix * rotationMatrix);

            return result;
        }
        
        
        public float Get(int x, int y)
        {
            return elements[y * SIZE + x];
        }


        public void Set(int x, int y, float data)
        {
            elements[y * SIZE + x] = data;
        }


        public static Matrix operator * (Matrix m1, Matrix m2)
        {
            Matrix result = Matrix.identity();
            for (int i = 0; i < Matrix.SIZE; i++)
            {
                for (int j = 0; j < Matrix.SIZE; j++)
                {
                    result.Set(i, j, m1.Get(i, 0) * m2.Get(0, j) +
                            m1.Get(i, 1) * m2.Get(1, j) +
                            m1.Get(i, 2) * m2.Get(2, j) +
                            m1.Get(i, 3) * m2.Get(3, j));
                }
            }
            return result;
        }


        public static implicit operator Matrix4(Matrix m1)
        {
            Matrix4 m4 = Matrix4.Identity;
            m4.Column0 = new Vector4(m1.Get(0, 0), m1.Get(1, 0), m1.Get(2, 0), m1.Get(3, 0));
            m4.Column1 = new Vector4(m1.Get(0, 1), m1.Get(1, 1), m1.Get(2, 1), m1.Get(3, 1));
            m4.Column2 = new Vector4(m1.Get(0, 2), m1.Get(1, 2), m1.Get(2, 2), m1.Get(3, 2));
            m4.Column3 = new Vector4(m1.Get(0, 3), m1.Get(1, 3), m1.Get(2, 3), m1.Get(3, 3));
            return m4;
        }

        public static implicit operator Matrix(Matrix4 m1)
        {
            Matrix m = Matrix.identity();

            m.Set(0, 0, m1.Column0.X); m.Set(0, 1, m1.Column0.Y); m.Set(0, 2, m1.Column0.Z); m.Set(0, 3, m1.Column0.W);
            m.Set(1, 0, m1.Column1.X); m.Set(1, 1, m1.Column1.Y); m.Set(1, 2, m1.Column1.Z); m.Set(1, 3, m1.Column1.W);
            m.Set(2, 0, m1.Column2.X); m.Set(2, 1, m1.Column2.Y); m.Set(2, 2, m1.Column2.Z); m.Set(2, 3, m1.Column2.W);
            m.Set(3, 0, m1.Column3.X); m.Set(3, 1, m1.Column3.Y); m.Set(3, 2, m1.Column3.Z); m.Set(3, 3, m1.Column3.W);
            return m;
        }

    }
}
