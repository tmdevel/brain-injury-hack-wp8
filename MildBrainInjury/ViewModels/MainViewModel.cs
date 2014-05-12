﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MildBrainInjury.Resources;

namespace MildBrainInjury.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    public MainViewModel()
    {
      this.Items = new ObservableCollection<ItemViewModel>();
    }

    /// <summary>
    /// A collection for ItemViewModel objects.
    /// </summary>
    public ObservableCollection<ItemViewModel> Items { get; private set; }

    private string _sampleProperty = "Sample Runtime Property Value";
    /// <summary>
    /// Sample ViewModel property; this property is used in the view to display its value using a Binding
    /// </summary>
    /// <returns></returns>
    public string SampleProperty
    {
      get
      {
        return _sampleProperty;
      }
      set
      {
        if (value != _sampleProperty)
        {
          _sampleProperty = value;
          NotifyPropertyChanged("SampleProperty");
        }
      }
    }

    /// <summary>
    /// Sample property that returns a localized string
    /// </summary>
    public string LocalizedSampleProperty
    {
      get
      {
        return AppResources.SampleProperty;
      }
    }

    public bool IsDataLoaded
    {
      get;
      private set;
    }

    /// <summary>
    /// Creates and adds a few ItemViewModel objects into the Items collection.
    /// </summary>
    public void LoadData()
    {
      // Sample data; replace with real data
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 1", LineTwo="Service centre address or contact" });
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 2", LineTwo="Service centre address or contact" });
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 3", LineTwo="Service centre address or contact" });
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 4", LineTwo="Service centre address or contact" });
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 5", LineTwo="Service centre address or contact" });
      this.Items.Add(new ItemViewModel() { LineOne="Service Centre 6", LineTwo="Service centre address or contact" });
      this.IsDataLoaded = true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (null != handler)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}