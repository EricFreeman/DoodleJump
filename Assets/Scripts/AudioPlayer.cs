using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : MonoBehaviour,
        IListener<PlaySoundMessage>,
        IListener<UpdateAudioSettingsMessage>
    {
        private bool _canPlaySounds;

        void Start()
        {
            this.Register<PlaySoundMessage>();
            this.Register<UpdateAudioSettingsMessage>();

            var p = PlayerManager.Load();
            _canPlaySounds = p.IsSoundEnabled;
        }

        void OnDestroy()
        {
            this.UnRegister<PlaySoundMessage>();
            this.UnRegister<UpdateAudioSettingsMessage>();
        }

        public void Handle(PlaySoundMessage message)
        {
            if (_canPlaySounds)
            {
                audio.clip = message.Clip;
                audio.Play();
            }
        }

        public void Handle(UpdateAudioSettingsMessage message)
        {
            _canPlaySounds = message.Sound;
        }
    }
}