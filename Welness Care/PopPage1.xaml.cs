using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using SQLite;
using Welness_Care.Model;
using Xamarin.Essentials;

namespace Welness_Care
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopPage1 : PopupPage
    {
        string MSPId, UserId, plong = "", plat = "", ServiceName;
        double distance = 0, distanceCharges = 0, TotalCharges = 0;
        int serviceCharges = 0;
        string UserLat, Userlong;

        private void BookingButton_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            con.Query<Users>("Update Users SET OrderMSPId = ? Where UserId = ?", MSPId, UserId);
            con.Close();

            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<MSPData>();
            con1.Query<MSPData>("Update MSPData SET OrderUserId = ?,ActiveStatus = 'Pending' Where MSPId = ?", UserId, MSPId);
            con1.Close();

            Orders Order = new Orders()
            {
                CustomerId = UserId,
                MSPId = MSPId,
                ServiceName = ServiceName,
                ServiceCharges = serviceCharges.ToString(),
                DistanceCharges = distanceCharges.ToString(),
                TotalCharges = TotalCharges.ToString(),
                Date = DateTime.Now.Date.ToShortDateString(),
                Status = "Pending",
                MSPLatitude = plat,
                MSPLongitude = plong,
                UserLatitude = UserLat,
                UserLongitude = Userlong,
            };
            SQLiteConnection conn = new SQLiteConnection(App.Databaselocation);
            conn.CreateTable<Orders>();
            conn.Insert(Order);
            conn.Close();

            PopupNavigation.Instance.PopAllAsync();
            Navigation.PushAsync(new UserMasterPage(UserId));
        }

        public PopPage1(string MSId, string SId, string SN)
        {
            InitializeComponent();
            MSPId = MSId;
            UserId = SId;
            ServiceName = SN;

            if(ServiceName == "BloodPressureService") { ServiceNameLabel.Text = "Blood Pressure"; ServiceImage.Source = "BloodPressure"; }
            else if(ServiceName == "InjectionsService") { ServiceNameLabel.Text = "Injections"; ServiceImage.Source = "Injection"; }
            else if (ServiceName == "BandagesService") { ServiceNameLabel.Text = "Bandages"; ServiceImage.Source = "Bandages"; }
            else if (ServiceName == "InsulinService") { ServiceNameLabel.Text = "Insulin"; ServiceImage.Source = "Insulin"; }
            else if (ServiceName == "PainKillerService") { ServiceNameLabel.Text = "Pain Killer"; ServiceImage.Source = "PainKiller"; }
            else if (ServiceName == "ChestPainService") { ServiceNameLabel.Text = "Chest Pain"; ServiceImage.Source = "ChestPain"; }
            else if (ServiceName == "MinorInjuryService") { ServiceNameLabel.Text = "Minor Injury"; ServiceImage.Source = "MinorInjury"; }
            else if (ServiceName == "BreatingProblemService") { ServiceNameLabel.Text = "Breathing Problem"; ServiceImage.Source = "BreathingProblem"; }

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var nms = con.Query<Users>("Select * from Users where UserId = '" + MSPId + "'");
            foreach (var s in nms)
            {
                TitleLabel.Text = s.FullName;
                GenderLabel.Text = s.Gender;
            }
            con.Close();

            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<MSPData>();
            var nms1 = con1.Query<MSPData>("Select * from MSPData where MSPId = '" + MSPId + "'");
            foreach (var s in nms1)
            {
                plat = s.MSPLatitude;
                plong = s.MSPLongitude;
                serviceCharges = Convert.ToInt32(s.ServiceCharges);
                DesignationLabel.Text = s.Designation;
                ExperienceLabel.Text = s.Experience + " years";
            }
            con1.Close();

            ServiceChargesLabel.Text = serviceCharges.ToString();


            


        }


        protected override async void OnAppearing()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            distance = Math.Round(Location.CalculateDistance(location.Latitude, location.Longitude, Convert.ToDouble(plat), Convert.ToDouble(plong), DistanceUnits.Kilometers), 2);
            DistanceLabel.Text = "Distance = " + distance + " Km";

            distanceCharges = distance * 50;
            TotalCharges = serviceCharges + distanceCharges;
            DistanceChargesLabel.Text = distanceCharges.ToString();
            TotalChargesLabel.Text = TotalCharges.ToString();

            UserLat = location.Latitude.ToString();
            Userlong = location.Longitude.ToString();
        }
    }
}