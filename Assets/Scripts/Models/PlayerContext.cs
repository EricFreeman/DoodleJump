using System.Collections.Generic;
using Assets.Scripts.Models.Upgrades;

namespace Assets.Scripts.Models
{
    public static class PlayerContext
    {
        public static Dictionary<UpgradeType, int> UpgradeLevel;

        public static void Setup(List<UpgradeLevel> l)
        {
            UpgradeLevel = new Dictionary<UpgradeType, int>();
            l.ForEach(x =>
            {
                UpgradeLevel[x.Upgrade.Type] = x.Level;
            });
        }

        public static int Get(UpgradeType u)
        {
            if (!UpgradeLevel.ContainsKey(u)) return 0;
            return UpgradeLevel[u];
        }
    }
}
