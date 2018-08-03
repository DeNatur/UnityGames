using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Animator anim;
    public GameObject playButton;
    public GameObject selectModeButton;

    private void Start()
    {
        playButton.SetActive(true);
        selectModeButton.SetActive(false);
    }
    public void PlayButton()
    {
        anim.SetBool("screenup",true);
        StartCoroutine("Wait");
        //SceneManager.LoadScene(1);
    }
    public void AimButton()
    {
        SceneManager.LoadScene("game");
    }
    public void AdventureButton()
    {
        SceneManager.LoadScene("adventure");
    }
    public void Restart()
    {
        Debug.Log("YEP");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        playButton.SetActive(false);
        selectModeButton.SetActive(true);
        anim.SetBool("screenup", false);
    }
}
