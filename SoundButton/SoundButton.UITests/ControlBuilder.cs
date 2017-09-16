using System;
using System.Collections.Generic;
using System.Windows;

namespace SoundButton.UITests
{
   public class ControlBuilder<T> where T: FrameworkElement, new()
   {
      private readonly List<Action<T>> _objectModifiers = new List<Action<T>>();

      internal ControlBuilder()
      {
      }

      public ControlBuilder<T> With( Action<T> controlModifier )
      {
         _objectModifiers.Add( controlModifier );
         return this;
      }

      internal T Build()
      {
         var control = new T();

         foreach ( var objectModifier in _objectModifiers )
         {
            objectModifier( control );
         }

         return control;
      }
   }
}
