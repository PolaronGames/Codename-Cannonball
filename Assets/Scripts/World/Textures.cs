using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CC
{
    [System.Serializable]
    public struct CCTile
    {
        public Color dark;
        public Color ambient;
        public Color bright;
        Tile darkTile;
        Tile ambientTile;
        Tile brightTile;
        public void Init(Sprite baseTileSprite)
        {
            darkTile = ScriptableObject.CreateInstance<Tile>();
            ambientTile = ScriptableObject.CreateInstance<Tile>();
            brightTile = ScriptableObject.CreateInstance<Tile>();
            darkTile.sprite = baseTileSprite;
            ambientTile.sprite = baseTileSprite;
            brightTile.sprite = baseTileSprite;
            darkTile.color = dark;
            ambientTile.color = ambient;
            brightTile.color = bright;
        }
        public Tile GetDarkTile()
        {
            return darkTile;
        }
		public Tile GetAmbientTile()
        {
            return ambientTile;
        }
		public Tile GetBrightTile()
        {
            return brightTile;
        }
    }
    [System.Serializable]
    public struct DropShadows
    {
        public Color shore;
        public Color shadow;
        Tile shoreTile;
        Tile shadowTile;
        public void Init(Sprite baseTileSprite)
        {
            shoreTile = ScriptableObject.CreateInstance<Tile>();
            shadowTile = ScriptableObject.CreateInstance<Tile>();
            shoreTile.sprite = baseTileSprite;
            shadowTile.sprite = baseTileSprite;
            shoreTile.color = shore;
            shadowTile.color = shadow;
        }
        public Tile GetShoreTile()
        {
            return shoreTile;
        }
        public Tile GetShadowTile()
        {
            return shadowTile;
        }
    }
    [System.Serializable]
    public struct Nature
    {
        public Color pinkFlower;
        Tile pinkFlowerTile;
        public void Init(Sprite baseTileSprite)
        {
            pinkFlowerTile = ScriptableObject.CreateInstance<Tile>();
            pinkFlowerTile.sprite = baseTileSprite;
            pinkFlowerTile.color = pinkFlower;
        }
        public Tile GetPinkFlowerTile()
        {
            return pinkFlowerTile;
        }
    }
}

public class Textures : MonoBehaviour
{
    public Sprite baseTileSprite;
    public CC.CCTile sand;
    public CC.CCTile grass;
    public CC.CCTile prairie;
    public CC.CCTile rock;
    public CC.CCTile snow;
    public CC.DropShadows dropShadows;
    public CC.Nature nature;

    void Awake()
    {
        sand.Init(baseTileSprite);
        grass.Init(baseTileSprite);
        prairie.Init(baseTileSprite);
        rock.Init(baseTileSprite);
        snow.Init(baseTileSprite);
        dropShadows.Init(baseTileSprite);
        nature.Init(baseTileSprite);
    }

}
