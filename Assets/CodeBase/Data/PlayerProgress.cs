using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State state;
        public List<MugsData> mugsData = new List<MugsData>();
        public PlayerProgress()
        {
            state = new State();
        }

        public PlayerProgress(State state) : this()
        {
            this.state = state;
        }
    }
}