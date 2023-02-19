using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02._Villian_Names;
internal static class SqlQueries
{
    public const string GetAllViliansAndCountOfTheirMinions =
        @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
              FROM Villains AS v 
              JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
          GROUP BY v.Id, v.Name 
            HAVING COUNT(mv.VillainId) > 3 
          ORDER BY COUNT(mv.VillainId)";

    public const string GetAllMinionsByVillanId =
        @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

    public const string GetMinionVillageId =
        @"SELECT Id FROM Towns WHERE Name = @townName";

    public const string CreateMinionVillage =
        @"INSERT INTO Towns (Name) VALUES (@townName)";

    public const string GetMinionId =
        @"SELECT Id FROM Minions WHERE Name = @Name";

    public const string CreateMinion =
        @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

    public const string GetVillainId =
        @"SELECT Id FROM Villains WHERE Name = @Name";

    public const string CreateVillain =
        @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

    public const string AddMinionToVillain =
        @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

    public const string UpdateTownsCasingInCountry =
        @"UPDATE Towns
             SET Name = UPPER(Name)
           WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

    public const string GetTownsWithChangedCasing =
        @" SELECT t.Name 
             FROM Towns as t
             JOIN Countries AS c ON c.Id = t.CountryCode
            WHERE c.Name = @countryName";

    public const string GetVillainNameById =
        @" SELECT Name FROM Villains WHERE Id = @villainId";

    public const string DeleteMinionsVillainsRelationByVilId =
        @"DELETE FROM MinionsVillains 
                WHERE VillainId = @villainId";

    public const string DeleteVillainById =
        @"DELETE FROM Villains
           WHERE Id = @villainId";
}
