using System;
using System.Collections.Generic;
using CodeBase.Inventory;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public int coins;
        public List<Plant> inventoryPlants = new List<Plant>();

        public State()
        {
        }

        public State(int coins)
        {
            this.coins = coins;
        }
    }
}