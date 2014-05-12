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
    protected double Longitute {
      get;
      set;
    }
    protected double Latitute {
      get;
      set;
    }

    public SupportSevice(string name, string address, string phone, double longitute, double latitute) {
      this.Name = name;
      this.Address = address;
      this.Phone = phone;
      this.Latitute = latitute;
      this.Longitute = longitute;
    }
  }
}