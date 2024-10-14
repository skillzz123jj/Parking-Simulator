using UnityEngine;

public class SpaceMode : MonoBehaviour
{
    [SerializeField] private SpaceMovement spaceMovement;
    bool flymodeActivated = true;
    [SerializeField] GameObject gears;
    [SerializeField] private CarAudio carAudio;
    [SerializeField] private GameObject carEngine;

    private void Start()
    {
            gears.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            
                flymodeActivated = false;
                carAudio.enabled = true;
            carEngine.SetActive(true);
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
            carAudio.enabled = false;
            carEngine.SetActive(false);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<SpaceMovement>().enabled = true;
            other.gameObject.GetComponent<CarLights>().enabled = false;
            other.gameObject.GetComponent<CarRotation>().enabled = false;
            gears.SetActive(false);
        }
    }

}
