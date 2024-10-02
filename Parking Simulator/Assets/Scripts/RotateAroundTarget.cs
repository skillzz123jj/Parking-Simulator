using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
    public Transform globe;             
    public float orbitSpeed = 20f;      
    public float orbitDistance = 5f;     
    private Rigidbody rb;               
    private Vector3 orbitAxis;          

    void Start()
    {
        rb = GetComponent<Rigidbody>();  

        orbitAxis = RandomizeOrbitAxis();

        Vector3 initialDirection = (transform.position - globe.position).normalized;
        transform.position = globe.position + initialDirection * orbitDistance;

    }

    void FixedUpdate()
    {
        Vector3 toCenter = (transform.position - globe.position).normalized;
        Vector3 tangentVelocity = Vector3.Cross(toCenter, orbitAxis) * orbitSpeed;

        rb.velocity = tangentVelocity;
    }

    Vector3 RandomizeOrbitAxis()
    {
        Vector3 axis = Vector3.zero;
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                axis.x = 1;
                break;
            case 1:
                axis.y = 1;
                break;
            case 2:
                axis.z = 1;
                break;
        }

        axis += new Vector3(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f));
        return axis.normalized;
    }
}
