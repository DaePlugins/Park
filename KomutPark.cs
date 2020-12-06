using System.Collections.Generic;
using System.Linq;
using DaePark.Modeller;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;

namespace DaePark
{
    internal class KomutPark : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "park";
        public string Help => "Parklar ile etkileşime geçer.";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>{ "dae.park.park" };
        
        public void Execute(IRocketPlayer komutuÇalıştıran, string[] parametreler)
        {
            if (parametreler.Length != 2)
            {
                UnturnedChat.Say(komutuÇalıştıran, Park.Örnek.Translate("HatalıParametre"), Color.red);
                return;
            }

            var oyuncu = (UnturnedPlayer)komutuÇalıştıran;

            var parametre = parametreler[0].ToLower();
            if (parametre == "1")
            {
                if (Park.Örnek.Configuration.Instance.Parklar.Exists(p => p.İsim == parametreler[1]))
                {
                    UnturnedChat.Say(komutuÇalıştıran, Park.Örnek.Translate("ParkZatenVar", parametreler[1]), Color.red);
                    return;
                }

                if (Park.Örnek.GeçiciKonumlar.ContainsKey(parametreler[1]))
                {
                    Park.Örnek.GeçiciKonumlar[parametreler[1]] = oyuncu.Position;
                }
                else
                {
                    Park.Örnek.GeçiciKonumlar.Add(parametreler[1], oyuncu.Position);
                }
            }
            else if (parametre == "2")
            {
                if (!Park.Örnek.GeçiciKonumlar.ContainsKey(parametreler[1]))
                {
                    UnturnedChat.Say(oyuncu, Park.Örnek.Translate("İlkKonumAyarlanmamış"), Color.red);
                    return;
                }

                var konum = Park.Örnek.GeçiciKonumlar[parametreler[1]];

                if (Park.Örnek.Configuration.Instance.Parklar.Any(p => p.İçeriyor(konum)))
                {
                    UnturnedChat.Say(oyuncu, Park.Örnek.Translate("BurasıZatenPark"), Color.red);
                    return;
                }

                Park.Örnek.Configuration.Instance.Parklar.Add(new ParkAlanı
                (
                    parametreler[1],
                    konum.x,
                    konum.z,
                    oyuncu.Position.x,
                    oyuncu.Position.z
                ));
                Park.Örnek.Configuration.Save();

                Park.Örnek.GeçiciKonumlar.Remove(parametreler[1]);
            }
            else if (parametre == "k")
            {
                if (Park.Örnek.GeçiciKonumlar.ContainsKey(parametreler[1]))
                {
                    Park.Örnek.GeçiciKonumlar.Remove(parametreler[1]);
                    return;
                }

                var silinecekPark = Park.Örnek.Configuration.Instance.Parklar.FirstOrDefault(p => p.İsim == parametreler[1]);
                if (silinecekPark == null)
                {
                    UnturnedChat.Say(komutuÇalıştıran, Park.Örnek.Translate("ParkBulunamadı", parametreler[1]), Color.red);
                    return;
                }

                Park.Örnek.Configuration.Instance.Parklar.Remove(silinecekPark);
                Park.Örnek.Configuration.Save();
            }
            else
            {
                UnturnedChat.Say(komutuÇalıştıran, Park.Örnek.Translate("HatalıParametre"), Color.red);
            }
        }
    }
}