using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rarity
{    
    [SerializeField] private Level rarityLevel;

    public string RarityLevel => $"{rarityLevel}";
    public Color Color
    {
        get
        {
            if (!rarityColors.ContainsKey(rarityLevel)) 
            {
                Debug.LogError($"Rarity level {rarityLevel} doesn't have an associated color.");
                return Color.white; 
            }

            return rarityColors[rarityLevel];
        }
    }

    private enum Level
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4
    }

    private Dictionary<Level, Color> rarityColors = new Dictionary<Level, Color>()
    {
        { Level.Common, Color.grey },
        { Level.Uncommon, Color.green },
        { Level.Rare, Color.blue },
        { Level.Epic, Color.magenta },
        { Level.Legendary, Color.yellow },
    };
}
