using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComplexDialogue", menuName = "Custom Assets/Create new dialogue option")]
public class ComplexDialogue : ScriptableObject
{
    [TextArea]
    public string dialogueLine;
    public string response1;
    public int specialAction1;
    public string response2;
    public int specialAction2;

}