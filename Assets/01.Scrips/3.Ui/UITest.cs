using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI도구 같은 것 들 테스트 할 스크립트입니다. 용도를 다하면 삭제할거에요
/// </summary>
public class UITest : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.SystemMessage("이것은 테스트 문구여" + UIManager.ColorText("이것은 색깔 글자 함수 테스트이고", ColorName.red)
                                                       + "어땨 잘 되지?");
    }
}
