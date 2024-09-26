using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMode : MonoBehaviour
{
    [SerializeField] private SpaceMovement spaceMovement;
    bool flymodeActivated = true;
    [SerializeField] GameObject gears;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            
                flymodeActivated = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                other.gameObject.GetComponent<SpaceMovement>().enabled = false;
                other.gameObject.GetComponent<CarRotation>().enabled = true;
                other.gameObject.GetComponent<CarLights>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gears.SetActive(true);      
 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            flymodeActivated = true;
            //transform.position += new Vector3(0, 10 * Time.deltaTime, 0);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<SpaceMovement>().enabled = true;
            other.gameObject.GetComponent<CarLights>().enabled = false;
            other.gameObject.GetComponent<CarRotation>().enabled = false;
            gears.SetActive(false);
        }
    }

}
