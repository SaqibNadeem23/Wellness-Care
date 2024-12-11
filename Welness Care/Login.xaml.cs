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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
         
        }

     
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Signup());
        }
   


        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var nms = con.Query<Users>("Select UserId from Users where UserName = ? and Password = ?", name.Text, ps1.Text);



            string[] arr = new string[1];
            foreach (var s in nms)
            {
                arr[0] = s.UserId.ToString();
            }

            string idd = "";
            idd = arr[0];
            var data = con.Query<Users>("Select * from Users where UserId = ?", idd);

            string[] dat = new string[11];
            foreach (var s in data)
            {
                dat[0] = s.UserId.ToString();               
                dat[1] = s.UserType;
                
            }

            con.Close();

            int x = nms.Count;


            if (name.Text == "admin" && ps1.Text == "admin")
            {
                Navigation.PushAsync(new AdminView());
            }

            else if (x > 0 && dat[1] == "User")
            {
                Navigation.PushAsync(new UserMasterPage(dat[0]));
            }
            else if (x > 0 && dat[1] == "Medical Service Provider")
            {
                Navigation.PushAsync(new MSPMasterPage(dat[0]));
            }
            else
            {
                DisplayAlert("Error", "Username or Password is incorrect", "Ok");

            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupMSP());
        }
    }
}