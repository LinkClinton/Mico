using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.World
{
    public static class Time
    {
        internal static DateTime g_LastTime = DateTime.Now;
        internal static DateTime g_NowTime = DateTime.Now;
        internal static TimeSpan g_DeltaTime = g_NowTime - g_LastTime;

        internal static void Update()
        {
            g_LastTime = g_NowTime;
            g_NowTime = DateTime.Now;
            g_DeltaTime = g_NowTime - g_LastTime;
        }

        public static DateTime NowTime
        {
            get { return g_NowTime; }
        }

        public static TimeSpan DeltaTime
        {
            get { return g_DeltaTime; }
        }
    }
}
