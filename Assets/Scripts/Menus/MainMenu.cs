using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]Transform optionScreen;
    [SerializeField] Transform mainScreen;

    Transform currentScreen;


    // Start is called before the first frame update
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
        OpenNewScreen(optionScreen);

    }

    public void Quit()
    {

        //(Maybe add Buttons Animation)

        //Quits the Apllication
        Application.Quit();
    }

    void OpenNewScreen(Transform screen)
    {
        currentScreen.GetComponent<Animator>().SetTrigger("SwipeOut");
        screen.gameObject.SetActive(true);
        
    

    }





}
