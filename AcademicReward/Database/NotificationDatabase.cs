using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     NotificationDatabase controls notifications from the database
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class NotificationDatabase : AcademicRewardsDatabase, IDatabase {
    /// <summary>
    ///     NotificationDatbase constructor
    /// </summary>
    public NotificationDatabase() { }

    /// <summary>
    ///     Method used to add a notification (database)
    /// </summary>
    /// <param name="notification">object notification</param>
    /// <returns>DatabaseErrorType</returns>
    public DatabaseErrorType AddItem(object notification) {
        DatabaseErrorType dbError;
        Notification notificationToAdd = notification as Notification;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //SQL statement to add the notification to our notifications table
            //Also adding all profileids and the new notificationid to the profilenotification table
            string sql = "INSERT INTO notifications (notificationtitle, notificationdescription, groupid)" +
                $"VALUES ('{notificationToAdd.Title}', '{notificationToAdd.Description}', {notificationToAdd.GroupId});" +
                "INSERT INTO profilenotification " +
                "SELECT profileid, MAX(notificationid) " +
                "FROM profilegroup, notifications " +
                $"WHERE profilegroup.groupid = {notificationToAdd.GroupId} " +
                "GROUP BY profileid;";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Something went wrong adding the task
            Console.WriteLine("Unexpected error while adding notification: {0}", ex);
            dbError = DatabaseErrorType.AddNotificationDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType UpdateItem(object notification) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType DeleteItem(object notification) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType LookupItem(object notification) {
        return DatabaseErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method used to lookup all notifications for a given profile
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType</returns>
    public DatabaseErrorType LookupFullItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileNotifications = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //SQL to lookup notifications for a group
            string sql = "SELECT * " +
                "FROM notifications " +
                $"WHERE notificationid IN (SELECT notificationid FROM profilenotification WHERE profileid = {profileNotifications.ProfileId});";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            //Creating notification objects
            //[0] -> notificationid | [1] -> notificationtitle | [2] -> notificationdescription | [3] -> groupid
            while (reader.Read()) {
                Notification notification =
                    new((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3]);
                profileNotifications.AddNotificationToProfile(notification);
            }

            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Something went wrong adding the task
            Console.WriteLine("Unexpected error while looking up notification: {0}", ex);
            dbError = DatabaseErrorType.LookupAllNotificationsDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public object FindById(int id) {
        return DatabaseErrorType.NotImplemented;
    }
}