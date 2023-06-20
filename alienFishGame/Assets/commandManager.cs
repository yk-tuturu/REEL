using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class commandManager : MonoBehaviour
{
    public Dictionary<string, Delegate> commandData = new Dictionary<string, Delegate>();
    public static commandManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        } 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Execute("shake");
        }
    }

    public void Execute(string commandName)
    {
        if (!commandData.ContainsKey(commandName))
        {
            return;
        }
        Delegate command = commandData[commandName];
        command.DynamicInvoke();
    }
}
