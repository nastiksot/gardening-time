using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtension
    {
        public static Vector3Data AsVectorData(this Vector3 vector3)
        {
            return new Vector3Data(vector3.x, vector3.y, vector3.z);
        }

        public static Vector3 AsUnityVector(this Vector3Data vector3Data)
        {
            return new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
        }

        public static Vector3 AddY(this Vector3 vector3, float y)
        {
            vector3.y += y;
            return vector3;
        }

        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string Serialize<T>(this T model)
        {
            return JsonUtility.ToJson(model);
        }
    }
}
