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
    float projectileLifeSpan;

    float fireRate;

    float nextFire = 0.0f;

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
        SetActiveWeapon();
    }

    public void SetActiveWeapon()
    {
        if (playerScript.activeWeapon == Player.Weapons.WEAPON_SLOT_ONE)
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerScript.weaponSlotOneName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
            fireRate = projectilePrefab.GetComponent<Projectile>().fireRate;
            projectileLifeSpan = projectilePrefab.GetComponent<Projectile>().lifeSpan;
        }
        else
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerScript.weaponSlotTwoName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
            fireRate = projectilePrefab.GetComponent<Projectile>().fireRate;
            projectileLifeSpan = projectilePrefab.GetComponent<Projectile>().lifeSpan;
        }
    }

    void Update()
    {
        // Fire Projectile
        if (Input.GetKey(KeyCode.A) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire(fireSide.LEFT, playerScript.shipDirectionState);
        }

        if (Input.GetKey(KeyCode.D) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
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
        float angle = 5.0f;
        if (fireSide == fireSide.LEFT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNLEFT)
            {
                offset = new Vector2(-0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNRIGHT)
            {
                offset = new Vector2(-0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPLEFT)
            {
                offset = new Vector2(-0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPRIGHT)
            {
                offset = new Vector2(-0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
        }
        else if (fireSide == fireSide.RIGHT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNLEFT)
            {
                offset = new Vector2(0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNRIGHT)
            {
                offset = new Vector2(0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPLEFT)
            {
                offset = new Vector2(0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPRIGHT)
            {
                offset = new Vector2(0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
        }

        // Destroy the bullet based on projectile life span
        Destroy(projectile, projectileLifeSpan);        
    }
}