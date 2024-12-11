using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welness_Care.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Welness_Care
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetLocationPage : ContentPage
    {
        string mLat = "", mLong = "";
        string SId;

        public SetLocationPage(string uId)
        {
            InitializeComponent();
            SId = uId;

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<MSPData>();
            var nms = con.Query<MSPData>("Select * from MSPData where MSPId = ?", SId);

            foreach (var s in nms)
            {
                if(s.MSPLatitude == "")
                {

                }
                else
                {
                    
                    
                    Pin pin = new Pin()
                    {
                        Label = "Location",
                        Position = new Position(Convert.ToDouble(s.MSPLatitude),Convert.ToDouble(s.MSPLongitude)),
                    };

                    map.Pins.Add(pin);
                   
                }
            }
        }
        protected override async void OnAppearing()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(1)));
            }
            catch (Exception e)
            {

            }
        }

        private void map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            map.Pins.Clear();
            Pin pin = new Pin()
            {
                Label = "Location",
                Position = new Position(e.Position.Latitude, e.Position.Longitude),
            };

            map.Pins.Add(pin);

            mLat = e.Position.Latitude.ToString();
            mLong = e.Position.Longitude.ToString();
        }

        private void Location_Clicked(object sender, EventArgs e)
        {
            bool a2;
            string error = "Following errors occured:\n";
            
            if (mLat != null && mLat != "")
            {
                a2 = true;
            }
            else
            {
                a2 = false;
                error += "Location is not Selected\n";
            }


            if (a2 == true)
            {
                SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                con.CreateTable<MSPData>();
                con.Query<MSPData>("Update MSPData set MSPLatitude = ?,MSPLongitude = ? where MSPId= '" + SId + "'", mLat, mLong);


                DisplayAlert("Successfull", "Location Set Successfully", "Ok");

                Navigation.PushAsync(new MSPMasterPage(SId));
            }

            else
            {
                DisplayAlert("Error", error, "Ok");
            }
        }

        


    }
}