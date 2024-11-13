using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private string objectID;

    void Awake()
    {
        // Create a unique key to store the object's ID
        string uniqueKey = gameObject.name + "_" + "_ID";

        // Retrieve or generate the objectID
        objectID = PlayerPrefs.GetString(uniqueKey, "");
        if (string.IsNullOrEmpty(objectID))
        {
            // Generate a new ID and store it
            objectID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString(uniqueKey, objectID);
            PlayerPrefs.Save();
        }
    }

    // Call this function only when loading a saved game
    public void LoadGame()
    {
        // Check if the object has already been destroyed in a previous session
        if (PlayerPrefs.GetInt(objectID) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt(objectID, 0);
        PlayerPrefs.Save();
    }

    public void PickupMe()
    {
        // Destroy the object and save its state
        Destroy(gameObject);
        PlayerPrefs.SetInt(objectID, 1);
        PlayerPrefs.Save();
    }
}
