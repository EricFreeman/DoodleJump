namespace Assets.Scripts.Models.Upgrades
{
    public class JumpUpgrade : Upgrade
    {
        public JumpUpgrade()
        {
            Name = "Super Jump";
            BasePrice = 10;
            Description = "Have an easier time manuvering the platforms with an extra bounce in your step.";
            MaxLevel = 10;
        }
    }
}
