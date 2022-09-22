using System;

namespace Lab2 {
    public class BusinessLogic : IBusinessLogic {
        private IDatabase flatDatabase;

        public BusinessLogic() {
            flatDatabase = new FlatDatabase();
        }

        public void AddEntry(String clue, String answer, String difficulty, String date) {
            //Make a call to our "database" after parsing data.
            //Need to find the new Id value
            int newId = flatDatabase.GetCurrentCollectionSize() + 1;
            //Need to parse the difficulty.
            int newDifficulty = int.TryParse(difficulty, out newDifficulty) ? newDifficulty : Entry.InvalidDifficultyEntry;
            //Try and make a new Entry
            Entry addEntry = new Entry(newId, clue, answer, newDifficulty, date);
            //Check to see if the values were properly added???
            bool test = true;
        }

        public void DeleteEntry() {
            //Make a call to our "database" after parsing data.
        }

        public void EditEntry() {
            //Make a call to our "database" after parsing data.

        }
    }
}
