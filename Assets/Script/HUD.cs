using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
   public enum InfoType {  Exp, Level, Kill, Time, Health}
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float currentExp = Gamemanager.instance.exp;
                float maxExp = Gamemanager.instance.nextExp[Mathf.Min(Gamemanager.instance.level, Gamemanager.instance.nextExp.Length - 1)];

                mySlider.value = currentExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("LeveL : {0:F0}", Gamemanager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("LeveL : {0:F0}", Gamemanager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = Gamemanager.instance.maxGameTime - Gamemanager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2} : {1:D2}", min, sec);
                break;
            case InfoType.Health:
                mySlider.value = (float)Gamemanager.instance.health / (float)Gamemanager.instance.maxHealth;

                break;
        }
    }
}
