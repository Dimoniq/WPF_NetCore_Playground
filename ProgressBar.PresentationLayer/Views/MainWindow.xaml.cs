using System.Windows;
using ProgressBar.PresentationLayer.ViewModels;

namespace ProgressBar.PresentationLayer.Views
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(MainWindowViewModel viewModel)
    {
      this.DataContext = viewModel;
      this.InitializeComponent();
    }
  }
}