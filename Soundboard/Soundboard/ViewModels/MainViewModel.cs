using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Soundboard.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      private readonly IAudioPlayer _audioPlayer;

      public ICommand PlayCommand { get; }

      public MainViewModel( IAudioPlayer audioPlayer )
      {
         _audioPlayer = audioPlayer;

         PlayCommand = new RelayCommand( OnPlayCommand );
      }

      private void OnPlayCommand()
      {
         _audioPlayer.Play();
      }
   }
}
