using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

//Олесов Максим

/*3.Переделать программу «Пример использования коллекций» для решения следующих задач:
а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (частотный массив);
в) отсортировать список по возрасту студента;
г) *отсортировать список по курсу и возрасту студента;
д) разработать единый метод подсчета количества студентов по различным параметрам
выбора с помощью делегата и методов предикатов.*/

namespace Lesson6Homework
{
    class ThirdTask
    {
        public delegate int SortBy(Student st1, Student st2);
        public delegate void CountBy(List<Student> students);

        #region SortBy
        int SortByFirstName(Student st1, Student st2)
        {
            return String.Compare(st1.firstName, st2.firstName);
        }

        int SortByLastName(Student st1, Student st2)
        {
            return String.Compare(st1.lastName, st2.lastName);
        }

        int SortByAge(Student st1, Student st2)
        {
            if (st1.age > st2.age) return 1;
            if (st1.age < st2.age) return -1;
            return 0;
        }

        int SortByAgeCourse(Student st1, Student st2)
        {
            int comp = 0;
            if (st1.age > st2.age) comp = 1;
            if (st1.age < st2.age) comp = -1;
            if (comp != 0)
                return comp;
            if (st1.course > st2.course) comp = 1;
            if (st1.course < st2.course) comp = -1;
            return comp;
        }

        int SortByCourse(Student st1, Student st2)
        {
            if (st1.course > st2.course) return 1;
            if (st1.course < st2.course) return -1;
            return 0;
        }

        int SortByFaculty(Student st1, Student st2)
        {
            return String.Compare(st1.faculty, st2.faculty);
        }

        int SortByUniversity(Student st1, Student st2)
        {
            return String.Compare(st1.university, st2.university);
        }

        int SortByCity(Student st1, Student st2)
        {
            return String.Compare(st1.city, st2.city);
        }
        #endregion

        void CountByUniversity(List<Student> students)
        {
            //Создаем список университетов
            List<string> universities = new List<string>();
            //Проверяем университеты учащихся. Если университет еще не в списке, добавляем его
            foreach (Student student in students)
                if (!universities.Contains(student.university))
                    universities.Add(student.university);
            //Создаём частотный массив, равный по размеру количеству университетов в списке
            int[] count = new int[universities.Count];
            //Сравниваем университет каждого ученика с элементом списка, при совпадении учеличиваем соответствующий элемент частотного массива на 1
            foreach (Student student in students)
                for (int i = 0; i < universities.Count; i++)
                    if (student.university == universities[i])
                    {
                        count[i]++;
                    }
            for (int i = 0; i < count.Length; i++)
                Console.WriteLine($"В {universities[i]} учатся {count[i]} человек");
        }

        void CountByCity(List<Student> students)
        {
            List<string> cities = new List<string>();
            foreach (Student student in students)
                if (!cities.Contains(student.city))
                    cities.Add(student.city);
            int[] count = new int[cities.Count];
            foreach (Student student in students)
                for (int i = 0; i < cities.Count; i++)
                    if (student.city == cities[i])
                    {
                        count[i]++;
                    }
            for (int i = 0; i < count.Length; i++)
                Console.WriteLine($"Из {cities[i]}а учатся {count[i]} человек");
        }

        void CountByCourse(List<Student> students)
        {
            //Создаем частотный массив, по размеру равный количеству курсов
            int[] count = new int[6];
            //Если возраст студента 18-20, увеличиваем на 1 значение элемента, соответствующего курсу студента
            foreach (Student student in students)
                if (student.age >= 18 && student.age <= 20)
                    count[student.course - 1]++;
            for (int i = 0; i < count.Length; i++)
                if (count[i] != 0)
                Console.WriteLine($"На {i+1} курсе учатся {count[i]} человек в возрасте 18-20 лет");
        }

        public void Run()
        {
            View view = new View();
            view.Print("Задача 3. Доработка программы по работе с коллекцией студентов\n");

            int bakalavr = 0;
            int magistr = 0;
            List<Student> list = new List<Student>();
           // DateTime dt = DateTime.Now;
            string path = "students.txt";
            using StreamReader sr = new StreamReader(path, Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();

            List<SortBy> sortBy = new List<SortBy> { SortByFirstName, SortByLastName, SortByUniversity, SortByFaculty, SortByCourse, SortByAge, SortByAgeCourse, SortByCity };
            view.Print("Выберите метод сортировки списка:");
            int sortInput;
            do
            {
                sortInput = view.GetInt("\n1) По имени\n2) По фамилии\n3) По университету\n4) По факультету\n5) По курсу\n6) По возрасту\n7) По возрасту и курсу\n8) По городу\n0) Без сортировки");
            } while (sortInput < 0 || sortInput > sortBy.Count);

            if (sortInput != 0)
                list.Sort(new Comparison<Student>(sortBy[sortInput - 1]));
            Console.WriteLine("\nВсего студентов:" + list.Count);
            foreach (var v in list) Console.WriteLine($"{v.firstName} {v.lastName}   |   {v.university}   |   {v.faculty} факультет   |   {v.age} лет   |   {v.course} курс   |   {v.city}");
            // Console.WriteLine(DateTime.Now - dt);

            view.Print("\nПо какому параметру отобразить количество учащихся?");
            List<CountBy> countBy = new List<CountBy> { CountByUniversity, CountByCity, CountByCourse };
            int countInput;
            do
            {
                countInput = view.GetInt("\n1) Количество учащихся в каждом университете\n2) Количество учащихся из каждого города\n3) Количество учащихся в возрасте 18-20 лет на каждом курсе");
            } while (countInput < 1 || countInput > countBy.Count);
            countBy[countInput - 1](list);

            view.Pause();
        }
    }

    class Student
    {
        public string lastName;
        public string firstName;
        public string university;
        public string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        public int age;

        public Student(string firstName, string lastName, string university, string faculty, string department, int age, int course, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }
    }
}
