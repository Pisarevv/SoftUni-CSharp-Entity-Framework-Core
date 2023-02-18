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


        string[] minionInformation = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
        string[] villainInformation = Console.ReadLine().Split(": ", StringSplitOptions.RemoveEmptyEntries);

        var result = await AddMinionVillian(sqlConnection, sqlTransaction, minionInformation[1], villainInformation[1]);

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

        SqlCommand getVillainNmae = new SqlCommand(SqlQueries.GetVillainId, sqlConnection);
        getVillainNmae.Parameters.AddWithValue("@Id", villianId);

        object? villainNameObj = await getVillainNmae.ExecuteScalarAsync();

        if (villainNameObj == null)
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
        StringBuilder sb = new StringBuilder();

        string[] minionArgs = minionInfo.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string minionName = minionArgs[0];
        int minionAge = int.Parse(minionArgs[1]);
        string town = minionArgs[2];

        object townIdObj = await GetMinionTownId(sqlConnection,sqlTransaction, town, sb);

        if (townIdObj == null)
        {
            await AddMinionVillage(sqlConnection, sqlTransaction, town);
            townIdObj = await GetMinionTownId(sqlConnection,sqlTransaction, town, sb);
            sb.AppendLine($"Town {town} was added to the database.");
        }

        int townId = (int)townIdObj;

        object minionIdObj = await GetMinionId(sqlConnection,sqlTransaction, minionName);

        if (minionIdObj == null)
        {
            await AddMinion(sqlConnection, sqlTransaction, minionName,minionAge,townId);
            minionIdObj = await GetMinionId(sqlConnection,sqlTransaction, minionName);
        }

        int minionId = (int)minionIdObj;

        object villainIdObj = await GetVillainId(sqlConnection,sqlTransaction, villainName);

        if (villainIdObj == null)
        {
            await AddVilain(sqlConnection, sqlTransaction, villainName);
            villainIdObj = await GetVillainId(sqlConnection,sqlTransaction, villainName);
            sb.AppendLine($"Villain {villainName} was added to the database.");
        }

        int vilainId = (int)villainIdObj;

        await AddMinionToVillain(sqlConnection, sqlTransaction, minionId, vilainId);
        sb.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");

        sqlTransaction.CommitAsync();


        return sb.ToString();
    }

    static async Task<object> GetMinionId(SqlConnection sqlConnection,SqlTransaction sqlTransaction, string minionName)
    {
        SqlCommand getMinionId = new SqlCommand(SqlQueries.GetMinionId, sqlConnection, sqlTransaction);
        getMinionId.Parameters.AddWithValue("@name", minionName);

        object minionIdObject = await getMinionId.ExecuteScalarAsync();

        return minionIdObject;

    }

    static async Task<object> GetVillainId(SqlConnection sqlConnection,SqlTransaction sqlTransaction, string minionName)
    {
        SqlCommand getVillainId = new SqlCommand(SqlQueries.GetVillainId, sqlConnection, sqlTransaction);
        getVillainId.Parameters.AddWithValue("@name", minionName);

        object villainIdObject = await getVillainId.ExecuteScalarAsync();

        return villainIdObject;

    }

    static async Task<object> GetMinionTownId(SqlConnection sqlConnection,SqlTransaction sqlTransaction, string townName, StringBuilder sb)
    {

        SqlCommand getMinionVillageId = new SqlCommand(SqlQueries.GetMinionVillageId, sqlConnection, sqlTransaction);
        getMinionVillageId.Parameters.AddWithValue("@townName", townName);

        object townIdObj = await getMinionVillageId.ExecuteScalarAsync();

        return townIdObj;

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

    static async Task AddMinion(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string minionName, int minionAge, int townId)
    {
        try
        {
            SqlCommand createMinion = new SqlCommand(SqlQueries.CreateMinion, sqlConnection, sqlTransaction);
            createMinion.Parameters.AddWithValue("@name", minionName);
            createMinion.Parameters.AddWithValue("@age", minionAge);
            createMinion.Parameters.AddWithValue("@townId", townId);
            await createMinion.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {

            await sqlTransaction.RollbackAsync();
            throw new Exception("Transactipn fail");
        }
    }

    static async Task AddVilain(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string villainName)
    {
        try
        {
            SqlCommand createVilllain = new SqlCommand(SqlQueries.CreateVillain, sqlConnection, sqlTransaction);
            createVilllain.Parameters.AddWithValue("@villainName", villainName);
            await createVilllain.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {

            await sqlTransaction.RollbackAsync();
            throw new Exception("Transactipn fail");
        }
    }

    static async Task AddMinionToVillain(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int minionId, int villainId)
    {
        try
        {
            SqlCommand sqlCommand = new SqlCommand(SqlQueries.AddMinionToVillain, sqlConnection, sqlTransaction);
            sqlCommand.Parameters.AddWithValue("@villainId", villainId);
            sqlCommand.Parameters.AddWithValue(@"minionId", minionId);
            await sqlCommand.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {
            await sqlTransaction.RollbackAsync();
            throw new Exception("Transactipn fail");
        }
    }
}
