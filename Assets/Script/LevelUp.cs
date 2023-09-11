using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] Items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        Items = GetComponentsInChildren<Item>(true);
    }

    public void Show() {
        Next();
        rect.localScale = Vector3.one;
        Gamemanager.instance.Stop();
    }
    public void Hide() {
        rect.localScale = Vector3.zero;
        Gamemanager.instance.Resume();
    }

    public void Select(int index)
    {
        Items[index].onClick();
    }

    void Next()
    {
        //모든 아이템 비활성화
        foreach (Item item in Items) {
            item.gameObject.SetActive(false);
        }

        //그중에서 랜덤하게 세 개의 아이템 활성화
        int[] ran = new int[3];
        while(true)
        {
            ran[0] = Random.Range(0, Items.Length);
            ran[1] = Random.Range(0, Items.Length);
            ran[2] = Random.Range(0, Items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0] )
                break;
        }

        for (int idx = 0; idx < ran.Length; idx++)
        {
            Item randItem = Items[ran[idx]];
            //3. 만랩 아이템의 경우는 소비아이템으로 대체
            if (randItem.level == randItem.data.damages.Length) {
                Items[4].gameObject.SetActive(true);
            }
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }


    }
}
