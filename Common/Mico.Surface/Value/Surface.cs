using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico
{
    public partial class Surface
    {
        private int g_origin_width;
        private int g_origin_height;

        private int g_width;
        private int g_height;

        private string g_ico;
        private string g_title;

        private IntPtr g_hwnd;

        private Message g_message;

        public float ScaleWidth
        {
            get { return (float)g_width / g_origin_width; }
        }

        public float ScaleHeight
        {
            get { return (float)g_height / g_origin_height; }
        }

        public int Width
        {
            get { return g_origin_width; }
        }

        public int Height
        {
            get { return g_origin_height; }
        }

        public int CurrentWidth
        {
            get { return g_width; }
        }

        public int CurrentHeight
        {
            get { return g_height; }
        }

        public string Ico
        {
            get { return g_ico; }
        }

        public string Title
        {
            get { return g_title; }
            set { g_title = value; SetWindowText(g_hwnd, g_title); }
        }

        public IntPtr Handle
        {
            get { return g_hwnd; }
        }

    }
}
