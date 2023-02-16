using Microsoft.Data.SqlClient;
using P02._Villian_Names;
using System.Text;

namespace ADO.NET;

internal class Program
{
    static async Task Main(string[] args)
    {
        await using SqlConnection sqlConnection =
             new SqlConnection(Config.ConnectionString);
        await sqlConnection.OpenAsync();

        string result = await GetAllVilliansWithTheirMiniosAsync(sqlConnection);

        Console.WriteLine(result);

    }

    static async Task<string> GetAllVilliansWithTheirMiniosAsync(SqlConnection sqlConnection)
    {
        StringBuilder sb = new StringBuilder();

        SqlCommand sqlCommand = new SqlCommand(SqlQueries.GetAllViliansAndCountOfTheirMinions, sqlConnection);
        SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
        while (reader.Read())
        {
            string villanName = (string)reader["Name"];
            int minionsCount = (int)reader["MinionsCount"];

            sb.AppendLine($"{villanName} - {minionsCount}");
        }

        return sb.ToString().TrimEnd();
    }
}
