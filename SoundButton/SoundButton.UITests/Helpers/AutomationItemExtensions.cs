using System.Windows.Automation;

namespace SoundButton.UITests.Helpers
{
   public static class AutomationItemExtensions
   {
      public static AutomationItem Find( this AutomationItem automationItem, Property by, object value )
      {
         var condition = by.GetCondition( value );
         var childElement = automationItem.AutomationElement.FindFirst( TreeScope.Children, condition );
         return new AutomationItem( childElement );
      }
   }
}
