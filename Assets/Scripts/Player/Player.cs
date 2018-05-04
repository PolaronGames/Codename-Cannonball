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
    public static ShipDirection shipDirectionState;
    public enum ShipDirection{
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
    enum TileType{
        WATER,
        SAND,
        GRASS,
        ROCK,
        EMPTY
    }

    // Menu
    GameObject PortButton;

    // Weapon State
    public static Weapons activeWeapon;

    public string weaponSlotOneName;
    public string weaponSlotTwoName;
    public enum Weapons{
        WEAPON_SLOT_ONE,
        WEAPON_SLOT_TWO
    }

    // Use this for initialization
    void Awake()
    {
        transform = this.GetComponentsInParent<Transform>()[1];
        Ship = this.GetComponentsInParent<Rigidbody2D>()[0];
        info = this.GetComponentsInParent<WorldInfo>()[0];
        animator = GetComponent<Animator>();
        tileWidth = info.water.rect.width / 100.0f;
        dockingDistance = info.dockingDistance;
        PortButton = GameObject.FindGameObjectWithTag("PortButton");
        PortButton.SetActive(false);
        shipDirectionState = ShipDirection.RIGHT;
        activeWeapon = Weapons.WEAPON_SLOT_ONE;
        weaponSlotOneName = "CannonBall";
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
                if (GetTileType(p) == TileType.SAND)
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

    TileType GetTileType(Vector3Int p)
    {
        Tile tile = (Tile)tilemap.GetTile(p);
        if (tile == null)
        {
            return TileType.EMPTY;
        }
        else
        {
            String name = tile.name;

            switch (name)
            {
                case "water":
                    return TileType.WATER;
                case "sand":
                    return TileType.SAND;
                case "grass":
                    return TileType.GRASS;
                case "rock":
                    return TileType.ROCK;
            }
        }
        return TileType.EMPTY;
    }

    public void SetWeaponOneSlotFilePath(string projectileName)
    {
        weaponSlotOneName = projectileName;
    }

    public void SetWeaponTwoSlotFilePath(string projectileName)
    {
        weaponSlotTwoName = projectileName;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.Alpha1))
        {
            activeWeapon = Weapons.WEAPON_SLOT_ONE;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            activeWeapon = Weapons.WEAPON_SLOT_TWO;
        }

        // Movement
        if (Input.GetKey("right"))
        {
            velocity += new Vector3(speed, 0.0f, 0.0f);
            animator.Play("Right");
            shipDirectionState = ShipDirection.RIGHT;
        }
        if (Input.GetKey("left"))
        {
            velocity += new Vector3(-speed, 0.0f, 0.0f);
            animator.Play("Left");
            shipDirectionState = ShipDirection.LEFT;
        }
        if (Input.GetKey("up"))
        {
            velocity += new Vector3(0.0f, speed, 0.0f);
            animator.Play("Up");
            shipDirectionState = ShipDirection.UP;
        }
        if (Input.GetKey("down"))
        {
            velocity += new Vector3(0.0f, -speed, 0.0f);
            shipDirectionState = ShipDirection.DOWN;
        }
        Ship.velocity = velocity;

        // Docking  
        PortButton.SetActive(isDockable());
    }
}
