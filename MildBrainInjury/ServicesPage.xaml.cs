using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using ViewModels;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Shell;
using MildBrainInjury.Resources;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;

namespace MildBrainInjury {
  public partial class ServicesPage : PhoneApplicationPage {

    const int MIN_ZOOM_LEVEL = 1;
    const int MAX_ZOOM_LEVEL = 20;
    const int MIN_ZOOMLEVEL_FOR_LANDMARKS = 16;

    ToggleStatus locationToggleStatus = ToggleStatus.ToggledOff;
    ToggleStatus landmarksToggleStatus = ToggleStatus.ToggledOff;

    private static Double niDefaultLatitude = 54.6077;
    private static Double niDefaultLongitude = -005.9194;
    private Double usDefaultLatitude = 47.669444;
    private Double usDefaultLongitude = -122.123889;
    GeoCoordinate currentLocation = new GeoCoordinate(niDefaultLatitude, niDefaultLongitude);
   
    MapLayer locationLayer = null;

    private List<SupportSevice> source;
    public List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();

    public ServicesPage() {
      InitializeComponent();

      SetupDataForServices();

      this.ServicesMap.Loaded += ServiceMapView_Loaded;
    }

    private void ServiceMapView_Loaded(object sender, RoutedEventArgs e) {

      ReadLocations();
    }

    private void SetupDataForServices() {
      /*
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
      }
      DataContext = App.ViewModel;*/

      source = new List<SupportSevice>();
      source.Add(new SupportSevice("Carers NI", "http://www.carersni.org", "02890439843", "Aftercare", -0.092215, 51.499559));
      source.Add(new SupportSevice("Child Brain Injury", "http://www.cbituk.org", "02890817145", "Aftercare", -1.201954, 51.961212));
      source.Add(new SupportSevice("Cedar", "http://www.cedar-foundatiom.org", "02890666188", "Help", -5.944646, 54.584680));
      source.Add(new SupportSevice("Jigsaw", "http://www.jigsawni.org.uk", "02890319054", "Help", -5.940712, 54.601806));
      source.Add(new SupportSevice("Praxis Care", "http://www.praxisprovides.com", "02890234555", "Help", -5.939479, 54.587831));

      List<CategorisedGroup<SupportSevice>> DataSource = CategorisedGroup<SupportSevice>.CreateGroups(source,
                                                                                            System.Threading.Thread
                                                                                                  .CurrentThread
                                                                                                  .CurrentUICulture,
                                                                                            (SupportSevice s) => s.Category, true);

      ServicesList.ItemsSource = DataSource;

    }

    // Placeholder code to contain the ApplicationID and AuthenticationToken
    // that must be obtained online from the Windows Phone Dev Center
    // before publishing an app that uses the Map control.
    private void ServiceMap_Loaded(object sender, RoutedEventArgs e) {
      MapsSettings.ApplicationContext.ApplicationId = "<applicationid>";
      MapsSettings.ApplicationContext.AuthenticationToken = "<authenticationtoken>";
    }

    #region Event handlers for App Bar buttons and menu items

    void ToggleLocation(object sender, EventArgs e) {
      switch (locationToggleStatus) {
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
      myCircle.Fill = new SolidColorBrush(Color.FromArgb(225, 227, 6, 19));
      myCircle.Height = 20;
      myCircle.Width = 20;
      myCircle.Opacity = 50;

      // Create a MapOverlay to contain the circle.
      MapOverlay myLocationOverlay = new MapOverlay();
      myLocationOverlay.Content = myCircle;
      myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
      GetLocation();
      myLocationOverlay.GeoCoordinate = currentLocation;

      // Create a MapLayer to contain the MapOverlay.
      locationLayer = new MapLayer();
      locationLayer.Add(myLocationOverlay);

      // Add the MapLayer to the Map.
      ServicesMap.Layers.Add(locationLayer);

    }

    private async void GetLocation() {
      // Get current location.
      Geolocator myGeolocator = new Geolocator();
      Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
      Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
      if (myGeocoordinate.Latitude.Equals(usDefaultLatitude) && myGeocoordinate.Longitude.Equals(usDefaultLongitude))
        currentLocation = new GeoCoordinate(niDefaultLatitude, niDefaultLongitude);
      else
        currentLocation = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
    }

    private void CenterMapOnLocation() {
      ServicesMap.Center = currentLocation;
    }

    #endregion

    // Create the localized ApplicationBar.
    private void BuildLocalizedApplicationBar() {
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
    }

    private enum ToggleStatus {
      ToggledOff,
      ToggledOn
    }


    private void ReadLocations() {
      try {
        ServicesMap.Layers.Clear();
        MyCoordinates.Clear();
        foreach (var s in source) {
          MyCoordinates.Add(new GeoCoordinate { Latitude = double.Parse("" + s.Latitute), Longitude = double.Parse("" + s.Longitute) });
        }

        DrawMapMarkers();

        ServicesMap.Center = MyCoordinates[MyCoordinates.Count - 1];

        Dispatcher.BeginInvoke(() => {
            ServicesMap.SetView(LocationRectangle.CreateBoundingRectangle(MyCoordinates));
        });
        ServicesMap.SetView(MyCoordinates[MyCoordinates.Count - 1], 10, MapAnimationKind.Linear);
      }
      catch
      {
      }
    }

    private void DrawMapMarkers()
    {
      //MapVieMode.Layers.Clear();
      MapLayer mapLayer = new MapLayer();
      // Draw marker for current position       

      // Draw markers for location(s) / destination(s)
      for (int i = 0; i < MyCoordinates.Count; i++) {
        CustomToolTipUC _tooltip = new CustomToolTipUC();
        _tooltip.Description = source[i].Name;// + "\n" + source[i].Address;
        _tooltip.DataContext = source[i];
        //_tooltip.Menuitem.Click += Menuitem_Click;
        _tooltip.imgmarker.Tap += _tooltip_Tapimg;
        MapOverlay overlay = new MapOverlay();
        overlay.Content = _tooltip;
        overlay.GeoCoordinate = MyCoordinates[i];
        overlay.PositionOrigin = new Point(0.0, 1.0);
        mapLayer.Add(overlay);
      }
      ServicesMap.Layers.Add(mapLayer);
    }

    private void _tooltip_Tapimg(object sender, System.Windows.Input.GestureEventArgs e) {
      try {
        Image item = (Image)sender;
        string selecteditem = item.Tag.ToString();
        var selectedparkdata = source.FindAll(s => s.Name == selecteditem).ToArray();

        if (selectedparkdata.Length > 0)
        {
          foreach (var items in selectedparkdata)
          {
            ContextMenu contextMenu = ContextMenuService.GetContextMenu(item);
            contextMenu.DataContext = items;
            if (contextMenu.Parent == null) {
              contextMenu.IsOpen = true;
            }
            break;
          }
        }
      }
      catch
      {
      }
    }

    private void NavigateToWebsite(object sender, RoutedEventArgs e) {
      var button = sender as HyperlinkButton;

      if (button == null) return;

      var wbt = new Microsoft.Phone.Tasks.WebBrowserTask {
        Uri = new Uri(button.NavigateUri.OriginalString)
      };
      wbt.Show();
    }

    private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
      BuildLocalizedApplicationBar();
      ApplicationBar.IsVisible = ((((Pivot)sender).SelectedIndex) == 1);
    }
  }
}