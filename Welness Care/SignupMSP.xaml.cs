﻿using SQLite;
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
    public partial class SignupMSP : ContentPage
    {
        public SignupMSP()
        {
            InitializeComponent();
        }

        string gender, Uid = "";
        private void Sgn_Clicked(object sender, EventArgs e)
        {
            bool usnmC, nmC, phC, psC, emC, c1,c2;
            String err = "Following Errors Occured:\n";

            SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
            con.CreateTable<Users>();
            var nms = con.Query<Users>("Select UserId from Users where UserName = ?", usnm.Text);
            con.Close();

            int x = nms.Count;


            if (x > 0)
            {
                err += "UserName Already Exist\n";
                usnmC = false;
            }

            else if (usnm.Text != null && usnm.Text != "" && usnm.Text != "admin")
            {
                usnmC = true;
            }

            else
            {
                usnmC = false;
                err += "UserName is Empty or Incorrect\n";
            }

            if (nm.Text != null && nm.Text != "" && Regex.IsMatch(nm.Text, "^(([A-za-z]+[ ]{1}[A-za-z]+)|([A-Za-z]+|[A-za-z]+[ ]{1}[A-za-z]+[ ]{1}[A-za-z]+))$"))
            {
                nmC = true;
            }
            else
            {
                nmC = false;
                err += "Name is Empty or Incorrect\n";
            }


            if (ph.Text != null && ph.Text != "" && Regex.IsMatch(ph.Text, @"^-?\d+\.?\d*$"))
            {
                phC = true;
            }
            else
            {
                phC = false;
                err += "Phone Number is Empty or Incorrect\n";
            }

            if (em.Text != null && em.Text != "" && Regex.IsMatch(em.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))

            {
                emC = true;
            }
            else
            {
                emC = false;
                err += "Email is Empty or Incorrect\n";
            }


            if (designation.Text != null && designation.Text != "")
            {
                c1 = true;
            }
            else
            {
                c1 = false;
                err += "Designation is Empty or Incorrect\n";
            }

            if (experience.Text != null && experience.Text != "")
            {
                c2 = true;
            }
            else
            {
                c2 = false;
                err += "Experience is Empty or Incorrect\n";
            }


            if (pass.Text != null && pass.Text != "" && pass.Text == cpass.Text && Regex.IsMatch(pass.Text, @"^.{5,20}$"))
            {
                psC = true;
            }
            else
            {
                err += "Password Must have atleast 5 characters and both passwords should match\n";
                psC = false;
            }

           

            if (nmC == true && usnmC == true && phC == true && psC == true && emC == true && c1 == true && c2 == true)
            {

                if (GenderMale.IsChecked == true)
                {
                    gender = "Male";
                }
                else if (GenderFemale.IsChecked == true)
                {
                    gender = "Female";
                }
                else if (GenderOther.IsChecked == true)
                {
                    gender = "Other";
                }

                Users users = new Users()
                {
                    UserName = usnm.Text.ToString(),
                    FullName = nm.Text.ToString(),
                    PhoneNumber = ph.Text.ToString(),
                    Email = em.Text.ToString(),
                    Gender = gender,                
                    Password = pass.Text.ToString(),
                    UserType = "Medical Service Provider",
                };
                SQLiteConnection conn = new SQLiteConnection(App.Databaselocation);
                conn.CreateTable<Users>();
                conn.Insert(users);
                conn.Close();


                SQLiteConnection con2 = new SQLiteConnection(App.Databaselocation);
                con2.CreateTable<Users>();
                var nms2 = con2.Query<Users>("Select UserId from Users where UserName = ?", usnm.Text);
                foreach (var s in nms2)
                {
                    Uid = s.UserId.ToString();
                    

                }
                con2.Close();

                MSPData data = new MSPData()
                {
                    MSPId = Uid,
                    ActiveStatus = "Inactive",
                    Gender = gender,
                    MSPLatitude = "",
                    MSPLongitude = "",
                    ServiceCharges = "",
                    Designation = designation.Text.ToString(),
                    Experience = experience.Text.ToString(),
                    BloodPressureService = "Inactive",
                    InjectionsService = "Inactive",
                    BandagesService = "Inactive",
                    InsulinService = "Inactive",
                    PainKillerService = "Inactive",
                    ChestPainService = "Inactive",
                    MinorInjuryService = "Inactive",
                    BreatingProblemService = "Inactive",
                };
                SQLiteConnection con3 = new SQLiteConnection(App.Databaselocation);
                con3.CreateTable<MSPData>();
                con3.Insert(data);
                con3.Close();


                DisplayAlert("Successfull", "SignUp is Successfull", "Ok");
                Navigation.PushAsync(new Login());
            }
            else
            {
                DisplayAlert("Error", err, "Ok");
            }
        }
    }
}