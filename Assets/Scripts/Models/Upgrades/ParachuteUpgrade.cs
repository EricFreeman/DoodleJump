namespace Assets.Scripts.Models.Upgrades
{
    public class ParachuteUpgrade : Upgrade
    {
        public ParachuteUpgrade()
        {
            Name = "Parachutes";
            BasePrice = 250;
            Description = "Deploy a parachute to fall slower.";
            MaxLevel = 5;
            Type = UpgradeType.Parachute;
        }
    }
}
