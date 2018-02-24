namespace myMD.Model.DependencyService
{
    public class XamarinDependencyService : IDependencyService
    {
        public T Get<T>() where T : class => Xamarin.Forms.DependencyService.Get<T>();
    }
}
