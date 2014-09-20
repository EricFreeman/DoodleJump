using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Upgrades
{
    [XmlInclude(typeof(JumpUpgrade))]
    [XmlInclude(typeof(MoneyUpgrade))]
    [XmlInclude(typeof(ArmorUpgrade))]
    [XmlInclude(typeof(ParachuteUpgrade))]
    [XmlInclude(typeof(RocketUpgrade))]
    public class Upgrade
    {
        public string Name;
        public float BasePrice;
        public string Description;
        public int MaxLevel;

        public virtual float LevelPrice(int level)
        {
            return (float)Math.Pow(10, level) * BasePrice;
        }
    }
}
