using UnityEngine;
using UnityEngine.SceneManagement;

public class Shatter : MonoBehaviour
{
    private bool isBroken = false;
    public GameObject breakEffectPrefab; 

    private void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        if (other.CompareTag("Player"))
        {
            Break(other.transform.position); //other.transform -> oyuncun carptigi konum
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Break(Vector3 forceOrigin)
    {
        if (isBroken) return;
        isBroken = true;

        if (breakEffectPrefab != null)
        {
            Vector3 hitPos = GetComponent<Collider>().ClosestPoint(forceOrigin);
            Instantiate(breakEffectPrefab, hitPos, Quaternion.identity);
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }


}