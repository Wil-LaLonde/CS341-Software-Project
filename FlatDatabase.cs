using System.Collections.ObjectModel;
using System.Text.Json;
namespace Lab2 {
    public class FlatDatabase : IDatabase {
        private const String FileName = "UserEntries.txt";
        private ObservableCollection<Entry> entries = new ObservableCollection<Entry>();

        public FlatDatabase() {
            entries = GetStoredEntries();
        }

        public ObservableCollection<Entry> GetStoredEntries() {
            ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
            //Check if the file exists.
            if (File.Exists(FileName)) {
                String jsonData = File.ReadAllText(FileName);
                //If there is data, convert to a list of entries.
                if (!String.Empty.Equals(jsonData)) {
                    entries = JsonSerializer.Deserialize<ObservableCollection<Entry>>(jsonData);
                }
            }
            return entries;
        }

        public ObservableCollection<Entry> GetEntries() {
            return entries;
        }

        public int GetCurrentCollectionSize() {
            return entries.Count;
        }

        public bool AddEntry(Entry entry) {
            //Add passed in entry to the list.
            entries.Add(entry);
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        public bool DeleteEntry(int id) {
            //Removing the given id.
            entries.RemoveAt(id);
            //When removing the entry, need to re-index all the other entries (id values)
            foreach (Entry entry in entries) {
                entry.Id++;
            }
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        public bool EditEntry(Entry entry, int id) {
            //Add the given entry at the specific id.
            entries[id] = entry;
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        public bool WriteCurrentDataToFile() {
            bool successFlatDatabaseCall;
            try {
                //Try adding the data to the text file.
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                String jsonData = JsonSerializer.Serialize(entries, options);
                File.WriteAllText(FileName, jsonData);
                successFlatDatabaseCall = true;
            } catch(Exception) {
                //If anything happens, return that it failed.
                successFlatDatabaseCall = false;
            }
            return successFlatDatabaseCall;
        }
    }
}
