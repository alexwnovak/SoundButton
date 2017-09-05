using System;
using System.Diagnostics;

namespace SoundButton.UITests.Helpers
{
   public class Application : IDisposable
   {
      private readonly Process _applicationProcess;

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
