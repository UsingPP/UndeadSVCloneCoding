using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update

    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.position = Camera.main.WorldToScreenPoint(Gamemanager.instance.player.transform.position);
    }
}
