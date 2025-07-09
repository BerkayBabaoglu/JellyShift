using UnityEngine;

public class JellyShapeManager : MonoBehaviour
{
    public static JellyShapeManager Instance
    {
        get; private set;
    }

    public Transform currentShape; //aktif jel

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
