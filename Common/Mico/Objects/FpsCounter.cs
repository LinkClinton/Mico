using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.World;
using Mico.Shapes;

namespace Mico.Objects
{
    public class FpsCounter : Shape
    {
        int g_export_cnt = 0;
        float g_pass_time = 0;
        int g_fps = 0;

        public FpsCounter()
        {
            Micos.Add(this);
        }

        public override void OnUpdate(object Unknown = null)
        {
            g_pass_time += (float)Time.DeltaTime.TotalSeconds;


            if (g_pass_time >= 1.0f)
            {
                g_fps = g_export_cnt;
                g_export_cnt = 0;
                g_pass_time -= 1.0f;
            }

            base.OnUpdate(Unknown);
        }

        public override void OnExport(object Unknown = null)
        {
            g_export_cnt++;

            base.OnExport(Unknown);
        }

        public int Fps
        {
            get => g_fps;
        }
    }
}
