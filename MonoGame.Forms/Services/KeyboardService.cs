using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame.Forms.Services
{
    internal static class KeyboardService
    {
        static Keys[] CurrentKeys = new Keys[0];
        static Dictionary<int, Keys[]> ArrayCache = new Dictionary<int, Keys[]>();

        internal static KeyboardState GetState()
        {
            return new KeyboardState(CurrentKeys);
        }

        internal static KeyboardState GetState(PlayerIndex playerIndex)
        {
            return new KeyboardState(CurrentKeys);
        }

        internal static void SetKeys(List<Keys> keys)
        {
            if (!ArrayCache.TryGetValue(keys.Count, out CurrentKeys))
            {
                CurrentKeys = new Keys[keys.Count];
                ArrayCache.Add(keys.Count, CurrentKeys);
            }

            keys.CopyTo(CurrentKeys);
        }
    }
}
