namespace myMD.Model.DependencyService
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}
