using System.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace Soundboard
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         SimpleIoc.Default.Register<IAudioPlayer>( () =>
         {
            var audioPlayer = new AudioPlayer();
            audioPlayer.Initialize();
            return audioPlayer;
         } );
      }
   }
}
