using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class CupMovement : MonoBehaviour
{
    public SplineContainer splineContainer;

    private bool isMoving = false;
    public float moveSpeed = 0.4f;
    public float move = 0f;

    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 90f;

    public float rotationY = -90f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) & !isMoving)
            isMoving = true;

        if (!isMoving || splineContainer == null) return;

        move += (moveSpeed / splineContainer.CalculateLength()) * Time.deltaTime;
        move = Mathf.Clamp01(move);

        var spline = splineContainer.Spline;

        float3 pos, tangent;
        spline.Evaluate(move, out pos, out tangent, out _);

        Vector3 worldPos = splineContainer.transform.TransformPoint((Vector3)pos);
        transform.position = worldPos;

        Vector3 worldTangent = splineContainer.transform.TransformDirection((Vector3)tangent);

        if(worldTangent.sqrMagnitude > 0.0001f)
        {
            Quaternion lookRot = Quaternion.LookRotation(worldTangent);
            lookRot *= Quaternion.Euler(0f, rotationY, 0f);
            transform.rotation = lookRot;
        }


        //donus
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Destroy(this.gameObject);
            PathFollow.Instance.moveSpeed += 0.1f;
        }

    }


}
