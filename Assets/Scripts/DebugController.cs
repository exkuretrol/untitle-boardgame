using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    private bool _showConsole;
    private bool _showHelp;
    private string _input;
    public List<object> commandList;
    
    public static DebugCommand Toggle_Moving;
    public static DebugCommand Toggle_Chosen;
    public static DebugCommand<string> Echo;
    public static DebugCommand Help;
    
    
    GameObject chess;

    public void OnToggleDebug(InputValue value)
    {
        _showConsole = !_showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (_showConsole)
        {
            HandleInput();
            _input = "";
        }
    }

    private void Awake()
    {
        // chess = GameObject.Find("ChessBase");
        // AnimationStateController animationStateController = chess.GetComponents<AnimationStateController>()[0];
        
        Toggle_Moving = new DebugCommand("toggleMoving", "觸發棋子移動動畫", "toggleMoving", () =>
        {
            Debug.Log("toggled.");
            // animationStateController.ToggleMoving();
        });

        Toggle_Chosen = new DebugCommand("toggleChosen", "觸發棋子被選中動畫", "toggleChosen", () =>
        {
            Debug.Log("toggled.");
            // animationStateController.ToggleChosen();
        });

        Echo = new DebugCommand<string>("echo", "你說什麼我就說什麼＝＝", "echo <字串>", (str) =>
        {
            Debug.Log(str);
        });

        Help = new DebugCommand("help", "指令幫助", "help", () =>
        {
            Debug.Log("Show help box.");
            _showHelp = !_showHelp;
        });
        
        commandList = new List<object>
        {
            Toggle_Moving,
            Toggle_Chosen,
            Echo,
            Help
        };
    }

    private Vector2 scroll;
    private void OnGUI()
    {
        if (!_showConsole) return;

        float y = 0f;

        if (_showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100;
        }
        
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
    }

    private void HandleInput()
    {
        string[] properties = _input.Split(' ');
        
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (_input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                     // Case to this type and invoke the command
                     (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }
}