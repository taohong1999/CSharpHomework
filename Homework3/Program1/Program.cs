using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{

    public abstract class Sharp
    {


        public abstract double Area    //面积属性
        {
            get;
        }

        public abstract void ShowArea();

        public abstract void Draw();
    }

    //正方形
    public class Square : Sharp
    {
        private double mySide;
        public Square(double side)
        {
            mySide = side;
        }
        public override double Area
        {
            get
            {
                return mySide * mySide;
            }
        }

        public override void Draw()
        {
            Console.WriteLine("正在绘制正方形图案！");
        }

        public override void ShowArea()
        {
            Console.WriteLine("正方形的面积为：" + Area);
        }
    }

    //圆形
    public class Circle : Sharp
    {
        private double myRadius;
        public Circle(double Radius)
        {
            myRadius = Radius;
        }
        public override double Area
        {
            get
            {
                return myRadius* myRadius *System.Math.PI;
            }
        }

        public override void Draw()
        {
            Console.WriteLine("正在绘制圆形图案！");
        }

        public override void ShowArea()
        {
            Console.WriteLine("圆形的面积为：" + Area);
        }
    }

    //长方形
    public class Rectangle : Sharp
    {
        private double myWidth;
        private double myHeight;
        public Rectangle(double Width,double Height)
        {
            myHeight = Height;
            myWidth = Width;
        }
        public override double Area
        {
            get
            {
                return myWidth * myHeight;
            }
        }

        public override void Draw()
        {
            Console.WriteLine("正在绘制长方形图案！");
        }

        public override void ShowArea()
        {
            Console.WriteLine("长方形的面积为：" + Area);
        }
    }

    //三角形
    public class Triangle : Sharp
    {
        private double mySide1;
        private double mySide2;
        private double mySide3;
        public Triangle(double Side1, double Side2,double Side3)
        {
            mySide1 = Side1;
            mySide2 = Side2;
            mySide3 = Side3;
        }
        public override double Area
        {
            get
            {
                double p = (mySide1 + mySide2 + mySide3) / 2;
                return System.Math.Sqrt(p * (p - mySide1) * (p - mySide2) * (p - mySide3));
            }
        }

        public override void Draw()
        {
            Console.WriteLine("正在绘制三角形图案！");
        }

        public override void ShowArea()
        {
            Console.WriteLine("三角形的面积为：" + Area);
        }
    }



    public class SharpFactory
    {
        public static Sharp CreateSharp(string sharpName,double num1 = 0,double num2 = 0,double num3 = 0)
        {
            if(sharpName == "Square")
            {
                return new Square(num1);
            }
            else if(sharpName == "Circle")
            {
                return new Circle(num1);
            }
            else if(sharpName == "Rectangle")
            {
                return new Rectangle(num1, num2);
            }
            else if (sharpName == "Triangle")
            {
                return new Triangle(num1, num2, num3);
            }
            else
            {
                Console.WriteLine("没有对应的图形绘制方法");
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sharp sharp1 = SharpFactory.CreateSharp("Square", 9);  //创建一个正方形
            sharp1.Draw();
            sharp1.ShowArea();

            Sharp sharp2 = SharpFactory.CreateSharp("Circle", 9);  //创建一个圆形
            sharp2.Draw();
            sharp2.ShowArea();

            Sharp sharp3 = SharpFactory.CreateSharp("Rectangle", 6, 9);  //创建一个长方形
            sharp3.Draw();
            sharp3.ShowArea();

            Sharp sharp4 = SharpFactory.CreateSharp("Triangle", 6 , 6 , 6);  //创建一个三角形
            sharp4.Draw();
            sharp4.ShowArea();

            Console.WriteLine("按任意键继续...");
            Console.ReadKey();

        }
    }
}
