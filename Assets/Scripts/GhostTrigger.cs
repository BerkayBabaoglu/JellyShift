using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public JellyGhostPreview preview;

    private void Awake()
    {
        if (preview == null)
            preview = Object.FindFirstObjectByType<JellyGhostPreview>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GhostTrigger tetiklendi: " + other.name + " | Tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            preview.NextGhost();
        }
    }
}
