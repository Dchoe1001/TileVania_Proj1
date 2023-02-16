using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //extension for textmeshpro

public class GameSession : MonoBehaviour
{
    [SerializeField] int playersLive = 3;
    [SerializeField] float delayTime = 1f;
    int currentScore = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;
    // runs before start
    void Awake() // gets called when we click play button or player dies and restart level scene
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length; //used legenth since objoftype is an array data type
        if(numGameSessions > 1) { //when the function is called a new script is awaken so destory the new one
            Destroy(gameObject);
        }
        else { 
            DontDestroyOnLoad(gameObject); //each time a scene is loaded dont destroy it
        }
    }
    
    void Start()
    {
        livesText.text = playersLive.ToString(); //print player lives
        scoresText.text = currentScore.ToString();
    }

    public void ProcessPlayerDeath() //gets called from playermovement die function
    {
        if(playersLive > 1) {
            Invoke("TakeLifePlayer", delayTime);
        }
        else {
            Invoke("ResetGameSession", delayTime); // no lives restart game
        }
    }

    public void AddToScore(int pointsToAddScore) // called in coinpickup
    {
        currentScore += pointsToAddScore;
        scoresText.text = currentScore.ToString();

    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0); //load first level
        Destroy(gameObject); //make a new instance of gamesession
        FindObjectOfType<ScenePersist>().ResetScenePersist();
    }

    void TakeLifePlayer()
    {
        playersLive--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //getting current level index #
        SceneManager.LoadScene(currentSceneIndex); // load level index #
        livesText.text = playersLive.ToString();
    }
}
