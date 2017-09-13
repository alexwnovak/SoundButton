using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using SoundButton.Controls;

namespace SoundButton.Automation
{
   public class MenuButtonAutomationPeer : FrameworkElementAutomationPeer, IInvokeProvider
   {
      public MenuButtonAutomationPeer( FrameworkElement owner )
         : base( owner )
      {
      }

      public override object GetPattern( PatternInterface patternInterface )
      {
         if ( patternInterface == PatternInterface.Invoke )
         {
            return this;
         }

         return base.GetPattern( patternInterface );
      }

      protected override string GetClassNameCore()
      {
         return nameof( MenuButton );
      }

      protected override AutomationControlType GetAutomationControlTypeCore()
      {
         return AutomationControlType.Button;
      }

      public void Invoke()
      {
         Dispatcher.BeginInvoke( DispatcherPriority.Input, new DispatcherOperationCallback( delegate
         {
            ((MenuButton) Owner).RaiseLeftClickEvent();
            return null;
         } ), null );
      }
   }
}
