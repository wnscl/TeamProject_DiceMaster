using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// UI도구 같은 것 들 테스트 할 스크립트입니다. 용도를 다하면 삭제할거에요
/// </summary>
public class UITest : MonoBehaviour
{
    void Start()
    {
        
    }
    [Button]
    public void TestSysMS()
    {
        UIManager.Instance.SystemMessage("이것은 테스트 문구여" + UIManager.ColorText("이것은 색깔 글자 함수 테스트이고", ColorName.red) + "그래요",2);
    }
    [Button]
   public void TestSysMS2()
    {
        UIManager.Instance.SystemMessage("이것은 대화용 텍스트 테스트 문구여" + UIManager.ColorText("이것은 색깔 글자 함수 테스트이고", ColorName.red) + "아무튼 그렇습니다.",0,true);
    }
}