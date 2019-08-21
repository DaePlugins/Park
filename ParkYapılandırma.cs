using System.Collections.Generic;
using DaePark.Modeller;
using Rocket.API;

namespace DaePark
{
	public class ParkYapılandırma : IRocketPluginConfiguration
	{
	    public string HasarVermeYetkisi { get; set; }

	    public List<ParkAlanı> Parklar { get; set; } = new List<ParkAlanı>();

		public void LoadDefaults()
		{
		    HasarVermeYetkisi = "HasarVerebilir";

		    Parklar = new List<ParkAlanı>();
		}
	}
}