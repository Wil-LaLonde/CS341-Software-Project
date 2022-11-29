using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class NotificationLogic : ILogic {
        IDatabase notificationDB;

        /// <summary>
        /// NotificationLogic constructor
        /// </summary>
        public NotificationLogic() { 
            notificationDB = new NotificationDatabase(); 
        }

        /// <summary>
        /// Method used to add a new notification (logic checks)
        /// </summary>
        /// <param name="notification">object notification</param>
        /// <returns></returns>
        public LogicErrorType AddItem(object notification) {
            LogicErrorType logicError;
            Notification notificationToAdd = notification as Notification;
            logicError = AddNotificationCheck(notificationToAdd);
            if(LogicErrorType.NoError == logicError) {
                DatabaseErrorType dbError = notificationDB.AddItem(notification);
                if(DatabaseErrorType.NoError == dbError) {
                    logicError = LogicErrorType.NoError;
                } else {
                    logicError = LogicErrorType.AddNotificationDBError;
                }
            }
            return logicError;
        }

        //Currently not needed
        public LogicErrorType UpdateItem(object notification) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object notification) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType LookupItem(object notification) {
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Helper method used to check the given notification
        /// </summary>
        /// <param name="notification">Notification notification</param>
        /// <returns>LogicErrorType</returns>
        private LogicErrorType AddNotificationCheck(Notification notification) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(notification.Title)) {
                logicError = LogicErrorType.EmptyNotificationTitle;
            } else if(string.IsNullOrEmpty(notification.Description)) {
                logicError = LogicErrorType.EmptyNotificationDescription;
            } else if(CheckNotificationTitleLength(notification.Title)) {
                logicError = LogicErrorType.InvalidNotificationtitleLength;
            } else if(CheckNotificationDescriptionLength(notification.Description)) {
                logicError = LogicErrorType.InvalidNotificationDescriptionLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check a notification's title length
        /// </summary>
        /// <param name="title">string title</param>
        /// <returns>true/false</returns>
        private bool CheckNotificationTitleLength(string title) {
            int titleLength = title.Length;
            return titleLength < Notification.MinTitleLength || titleLength > Notification.MaxTitleLength;
        }

        /// <summary>
        /// Helper method used to check a notification's description length
        /// </summary>
        /// <param name="description">string description</param>
        /// <returns>true/false</returns>
        private bool CheckNotificationDescriptionLength(string description) {
            int descriptionLength = description.Length;
            return descriptionLength < Notification.MinDescriptionLength || descriptionLength > Notification.MaxDescriptionLength;
        }
    }
}
