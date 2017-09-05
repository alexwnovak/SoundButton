using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SoundButton.Automation;

namespace SoundButton.Controls
{
   [TemplatePart( Name = "OuterBorder", Type = typeof( Border ) )]
   [TemplateVisualState( Name = "Normal", GroupName = "CommonStates" )]
   [TemplateVisualState( Name = "Pressed", GroupName = "CommonStates" )]
   [TemplateVisualState( Name = "MouseOver", GroupName = "CommonStates" )]
   public class MenuButton : ContentControl
   {
      private readonly DispatcherTimer _longPressDispatcherTimer = new DispatcherTimer( DispatcherPriority.Input );
      private bool _hasLongPressed;

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
               _outerBorder.MouseRightButtonDown -= OuterBorderMouseRightButtonDown;
               _outerBorder.MouseRightButtonUp -= OuterBorderMouseRightButtonUp;
            }

            _outerBorder = value;

            if ( _outerBorder != null )
            {
               _outerBorder.MouseEnter += OuterBorderMouseEnter;
               _outerBorder.MouseLeave += OuterBorderMouseLeave;
               _outerBorder.MouseLeftButtonDown += OuterBorderMouseLeftButtonDown;
               _outerBorder.MouseLeftButtonUp += OuterBorderMouseLeftButtonUp;
               _outerBorder.MouseRightButtonDown += OuterBorderMouseRightButtonDown;
               _outerBorder.MouseRightButtonUp += OuterBorderMouseRightButtonUp;

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

      public static DependencyProperty LongPressIntervalProperty = DependencyProperty.Register( nameof( LongPressInterval ),
         typeof( TimeSpan ),
         typeof( MenuButton ),
         new PropertyMetadata( TimeSpan.FromMilliseconds( 500 ) ) );

      public TimeSpan LongPressInterval
      {
         get => (TimeSpan) GetValue( LongPressIntervalProperty );
         set => SetValue( LongPressIntervalProperty, value );
      }

      public static readonly RoutedEvent LeftClickEvent = EventManager.RegisterRoutedEvent( nameof( LeftClick ),
         RoutingStrategy.Bubble,
         typeof( RoutedEventHandler ),
         typeof( MenuButton ) );

      public event RoutedEventHandler LeftClick
      {
         add => AddHandler( LeftClickEvent, value );
         remove => RemoveHandler( LeftClickEvent, value );
      }

      public static readonly RoutedEvent RightClickEvent = EventManager.RegisterRoutedEvent( nameof( RightClick ),
         RoutingStrategy.Bubble,
         typeof( RoutedEventHandler ),
         typeof( MenuButton ) );

      public event RoutedEventHandler RightClick
      {
         add => AddHandler( RightClickEvent, value );
         remove => RemoveHandler( RightClickEvent, value );
      }

      public static readonly RoutedEvent LongPressEvent = EventManager.RegisterRoutedEvent( nameof( LongPress ),
         RoutingStrategy.Bubble,
         typeof( RoutedEventHandler ),
         typeof( MenuButton ) );

      public event RoutedEventHandler LongPress
      {
         add => AddHandler( LongPressEvent, value );
         remove => RemoveHandler( LongPressEvent, value );
      }

      static MenuButton()
      {
         DefaultStyleKeyProperty.OverrideMetadata( typeof( MenuButton ), new FrameworkPropertyMetadata( typeof( MenuButton ) ) );
      }

      public MenuButton()
      {
         _longPressDispatcherTimer.Interval = LongPressInterval;
         _longPressDispatcherTimer.Tick += ( _, __ ) => LongPressDispatcherTimerTick();
      }

      protected override AutomationPeer OnCreateAutomationPeer()
      {
         return new MenuButtonAutomationPeer( this );
      }

      private void LongPressDispatcherTimerTick()
      {
         _longPressDispatcherTimer.Stop();
         _hasLongPressed = true;

         RaiseLongPressEvent();
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
         _longPressDispatcherTimer.Stop();
         _hasLongPressed = false;

         VisualStateManager.GoToState( this, "Normal", true );
      }

      private void OuterBorderMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
      {
         _longPressDispatcherTimer.Start();
         VisualStateManager.GoToState( this, "Pressed", true );
      }

      private void OuterBorderMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
      {
         _longPressDispatcherTimer.Stop();
         VisualStateManager.GoToState( this, "MouseOver", true );

         if ( !_hasLongPressed )
         {
            RaiseLeftClickEvent();
         }

         _hasLongPressed = false;
      }

      private void OuterBorderMouseRightButtonDown( object sender, MouseEventArgs e )
      {
         VisualStateManager.GoToState( this, "Pressed", true );
      }

      private void OuterBorderMouseRightButtonUp( object sender, MouseEventArgs e )
      {
         RaiseRightClickEvent();
         VisualStateManager.GoToState( this, "MouseOver", true );
      }

      internal void RaiseLeftClickEvent() => RaiseEvent( new RoutedEventArgs( LeftClickEvent ) );
      internal void RaiseRightClickEvent() => RaiseEvent( new RoutedEventArgs( RightClickEvent ) );
      internal void RaiseLongPressEvent() => RaiseEvent( new RoutedEventArgs( LongPressEvent ) );
   }
}
