﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    private BoardManager boardScript;
    [SerializeField]
    private Text ScoreText;
    public int score { get; private set; }
    private float time;

    public int level = 0;
    void Awake()
    {
        // khởi tạo instance
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //lấy reference của board manager (ngoài ra còn cách kéo thả thông qua inspector
        boardScript = GetComponent<BoardManager>();

        // gọi hàm InitGame
        InitGame();
    }

    void InitGame()
    {
        // gọi sang Board Manager
        boardScript.SetupScene(level);
        score = 0;
        time = 0;
        ScoreText.text = score.ToString();
    }

    
    private void Update()
    {
        
    }


    public Action OnPlayerDead;
    public void PlayerDead()
    {
        //OnPlayerDead?.Invoke();
        if (OnPlayerDead != null)
            OnPlayerDead();
    }
}
