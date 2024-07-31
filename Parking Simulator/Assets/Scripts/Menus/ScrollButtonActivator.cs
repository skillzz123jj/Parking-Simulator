using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ScrollButtonActivator : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public List<Button> buttons; // List of buttons to manage

    private int activeButtonIndex = 0; // Index of the currently active button
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;

        // Initialize all buttons to be non-interactable except the first one
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = (i == activeButtonIndex);
        }

        // Select the first button
        eventSystem.SetSelectedGameObject(buttons[activeButtonIndex].gameObject);
    }

    void Update()
    {
        // Calculate the scroll position and update the active button index accordingly
        float scrollPosition = scrollRect.horizontalNormalizedPosition;
        int newActiveButtonIndex = Mathf.RoundToInt(scrollPosition * (buttons.Count - 1));

        if (newActiveButtonIndex != activeButtonIndex)
        {
            SetActiveButton(newActiveButtonIndex);
        }
    }

    void SetActiveButton(int index)
    {
        // Ensure the index is within bounds
        if (index < 0 || index >= buttons.Count)
            return;

        // Deactivate the current active button
        buttons[activeButtonIndex].interactable = false;

        // Activate the new button
        buttons[index].interactable = true;

        // Update the active button index
        activeButtonIndex = index;

        // Select the new active button
        eventSystem.SetSelectedGameObject(buttons[activeButtonIndex].gameObject);
    }
}

