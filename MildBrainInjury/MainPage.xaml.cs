using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.ApplicationModel;
using Windows.System;
using Microsoft.Phone.Controls;
using MildBrainInjury.Resources;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace MildBrainInjury {
  public partial class MainPage : PhoneApplicationPage {
    private List<SupportPdf> supportPdfs;

    // Constructor
    public MainPage() {
      InitializeComponent();

      PopulateSupportDocuments();
    }

    private void PopulateSupportDocuments() {
      supportPdfs = new List<SupportPdf>();

      supportPdfs.Add(new SupportPdf { DocId = 1, DocName = "Information Leaflet on Brain Injury", DocFileName = "1 Information Leaflet on Brain Injury.pdf", DocColor = "#86146A"}); //Color.FromArgb(0, 134, 20, 106)
      supportPdfs.Add(new SupportPdf { DocId = 2, DocName = "Arrival at Hospital and Early Treatment", DocFileName = "2 Arrival at Hospital and Early Treatment for Moderate to Severe Acquired Brain Injury.pdf", DocColor = "#1292D1" }); // Color.FromArgb(0, 18, 146, 209)
      supportPdfs.Add(new SupportPdf { DocId = 3, DocName = "Discharge and Leaving Hospital", DocFileName = "3 Discharge and Leaving Hospital.pdf", DocColor = "#DC5D21" }); // Color.FromArgb(0, 220, 93, 33)
      supportPdfs.Add(new SupportPdf { DocId = 4, DocName = "Recovery and Early Rehabilitation", DocFileName = "4 Recovery and Early Rehabilitation.pdf", DocColor = "#CD006A" }); // Color.FromArgb(0, 205, 0, 106)
      supportPdfs.Add(new SupportPdf { DocId = 5, DocName = "Rehabilitation", DocFileName = "5 Rehabilitation.pdf", DocColor = "#349737" }); // Color.FromArgb(0, 52, 151, 55)
      supportPdfs.Add(new SupportPdf { DocId = 6, DocName = "Acquired Brain Injury in Children and Young People", DocFileName = "6 Acquired Brain Injury in Children and Young People.pdf", DocColor = "#073E5C" }); // Color.FromArgb(0, 7, 62, 92)
      supportPdfs.Add(new SupportPdf { DocId = 7, DocName = "Family Dynamics and Caring Responsibility", DocFileName = "7 Family Dynamics and Caring Responsibility.pdf", DocColor = "#743F68" }); // Color.FromArgb(0, 116, 63, 104)
      supportPdfs.Add(new SupportPdf { DocId = 8, DocName = "Support Services Available", DocFileName = "8 Support Services Available.pdf", DocColor = "#62AE34" }); // Color.FromArgb(0, 98, 174, 52)
                
      SupportDocumentList.ItemsSource = supportPdfs;
    }

    // Load data for the ViewModel Items
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      ShowDisclaimer();
    }

    private void ShowServices(object sender, EventArgs e) {
      NavigationService.Navigate(new Uri("/ServicesPage.xaml", UriKind.Relative));
    }

    //private void ShowWebsite(object sender, EventArgs eventArgs) {
    //  var wbt = new Microsoft.Phone.Tasks.WebBrowserTask {
    //    Uri = new Uri("http://www.hscboard.hscni.net/RABIIG")
    //  };
    //  wbt.Show();
    //}

    private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        Pivot pivot = (Pivot)sender;
        PivotItem selected = (PivotItem)pivot.SelectedItem;
        
        Brush colourcode = selected.Background;
        LayoutRoot.Background = colourcode;
    }

    private async void SupportDocumentList_OnTap(object sender, GestureEventArgs e) {
      SupportPdf data = (SupportPdf)((LongListSelector)sender).SelectedItem;
      if (data != null && !String.IsNullOrEmpty(data.DocFileName)) {
        var installFolder = Package.Current.InstalledLocation;
        var docFolder = await installFolder.GetFolderAsync("pdf");
        var pdfFile = await docFolder.GetFileAsync(data.DocFileName);

        await Launcher.LaunchFileAsync(pdfFile);
      }
    }

    private void ShowDisclaimer() {
      if (IsolatedStorageSettings.ApplicationSettings.Contains("DisclaimerConsent")) {
        return;
      }
      else {
        HyperlinkButton hyperlinkButton = new HyperlinkButton() {
          Content = "More Information",
          Margin = new Thickness(0, 28, 0, 8),
          HorizontalAlignment = HorizontalAlignment.Left,
          NavigateUri = new Uri("http://www.hscboard.hscni.net/RABIIG", UriKind.Absolute)
        };

        CustomMessageBox messageBox = new CustomMessageBox() {
          Caption = "Disclaimer",
          Message = String.Format("The {0} app uses content from the {1} website. We do not guarantee that all the content is correct at all times. Sometimes the content may be either inaccurate or out of date. It is always the responsibility of the user to verify the accuracy of any information obtained from {0}.  By tapping 'I agree' and using this app you agree that neither TotalMobile nor {1} will be held responsible for any loss, damage or inconvenience caused as a result of any inaccuracy or error within the app.", AppResources.ApplicationTitle, AppResources.AppSupportAgency),
          LeftButtonContent = "I Agree",
          Content = hyperlinkButton,
          IsFullScreen = false
        };

        messageBox.Dismissed += (s1, e1) => {
          switch (e1.Result) {
            // Agree
            case CustomMessageBoxResult.LeftButton:
              IsolatedStorageSettings.ApplicationSettings["DisclaimerConsent"] = true;
              IsolatedStorageSettings.ApplicationSettings.Save();
              break;

            case CustomMessageBoxResult.None:
              Application.Current.Terminate();
              break;
          }
        };

        messageBox.Show();

      }
    }


    public class SupportPdf {
      private int _docId;
      public int DocId {
        get { return _docId; }
        set { _docId = value; }
      }

      private string _docName;
      public string DocName {
        get { return _docName; }
        set { _docName = value; }
      }

      private string _docFileName;
      public string DocFileName {
        get { return _docFileName; }
        set { _docFileName = value; }
      }

      private string _docColor;
      public string DocColor {
        get { return _docColor; }
        set { _docColor = value; }
      }
    }
  }
}