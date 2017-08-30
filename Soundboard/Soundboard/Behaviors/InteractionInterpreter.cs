using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soundboard.Behaviors
{
   public class InteractionInterpreter
   {
      private bool _leftMouseDown;
      private bool _hasLeftDragged;
      private bool _hasLongPressed;
      private CancellationTokenSource _cancellationTokenSource;

      public TimeSpan LongPressDuration
      {
         get;
         set;
      } = TimeSpan.FromMilliseconds( 400 );

      public event EventHandler LeftClick;
      protected virtual void OnLeftClick( object sender, EventArgs e ) => LeftClick?.Invoke( sender, e );

      public event EventHandler LeftDrag;
      protected virtual void OnLeftDrag( object sender, EventArgs e ) => LeftDrag?.Invoke( sender, e );

      public event EventHandler LeftLongPress;
      protected virtual void OnLeftLongPress( object sender, EventArgs e ) => LeftLongPress?.Invoke( sender, e );

      public void LeftMouseDown()
      {
         _leftMouseDown = true;
         _cancellationTokenSource = new CancellationTokenSource();

         Task.Factory.StartNew( async () =>
         {
            // ContinueWith() is the answer here, which means the SynchronizationContext
            // needs to work in the tests--need to figure that out

            await Task.Delay( LongPressDuration );

            if ( !_cancellationTokenSource.Token.IsCancellationRequested )
            {
               _hasLongPressed = true;

               if ( !_hasLeftDragged )
               {
                  OnLeftLongPress( this, EventArgs.Empty );
               }
            }
         }, _cancellationTokenSource.Token );
      }

      public void LeftMouseUp()
      {
         if ( _leftMouseDown )
         {
            if ( !_hasLeftDragged && !_hasLongPressed )
            {
               OnLeftClick( this, EventArgs.Empty );
            }

            _cancellationTokenSource.Cancel();

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
