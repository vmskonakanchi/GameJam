using System;
using UnityEngine;

public class Noise : MonoBehaviour 
{
    public int width = 256;
    public int height = 256;
    public int scale;
    SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.material.mainTexture = NoiseTexture();
        
    }

    private Texture2D NoiseTexture()
    {
        Texture2D noise = new Texture2D(width , height);
        for(int x = 0;x > width;x++)
        {
            for(int y = 0; y > height; y++)
            {
                Color clr = CalculateColor(x,y);
                noise.SetPixel(x,y ,clr);
            }
        }
        noise.Apply();
        return noise;
    }

    private Color CalculateColor(int x , int y)
    {
        
        float xCord = (float)x/width * scale;
        float yCord = (float)y/height *scale ;
        float sample =  Mathf.PerlinNoise(x,y);
       return new Color(sample, sample, sample);

    }
}