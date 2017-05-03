namespace Generics
{
    abstract public class AbstractSingleton<T>
        where T : AbstractSingleton<T>, new()
    {
        private static T _instance = new T();
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
