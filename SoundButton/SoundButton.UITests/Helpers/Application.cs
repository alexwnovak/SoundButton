using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace SoundButton.UITests.Helpers
{
   public class Application : IDisposable
   {
      private readonly Process _applicationProcess;

      public AutomationItem MainWindow
      {
         get
         {
            var condition = new PropertyCondition( AutomationElement.ProcessIdProperty, _applicationProcess.Id );
            var mainWindow = AutomationElement.RootElement.FindFirst( TreeScope.Children, condition );
            return new AutomationItem( mainWindow );
         }
      }

      private Application( Process applicationProcess )
      {
         _applicationProcess = applicationProcess;
      }

      public static Application Launch( string applicationPath )
      {
         var applicationProcess = Process.Start( applicationPath );

         return new Application( applicationProcess );
      }

      public void Dispose()
      {
         _applicationProcess.Kill();
         _applicationProcess.Dispose();
      }
   }
}
