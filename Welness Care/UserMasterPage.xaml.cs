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
    public partial class UserMasterPage : FlyoutPage
    {
        private string UId;
        string OrderStatus, GenderCheck = "Any";
        string mspLat, mspLong;
        public UserMasterPage(string UserId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var data = con.Query<Users>("Select * from Users where UserId = ?", UserId);
            string[] dat = new string[3];
            foreach (var s in data)
            {
                dat[0] = s.FullName;
                dat[1] = s.Gender;
                OrderStatus = s.OrderMSPId;
            }
            con.Close();

            lb2.Text = dat[0];
            UId = UserId;

            if (OrderStatus == "" || OrderStatus == "None" || OrderStatus == null)
            {
                MainStack.IsVisible = true;
                OrdetStack.IsVisible = false;

                if (dat[1] == "Male")
                {
                    GenderStack.IsVisible = false;
                }
                else
                {
                    GenderStack.IsVisible = true;
                }
            }
            else
            {
                MainStack.IsVisible = false;
                OrdetStack.IsVisible = true;

                string mspId = "", ServiceName = "";
                

                SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
                var nms1 = con1.Query<Orders>("Select * from Orders where CustomerId = ? and Status = 'Pending'", UId);
                foreach (var s in nms1)
                {
                    ServiceName = s.ServiceName;
                    BookingDateLabel.Text = s.Date;   
                    ServiceChargesLabel.Text = s.ServiceCharges;
                    DistanceChargesLabel.Text = s.DistanceCharges;
                    TotalChargesLabel.Text = s.TotalCharges;
                    mspId = s.MSPId;
                    mspLat = s.MSPLatitude;
                    mspLong = s.MSPLongitude;
                }
                con1.Close();

                
                if (ServiceName == "BloodPressureService") { ServiceNameLabel.Text = "Blood Pressure"; ServiceImage.Source = "BloodPressure"; }
                else if (ServiceName == "InjectionsService") { ServiceNameLabel.Text = "Injections"; ServiceImage.Source = "Injection"; }
                else if (ServiceName == "BandagesService") { ServiceNameLabel.Text = "Bandages"; ServiceImage.Source = "Bandages"; }
                else if (ServiceName == "InsulinService") { ServiceNameLabel.Text = "Insulin"; ServiceImage.Source = "Insulin"; }
                else if (ServiceName == "PainKillerService") { ServiceNameLabel.Text = "Pain Killer"; ServiceImage.Source = "PainKiller"; }
                else if (ServiceName == "ChestPainService") { ServiceNameLabel.Text = "Chest Pain"; ServiceImage.Source = "ChestPain"; }
                else if (ServiceName == "MinorInjuryService") { ServiceNameLabel.Text = "Minor Injury"; ServiceImage.Source = "MinorInjury"; }
                else if (ServiceName == "BreatingProblemService") { ServiceNameLabel.Text = "Breathing Problem"; ServiceImage.Source = "BreathingProblem"; }


                SQLiteConnection con2 = new SQLiteConnection(App.Databaselocation);
                var nms2 = con2.Query<Users>("Select * from Users where UserId = ?", mspId);
                foreach (var s in nms2)
                {
                    ServiceProviderNameLabel.Text = s.FullName;
                    GenderLabel.Text = s.Gender;
                    MobileNumberLabel.Text = s.PhoneNumber;
                }
                con2.Close();

                SQLiteConnection con4 = new SQLiteConnection(App.Databaselocation);
                con4.CreateTable<MSPData>();
                var nms4 = con4.Query<MSPData>("Select * from MSPData where MSPId = '" + mspId + "'");
                foreach (var s in nms4)
                {

                    DesignationLabel.Text = s.Designation;
                    ExperienceLabel.Text = s.Experience + " years";
                }
                con4.Close();

                Location loc = new Location
                {
                    Latitude = Convert.ToDouble(mspLat),
                    Longitude = Convert.ToDouble(mspLong),
                };
                meramap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc.Latitude, loc.Longitude), Distance.FromMiles(1)));

                CPin pin = new CPin
                {
                    Position = new Position(loc.Latitude, loc.Longitude),
                    Label = "Medical Service Provider",
                };
                meramap.CPins = new List<CPin> { pin };

                meramap.Pins.Add(pin);

            }           
        }

        private void StatusSwtich_Toggled(object sender, ToggledEventArgs e)
        {

            if (StatusSwtich.IsToggled == true)
            {
                GenderCheck = "Female";
            }

            else if (StatusSwtich.IsToggled == false)
            {
                GenderCheck = "Any";
            }

        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserInfo(UId));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        


        string ServiceType = "";
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            ServiceType = "BloodPressureService"; 
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            ServiceType = "InjectionsService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));

        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            ServiceType = "BandagesService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            ServiceType = "InsulinService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            ServiceType = "PainKillerService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            ServiceType = "ChestPainService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            ServiceType = "MinorInjuryService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }

        private void TapGestureRecognizer_Tapped_9(object sender, EventArgs e)
        {
            ServiceType = "BreatingProblemService";
            Navigation.PushAsync(new UserMap(UId, GenderCheck, ServiceType));
        }
    }
}