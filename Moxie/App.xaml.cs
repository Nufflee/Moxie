using System.Windows;

namespace Moxie
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      ShutdownMode = ShutdownMode.OnLastWindowClose;
    }
  }
}