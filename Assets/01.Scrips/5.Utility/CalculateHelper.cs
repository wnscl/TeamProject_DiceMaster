using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Axis
{
    None,
    X,
    Y,
    Z
}

public static class CalculateHelper
{
    public static Vector3 GetDirection(this GameObject target, GameObject requester, Axis ignoreAxis = Axis.None) //옵셔널 파라미터 현재 기본값이 none인 상태
    {
        if (target == null || requester == null)
        {
            return Vector3.zero;
        }

        Vector3 direction = Vector3.zero;
        direction = (target.transform.position - requester.transform.position);

        if (ignoreAxis == Axis.None)
        {
            return direction.normalized;
        }

        switch (ignoreAxis)
        {
            case Axis.X:
                direction.x = 0f;
                break;

            case Axis.Y:
                direction.y = 0f;
                break;

            case Axis.Z:
                direction.z = 0f;
                break;
        }

        return direction.normalized;
    }

    public static Quaternion GetRotation(this GameObject requester, Vector3 direction, Axis aliveAxis = Axis.None)
    {
        Quaternion rotation = requester.transform.rotation;
        if (aliveAxis == Axis.None)
        {
            rotation = Quaternion.LookRotation(direction);
        }


        return rotation;
    }

    public static float GetDistance(this Vector3 target, Vector3 requester, Axis ignoreAxis = Axis.None)
    {
        if (ignoreAxis == Axis.None)
        {
            float distance = 0f;
            distance = Vector3.Distance(target, requester);
            //Vector3.Distance는 두벡터의 차이를 구하고 그걸 magnitude로 반환 즉
            //(target - requester).magnitude와 똑같다.
            return distance;
        }

        Vector3 direction = target - requester;

        switch (ignoreAxis)
        {
            case Axis.X:
                direction.x = 0f;
                break;

            case Axis.Y:
                direction.y = 0f;
                break;

            case Axis.Z:
                direction.z = 0f;
                break;
        }

        return direction.magnitude;
        //Unity는 * *"비교 목적이라면 magnitude 대신 sqrMagnitude를 사용하라" * *고 권장한다.
        //정확한 값이 필요 없을 땐 성능상 이점이 확실
        //즉 magnitude 는 정확한 계산이 필요할 때 거리비교를 제외한 실제 물리적 계산 (수치보다 상대적 거리관계가 필요할 때)
        //sqrMagnitude는 업데이트같은 지속되는 연산에서 또는 거리비교를 할 때 (사거리안에 들어왔는가? 등)
    }
}
