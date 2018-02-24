using Xamarin.Forms.Internals;

namespace myMD.Model.DependencyService
{
    [Preserve(AllMembers = true)]
    public class XamarinDependencyService : IDependencyService
    {
        public T Get<T>() where T : class => Xamarin.Forms.DependencyService.Get<T>();
    }
}
