using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts
{
    public class RemoveBelowScreen : MonoBehaviour
    {
        void Update()
        {
            if (transform.position.y < Camera.main.transform.position.y - WorldContext.OffScreenY)
                Destroy(gameObject);
        }
    }
}