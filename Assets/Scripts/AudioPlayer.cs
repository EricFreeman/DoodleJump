using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : MonoBehaviour,
        IListener<PlaySoundMessage>
    {
        private bool _canPlaySounds;

        void Start()
        {
            this.Register<PlaySoundMessage>();

            var p = PlayerManager.Load();
            _canPlaySounds = p.IsSoundEnabled;
        }

        void OnDestroy()
        {
            this.UnRegister<PlaySoundMessage>();
        }

        public void Handle(PlaySoundMessage message)
        {
            if (_canPlaySounds)
            {
                audio.clip = message.Clip;
                audio.Play();
            }
        }
    }
}