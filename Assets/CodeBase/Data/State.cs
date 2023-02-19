using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public int coins;
        public List<PlantData> inventoryPlants = new List<PlantData>();

        public State()
        {
        }

        public State(int coins)
        {
            this.coins = coins;
        }
    }
}