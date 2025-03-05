using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    IEnumerator Count(int count)
    {
        for (var i = 0; i < count; i++)
        {
            Debug.Log(i+1);
        }
        
        yield return null;
    }
    void Start()
    {
        RPSPlay rspPlay = new()
        {
            player = new CharacterInfo(),
            npc = new CharacterInfo()
        };

        StartCoroutine(Count(5));
        Debug.Log(rspPlay.Play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
