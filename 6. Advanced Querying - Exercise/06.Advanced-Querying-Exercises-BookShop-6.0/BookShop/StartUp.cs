using BookShop.Data;

namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //string? input = Console.ReadLine();

            var result = GetBooksByPrice(db);

            Console.WriteLine(result);
        }




        //Problem 2
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction ageRestriction;
            if(Enum.TryParse(command, true, out ageRestriction))
            {
                var books = context.Books.
                            Where(b => b.AgeRestriction == ageRestriction)
                            .OrderBy(b => b.Title)
                            .Select(b => b.Title)                       
                            .ToArray();

                return string.Join(Environment.NewLine, books);
            }

            return null!;
        }

        //Problem  3
        public static string GetGoldenBooks(BookShopContext context)
        {
            var result = context.Books.
                Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title).ToArray();


            //Console.WriteLine(context.Books.
            //    Where(b => b.EditionType.Equals(EditionType.Gold) && b.Copies > 5000)
            //    .OrderBy(b => b.BookId)
            //    .Select(b => b.Title).ToQueryString());

            Console.WriteLine( );

            return string.Join (Environment.NewLine, result);
        }

        //Problem 4 
        public static string GetBooksByPrice(BookShopContext context)
        {
            var result = context.Books.
                         Where(b => b.Price > 40)
                         .Select(b => new
                         {
                             b.Title,
                             b.Price
                         })
                         .OrderByDescending(b => b.Price)
                         .ToArray();

            return string.Join(Environment.NewLine, result.Select(b => $"{b.Title} - ${b.Price:F2}"));
        }
    }

       

    
    

}




