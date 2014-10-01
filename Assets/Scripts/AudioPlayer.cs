using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : MonoBehaviour,
        IListener<PlaySoundMessage>
    {
        void Start()
        {
            this.Register<PlaySoundMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PlaySoundMessage>();
        }

        public void Handle(PlaySoundMessage message)
        {
            audio.clip = message.Clip;
            audio.Play();
        }
    }
}