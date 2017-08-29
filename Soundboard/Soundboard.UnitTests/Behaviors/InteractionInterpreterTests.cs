﻿using FluentAssertions;
using Soundboard.Behaviors;
using Xunit;

namespace Soundboard.UnitTests.Behaviors
{
   public class InteractionInterpreterTests
   {
      [Fact]
      public void LeftMouseUp_LeftMouseButtonWasNotDown_DoesNotRaiseClickEvent()
      {
         var interactionInterpreter = new InteractionInterpreter();

         interactionInterpreter.MonitorEvents();

         interactionInterpreter.LeftMouseUp();

         interactionInterpreter.ShouldNotRaise( nameof( interactionInterpreter.LeftClick ) );
      }

      [Fact]
      public void LeftMouseUp_LeftMouseButtonWasDown_RaisesClickEvent()
      {
         var interactionInterpreter = new InteractionInterpreter();

         interactionInterpreter.MonitorEvents();

         interactionInterpreter.LeftMouseDown();
         interactionInterpreter.LeftMouseUp();

         interactionInterpreter.ShouldRaise( nameof( interactionInterpreter.LeftClick ) );
      }

      [Fact]
      public void MouseMove_LeftMouseButtonWasNotDown_DoesNotRaiseDragEvent()
      {
         var interactionInterpreter = new InteractionInterpreter();

         interactionInterpreter.MonitorEvents();

         interactionInterpreter.MouseMove( 1, 0, false );

         interactionInterpreter.ShouldNotRaise( nameof( interactionInterpreter.LeftDrag ) );
      }

      [Fact]
      public void MouseMove_LeftMouseButtonWasDown_RaisesDragEvent()
      {
         var interactionInterpreter = new InteractionInterpreter();

         interactionInterpreter.MonitorEvents();

         interactionInterpreter.MouseMove( 1, 0, true );

         interactionInterpreter.ShouldRaise( nameof( interactionInterpreter.LeftDrag ) );
      }
   }
}
