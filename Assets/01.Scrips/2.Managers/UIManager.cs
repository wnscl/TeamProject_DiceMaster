using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
/// ColorText 함수용 컬러
/// </summary>
public enum ColorName
{
    red,
    green,
    blue,
    cyan,
    yellow,
    orange,
    purple,
   magenta
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this)
            Destroy(gameObject);
    }

    public GameObject playerSettingWindow;
    public Inventory inventory;
    public StatusPanel statusPanel;
    public SkillPanel skillPanel;
    public SkillInfo skillInfo;
    public ItemInfo itemInfo;
    public SkillShortCut skillShortcut;
    public BattleWindow battleWindow;
    public EquipSlot equipSlot;

    //시스템 메세지용 필드
    public Image systemMessageImage;
    public TextMeshProUGUI systemText;

    private Coroutine systemMessageRoutine;
    [SerializeField] private string dialogCall;

    private NpcScriptUI npcScriptUI;

    public NpcScriptUI NpcScriptUI
    {
        get { return npcScriptUI; }
        set { npcScriptUI = value; }
    }

    


    private static string GetColor(ColorName colorName)
    {
        switch (colorName)
        {
            case ColorName.red:
                return "#ff0000";         // 빨강
            case ColorName.green:
                return "#00ff00";         // 초록
            case ColorName.blue:
                return "#0000ff";         // 파랑
            case ColorName.cyan:
                return "#00ffff";         // 청록
            case ColorName.yellow:
                return "#ffd700";         // 노랑 (골드 느낌)
            case ColorName.orange:
                return "#ff8c00";         // 주황 (다크 오렌지)
            case ColorName.purple:
                return "#800080";         // 보라
            case ColorName.magenta:
                return "#ff00ff";         // 마젠타
            default:
                return "#000000";         // 기본값: 검정
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

    public void SystemMessage(string text, float delay = 1, bool isDialog = false)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        if (systemMessageRoutine != null)
        {
            StopCoroutine(systemMessageRoutine);
        }

        systemMessageRoutine = StartCoroutine(SystemText(text, delay, isDialog));
    }

    private IEnumerator
        SystemText(string text, float delay = 1, bool isDialog = false) //대화용으로 쓰고 싶으면 "대화" 스트링 입력 이것은 의견에 따라 바꾸면 됩니다이 
    {
        systemText.text = text;
        systemMessageImage.gameObject.SetActive(true);

        if (!isDialog)
        {
            yield return new WaitForSeconds(delay);
        }
        else if (isDialog)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
        }

        systemMessageImage.gameObject.SetActive(false);

        systemText.text = string.Empty;

        systemMessageRoutine = null;
    }


    public void OnSettingWindow(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;

        if (playerSettingWindow.activeInHierarchy)
        {
            playerSettingWindow.SetActive(false);
            inventory.gameObject.SetActive(true);
            statusPanel.gameObject.SetActive(false);
            skillPanel.gameObject.SetActive(false);
            itemInfo.gameObject.SetActive(false);
            itemInfo.ResetInfo();
            skillInfo.gameObject.SetActive(false);
            skillInfo.ResetInfo();
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Unpause);            
        }
        else
        {
            playerSettingWindow.SetActive(true);
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Pause);
        }
    }
}