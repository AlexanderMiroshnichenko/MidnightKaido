using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "Scriptable Objects/PlayerScore")]
public class PlayerScore : ScriptableObject
{
    [SerializeField] public int score;


    public void UpdateStats()
    {

    }

}
