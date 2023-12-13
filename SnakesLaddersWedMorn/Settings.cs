

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakesLaddersWedMorn 
{
    public class Settings : INotifyPropertyChanged
    {
        private string grid_colour1;
        private string grid_colour2;

        public string GRID_COLOUR1
        {
            get
            {
                return grid_colour1;
            }
            set
            {
                if(value != grid_colour1) {
                    grid_colour1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
