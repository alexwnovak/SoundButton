using System;
using System.Collections.Generic;
using System.Windows;

namespace Lodestone.Wpf
{
   public static class ControlBuilder
   {
      public static ControlBuilder<T> For<T>() where T : FrameworkElement, new()
      {
         return new ControlBuilder<T>();
      }

      public static ControlBuilder<T> For<T>( Func<T> creator ) where T: FrameworkElement, new()
      {
         return new ControlBuilder<T>( creator );
      }
   }

   public class ControlBuilder<T> where T: FrameworkElement, new()
   {
      private readonly List<Action<T>> _objectModifiers = new List<Action<T>>();
      private readonly Func<T> _creator;

      internal ControlBuilder()
         : this( () => new T() )
      {
      }

      internal ControlBuilder( Func<T> creator )
      {
         _creator = creator;
      }

      public ControlBuilder<T> With( Action<T> controlModifier )
      {
         _objectModifiers.Add( controlModifier );
         return this;
      }

      internal T Build()
      {
         var control = _creator();

         foreach ( var objectModifier in _objectModifiers )
         {
            objectModifier( control );
         }

         return control;
      }
   }
}
