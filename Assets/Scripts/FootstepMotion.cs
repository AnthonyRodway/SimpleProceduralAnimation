using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepMotion : MonoBehaviour
{
    public float stepHeight = 0.2f; // height of each step
    public float stepDistance = 1.0f; // distance covered by each step
    public float stepDuration = 1.0f; // duration of each step
    public Transform leftFoot; // transform of the left foot
    public Transform rightFoot; // transform of the right foot

    private bool isLeftStep = true; // flag to keep track of which foot is currently stepping
    private Vector3 initialLeftFootPosition; // initial position of the left foot
    private Vector3 initialRightFootPosition; // initial position of the right foot
    private float stepTimer; // timer for each step
    private float stepProgress; // progress of the current step

    private bool firstStep = true;

    void Start()
    {
        initialLeftFootPosition = leftFoot.position;
        initialRightFootPosition = rightFoot.position;
    }

    void Update()
    {
        stepTimer += Time.deltaTime;
        stepProgress = Mathf.Clamp01(stepTimer / stepDuration);

        if (isLeftStep)
        {
            // Calculate step position based on step progress using an arc equation
            Vector3 stepPosition = new Vector3(
                initialLeftFootPosition.x - stepProgress * stepDistance,
                initialLeftFootPosition.y + Mathf.Sin(stepProgress * Mathf.PI) * stepHeight,
                initialLeftFootPosition.z
            );

            if(firstStep)
            {
                stepPosition.x = initialLeftFootPosition.x - stepProgress * stepDistance / 2;
                
            }

            leftFoot.position = stepPosition;

            // Check if current step is complete
            if (stepProgress >= 1.0f)
            {
                stepTimer = 0.0f;
                isLeftStep = false;
                initialLeftFootPosition = stepPosition;
                firstStep = false;
            }
        }
        else
        {
            // Calculate step position based on step progress using an arc equation
            Vector3 stepPosition = new Vector3(
                initialRightFootPosition.x - stepProgress * stepDistance,
                initialRightFootPosition.y + Mathf.Sin(stepProgress * Mathf.PI) * stepHeight,
                initialRightFootPosition.z
            );

            rightFoot.position = stepPosition;

            // Check if current step is complete
            if (stepProgress >= 1.0f)
            {
                stepTimer = 0.0f;
                isLeftStep = true;
                initialRightFootPosition = stepPosition;
            }
        }
    }

}