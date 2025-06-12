using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ColorName
{
    Red,
    Green,
    Blue,
    Cyan
}

    

    public static class UIHelperTool//오류가 있음 
    {
        public static string GetColor(ColorName colorName)
        {
            string colorcode;

            switch (colorName)
            {
                case ColorName.Red:
                    return colorcode = "red";
                case ColorName.Green:
                    return colorcode = "#green";
                case ColorName.Blue:
                    return colorcode = "#blue";
                case ColorName.Cyan:
                    return colorcode = "#cyan";
                default:
                    return colorcode = "black";
            }
        }


        public static string UIColorText(string text, ColorName colorName)
        {
            return $"<color={GetColor(colorName)}>{text}</color>";
        }
    }