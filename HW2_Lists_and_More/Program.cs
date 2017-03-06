using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Lists_and_More
{
    // =====================================================================================
    //
    // Create a console application and use the following snippet of code.
    //
    // Create a Models directory and copy the User class from the Data Binding topic.
    // var users = new List();
    //
    //    users.Add(new Models.User { Name = "Dave", Password="hello" });
    //    users.Add(new Models.User { Name = "Steve", Password="steve" });
    //    users.Add(new Models.User { Name = "Lisa", Password="hello" });
    //
    // Use System.Linq for all the requirements.
    // IE. Don't use a for/foreach loop.
    //
    // (1.) Display to the console, all the passwords that are "hello". Hint: Where
    //
    // (2.) Delete any passwords that are the lower-cased version of their Name.
    // Do not just look for "steve". 
    // It has to be data-driven. Hint: Remove
    //
    // (3.) Delete the first User that has the password "hello". Hint: First or FirstOrDefault
    //
    // (4.) Display to the console the remaining users with their Name and Password.
    //
    //     Steph - only Lisa should be in the list
    //
    // =====================================================================================

    class Program
    {
        static void Main(string[] args)
        {

            List<Models.User> users = new List<Models.User>();

            List<Models.User> tempUsers = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa", Password = "hello" });
            users.Add(new Models.User { Name = "Sally", Password = "sally" });

            // (1.) Display to the console, all the passwords that are "hello". Hint: Where
            Console.WriteLine("\nusers");
            users.Display();
            Console.WriteLine();

            Console.WriteLine("Display to the console, all the passwords that are hello");
            users.Where(u => u.Password == "hello")
                 .Display();

            // (2.) Delete any passwords that are the lower-cased version of their Name.
            // Do not just look for "steve". 
            // It has to be data-driven. Hint: Remove

            // Remove() removes the first one from the list  !!

 
            // removes the users from the list 
            // ... with Name.ToLower() == Password
            users.RemoveAll(u => u.Name.ToLower() == u.Password);

            Console.WriteLine("\nremoved all users from list with Password equal to Name lower case");
            users.Display();

            // (3.) Delete the first User that has the password "hello". Hint: First or FirstOrDefault

            Console.WriteLine("\nDelete the first User that has the password hello");

            var match = users.FirstOrDefault(u => u.Password == "hello");

            if (match != null)
            {
                users.Remove(match);
            }

            // (4.) Display to the console the remaining users with their Name and Password.
            Console.WriteLine("\nDisplay to the console the remaining users with their Name and Password.");
            users.Display();

        }

    }

    static class Extensions
    {
        // extension method that displays all elements separated by spaces
        public static void Display<T>(this IEnumerable<T> data)
        {
            Console.WriteLine(string.Join("\n", data));
        }
    }
}
