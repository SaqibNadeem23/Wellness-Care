using Rg.Plugins.Popup.Services;
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
    public partial class MSPMasterPage : FlyoutPage
    {
        private string UId;
        string activeStatus, OrderStatus;
        string CustomerId = "", OrderId = "";
        string mspLat, mspLong, userLat, userLong;
        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAllAsync();
            return true;
        }

        
        public MSPMasterPage(string UserId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var data = con.Query<Users>("Select * from Users where UserId = ?", UserId);
            string[] dat = new string[1];
            foreach (var s in data)
            {
                dat[0] = s.FullName;
                
            }

            con.Close();

            lb2.Text = dat[0];
            UId = UserId;

            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<MSPData>();
            var nms = con1.Query<MSPData>("Select * from MSPData where MSPId = ?", UId);

            foreach (var s in nms)
            {
                activeStatus = s.ActiveStatus;
                OrderStatus = s.OrderUserId;
            }
            con1.Close();

            if (OrderStatus == "" || OrderStatus == "None" || OrderStatus == null)
            {
                MainStack.IsVisible = true;
                OrdetStack.IsVisible = false;

                if (activeStatus == "Inactive")
                {
                    StatusSwtich.IsToggled = false;
                    StatusLabel.Text = "Offline";
                    StatusLabel.TextColor = Color.Red;

                }
                else if (activeStatus == "Active")
                {
                    StatusSwtich.IsToggled = true;
                    StatusLabel.Text = "Online";
                    StatusLabel.TextColor = Color.Green;
                }
            }
            else
            {
                MainStack.IsVisible = false;
                OrdetStack.IsVisible = true;

                string ServiceName = "";


                SQLiteConnection con3 = new SQLiteConnection(App.Databaselocation);
                var nms3 = con3.Query<Orders>("Select * from Orders where MSPId = ? and Status = 'Pending'", UId);
                foreach (var s in nms3)
                {
                    ServiceName = s.ServiceName;
                    BookingDateLabel.Text = s.Date;
                    ServiceChargesLabel.Text = s.ServiceCharges;
                    DistanceChargesLabel.Text = s.DistanceCharges;
                    TotalChargesLabel.Text = s.TotalCharges;
                    CustomerId = s.CustomerId;
                    OrderId = s.Id.ToString();
                    mspLat = s.MSPLatitude;
                    mspLong = s.MSPLongitude;
                    userLat = s.UserLatitude;
                    userLong = s.UserLongitude;
                }
                con3.Close();


                if (ServiceName == "BloodPressureService") { ServiceNameLabel.Text = "Blood Pressure"; ServiceImage.Source = "BloodPressure"; }
                else if (ServiceName == "InjectionsService") { ServiceNameLabel.Text = "Injections"; ServiceImage.Source = "Injection"; }
                else if (ServiceName == "BandagesService") { ServiceNameLabel.Text = "Bandages"; ServiceImage.Source = "Bandages"; }
                else if (ServiceName == "InsulinService") { ServiceNameLabel.Text = "Insulin"; ServiceImage.Source = "Insulin"; }
                else if (ServiceName == "PainKillerService") { ServiceNameLabel.Text = "Pain Killer"; ServiceImage.Source = "PainKiller"; }
                else if (ServiceName == "ChestPainService") { ServiceNameLabel.Text = "Chest Pain"; ServiceImage.Source = "ChestPain"; }
                else if (ServiceName == "MinorInjuryService") { ServiceNameLabel.Text = "Minor Injury"; ServiceImage.Source = "MinorInjury"; }
                else if (ServiceName == "BreatingProblemService") { ServiceNameLabel.Text = "Breathing Problem"; ServiceImage.Source = "BreathingProblem"; }


                SQLiteConnection con2 = new SQLiteConnection(App.Databaselocation);
                var nms2 = con2.Query<Users>("Select * from Users where UserId = ?", CustomerId);
                foreach (var s in nms2)
                {
                    ServiceProviderNameLabel.Text = s.FullName;
                    GenderLabel.Text = s.Gender;
                    MobileNumberLabel.Text = s.PhoneNumber;
                }
                con2.Close();

                

                Location loc1 = new Location
                {
                    Latitude = Convert.ToDouble(userLat),
                    Longitude = Convert.ToDouble(userLong),
                };
                Pin pin1 = new Pin
                {
                    Position = new Position(loc1.Latitude, loc1.Longitude),
                    Label = "Patient Location",
                };
                meramap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc1.Latitude, loc1.Longitude), Distance.FromMiles(1)));


                //meramap.CPins = new List<CPin> { pin };

                meramap.Pins.Add(pin1);
            }
        }

        private void CompleteButton_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            con.Query<Users>("Update Users SET OrderMSPId = 'None' Where UserId = ?", CustomerId);
            con.Close();

            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<MSPData>();
            con1.Query<MSPData>("Update MSPData SET OrderUserId = 'None',ActiveStatus = 'Active' Where MSPId = ?", UId);
            con1.Close();

            SQLiteConnection con2 = new SQLiteConnection(App.Databaselocation);
            con2.CreateTable<Orders>();
            con2.Query<Orders>("Update Orders SET Status = 'Completed' Where Id = ?", OrderId);
            con2.Close();

            DisplayAlert("Successfull", "The task completed successfully, Kindly receive the payment of "+TotalChargesLabel.Text.ToString()+" from the patient.", "Ok");


            MainStack.IsVisible = true;
            OrdetStack.IsVisible = false;
            StatusSwtich.IsToggled = true;
            StatusLabel.Text = "Online";
            StatusLabel.TextColor = Color.Green;
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SetLocationPage(UId));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserInfo(UId));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }



        private void StatusSwtich_Toggled(object sender, ToggledEventArgs e)
        {
            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<MSPData>();
            var data1 = con1.Query<MSPData>("Select * from MSPData where MSPId = ?", UId);

            foreach (var s in data1)
            {
                if (StatusSwtich.IsToggled == true)
                {
                    if (s.ServiceCharges == "" || s.MSPLatitude == "" || s.ServiceCharges == null || s.MSPLatitude == null)
                    {
                        DisplayAlert("Error", "Please set your Location and Service Charges first.", "Ok");
                        StatusSwtich.IsToggled = false;
                    }
                    else
                    {
                        SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                        con.CreateTable<MSPData>();
                        con.Query<MSPData>("Update MSPData set ActiveStatus = 'Active' where MSPId= '" + UId + "'");
                        con.Close();

                        StatusLabel.Text = "Online";
                        StatusLabel.TextColor = Color.Green;
                    }

                }

                else if (StatusSwtich.IsToggled == false)
                {
                    SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                    con.CreateTable<MSPData>();
                    con.Query<MSPData>("Update MSPData set ActiveStatus = 'Inactive' where MSPId= '" + UId + "'");
                    con.Close();
                    StatusLabel.Text = "Offline";
                    StatusLabel.TextColor = Color.Red;
                }
            }
            con1.Close();

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            
                Navigation.PushAsync(new CompletedBookings(UId));
            
        }

        

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            
                Navigation.PushAsync(new ServicesPage(UId));
           
        }
    }
}