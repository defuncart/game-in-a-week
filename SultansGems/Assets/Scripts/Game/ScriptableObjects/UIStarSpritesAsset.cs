/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using UnityEngine;

///<summary>An asset used to store filled and unfilled sprites for the UI.</summary>
//[CreateAssetMenu(fileName = "UIStarSprites", menuName = "UIStarSprites", order = 1000)]
public class UIStarSpritesAsset : ScriptableObject
{
    ///<summary>The unfilled star.</summary>
    public Sprite unfilled;
    ///<summary>The filled star.</summary>
    public Sprite filled;
}
