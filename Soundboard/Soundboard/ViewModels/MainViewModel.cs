using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Soundboard.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      private readonly IAudioPlayer _audioPlayer;

      public ICommand PlayCommand { get; }
      public ICommand MinimizeCommand { get; }
      public ICommand ExitCommand { get; }

      public event EventHandler MinimizeReqested;
      protected virtual void OnMinimizeRequested( object sender, EventArgs e ) => MinimizeReqested?.Invoke( sender, e );

      public event EventHandler ExitRequested;
      protected virtual void OnExitRequested( object sender, EventArgs e ) => ExitRequested?.Invoke( sender, e );

      public MainViewModel( IAudioPlayer audioPlayer )
      {
         _audioPlayer = audioPlayer;

         PlayCommand = new RelayCommand( OnPlayCommand );
         MinimizeCommand = new RelayCommand( () => OnMinimizeRequested( this, EventArgs.Empty ) );
         ExitCommand = new RelayCommand( () => OnExitRequested( this, EventArgs.Empty ) );
      }

      private void OnPlayCommand()
      {
         _audioPlayer.Play();
      }
   }
}
