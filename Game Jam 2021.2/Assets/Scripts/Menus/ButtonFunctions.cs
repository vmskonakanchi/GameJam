<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    Sprite normalSprite;
    public Sprite highlightedSprite;

    Image childImage;

    void Start()
    {
        childImage = GetComponentInChildren<Image>();
        normalSprite = childImage.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonHighlight()
    {
        childImage.sprite = highlightedSprite;
    }

    public void NormalButtonState()
    {
        childImage.sprite = normalSprite;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    
    public GameObject highlightedSprite;

    Image childImage;

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
>>>>>>> Stashed changes
