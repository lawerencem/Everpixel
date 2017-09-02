namespace Assets.Template.Other
{
    abstract public class ASingleton<T> where T : ASingleton<T>, new()
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
