namespace Assets.Scripts.Models.Upgrades
{
    public class ArmorUpgrade : Upgrade
    {
        public ArmorUpgrade()
        {
            Name = "Armor";
            BasePrice = 500;
            Description = "Take more of a beating before dying.";
            MaxLevel = 5;
            Type = UpgradeType.Armor;
        }
    }
}
