using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int perfectCount = 0;
    public int perfectNeededForFever = 5;
    public bool isInFever = false;

    public float feverDuration = 5f;
    private float feverTimer = 0f;

    public void RegisterPerfect()
    {
        if (!isInFever)
        {
            perfectCount++;
            Debug.Log("Perfect! Count:" +  perfectCount);

            if(perfectCount >= perfectNeededForFever)
            {
                StartFever();
            }
        }
    }

    void StartFever()
    {
        isInFever = true;
        feverTimer = feverDuration;
        perfectCount = 0;
        Debug.Log("Fever mod aktif");

    }

    private void Update()
    {
        if (isInFever)
        {
            feverTimer -= Time.deltaTime;
            if (feverTimer <= 0f)
                EndFever();
        }
    }

    void EndFever()
    {
        isInFever = false;
        Debug.Log("Fever mod kapali");

    }
}
