using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// System to create and easily apply presets
/// </summary>
public class Preset : MonoBehaviour
{
    public Color backgroundColor = Color.black;
    public Color textColor = Color.white;

    public DialogueMakerOptions optionsScript;

    public void AssignPreset()
    {
        optionsScript.ApplyPreset(backgroundColor, textColor);
    }
}
