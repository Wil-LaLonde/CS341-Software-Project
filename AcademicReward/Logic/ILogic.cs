using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// ILogic interface
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public interface  ILogic {
        public LogicErrorType AddItem(object obj);
        public LogicErrorType AddItemWithArgs(object[] obj);
        public LogicErrorType UpdateItem(object obj);
        public LogicErrorType DeleteItem(object obj);
        public LogicErrorType LookupItem(object obj);
    }
}
