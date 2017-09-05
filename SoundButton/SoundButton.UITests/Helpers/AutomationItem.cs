using System.Windows.Automation;

namespace SoundButton.UITests.Helpers
{
   public class AutomationItem
   {
      internal AutomationElement AutomationElement
      {
         get;
         set;
      }

      internal AutomationItem()
      {
      }

      public void Click()
      {
         var invokePattern = (InvokePattern) AutomationElement.GetCurrentPattern( InvokePattern.Pattern );
         invokePattern.Invoke();
      }
   }
}
