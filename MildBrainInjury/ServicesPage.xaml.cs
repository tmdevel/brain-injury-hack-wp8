using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;
using ViewModels;
using Microsoft.Phone.Controls;
using Windows.Devices.Geolocation;

namespace MildBrainInjury
{
  public partial class ServicesPage : PhoneApplicationPage {
    public ServicesPage() {
      InitializeComponent();
      ShowMyLocationOnTheMap();


      SetupDataForServices();
    }

    private void SetupDataForServices() {
      /*
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
      }
      DataContext = App.ViewModel;*/
      var source = new List<SupportSevice>();
      source.Add(new SupportSevice("Carers NI", "www.carersni.org", "02890439843", "Help", 54.597, 5.93));
      source.Add(new SupportSevice("Child Brain Injury", "www.cbituk.org", "02890817145", "Help", 54.597, 5.93));
      source.Add(new SupportSevice("Cedar", "www.cedar-foundatiom.org", "02890666188", "Care", 54.597, 5.93));
      source.Add(new SupportSevice("Jigsaw", "www.jigsawni.org.uk", "02890319054", "Care", 54.597, 5.93));
      source.Add(new SupportSevice("Praxis Care", "www.praxisprovides.com", "02890234555", "Help", 54.597, 5.93));

      List<CategorisedGroup<SupportSevice>> DataSource = CategorisedGroup<SupportSevice>.CreateGroups(source, System.Threading.Thread.CurrentThread.CurrentUICulture,
                                                                                            (s) => { return s.Category; }, true);

      ServicesList.ItemsSource = DataSource;

    }

    private async void ShowMyLocationOnTheMap() {
      // Get my current location.
      //Geolocator myGeolocator = new Geolocator();
      //Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
      //myGeoposition.Coordinate.Latitude = 147.00;
      //Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
      //GeoCoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

      // Make my current location the center of the Map.
      //ServicesMap.Center = myGeoCoordinate;
      ServicesMap.Center = new GeoCoordinate(54.6077, -005.9194);
      ServicesMap.ZoomLevel = 15;
    }

    private void Pivot_Loaded(object sender, RoutedEventArgs e) {
       
    }
  }
}