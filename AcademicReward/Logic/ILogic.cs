using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// ILogic interface
    /// </summary>
    public interface  ILogic {
        public LogicErrorType AddItem(object obj);
        public LogicErrorType UpdateItem(object obj);
        public LogicErrorType DeleteItem(object obj);
        public LogicErrorType LookupItem(object obj);
    }
}
