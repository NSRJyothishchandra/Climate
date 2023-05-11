using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Climate.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _city;
        private string _temperature;
        private string _humidity;
        private string _pressure;
        private string _windSpeed;
        private bool _isLoading;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        public string Pressure
        {
            get => _pressure;
            set
            {
                _pressure = value;
                OnPropertyChanged();
            }
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public async Task GetWeatherAsync()
        {
            IsLoading = true;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={City}&units=metric&appid=ee1b8eaa308cd9f07b324f8b805d6fe1");
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherData>(content);

                Temperature = $"{weatherData.Main.Temp} °C";
                Humidity = $"{weatherData.Main.Humidity} %";
                Pressure = $"{weatherData.Main.Pressure} hPa";
                WindSpeed = $"{weatherData.Wind.Speed} m/s";
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WeatherData
    {
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
    }

    public class MainData
    {
        public float Temp { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindData
    {
        public float Speed { get; set; }
    }
}
