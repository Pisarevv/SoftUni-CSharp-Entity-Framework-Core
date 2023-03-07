using BookShop.Data;

namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            string input = Console.ReadLine();

            var result = GetBooksByAgeRestriction(db, input);

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
    }

    
    

}




