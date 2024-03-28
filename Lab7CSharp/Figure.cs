using System;


namespace Lab7CSharp
{
    using System.Drawing;

    public abstract class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public Shape(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public abstract void Draw(Graphics g);
    }

    public class Hexagon : Shape
    {
        public int Side { get; set; }

        public Hexagon(int x, int y, int side, Color color)
            : base(x, y, color)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI / 6 * i;
                int x = (int)(X + Side * Math.Cos(angle));
                int y = (int)(Y + Side * Math.Sin(angle));
                points[i] = new Point(x, y);
            }
            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }

    public class Pentagon : Shape
    {
        public int Side { get; set; }

        public Pentagon(int x, int y, int side, Color color)
           : base(x, y, color)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[5];
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI / 5 * i;
                int x = (int)(X + Side * Math.Cos(angle));
                int y = (int)(Y + Side * Math.Sin(angle));
                points[i] = new Point(x, y);
            }
            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }

    public class Octagon : Shape
    {
        public int Side { get; set; }

        public Octagon(int x, int y, int side, Color color)
           : base(x, y, color)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[8];
            for (int i = 0; i < 8; i++)
            {
                double angle = 2 * Math.PI / 8 * i;
                int x = (int)(X + Side * Math.Cos(angle));
                int y = (int)(Y + Side * Math.Sin(angle));
                points[i] = new Point(x, y);
            }
            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }
}
