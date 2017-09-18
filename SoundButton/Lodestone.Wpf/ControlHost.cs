using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lodestone.Wpf
{
   public class ControlHost<T> where T : FrameworkElement, new()
   {
      private readonly Thread _thread;
      private readonly Window _window;

      public T Control
      {
         get;
      }

      private ControlHost( T control, Window window, Thread thread )
      {
         Control = control;
         _window = window;
         _thread = thread;
      }

      public void WaitForExit() => _thread.Join();

      public void Close()
      {
         _window.Dispatcher.Invoke( delegate
         {
            _window.Close();
         } );
      }

      public static ControlHost<T> Launch( ControlBuilder<T> controlBuilder )
      {
         T control = null;
         Window window = null;

         var tcs = new TaskCompletionSource<bool>();

         var windowThread = new Thread( () =>
         {
            control = controlBuilder.Build();

            window = new MainWindow
            {
               Content = control
            };

            window.Closed += ( s, e ) => window.Dispatcher.InvokeShutdown();
            window.Show();

            tcs.SetResult( true );

            Dispatcher.Run();
         } )
         {
            IsBackground = true
         };

         windowThread.SetApartmentState( ApartmentState.STA );
         windowThread.Start();

         tcs.Task.Wait();

         return new ControlHost<T>( control, window, windowThread );
      }
   }
}
