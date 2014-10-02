using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Rocket : MonoBehaviour,
        IListener<LaunchRocketMessage>
    {
        public Player Player;
        public AudioClip RocketSound;

        private float _rocketStart;
        private bool _pressedScreen;
        private bool _isPlayerInput
        {
            get
            { 
                return Input.GetKeyDown(KeyCode.W) 
                    || Input.GetKeyDown(KeyCode.UpArrow) 
                    || _pressedScreen;
            }
        }

        void Start()
        {
            Player.RemainingRockets = PlayerContext.Get(UpgradeType.Rocket);

            if (Player.RemainingRockets == 0) GetComponent<SpriteRenderer>().enabled = false;
        }

        void Update()
        {
            UpdateRocket();
            UpdateFire();

            _pressedScreen = false;
        }

        private void UpdateRocket()
        {
            if (!CanFireRocket() || !_isPlayerInput)
                return;

            EventAggregator.SendMessage(new PlaySoundMessage { Clip = RocketSound });
            Player.IsRocketFiring = true;
            _rocketStart = Time.fixedTime;
            Player.RemainingRockets--;
        }

        private bool CanFireRocket()
        {
            return !Player.IsParachuteLaunched
                   && !Player.IsRocketFiring
                   && Player.RemainingRockets > 0;
        }

        private void UpdateFire()
        {
            if (!Player.IsRocketFiring) return;

            var fire = (GameObject)Instantiate(Resources.Load("Prefabs/Fire"));
            fire.transform.position = Player.transform.position;

            Player.rigidbody.AddForce(0, 25, 0);
            if (Time.fixedTime > _rocketStart + Player.RocketLength) Player.IsRocketFiring = false;
        }

        public void Handle(LaunchRocketMessage message)
        {
            _pressedScreen = true;
        }
    }
}