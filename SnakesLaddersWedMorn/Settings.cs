

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

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

        public string GRID_COLOUR2
        {
            get
            {
                return grid_colour2;
            }
            set
            {
                if (value != grid_colour2) {
                    grid_colour2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public Settings() {
            //Set Default Values
            GRID_COLOUR1 = "#2B0B98";
            GRID_COLOUR2 = "#2B0B98";
        }

        public bool WritetoJson() {
            string jsonalice = JsonSerializer.Serialize(this);
            string filename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "settings.json");
            try {
                using (StreamWriter writer = new StreamWriter(filename)) {
                    writer.Write(jsonalice);
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
