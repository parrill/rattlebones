using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;
    private Vector2 lastExitPosition; // Store the last exit position

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Store the current position as the last exit position
            lastExitPosition = other.transform.position;
            Debug.Log(lastExitPosition);

            // Switch to the new scene
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Store the last exit position when leaving the trigger zone
            lastExitPosition = other.transform.position;
        }
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == sceneBuildIndex)
        {
            // Find the player in the newly loaded scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // Set the player's position to the last exit position
                player.transform.position = lastExitPosition;
            }
        }
    }
}