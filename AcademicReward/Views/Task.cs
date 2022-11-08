using CommunityToolkit.Mvvm.ComponentModel;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicReward.Views {
    public class Task : ObservableObject {

        public bool IsChecked { get; set; }
        public String Title { get; set;}
           //Title should have limited chars
        public String Description { get; set; }
        public int Points { get; set; }
        public String Date { get; set; }
        public Task(bool isChecked, String title, String description, int points, String date) {
            this.IsChecked = isChecked;
            this.Title = title;
            this.Description = description;
            this.Points = points;
            this.Date = date;
        }   
       

    }
}
