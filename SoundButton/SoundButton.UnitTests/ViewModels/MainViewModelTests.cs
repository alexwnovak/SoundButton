using Moq;
using SoundButton.ViewModels;
using Xunit;

namespace SoundButton.UnitTests.ViewModels
{
   public class MainViewModelTests
   {
      [Fact]
      public void PlayCommand_HasAudio_PlaysAudio()
      {
         var audioPlayerMock = new Mock<IAudioPlayer>();

         var mainViewModel = new MainViewModel( audioPlayerMock.Object );
         mainViewModel.PlayCommand.Execute( null );

         audioPlayerMock.Verify( ap => ap.Play(), Times.Once() );
      }
   }
}
