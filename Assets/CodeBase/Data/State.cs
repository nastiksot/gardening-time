using System;
using System.Collections.Generic;
using CodeBase.Inventory;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public int coins;
        public List<Plant> inventoryPlants;
    }
}