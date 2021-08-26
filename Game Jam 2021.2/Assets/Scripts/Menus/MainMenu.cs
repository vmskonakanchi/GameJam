<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }

    public void Quit()
    {

        //(Maybe add Buttons Animation)

        //Quits the Apllication
        Application.Quit();
    }




}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject titleScreen;
    Transform currentScreen;

    void Start()
    {
        currentScreen = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ShowNewScreen(optionsScreen);

    }

    public void Quit()
    {

        //(Maybe add Buttons Animation)

        //Quits the Apllication
        Application.Quit();
    }

    public void Menu()
    {

        //Open new Options Menu
        ShowNewScreen(titleScreen);

    }

    void ShowNewScreen(GameObject screen)
    {
        currentScreen.GetComponent<Animator>().SetTrigger("SwipeOut");
        screen.SetActive(true);
        


            

    }

    //Is an animation trigger (triggered by swipe out animation)
    public void DisableGameobject()
    {
        currentScreen.gameObject.SetActive(false);
    }



}
>>>>>>> Stashed changes
