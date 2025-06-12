using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorName
{
    
  Red,
  Green,
  Blue,
  Cian
   
}

public static class UIHelperTool 
{
    public static string GetColor(ColorName colorName)
    {
        string colorcode;
        
        switch (colorName)
        {
            case ColorName.Red:
                return colorcode ="#df0101";
            case ColorName.Green:
                return colorcode ="#29b62e";
            case  ColorName.Blue:
                return colorcode ="#432afc";
            case ColorName.Cian:
                return colorcode ="#01dfcc";
                default :
                return colorcode = "#FFFFFF";
        }

       
    }


    public static string ColorText(string text,ColorName colorName)
    {
        return $"<color={GetColor(colorName)}>+{text}</color>";

    }
}
