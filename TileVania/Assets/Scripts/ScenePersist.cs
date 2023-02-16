using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // purpose of function is to keep track of all the actions you've done before you lose all 3 lives or proceed to new stage
    void Awake() // gets called when we click play button or player dies and restart level scene
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length; //used legenth since objoftype is an array data type
        if(numScenePersists > 1) { //when the function is called a new script is awaken so destory the new one
            Destroy(gameObject);
        }
        else { 
            DontDestroyOnLoad(gameObject); //each time a scene is loaded dont destroy it
        }
    }

    public void ResetScenePersist() //called in levelexit and gamesession(after 3 deaths)
    {
        Destroy(gameObject);
    }
}
