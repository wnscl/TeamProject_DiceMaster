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
    public static Vector3 GetDirection(this GameObject target, GameObject requester, Axis ignoreAxis = Axis.None) //�ɼų� �Ķ���� ���� �⺻���� none�� ����
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
            //Vector3.Distance�� �κ����� ���̸� ���ϰ� �װ� magnitude�� ��ȯ ��
            //(target - requester).magnitude�� �Ȱ���.
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
        //Unity�� * *"�� �����̶�� magnitude ��� sqrMagnitude�� ����϶�" * *�� �����Ѵ�.
        //��Ȯ�� ���� �ʿ� ���� �� ���ɻ� ������ Ȯ��
        //�� magnitude �� ��Ȯ�� ����� �ʿ��� �� �Ÿ��񱳸� ������ ���� ������ ��� (��ġ���� ����� �Ÿ����谡 �ʿ��� ��)
        //sqrMagnitude�� ������Ʈ���� ���ӵǴ� ���꿡�� �Ǵ� �Ÿ��񱳸� �� �� (��Ÿ��ȿ� ���Դ°�? ��)
    }
}
