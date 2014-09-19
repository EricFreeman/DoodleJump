namespace Assets.Scripts.Models.Upgrades
{
    public class RocketUpgrade : Upgrade
    {
        public RocketUpgrade()
        {
            Name = "Rockets";
            BasePrice = 250;
            Description = "Delay falling to your death by at least one jump!";
            MaxLevel = 5;
            Type = UpgradeType.Rocket;
        }
    }
}
