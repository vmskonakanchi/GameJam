using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    private MainMenu menu;
    private AudioManager audioManager;
    public GameObject highlightedSprite;

    private void Start()
    {
        menu = FindObjectOfType<MainMenu>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ButtonHighlight()
    {
        audioManager.PlaySound("iconClick");
        highlightedSprite.SetActive(true);
    }

    public void NormalButtonState()
    {
        highlightedSprite.SetActive(false);
    }

    public void ShowMenu()
    {
         SceneManager.LoadScene("MainMenu");
    }
}
