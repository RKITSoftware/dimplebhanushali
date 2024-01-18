using System;

namespace Abstraction
{
    class Program
    {
        /// <summary>
        /// Entry point of the program to demonstrate abstraction with shapes.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Creating instances of derived classes
            Circle objCircle = new Circle(5);
            Square objSquare = new Square(4);

            // Using the common interface (abstract class)
            Console.WriteLine($"Area of Circle: {objCircle.CalculateArea()}");
            Console.WriteLine($"Area of Square: {objSquare.CalculateArea()}");

            // Wait for a key press before exiting
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Abstract class representing a generic shape.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Abstract method to calculate the area of the shape.
        /// </summary>
        /// <returns>The area of the shape.</returns>
        public abstract double CalculateArea();
    }

    /// <summary>
    /// Derived class representing a circle.
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Constructor to initialize the circle with a given radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Implementation of the abstract method to calculate the area of the circle.
        /// </summary>
        /// <returns>The area of the circle.</returns>
        public override double CalculateArea()
        {
            return 3.14 * Radius * Radius;
        }
    }

    /// <summary>
    /// Derived class representing a square.
    /// </summary>
    public class Square : Shape
    {
        /// <summary>
        /// Gets or sets the side length of the square.
        /// </summary>
        public double SideLength { get; set; }

        /// <summary>
        /// Constructor to initialize the square with a given side length.
        /// </summary>
        /// <param name="sideLength">The side length of the square.</param>
        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        /// <summary>
        /// Implementation of the abstract method to calculate the area of the square.
        /// </summary>
        /// <returns>The area of the square.</returns>
        public override double CalculateArea()
        {
            return SideLength * SideLength;
        }
    }
}
