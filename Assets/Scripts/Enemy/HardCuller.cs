using UnityEngine;

namespace Enemy
{
    public class HardCuller : MonoBehaviour
    {
        private Renderer _renderer;
        
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        private void OnBecameInvisible()
        {
            _renderer.enabled = false;
            Debug.Log("Invisible");
        }

        private void OnWillRenderObject()
        {
            Debug.Log("Now Visible");
            _renderer.enabled = true;
        }
    }
}
