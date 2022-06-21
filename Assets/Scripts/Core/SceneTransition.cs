using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private int transitionStartHash = Animator.StringToHash("Start");
    private int transitionEndHash = Animator.StringToHash("Start");

    public Animator animator;

    private void Start()
    {
        animator.SetTrigger(transitionEndHash);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }


    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(StartTransition(sceneName));
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private IEnumerator StartTransition(string sceneName)
    {
        animator.SetTrigger(transitionStartHash);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
