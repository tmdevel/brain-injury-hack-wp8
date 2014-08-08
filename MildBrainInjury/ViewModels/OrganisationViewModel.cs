using System.Collections.ObjectModel;
using System.ComponentModel;
using MildBrainInjury.Models;
using SQLite;

namespace MildBrainInjury.ViewModels {
  public class OrganisationViewModel : INotifyPropertyChanged {

    public SQLiteConnection conn;

    public OrganisationViewModel(string connectionString) {
      conn = new SQLite.SQLiteConnection(connectionString);
    }

    private ObservableCollection<Organisation> _organisations;
    public ObservableCollection<Organisation> Organisations {
      get { return _organisations; }
      set {
        _organisations = value;
        NotifyPropertyChanged("Organisations");
      }
    }

    public void LoadData() {
      Organisations = new ObservableCollection<Organisation>(conn.Query<Organisation>("select * from Organisation LIMIT 100"));
      IsDataLoaded = true;
    }

    public bool IsDataLoaded {
      get;
      private set;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
