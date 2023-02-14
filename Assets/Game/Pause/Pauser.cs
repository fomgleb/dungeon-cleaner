using System.Collections.Generic;

namespace Game.Pause
{
    public static class Pauser
    {
        private static readonly List<IPauseHandler> Handlers = new();
        
        public static bool IsPaused { get; private set; }

        public static void Register(IPauseHandler handler)
        {
            Handlers.Add(handler);
        }

        public static void UnRegister(IPauseHandler handler)
        {
            Handlers.Remove(handler);
        }

        public static void ClearRegisteredHandlers()
        {
            Handlers.Clear();
        }

        public static void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var handler in Handlers)
            {
                handler.SetPaused(isPaused);
            }
        }
    }
}
