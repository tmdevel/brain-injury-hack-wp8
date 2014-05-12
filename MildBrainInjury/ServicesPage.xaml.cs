using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Shell;
using MildBrainInjury.Resources;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;

namespace MildBrainInjury
{
  public partial class ServicesPage : PhoneApplicationPage {

    const int MIN_ZOOM_LEVEL = 1;
    const int MAX_ZOOM_LEVEL = 20;
    const int MIN_ZOOMLEVEL_FOR_LANDMARKS = 16;

    ToggleStatus locationToggleStatus = ToggleStatus.ToggledOff;
    ToggleStatus landmarksToggleStatus = ToggleStatus.ToggledOff;

    GeoCoordinate currentLocation = null;
   
    MapLayer locationLayer = null;

    private List<SupportSevice> source;

    public ServicesPage() {
      InitializeComponent();

      SetupDataForServices();

      // Create the localized ApplicationBar.
      BuildLocalizedApplicationBar();

      //ServicesMap.ZoomLevel = 15;
      //ServicesMap.LandmarksEnabled = true;
      //ServicesMap.PedestrianFeaturesEnabled = true;

      //ShowMyLocationOnTheMap();
      GetLocation();
      ReadLocations();
    }

    private void SetupDataForServices() {
      /*
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
      }
      DataContext = App.ViewModel;*/
      source = new List<SupportSevice>();
      source.Add(new SupportSevice("Carers NI", "www.carersni.org", "02890439843", -0.092215, 51.499559));
      source.Add(new SupportSevice("Child Brain Injury", "www.cbituk.org", "02890817145", -1.201954, 51.961212));
      source.Add(new SupportSevice("Cedar", "www.cedar-foundatiom.org", "02890666188", -5.944646, 54.584680));
      source.Add(new SupportSevice("Jigsaw", "www.jigsawni.org.uk", "02890319054", -5.940712, 54.601806));
      source.Add(new SupportSevice("Praxis Care", "www.praxisprovides.com", "02890234555", -5.939479, 54.587831));


      List<AlphaKeyGroup<SupportSevice>> DataSource = AlphaKeyGroup<SupportSevice>.CreateGroups(source,
                                                                                            System.Threading.Thread
                                                                                                  .CurrentThread
                                                                                                  .CurrentUICulture,
                                                                                            (SupportSevice s) => { return s.Name; }, true);

      ServicesList.ItemsSource = DataSource;

    }

    //private async void ShowMyLocationOnTheMap() {
      // Get my current location.
      //Geolocator myGeolocator = new Geolocator();
      //Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
      //Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
      //GeoCoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

      // Make my current location the center of the Map.
      //ServicesMap.Center = myGeoCoordinate;
      //ServicesMap.Center = new GeoCoordinate(54.6077, -005.9194);
      //ServicesMap.ZoomLevel = 15;
      //ServicesMap.LandmarksEnabled = true;
      //ServicesMap.PedestrianFeaturesEnabled = true;
    //}

    // Load data for the ViewModel Items
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();

      }
    }


    // Placeholder code to contain the ApplicationID and AuthenticationToken
    // that must be obtained online from the Windows Phone Dev Center
    // before publishing an app that uses the Map control.
    private void ServiceMap_Loaded(object sender, RoutedEventArgs e) {
      MapsSettings.ApplicationContext.ApplicationId = "<applicationid>";
      MapsSettings.ApplicationContext.AuthenticationToken = "<authenticationtoken>";
    }

    #region Event handlers for App Bar buttons and menu items

    void ToggleLocation(object sender, EventArgs e)
    {
      switch (locationToggleStatus)
      {
        case ToggleStatus.ToggledOff:
          ShowLocation();
          CenterMapOnLocation();
          locationToggleStatus = ToggleStatus.ToggledOn;
          break;
        case ToggleStatus.ToggledOn:
          ServicesMap.Layers.Remove(locationLayer);
          locationLayer = null;
          locationToggleStatus = ToggleStatus.ToggledOff;
          break;
      }
    }

    void ToggleLandmarks(object sender, EventArgs e)
    {
      switch (landmarksToggleStatus)
      {
        case ToggleStatus.ToggledOff:
          ServicesMap.LandmarksEnabled = true;
          if (ServicesMap.ZoomLevel < MIN_ZOOMLEVEL_FOR_LANDMARKS)
          {
            ServicesMap.ZoomLevel = MIN_ZOOMLEVEL_FOR_LANDMARKS;
          }
          landmarksToggleStatus = ToggleStatus.ToggledOn;
          break;
        case ToggleStatus.ToggledOn:
          ServicesMap.LandmarksEnabled = false;
          landmarksToggleStatus = ToggleStatus.ToggledOff;
          break;
      }

    }

    void ZoomIn(object sender, EventArgs e)
    {
      if (ServicesMap.ZoomLevel < MAX_ZOOM_LEVEL)
      {
        ServicesMap.ZoomLevel++;
      }
    }

    void ZoomOut(object sender, EventArgs e)
    {
      if (ServicesMap.ZoomLevel > MIN_ZOOM_LEVEL)
      {
        ServicesMap.ZoomLevel--;
      }
    }

    #endregion

    #region Helper functions for App Bar button and menu item event handlers

    private void ShowLocation() {
      // Create a small circle to mark the current location.
      Ellipse myCircle = new Ellipse();
      myCircle.Fill = new SolidColorBrush(Colors.Blue);
      myCircle.Height = 20;
      myCircle.Width = 20;
      myCircle.Opacity = 50;

      // Create a MapOverlay to contain the circle.
      MapOverlay myLocationOverlay = new MapOverlay();
      myLocationOverlay.Content = myCircle;
      myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
      myLocationOverlay.GeoCoordinate = currentLocation;

      // Create a MapLayer to contain the MapOverlay.
      locationLayer = new MapLayer();
      locationLayer.Add(myLocationOverlay);

      // Add the MapLayer to the Map.
      ServicesMap.Layers.Add(locationLayer);

    }

    private async void GetLocation() {
      // Get current location.
      //Geolocator myGeolocator = new Geolocator();
      //Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
      //Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
      //currentLocation = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
      currentLocation = new GeoCoordinate(54.6077, -005.9194);
      DrawLocationToMap(currentLocation, "current");
      CenterMapOnLocation();
    }

    private void CenterMapOnLocation()
    {
      ServicesMap.Center = currentLocation;
    }

    #endregion

    // Create the localized ApplicationBar.
    private void BuildLocalizedApplicationBar()
    {
      // Set the page's ApplicationBar to a new instance of ApplicationBar.
      ApplicationBar = new ApplicationBar();
      ApplicationBar.Opacity = 0.5;

      // Create buttons with localized strings from AppResources.
      // Toggle Location button.
      ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/location.png", UriKind.Relative));
      appBarButton.Text = AppResources.AppBarToggleLocationButtonText;
      appBarButton.Click += ToggleLocation;
      ApplicationBar.Buttons.Add(appBarButton);
      // Toggle Landmarks button.
      appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/landmarks.png", UriKind.Relative));
      appBarButton.Text = AppResources.AppBarToggleLandmarksButtonText;
      appBarButton.Click += ToggleLandmarks;
      ApplicationBar.Buttons.Add(appBarButton);
      // Zoom In button.
      appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/zoomin.png", UriKind.Relative));
      appBarButton.Text = AppResources.AppBarZoomInButtonText;
      appBarButton.Click += ZoomIn;
      ApplicationBar.Buttons.Add(appBarButton);
      // Zoom Out button.
      appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/zoomout.png", UriKind.Relative));
      appBarButton.Text = AppResources.AppBarZoomOutButtonText;
      appBarButton.Click += ZoomOut;
      ApplicationBar.Buttons.Add(appBarButton);

      // Create menu items with localized strings from AppResources.
      // Toggle Location menu item.
      ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarToggleLocationMenuItemText);
      appBarMenuItem.Click += ToggleLocation;
      ApplicationBar.MenuItems.Add(appBarMenuItem);
      // Toggle Landmarks menu item.
      appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarToggleLandmarksMenuItemText);
      appBarMenuItem.Click += ToggleLandmarks;
      ApplicationBar.MenuItems.Add(appBarMenuItem);
      // Zoom In menu item.
      appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarZoomInMenuItemText);
      appBarMenuItem.Click += ZoomIn;
      ApplicationBar.MenuItems.Add(appBarMenuItem);
      // Zoom Out menu item.
      appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarZoomOutMenuItemText);
      appBarMenuItem.Click += ZoomOut;
      ApplicationBar.MenuItems.Add(appBarMenuItem);
    }

    private enum ToggleStatus {
      ToggledOff,
      ToggledOn
    }


    private void ReadLocations() {
      foreach (var s in source) {
        DrawLocationToMap(new GeoCoordinate(s.Latitute, s.Longitute), s.Name);
      }
    }

    private void DrawLocationToMap(GeoCoordinate currGeo, string currGeoTitle) {
      Pushpin locationPushPin = new Pushpin();
      locationPushPin.Background = new SolidColorBrush(Colors.Black);
      locationPushPin.Content = currGeoTitle;

      MapOverlay locationPushPinOverlay = new MapOverlay();
      locationPushPinOverlay.Content = locationPushPin;
      locationPushPinOverlay.PositionOrigin = new Point(0, 1);
      locationPushPinOverlay.GeoCoordinate = new GeoCoordinate(currGeo.Latitude, currGeo.Longitude);

      MapLayer locationLayer = new MapLayer();
      locationLayer.Add(locationPushPinOverlay);

      ServicesMap.Layers.Add(locationLayer);
    }

    private void Pivot_Loaded(object sender, RoutedEventArgs e) {
       
    }
  }
}