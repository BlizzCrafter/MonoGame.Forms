namespace MonoGame.Forms
{
    /// <summary>
    /// Use this class to set-up MonoGame.Forms!
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Show or hide the 'FPS' (frames per second) of the corresponding control / window.
        /// </summary>
        public static bool ShowFPS
        {
            get { return _ShowFPS; }
            set { _ShowFPS = value; }
        }
        private static bool _ShowFPS = true;

        /// <summary>
        /// Show or hide the 'cursor position' of the corresponding control / window.
        /// </summary>
        public static bool ShowCursorPosition
        {
            get { return _ShowCursorPosition; }
            set { _ShowCursorPosition = value; }
        }
        private static bool _ShowCursorPosition = true;

        /// <summary>
        /// Show or hide the 'cam position' of the corresponding control / window.
        /// </summary>
        public static bool ShowCamPosition
        {
            get { return _ShowCamPosition; }
            set { _ShowCamPosition = value; }
        }
        private static bool _ShowCamPosition = false;
    }
}
