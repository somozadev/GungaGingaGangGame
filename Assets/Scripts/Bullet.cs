using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool debugPath;
    Rigidbody rb;
    [SerializeField] float h = 5f;
    [SerializeField] float g = -18f;

    Vector3 target_pos;
    
    LaunchData CalculateLaunchData()
    {
        rb.useGravity = true;
        float displacementY = target_pos.y - rb.transform.position.y;
        Vector3 displacementXZ = new Vector3(target_pos.x - rb.transform.position.x, 0,target_pos.z - rb.transform.position.z);

        Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * g * h);
        float time = (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (displacementY - h) / g));
        Vector3 velXZ = displacementXZ / time;
        return new LaunchData(velXZ + velY, time); //if negative g => * Mathf.Sign(g);
    }



    public void Launch(Vector3 pos)
    {
        target_pos = pos;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Physics.gravity = Vector3.up * g;
        rb.velocity = CalculateLaunchData().initialVelocity;

    }



    private void Update()
    {
        if (debugPath)
            DrawPath();
    }

    private void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = rb.position;
        int resolution = 30;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * g * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = rb.transform.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
