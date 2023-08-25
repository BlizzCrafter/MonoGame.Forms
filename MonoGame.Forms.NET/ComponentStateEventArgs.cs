namespace MonoGame.Forms.NET
{
    /// <summary>
    /// These are the states <see cref="Microsoft.Xna.Framework.IUpdateable"/> and <see cref="Microsoft.Xna.Framework.IDrawable"/> components can have.
    /// </summary>
    public enum ComponentState
    {
        /// <summary>
        /// Triggers before the component starts its regular update routine.
        /// </summary>
        BeforeUpdate,
        /// <summary>
        /// Triggers after the component ends its regular update routine.
        /// </summary>
        AfterUpdate,
        /// <summary>
        /// Triggers before the component starts its regular draw routine.
        /// </summary>
        BeforeDraw,
        /// <summary>
        /// Triggers after the component ends its regular draw routine.
        /// </summary>
        AfterDraw
    }

    /// <summary>
    /// Gets delivered on <see cref="ComponentState"/> events.
    /// </summary>
    public class ComponentStateEventArgs : EventArgs
    {
        /// <summary>
        /// Get the current state of the component.
        /// </summary>
        public ComponentState ComponentState;

        public ComponentStateEventArgs(ComponentState componentState)
        {
            ComponentState = componentState;
        }
    }
}
