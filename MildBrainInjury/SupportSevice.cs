namespace MildBrainInjury {
  public class SupportSevice {
    public string Name {
      get;
      set;
    }
    public string Address {
      get;
      set;
    }
    public string Phone {
      get;
      set;
    }
    public string Category {
      get;
      set;
    }
    public double Longitute {
      get;
      set;
    }
    public double Latitute {
      get;
      set;
    }

    public SupportSevice(string name, string address, string phone, string category, double longitute, double latitute) {
      this.Name = name;
      this.Address = address;
      this.Phone = phone;
      this.Latitute = latitute;
      this.Longitute = longitute;
      this.Category = category;
    }
  }
}