using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ShapeManager : MonoBehaviour
{
    public List<Transform> shapes;
    private int currentIndex = 0;

    public float tweenDuration = 0.3f;

    private List<Vector3> originalScales = new List<Vector3>();
    private List<Quaternion> originalRotations = new List<Quaternion>(); //rotations degerlerimi korumam gerekiyordu.w

    private void Start()
    {
        foreach(var shape in shapes)
        {
            originalScales.Add(shape.localScale);
            originalRotations.Add(shape.localRotation);
        }

        shapes[0].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //GetMouseButtonDown 1 frame calisir. frame sorunu aldim burada. GetMouseButton her frame icin calisir basili kaldigi sure boyunca.
            ChangeShape(1);
        else if (Input.GetMouseButtonDown(1))
            ChangeShape(-1);
    }

    private void ChangeShape(int direction)
    {
        int nextIndex = currentIndex + direction;


        if (nextIndex < 0 || nextIndex >= shapes.Count)
            return;

        Transform currentShape = shapes[currentIndex];
        Transform nextShape = shapes[nextIndex];

        currentShape.DOScale(Vector3.zero, tweenDuration).OnComplete(() =>
        {
            currentShape.gameObject.SetActive(false);
        });

        nextShape.gameObject.SetActive(true);
        nextShape.localPosition = Vector3.zero;
        //nextShape.localRotation = Quaternion.identity;

        nextShape.DOLocalRotateQuaternion(originalRotations[nextIndex],tweenDuration);

        Vector3 localShape = nextShape.localScale = originalScales[nextIndex];

        nextShape.DOScale(localShape, tweenDuration);

        currentIndex = nextIndex;

        Debug.LogWarning("nextIndex: " + nextIndex);
        Debug.LogWarning("currentIndex: " + currentIndex);

    }
}
