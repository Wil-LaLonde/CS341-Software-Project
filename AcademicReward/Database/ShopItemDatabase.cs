using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicReward.ModelClass;
using Npgsql;
/**
 *  Primary Author: Sean Stille
 *  Reviewer: TBD
 *  Desc: The database implementation for items within the shop, adding them, deleting them, etc.
 */
namespace AcademicReward.Database
{
    internal class ShopItemDatabase : AcademicRewardsDatabase, IDatabase
    {
        public DatabaseErrorType AddItem(object obj)
        {
            DatabaseErrorType dbError;
            try
            {
                ShopItem item = (ShopItem)obj;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO shopitems (shopitemid, itemtitle, itemdescription, pointcost, levelrequirment, groupid)" +
                          $"VALUES ({item.Id}, '{item.Title}', '{item.Description}', {item.PointCost}, {item.LevelRequirement}, {1});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex)
            {
                //Username already exists.
                Console.WriteLine("Error while adding item: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            catch (NpgsqlException ex)
            {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while adding item: {0}", ex);
                dbError = DatabaseErrorType.AddProfileDBError;
            }
            return dbError;
        }

        public DatabaseErrorType DeleteItem(object obj)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType LookupFullItem(object obj)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType LookupItem(object obj)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType UpdateItem(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
