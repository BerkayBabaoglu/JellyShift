using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject[] UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject go in UI){
                go.SetActive(true);
            }
        }
    }
}
