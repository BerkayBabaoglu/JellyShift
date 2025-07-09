using UnityEngine;

public class JellyGhostPreview : MonoBehaviour
{
    public GameObject[] ghostCubes; 
    private int currentIndex = 0;

    public void ForceUpdateGhost()
    {
        Transform playerShape = ShapeManager.Instance.GetCurrentShape();
        if (ghostCubes.Length > currentIndex && ghostCubes[currentIndex] != null && playerShape != null)
        {
            GameObject ghost = ghostCubes[currentIndex];

            Vector3 worldScale = playerShape.lossyScale;
            Transform ghostParent = ghost.transform.parent;
            Vector3 parentWorldScale = ghostParent != null ? ghostParent.lossyScale : Vector3.one;

            Vector3 adjustedLocalScale = new Vector3(
                worldScale.x / parentWorldScale.x,
                worldScale.y / parentWorldScale.y,
                worldScale.z / parentWorldScale.z
            );

            ghost.transform.localScale = adjustedLocalScale;

            float yRotation = playerShape.eulerAngles.y;
            ghost.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }

    void Update()
    {
        Transform playerShape = ShapeManager.Instance.GetCurrentShape();

        for (int i = 0; i < ghostCubes.Length; i++)
            ghostCubes[i].SetActive(i == currentIndex);

        if (ghostCubes.Length > currentIndex && ghostCubes[currentIndex] != null && playerShape != null)
        {
            GameObject ghost = ghostCubes[currentIndex];

            Vector3 worldScale = playerShape.lossyScale;
            Transform ghostParent = ghost.transform.parent;
            Vector3 parentWorldScale = ghostParent != null ? ghostParent.lossyScale : Vector3.one;

            Vector3 adjustedLocalScale = new Vector3(
                worldScale.x / parentWorldScale.x,
                worldScale.y / parentWorldScale.y,
                worldScale.z / parentWorldScale.z
            );

            ghost.transform.localScale = adjustedLocalScale;

            float yRotation = playerShape.eulerAngles.y;
            ghost.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }

    public void NextGhost()
    {
        currentIndex++;
        if (currentIndex >= ghostCubes.Length)
            currentIndex = ghostCubes.Length - 1;
    }
}
