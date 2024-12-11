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
    public partial class ServicesPage : ContentPage
    {
        string SId;
        string ServiceCharges = "", BloodPressureService = "", InjectionsService = "", BandagesService = "", InsulinService = "", PainKillerService = "", ChestPainService = "", MinorInjuryService = "", BreatingProblemService = "";
        public ServicesPage(string uId)
        {
            InitializeComponent();
            SId = uId;

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<MSPData>();
            var nms = con.Query<MSPData>("Select * from MSPData where MSPId = ?", SId);

            foreach (var s in nms)
            {
                if(s.ServiceCharges != "" && s.ServiceCharges != null)
                {
                    ChargesEntry.Text = s.ServiceCharges;
                }

                if(s.BloodPressureService == "Active")
                {
                    BPSwtich.IsToggled = true;
                }

                if (s.InjectionsService == "Active")
                {
                    InjectionsSwtich.IsToggled = true;
                }

                if (s.BandagesService == "Active")
                {
                    BandagesSwtich.IsToggled = true;
                }

                if (s.InsulinService == "Active")
                {
                    InsulinSwtich.IsToggled = true;
                }

                if (s.PainKillerService == "Active")
                {
                    PainKillerSwtich.IsToggled = true;
                }

                if (s.ChestPainService == "Active")
                {
                    ChestPainSwtich.IsToggled = true;
                }

                if (s.MinorInjuryService == "Active")
                {
                    MinorInjurySwtich.IsToggled = true;
                }

                if (s.BreatingProblemService == "Active")
                {
                    BreatingProblemSwtich.IsToggled = true;
                }

                


            }
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            bool a1;
            string error = "Following errors occured:\n";

            if (ChargesEntry.Text != "" && ChargesEntry.Text != null)
            {
                a1 = true;
                ServiceCharges = ChargesEntry.Text;
            }
            else
            {
                a1 = false;
                error += "Please Enter Service Charges\n";
            }

            if (a1 == true)
            {
                if (BPSwtich.IsToggled == false)
                {
                    BloodPressureService = "Inactive";
                }
                else
                {
                    BloodPressureService = "Active";
                }

                if (InjectionsSwtich.IsToggled == false)
                {
                    InjectionsService = "Inactive";
                }
                else
                {
                    InjectionsService = "Active";
                }

                if (BandagesSwtich.IsToggled == false)
                {
                    BandagesService = "Inactive";
                }
                else
                {
                    BandagesService = "Active";
                }

                if (InsulinSwtich.IsToggled == false)
                {
                    InsulinService = "Inactive";
                }
                else
                {
                    InsulinService = "Active";
                }

                if (PainKillerSwtich.IsToggled == false)
                {
                    PainKillerService = "Inactive";
                }
                else
                {
                    PainKillerService = "Active";
                }

                if (ChestPainSwtich.IsToggled == false)
                {
                    ChestPainService = "Inactive";
                }
                else
                {
                    ChestPainService = "Active";
                }

                if (MinorInjurySwtich.IsToggled == false)
                {
                    MinorInjuryService = "Inactive";
                }
                else
                {
                    MinorInjuryService = "Active";
                }

                if (BreatingProblemSwtich.IsToggled == false)
                {
                    BreatingProblemService = "Inactive";
                }
                else
                {
                    BreatingProblemService = "Active";
                }


                SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                con.CreateTable<MSPData>();
                con.Query<MSPData>("Update MSPData set ServiceCharges = ?,BloodPressureService = ?,InjectionsService = ?,BandagesService = ?,InsulinService = ?,PainKillerService = ?,ChestPainService = ?,MinorInjuryService = ?,BreatingProblemService = ? where MSPId= '" + SId + "'", ServiceCharges, BloodPressureService, InjectionsService, BandagesService, InsulinService, PainKillerService, ChestPainService, MinorInjuryService, BreatingProblemService);


                DisplayAlert("Successfull", "Changes Executed Successfully", "Ok");

                Navigation.PushAsync(new MSPMasterPage(SId));
            }

            else
            {
                DisplayAlert("Error", error, "Ok");
            }

            









        }
    }
}