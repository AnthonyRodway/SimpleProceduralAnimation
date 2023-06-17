using System;
using System.Collections.Generic;
using UnityEngine;

public class CCDIK : MonoBehaviour
{
    // The hierarchy of bones representing the character's joints
    public Transform[] bones;

    // The target positions for the feet
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    // The maximum number of iterations for CCD
    public int maxIterations = 100;

    // The distance threshold for stopping CCD iterations
    public float distanceThreshold = 0.01f;

    // The indices of the bones representing the left and right feet
    private int leftAnkleIndex;
    private int rightAnkleIndex;
    private int leftKneeIndex;
    private int rightKneeIndex;
    private int leftHipIndex;
    private int rightHipIndex;
    private int pelvisIndex;

    // Joint limits and rotation constraints
    private float[] hipXBounds = {-30.0f, 30.0f}; 
    private float[] hipYBounds = {90.0f, 200.0f}; // right leg needs negative of these
    private float[] hipZBounds = {-90.0f, 90.0f};
    private float[] kneeXBounds = {-30.0f, 30.0f};
    private float[] kneeYBounds = {-30.0f, 30.0f};
    private float[] kneeZBounds = {0.0f, 150.0f};

    private void Start()
    {
        if (leftFootTarget == null || rightFootTarget == null)
        {
            Debug.LogError("[ERROR] Target positions for the feetsies are not set!");
            return;
        }

        // Find the bones representing the left and right feet, knees, hips, and pelvis
        for (int i = 0; i < bones.Length; i++)
        {
            string boneName = bones[i].name.ToLower();
            switch (boneName)
            {
                case string s when s.Contains("leftankle"):
                    leftAnkleIndex = i;
                    break;
                case string s when s.Contains("rightankle"):
                    rightAnkleIndex = i;
                    break;
                case string s when s.Contains("leftknee"):
                    leftKneeIndex = i;
                    break;
                case string s when s.Contains("rightknee"):
                    rightKneeIndex = i;
                    break;
                case string s when s.Contains("lefthip"):
                    leftHipIndex = i;
                    break;
                case string s when s.Contains("righthip"):
                    rightHipIndex = i;
                    break;
                case string s when s.Contains("pelvis"):
                    pelvisIndex = i;
                    break;
                default:
                    // handle cases where bone name doesn't match any of the above conditions
                    Debug.LogError("[ERROR] An unidentified bone has been found!");
                    return;
            }
        }
    }

    private void Update()
    {
        Vector3 newPosition = bones[pelvisIndex].position;
        newPosition.x = (leftFootTarget.position.x + rightFootTarget.position.x) / 2f;
        bones[pelvisIndex].position = newPosition;

        // Perform CCD for the left leg
        CCD(leftFootTarget, bones[leftAnkleIndex], bones[leftKneeIndex], bones[leftHipIndex]);

        // Perform CCD for the right leg
        CCD(rightFootTarget, bones[rightAnkleIndex], bones[rightKneeIndex], bones[rightHipIndex]);
    }

    private void CCD(Transform target, Transform endEffector, Transform knee, Transform hip)
    {
        // // Start timer
        // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // stopwatch.Start();

        int iterations = 0;
        while (iterations < maxIterations && Vector3.Distance(target.position, endEffector.position) > distanceThreshold)
        {
            // Calculate the vector from the knee to the end effector
            Vector3 toEndEffector = endEffector.position - knee.position;

            // Calculate the vector from the knee to the target position
            Vector3 toTarget = target.position - knee.position;

            // Calculate the angle between the two vectors
            float angle = Vector3.Angle(toEndEffector, toTarget);

            // Calculate the rotation axis using the cross product of the two vectors
            Vector3 rotationAxis = Vector3.Cross(toEndEffector, toTarget);

            // Rotate the knee joint around the rotation axis
            knee.Rotate(rotationAxis, angle, Space.World);

            // Constrain the knee joint rotation to the allowed bounds
            // Vector3 kneeRotation = knee.localEulerAngles;
            // kneeRotation.x = Mathf.Clamp(kneeRotation.x, kneeXBounds[0], kneeXBounds[1]);
            // kneeRotation.y = Mathf.Clamp(kneeRotation.y, kneeYBounds[0], kneeYBounds[1]);
            // kneeRotation.z = Mathf.Clamp(kneeRotation.z, kneeZBounds[0], kneeZBounds[1]);
            // knee.localEulerAngles = kneeRotation;

            // Calculate the new vector from the hip to the end effector
            toEndEffector = endEffector.position - hip.position;

            // Calculate the vector from the hip to the target position
            toTarget = target.position - hip.position;

            // Calculate the new angle between the two vectors
            angle = Vector3.Angle(toEndEffector, toTarget);

            // Calculate the rotation axis using the cross product of the two vectors
            rotationAxis = Vector3.Cross(toEndEffector, toTarget);

            // Rotate the hip joint around the rotation axis
            hip.Rotate(rotationAxis, angle, Space.World);

            // Constrain the hip joint rotation to the allowed bounds
            // Vector3 hipRotation = hip.localEulerAngles;
            // hipRotation.x = Mathf.Clamp(hipRotation.x, hipXBounds[0], hipXBounds[1]);
            // hipRotation.y = Mathf.Clamp(hipRotation.y, hipYBounds[0], hipYBounds[1]);
            // hipRotation.z = Mathf.Clamp(hipRotation.z, hipZBounds[0], hipZBounds[1]);
            // hip.localEulerAngles = hipRotation;

            iterations++;
        }

        // // Print iteration count
        // Debug.Log("Iteration: " + iterations);

        // // Print time elapsed and number of iterations
        // Debug.Log("Time elapsed: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        // // Stop timer
        // stopwatch.Stop();
    }

}