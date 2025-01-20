using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.Json;
using UFCServer.Server.Models;

namespace UFCServer.Utils
{
    public class Common
    {
        public static string GetLocalIp()
        {
            var hosts = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in hosts.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (ip.ToString().Contains("192."))
                    {
                        return ip.ToString();
                    };
                }
            }
            return null;
        }

        public static SettingsModel GetSettings()
        {
            string raw = File.ReadAllText("settings.json");

            SettingsModel settings = JsonSerializer.Deserialize<SettingsModel>(raw, options: new()
            {
                PropertyNameCaseInsensitive = true
            });

            return settings;
        }
    }
}
