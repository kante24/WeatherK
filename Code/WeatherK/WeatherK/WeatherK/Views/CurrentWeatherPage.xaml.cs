using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherK.Helper;
using WeatherK.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherK.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentWeatherPage : ContentPage
    {
        public CurrentWeatherPage()
        {
            InitializeComponent();

            //Call method getWeatherInfo
            GetWeatherInfo();

            //Call method getWeatherInfo
            GetForecast();

            //For Error
            //Put  android:usesCleartextTraffic="true" in Android - Propreties - AndroidManifest.xaml
        }

        //Location
        private string Location = "Canada"; 

        private async void GetWeatherInfo()
        {
            //Url to get weather for api on open wheater man with location and apiKey
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Location}&appid=8bf3b9bb12be7aa8ec2a3fba7548daac";

            //Put result from url into result
            var result = await ApiCaller.Get(url);

            //If successful bind result to view
            if(result.Successful)
            {
                try
                {
                    //Convert and put the json result from result's response
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);

                    //Display informations from weatherInfo into CurrentWeahterPage by using names
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{ weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    //Local date and time
                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();

                }
                catch(Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message.ToString(), "Ok");
                }
            }

            //Else display an error
            else
            {
                await DisplayAlert("Weather Info", "No weather Information found", "Ok");
            }
        }

        private async void GetForecast()
        {
            //Url to get weather for api on open wheater man with location and apiKey
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={Location}&appid=8bf3b9bb12be7aa8ec2a3fba7548daac";

            //Put result from url into result
            var result = await ApiCaller.Get(url);

            //If successful bind result to view
            if (result.Successful)
            {
                try
                {
                    //Convert and put the json result from result's response
                    var forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();   
                    
                    //For each list in forecastInfo, add list in my variable allList if date > Today
                    foreach(var list in forecastInfo.list)
                    {
                        var date = DateTime.Parse(list.dt_txt);

                        if(date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                        {
                            allList.Add(list);
                        }
                    }

                    //Day One Infos
                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    //Day Two Infos
                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    //Day Three Infos
                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    //Day Four Infos
                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message.ToString(), "Ok");
                }
            }

            //Else display an error
            else
            {
                await DisplayAlert("Weather Info", "No weather Information found", "Ok");
            }

        }
    }
}