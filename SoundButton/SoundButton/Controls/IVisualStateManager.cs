using System.Windows;

namespace SoundButton.Controls
{
   public interface IVisualStateManager
   {
      bool GoToState( FrameworkElement control, string stateName, bool useTransitions );
   }
}
