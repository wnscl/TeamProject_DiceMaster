using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ColorText 함수용 컬러
/// </summary>
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

    public Inventory inventory;


    //시스템 메세지용 필드
    public Image systemMessageImage;
    public TextMeshProUGUI systemText;
    private Coroutine systemMessageRoutine;
    

    private void Start()
    {
    }


    private static string GetColor(ColorName colorName)
    {
        switch (colorName)
        {
            case ColorName.red:
                return ColorName.red.ToString();
            case ColorName.green:
                return ColorName.green.ToString();
            case ColorName.blue:
                return ColorName.blue.ToString();
            case ColorName.cyan:
                return ColorName.cyan.ToString();
                ;
            default:
                return "black";
        }
    }

    /// <summary>
    /// string에 색 입혀서 반환해주는 매서드입니다
    /// </summary>
    /// <param name="text">출력 할 글자 써주시면 됩니다.</param>
    /// <param name="colorName">색깔 골라주시면 됩니다.</param>
    /// 혹시 사용하시거나 색 추가 하시려면 ColorName 열거형에 색상이름 추가해주시고
    /// GetColor 함수에 Case추가하셔서 색상이름이 기본 제공 색일 때는 똑같이 열거형 스트링형변환 해주시면 되고
    /// 기본 제공색상에 없는 색이면, 칼라코드 검색하셔서 해당 코드 스트링으로 반환하게 추가 해주시면 됩니다.
    /// <returns></returns>
    public static string ColorText(string text, ColorName colorName)
    {
        return $"<color={GetColor(colorName)}>{text}</color>";
    }

    public void SystemMessage(string text,float delay)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        if (systemMessageRoutine != null)
        {
            StopCoroutine(systemMessageRoutine);
        }

        systemMessageRoutine = StartCoroutine(SystemText(text,delay));
    }

    private IEnumerator SystemText(string text, float delay = 1)
    {
        systemText.text = text;
        systemMessageImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        systemMessageImage.gameObject.SetActive(false);
        systemText.text = string.Empty;

        systemMessageRoutine = null;
    }
}