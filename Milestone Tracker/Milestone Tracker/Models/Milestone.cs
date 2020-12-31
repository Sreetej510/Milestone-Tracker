using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milestone_Tracker.Models
{
    class Milestone
    {
        // fields
        public string Name { get; set; }
        public int NumOfCheckpoints { get; set; }
        public List<int> CheckpointValues { get; set; }
        public int CurrentCheckpoint { get; set; }
        public static double StageBGOffset { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public int CurrentValue { get; set; }
        public int CurrentStartValue { get; set; }
        public int CurrentEndValue { get; set; }
        public float Progress { get; set; }
        public string ProgressRatio { get; set; }
        public string StageBGD { get; set; }
        public string StageBGL { get; set; }
        private readonly static string[] ArrStageBGD = new string[] { "#787978","#439C05", "#38AFD8", "#B078E2", "#E28126" };
        private readonly static string[] ArrStageBGL = new string[] { "#585958", "#43BC05", "#38BFF8", "#D078F2", "#F29126" };

        // methods
        public Milestone(string name, int[] checkpointValues , int currentValue)
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
                if (CheckpointValues[i] <= CurrentValue && CheckpointValues[i+1] > CurrentValue)
                {
                    CurrentCheckpoint = i+1;
                    CurrentEndValue = CheckpointValues[i + 1];
                    CurrentStartValue = CheckpointValues[i];
                }
            }

            //color fix
            StageBGD = ArrStageBGD[CurrentCheckpoint-1];
            StageBGL = ArrStageBGL[CurrentCheckpoint-1];

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
