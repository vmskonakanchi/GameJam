using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;

    [SerializeField] string mainMenuSceneName;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {

        //Make the Pause Menu disapear (depends on how it appears)
        //Solution for now
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void BackToMainMenu()
    {
        //Set Game back to main
        SceneManager.LoadScene(mainMenuSceneName);
        

    }
}
