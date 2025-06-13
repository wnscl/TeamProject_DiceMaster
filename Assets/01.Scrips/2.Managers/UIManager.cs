using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorName
{
    red,
    green,
    blue,
    cyan
}


public class UIManager : MonoBehaviour
{
 public static UIManager Instance;
    void Awake()
    {
        if (Instance == null)
            {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            }
        else
        {
            Destroy(gameObject);
        }
    }

    
    
    
    
    
    
    
    
    
    
    
    public static string GetColor(ColorName colorName)
    {
        switch (colorName)
        {
            case ColorName.red:
                return  ColorName.red.ToString();
            case ColorName.green:
                return  ColorName.green.ToString();
            case ColorName.blue:
                return  ColorName.blue.ToString();
            case ColorName.cyan:
                return ColorName.cyan.ToString(); ;
            default:
                return  "black";
        }
    }


    public static string UIColorText(string text, ColorName colorName)
    {
        return $"<color={GetColor(colorName)}>{text}</color>";
    }
   
}
