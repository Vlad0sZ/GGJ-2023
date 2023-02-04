using UnityEngine;

namespace Main
{
    public class InteractableCube : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interact with " + name);
        }
    }
}