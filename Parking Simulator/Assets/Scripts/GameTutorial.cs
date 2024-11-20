using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTutorial : MonoBehaviour
{
    [SerializeField] GameObject arrowD;
    [SerializeField] GameObject arrowR;
    [SerializeField] GameObject arrowP;
    [SerializeField] GameObject instructionText;

    void Start()
    {
        TutorialStates(1);
    }

    void TutorialStates(int state)
    {
        switch (state)
        {
            case 1:            
                StartCoroutine(ChangeToDrive());   
                break;

            case 2:
                StartCoroutine(Accelerate());
                break;

            case 3:
                StartCoroutine(Brake());
                break;

            case 4:
                StartCoroutine(ChangeToReverse());
                break;

            case 5:
                StartCoroutine(Reverse());
                break;
        }
    }

    IEnumerator ChangeToDrive()
    {
        instructionText.SetActive(true);
        instructionText.GetComponent<TMP_Text>().text = "Change gear: 4/Scroll";
        yield return new WaitUntil(() => CarStates.currentState == "D");
        arrowD.GetComponent<Animator>().SetTrigger("Completed");
        instructionText.GetComponent<Animator>().SetTrigger("Completed");
        TutorialStates(2);
    }

    IEnumerator Accelerate()
    {
        yield return new WaitForSeconds(2);
        instructionText.SetActive(true);
        instructionText.GetComponent<TMP_Text>().text = "Accelerate: W/Gas";
        yield return new WaitUntil(() => CarStates.currentState == "D" && GameData.Instance.VehicleMoving);
        instructionText.GetComponent<Animator>().SetTrigger("Completed");
        TutorialStates(3);

    }

    IEnumerator Brake()
    {
        yield return new WaitForSeconds(2);
        instructionText.SetActive(true);
        instructionText.GetComponent<TMP_Text>().text = "Brake: Space/Brake";
        yield return new WaitUntil(() => GameData.Instance.VehicleBraking);
        instructionText.GetComponent<Animator>().SetTrigger("Completed");
        TutorialStates(4);

    }

    IEnumerator ChangeToReverse()
    {
        yield return new WaitForSeconds(2);
        instructionText.SetActive(true);
        instructionText.GetComponent<TMP_Text>().text = "Change gear: 2/Scroll";
        arrowR.SetActive(true);
        yield return new WaitUntil(() => CarStates.currentState == "R");
        arrowR.GetComponent<Animator>().SetTrigger("Completed");
        instructionText.GetComponent<Animator>().SetTrigger("Completed");
        TutorialStates(5);
    }

    IEnumerator Reverse()
    {
        yield return new WaitForSeconds(2);
        instructionText.SetActive(true);
        instructionText.GetComponent<TMP_Text>().text = "Reverse: S/Gas";
        yield return new WaitUntil(() => CarStates.currentState == "R" && GameData.Instance.VehicleReversing);
        instructionText.GetComponent<Animator>().SetTrigger("Completed");
        TutorialStates(6);
    }
}

