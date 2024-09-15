using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpace : MonoBehaviour
{
    [SerializeField] private List<GameObject> cars = new List<GameObject>();
    void Start()
    {
        int index = Random.Range(0, cars.Count);
        GameObject chosenCar = cars[index];
        chosenCar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
