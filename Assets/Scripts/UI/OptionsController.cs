using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class OptionsController : MonoBehaviour
    {
        public Toggle Music;
        public Toggle Sound;

        private PlayerModel _player;

        void Start()
        {
            _player = PlayerManager.Load();
            Music.isOn = _player.IsMusicEnabled;
            Sound.isOn = _player.IsSoundEnabled;

            Music.onValueChanged.AddListener(SaveChanges);
            Sound.onValueChanged.AddListener(SaveChanges);
        }

        public void SaveChanges(bool newValue)
        {
            _player.IsMusicEnabled = Music.isOn;
            _player.IsSoundEnabled = Sound.isOn;
            PlayerManager.Save(_player);
        }

        public void HardReset()
        {
            PlayerManager.Reset();
        }

        public void Back()
        {
            Destroy(gameObject);
        }
    }
}