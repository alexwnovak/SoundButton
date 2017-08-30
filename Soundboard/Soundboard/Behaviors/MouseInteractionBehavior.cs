using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Soundboard.ViewModels;
using Soundboard.Views;

namespace Soundboard.Behaviors
{
   public class MouseInteractionBehavior : Behavior<MainWindow>
   {
      private readonly InteractionInterpreter _interactionInterpreter = new InteractionInterpreter();
      private readonly List<string> _nameList = new List<string>();
      private MainViewModel _mainViewModel;

      public MouseInteractionBehavior()
      {
         _interactionInterpreter.LeftClick += ( _, __ ) => _mainViewModel.PlayCommand.Execute( null );
         _interactionInterpreter.LeftDrag += ( _, __ ) => AssociatedObject.DragMove();
         _interactionInterpreter.LeftLongPress += ( _, __ ) => Dispatcher.BeginInvoke( new Action( OnLongPress ) );
      }

      protected override void OnAttached()
      {
         AssociatedObject.Loaded += ( _, __ ) =>
         {
            _mainViewModel = (MainViewModel) AssociatedObject.DataContext;
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.MouseRightButtonUp += OnMouseRightButtonUp;
            AssociatedObject.MouseMove += OnMouseMove;
         };

         AssociatedObject.Unloaded += ( _, __ ) =>
         {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.MouseRightButtonUp -= OnMouseRightButtonUp;
            AssociatedObject.MouseMove -= OnMouseMove;
         };
      }

      private void OnMouseLeftButtonDown( object sender, MouseEventArgs e )
      {
         _interactionInterpreter.LeftMouseDown();
      }

      private void OnMouseLeftButtonUp( object sender, MouseEventArgs e )
      {
         _interactionInterpreter.LeftMouseUp();
      }

      private void OnMouseRightButtonUp( object sender, MouseEventArgs e ) => FadeInMenu();

      private void OnMouseMove( object sender, MouseEventArgs e )
      {
         var position = e.GetPosition( AssociatedObject );

         _interactionInterpreter.MouseMove( position.X, position.Y, e.LeftButton == MouseButtonState.Pressed );
      }

      private void OnLongPress() => FadeInMenu();

      private void FadeInMenu()
      {
         var storyboard = new Storyboard();
         int beginTime = 0;

         NameScope.SetNameScope( this, new NameScope() );

         foreach ( UIElement item in AssociatedObject.MenuStackPanel.Children )
         {
            var fadeAnimation = new DoubleAnimation( 0, 1, new Duration( TimeSpan.FromSeconds( 0.4 ) ) );
            Storyboard.SetTarget( fadeAnimation, item );
            Storyboard.SetTargetProperty( fadeAnimation, new PropertyPath( UIElement.OpacityProperty ) );
            storyboard.Children.Add( fadeAnimation );

            string transformName = $"TranslateTransform{beginTime}";

            if ( !_nameList.Contains( transformName ) )
            {
               _nameList.Add( transformName );
               item.RenderTransform = new TranslateTransform( -20, 0 );
               AssociatedObject.RegisterName( transformName, item.RenderTransform );
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

         AssociatedObject.Width = 500;

         storyboard.Begin( AssociatedObject );
      }
   }
}
