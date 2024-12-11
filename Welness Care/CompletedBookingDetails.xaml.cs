using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welness_Care.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Welness_Care
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedBookingDetails : ContentPage
    {
        string UserId, CustomerId, OrderId;
        public CompletedBookingDetails(string UId)
        {
            InitializeComponent(); 
            UserId = UId;


            string ServiceName = "";


            SQLiteConnection con3 = new SQLiteConnection(App.Databaselocation);
            var nms3 = con3.Query<Orders>("Select * from Orders where Id = ?", UserId);
            foreach (var s in nms3)
            {
                ServiceName = s.ServiceName;
                BookingDateLabel.Text = s.Date;
                ServiceChargesLabel.Text = s.ServiceCharges;
                DistanceChargesLabel.Text = s.DistanceCharges;
                TotalChargesLabel.Text = s.TotalCharges;
                CustomerId = s.CustomerId;
                OrderId = s.Id.ToString();
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
        }
    }
}