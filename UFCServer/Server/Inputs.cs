using vJoy.Wrapper;
using WindowsInput.Events;

namespace UFCServer.Data
{
    public class Inputs
    {
        private static VirtualJoystick joystickInstance = new(1);

        #region Keyboard actions
        public static void PressKeyKeyboard(string key, string modifier)
        {
            bool extended = key.Contains('#');
            KeyCode keyCode = GetKeyCode(key.Replace("#", ""));

            if (modifier != "")
            {
                KeyCode modifierCode = GetKeyCode(modifier.Replace("#", ""));
                WindowsInput.Simulate.Events().Hold(modifierCode).Invoke();
            }

            WindowsInput.Simulate.Events().Hold(keyCode, extended ? true : null).Invoke();
        }

        public static void ReleaseKeyKeyboard(string key, string modifier)
        {
            bool extended = key.Contains('#');
            KeyCode keyCode = GetKeyCode(key.Replace("#", ""));

            if (modifier != "")
            {
                KeyCode modifierCode = GetKeyCode(modifier.Replace("#", ""));
                WindowsInput.Simulate.Events().Release(modifierCode).Invoke();
            }

            WindowsInput.Simulate.Events().Release(keyCode, extended ? true : null).Invoke();
        }

        #endregion

        #region vJoy actions

        public static void PressKeyVjoy(uint vjoyKey)
        {
            VirtualJoystick joystick = joystickInstance;
            joystick.SetJoystickButton(true, vjoyKey);
        }

        public static void ReleaseKeyVjoy(uint vjoyKey)
        {
            //A small delay is required otherwise vJoy will not register the input on quick presses.
            Task.Delay(30).ContinueWith(t =>
            {
                VirtualJoystick joystick = joystickInstance;
                joystick.SetJoystickButton(false, vjoyKey);
            });
        }

        #endregion

        private static KeyCode GetKeyCode(string key)
        {
            var value = Convert.ToInt32(key, 16);
            KeyCode keyCode = (KeyCode)value;
            return keyCode;
        }
    }
}
