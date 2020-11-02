using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;


    private void Awake()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a == 0)
        {
            //Map is colourless, so do nothing!...
            return;
        }

        Vector2 pos = new Vector2(x, y);

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color == pixelColor)
            {
                Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
            }
        }
    }

}
