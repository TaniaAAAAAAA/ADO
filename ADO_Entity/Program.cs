using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            UniversityEntities context =new UniversityEntities();
            foreach (var item in context.Student)
            {
                Console.WriteLine($"{item.Name}  {item.Surname}  ===>> {item.Groups.Name}");
            }

            //Console.WriteLine("Enter name student : ");
            //string name = Console.ReadLine();

            //foreach (var item in context.Student)
            //{
            //    if (item.Name == name)
            //    {
            //        context.Student.Remove(item);
            //    }
            //}


            //context.SaveChanges();
            //foreach (var item in context.Student)
            //{
            //    Console.WriteLine($"{item.Name}  {item.Surname}  ===>> {item.Groups.Name}");
            //}

            //-----------------------Update-------------------------

            //    Console.WriteLine("Enter name group : ");
            //    string num = Console.ReadLine();

            //    foreach (var item in context.Student)
            //    {
            //        if (item.Name == "Alina")
            //        {
            //            item.Groups.Name=num;
            //            break;
            //        }
            //    }

            //    context.SaveChanges();
            //    foreach (var item in context.Student)
            //    {
            //        Console.WriteLine($"{item.Name}  {item.Surname}  ===>> {item.Groups.Name}");
            //    }




            //---------------------   if (item.Groups.Name == "B123")-------------------


            // Console.WriteLine("----------------if (item.Groups.Name == 'B123')----------------");
            // foreach (var item in context.Student)
            // {
            //     if (item.Groups.Name == "B123")
            //     {
            //         Console.WriteLine($"{item.Name}  {item.Surname}  ===>> {item.Groups.Name}");
            //     }
            // }



            // Console.WriteLine("----------------if (item.Groups.Name == 'B123'|| item.Groups.Name == 'Pr456')----------------");
            // int num = 0;
            // foreach (var item in context.Student)
            // {
            //     if (item.Groups.Name == "B123"|| item.Groups.Name == "Pr456")
            //     {
            //         num++;
            //     }
            // }
            // Console.WriteLine($"Count students : {num}");



            // Console.WriteLine("--------------------------Max mark C++  ----------");
            // var mark = context.Achievement.Where(x => x.Subject.Name == "C++").Select(x => x.Mark).Max();

            // var res = context.Achievement.Where(x => x.Subject.Name == "C++").Where(x => x.Mark == mark).FirstOrDefault().Student;

            // Console.WriteLine(mark);
            //Console.WriteLine($"{res.Name} {res.Surname}");


            //Console.WriteLine("-------------------Вивести всі предмети, які читає Андрій Трофімчук------- ----------");

            //var id_department = context.Teacher.Where(x => x.Name == "Andrii" && x.Surname == "Trofimchuk").Select(x => x.Id_Department).FirstOrDefault();
            //var subjects = context.Subject.Where(x => x.Id_Department == id_department);
            //foreach (var item in subjects)
            //{
            //    Console.WriteLine(item.Name);
            //}



            //Console.WriteLine("----------------- Students with Name Olia --------------------------------------------");

            //var studentsWithNameOlia = context.Student.Where(x => x.Name == "Olia").Count();
            //Console.WriteLine($"Count tudents with Name Olia {studentsWithNameOlia}");








            Console.WriteLine("-------------------------Студенту з мінімальною оцінкою по предмету С# змінити прізвище----------");
            var mark = context.Achievement.Where(x => x.Subject.Name == "C#").Select(x => x.Mark).Min();

            var res = context.Achievement.Where(x => x.Subject.Name == "C#").Where(x => x.Mark == mark).FirstOrDefault().Student;
            res.Surname = "NewSurnameYessss";
            context.SaveChanges();

            Console.WriteLine(mark);
            Console.WriteLine($"{res.Name} {res.Surname}");

        }
    }
}
