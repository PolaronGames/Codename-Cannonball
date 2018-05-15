using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderIsland : MonoBehaviour
{

    public Texture2D heightmap; // Pixels start at lower left corner

    // Layers
    enum Layer
    {
        SAND, GRASS, PRAIRIE, ROCK, SNOW
    }
    public float sandElevation = 0.5f;
    public float grassElevation = 0.6f;
    public float prairieElevation = 0.7f;
    public float rockElevation = 0.9f;
    public float snowElevation = 0.95f;

    // Settings
    //public float flowerDensity = 0.01f;

    Tilemap tilemap;
    CCResources resources;

    void Start()
    {
        resources = this.GetComponent<CCResources>();
        tilemap = this.GetComponentInChildren<Tilemap>();
		RenderLayer(Layer.SAND);
		RenderLayer(Layer.GRASS);
		RenderLayer(Layer.PRAIRIE);
		RenderLayer(Layer.ROCK);
		RenderLayer(Layer.SNOW);
    }

    void RenderLayer(Layer layer)
    {
        float baseElevation = 0.0f, maxElevation = 0.0f;
		CC.TerrainMaterial material = CCResources.nullMaterial;
		CC.Pixel dropShadow = CCResources.nullPixel;
        switch (layer)
        {
            case Layer.SAND:
                baseElevation = sandElevation;
                maxElevation = grassElevation;
				material = resources.sand;
				dropShadow = CCResources.shore;
                break;
            case Layer.GRASS:
                baseElevation = grassElevation;
                maxElevation = prairieElevation;
				material = resources.grass;
				dropShadow = CCResources.shadow;
                break;
            case Layer.PRAIRIE:
                baseElevation = prairieElevation;
                maxElevation = rockElevation;
				material = resources.prairie;
				dropShadow = CCResources.shadow;
                break;
            case Layer.ROCK:
                baseElevation = rockElevation;
                maxElevation = snowElevation;
				material = resources.rock;
				dropShadow = CCResources.shadow;
                break;
            case Layer.SNOW:
                baseElevation = snowElevation;
                maxElevation = 1.0f;
				material = resources.snow;
				dropShadow = CCResources.shadow;
                break;
            default:
                break;
        }

        float elevation, layerHeight;
        for (int j = heightmap.height - 1; j >= 0; j--)
        {
            for (int i = 0; i < heightmap.width; i++)
            {
                elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
                if (elevation > baseElevation)
                {
                    Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
                    layerHeight = (elevation - baseElevation) / (maxElevation - baseElevation);
                    if (layerHeight < 0.333f)
                    {
                        RenderTile(tilemapPosition, material.GetDarkTile(), dropShadow.GetTile());
                    }
                    if (layerHeight >= 0.333f && layerHeight < 0.666f)
                    {
                        RenderTile(tilemapPosition, material.GetAmbientTile(), dropShadow.GetTile());
                    }
                    if (layerHeight >= 0.666f)
                    {
                        RenderTile(tilemapPosition, material.GetBrightTile(), dropShadow.GetTile());
                    }
                }
            }
        }
    }

    void RenderTile(Vector3Int tilemapPosition, Tile tile, Tile dropShadow)
    {
        // base tile
        tilemap.SetTile(tilemapPosition, tile);
        // drop shadow depth
        int depth = 3;
        if (tile == resources.sand.GetDarkTile())
        {
            depth = 5;
        }
        if (tile == resources.prairie.GetDarkTile())
        {
            depth = 5;
        }
        if (tile == resources.rock.GetDarkTile())
        {
            depth = 5;
        }
        RenderDropShadow(tilemapPosition, dropShadow, depth);
    }

    void RenderDropShadow(Vector3Int tilemapPosition, Tile dropShadow, int depth)
    {
        Vector3Int dropShadowPosition;
        int dx = 1;
        for (int i = 0; i < depth; i++)
        {
            dropShadowPosition = new Vector3Int(tilemapPosition.x + dx, tilemapPosition.y - i, 0);
            tilemap.SetTile(dropShadowPosition, dropShadow);
            if (i % 2 != 0) { dx++; }
        }
    }

}
