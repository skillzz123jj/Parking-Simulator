using UnityEngine;

public class ParkedState : MonoBehaviour
{
    public bool isCarMoving;

    private const float stoppedThreshold = 0.1f;
    private float stoppedDurationThreshold = 1.0f; 
    private float stoppedTimer = 0f;

    [SerializeField] GameObject instruction;
    Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;

    }

    void Update()
    {
        if (!isCarMoving && GameData.Instance.Parked)      
        {
            instruction.SetActive(true);

            if (CarStates.currentState == "P")
            {
                GameData.Instance.LevelFinished = true;
            }
        }
        else
        {
            if (instruction != null)
            {

                instruction.SetActive(false);
            }

        }
        if (Vector3.Distance(transform.position, previousPosition) < stoppedThreshold)
        {
            stoppedTimer += Time.deltaTime;
            if (stoppedTimer >= stoppedDurationThreshold)
            {
                isCarMoving = false;
            }
        }
        else
        {
            stoppedTimer = 0f;
            isCarMoving = true;
        }
        previousPosition = transform.position;
    }
}
