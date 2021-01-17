using Milestone_Tracker.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Milestone_Tracker.Models
{
    public class Milestone : INotifyPropertyChanged
    {
        // dummy fields
        public string D_Name { get; set; }

        public int D_NumOfCheckpoints { get; set; }
        public List<int> D_CheckpointValues { get; set; }
        public int D_CurrentCheckpoint { get; set; }
        public int D_CurrentValue { get; set; }
        public int D_CurrentStartValue { get; set; }
        public int D_CurrentEndValue { get; set; }
        public float D_Progress { get; set; }
        public string D_StageBGL { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // fields

        private readonly string ListName;

        public string Category { get; set; }

        public string Name
        {
            get => D_Name;
            set
            {
                D_Name = value;
                OnPropertyChanged();
            }
        }

        public int NumOfCheckpoints
        {
            get => D_NumOfCheckpoints;
            set
            {
                D_NumOfCheckpoints = value;
                OnPropertyChanged();
            }
        }

        public List<int> CheckpointValues
        {
            get => D_CheckpointValues;
            set
            {
                D_CheckpointValues = value;
                OnPropertyChanged();
            }
        }

        public int CurrentCheckpoint
        {
            get => D_CurrentCheckpoint;
            set
            {
                D_CurrentCheckpoint = value;
                OnPropertyChanged();
            }
        }

        public int CurrentValue
        {
            get => D_CurrentValue;
            set
            {
                D_CurrentValue = value;
                OnPropertyChanged();
            }
        }

        public int CurrentStartValue
        {
            get => D_CurrentStartValue;
            set
            {
                D_CurrentStartValue = value;
                OnPropertyChanged();
            }
        }

        public int CurrentEndValue
        {
            get => D_CurrentEndValue;
            set
            {
                D_CurrentEndValue = value;
                OnPropertyChanged();
            }
        }

        public float Progress
        {
            get => D_Progress;
            set
            {
                D_Progress = value;
                OnPropertyChanged();
            }
        }

        public string StageBGL
        {
            get => D_StageBGL;
            set
            {
                D_StageBGL = value;
                OnPropertyChanged();
            }
        }

        public bool NeedStageChange { get; set; }

        private readonly static string[] ArrStageBGL = new string[] { "#787878", "#2f9813", "#48C4FF", "#b222ff", "#fc6001", "#ffe24d" };

        public event PropertyChangedEventHandler PropertyChanged;

        // constructor
        public Milestone(string listName, string category, string name, int[] checkpointValues, int currentValue)
        {
            ListName = listName;
            Category = category;
            Name = name;
            CurrentValue = currentValue;

            NumOfCheckpoints = checkpointValues.Count();
            var tempList = new List<int>() { 0 };
            tempList.AddRange(checkpointValues);
            List<int> distinct = tempList.Distinct().ToList();
            CheckpointValues = new List<int> { };
            CheckpointValues.AddRange(distinct);
            NeedStageChange = false;

            // current stage
            Task.Run(() => ChangeStage());
        }

        // Methods
        public void ChangeCurrentValue()
        {
            Progress = (float)CurrentValue / (float)CurrentEndValue;
            if (Progress == 1)
            {
                NeedStageChange = true;
                ChangeStage();
            }

            var json = new ReadAndWriteJson(ListName, "List_Data", "advanced");

            var jObject = json.ReadJson();

            var onGoing = (JArray)jObject["onGoing"];
            var allCat = (JArray)jObject["allCategories"];
            var allCatArray = allCat.ToObject<List<string>>();

            var groupIndex = allCatArray.IndexOf(Category.Trim().ToUpper());

            var group = (JArray)onGoing[groupIndex]["contents"];

            foreach (var item in group)
            {
                if (item["name"].ToString() == Name)
                {
                    item["currentValue"] = CurrentValue;
                }
            }

            json.WriteJson(jObject);
        }

        public void ChageInfo(string name, int[] checkpoints, int currentValue)
        {
            Name = name;

            NumOfCheckpoints = checkpoints.Count() - 1;
            var tempList = new List<int>() { 0 };
            tempList.AddRange(checkpoints);
            List<int> distinct = tempList.Distinct().ToList();
            CheckpointValues.Clear();
            CheckpointValues.AddRange(distinct);

            CurrentValue = currentValue;

            ChangeStage();
        }

        public void ChangeStage()
        {
            for (var i = 0; i < NumOfCheckpoints; i++)
            {
                if (CheckpointValues[i] <= CurrentValue && CheckpointValues[i + 1] >= CurrentValue)
                {
                    CurrentCheckpoint = i + 1;
                    CurrentEndValue = CheckpointValues[i + 1];
                    CurrentStartValue = CheckpointValues[i];
                }
            }

            //color fix
            StageBGL = ArrStageBGL[(CurrentCheckpoint - 1) % 5];

            //progress
            Progress = (float)CurrentValue / (float)CurrentEndValue;
        }
    }
}