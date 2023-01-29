using System;
using CodeBase.Inventory; 

namespace CodeBase.Data
{
    [Serializable]
    public class MugsData
    {
        public string guid; 
        public PlantData plantData;
        public MugsData() { }

        public MugsData(string guid, PlantData plantData)
        {
            this.guid = guid;
            this.plantData = plantData;
        }
    }
}