using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main()
    {
        SoftUniContext context = new SoftUniContext();

        string result = GetEmployeesByFirstNameStartingWithSa(context);

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

        person!.Address = newAddress;

        context.SaveChanges();

        var result = context.Employees
                     .AsNoTracking()
                     .OrderByDescending(e => e.AddressId)
                     .Select(a => a.Address!.AddressText)
                     .Take(10)
                     .ToList();

        foreach(var address  in result)
        {
            sb.AppendLine(address);
        }

        return sb.ToString();
    }

    //Problem 7
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var empWithProjects = context.Employees
                     .Take(10)
                     //.Where(p => p.EmployeesProjects.
                     //            Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                     .AsNoTracking()
                     .Select(e => new
                     {
                         e.FirstName,
                         e.LastName,
                         e.Manager,
                         Projects = e.EmployeesProjects
                                    .Where(p => p.Project!.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003)
                                    .Select(p => new
                                    {
                                        Name = p.Project!.Name,
                                        StartDate = p.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                                        EndDate = p.Project.EndDate != null 
                                        ? p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                        : "not finished"

                                    })
                                    
                                    .ToList()
                                            
                     })
                     .ToList();

        foreach(var employee in empWithProjects)
        {
            sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager!.FirstName} {employee.Manager.LastName}");

            foreach (var p in employee.Projects)
            {
                sb.AppendLine($"--{p.Name} - {p.StartDate} - {p.EndDate}");
            }
                
        }

        return sb.ToString().TrimEnd();
    }

    //Problem 8
    public static string GetAddressesByTown(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var addresses = context.Addresses
                        .AsNoTracking()
                        .OrderByDescending(a => a.Employees.Count())
                        .ThenBy(a => a.Town!.Name)
                        .ThenBy(a => a.AddressText)
                        .Take(10)
                        .Select(a => new
                        {
                            a.AddressText,
                            TownName = a.Town!.Name,
                            EmployeeCount = a.Employees.Count()
                        })
                        .ToList();

        foreach (var a in addresses)
        {
            sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
        }

        return sb.ToString().TrimEnd();
    }

    //Problem 9
    public static string GetEmployee147(SoftUniContext context)
    {
        var sb = new StringBuilder();
        Employee ?employee147 = context.Employees
                          .AsNoTracking()
                          .FirstOrDefault(e => e.EmployeeId == 147);
        var projects = context.EmployeesProjects
                       .AsNoTracking()
                       .Where(e => e.EmployeeId == 147)
                       .OrderBy(p => p.Project!.Name)
                       .Select(p => p.Project!.Name)
                       .ToList();
                       
                       

        if(employee147 != null)
        {
            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
            foreach(var e in projects)
            {
                sb.AppendLine($"{e}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    //Problem 10
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var departments = context.Departments
                          .AsNoTracking()
                          .Where(d => d.Employees.Count() > 5)
                          .OrderBy(d => d.Employees.Count())
                          .ThenBy(d => d.Name)
                          .Select(x => new
                          {
                              DepName = x.Name,
                              ManagerFirstName = x.Manager.FirstName,
                              ManagerLastName = x.Manager.LastName,
                              Employees = x.Employees
                                          .OrderBy(e => e.FirstName)
                                          .ThenBy(e => e.LastName)
                                          .Select(e => new
                                          {
                                              e.FirstName,
                                              e.LastName,
                                              e.JobTitle
                                          })
                                          .ToList()
                                         
                          });

        foreach(var d in departments)
        {
            sb.AppendLine($"{d.DepName} - {d.ManagerFirstName} {d.ManagerLastName}");
            foreach(var e in d.Employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    //Problem 11
    public static string GetLatestProjects(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var projects = context.Projects
                      .AsNoTracking()
                      .OrderByDescending(p => p.StartDate)
                      .Take(10)
                      .OrderBy(p => p.Name)
                      .Select(p => new
                      {
                          p.Name,
                          p.Description,
                          StartDate  = p.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)
                      });

        foreach(var p in projects)
        {
            sb.AppendLine(p.Name +
                         Environment.NewLine +
                         p.Description +
                         Environment.NewLine +
                         p.StartDate);
        }       
        
        return sb.ToString().TrimEnd();
    }

    //Problem 12
    public static string IncreaseSalaries(SoftUniContext context)
    {
        string[] departemntsToIncSalary = new string[4]
        {
          "Engineering",
          "Tool Design",
          "Marketing",
          "Information Services"
        };

        var sb = new StringBuilder();

        var employees = context.Employees
                     .Where(e => departemntsToIncSalary.Contains(e.Department.Name))
                     .OrderBy(e => e.FirstName)
                     .ThenBy(e => e.LastName)
                     .Select(e => e)
                     .ToList();

       foreach(var e in employees)
        {
            e.Salary = e.Salary * 1.12m;
            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
        }

       //context.SaveChanges();

        return sb.ToString();

    }

    //Problem 13
    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
                        .AsNoTracking()
                        .Where(e => e.FirstName.ToLower().StartsWith("sa"))
                        .OrderBy(e => e.FirstName)
                        .ThenBy (e => e.LastName)
                        .Select(e => new
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle,
                            e.Salary
                        })
                        .ToArray();

        foreach(var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
        }

        return sb.ToString();
    }
}
