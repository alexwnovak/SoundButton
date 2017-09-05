using System.Windows.Automation;

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
   }
}
