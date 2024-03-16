using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Button[] buttons;
    private void Awake()
    {
        int unlock = PlayerPrefs.GetInt("unlock", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlock; i++)
        {
            buttons[i].interactable = true;
        }
    }
}