﻿using Ch1_L1_Checked;
namespace Ch1_L2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 值类型和引用类型
            // class是引用类型。
            Person Person1 = new Person("Peter", 30, "男", new DateOnly(1990, 7, 29));
            Person Person2 = new Person("Tom", 20, "男", new DateOnly(2000, 10, 1));
            Console.WriteLine($"Person1：{Person1}\nPerson2: {Person2}");
            Person1.Age += 1;
            Console.WriteLine(Person1);
            // Person2的信息会不会随Person3的改变而改变？
            Person Person3 = Person2;
            Person3.Name = "Jack";
            Console.WriteLine($"Person2: {Person2}\nPerson3: {Person3}");

            // struct是值类型。
            var p1 = new CoordsStruct(1, 1);
            // WITH expression generate a new copy, only applicable to struct.
            var p2 = p1 with { X = 2 };
            var p3 = p2;
            //修改p3的值，并不会造成p2的值的修改，这是值类型和引用类型的最大区别。
            p3.X = 3;
            Console.WriteLine($"p2:{p2};p3:{p3}");
            #endregion

            #region 静态成员
            // 静态成员
            var Rose = new Person("Rose", 10, "女", new DateOnly(2019, 3, 1));
            Console.WriteLine($"{Rose.Name}的年龄是{Rose.Age}岁。她是一个人类，因此有{Person.GetLegCount}个肢体。");
            #endregion

            #region quiz
            Imaginary v1 = new Imaginary(3, 2);
            Imaginary v2 = new Imaginary(3, -2);
            
            Console.WriteLine(Imaginary.Plus(v1, v2));
            Console.WriteLine(v1 + v2);
            Console.WriteLine(Imaginary.Minus(v1, v2));
            Console.WriteLine(v1 - v2);
            Console.WriteLine(Imaginary.Times(v1, v2));
            Console.WriteLine(v1 * v2);
            Console.WriteLine(Imaginary.Divide(v1, v2));
            Console.WriteLine(v1 / v2);
            #endregion
        }
    }
    public class Imaginary
    {
        public double Real { get; set; }
        public double Im { get; set; }

        public Imaginary(double real, double im)
        {
            Real = real;
            Im = im;
        }

        public static Imaginary Plus(Imaginary a, Imaginary b)
        {
            return new Imaginary(a.Real + b.Real, a.Im + b.Im);
        }

        public static Imaginary Minus(Imaginary a, Imaginary b)
        {
            return new Imaginary(a.Real - b.Real, a.Im - b.Im);
        }

        public static Imaginary Times(Imaginary a, Imaginary b)
        {
            return new Imaginary(a.Real * b.Real - a.Im * b.Im, a.Real * b.Im + a.Im * b.Real);
        }

        public Imaginary Conjugate()
        {
            return new Imaginary(Real, -Im);
        }

        double TimesByConjugate()
        {
            return Math.Pow(Real, 2) + Math.Pow(Im, 2);
        }

        Imaginary DivideByReal(double b)
        {
            return new Imaginary(Real / b, Im / b);
        }
        public static Imaginary Divide(Imaginary a, Imaginary b)
        {
            return Imaginary.Times(a, b.Conjugate()).DivideByReal(b.TimesByConjugate());
        }

        //public override string ToString() 
        //{
        //    return $"{Real}+j{Im}";
        //}

        // 模式匹配
        public override string ToString() => (Real, Im) switch
        {
            { Im: double im } when im == 0 => $"{Real}",
            { Real: double real, Im: > 0 } when real == 0 => $"j{Im}",
            { Real: double real, Im: < 0 } when real == 0 => $"j{-Im}",
            _ => $"{Real}+j{Im}",
        };
        

        public static Imaginary operator+ (Imaginary a, Imaginary b)
        {
            return Imaginary.Plus(a, b);
        }
        public static Imaginary operator- (Imaginary a, Imaginary b)
        {
            return Imaginary.Minus(a, b);
        }

        public static Imaginary operator* (Imaginary a, Imaginary b)
        {
            return Imaginary.Times(a, b);
        }

        public static Imaginary operator /(Imaginary a, Imaginary b)
        {
            return Imaginary.Divide(a, b);
        }
    }
}
