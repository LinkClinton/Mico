using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Math
{
    public class Vector2
    {
        float g_x;
        float g_y;

        public override string ToString()
            => "x=" + g_x + ",y=" + g_y;
        

        public Vector2(float x = 0, float y = 0)
        {
            g_x = x;
            g_y = y;
        }


        public float x { get => g_x; }
        public float y { get => g_y; }

        public static Vector2 operator *(Vector2 vec, float value)
            => new Vector2(vec.x * value, vec.y * value);
        

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
            => new Vector2(vec1.x + vec2.x, vec1.y + vec2.y);
        
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
            => new Vector2(vec1.x - vec2.x, vec1.y - vec2.y);
        

        public Vector2 Normalize
        {
            get
            {
                float g_len = (float)System.Math.Sqrt(g_x * g_x + g_y * g_y);
                return new Vector2(g_x / g_len, g_y / g_len);
            }
        }

        private float sqr(float x) => x * x;

        public float Distance(Vector2 vec)
            => (float)System.Math.Sqrt(sqr(g_x - vec.g_x) + sqr(g_y - vec.g_y));
        
    }
}
