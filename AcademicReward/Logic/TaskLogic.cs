using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    public class TaskLogic : ILogic {
        private IDatabase taskDB;

        /// <summary>
        /// TaskLogic constructor
        /// </summary>
        public TaskLogic() { 
            //taskDB = new TaskDatabse();
        }

        public LogicErrorType AddItem(object task) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType UpdateItem(object task) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object task) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType LookupItem(object task) {
            return LogicErrorType.NoError;
        }
    }
}
