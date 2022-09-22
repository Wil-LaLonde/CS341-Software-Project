using System;

namespace Lab2 {
    [Serializable()]
    public class Entry {
        private int id;
        private String clue;
        private String answer;
        private int difficulty;
        private String date;

        //Invalid entry constants.
        public const int InvalidIdEntry = 0;
        public const int InvalidDifficultyEntry = -1;
        public const String InvalidStringEntry = "";

        //Maybe find a way to have the setter methods contain the logic for an Entry???
        public int Id {
            get { return id; }
            set {
                //Try and set the value only if it fits the requirements.
                if(value > InvalidIdEntry) {
                    id = value;
                } else {
                    id = InvalidIdEntry;
                }
            }
        }

        public String Clue {
            get { return clue; }
            set {
                int clueLength = value.Length;
                int minClueLength = 1;
                int maxClueLength = 250;
                if(clueLength >= minClueLength && clueLength <= maxClueLength) {
                    clue = value;
                } else {
                    clue = "test value";
                }
            }
        }

        public String Answer {
            get { return answer; }
            set {
                int answerLength = value.Length;
                int minAnswerLength = 1;
                int maxAnswerLength = 25;
                if(answerLength >= minAnswerLength || answerLength <= maxAnswerLength) {
                    answer = value;
                } else {
                    answer = InvalidStringEntry;
                }
            }
        }

        public int Difficulty {
            get { return difficulty; }
            set {
                int[] validDifficulties = { 0, 1, 2 };
                if(validDifficulties.Contains(value)) {
                    difficulty = value;
                } else {
                    difficulty = InvalidDifficultyEntry;
                }
            }
        }

        public String Date {
            get { return date; }
            set {
                String dateFormat = "mm/dd/yyyy";
                bool validDate = DateTime.TryParseExact(value, dateFormat,
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out _);
                if (validDate) {
                    date = value;
                } else {
                    date = InvalidStringEntry;
                }
            }
        }

        public Entry(int id, String clue, String answer, int difficulty, String date) {
            Id = id;
            Clue = clue;
            Answer = answer;
            Difficulty = difficulty;
            Date = date;
        }
    }
}
