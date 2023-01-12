using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Utilities.Mathmatics.Vectors
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public float x { get => X; set => X = value; }
        public float y { get => Y; set => Y = value; }
        public float z { get => Z; set => Z = value; }

        private float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return a * t + b * (1.0f - t);
        }

        public bool Equals(Vector3 other)
        {
            if (x == other.x && y == other.y && z == other.z)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object? obj)
        {
            return obj is Vector3 && Equals((Vector3)obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return x + "/" + y + "/" + z;
        }


        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            if (left.Equals(right))
            {
                return false;
            }
            return true;
        }
        public static bool operator >=(Vector3 left, Vector3 right)
        {
            if (left.x >= right.x || left.y >= right.y || left.z >= right.z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector3 left, Vector3 right)
        {
            if (left.x <= right.x || left.y <= right.y || left.z <= right.z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector3 left, Vector3 right)
        {
            if (left.x > right.x || left.y > right.y || left.z > right.z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector3 left, Vector3 right)
        {
            if (left.x < right.x || left.y < right.y || left.z < right.z)
            {
                return true;
            }
            return false;
        }


        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
        }
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x / right.x, left.y / right.y, left.z / right.z);
        }
        public static Vector3 operator %(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x % right.x, left.y % right.y, left.z % right.z);
        }


        public static Vector3 operator +(float left, Vector3 right)
        {
            return new Vector3(left + right.x, left + right.y, left + right.z);
        }
        public static Vector3 operator -(float left, Vector3 right)
        {
            return new Vector3(left - right.x, left - right.y, left - right.z);
        }
        public static Vector3 operator *(float left, Vector3 right)
        {
            return new Vector3(left * right.x, left * right.y, left * right.z);
        }
        public static Vector3 operator /(float left, Vector3 right)
        {
            return new Vector3(left / right.x, left / right.y, left / right.z);
        }
        public static Vector3 operator %(float left, Vector3 right)
        {
            return new Vector3(left % right.x, left % right.y, left % right.z);
        }

        public static Vector3 operator +(Vector3 left, float right)
        {
            return new Vector3(left.x + right, left.y + right, left.z + right);
        }
        public static Vector3 operator -(Vector3 left, float right)
        {
            return new Vector3(left.x - right, left.y - right, left.z - right);
        }
        public static Vector3 operator *(Vector3 left, float right)
        {
            return new Vector3(left.x * right, left.y * right, left.z * right);
        }
        public static Vector3 operator /(Vector3 left, float right)
        {
            return new Vector3(left.x / right, left.y / right, left.z / right);
        }
        public static Vector3 operator %(Vector3 left, float right)
        {
            return new Vector3(left.x % right, left.y % right, left.z % right);
        }

        public static Vector3 operator +(int left, Vector3 right)
        {
            return new Vector3(left + right.x, left + right.y, left + right.z);
        }
        public static Vector3 operator -(int left, Vector3 right)
        {
            return new Vector3(left - right.x, left - right.y, left - right.z);
        }
        public static Vector3 operator *(int left, Vector3 right)
        {
            return new Vector3(left * right.x, left * right.y, left * right.z);
        }
        public static Vector3 operator /(int left, Vector3 right)
        {
            return new Vector3(left / right.x, left / right.y, left / right.z);
        }
        public static Vector3 operator %(int left, Vector3 right)
        {
            return new Vector3(left % right.x, left % right.y, left % right.z);
        }

        public static Vector3 operator +(Vector3 left, int right)
        {
            return new Vector3(left.x + right, left.y + right, left.z + right);
        }
        public static Vector3 operator -(Vector3 left, int right)
        {
            return new Vector3(left.x - right, left.y - right, left.z - right);
        }
        public static Vector3 operator *(Vector3 left, int right)
        {
            return new Vector3(left.x * right, left.y * right, left.z * right);
        }
        public static Vector3 operator /(Vector3 left, int right)
        {
            return new Vector3(left.x / right, left.y / right, left.z / right);
        }
        public static Vector3 operator %(Vector3 left, int right)
        {
            return new Vector3(left.x % right, left.y % right, left.z % right);
        }


        public static Vector3 operator +(Vector2 left, Vector3 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, right.z);
        }
        public static Vector3 operator -(Vector2 left, Vector3 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, right.z);
        }

        public static Vector3 operator +(Vector3 left, Vector2 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, left.z);
        }
        public static Vector3 operator -(Vector3 left, Vector2 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, left.z);
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }

        public static implicit operator Vector3(Vector3D<int> v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator Vector3D<int>(Vector3 v)
        {
            return new Vector3D<int>((int)v.x, (int)v.y, (int)v.z);
        }

        public static implicit operator Vector3(Vector3D<float> v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator Vector3D<float>(Vector3 v)
        {
            return new Vector3D<float>(v.x, v.y, v.z);
        }
    }

    public struct Vector2 : IEquatable<Vector2>
    {
        public float x { get => X; }
        public float y { get => Y;}

        private float X = 0, Y = 0;

        public Vector2(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }


        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return a * t + b * (1.0f - t);
        }


        public bool Equals(Vector2 other)
        {
            if (x == other.x && y == other.y)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object? obj)
        {
            return obj is Vector2 && Equals((Vector2)obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return x + "/" + y;
        }



        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            if (left.Equals(right))
            {
                return false;
            }
            return true;
        }
        public static bool operator >=(Vector2 left, Vector2 right)
        {
            if (left.x >= right.x || left.y >= right.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector2 left, Vector2 right)
        {
            if (left.x <= right.x || left.y <= right.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector2 left, Vector2 right)
        {
            if (left.x > right.x || left.y > right.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector2 left, Vector2 right)
        {
            if (left.x < right.x || left.y < right.y)
            {
                return true;
            }
            return false;
        }


        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x * right.x, left.y * right.y);
        }
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x / right.x, left.y / right.y);
        }
        public static Vector2 operator %(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x % right.x, left.y % right.y);
        }

        public static Vector2 operator +(float left, Vector2 right)
        {
            return new Vector2(left + right.x, left + right.y);
        }
        public static Vector2 operator -(float left, Vector2 right)
        {
            return new Vector2(left - right.x, left - right.y);
        }
        public static Vector2 operator *(float left, Vector2 right)
        {
            return new Vector2(left * right.x, left * right.y);
        }
        public static Vector2 operator /(float left, Vector2 right)
        {
            return new Vector2(left / right.x, left / right.y);
        }
        public static Vector2 operator %(float left, Vector2 right)
        {
            return new Vector2(left % right.x, left % right.y);
        }

        public static Vector2 operator +(Vector2 left, float right)
        {
            return new Vector2(left.x + right, left.y + right);
        }
        public static Vector2 operator -(Vector2 left, float right)
        {
            return new Vector2(left.x - right, left.y - right);
        }
        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(left.x * right, left.y * right);
        }
        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(left.x / right, left.y / right);
        }
        public static Vector2 operator %(Vector2 left, float right)
        {
            return new Vector2(left.x % right, left.y % right);
        }

        public static Vector2 operator +(int left, Vector2 right)
        {
            return new Vector2(left + right.x, left + right.y);
        }
        public static Vector2 operator -(int left, Vector2 right)
        {
            return new Vector2(left - right.x, left - right.y);
        }
        public static Vector2 operator *(int left, Vector2 right)
        {
            return new Vector2(left * right.x, left * right.y);
        }
        public static Vector2 operator /(int left, Vector2 right)
        {
            return new Vector2(left / right.x, left / right.y);
        }
        public static Vector2 operator %(int left, Vector2 right)
        {
            return new Vector2(left % right.x, left % right.y);
        }

        public static Vector2 operator +(Vector2 left, int right)
        {
            return new Vector2(left.x + right, left.y + right);
        }
        public static Vector2 operator -(Vector2 left, int right)
        {
            return new Vector2(left.x - right, left.y - right);
        }
        public static Vector2 operator *(Vector2 left, int right)
        {
            return new Vector2(left.x * right, left.y * right);
        }
        public static Vector2 operator /(Vector2 left, int right)
        {
            return new Vector2(left.x / right, left.y / right);
        }
        public static Vector2 operator %(Vector2 left, int right)
        {
            return new Vector2(left.x % right, left.y % right);
        }

        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator Vector2(Vector2D<int> v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector2D<int>(Vector2 v)
        {
            return new Vector2D<int>((int)v.x, (int)v.y);
        }

        public static implicit operator Vector2(Vector2D<float> v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector2D<float>(Vector2 v)
        {
            return new Vector2D<float>(v.x, v.y);
        }
    }
}
