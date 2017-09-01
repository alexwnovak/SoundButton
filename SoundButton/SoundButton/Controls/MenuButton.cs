using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoundButton.Controls
{
   [TemplatePart( Name = "OuterBorder", Type = typeof( Border ))]
   [TemplateVisualState( Name = "Normal", GroupName = "CommonStates" )]
   [TemplateVisualState( Name = "Pressed", GroupName = "CommonStates" )]
   [TemplateVisualState( Name = "MouseOver", GroupName = "CommonStates" )]
   public class MenuButton : ContentControl
   {
      private Border _outerBorder;
      public Border OuterBorder
      {
         get => _outerBorder;
         set
         {
            if ( _outerBorder != null )
            {
               _outerBorder.MouseEnter -= OuterBorderMouseEnter;
               _outerBorder.MouseLeave -= OuterBorderMouseLeave;
               _outerBorder.MouseLeftButtonDown -= OuterBorderMouseLeftButtonDown;
               _outerBorder.MouseLeftButtonUp -= OuterBorderMouseLeftButtonUp;
            }

            _outerBorder = value;

            if ( _outerBorder != null )
            {
               _outerBorder.MouseEnter += OuterBorderMouseEnter;
               _outerBorder.MouseLeave += OuterBorderMouseLeave;
               _outerBorder.MouseLeftButtonDown += OuterBorderMouseLeftButtonDown;
               _outerBorder.MouseLeftButtonUp += OuterBorderMouseLeftButtonUp;
            }
         }
      }

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

      public override void OnApplyTemplate()
      {
         OuterBorder = GetTemplateChild( "OuterBorder" ) as Border;
      }

      private void OuterBorderMouseEnter( object sender, MouseEventArgs e )
      {
         VisualStateManager.GoToState( this, "MouseOver", true );
      }

      private void OuterBorderMouseLeave( object sender, MouseEventArgs e )
      {
         VisualStateManager.GoToState( this, "Normal", true );
      }

      private void OuterBorderMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
      {
         VisualStateManager.GoToState( this, "Pressed", true );
      }

      private void OuterBorderMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
      {
         VisualStateManager.GoToState( this, "MouseOver", true );
      }
   }
}
