using System;
using Xunit;
using SoundButton.UITests.Helpers;

namespace SoundButton.UITests
{
   public class ApplicationTests : IDisposable
   {
      private readonly Application _application;

      public ApplicationTests()
      {
         _application = Application.Launch( @"..\..\..\SoundButton\bin\Debug\SoundButton.exe" );
      }

      public void Dispose()
      {
         _application.Dispose();
      }

      [Fact]
      public void AppLaunches_ExitButtonIsClicked_AppExits()
      {
         var playButton = _application.MainWindow.Find( Property.AutomationId, "ai-PlayButton" );
         playButton.Press( TimeSpan.FromSeconds( 1 ) );

         var exitButton = _application.MainWindow.Find( Property.AutomationId, "ai-ExitButton" );
         exitButton.Click();
      }
   }
}
