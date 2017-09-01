using System.Windows;
using System.Windows.Controls;

namespace SoundButton.Controls
{
   public class MenuButton : ContentControl
   {
      public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register( nameof( CornerRadius ),
         typeof( CornerRadius ),
         typeof( MenuButton ),
         new PropertyMetadata( new CornerRadius( 0 ) ) );

      public CornerRadius CornerRadius
      {
         get => (CornerRadius) GetValue( CornerRadiusProperty );
         set => SetValue( CornerRadiusProperty, value );
      }

      static MenuButton()
      {
         DefaultStyleKeyProperty.OverrideMetadata( typeof( MenuButton ), new FrameworkPropertyMetadata( typeof( MenuButton ) ) );
      }
   }
}
