using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableCube : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interact with " + name);
        }
    }
}