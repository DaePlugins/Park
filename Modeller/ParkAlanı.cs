using System.Xml.Serialization;
using UnityEngine;

namespace DaePark.Modeller
{
    [XmlType("Park")]
    public class ParkAlanı
    {
        [XmlAttribute]
        public string İsim { get; set; }

        [XmlAttribute]
        public float X1 { get; set; }
        [XmlAttribute]
        public float Z1 { get; set; }
        [XmlAttribute]
        public float X2 { get; set; }
        [XmlAttribute]
        public float Z2 { get; set; }

        public ParkAlanı()
        {
        }

        public ParkAlanı(string isim, float x1, float z1, float x2, float z2)
        {
            İsim = isim;

            X1 = x1;
            Z1 = z1;
            X2 = x2;
            Z2 = z2;
        }

        public bool İçeriyor(Vector3 konum) => X1 <= konum.x && konum.x < X2
                                                             && Z1 <= konum.z && konum.z < Z2;
    }
}