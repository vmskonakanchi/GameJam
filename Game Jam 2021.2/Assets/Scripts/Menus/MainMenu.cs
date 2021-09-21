using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform optionScreen;
    [SerializeField] Transform mainScreen;

    Transform currentScreen;


    // Start is called before the first frame update
    void Awake()
    {
        currentScreen = GetComponent<Transform>();

    }
    private void Start() 
    {
        currentScreen.gameObject.GetComponent<Animator>().Play("SwipeIn");
    }
    public void Play()
    {

        //(Maybe add Buttons Animation)

        //Get Player into next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {

        //Open new Options Menu
        CloseScreen(mainScreen);
        OpenScreen(optionScreen,"SwipeIn");

    }

    public void Quit()
    {

        //(Maybe add Buttons Animation)

        //Quits the Apllication
        Application.Quit();
    }

    public void OpenScreen(Transform screen, string Animation)
    {
            screen.gameObject.SetActive(true);           
            screen.gameObject.GetComponent<Animator>().Play(Animation);    
    }
    private void CloseScreen(Transform screen)
    {
        screen.gameObject.SetActive(false);
    }
}
