using UnityEngine;

public class CarRotation : MonoBehaviour
{
    Transform car;
    void Start()
    {
        car = gameObject.transform;
    }

    void Update()
    {
        Vector3 eulerRotation = car.rotation.eulerAngles;

        float zRotation = eulerRotation.z > 180 ? eulerRotation.z - 360 : eulerRotation.z;

        zRotation = Mathf.Clamp(zRotation, -5, 5);

        eulerRotation.z = zRotation;
        car.rotation = Quaternion.Euler(eulerRotation);
    }
}