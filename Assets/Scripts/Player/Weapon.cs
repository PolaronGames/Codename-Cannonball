using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Weapon : MonoBehaviour
{
    // Projectile Data
    GameObject projectilePrefab;
    
    GameObject player;
    Player playerScript;

    Transform position;

    float speed;
    float damage;

    public fireSide fireSideState;
    public enum fireSide {
        LEFT,
        RIGHT
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerScript = player.GetComponent<Player>();
        position = player.GetComponent<Transform>();
        fireSideState = fireSide.LEFT;
        if (playerScript.activeWeapon == Player.Weapons.WEAPON_SLOT_ONE)
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerScript.weaponSlotOneName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
        }
        else
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerScript.weaponSlotOneName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
        }
    }

    void Update()
    {
        // Fire Projectile
        if (Input.GetKeyDown(KeyCode.A))
        {
            Fire(fireSide.LEFT, playerScript.shipDirectionState);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Fire(fireSide.RIGHT, playerScript.shipDirectionState);
        }
    }

    // FIREEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
    void Fire(fireSide fireSide, Player.ShipDirection shipDirectionState)
    {
        Vector2 offset = new Vector2(0.0f, 0.0f);
        // Create the Projectile from the Bullet Prefab
        var projectile = (GameObject)Instantiate(
            projectilePrefab,
            (Vector2)position.position + offset,
            Quaternion.identity);

        var projectileComponent = projectile.GetComponent<Rigidbody2D>();    

        // Set Velocity
        if (fireSide == fireSide.LEFT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, 0.0f);
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, 0.0f);
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed);
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed);
            }
        }
        else if (fireSide == fireSide.RIGHT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, 0.0f);
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, 0.0f);
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed);
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed);
            }
        }

        // Destroy the bullet after 2 seconds
        Destroy(projectile, 2.0f);        
    }
}