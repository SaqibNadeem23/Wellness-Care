using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Welness_Care.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Welness_Care
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfo : ContentPage
    {
        string SName, SEmail, SPhone, SPass, SId,UserType;
        public UserInfo(string UId)
        {
            InitializeComponent();
            SId = UId;

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var data = con.Query<Users>("Select * from Users where UserId = ?", SId);
            foreach (var s in data)
            {
                SName = s.FullName;
                SEmail = s.Email;
                SPhone = s.PhoneNumber;
                SPass = s.Password;
                UserType = s.UserType;

                NameEntry.Text = SName;
                NumberEntry.Text = SPhone;
                EmailEntry.Text = SEmail;
                PasswordEntry.Text = SPass;

                NameEntry.IsEnabled = false;
                NumberEntry.IsEnabled = false;
                EmailEntry.IsEnabled = false;
                PasswordEntry.IsEnabled = false;
                btn.IsEnabled = false;
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            NameEntry.IsEnabled = true;
            NumberEntry.IsEnabled = true;
            EmailEntry.IsEnabled = true;
            PasswordEntry.IsEnabled = true;
            btn.IsEnabled = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            bool nmC, phC, psC, emC;
            String err = "Following Errors Occured:\n";

            if (NameEntry.Text != null && NameEntry.Text != "" && Regex.IsMatch(NameEntry.Text, "^(([A-za-z]+[ ]{1}[A-za-z]+)|([A-Za-z]+|[A-za-z]+[ ]{1}[A-za-z]+[ ]{1}[A-za-z]+))$"))
            {
                nmC = true;
            }
            else
            {
                nmC = false;
                err += "Name is Empty or Incorrect\n";
            }


            if (NumberEntry.Text != null && NumberEntry.Text != "" && Regex.IsMatch(NumberEntry.Text, @"^-?\d+\.?\d*$"))
            {
                phC = true;
            }
            else
            {
                phC = false;
                err += "Phone Number is Empty or Incorrect\n";
            }

            if (EmailEntry.Text != null && EmailEntry.Text != "" && Regex.IsMatch(EmailEntry.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))

            {
                emC = true;
            }
            else
            {
                emC = false;
                err += "Email is Empty or Incorrect\n";
            }


            if (PasswordEntry.Text != null && PasswordEntry.Text != "")
            {
                psC = true;
            }
            else
            {
                psC = false;
                err += "Password is Empty or Incorrect\n";
            }

            if (nmC == true && phC == true && psC == true && emC == true)
            {
                SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                con.CreateTable<Users>();
                con.Query<Users>("Update Users SET FullName = ?, PhoneNumber = ?, Email = ?, Password = ? Where UserId = ?", NameEntry.Text, NumberEntry.Text, EmailEntry.Text, PasswordEntry.Text, SId);
                con.Close();

                DisplayAlert("Successfull", "Updated Successfully", "ok");

                if(UserType == "User")
                {
                      Navigation.PushAsync(new UserMasterPage(SId));

                }
                else if(UserType == "Medical Service Provider")
                {
                    Navigation.PushAsync(new MSPMasterPage(SId));
                }
            }
            else
            {
                DisplayAlert("Error", err, "Ok");
            }
            //Navigation.PushAsync(new Main("1", "Admin", "123", "admin@admin.com", "admin123"));
        }
    }
}