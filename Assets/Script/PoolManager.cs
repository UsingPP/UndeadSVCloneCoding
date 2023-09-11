using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ... ������ ���� ����
    public GameObject[] prefebs;

    // ... Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefebs.Length];
        for (int i = 0; i < prefebs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    } 

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach ( GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate<GameObject>(prefebs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}
