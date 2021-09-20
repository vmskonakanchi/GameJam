
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;

    [SerializeField] string mainMenuSceneName;

    bool isPaused = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

        }

        PauseScreen();
    }

    public void Play()
    {

        //Make the Pause Menu disapear
        isPaused = false;

    }

    public void BackToMainMenu()
    {
        //Set Game back to main
        SceneManager.LoadScene(mainMenuSceneName);
        isPaused = false;

    }

    void PauseScreen()
    {
        if(isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Retry()
    {
        Scene presentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(presentScene.name);
    }
}
