using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class PathFollow : MonoBehaviour
{
    public static PathFollow Instance
    {
        get; private set;
    }

    private bool isMoving = false;
    public SplineContainer splineContainer;
    public float moveSpeed = 0.09f;
    private float move = 0f;

    public float rotationY = -90f;

    public GameObject[] UI;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) & !isMoving)
        {
            isMoving = true;
            foreach (GameObject go in UI) {
                go.SetActive(false);
            }
        }
            


        if (!isMoving || splineContainer == null) return;

        // t adim ilerletiyorum
        move += (moveSpeed / splineContainer.CalculateLength()) * Time.deltaTime;
        move = Mathf.Clamp01(move);

        var spline = splineContainer.Spline;

        float3 pos, tangent;
        spline.Evaluate(move, out pos, out tangent, out _);

        Vector3 worldPos = splineContainer.transform.TransformPoint((Vector3)pos); //karakterin dunya pozisyonuna gore ayarlanmasini saglad�m. yoksa child oldugu icin farkli noktalarda doguyordu.
        transform.position = worldPos;

        Vector3 worldTangent = splineContainer.transform.TransformDirection((Vector3)tangent); //ayni sey yon icin de yapildi

        if (worldTangent.sqrMagnitude > 0.0001f) // sqrMagnitude fonksiyonu vektorun uzunlugunun karesini verir. sadece mesafe hesapladigi icin karekokle ugrasmaz direkt karesini verir. mesafeye 0 kontrolu yaptigimiz icin karekok yapmaya gerek yoktur. magnitude'dan farki bu. 3-4-5 ucgeni icin magnitude 5 verir, sqrmagnitude 25 verir (Vector3(3,4,0))
        {
            Quaternion lookRot = Quaternion.LookRotation(worldTangent);
            lookRot *= Quaternion.Euler(0f, rotationY, 0f);
            transform.rotation = lookRot;
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}