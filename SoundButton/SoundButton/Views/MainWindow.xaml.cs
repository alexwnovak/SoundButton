using System;
using System.Windows;
using SoundButton.ViewModels;

namespace SoundButton.Views
{
   public partial class MainWindow : Window
   {
      private double _originalWidth;

      public MainWindow()
      {
         InitializeComponent();

         var viewModel = (MainViewModel) DataContext;
         viewModel.MinimizeReqested += ( _, __ ) => OnMinimizeRequested();
         viewModel.ExitRequested += ( _, __ ) => OnExitRequested();
      }

      private void ResetWindowWidth() => Width = _originalWidth;

      private void OnMinimizeRequested()
      {
         ResetWindowWidth();
         WindowState = WindowState.Minimized;
      }

      private void OnExitRequested()
      {
         Close();
      }

      private void MainWindow_OnLoaded( object sender, RoutedEventArgs e ) => _originalWidth = Width;
      private void MainWindow_OnDeactivated( object sender, EventArgs e ) => ResetWindowWidth();
   }
}
