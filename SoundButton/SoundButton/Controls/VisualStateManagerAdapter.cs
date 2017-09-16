using System.Windows;

namespace SoundButton.Controls
{
   public class VisualStateManagerAdapter : IVisualStateManager
   {
      public bool GoToState( FrameworkElement control, string stateName, bool useTransitions )
      {
         return VisualStateManager.GoToState( control, stateName, useTransitions );
      }
   }
}
