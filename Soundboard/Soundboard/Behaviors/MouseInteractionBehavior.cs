using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Soundboard.ViewModels;

namespace Soundboard.Behaviors
{
   public class MouseInteractionBehavior : Behavior<Window>
   {
      private readonly InteractionInterpreter _interactionInterpreter = new InteractionInterpreter();
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
            AssociatedObject.MouseMove += OnMouseMove;
         };

         AssociatedObject.Unloaded += ( _, __ ) =>
         {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
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

      private void OnMouseMove( object sender, MouseEventArgs e )
      {
         var position = e.GetPosition( AssociatedObject );

         _interactionInterpreter.MouseMove( position.X, position.Y, e.LeftButton == MouseButtonState.Pressed );
      }

      private void OnLongPress()
      {
         AssociatedObject.Width = 500;
      }
   }
}
