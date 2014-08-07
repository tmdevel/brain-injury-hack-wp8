﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MildBrainInjury.Resources;
using System.Windows.Media;
using System.Globalization;

namespace MildBrainInjury
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
      }
      // Set the data context of the listbox control to the sample data
      DataContext = App.ViewModel;

      // Sample code to localize the ApplicationBar
      //BuildLocalizedApplicationBar();
    }

    // Load data for the ViewModel Items
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      //if (!App.ViewModel.IsDataLoaded)
      //{
      //  App.ViewModel.LoadData();
      //}
    }


    // Sample code for building a localized ApplicationBar
    //private void BuildLocalizedApplicationBar()
    //{
    //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
    //    ApplicationBar = new ApplicationBar();

    //    // Create a new button and set the text value to the localized string from AppResources.
    //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
    //    appBarButton.Text = AppResources.AppBarButtonText;
    //    ApplicationBar.Buttons.Add(appBarButton);

    //    // Create a new menu item with the localized string from AppResources.
    //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
    //    ApplicationBar.MenuItems.Add(appBarMenuItem);
    //}
    private void ShowServices(object sender, EventArgs e) {
      NavigationService.Navigate(new Uri("/ServicesPage.xaml", UriKind.Relative));
    }

    private void ShowWebsite(object sender, EventArgs eventArgs) {
      var wbt = new Microsoft.Phone.Tasks.WebBrowserTask {
        Uri = new Uri("http://www.hscboard.hscni.net/RABIIG")
      };
      wbt.Show();
    }

    private void thisPivot_selectionChanged(object sender, SelectionChangedEventArgs e) {
        
    }

    private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Pivot pivot = (Pivot)sender;
        PivotItem selected = (PivotItem)pivot.SelectedItem;
        //Object selected = Pivot1.SelectedItem;
        
        Brush colourcode = selected.Background;
        LayoutRoot.Background = colourcode;
        
        //Brush colourcode = PivotItem2.Background;
               
        
        //LayoutRoot.Background = new SolidColorBrush(Color.FromArgb(255,227,6,19));
    }


  }
}