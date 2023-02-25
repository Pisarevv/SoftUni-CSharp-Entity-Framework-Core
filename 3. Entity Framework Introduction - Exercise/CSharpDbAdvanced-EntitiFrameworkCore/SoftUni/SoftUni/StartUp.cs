using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main()
    {
        SoftUniContext context = new SoftUniContext();

        string result = AddNewAddressToEmployee(context);

        Console.WriteLine(result);

    }


    //Problem 3
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var result = context
                     .Employees
                     .OrderBy(e => e.EmployeeId)
                     .Select(e => new
                     {
                         e.FirstName,
                         e.LastName,
                         e.MiddleName,
                         e.JobTitle,
                         e.Salary
                     })
                     .AsNoTracking()
                     .ToArray();

        foreach (var e in result)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
        }  
        
        return sb.ToString();
                                     
    }

    //Problem 4
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var result = context.Employees
                     .AsNoTracking()
                     .Where(e => e.Salary > 50000)
                     .OrderBy(e => e.FirstName)
                     .Select(e => new {
                         e.FirstName,
                         e.Salary
                         })
                     .ToArray();

        foreach ( var e in result)
        {
            sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
        }

        return sb.ToString();
    }

    //Problem 5
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var result = context.Employees
                     .AsNoTracking()
                     .Where(d => d.Department.Name == "Research and Development")
                     .OrderBy(e => e.Salary)
                     .ThenByDescending(e => e.FirstName)
                     .Select(e => new
                     {
                         e.FirstName,
                         e.LastName,
                         DepName = e.Department.Name,
                         e.Salary
                     })
                     .ToArray();

        foreach( var e in result)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepName} - ${e.Salary:F2}");
        }

        return sb.ToString();
    }

    //Problem 6
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        Employee? person = context.Employees
                     .FirstOrDefault(e => e.LastName == "Nakov");

        var newAddress = new Address
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        person.Address = newAddress;

        context.SaveChanges();

        var result = context.Employees
                     .AsNoTracking()
                     .OrderByDescending(e => e.AddressId)
                     .Select(a => a.Address.AddressText)
                     .Take(10)
                     .ToList();

        foreach(var address  in result)
        {
            sb.AppendLine(address);
        }

        return sb.ToString();
    }
}
