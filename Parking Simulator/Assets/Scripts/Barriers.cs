using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Barriers : MonoBehaviour
{
    [SerializeField] TMP_Text warning;
    [SerializeField] TMP_Text timer;
    Collider areaCollider;
    [SerializeField] Transform familyCar;
    [SerializeField] Transform pickupTruck;
    Coroutine restartLevel;
    [SerializeField] int currentLevel;

    void Start()
    {
        areaCollider = GetComponent<Collider>();

  
    }
  
        void Update()
        {
            if (areaCollider.bounds.Contains(familyCar.transform.position) && areaCollider.bounds.Contains(pickupTruck.transform.position))
            {
                warning.enabled = false;
            timer.enabled = false;
            if (restartLevel != null)
            {
                StopCoroutine(restartLevel);
            
                restartLevel = null;
            }
            }
            else
            {
            warning.enabled = true;
            if (restartLevel == null)
            {
             restartLevel = StartCoroutine(OutOfBoundsWarning(15));

            }
            }

        }

    IEnumerator OutOfBoundsWarning(float time)
    {
        timer.color = Color.white;
        timer.enabled = true;

        float currentTime = time;

        while (currentTime > 0)
        {

            timer.text = currentTime.ToString("0");

            yield return new WaitForSeconds(1.0f);

            currentTime--;

            if (currentTime < 6)
            {
                timer.color = Color.red;
            }
        }

        SceneManager.LoadScene(currentLevel);

    }
}
