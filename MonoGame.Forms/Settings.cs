namespace MonoGame.Forms
{
    public static class Settings
    {
        public static bool ShowFPS
        {
            get { return _ShowFPS; }
            set { _ShowFPS = value; }
        }
        private static bool _ShowFPS = true;

        public static bool ShowCursorPosition
        {
            get { return _ShowCursorPosition; }
            set { _ShowCursorPosition = value; }
        }
        private static bool _ShowCursorPosition = true;

        public static bool ShowCamPosition
        {
            get { return _ShowCamPosition; }
            set { _ShowCamPosition = value; }
        }
        private static bool _ShowCamPosition = true;
    }
}
