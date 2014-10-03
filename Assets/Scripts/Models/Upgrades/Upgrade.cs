using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Upgrades
{
    [XmlInclude(typeof(JumpUpgrade))]
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
