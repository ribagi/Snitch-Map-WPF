using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snitch_Map
{
    class Map
    {
        private int radius;
        private int radiusMap;
        private int factor;
        private string path;
        private Bitmap bitmap;
        private List<int> _X;
        private List<int> _Y;

        public List<int> X { set { _X = value; } }
        public List<int> Y { set { _Y = value; } }

        public Map(int radius, int factor, string path)
        {
            this.radius = radius;
            this.radiusMap = radius/factor;
            this.factor = factor;
            this.path = path;
            bitmap = new Bitmap(radiusMap*2+2, radiusMap*2+2);
        }

        public void Create()
        {
            for (int _x = -radiusMap, x = 0; _x < radiusMap; _x++)
            {
                for (int _y = -radiusMap, y = 0; _y < radiusMap; _y++)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(0, Color.Teal));
                }
            }

            using (Graphics grf = Graphics.FromImage(bitmap))
            {
                using (Brush brsh = new SolidBrush(Color.Black))
                {
                    grf.FillEllipse(brsh, 0, 0, radiusMap * 2, 2 * radiusMap);
                }
            }

            for (int i = 0; i < _X.Count; i++)
            {
                try
                {
                    int posX = Math.Abs((_X[i]/factor) + radiusMap);
                    int posY = Math.Abs((_Y[i] / factor) + radiusMap);
                    Console.WriteLine("\"" + posX + " " + posY + "\"");
                    bitmap.SetPixel(posX, posY, Color.Teal);
                    bitmap.SetPixel((int)(_X[i] / factor + 1 + radiusMap), (int)(_Y[i] / factor + radiusMap), Color.Teal);
                    bitmap.SetPixel((int)(_X[i] / factor + radiusMap), (int)(_Y[i] / factor + 1 + radiusMap), Color.Teal);
                    bitmap.SetPixel((int)(_X[i] / factor + 1 + radiusMap), (int)(_Y[i] / factor + 1 + radiusMap), Color.Teal);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + _X[i].ToString());
                }
            }

            bitmap.Save(path+"\\map.bmp");
        }
    }
}
