using Assets.Scripts.Models;
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
            transform.rotation = _moveDir ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 180, 0, 0);

            if (transform.position.x < -WorldContext.OffScreenX)
                _moveDir = true;
            if (transform.position.x > WorldContext.OffScreenX)
                _moveDir = false;

            transform.Translate(.05f, 0, 0);
        }
    }
}