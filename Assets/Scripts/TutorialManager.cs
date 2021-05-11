using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    int tutorialStage;
    float timer;
    string[] messages = {"You use the left and right arrows to move",
                        "Try moving to the left",
                        "Now try moving to the right",
                        "The ball will follow the paddle until it is in play",
                        "Movement has been disabled again",
                        "To put the ball in play press spacebar",
                        "The ball will always shoot straight upwards",
                        "When the ball is not returned you lose a life and the ball is reset",
                        "When you lose all lives its game over"};
    public Text tutorialMessage;
    public GameObject paddle;
    TutorialPaddleScript paddleScript;
    public GameObject ball;
    TutorialBallScript tutorialBallScript;
    private int brickNumber;
    public Animator transitionAnimator;

    void Start()
    {
        tutorialBallScript = ball.GetComponent<TutorialBallScript>();
        paddleScript = paddle.GetComponent<TutorialPaddleScript>();
        tutorialStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        updateStage();
        brickNumber = GameObject.FindGameObjectsWithTag("Brick").Length;
    }
    
    private void updateStage(){
        switch (tutorialStage)
        {
            case 1:
                tutorialBallScript.disabled = true;
                paddleScript.disabledLeft = false;
                paddleScript.disabledRight = true;
                if(paddleScript.position < -3){
                    tutorialStage++;
                }
                tutorialMessage.text = messages[0] + "\r\n" + messages[1];
                break;
            case 2:
                tutorialBallScript.disabled = true;
                paddleScript.disabledLeft = true;
                paddleScript.disabledRight = false;
                if(paddleScript.position > 3){
                    tutorialStage++;
                }
                tutorialMessage.text = messages[0] + "\r\n" + messages[2];
                break;
            case 3:
                paddleScript.disabledLeft = false;
                paddleScript.disabledRight = false;
                timer += Time.deltaTime;
                if (timer >= 10f) {
                    timer = timer % 1f;
                    tutorialStage++;
                }
                tutorialMessage.text = messages[3];
                break;
            case 4:
                paddleScript.resetPosition();
                tutorialBallScript.disabled = false;
                paddleScript.disabledLeft = true;
                paddleScript.disabledRight = true;
                tutorialMessage.text = messages[4] + "\r\n" + messages[5] + "\r\n" + messages[6];
                break;
            case 5:
                paddleScript.resetPosition();
                tutorialBallScript.disabled = true;
                paddleScript.disabledLeft = false;
                paddleScript.disabledRight = false;
                tutorialMessage.text = messages[7] + "\r\n" + messages[8];
                timer += Time.deltaTime;
                if (timer >= 10f) {
                    timer = timer % 1f;
                    MainMenu();
                }
                break;
        }
    }

    IEnumerator LoadTransitions(string scene){
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void UpdateBrickNumber(){
        brickNumber--;
        if(brickNumber < 1){
            tutorialStage++;
        }
    }

    public void MainMenu(){
        StartCoroutine(LoadTransitions("MainMenu"));
    }
}
