using System.Collections.Generic;

namespace UnnamedGame.Pause
{
    public class Pauser : IPauseHandler
    {
        private readonly List<IPauseHandler> handlers = new List<IPauseHandler>();
        
        public bool IsPaused { get; private set; }

        public void Register(IPauseHandler handler)
        {
            handlers.Add(handler);
        }

        public void UnRegister(IPauseHandler handler)
        {
            handlers.Remove(handler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var handler in handlers)
            {
                handler.SetPaused(isPaused);
            }
        }
    }
}