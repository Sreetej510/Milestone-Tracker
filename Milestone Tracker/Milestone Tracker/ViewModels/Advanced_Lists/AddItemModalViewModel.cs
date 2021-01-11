using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    internal class AddItemModalViewModel : BindableObject
    {
        public Grid ModalGrid { get; }

        private readonly PopulateList populateList;

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoneEnable));
            }
        }

        private string _category;

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoneEnable));
            }
        }

        private string _checkpointString;

        public string CheckpointString
        {
            get { return _checkpointString; }
            set
            {
                _checkpointString = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoneEnable));
            }
        }

        public bool DoneEnable
        {
            get
            {
                if (CheckpointString != "" && Name != "" && Category != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool closeEnabled;
        public Command CloseAddItemModal { get; }
        public Command AddItemToList { get; }
        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value;
                OnPropertyChanged();
            }
        }

        public AddItemModalViewModel(Grid modalGrid, PopulateList list)
        {
            ModalGrid = modalGrid;
            populateList = list;
            closeEnabled = true;
            CloseAddItemModal = new Command(EventCloseThisModal);
            AddItemToList = new Command(EventAddItemToList);
        }

        //event
        private async void EventAddItemToList()
        {
            try
            {
                populateList.AddItemToPopulateList(Category, Name, CheckpointString, 0);
                EventCloseThisModal();
            }
            catch (System.Exception)
            {
                ErrorMsg = "Checkpoints should only contain numbers and '-' ";
            }
        }

        public async void EventCloseThisModal()
        {
            if (closeEnabled)
            {
                closeEnabled = false;
                await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
                new NavigationService().PopModalPage(false);
            }
        }
    }
}