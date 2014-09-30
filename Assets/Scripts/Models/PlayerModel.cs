using System.Collections.Generic;
using Assets.Scripts.Models.Upgrades;

namespace Assets.Scripts.Models
{
    public class PlayerModel
    {
        public float Money;
        public List<UpgradeLevel> UpgradeLevels;
        public bool IsMusicEnabled;
        public bool IsSoundEnabled;

        public PlayerModel() { }
    }

    public class UpgradeLevel
    {
        public Upgrade Upgrade;
        public int Level;

        public UpgradeLevel(Upgrade u, int l)
        {
            Upgrade = u;
            Level = l;
        }

        public UpgradeLevel() { }
    }
}