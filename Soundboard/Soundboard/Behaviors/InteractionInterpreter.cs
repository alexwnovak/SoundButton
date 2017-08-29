using System;

namespace Soundboard.Behaviors
{
   public class InteractionInterpreter
   {
      private bool _leftMouseDown;

      public event EventHandler LeftClick;
      protected virtual void OnLeftClick( object sender, EventArgs e ) => LeftClick?.Invoke( sender, e );

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
   }
}
