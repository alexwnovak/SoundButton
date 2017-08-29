using System.Windows;
using System.Windows.Interactivity;

namespace Soundboard.Behaviors
{
   public class MouseInteractionBehavior : Behavior<Window>
   {
      private readonly InteractionInterpreter _interactionInterpreter = new InteractionInterpreter();
   }
}
