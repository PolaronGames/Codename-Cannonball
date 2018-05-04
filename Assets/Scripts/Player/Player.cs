using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    // Player data
    Transform transform;
    public float speed;
    Rigidbody2D Ship;
    Animator animator;
    public static shipDirection shipDirectionState;
    public enum shipDirection{
        UP,
        DOWN,
        RIGHT,
        LEFT
    };

    // World data
    WorldInfo info;
    public Tilemap tilemap;
    int dockingDistance;
    float tileWidth;

    // make this an enum
    // Tile types
    const int WATER = 0;
    const int SAND = 1;
    const int GRASS = 2;
    const int ROCK = 3;
    const int EMPTY = -1;

    // Menu
    GameObject PortButton;

    // Use this for initialization
    void Start()
    {
        transform = this.GetComponentsInParent<Transform>()[1];
        Ship = this.GetComponentsInParent<Rigidbody2D>()[0];
        info = this.GetComponentsInParent<WorldInfo>()[0];
        animator = GetComponent<Animator>();
        tileWidth = info.water.rect.width / 100.0f;
        dockingDistance = info.dockingDistance;
        PortButton = GameObject.FindGameObjectWithTag("PortButton");
        PortButton.SetActive(false);
        shipDirectionState = shipDirection.RIGHT;
    }

    bool isDockable()
    {
        for (int j = 0; j < dockingDistance; j++)
        {
            int y = WorldToCell().y + j - dockingDistance / 2;
            for (int i = 0; i < dockingDistance; i++)
            {
                int x = WorldToCell().x + i - dockingDistance / 2;
                Vector3Int p = new Vector3Int(x, y, 0);
                if (GetTileType(p) == SAND)
                {
                    return true;
                }
            }
        }

        return false;
    }

    Vector3Int WorldToCell()
    {
        int x = (int)(transform.position.x / tileWidth) + 1;
        int y = (int)(transform.position.y / tileWidth) + 1;
        return new Vector3Int(x, y, 0);
    }

    int GetTileType(Vector3Int p)
    {
        Tile tile = (Tile)tilemap.GetTile(p);
        if (tile == null)
        {
            return EMPTY;
        }
        else
        {
            String name = tile.name;

            switch (name)
            {
                case "water":
                    return WATER;
                case "sand":
                    return SAND;
                case "grass":
                    return GRASS;
                case "rock":
                    return ROCK;
            }
        }
        return EMPTY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        // Movement
        if (Input.GetKey("right"))
        {
            velocity += new Vector3(speed, 0.0f, 0.0f);
            animator.Play("Right");
            shipDirectionState = shipDirection.RIGHT;
        }
        if (Input.GetKey("left"))
        {
            velocity += new Vector3(-speed, 0.0f, 0.0f);
            animator.Play("Left");
            shipDirectionState = shipDirection.LEFT;
        }
        if (Input.GetKey("up"))
        {
            velocity += new Vector3(0.0f, speed, 0.0f);
            animator.Play("Up");
            shipDirectionState = shipDirection.UP;
        }
        if (Input.GetKey("down"))
        {
            velocity += new Vector3(0.0f, -speed, 0.0f);
            shipDirectionState = shipDirection.DOWN;
        }
        Ship.velocity = velocity;

        // Docking  
        PortButton.SetActive(isDockable());
    }
}
