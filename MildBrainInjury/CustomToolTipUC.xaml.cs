using System.Windows.Controls;

namespace MildBrainInjury
{
  public partial class CustomToolTipUC : UserControl
  {
    private string _description;

    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    public CustomToolTipUC()
    {
      InitializeComponent();
    }
  }
}
