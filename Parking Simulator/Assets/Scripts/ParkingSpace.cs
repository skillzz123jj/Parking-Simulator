using UnityEngine;

public class ParkingSpace : MonoBehaviour
{
    [SerializeField] int collisionCount;
    [SerializeField] Material whitePaint;
    [SerializeField] Material greenPaint;

 
    void Update()
    {
        if (collisionCount >= 4) {
            gameObject.GetComponent<MeshRenderer>().material = greenPaint;
            GameData.Instance.Parked = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = whitePaint;
         
        }
    }
 
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Wheel") {
            collisionCount++;
        }
    }
 
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Wheel") {
            collisionCount--;
            GameData.Instance.Parked = false;
        }
    }
}