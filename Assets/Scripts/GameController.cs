using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int[,] grid = new int[3, 3];
    [SerializeField] Vector2Int boxPos = new Vector2Int(0, 1);
    [SerializeField] Vector2Int cakePos = new Vector2Int(0, 1);
    [SerializeField] Vector2Int[] candyPos;
    [SerializeField] int candy;
    [SerializeField] Image[] starWin;
    public GameObject box, cake, winPanel, losePanel;
    private bool win = false;
    private bool isEnd = false;
    private bool isUp = false;
    private bool check = false;
    private float countdown = 45f;
    public TextMeshProUGUI countdownText;
    public string swipe;
    private int star=0;

    private void Start()
    {
        InitializeGame();
    }
    private void InitializeGame()
    {
        for(int i = 0;i<3;i++)
            for(int j = 0;j<3;j++)
            {
                grid[i, j] = 0;
            }
        grid[boxPos.x, boxPos.y] = 1;
        grid[cakePos.x, cakePos.y] = 2;
        if (candyPos != null && candyPos.Length > 0)
        {
            foreach (Vector2Int pos in candyPos)
            {
                grid[pos.x, pos.y] = candy;
            }
        }
    }
    private void Update()
    {
        if(!win && !isEnd)
        {
            if (countdown > 0)
            {
                int minutes = Mathf.FloorToInt(countdown / 60);
                int seconds = Mathf.FloorToInt(countdown % 60);
                countdownText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                countdown -= Time.deltaTime;
            }
            else
            {
                isEnd= true;
                losePanel.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.W) || swipe=="Up")
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.D) || swipe == "Right")
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.S) || swipe == "Down")
            {
                MoveDown();
            }
            if (Input.GetKeyDown(KeyCode.A) || swipe == "Left")
            {
                MoveLeft();
            }
        }
        else if(win && !isEnd)
        {
            if (countdown > 35f) star = 3;
            else if (countdown > 25) star = 2;
            else  star = 1;
            winPanel.SetActive(true);
            for (int i = 0; i<3;i++)
            {
                if(i<star) starWin[i].enabled = true;
                else starWin[i].enabled = false;
            }
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, star);
        }
        
    }
    private void MoveCake(Vector2Int direction)
    {
        if(cakePos.x<boxPos.x) isUp=true;
        else isUp=false;
        check = false;
        Vector2Int newCakePosition = cakePos + direction;
        while (newCakePosition.x >= 0 && newCakePosition.x < 3 && newCakePosition.y >= 0 && newCakePosition.y < 3)
        {
            if (grid[newCakePosition.x, newCakePosition.y] == 0)
            {
                grid[cakePos.x, cakePos.y] = 0;
                grid[newCakePosition.x, newCakePosition.y] = 2;
                cakePos = newCakePosition;
            }
            else if (grid[newCakePosition.x, newCakePosition.y] == 1)
            {
                if (isUp)
                {
                    grid[cakePos.x, cakePos.y] = 1;
                    grid[newCakePosition.x, newCakePosition.y] = 2;
                    cakePos = newCakePosition;
                    win = true;
                    break;
                }
                else
                {
                    check = true;
                    MoveBox(direction);
                }

            }
            else break;
             newCakePosition += direction;
        }
        for (int i = 0; i < 3; i++)
        {
            if (cakePos.x == i)
            {
                cake.transform.position = new Vector3((cakePos.y - 1) * 1.3f, (cakePos.x + 1 - i * 2) * 1.3f, 89.43f);
                break;
            }
        }
        Debug.Log("Cake "+cakePos);
    }
    private void MoveBox(Vector2Int direction)
    {
        Vector2Int newBoxPosition = boxPos + direction;
        while(newBoxPosition.x >= 0 && newBoxPosition.x < 3 && newBoxPosition.y >= 0 && newBoxPosition.y < 3)
        {
            if (grid[newBoxPosition.x, newBoxPosition.y] == 0)
            {
                
                if (check)
                {
                    grid[cakePos.x, cakePos.y] = 0;
                    grid[boxPos.x, boxPos.y] = 2;
                    cakePos = boxPos;
                }
                grid[boxPos.x, boxPos.y] = 0;
                grid[newBoxPosition.x, newBoxPosition.y] = 1;
                boxPos = newBoxPosition;
            }
            else if(grid[newBoxPosition.x, newBoxPosition.y] == 2)
            {
                if(isUp)
                {
                    grid[boxPos.x, boxPos.y] = 2;
                    grid[newBoxPosition.x, newBoxPosition.y] = 1;
                    boxPos = newBoxPosition;
                    win = true;
                    break;
                }
            }
            else break;

            newBoxPosition += direction;
        }
        for(int i=0;i<3;i++)
        {
            if (boxPos.x == i)
            {
                box.transform.position = new Vector3((boxPos.y - 1) * 1.3f, (boxPos.x + 1 - i*2) * 1.3f, 88.43f);
                break;
            }
        }
        Debug.Log("Box " + boxPos);
        
    }
    private void MoveUp()
    {
        MoveCake(new Vector2Int(-1, 0));
        if(!win && !check) 
            MoveBox(new Vector2Int(-1, 0));

    }
    private void MoveDown()
    {
        MoveCake(new Vector2Int(1, 0));
        if (!win && !check)
            MoveBox(new Vector2Int(1, 0));

    }
    private void MoveLeft()
    {
        MoveCake(new Vector2Int(0, -1));
        if (!win && !check )
            MoveBox(new Vector2Int(0, -1));
    }
    private void MoveRight()
    {
        MoveCake(new Vector2Int(0, 1));
        if (!win && !check)
            MoveBox(new Vector2Int(0, 1));
    }
}
