using System;
using System.Windows.Input;

namespace Soundboard.Behaviors
{
   public class InteractionInterpreter
   {
      private bool _leftMouseDown;

      public event EventHandler LeftClick;
      protected virtual void OnLeftClick( object sender, EventArgs e ) => LeftClick?.Invoke( sender, e );

      public event EventHandler LeftDrag;
      protected virtual void OnLeftDrag( object sender, EventArgs e ) => LeftDrag?.Invoke( sender, e );

      public void LeftMouseDown()
      {
         _leftMouseDown = true;
      }

      public void LeftMouseUp()
      {
         if ( _leftMouseDown )
         {
            _leftMouseDown = false;
            OnLeftClick( this, EventArgs.Empty );
         }
      }

      public void MouseMove( double deltaX, double deltaY, bool leftButtonDown )
      {
         if ( leftButtonDown )
         {
            OnLeftDrag( this, EventArgs.Empty );
         }
      }
   }
}
