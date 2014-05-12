using System;
using System.Device.Location;
using Microsoft.Phone.Controls;
using Windows.Devices.Geolocation;

namespace MildBrainInjury
{
  public partial class ServicesPage : PhoneApplicationPage {
    public ServicesPage() {
      InitializeComponent();
      ShowMyLocationOnTheMap();
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
  }
}