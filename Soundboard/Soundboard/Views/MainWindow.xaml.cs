using System;
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
         viewModel.MinimizeReqested += ( _, __ ) => OnMinimizeRequested();
         viewModel.ExitRequested += ( _, __ ) => OnExitRequested();
      }

      private void ResetWindowWidth() => Width = 148;

      private void OnMinimizeRequested()
      {
         ResetWindowWidth();
         WindowState = WindowState.Minimized;
      }

      private void OnExitRequested()
      {
         Close();
      }

      private void MainWindow_OnDeactivated( object sender, EventArgs e ) => ResetWindowWidth();
   }
}
