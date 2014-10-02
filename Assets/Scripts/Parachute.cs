using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Parachute : MonoBehaviour,
        IListener<LaunchParachuteMessage>
    {
        public Player Player;

        private bool _pressedScreen;
        private bool _isPlayerInput
        {
            get
            {
                return Input.GetKeyDown(KeyCode.S)
                       || Input.GetKeyDown(KeyCode.DownArrow)
                       || _pressedScreen;
            }
        }

        void Start()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Player.RemainingParachutes = PlayerContext.Get(UpgradeType.Parachute);

        }

        void Update()
        {
            if (Player.IsRocketFiring) return;
            if (!Player.IsParachuteLaunched)
            {
                if (Player.RemainingParachutes <= 0 || Player.rigidbody.velocity.y > 0) return;
                if (_isPlayerInput)
                {
                    Player.IsParachuteLaunched = true;
                    Player.RemainingParachutes--;

                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (Player.rigidbody.velocity.y > 0 || _isPlayerInput)
                {
                    Player.IsParachuteLaunched = false;
                    foreach (Transform t in Player.transform)
                        if (t.name.Contains("Parachute")) Destroy(t.gameObject);
                }
                else
                    Player.rigidbody.velocity = new Vector3(Player.rigidbody.velocity.x, -.5f, 0);
            }

            _pressedScreen = false;
        }

        public void Handle(LaunchParachuteMessage message)
        {
            _pressedScreen = true;
        }
    }
}