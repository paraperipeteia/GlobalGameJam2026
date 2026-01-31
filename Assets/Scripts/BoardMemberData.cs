using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Board Member Data")]
public class BoardMemberData : ScriptableObject
{
    [SerializeField] 
    public Sprite avatar;

    [SerializeField] 
    public Color frameColor;

    [SerializeField] 
    public Color backgroundColor;

    [SerializeField] 
    public List<Sprite> maskSprites;
    
    [SerializeField]
    public BoardMemberType boardMemberType;
}