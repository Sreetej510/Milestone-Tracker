using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    public class CurrentValueModalViewModel : BindableObject
    {
        public Milestone Item { get; set; }
        private int _sliderValue;
        private string _buttonText;
        private int _sliderEndValue;
        private int _sliderStartValue;

        public int SliderStartValue
        {
            get { return _sliderStartValue; }
            set
            {
                _sliderStartValue = value;
                OnPropertyChanged();
            }
        }

        public int SliderEndValue
        {
            get { return _sliderEndValue; }
            set
            {
                _sliderEndValue = value;
                OnPropertyChanged();
            }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public int SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                OnPropertyChanged();
            }
        }

        private float _ringProgress;
        public float RingProgress
        {
            get
            {
                return _ringProgress;
            }
            set
            {
                _ringProgress = value;
                OnPropertyChanged();
            }
        }

        public Command CloseCurrentValueModal { get; set; }
        public Command UpdateTapped { get; set; }
        private Grid ModalGrid;
        private Grid ModalContainer;
        public int ModalPageCount { get; set; }

        // Constructor
        public CurrentValueModalViewModel(Milestone item, Grid modalGrid, Grid modalContainer, int count)
        {
            Item = item;
            ModalPageCount = count;
            ModalGrid = modalGrid;
            ModalContainer = modalContainer;

            ButtonText = "Update";
            SliderValue = (int)Item.CurrentValue;
            SliderEndValue = (int)Item.CurrentEndValue;
            SliderStartValue = (int)Item.CurrentStartValue;
            RingProgress = (float)SliderValue / Item.CurrentEndValue;

            CloseCurrentValueModal = new Command(eventCloseCurrentValueModal);
            UpdateTapped = new Command(eventUpdateButtonAsync);

        }

        // events
        public async void eventCloseCurrentValueModal()
        {
            await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
            new NavigationService().PopToListPage(ModalPageCount);
        }
        private async void eventUpdateButtonAsync()
        {

            Item.CurrentValue = (int)SliderValue;
            Item.Progress = (float)RingProgress;
            if (Item.NumOfCheckpoints != Item.D_CurrentCheckpoint && RingProgress == 1)
            {
                ModalPageCount++;
                await ModalContainer.TranslateTo(0, 1000, 300);
                await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
                Item.ChangeCurrentValue();
                await new NavigationService().PushModalPage(new CurrentValueModal(Item, ModalPageCount), false);
            }
            Item.ChangeCurrentValue();
        }
    }
}