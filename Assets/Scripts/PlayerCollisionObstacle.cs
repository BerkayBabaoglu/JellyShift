using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCollisionObstacle : MonoBehaviour
{
    private int hitCount = 0;
    public int maxHits = 2;

    public GameObject[] UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("carpti");

            Shatter obstacle = other.GetComponent<Shatter>();
            if (obstacle != null)
            {
                obstacle.Break(transform.position);
            }

            StartCoroutine(Wait());

            hitCount++;
            Debug.Log("Carpma sayisi:" + hitCount);

            if (hitCount >= maxHits)
            {
                Debug.Log("carpma sinirina ulasildi");
                foreach (GameObject go in UI) { 
                    go.SetActive(true);
                }
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator Wait()
    {
        bool isKnockedBack = true;

        float speedyMoveSpeed = PathFollow.Instance.GetMoveSpeed();


        speedyMoveSpeed = 0f;

        float backMove = 0.5f;
        Vector3 back = -transform.forward;
        float backTime = 0.1f;

        float elapsed = 0f;
        Vector3 start = transform.position;
        Vector3 target = start + back * backMove;

        while(elapsed < backTime)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / backTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        yield return new WaitForSeconds(0.1f);

        speedyMoveSpeed = 0.09f;
        isKnockedBack = false;
    }

}
