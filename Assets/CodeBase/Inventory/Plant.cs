using System;

namespace CodeBase.Inventory
{
    [Serializable]
    public class Plant
    {
        public PlantType type;
        public int count;

        public Plant()
        {
        }

        public Plant(int count, PlantType type)
        {
            this.count = count;
            this.type = type;
        }
    }
}