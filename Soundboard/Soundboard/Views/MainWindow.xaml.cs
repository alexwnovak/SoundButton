using System.Windows;
using Soundboard.ViewModels;

namespace Soundboard.Views
{
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();

         var viewModel = (MainViewModel) DataContext;
         viewModel.MinimizeReqested += ( _, __ ) => WindowState = WindowState.Minimized;
         viewModel.ExitRequested += ( _, __ ) => Close();
      }
   }
}
