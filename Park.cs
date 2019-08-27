using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DaePark
{
	public class Park : RocketPlugin<ParkYapılandırma>
	{
	    public static Park Örnek { get; private set; }

        public Dictionary<string, Vector3> GeçiciKonumlar { get; } = new Dictionary<string, Vector3>();

        protected override void Load()
		{
		    Örnek = this;
			
			VehicleManager.onDamageVehicleRequested += AracaHasarVerilmekİstendiğinde;
			VehicleManager.onDamageTireRequested += TekereHasarVerilmekİstendiğinde;
		}

	    protected override void Unload()
		{
			Örnek = null;

            GeçiciKonumlar.Clear();

			VehicleManager.onDamageVehicleRequested -= AracaHasarVerilmekİstendiğinde;
			VehicleManager.onDamageTireRequested -= TekereHasarVerilmekİstendiğinde;
		}
        
        private void AracaHasarVerilmekİstendiğinde(CSteamID hasarıVeren, InteractableVehicle araç, ref ushort hasar, ref bool tamirEdebilir, ref bool hasarVerebilir, EDamageOrigin kaynak)
		{
			if (hasarıVeren == CSteamID.Nil || araç == null)
			{
				return;
			}

            var oyuncu = UnturnedPlayer.FromCSteamID(hasarıVeren);
		    if (oyuncu.HasPermission($"dae.park.{Configuration.Instance.HasarVermeYetkisi}"))
		    {
		        return;
		    }

            if (Configuration.Instance.Parklar.Any(p => p.İçeriyor(araç.transform.position)))
			{
				hasarVerebilir = false;
			}
		}
        
        private void TekereHasarVerilmekİstendiğinde(CSteamID hasarıVeren, InteractableVehicle araç, int tekerSırası, ref bool hasarVerebilir, EDamageOrigin kaynak)
		{
			if (hasarıVeren == CSteamID.Nil || araç == null)
			{
				return;
			}

            var oyuncu = UnturnedPlayer.FromCSteamID(hasarıVeren);
		    if (oyuncu.HasPermission($"dae.park.{Configuration.Instance.HasarVermeYetkisi}"))
		    {
		        return;
		    }

            if (Configuration.Instance.Parklar.Any(p => p.İçeriyor(araç.transform.position)))
			{
				hasarVerebilir = false;
			}
		}

        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "HatalıParametre", "Hatalı parametre." },
            { "Parklar", "Parklar: {0}" },
            { "ParkZatenVar", "{0} isimli park zaten var." },
            { "İlkKonumAyarlanmamış", "Parkın ilk konumu ayarlanmamış." },
            { "BurasıZatenPark", "Park alanı içerisinde başka bir park alanı oluşturamazsın." },
            { "ParkBulunamadı", "{0} isimli park bulunamadı." }
        };
    }
}