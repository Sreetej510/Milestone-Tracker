using Milestone_Tracker.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Milestone_Tracker.Models
{
    public class Milestone : INotifyPropertyChanged
    {

        // dummy fields
        public string D_Name { get; set; }
        public int D_NumOfCheckpoints { get; set; }
        public List<int> D_CheckpointValues { get; set; }
        public int D_CurrentCheckpoint { get; set; }
        public int D_StartValue { get; set; }
        public int D_EndValue { get; set; }
        public int D_CurrentValue { get; set; }
        public int D_CurrentStartValue { get; set; }
        public int D_CurrentEndValue { get; set; }
        public float D_Progress { get; set; }
        public string D_StageBGL { get; set; }


        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // fields

        public int GroupIndex { get; set; }

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
        public int StartValue
        {
            get => D_StartValue;
            set
            {
                D_StartValue = value;
                OnPropertyChanged();
            }
        }
        public int EndValue
        {
            get => D_EndValue;
            set
            {
                D_EndValue = value;
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
        public Milestone(int groupIndex,string name, int[] checkpointValues, int currentValue)
        {
            GroupIndex = groupIndex;
            Name = name;
            CurrentValue = currentValue;
            NumOfCheckpoints = checkpointValues.Count();
            StartValue = 0;
            CheckpointValues = new List<int>() { StartValue };
            CheckpointValues.AddRange(checkpointValues);
            NeedStageChange = false;

            // current stage
            ChangeStage();


        }


        // Methods
        public void ChangeInfo(string name, byte numOfCheckpoints)
        {
            Name = name;
            NumOfCheckpoints = numOfCheckpoints;
        }
        public void ChangeCheckpointInfo(int[] checkpointValues, int startValue, int endValue, int currentValue)
        {
            CheckpointValues.Clear();
            CheckpointValues.AddRange(checkpointValues);
            StartValue = startValue;
            EndValue = endValue;
            CurrentValue = currentValue;
        }
        public void ChangeCurrentValue()
        {

            Progress = (float)CurrentValue / (float)CurrentEndValue;
            if (Progress == 1)
            {
                NeedStageChange = true;
                ChangeStage();                
            }

            var json = new ReadAndWriteJson("Fortnite","List_Data", "advanced");

            var jObject = json.ReadJson();

            var onGoing = (JArray)jObject["onGoing"];

            var group = (JArray)onGoing[GroupIndex]["contents"];

            foreach (var item in group)
            {
                if (item["name"].ToString() == Name)
                {
                    item["currentValue"] = CurrentValue;
                }
            }

            json.WriteJson(jObject);
        }

        public void ChangeStage()
        {
            for (var i = 0; i < NumOfCheckpoints; i++)
            {
                if (CheckpointValues[i] <= CurrentValue && CheckpointValues[i + 1] > CurrentValue)
                {
                    CurrentCheckpoint = i + 1;
                    CurrentEndValue = CheckpointValues[i + 1];
                    CurrentStartValue = CheckpointValues[i];
                }
            }


            //color fix
            StageBGL = ArrStageBGL[CurrentCheckpoint - 1];

            //progress
            Progress = (float)CurrentValue / (float)CurrentEndValue;            
        }

    }
}
