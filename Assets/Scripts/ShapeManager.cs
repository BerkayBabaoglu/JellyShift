using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class ShapeManager : MonoBehaviour
{
    public static ShapeManager Instance { get; private set; }
    public List<Transform> shapes;
    private int currentIndex = 0;
    public float tweenDuration = 0.3f;
    private List<Vector3> originalScales = new List<Vector3>();
    private List<Quaternion> originalRotations = new List<Quaternion>();

    private void Awake() { Instance = this; }
    private void Start()
    {
        foreach(var shape in shapes)
        {
            originalScales.Add(shape.localScale);
            originalRotations.Add(shape.localRotation);
        }
        shapes[0].gameObject.SetActive(true);
    }

    public Transform GetCurrentShape()
    {
        if (currentIndex < 0 || currentIndex >= shapes.Count)
            return null;
        return shapes[currentIndex];
    }
    public Vector3 GetCurrentShapeScale()
    {
        if (currentIndex < 0 || currentIndex >= originalScales.Count)
            return Vector3.one;
        return originalScales[currentIndex];
    }
    public Quaternion GetCurrentShapeRotation()
    {
        if (currentIndex < 0 || currentIndex >= originalRotations.Count)
            return Quaternion.identity;
        return originalRotations[currentIndex];
    }

    public GameObject GetCurrentShapeObject()
    {
        var currentShape = GetCurrentShape();
        return currentShape != null ? currentShape.gameObject : null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //bunu buttondown yaptim cunku her tiklamada birden fazla framw ilerlemesini istemiyorum. GetMouseButton'dan da ayiran fark budur zaten. 
            ChangeShape(1);
        else if (Input.GetMouseButtonDown(1))
            ChangeShape(-1);
    }
    public void ChangeShape(int direction)
    {
        int nextIndex = currentIndex + direction;
        if (nextIndex < 0 || nextIndex >= shapes.Count)
            return;

        Transform currentShape = shapes[currentIndex];
        Transform nextShape = shapes[nextIndex];


        for (int i = 0; i < shapes.Count; i++) //sekillerin colliderlarini tek tek kapadik
        {
            Collider col = shapes[i].GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }

        currentShape.DOScale(Vector3.zero, tweenDuration).OnComplete(() =>
        {
            currentShape.gameObject.SetActive(false);
        });

  
        nextShape.gameObject.SetActive(true);
        nextShape.localPosition = Vector3.zero;
        // nextShape.localRotation = Quaternion.identity; 
        nextShape.DOLocalRotateQuaternion(originalRotations[nextIndex], tweenDuration);
        nextShape.localScale = Vector3.zero; 
        nextShape.DOScale(originalScales[nextIndex], tweenDuration);


        Collider nextCol = nextShape.GetComponent<Collider>(); //yeni seklin collideri
        if (nextCol != null) nextCol.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        currentIndex = nextIndex;

        Debug.LogWarning("nextIndex: " + nextIndex);
        Debug.LogWarning("currentIndex: " + currentIndex);

        
        JellyGhostPreview preview = Object.FindFirstObjectByType<JellyGhostPreview>();
        if (preview != null)
            preview.ForceUpdateGhost();
    }
}
