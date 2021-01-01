using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Milestone_Tracker.Models
{
    class Milestone
    {
        // fields
        public string Name { get; set; }
        public int NumOfCheckpoints { get; set; }
        public List<int> CheckpointValues { get; set; }
        public int CurrentCheckpoint { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public int CurrentValue { get; set; }
        public int CurrentStartValue { get; set; }
        public int CurrentEndValue { get; set; }
        public float Progress { get; set; }
        public string ProgressColor { get; set; }
        public string ProgressRatio { get; set; }
        public string StageBGD { get; set; }
        
        public string StageBGL { get; set; }
        private readonly static string[] ArrStageBGL = new string[] { "#2f9813", "#48C4FF", "#b222ff", "#fc6001", "#ffe24d" };
        private readonly static string[] ArrStageBGD = new string[] { "#24af16", "#3faccf", "#9b00ee", "#d35501", "#e6c000" };
        private readonly static string[] ArrProgressColor = new string[] { "#29F619", "#28C4FF", "#9b00ee", "#fc5e01", "#ffff00" };



        // methods
        public Milestone(string name, int[] checkpointValues, int currentValue)
        {
            Name = name;
            CurrentValue = currentValue;
            NumOfCheckpoints = checkpointValues.Count();
            StartValue = 0;
            CheckpointValues = new List<int>() { StartValue };
            CheckpointValues.AddRange(checkpointValues);

            // current stage
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
            StageBGD = ArrStageBGD[CurrentCheckpoint - 1];
            StageBGL = ArrStageBGL[CurrentCheckpoint - 1];
            ProgressColor = ArrProgressColor[CurrentCheckpoint - 1];

            //progress
            Progress = (float)CurrentValue / (float)CurrentEndValue;

            ProgressRatio = CurrentValue.ToString() + " / " + CurrentEndValue.ToString();


        }
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
        public void ChangeCurrentValue(int currentValue)
        {
            CurrentValue = currentValue;
        }

    }
}
