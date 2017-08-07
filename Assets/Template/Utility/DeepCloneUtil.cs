using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Template.Utility
{
    public class DeepCloneUtil<T>
    {
        #pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        public static T DeepClone<T>(T obj)
        #pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
