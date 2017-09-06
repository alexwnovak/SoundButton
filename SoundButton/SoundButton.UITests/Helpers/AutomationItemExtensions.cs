using System;
using System.Threading;
using System.Windows.Automation;

namespace SoundButton.UITests.Helpers
{
   public static class AutomationItemExtensions
   {
      public static AutomationElement Find( this AutomationElement automationElement, Property by, object value )
      {
         var propertyCondition = by.GetCondition( value );
         DateTime startTime = DateTime.Now;

         for ( int attempt = 0; attempt < 50; attempt++ )
         {
            System.Diagnostics.Debug.WriteLine( "Looking for element" );

            var childElement = automationElement.FindFirst( TreeScope.Children, propertyCondition );

            if ( childElement != null )
            {
               System.Diagnostics.Debug.WriteLine( "Element found" );
               return childElement;
            }

            System.Diagnostics.Debug.WriteLine( "Element not found, sleeping..." );
            Thread.Sleep( 100 );
         }

         var elapsedTime = DateTime.Now - startTime;
         throw new Exception( $"Unable to find by value {value} after {elapsedTime.TotalSeconds} seconds" );
      }

      public static AutomationItem Find( this AutomationItem automationItem, Property by, object value )
      {
         return new AutomationItem( Find( automationItem.AutomationElement, by, value ) );
      }
   }
}
