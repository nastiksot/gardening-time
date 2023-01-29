using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State state;

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