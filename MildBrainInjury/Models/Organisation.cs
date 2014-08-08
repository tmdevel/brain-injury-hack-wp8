using System;
using System.ComponentModel;
using System.Device.Location;
using System.IO.IsolatedStorage;

namespace MildBrainInjury.Models {

  public class Organisation : INotifyPropertyChanged {

    private int _id;
    public int id {
      get { return _id; }
      set {
        if (_id != value) {
          _id = value;
          NotifyPropertyChanged("id");
        }
      }
    }

    private string _name;

    public string name {
      get { return _name; }
      set {
        if (_name != value) {
          _name = value;
          NotifyPropertyChanged("name");
        }
      }
    }

    private string _contact;

    public string contact {
      get { return _contact; }
      set {
        if (_contact != value) {
          _contact = value;
          NotifyPropertyChanged("contact");
        }
      }
    }

    private string _address1;

    public string address1 {
      get { return _address1; }
      set {
        if (_address1 != value) {
          _address1 = value;
          NotifyPropertyChanged("address1");
        }
      }
    }

    private string _address2;

    public string address2 {
      get { return _address2; }
      set {
        if (_address2 != value) {
          _address2 = value;
          NotifyPropertyChanged("address2");
        }
      }
    }

    private string _telephone;

    public string telephone {
      get { return _telephone; }
      set {
        if (_telephone != value) {
          _telephone = value;
          NotifyPropertyChanged("telephone");
        }
      }
    }

    private string _town;

    public string town {
      get { return _town; }
      set {
        if (_town != value) {
          _town = value;
          NotifyPropertyChanged("town");
        }
      }
    }

    private string _postcode;

    public string postcode {
      get { return _postcode; }
      set {
        if (_postcode != value) {
          _postcode = value;
          NotifyPropertyChanged("postcode");
        }
      }
    }

    private string _ageGroups;

    public string ageGroups {
      get { return _ageGroups; }
      set {
        if (_ageGroups != value) {
          _ageGroups = value;
          NotifyPropertyChanged("ageGroups");
        }
      }
    }

    private string _overviewOfServices;

    public string overviewOfServices {
      get { return _overviewOfServices; }
      set {
        if (_overviewOfServices != value) {
          _overviewOfServices = value;
          NotifyPropertyChanged("overviewOfServices");
        }
      }
    }

    private string _email;

    public string email {
      get { return _email; }
      set {
        if (_email != value) {
          _email = value;
          NotifyPropertyChanged("email");
        }
      }
    }

    private string _website;

    public string website {
      get { return _website; }
      set {
        if (_website != value) {
          _website = value;
          NotifyPropertyChanged("website");
        }
      }
    }

    private string _lat;

    public string lat {
      get { return _lat; }
      set {
        if (value != _lat) {
          _lat = value;
          NotifyPropertyChanged("lat");
        }
      }
    }

    private string _long;

    public string @long {
      get { return _long; }
      set {
        if (value != _long) {
          _long = value;
          NotifyPropertyChanged("long");
        }
      }
    }

    private GeoCoordinate _geo;

    public GeoCoordinate geo {
      get { return GetGeo(); }
      set {
        _geo = value;
        NotifyPropertyChanged("geo");
      }
    }

    private double _distance;

    public double distance {
      get { return GetDistance(); }
      set {
        _distance = value;
        NotifyPropertyChanged("distance");
      }
    }

    private string _distanceDisplay;

    public string distanceDisplay {
      get {
        if (lat == null || @long == null)
          return "";

        if (distance == 99999)
          return "";

        return "Distance: " + Math.Round(((distance / 1000) * 0.621371192), 2) + " Miles";
      }
      set {
        {
          _distanceDisplay = value;
          NotifyPropertyChanged("distanceDisplay");
        }
      }
    }

    private GeoCoordinate GetGeo() {
      if (lat == null || @long == null)
        return null;

      return new GeoCoordinate(Double.Parse(lat), Double.Parse(@long));
    }

    private double GetDistance() {
      //if (!App.LocationPermission)
      //  return 99999;

      // if no lat or long, make the distance impossibly high to drop it to the bottom of the list
      if (lat == null || @long == null)
        return 99999;

      if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
        if (!(bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"])
          return 99999;

      //var current = new GeoCoordinate(App.CurrentLocation.Latitude, App.CurrentLocation.Longitude);
      //return geo.GetDistanceTo(current);
      return 99999;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName) {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
