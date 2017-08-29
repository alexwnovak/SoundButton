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
      }

      protected override void OnAttached()
      {
         AssociatedObject.Loaded += ( _, __ ) =>
         {
            _mainViewModel = (MainViewModel) AssociatedObject.DataContext;
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
         };

         AssociatedObject.Unloaded += ( _, __ ) =>
         {
            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
         };
      }

      private void OnMouseLeftButtonDown( object sender, MouseEventArgs e )
         => _interactionInterpreter.LeftMouseDown();

      private void OnMouseLeftButtonUp( object sender, MouseEventArgs e )
         => _interactionInterpreter.LeftMouseUp();
   }
}
