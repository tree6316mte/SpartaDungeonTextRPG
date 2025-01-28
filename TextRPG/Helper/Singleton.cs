namespace TextRPG.Helper
{
    public class Singleton<T> where T : class, new()
    {
        private static T? _instance;

        // Singleton 인스턴스에 접근하는 속성
        public static T instance
        {
            get
            {
                // Double-Check Locking
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}