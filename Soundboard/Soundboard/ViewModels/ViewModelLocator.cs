using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Soundboard.ViewModels
{
   public class ViewModelLocator
   {
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default );
         SimpleIoc.Default.Register<MainViewModel>();
      }

      public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
   }
}