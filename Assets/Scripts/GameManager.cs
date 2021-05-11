using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoresText;
    public Text newHighScoreText;
    public bool gameOver;
    private int brickNumber;
    private int seed;
    private int randomValuesPointer;
    public float[] randomValues = new float[250];
    public Animator transitionAnimator;
    public GameObject gameOverPanel;
    public GameObject brickGroup;
    public bool runReplay = false;
    public bool recordingFinished = false;
    bool scoresUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        brickNumber = GameObject.FindGameObjectsWithTag("Brick").Length;
        randomValuesPointer = -1;
        GenerateRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateRandom(){
        seed = Random.Range(0,int.MaxValue);
        Random.InitState(seed);
        int i = 0;
        while (i < randomValues.Length) {
            randomValues[i] = Random.value;
            i++;
        }
    }
    public float GetRandomValue(){
        if(randomValuesPointer >= randomValues.Length){
            randomValuesPointer = -1;
        }
        randomValuesPointer++;
        return randomValues[randomValuesPointer];
    }
    public void LifeLost(){
        lives -= 1;
        if(lives <= 0){
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives; 
    }

    public void LifeGain(){
        lives++;
        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int addScore){
        score += addScore;
        if(score < 0){
            score = 0;
        }
        scoreText.text = "Score: " + score;
    }

    public void GameOver(){
        SetHighScore();       
        gameOver = true;
        recordingFinished = true;  
        gameOverPanel.SetActive(true);
    }

    public void UpdateBrickNumber(){
        brickNumber--;
        if(brickNumber < 1){
            GameOver();
        }
    }
    
    private void SetHighScore(){
        int[] HighScores = new int[3];
        bool newHighScore = false;
        for(int i=0;i<3;i++){
            HighScores[i] = PlayerPrefs.GetInt("HIGHSCORE" + i);
            if(score > HighScores[i]){
                newHighScore = true;
            }
        }
        if(newHighScore && !scoresUpdated){
            newHighScoreText.text = "New High Score!";
            for(int i=0;i<3;i++){
                if(score > HighScores[i]){
                    int tempScore = HighScores[i];
                    HighScores[i] = score;
                    score = tempScore;
                }
            }
            for(int i=0;i<3;i++){
                PlayerPrefs.SetInt("HIGHSCORE" + i, HighScores[i]);
            }
            scoresUpdated = true;
        }
        highScoresText.text = "1#: " + HighScores[0] + "\r\n" +"2#: " + HighScores[1] + "\r\n" + "3#: " + HighScores[2];
    }

    IEnumerator LoadTransitions(string scene){
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void PlayAgain(){
        StartCoroutine(LoadTransitions("Level1"));
    }
    public void Replay(){
        //DestroyImmediate(brickGroup, true);
        Instantiate(brickGroup);
        brickNumber = GameObject.FindGameObjectsWithTag("Brick").Length;
        score = 0;
        UpdateScore(0);
        randomValuesPointer = -1;
        gameOverPanel.SetActive(false);
        gameOver = false;
        runReplay = true;
    }

    public void Exit(){
        Application.Quit();
    }
}
