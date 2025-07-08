using UnityEngine;

public class CameraFreeze : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; //kameranin oyuncuya gore konumu
    public Vector3 fixedEulerRotation;

    private Quaternion fixedRotation;

    private void Start()
    {
        fixedRotation = Quaternion.Euler(fixedEulerRotation); //sabit rotasyon hesabi
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;

        transform.rotation = fixedRotation;
    }
}
