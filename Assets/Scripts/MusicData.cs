using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    public AudioClip musicMainMenu;
    public AudioClip musicGamePlay;
}
