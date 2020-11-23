using System;

//Олесов Максим

//1.Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double,double).
//Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).

namespace Lesson6Homework
{
    public delegate double Fun(double x);
    public delegate double AnotherFun(double a, double x);

    class FirstTask
    {
        private void Table(Fun F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }

        private void Table(AnotherFun F, double a, double x, double b)
        {
            Console.WriteLine("------ A -------- X -------- Y ---");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |", a, x, F(a, x));
                x += 1;
            }
            Console.WriteLine("----------------------------------");
        }

        private double MyFunc(double x)
        {
            return x * x * x;
        }

        private double FirstNewFunc(double a, double x)
        {
            return a * Math.Pow(x, 2);
        }

        private double SecondtNewFunc(double a, double x)
        {
            return a * Math.Sin(x);
        }

        public void Run()
        {
            View view = new View();
            view.Print("Задача 1. Доработка программы вывода функции\n");

            #region OriginalCode
            /*
            // Создаем новый делегат и передаем ссылку на него в метод Table
            Console.WriteLine("Таблица функции MyFunc:");
            // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            Table(new Fun(MyFunc), -2, 2);
            Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
            // Упрощение(c C# 2.0).Делегат создается автоматически.            
            Table(MyFunc, -2, 2);
            Console.WriteLine("Таблица функции Sin:");
            Table(Math.Sin, -2, 2);      // Можно передавать уже созданные методы
            Console.WriteLine("Таблица функции x^2:");
            // Упрощение(с C# 2.0). Использование анонимного метода
            Table(delegate (double x) { return x * x; }, 0, 3);
            */
            #endregion

            #region NewCode
            view.Print("Таблица функции a*x^2");
            Table(FirstNewFunc, 2, -2, 2);

            view.Print("Таблица функции a*sin(x)");
            Table(SecondtNewFunc, 2, -2, 2);
            #endregion

            view.Pause();
        }
    }
}
