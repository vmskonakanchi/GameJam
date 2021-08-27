using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    
    public GameObject highlightedSprite;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonHighlight()
    {
        highlightedSprite.SetActive(true);
    }

    public void NormalButtonState()
    {
        highlightedSprite.SetActive(false);
    }
}
