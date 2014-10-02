namespace Assets.Scripts.Events.Messages
{
    public class ShowPanelMessage
    {
        public PanelType Type;
    }

    public enum PanelType
    {
        MainMenu,
        Options,
        Controls,
        Credits
    }
}