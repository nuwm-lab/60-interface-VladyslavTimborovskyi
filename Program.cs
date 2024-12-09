using System;

namespace Geometry
{
    
    public interface IShape
    {
        void SetVertices(params (double X, double Y)[] vertices);
        void DisplayVertices();
        double CalculateArea();
    }

    
    public abstract class Shape : IShape
    {
        public abstract void SetVertices(params (double X, double Y)[] vertices);
        public abstract void DisplayVertices();
        public abstract double CalculateArea();

        
        public virtual void Info()
        {
            Console.WriteLine("This is a shape.");
        }
    }

    
    public class Triangle : Shape
    {
        protected (double X, double Y) _vertexA;
        protected (double X, double Y) _vertexB;
        protected (double X, double Y) _vertexC;

        public Triangle()
        {
            Console.WriteLine("Triangle created.");
        }

        ~Triangle()
        {
            Console.WriteLine("Triangle destroyed.");
        }

        public override void SetVertices(params (double X, double Y)[] vertices)
        {
            if (vertices.Length != 3)
                throw new ArgumentException("Triangle requires exactly 3 vertices.");

            _vertexA = vertices[0];
            _vertexB = vertices[1];
            _vertexC = vertices[2];
        }

        public override void DisplayVertices()
        {
            Console.WriteLine($"Vertex A: ({_vertexA.X}, {_vertexA.Y})");
            Console.WriteLine($"Vertex B: ({_vertexB.X}, {_vertexB.Y})");
            Console.WriteLine($"Vertex C: ({_vertexC.X}, {_vertexC.Y})");
        }

        public override double CalculateArea()
        {
            return Math.Abs((_vertexA.X * (_vertexB.Y - _vertexC.Y) +
                             _vertexB.X * (_vertexC.Y - _vertexA.Y) +
                             _vertexC.X * (_vertexA.Y - _vertexB.Y)) / 2.0);
        }

        public override void Info()
        {
            Console.WriteLine("This is a triangle.");
        }
    }

    
    public class Quadrilateral : Triangle
    {
        private (double X, double Y) _vertexD;

        public Quadrilateral()
        {
            Console.WriteLine("Quadrilateral created.");
        }

        ~Quadrilateral()
        {
            Console.WriteLine("Quadrilateral destroyed.");
        }

        public override void SetVertices(params (double X, double Y)[] vertices)
        {
            if (vertices.Length != 4)
                throw new ArgumentException("Quadrilateral requires exactly 4 vertices.");

            base.SetVertices(vertices[0], vertices[1], vertices[2]);
            _vertexD = vertices[3];
        }

        public override void DisplayVertices()
        {
            base.DisplayVertices();
            Console.WriteLine($"Vertex D: ({_vertexD.X}, {_vertexD.Y})");
        }

        public override double CalculateArea()
        {
            var triangle1Area = base.CalculateArea();
            var triangle2Area = Math.Abs((base._vertexC.X * (base._vertexA.Y - _vertexD.Y) +
                                          _vertexD.X * (base._vertexC.Y - base._vertexA.Y) +
                                          base._vertexA.X * (_vertexD.Y - base._vertexC.Y)) / 2.0);
            return triangle1Area + triangle2Area;
        }

        public override void Info()
        {
            Console.WriteLine("This is a quadrilateral.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            var triangle = new Triangle();
            triangle.SetVertices((0, 0), (4, 0), (0, 3));
            Console.WriteLine("Triangle:");
            triangle.DisplayVertices();
            Console.WriteLine($"Area: {triangle.CalculateArea()}");
            triangle.Info();

            Console.WriteLine();

            
            var quadrilateral = new Quadrilateral();
            quadrilateral.SetVertices((0, 0), (4, 0), (4, 3), (0, 3));
            Console.WriteLine("Quadrilateral:");
            quadrilateral.DisplayVertices();
            Console.WriteLine($"Area: {quadrilateral.CalculateArea()}");
            quadrilateral.Info();
        }
    }
}
