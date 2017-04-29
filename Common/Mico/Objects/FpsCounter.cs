using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Shapes;

namespace Mico.Objects
{
    public class FpsCounter : Shape
    {
        int export_cnt = 0;
        float pass_time = 0;
        int fps = 0;

        public FpsCounter()
        {
        }

        protected internal override void OnUpdate(object Unknown = null)
        {
            pass_time += (float)Time.DeltaTime.TotalSeconds;


            if (pass_time >= 1.0f)
            {
                fps = export_cnt;
                export_cnt = 0;
                pass_time -= 1.0f;
            }

            base.OnUpdate(Unknown);
        }

        protected internal override void OnExport(object Unknown = null)
        {
            export_cnt++;

            base.OnExport(Unknown);
        }


        public int Fps
        {
            get => fps;
        }
    }
}
