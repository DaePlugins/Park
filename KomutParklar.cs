using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;

namespace DaePark
{
    internal class KomutParklar : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "parklar";
        public string Help => "Parkları listeler.";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>{ "dae.park.parklar" };
        
        public void Execute(IRocketPlayer komutuÇalıştıran, string[] parametreler)
        {
            var parklar = Park.Örnek.Configuration.Instance.Parklar.Select(p => p.İsim).ToArray();
            if (parklar.Any())
            {
                UnturnedChat.Say(komutuÇalıştıran, Park.Örnek.Translate("Parklar", string.Join(", ", parklar)));
            }
        }
    }
}