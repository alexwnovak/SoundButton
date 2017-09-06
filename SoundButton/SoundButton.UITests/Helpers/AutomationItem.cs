using System;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using FlaUI.Core.Input;

namespace SoundButton.UITests.Helpers
{
   public class AutomationItem
   {
      internal AutomationElement AutomationElement
      {
         get;
      }

      internal AutomationItem( AutomationElement automationElement )
      {
         AutomationElement = automationElement;
      }

      public void Click()
      {
         var invokePattern = (InvokePattern) AutomationElement.GetCurrentPattern( InvokePattern.Pattern );
         invokePattern.Invoke();
      }

      public void Press( TimeSpan duration )
      {
         var boundingRect = (Rect) AutomationElement.GetCurrentPropertyValue( AutomationElement.BoundingRectangleProperty );

         double centerX = boundingRect.Left + boundingRect.Width / 2;
         double centerY = boundingRect.Top + boundingRect.Height / 2;

         Mouse.MoveTo( (int) centerX, (int) centerY );
         Mouse.Down( MouseButton.Left );

         Thread.Sleep( duration );

         Mouse.Up( MouseButton.Left );
      }
   }
}
