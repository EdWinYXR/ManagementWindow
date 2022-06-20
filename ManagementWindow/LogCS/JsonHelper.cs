using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace BaseClass.Tool
{
    public static class JsonHelper<T>
    {
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();
        public static string GetJsonStr(T objectList)
        {
            return serializer.Serialize(objectList);
        }
        public static List<T> GetObjectList<T>(string jsonStr)
        {
            List<T> objs = serializer.Deserialize<List<T>>(jsonStr);
            return objs;
        }
        public static T GetObj(string jsonStr)
        {
            return serializer.Deserialize<T>(jsonStr);
        }
    }
}
