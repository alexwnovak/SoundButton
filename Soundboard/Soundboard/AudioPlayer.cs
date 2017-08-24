using System;
using System.Windows.Media;

namespace Soundboard
{
   public class AudioPlayer : IAudioPlayer
   {
      private MediaPlayer _mediaPlayer;
      private bool _isPlaying;

      public void Initialize()
      {
         _mediaPlayer = new MediaPlayer();
         _mediaPlayer.Open( new Uri( "Resources/Airhorn.wav", UriKind.Relative ) );
         _mediaPlayer.MediaEnded += ( _, __ ) => _isPlaying = false;
      }

      public void Play()
      {
         if ( _isPlaying )
         {
            _mediaPlayer.Stop();
            _mediaPlayer.Play();
         }
         else
         {
            _mediaPlayer.Play();
            _isPlaying = true;
         }
      }
   }
}
