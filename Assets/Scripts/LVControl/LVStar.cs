using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LVStar : MonoBehaviour
{
    public int star = 0;
    [SerializeField] Image[] starWin;
    private void Update()
    {
        star=PlayerPrefs.GetInt(gameObject.name, 0);
        for (int i = 0; i < 3; i++)
        {
            if (i < star) starWin[i].enabled = true;
            else starWin[i].enabled = false;
        }
    }
}
