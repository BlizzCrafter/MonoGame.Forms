namespace MonoGame.Forms.NET
{
    /// <summary>
    /// These are the states a <see cref="Controls.MonoGameControl"/> or a <see cref="Controls.InvalidationControl"/> can have.
    /// <remarks><see cref="Controls.InvalidationControl"/>'s don't have update events!</remarks>
    /// </summary>
    public enum ControlState
    {
        /// <summary>
        /// Triggers before the control starts a new update cycle.
        /// </summary>
        StartUpdate,
        /// <summary>
        /// Triggers after the control ends a complete update cycle.
        /// </summary>
        EndUpdate,
        /// <summary>
        /// Triggers before the control starts its regular update routine.
        /// </summary>
        BeforeUpdate,
        /// <summary>
        /// Triggers after the control ends its regular update routine.
        /// </summary>
        AfterUpdate,
        /// <summary>
        /// Triggers before the components of a control starting their regular update routine.
        /// </summary>
        BeforeComponentUpdate,
        /// <summary>
        /// Triggers after the components of a control ending their regular update routine.
        /// </summary>
        AfterComponentUpdate,

        /// <summary>
        /// Triggers before the control starts a new draw cycle.
        /// </summary>
        StartDraw,
        /// <summary>
        /// Triggers after the control ends a complete draw cycle.
        /// </summary>
        EndDraw,
        /// <summary>
        /// Triggers before the control starts its regular draw routine.
        /// </summary>
        BeforeDraw,
        /// <summary>
        /// Triggers after the control ends its regular draw routine.
        /// </summary>
        AfterDraw,
        /// <summary>
        /// Triggers before the components of a control starting their regular draw routine.
        /// </summary>
        BeforeComponentDraw,
        /// <summary>
        /// Triggers after the components of a control ending their regular draw routine.
        /// </summary>
        AfterComponentDraw
    }

    /// <summary>
    /// Gets delivered on <see cref="ControlState"/> events.
    /// </summary>
    public class ControlStateEventArgs : EventArgs
    {
        /// <summary>
        /// Get the current state of the control.
        /// </summary>
        public ControlState ControlsState;

        public ControlStateEventArgs(ControlState state)
        {
            ControlsState = state;
        }
    }
}
