using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Lodestone.Wpf
{
   public class ControlHost
   {
      private readonly Thread _thread;

      private ControlHost( Thread thread )
      {
         _thread = thread;
      }

      public void WaitForExit() => _thread.Join();

      public void Close()
      {
         _thread.Abort();
      }

      public static ControlHost Launch<T>( ControlBuilder<T> controlBuilder ) where T: FrameworkElement, new()
      {
         var windowThread = new Thread( () =>
         {
            var window = new MainWindow
            {
               Content = controlBuilder.Build()
            };

            window.Closed += ( s, e ) => window.Dispatcher.InvokeShutdown();
            window.Show();
            Dispatcher.Run();
         } )
         {
            IsBackground = true
         };

         windowThread.SetApartmentState( ApartmentState.STA );
         windowThread.Start();

         return new ControlHost( windowThread );
      }
   }
}
