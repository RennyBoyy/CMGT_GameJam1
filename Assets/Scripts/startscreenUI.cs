using UnityEngine;
using UnityEngine.UIElements;

public class StartScreenUI : MonoBehaviour
{
    public UIDocument uiDocument;
    private Button playButton;
    private PlayerController playerController;

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        // Get Play button and player controller component
        playButton = root.Q<Button>("Play");
        playerController = FindObjectOfType<PlayerController>(); // Assuming PlayerController is attached to the player

        // Add listener to the Play button
        if (playButton != null)
            playButton.clicked += StartGame;
        else
            Debug.LogError("PlayButton not found!");
    }

    void StartGame()
    {
        // Hide the UI after clicking Play
        var root = uiDocument.rootVisualElement;
        root.style.display = DisplayStyle.None;

        // Enable player movement
        if (playerController != null)
        {
            playerController.EnableMovement();
        }

        // Start the game logic here (e.g., load scene, etc.)
        Debug.Log("Play button clicked! Game is starting...");
    }
}
