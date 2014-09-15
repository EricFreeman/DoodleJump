using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if(col.collider.rigidbody.velocity.y < 0)
                col.collider.rigidbody.velocity = new Vector3(0, 20, 0);
        }
    }
}