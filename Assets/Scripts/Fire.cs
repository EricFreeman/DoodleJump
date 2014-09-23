using UnityEngine;

namespace Assets.Scripts
{
    public class Fire : MonoBehaviour
    {
        private float _scale = 1;

        void Start()
        {
            rigidbody.AddForce(0, -100, 0);
        }

        void Update()
        {
            _scale -= .05f;
            transform.localScale = new Vector3(_scale, _scale, _scale);

            if(_scale <= 0) Destroy(gameObject);
        }
    }
}