/*
Manage the flow of the Simon game


Enrique Martinez de Velasco Reyna
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonController : MonoBehaviour
{
    [SerializeField] List<SimonButton> buttons;
    [SerializeField] List<int> sequence;
    [SerializeField] float initialDelay = 3f;
    [SerializeField] float minDelay = 0.6f;
    [SerializeField] float delayStep = 0.2f;
    [SerializeField] int level;
    [SerializeField] bool playerTurn = false;

    [SerializeField] int counter = 0;
    [SerializeField] int numButtons;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    float currentDelay;

    // Start is called before the first frame update
    void Start()
    {
        // Configure the buttons to be used in the game
        PrepareButtons();
        currentDelay = initialDelay;
    }

    // Configure the callback functions for the buttons
    void PrepareButtons()
    {
        for (int i = 0; i < numButtons; i++)
        {
            int index = i;
            // Create the copies of the button as children of the panel
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.GetComponent<Image>().color = Color.HSVToRGB((float)index / numButtons, 1, 1);
            newButton.GetComponent<SimonButton>().Init(index);
            buttons.Add(newButton.GetComponent<SimonButton>());
            buttons[i].gameObject.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(index));
        }
        // Start the game by adding the first button
        AddToSequence();

    }

    // Main function to validate that the button pressed by the user 
    // corresponds with the sequence generated by the CPU
    public void ButtonPressed(int index)
    {
        if (playerTurn)
        {
            if (index == sequence[counter++])
            {
                // Highlight the button selected by the player
                buttons[index].Highlight();
                if (counter == sequence.Count)
                {
                    // Finish the player turn to ensure no other actions are
                    // taken into account
                    playerTurn = false;
                    level++;
                    counter = 0;
                    AddToSequence();
                }
            }
            else
            {
                Debug.Log("Game Over!");
            }
        }
    }

    // Add another number to the sequence and display it
    void AddToSequence()
    {
        // Add a new button to the sequence
        sequence.Add(Random.Range(0, buttons.Count));
        StartCoroutine(PlaySequence());
    }

    // Display every button in the sequence so far
    IEnumerator PlaySequence()
    {
        // Add an initial delay before showing the sequence
        yield return new WaitForSeconds(initialDelay);
        foreach (int index in sequence)
        {
            buttons[index].Highlight();
            yield return new WaitForSeconds(currentDelay);
        }
        // Switch the turn over to the player
        playerTurn = true;
        // Reduce the delay for the next turn
        currentDelay = Mathf.Max(minDelay, currentDelay - delayStep);
    }
}
