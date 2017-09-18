using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using Xunit;
using FlaUI.Core.Input;
using Moq;
using Lodestone.Wpf;
using SoundButton.Controls;
using SoundButton.UITests.Helpers;

namespace SoundButton.UITests
{
   public class MenuButtonTests
   {
      [Fact]
      [Trait( "Category", "UI" )]
      public void HoversMouseOverControl_EntersMouseOverVisualState()
      {
         var visualStateManagerMock = new Mock<IVisualStateManager>();

         var controlBuilder = ControlBuilder.For( () => new MenuButton( visualStateManagerMock.Object ) )
            .With( b => AutomationProperties.SetAutomationId( b, "ai-Button" ) );

         var controlHost = ControlHost<MenuButton>.Launch( controlBuilder );

         int processId = Process.GetCurrentProcess().Id;
         var mainWindow = AutomationElement.RootElement.Find( Property.ProcessId, processId );
         var button = mainWindow.Find( Property.AutomationId, "ai-Button" );

         var rect = (Rect) button.GetCurrentPropertyValue( AutomationElement.BoundingRectangleProperty );
         Mouse.MoveTo( (int) rect.Left, (int) rect.Top );

         visualStateManagerMock.Verify( vsm => vsm.GoToState( controlHost.Control, "MouseOver", true ), Times.Once() );

         controlHost.Close();
      }
   }
}
