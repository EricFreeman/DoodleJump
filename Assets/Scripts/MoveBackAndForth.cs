using UnityEngine;

namespace Assets.Scripts
{
    public class MoveBackAndForth : MonoBehaviour
    {
        private bool _moveDir;

        void Start()
        {
            _moveDir = UnityEngine.Random.Range(0, 1) == 1;
        }

        void Update()
        {
            if (transform.position.x < -4) _moveDir = true;
            if (transform.position.x > 4) _moveDir = false;

            transform.Translate(_moveDir ? .05f : -.05f, 0, 0);
        }
    }
}