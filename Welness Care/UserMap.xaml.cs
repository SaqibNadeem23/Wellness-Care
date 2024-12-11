using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Diagnostics;
using SQLite;
using Welness_Care.Model;
using Rg.Plugins.Popup.Services;

namespace Welness_Care
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMap : ContentPage
    {
        string SId, Gender,ServiceType;

        public UserMap(string UId, string GD, string STP)
        {
            try
            {
                InitializeComponent();
                SId = UId;
                Gender = GD;
                ServiceType = STP;
            }
            catch (Exception e)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(e, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                DisplayAlert("Try Catch error:", e.ToString() + "\n Line Number: " + line.ToString(), "Ok");
            }
        }


        protected override bool OnBackButtonPressed()
        {

            return false;
        }




        private async void meramap_MapClicked(object sender, MapClickedEventArgs e)
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(e.Position.Latitude, e.Position.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                var geocodeAddress = placemark.AdminArea + ", " + placemark.Locality;
                srch1.Text = geocodeAddress;
            }
        }



 
        protected override async void OnAppearing()
        {
            
                var location = await Geolocation.GetLastKnownLocationAsync();

                meramap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(1)));
                var placemarks = await Geocoding.GetPlacemarksAsync(location);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress = placemark.AdminArea + ", " + placemark.Locality;

                }

                if(Gender == "Any")
                {
                    SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                    con.CreateTable<MSPData>();
                    if (ServiceType == "BloodPressureService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BloodPressureService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "InjectionsService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where InjectionsService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "BandagesService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BandagesService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "InsulinService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where InsulinService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "PainKillerService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where PainKillerService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "ChestPainService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where ChestPainService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "MinorInjuryService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where MinorInjuryService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "BreatingProblemService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BreatingProblemService = 'Active' and ActiveStatus = 'Active'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    con.Close();
                    //var nms = con.Query<MSPData>("DECLARE @ServiceType varchar(50) SET @ServiceType = '"+ServiceType+"' Select * from MSPData where ActiveStatus = 'Active' and @ServiceType = 'Active'");
                    //var nms = con.Query<MSPData>("Select * from MSPData where @ServiceType = 'Active' and ActiveStatus = 'Active'");




                }

                else if (Gender == "Female")
                {
                    SQLiteConnection con = new SQLiteConnection(App.Databaselocation);
                    con.CreateTable<MSPData>();
                    if (ServiceType == "BloodPressureService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BloodPressureService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "InjectionsService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where InjectionsService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "BandagesService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BandagesService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "InsulinService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where InsulinService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "PainKillerService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where PainKillerService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "ChestPainService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where ChestPainService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "MinorInjuryService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where MinorInjuryService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    if (ServiceType == "BreatingProblemService")
                    {
                        var nms = con.Query<MSPData>("Select * from MSPData where BreatingProblemService = 'Active' and ActiveStatus = 'Active' and Gender != 'Male'");
                        if (nms.Count > 0)
                        {
                            foreach (var s in nms)
                            {
                                Location loc = new Location
                                {
                                    Latitude = Convert.ToDouble(s.MSPLatitude),
                                    Longitude = Convert.ToDouble(s.MSPLongitude),
                                };


                                CPin pin = new CPin
                                {
                                    Position = new Position(loc.Latitude, loc.Longitude),
                                    Label = "Medical Service Provider",
                                };
                                meramap.CPins = new List<CPin> { pin };

                                pin.MarkerClicked += Pin_Clicked;

                                meramap.Pins.Add(pin);

                                void Pin_Clicked(object sender, EventArgs e)
                                {
                                    PopupNavigation.Instance.PushAsync(new PopPage1(s.MSPId, SId, ServiceType));
                                }
                            }
                        }
                    }

                    con.Close();
                }


            
        }
    }
}