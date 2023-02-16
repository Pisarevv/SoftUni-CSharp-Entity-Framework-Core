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

        int vilainId = int.Parse(Console.ReadLine());

        string result = await GetVillianWithAllMinionsAsync(sqlConnection, vilainId);

        Console.WriteLine(result);

    }

    //Problem 02
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

    //Problem 03
    static async Task<string> GetVillianWithAllMinionsAsync(SqlConnection sqlConnection, int villianId) 
    {
        StringBuilder sb = new StringBuilder();

        SqlCommand getVillainNmae = new SqlCommand(SqlQueries.GetVillainById, sqlConnection);
        getVillainNmae.Parameters.AddWithValue("@Id", villianId);

        object ?villainNameObj = await getVillainNmae.ExecuteScalarAsync();

        if(villainNameObj == null)
        {
            return $"No villain with ID ${villianId} exists in the database.";
        }

        string vilainName = (string)villainNameObj;
        sb.AppendLine($"{vilainName}");

        SqlCommand getAllMinionsCmd = new SqlCommand(SqlQueries.GetAllMinionsByVillanId, sqlConnection);
        getAllMinionsCmd.Parameters.AddWithValue("@id", villianId);
        SqlDataReader minionsReader = await getAllMinionsCmd.ExecuteReaderAsync();
        

        if (!minionsReader.HasRows)
        {
            sb.AppendLine("(no minions)");
        }
        else
        {
            while (minionsReader.Read()) 
            {
                long rowNum = (long)minionsReader["RowNum"];
                string minionName = (string)minionsReader["Name"];
                int minionAge = (int)minionsReader["Age"];

                sb.AppendLine($"{rowNum}. {minionName} {minionAge}");
            }
        }

        return sb.ToString();
          
    }
}
