namespace Assets.Scripts.Models.Upgrades
{
    public class MoneyUpgrade : Upgrade
    {
        public MoneyUpgrade()
        {
            Name = "Investments";
            BasePrice = 10;
            Description = "Earn more money";
            MaxLevel = 5;
        }
    }
}
