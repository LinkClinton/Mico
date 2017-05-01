using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Objects
{
    public static class Time
    {
        static DateTime m_last_time = DateTime.Now;
        static DateTime m_now_time = DateTime.Now;
        static TimeSpan m_delta_time = m_now_time - m_last_time;

        internal static void Update()
        {
            m_last_time = m_now_time;
            m_now_time = DateTime.Now;
            m_delta_time = m_now_time - m_last_time;
        }

        public static DateTime NowTime
        {
            get => m_now_time;
        }

        public static TimeSpan DeltaTime
        {
            get => m_delta_time;
        }

        public static float DeltaSeconds
        {
            get => (float)m_delta_time.TotalSeconds;
        }

    }
}
