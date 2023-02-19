using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class Vector3Data
    {
        [FormerlySerializedAs("X")]
        public float x;
        [FormerlySerializedAs("Y")]
        public float y;
        [FormerlySerializedAs("Z")]
        public float z;

        public Vector3Data(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}