using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CC
{
    [System.Serializable]
    public struct Pixel
    {
        Tile pixelTile;
        public void Create(Sprite baseTileSprite, string name, Color32 color)
        {
            pixelTile = ScriptableObject.CreateInstance<Tile>();
            pixelTile.sprite = baseTileSprite;
            pixelTile.name = name;
            pixelTile.colliderType = Tile.ColliderType.Grid;
            pixelTile.color = color;
        }
        public Tile GetTile()
        {
            return pixelTile;
        }
    }
    [System.Serializable]
    public struct TerrainMaterial
    {
        public Color32 dark;
        public Color32 ambient;
        public Color32 bright;
        Pixel darkTile;
        Pixel ambientTile;
        Pixel brightTile;
        public void Create(Sprite baseTileSprite, string name)
        {
            darkTile.Create(baseTileSprite, name, dark);
            ambientTile.Create(baseTileSprite, name, ambient);
            brightTile.Create(baseTileSprite, name, bright);
        }
        public Tile GetDarkTile()
        {
            return darkTile.GetTile();
        }
        public Tile GetAmbientTile()
        {
            return ambientTile.GetTile();
        }
        public Tile GetBrightTile()
        {
            return brightTile.GetTile();
        }
    }
    [System.Serializable]
    public struct Structure 
    {

    }
}

public class CCResources : MonoBehaviour
{
    public Sprite baseTileSprite;
    public CC.TerrainMaterial sand;
    public CC.TerrainMaterial grass;
    public CC.TerrainMaterial prairie;
    public CC.TerrainMaterial rock;
    public CC.TerrainMaterial snow;
    public static CC.TerrainMaterial nullMaterial;
    public static CC.Pixel shore;
    public static CC.Pixel shadow;
    public static CC.Pixel nullPixel;

    void Awake()
    {
        sand.Create(baseTileSprite, "sand");
        grass.Create(baseTileSprite, "grass");
        prairie.Create(baseTileSprite, "prairie");
        rock.Create(baseTileSprite, "rock");
        snow.Create(baseTileSprite, "snow");
        shore.Create(baseTileSprite, "shore", new Color32(136, 213, 217, 255));
        shadow.Create(baseTileSprite, "shadow", new Color32(40, 40, 40, 255));
        nullMaterial.Create(baseTileSprite, "null");
        nullPixel.Create(baseTileSprite, "shadow", new Color32(40, 40, 40, 255));
    }
}
