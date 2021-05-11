using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public Animator transitionAnimator;
    public void Play(){
        StartCoroutine(LoadTransitions("Level1"));
    }

    public void PlayTutorial(){
        StartCoroutine(LoadTransitions("Tutorial"));
    }

    IEnumerator LoadTransitions(string scene){
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void Exit(){
        Application.Quit();
    }

}
