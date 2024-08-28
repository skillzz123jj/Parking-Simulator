using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkedState : MonoBehaviour
{
    public bool isCarMoving;

    private const float stoppedThreshold = 0.1f;
    private float stoppedDurationThreshold = 1.0f; // Time in seconds to consider the car stopped
    private float stoppedTimer = 0f;

    [SerializeField] GameObject instruction;
    Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;

    }

    // Update is called once per frame
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
            // Increment timer if car is within the threshold
            stoppedTimer += Time.deltaTime;
            if (stoppedTimer >= stoppedDurationThreshold)
            {
                // Car is considered stopped after the duration threshold
                isCarMoving = false;
            }
        }
        else
        {
            // Reset the timer if the car has moved beyond the threshold
            stoppedTimer = 0f;
            isCarMoving = true;
        }

        // Update previousPosition to the current position for the next frame
        previousPosition = transform.position;
    }
}
