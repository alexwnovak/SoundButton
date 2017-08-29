using System;
using System.Threading.Tasks;

namespace Soundboard.Behaviors
{
   public class InteractionInterpreter
   {
      private bool _leftMouseDown;
      private bool _hasLeftDragged;
      private bool _hasLongPressed;

      public TimeSpan LongPressDuration
      {
         get;
         set;
      } = TimeSpan.FromMilliseconds( 500 );

      public event EventHandler LeftClick;
      protected virtual void OnLeftClick( object sender, EventArgs e ) => LeftClick?.Invoke( sender, e );

      public event EventHandler LeftDrag;
      protected virtual void OnLeftDrag( object sender, EventArgs e ) => LeftDrag?.Invoke( sender, e );

      public event EventHandler LeftLongPress;
      protected virtual void OnLeftLongPress( object sender, EventArgs e ) => LeftLongPress?.Invoke( sender, e );

      public void LeftMouseDown()
      {
         _leftMouseDown = true;

         Task.Factory.StartNew( async () =>
         {
            await Task.Delay( LongPressDuration );
            _hasLongPressed = true;
            OnLeftLongPress( this, EventArgs.Empty );
         } );
      }

      public void LeftMouseUp()
      {
         if ( _leftMouseDown )
         {
            if ( !_hasLeftDragged && !_hasLongPressed )
            {
               OnLeftClick( this, EventArgs.Empty );
            }

            _leftMouseDown = false;
            _hasLeftDragged = false;
            _hasLongPressed = false;
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
