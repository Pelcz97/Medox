namespace myMD.Model.DependencyService
{
    public static class DependencyServiceWrapper
    {
        public static IDependencyService Service { private get;  set; }

        public static T Get<T>() where T : class => Service.Get<T>();
    }
}
