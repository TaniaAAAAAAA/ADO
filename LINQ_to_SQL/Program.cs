using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_to_SQL
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

        static DataClasses1DataContext context;

        static void Main(string[] args)
        {
            context = new DataClasses1DataContext(connectionString);
            Console.OutputEncoding = Encoding.UTF8;

            // new_func(); //Books
            // func_array(); //Array
            func_list();

        }

        private static void func_list()
        {
           List<Good> list= FillList();
            // 1) Выбрать товары категории Mobile, цена которых превышает 1000 грн.
            //  var list1 = list.Where(x => x.Category == "Mobile" && x.Price > 1000);
            // PrintList(list1);


            // 2) Вывести название и цену тех товаров, которые не относятся к категории Kitchen,
            //цена которых превышает 1000 грн.
            // var list2 = list.Where(x => x.Category != "Kitchen" && x.Price > 1000);
            //PrintList(list2);


            //3) Вывести название товара и его категорию, который имеет максимальную цену.
            // var list3 = list.Where(x => x.Price== list.Select(y => y.Price).Max());
            // PrintList(list3);


            //4) Вычислить среднее значение всех цен товаров.
            // var list4 = list.Select(x => x.Price).Average();
            //Console.WriteLine($"среднее значение всех цен товаров : {list4}");


            // 5) Вывести список категорий без повторений.
            //var list5 = list.Select(x => x.Category).Distinct();
            //foreach (var item in list5)
            //{
            //    Console.WriteLine($"{item}");
            //}


            // 6) Вывести названия тех товаров, цены которых совпадают.
            //var list6 = list.Where(x => list.Count(y => y.Price == x.Price) > 1);
            //PrintList(list6);


            //7) Вывести названия и категории товаров в алфавитном порядке, упорядоченных по
            //названию.
            // var list7 = list.OrderBy(x => x.Title);
            // PrintList(list7);



            // 8) Проверить, содержит ли категория Car товары, с ценой от 1000 до 2000 грн.
            //var list8 = list.Where(x => x.Category == "Car" && x.Price >= 1000 && x.Price < 2000);
            //PrintList(list8);



            //9) Посчитать суммарное количество товаров категорий Сar и Mobile.
            // var list9 = list.Where(x => x.Category == "Car" || x.Category == "Mobile").Count();
            // Console.WriteLine($"суммарное количество товаров категорий Сar и Mobile ==> {list9}");



            // 10) Вывести список категорий и количество товаров каждой категории.

            var list10 = list.Select(x => x.Category).Distinct();
            foreach (var item in list10)
            {
                Console.WriteLine($"{item}  ==> { list.Where(x => x.Category==item).Count()}");
               
            }

            

        }

        private static void PrintList(IEnumerable<Good> list1)
        {
            foreach (var item in list1)
            {
                Console.WriteLine($"{item.Id} {item.Title} {item.Price} {item.Category}");
            }
        }

        private static List<Good> FillList()
        {
            List<Good> goods1 = new List<Good>()
{
new Good()
{ Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
new Good()
{ Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
new Good()
{ Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
new Good()
{ Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
new Good()
{ Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" }
};
            List<Good> goods2 = new List<Good>()
{
new Good()
{ Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
new Good()
{ Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
new Good()
{ Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
new Good()
{ Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
new Good()
{ Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
};
            return goods1.Concat(goods2).ToList();
        }

        private static void func_array()
        {
            int[] values1 = new int[5] { 1, 10, 5, 13, 4 };
            int[] values2 = new int[5] { 19, 1, 4, 9, 8 };

            //1) Посчитать среднее значение четных элементов, которые больше 10.
            // var array1 = values1.Concat(values2).Where(x => x % 2 == 0 ).Where(x=>x >= 10).Average();
            // Console.WriteLine(array1);

            //2) Выбрать только уникальные элементы из массивов values1 и values2.
            //var array1 = values1.Concat(values2).GroupBy(x => x).Where(x => x.Count() == 1);
            //foreach (var item in array1)
            //{
            //    Console.WriteLine(item.Key);
            //}

            //3) Найти максимальный элемент массива values2, который есть в массиве values1.
            //   var maxNum = values1.Intersect(values2).Max();
            // Console.WriteLine(maxNum);

            //4) Посчитать сумму элементов массивов values1 и values2, которые попадают
            //в диапазон от 5 до 15.
            //var arr = values1.Concat(values2).Where(x => x >= 5 && x < 15).Sum();
            //Console.WriteLine(arr);

            // 5) Отсортировать элементы массивов values1 и values2 по убыванию.
            var arra = values1.Concat(values2).OrderByDescending(x => x);
            foreach (var item in arra)
            {
                Console.WriteLine(item);
            }


        }

        private static void new_func()
        {
            // 1.Вибрати всі книги, кількість сторінок в яких більше 100
            //  var books = context.Books.Where(x => x.Pages > 100);
            // Print(books);

            //2.Вибрати всі книги, ім'я яких починається на літеру 'А' або 'а
            // var books1 = context.Books.Where(x => x.Name.ToLower().StartsWith("a"));
            // Print(books1);

            //  3.Вибрати всі книги автора L. Kostenko
            //var idAuthor = context.Authors.Where(x => x.Name == "L. Kostenko").Select(x => x.Id).FirstOrDefault();
            //var booksAuthora = context.Books.Where(x => x.Author_Id == idAuthor);
            //Print(booksAuthora);

            // 5.Вибрати всі книги, ім'я в яких складається менше ніж з 10-ти символів
            //var booksNameCountLetters = context.Books.Where(x => x.Name.Length < 10);
            //Print(booksNameCountLetters);

            //6.Вибрати книгу з максимальною кількістю сторінок 
            //var b = context.Books.Where(x => x.Pages==context.Books.Select(y=>y.Pages).Max());
            //Print(b);

            //7.Вибрати автора, який має найменше книг в базі даних
            //var leastBooksInDB=context.Authors.Where(x=>x.Books.Count()==context.Authors.Select(y=>y.Books.Count()).Min());
            //PrintAuthor(leastBooksInDB);

            //8. Вибрати імена всіх авторів, крім американських, розташованих в алфавітном порядку
            var authors = context.Authors.OrderBy(x => x.Name);
            PrintAuthor(authors);


        }

        private static void PrintAuthor(IQueryable<Author> leastBooksInDB)
        {
            foreach (var item in leastBooksInDB)
            {
                Console.WriteLine($"{item.Id}==>{item.Name}");
            }
        }

        private static void Print(IQueryable<Book> books)
        {
            foreach (var item in books)
            {
                Console.WriteLine($"{item.Id}==>{item.Year}==>{item.Desc} ==>{item.Name} ==> {item.Pages}");
            }
        }
    }
}
