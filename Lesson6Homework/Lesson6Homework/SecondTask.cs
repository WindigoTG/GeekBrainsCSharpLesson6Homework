using System.Collections.Generic;
using System.IO;

//Олесов Максим

/*2.Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
б) Используйте массив(или список) делегатов, в котором хранятся различные функции.
в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений. Пусть она
возвращает минимум через параметр.*/

namespace Lesson6Homework
{
    class SecondTask
    {
        public delegate double Fun(double x);

        private double F1(double x)
        {
            return x * x - 50 * x + 10;
        }

        private double F2(double x)
        {
            return 30 * x * x + 20 * x - 35;
        }

        private double F3(double x)
        {
            return -16 * x * x - 27 * x - 38;
        }

        private void SaveFunc(string fileName, Fun F, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            if (b < a)
            {
                a = a + b;
                b = a - b;
                a = a - b;
            }
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }

        private double[] Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            min = double.MaxValue;
            double d;
            double[] results = new double[fs.Length / sizeof(double)];
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                d = bw.ReadDouble();
                results[i] = d;
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return results;
        }


        public void Run()
        {
            View view = new View();
            view.Print("Задача 2. Доработка программы нахождения минимума функции\n");

            List<Fun> fun = new List<Fun> { F1, F2, F3 };
            int f;

            view.Print("Пожалуйста, выберите функцию, для которой нужно найти минимальное значение:");
            do
            {
                f = view.GetInt("\n1) x^2 - 50x + 10\n2) 30x^2 + 20 - 35\n3) -16x^2 -27x - 38");
            } while (f < 0 || f > 3);

            view.Print("\nДавайте зададим отрезок для нахождения значиний функции");
            double a = view.GetDouble("\nВведите начальное значение отрезка");
            double b = view.GetDouble("\nВведите Конечное значение отрезка");

            
            double min;
            SaveFunc("data.bin", fun[f-1], a, b, 0.5);
            double[] results = Load("data.bin", out min);

            view.Print("\nЗначения функции:");
            foreach (double d in results)
                view.Print($"{d:0.00}     ", false);

            view.Print($"\nМинимальное значение функции: {min}");

            view.Pause();
        }
    }
}
