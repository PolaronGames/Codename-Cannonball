using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderIsland : MonoBehaviour {

	public Texture2D heightmap; // Pixels start at lower left corner

	// Layers
	public float sandElevation = 0.5f;
	public float grassElevation = 0.6f;
	public float prairieElevation = 0.7f;
	public float rockElevation = 0.9f;
	public float snowElevation = 0.95f;

	// Settings
	public float flowerDensity = 0.01f;

	Tilemap tilemap;
	Textures textures;

	void Start () {
		textures = this.GetComponent<Textures>();
		tilemap = this.GetComponentInChildren<Tilemap>();
		int X = heightmap.width; // number of pixels in X-direction
		int Y = heightmap.height; // number of pixels in Y-direction
		float elevation;

		// Sand Layer
		for(int j = Y-1; j >= 0; j--)
		{
			for(int i = 0; i < X; i++)
			{
				elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
				if(elevation > sandElevation && elevation <= grassElevation)
				{
					Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
					float layerHeight = (elevation - sandElevation)/(grassElevation - sandElevation);
					if(layerHeight < 0.333f)
					{
						RenderTile(tilemapPosition, textures.sand.GetDarkTile(), textures.dropShadows.GetShoreTile());
					}
					if(layerHeight >= 0.333f && layerHeight < 0.666f)
					{
						RenderTile(tilemapPosition, textures.sand.GetAmbientTile(), textures.dropShadows.GetShoreTile());
					}
					if(layerHeight >= 0.666f)
					{
						RenderTile(tilemapPosition, textures.sand.GetBrightTile(), textures.dropShadows.GetShoreTile());
					}
				}
			}
		}

		// Grass Layer
		for(int j = Y-1; j >= 0; j--)
		{
			for(int i = 0; i < X; i++)
			{
				elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
				if(elevation > grassElevation && elevation <= prairieElevation)
				{
					Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
					float layerHeight = (elevation - grassElevation)/(prairieElevation - grassElevation);
					if(layerHeight < 0.333f)
					{
						RenderTile(tilemapPosition, textures.grass.GetDarkTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.333f && layerHeight < 0.666f)
					{
						RenderTile(tilemapPosition, textures.grass.GetAmbientTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.666f)
					{
						RenderTile(tilemapPosition, textures.grass.GetBrightTile(), textures.dropShadows.GetShadowTile());
					}
				}
			}
		}

		// Prairie Layer
		for(int j = Y-1; j >= 0; j--)
		{
			for(int i = 0; i < X; i++)
			{
				elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
				if(elevation > prairieElevation && elevation <= rockElevation)
				{
					Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
					float layerHeight = (elevation - prairieElevation)/(rockElevation - prairieElevation);
					if(layerHeight < 0.333f)
					{
						RenderTile(tilemapPosition, textures.prairie.GetDarkTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.333f && layerHeight < 0.666f)
					{
						RenderTile(tilemapPosition, textures.prairie.GetAmbientTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.666f)
					{
						RenderTile(tilemapPosition, textures.prairie.GetBrightTile(), textures.dropShadows.GetShadowTile());
					}
				}
			}
		}

		// Rock Layer
		for(int j = Y-1; j >= 0; j--)
		{
			for(int i = 0; i < X; i++)
			{
				elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
				if(elevation > rockElevation && elevation <= snowElevation)
				{
					Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
					float layerHeight = (elevation - rockElevation)/(snowElevation - rockElevation);
					if(layerHeight < 0.333f)
					{
						RenderTile(tilemapPosition, textures.rock.GetDarkTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.333f && layerHeight < 0.666f)
					{
						RenderTile(tilemapPosition, textures.rock.GetAmbientTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.666f)
					{
						RenderTile(tilemapPosition, textures.rock.GetBrightTile(), textures.dropShadows.GetShadowTile());
					}
				}
			}
		}

		// Snow Layer
		for(int j = Y-1; j >= 0; j--)
		{
			for(int i = 0; i < X; i++)
			{
				elevation = 1.0f - heightmap.GetPixel(i, j).grayscale;
				if(elevation > snowElevation)
				{
					Vector3Int tilemapPosition = new Vector3Int(i, j, 0);
					float layerHeight = (elevation - snowElevation)/(1.0f - snowElevation);
					if(layerHeight < 0.333f)
					{
						RenderTile(tilemapPosition, textures.snow.GetDarkTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.333f && layerHeight < 0.666f)
					{
						RenderTile(tilemapPosition, textures.snow.GetAmbientTile(), textures.dropShadows.GetShadowTile());
					}
					if(layerHeight >= 0.666f)
					{
						RenderTile(tilemapPosition, textures.snow.GetBrightTile(), textures.dropShadows.GetShadowTile());
					}
				}
			}
		}

	}

	void RenderTile(Vector3Int tilemapPosition, Tile tile, Tile dropShadow)
	{
		// base tile
		tilemap.SetTile(tilemapPosition, tile);
		// drop shadow
		int depth = 3;
		if(tile == textures.sand.GetDarkTile())
		{
			depth = 5;
		}
		if(tile == textures.prairie.GetDarkTile())
		{
			depth = 5;
		}
		if(tile == textures.rock.GetDarkTile())
		{
			depth = 5;
		}
		RenderDropShadow(tilemapPosition, dropShadow, depth);
	}

	void RenderDropShadow(Vector3Int tilemapPosition, Tile dropShadow, int depth)
	{
		Vector3Int dropShadowPosition;
		int dx = 1;
		for(int i = 0; i < depth; i++)
		{
			dropShadowPosition = new Vector3Int(tilemapPosition.x+dx, tilemapPosition.y-i, 0);
			tilemap.SetTile(dropShadowPosition, dropShadow);
			if( i%2 != 0) { dx++; }
		}
	}

}
