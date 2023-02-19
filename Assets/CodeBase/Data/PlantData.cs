using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlantData
    {
        public PlantType type;
        public int count;

        public PlantData() { }

        public PlantData(PlantType type, int count)
        {
            this.count = count;
            this.type = type;
        }

        public PlantData(int type, int count)
        {
            this.count = count;
            this.type = (PlantType)type;
        }
    }
}
