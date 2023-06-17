using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIK : MonoBehaviour
{
    public Transform pelvis; // 

    public Transform[] leftJoints; // array of joints in the leg
    public Transform leftFootTarget; // target position for the end-effector
    private float[] leftBoneLengths; // array of bone lengths in the leg
    private float leftTotalLength; // total length of the leg
    private Vector3 leftRoot; // root position of the leg

    public Transform[] rightJoints; // array of joints in the leg
    public Transform rightFootTarget; // target position for the end-effector    
    private float[] rightBoneLengths; // array of bone lengths in the leg
    private float rightTotalLength; // total length of the leg
    private Vector3 rightRoot; // root position of the leg

    public float threshold = 0.01f; // threshold for convergence
    public int maxIterations = 20; // maximum number of iterations
    
    void Start()
    {
        if (leftFootTarget == null || rightFootTarget == null)
        {
            Debug.LogError("[ERROR] Target positions for the feetsies are not set!");
            return;
        }

        leftBoneLengths = new float[leftJoints.Length - 1];
        for (int i = 0; i < leftBoneLengths.Length; i++)
        {
            leftBoneLengths[i] = Vector3.Distance(leftJoints[i + 1].position, leftJoints[i].position);
            leftTotalLength += leftBoneLengths[i];
        }
        leftRoot = leftJoints[0].position;

        rightBoneLengths = new float[rightJoints.Length - 1];
        for (int i = 0; i < rightBoneLengths.Length; i++)
        {
            rightBoneLengths[i] = Vector3.Distance(rightJoints[i + 1].position, rightJoints[i].position);
            rightTotalLength += rightBoneLengths[i];
        }
        rightRoot = rightJoints[0].position;
    }

    void Update()
    {
        // Update the position of the pelvis to the average (middle) between the two feet so it moves the skeleton with the feet
        Vector3 newPosition = pelvis.position;
        newPosition.x = (leftFootTarget.position.x + rightFootTarget.position.x) / 2f;
        pelvis.position = newPosition;

        // Update the position of the left and right hip as well
        leftRoot.x = (leftFootTarget.position.x + rightFootTarget.position.x) / 2f;
        rightRoot.x = (leftFootTarget.position.x + rightFootTarget.position.x) / 2f;

        // Perform CCD for the left leg
        FABRIKSolve(leftFootTarget, leftJoints, leftBoneLengths, leftRoot);

        // Perform CCD for the left leg
        FABRIKSolve(rightFootTarget, rightJoints, rightBoneLengths, rightRoot);
    }

    private void FABRIKSolve(Transform target, Transform[] joints, float[] boneLengths, Vector3 root)
    {
        // // Start timer
        // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // stopwatch.Start();

        // forward reaching
        joints[joints.Length - 1].position = target.position;
        for (int i = joints.Length - 2; i >= 0; i--)
        {
            Vector3 direction = joints[i + 1].position - joints[i].position;
            direction.Normalize();
            joints[i].position = joints[i + 1].position - direction * boneLengths[i];
        }

        // backward reaching
        joints[0].position = root;
        for (int i = 1; i < joints.Length; i++)
        {
            Vector3 direction = joints[i - 1].position - joints[i].position;
            direction.Normalize();
            joints[i].position = joints[i - 1].position - direction * boneLengths[i - 1];
        }

        // check for convergence
        if (Vector3.Distance(joints[joints.Length - 1].position, target.position) < threshold){
            // // Print iteration count
            // Debug.Log("Iteration: 0");

            // // Print time elapsed and number of iterations
            // Debug.Log("Time elapsed: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

            // // Stop timer
            // stopwatch.Stop();

            return;
        } 

        // iteration
        for (int i = 0; i < maxIterations; i++)
        {
            // forward reaching
            joints[joints.Length - 1].position = target.position;
            for (int j = joints.Length - 2; j >= 0; j--)
            {
                Vector3 direction = joints[j + 1].position - joints[j].position;
                direction.Normalize();
                joints[j].position = joints[j + 1].position - direction * boneLengths[j];
            }

            // backward reaching
            joints[0].position = root;
            for (int j = 1; j < joints.Length; j++)
            {
                Vector3 direction = joints[j - 1].position - joints[j].position;
                direction.Normalize();
                joints[j].position = joints[j - 1].position - direction * boneLengths[j - 1];
            }

            // check for convergence
            if (Vector3.Distance(joints[joints.Length - 1].position, target.position) < threshold){
                // // Print iteration count
                // Debug.Log("Iteration: " + i);

                // // Print time elapsed and number of iterations
                // Debug.Log("Time elapsed: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

                // // Stop timer
                // stopwatch.Stop();

                return;
            } 
        }
    }

}