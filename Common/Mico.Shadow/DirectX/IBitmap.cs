using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX
{
    public partial class IBitmap
    {
        IntPtr source = IntPtr.Zero;

        float g_widht = 0;
        float g_height = 0;


        public IBitmap(IDevice device, string filename)
            => IDirectXBitmapCreate(out source, device, filename, ref g_widht, ref g_height); 

        ~IBitmap() => IDirectXBitmapDestory(source);

        
        public float Width { get => g_widht; }

        public float Height { get => g_height; }



        public static implicit operator IntPtr(IBitmap bitmap)
            => bitmap.source;

    }
}
