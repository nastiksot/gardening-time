using System;

namespace CodeBase.Inventory
{
    [Serializable]
    public class PlantData
    {
        public PlantType type;
        public int count;

        public PlantData()
        {
        }

        public PlantData(PlantType type, int count)
        {
            this.count = count;
            this.type = type;
        }
    }
}