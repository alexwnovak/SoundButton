using System;
using System.Windows.Automation;

namespace SoundButton.UITests.Helpers
{
   public class Property
   {
      private readonly Func<object, PropertyCondition> _propertyConditionCreator;

      public static Property ProcessId { get; } = new Property( obj => new PropertyCondition( AutomationElement.ProcessIdProperty, obj ) );
      public static Property AutomationId { get; } = new Property( obj => new PropertyCondition( AutomationElement.AutomationIdProperty, obj ) );

      private Property( Func<object, PropertyCondition> propertyConditionCreator )
      {
         _propertyConditionCreator = propertyConditionCreator;
      }

      internal PropertyCondition GetCondition( object obj ) => _propertyConditionCreator( obj );
   }
}
