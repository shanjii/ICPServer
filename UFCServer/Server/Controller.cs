using Microsoft.AspNetCore.Mvc;
using UFCServer.Server.Models;

namespace UFCServer.Data
{
    public class Controller
    {

        [HttpPost]
        [Route("/press-keyboard")]
        public void PressKeyboard([FromBody] ActionModel payload)
        {
            try
            {
                if (payload.Key == "") return;
                Inputs.PressKeyKeyboard(payload.Key, payload.Modifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        [HttpPost]
        [Route("/release-keyboard")]
        public void ReleaseKeyboard([FromBody] ActionModel payload)
        {
            try
            {
                if (payload.Key == "") return;
                Inputs.ReleaseKeyKeyboard(payload.Key, payload.Modifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        [HttpPost]
        [Route("/press-vjoy")]
        public void PressVjoy([FromBody] ActionModel payload)
        {
            try
            {
                if (payload.JoyKey == null) return;
                Inputs.PressKeyVjoy((uint)payload.JoyKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        [HttpPost]
        [Route("/release-vjoy")]
        public void ReleaseVjoy([FromBody] ActionModel payload)
        {
            try
            {
                if (payload.JoyKey == null) return;
                Inputs.ReleaseKeyVjoy((uint)payload.JoyKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
    }
}
