using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Math
{
    public class Vector3
    {
        float g_x;
        float g_y;
        float g_z;


        public override string ToString()
        {
            return "x = " + g_x + ", y = " + g_y + ", z = " + g_z;
        }

        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            g_x = x;
            g_y = y;
            g_z = z;
        }

        public float x
        {
            get { return g_x; }
        }

        public float y
        {
            get { return g_y; }
        }

        public float z
        {
            get { return g_z; }
        }

        public static Vector3 operator *(Vector3 vec, float value)
        {
            return new Vector3(vec.x * value, vec.y * value, vec.z * value);
        }

        public static Vector3 operator+(Vector3 vec1,Vector3 vec2)
        {
            return new Vector3(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);
        }

        public static Vector3 operator-(Vector3 vec1,Vector3 vec2)
        {
            return new Vector3(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);
        }

        internal float sqr(float x)
        {
            return x * x;
        }

        public Vector3 Normalize
        {
            get
            {
                float g_len = Length;
                return new Vector3(x / g_len, y / g_len, z / g_len);
            }
        }

        public float Length
        {
            get
            {
                return (float)System.Math.Sqrt(sqr(x) + sqr(y) + sqr(z));
            }
        }

        public float Distance(Vector3 vec)
        {
            return (float)System.Math.Sqrt(sqr(x - vec.x) + sqr(y - vec.y) + sqr(z - vec.z));
        }


        public static implicit operator Vector2(Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }
}
