using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.World
{
    public static class Time
    {
        static DateTime g_LastTime = DateTime.Now;
        static DateTime g_NowTime = DateTime.Now;
        static TimeSpan g_DeltaTime = g_NowTime - g_LastTime;

        internal static void Update()
        {
            g_LastTime = g_NowTime;
            g_NowTime = DateTime.Now;
            g_DeltaTime = g_NowTime - g_LastTime;
        }

        public static DateTime NowTime
        {
            get => g_NowTime;
        }

        public static TimeSpan DeltaTime
        {
            get => g_DeltaTime;
        }
    }
}
