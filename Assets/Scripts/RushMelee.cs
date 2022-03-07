using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushMelee : MonoBehaviour
{
    public Transform startPoint;
        public Transform target;
        public int resolution = 30;
        public float curveHight = 1;
        public float gravity = -18;
    
        
        public Rigidbody bullet;
        public LineRenderer lineRenderer;
        void Start()
        {
            lineRenderer = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();
            lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
        }
    
        void Update() {
            //DrawPath();     
            if (Input.GetKeyDown(KeyCode.Space)) {
                //Rush();
            }
        }
    
        public void Rush(Transform startPoint, Transform target)
        {
            Rigidbody clone = Instantiate(bullet, startPoint.transform.position, Quaternion.identity);
    
            Physics.gravity = Vector3.up * gravity;
    
            clone.velocity = CalculateRushData(startPoint.transform, target.transform).initialVelocity;
        }
    
        RushData CalculateRushData(Transform startPoint, Transform target)
        {
            float displacementY = target.position.y - startPoint.position.y;
            Vector3 displacementXZ = new Vector3(target.position.x - startPoint.position.x, 0, target.position.z - startPoint.position.z);
            float value = (displacementY - curveHight) / gravity;
            float clampedCurve = Mathf.Clamp(value, 0, value);
            float time = Mathf.Sqrt(-2 * curveHight / gravity) + Mathf.Sqrt(2 * clampedCurve);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHight);
            Vector3 velocityXZ = displacementXZ / time;
            return new RushData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }
    
        public void DrawPath(Transform startPoint, Transform target)
        {
            RushData RushData = CalculateRushData(startPoint, target);
            Vector3 previousDrawPoint = startPoint.transform.position;
    
    
            for (int i = 1; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * RushData.timeToTarget;
                Vector3 displacement = RushData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = startPoint.transform.position + displacement;
                Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
    
                previousDrawPoint = drawPoint;
    
                lineRenderer.positionCount = resolution;
    
                lineRenderer.SetPosition(i - 1, drawPoint);
            }
        }
    
        struct RushData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;
    
            public RushData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
    
        }
}
