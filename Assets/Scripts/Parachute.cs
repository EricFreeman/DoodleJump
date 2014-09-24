using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Parachute : MonoBehaviour
    {
        public Player Player;

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
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Player.IsParachuteLaunched = true;
                    Player.RemainingParachutes--;

                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (Player.rigidbody.velocity.y > 0)
                {
                    Player.IsParachuteLaunched = false;
                    foreach (Transform t in Player.transform)
                        if (t.name.Contains("Parachute")) Destroy(t.gameObject);
                }
                else
                    Player.rigidbody.velocity = new Vector3(Player.rigidbody.velocity.x, -.5f, 0);
            }
        }
    }
}
