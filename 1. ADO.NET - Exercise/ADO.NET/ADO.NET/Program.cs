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

        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();


        string[] minionInformation = Console.ReadLine().Split(":",StringSplitOptions.RemoveEmptyEntries);
        string[] villainInformation = Console.ReadLine().Split(": ", StringSplitOptions.RemoveEmptyEntries);

        var result = await AddMinionVillian(sqlConnection, sqlTransaction, minionInformation[1], villainInformation[1]);
        

      

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

    //Problem 04

    static async Task<string> AddMinionVillian
        (SqlConnection sqlConnection, SqlTransaction sqlTransaction, string minionInfo, string villainName)
    {
        string[] minionArgs = minionInfo.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return null;
    }



    static async Task<int> GetMinionTownId (SqlConnection sqlConnection,SqlTransaction sqlTransaction, string townName)
    {

        SqlCommand getMinionVillageId = new SqlCommand(SqlQueries.GetMinionVillageId, sqlConnection, sqlTransaction);
        getMinionVillageId.Parameters.AddWithValue("@townName", townName);

        var townIdObj = await getMinionVillageId.ExecuteScalarAsync();

        if (townIdObj == null)
        {
            await AddMinionVillage(sqlConnection, sqlTransaction, townName);
            townIdObj = await getMinionVillageId.ExecuteScalarAsync();
        }

        var townId = (int)townIdObj;

        return townId;

    }

    static async Task AddMinionVillage(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string townName)
    {
        try
        {
            SqlCommand createMinionVillage = new SqlCommand(SqlQueries.CreateMinionVillage, sqlConnection, sqlTransaction);
            createMinionVillage.Parameters.AddWithValue("@townName", townName);
            await createMinionVillage.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {
            await sqlTransaction.RollbackAsync();
            throw new Exception("Transactipn fail");
        }
    }
}
