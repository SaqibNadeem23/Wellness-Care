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
    public partial class CompletedBookings : ContentPage
    {
        string UserId;

        string name;
        public CompletedBookings(string UId)
        {
            InitializeComponent();
            UserId = UId;


            
            SQLiteConnection con1 = new SQLiteConnection(App.Databaselocation);
            con1.CreateTable<Orders>();
            var nms1 = con1.Query<Orders>("Select * from Orders where MSPId = '" + UserId + "' and Status = 'Completed' ORDER BY Id DESC");
            foreach (var s in nms1)
            {
                Frame f = new Frame();
                MainStack.Children.Add(f);

                Grid grid = new Grid()
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    },
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true),
                    Margin = new Thickness(0, 5),
                };
                f.Content = grid;

                string ServiceName = s.ServiceName;
                


                Image img = new Image()
                {
                    HeightRequest = 30,
                    WidthRequest = 30,
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    
                };
                if (ServiceName == "BloodPressureService") { img.Source = "BloodPressure"; }
                else if (ServiceName == "InjectionsService") { img.Source = "Injection"; }
                else if (ServiceName == "BandagesService") { img.Source = "Bandages"; }
                else if (ServiceName == "InsulinService") { img.Source = "Insulin"; }
                else if (ServiceName == "PainKillerService") { img.Source = "PainKiller"; }
                else if (ServiceName == "ChestPainService") { img.Source = "ChestPain"; }
                else if (ServiceName == "MinorInjuryService") { img.Source = "MinorInjury"; }
                else if (ServiceName == "BreatingProblemService") { img.Source = "BreathingProblem"; }

                img.SetValue(Grid.ColumnProperty, 0);
                grid.Children.Add(img);
                

                StackLayout sss = new StackLayout();
                sss.SetValue(Grid.ColumnProperty, 1);
                grid.Children.Add(sss);

                
                SQLiteConnection con2 = new SQLiteConnection(App.Databaselocation);
                con2.CreateTable<Users>();
                var nms2 = con2.Query<Users>("Select * from Users where UserId = '" + s.CustomerId + "'");
                foreach (var s2 in nms2)
                {
                    name = s2.FullName;
                }
                con2.Close();

                Label label = new Label()
                {
                    Text = name,
                    FontFamily = "serif",
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    TextColor = Color.FromHex("#1b1b1b"),
                };
                sss.Children.Add(label);

                Label label1 = new Label()
                {
                    Text = "Total Charges: " + s.TotalCharges,
                    FontFamily = "serif",
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    TextColor = Color.FromHex("#1b1b1b"),
                };
                sss.Children.Add(label1);


                Label label2 = new Label()
                {
                    Text = s.Date,
                    FontFamily = "serif",
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                    TextColor = Color.FromHex("#1b1b1b"),
                };
                label2.SetValue(Grid.ColumnProperty, 2);
                grid.Children.Add(label2);


                var TapGestureRecognizer = new TapGestureRecognizer();
                TapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                f.GestureRecognizers.Add(TapGestureRecognizer);

                void TapGestureRecognizer_Tapped(object sender, EventArgs e)
                {
                    Navigation.PushAsync(new CompletedBookingDetails(s.Id.ToString()));
                }

            }
            con1.Close();
        }

        
    }
}


