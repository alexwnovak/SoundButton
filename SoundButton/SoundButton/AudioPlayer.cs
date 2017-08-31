using System;
using System.Windows.Media;

namespace Soundboard
{
   public class AudioPlayer : IAudioPlayer
   {
      private MediaPlayer _mediaPlayer;

      public void Initialize()
      {
         _mediaPlayer = new MediaPlayer();
         _mediaPlayer.Open( new Uri( "Resources/Airhorn.wav", UriKind.Relative ) );
      }

      public void Play()
      {
         _mediaPlayer.Stop();
         _mediaPlayer.Play();
      }
   }
}
