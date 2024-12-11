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
    public partial class MSPDataTableView : ContentPage
    {
        public MSPDataTableView()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<MSPData>();
            var users = con.Table<MSPData>();
            foreach (var x in users)
            {
                StackLayout stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,

                };

                MS.Children.Add(stackLayout);
                Label label = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.Id.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label);



                Label label1 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.MSPId.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label1);

                string gen = "";
                if(x.Gender != "" && x.Gender != null)
                {
                    gen = x.Gender.ToString();
                }
                else
                {
                    gen = "n/a";
                }

                Label label2 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = gen,
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label2);

                Label label3 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.ActiveStatus.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label3);
                Label label4 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.MSPLatitude.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label4);

                Label label7 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.MSPLongitude.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label7);
                Label label5 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.ServiceCharges.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label5);
                Label label6 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.BloodPressureService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label6);
                Label label14 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.InjectionsService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label14);
                Label label8 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.BandagesService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label8);
                Label label9 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.InsulinService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label9);
                Label label10 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.PainKillerService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label10);
                Label label11 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.ChestPainService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label11);
                Label label12 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.MinorInjuryService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label12);
                Label label13 = new Label()
                {
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = x.BreatingProblemService.ToString(),
                    Margin = new Thickness(0, 0, 0, 5),
                };
                stackLayout.Children.Add(label13);
                
            }
            con.Close();
        }
    }
}