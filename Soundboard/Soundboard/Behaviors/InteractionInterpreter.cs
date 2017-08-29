using System;

namespace Soundboard.Behaviors
{
   public class InteractionInterpreter
   {
      private bool _leftMouseDown;
      private bool _hasLeftDragged;

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
            if ( !_hasLeftDragged )
            {
               OnLeftClick( this, EventArgs.Empty );
            }

            _leftMouseDown = false;
            _hasLeftDragged = false;
         }
      }

      public void MouseMove( double deltaX, double deltaY, bool leftButtonDown )
      {
         if ( leftButtonDown )
         {
            _hasLeftDragged = true;
            OnLeftDrag( this, EventArgs.Empty );
         }
      }
   }
}
