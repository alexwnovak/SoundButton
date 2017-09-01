using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SoundButton.ViewModels;

namespace SoundButton.Views
{
   public partial class MainWindow : Window
   {
      private double _originalWidth;
      private readonly MainViewModel _viewModel;
      private readonly List<string> _nameList = new List<string>();

      public MainWindow()
      {
         InitializeComponent();

         _viewModel = (MainViewModel) DataContext;
         _viewModel.MinimizeReqested += ( _, __ ) => OnMinimizeRequested();
         _viewModel.ExitRequested += ( _, __ ) => OnExitRequested();
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

      private void MenuButton_OnLeftClick( object sender, RoutedEventArgs e )
      {
         _viewModel.PlayCommand.Execute( null );
      }

      private void MenuButton_OnRightClick( object sender, RoutedEventArgs e )
      {
         FadeMenuIn();
      }

      private void MenuButton_OnLongPress( object sender, RoutedEventArgs e )
      {
         FadeMenuIn();
      }

      private void FadeMenuIn()
      {
         var storyboard = new Storyboard();
         int beginTime = 0;

         foreach ( UIElement item in MenuStackPanel.Children )
         {
            var fadeAnimation = new DoubleAnimation( 0, 1, new Duration( TimeSpan.FromSeconds( 0.4 ) ) );
            Storyboard.SetTarget( fadeAnimation, item );
            Storyboard.SetTargetProperty( fadeAnimation, new PropertyPath( OpacityProperty ) );
            storyboard.Children.Add( fadeAnimation );

            string transformName = $"TranslateTransform{beginTime}";

            if ( !_nameList.Contains( transformName ) )
            {
               _nameList.Add( transformName );
               item.RenderTransform = new TranslateTransform( -20, 0 );
               RegisterName( transformName, item.RenderTransform );
            }

            var moveAnimation = new DoubleAnimation( -20, 0, new Duration( TimeSpan.FromSeconds( 0.4 ) ) )
            {
               BeginTime = TimeSpan.FromMilliseconds( beginTime ),
               EasingFunction = new CircleEase
               {
                  EasingMode = EasingMode.EaseOut
               }
            };

            Storyboard.SetTargetName( moveAnimation, transformName );
            Storyboard.SetTargetProperty( moveAnimation, new PropertyPath( TranslateTransform.XProperty ) );
            storyboard.Children.Add( moveAnimation );

            beginTime += 120;
         }

         Width = 500;

         storyboard.Begin( this );
      }
   }
}
