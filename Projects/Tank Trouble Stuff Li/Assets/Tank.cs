using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tank : MonoBehaviour
{
    private int playerNumber;
    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }
        set
        {
            playerNumber = value;
            UpdatePlayerMovement();
        }
    }

    public TankEvents events;

    public float movementSpeed = 1.0F;
    public Vector3 aimDirection = Vector3.one;

    public float timeSinceLastShot = 0.0F;

    public PlayerMovement playerMovement;

    public Transform middle;
    public Barrel barrel;
    public GameObject cockpit;
    public Transform connectBarrel;
    public GameObject engine;
    public Treads treads;

    public Storage storage;

    public TextMeshProUGUI bulletsLeftText;

    public new Rigidbody2D rigidbody;

    public void UpdatePlayerMovement()
    {
        playerMovement = new PlayerMovement(playerNumber);
    }

    public void RandomizeColors()
    {
        cockpit.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        barrel.SetColor(Random.ColorHSV());
        engine.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        treads.SetTreadColor(Random.ColorHSV());
        treads.SetLinksColors(Random.ColorHSV());
    }

    public class PlayerMovement
    {
        public string moveX;
        public string moveY;
        public string aimX;
        public string aimY;
        public string fire;
        public string lb;
        public string rb;

        public PlayerMovement(int playerNumber)
        {
            string prefix = "C" + playerNumber.ToString() + " ";
            moveX = prefix + "MoveX";
            moveY = prefix + "MoveY";
            aimX = prefix + "AimX";
            aimY = prefix + "AimY";
            fire = prefix + "Fire";
            lb = prefix + "LB";
            rb = prefix + "RB";
        }
    }

    public Projectile projectile;
    public Projectile anotherProjectile;
    public Projectile yetAnotherProjectile;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Destroy(barrel.gameObject);

        Ammo[] ammo = new Ammo[3];
        ammo[0] = new Ammo(50, projectile);
        ammo[1] = new Ammo(15, anotherProjectile);
        ammo[2] = new Ammo(20, yetAnotherProjectile);
        storage = new Storage(ammo, this);
    }

    private void Start()
    {

    }
    
    void Update()
    {
        Move();
        Aim();
        Shoot();
        ChangeAmmo();
    }                

    public void ChangeAmmo()
    {
        if(Input.GetButtonDown(playerMovement.lb))
        {
            barrel.Retract(-1);
            // storage.ChangeToRelativeWeapon(-1);
        }
        if(Input.GetButtonDown(playerMovement.rb))
        {
            barrel.Retract(1);
            // storage.ChangeToRelativeWeapon(1);
        }
    }

//    public IEnumerator ILiftAndSinkBarrel(int change)
//    {
//        barrel.readyForNextShot = false;
//
//        float liftSpeed = 20.0F;
//
//        while(connectBarrel.localScale.x >= 0)
//        {
//            connectBarrel.localScale -= Vector3.right * liftSpeed * Time.deltaTime;
//            yield return null;
//        }
//
//        storage.ChangeToRelativeWeapon(change);
//        barrel.readyForNextShot = false;
//
//        while (connectBarrel.localScale.x <= 1)
//        {
//            connectBarrel.localScale += Vector3.right * liftSpeed * Time.deltaTime;
//            yield return null;
//        }
//
//        connectBarrel.localScale = Vector3.one;
//
//        barrel.readyForNextShot = true;
//    }

    public void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetAxisRaw(playerMovement.fire) > 0 && storage.currentAmmo != null && barrel.readyForNextShot /* timeSinceLastShot > storage.currentAmmo.projectile.shootDelay*/)
        {
            timeSinceLastShot = 0.0F;

            //      GameObject newProjectile = Instantiate(projectile.gameObject, barrel.barrelEnd.position, Quaternion.identity);
            //      newProjectile.GetComponent<Projectile>().tank = this;
            //
            //      barrel.AnimateShot();

            storage.UseAmmo();
        }
    }

    public void Move()
    {
        Vector3 movementDirection = Input.GetAxis(playerMovement.moveY) * Vector3.up + Input.GetAxis(playerMovement.moveX) * Vector3.right;

        if (movementDirection.magnitude > 1)
        {
            movementDirection.Normalize();
        }
        else if(movementDirection.magnitude < 0.2F)
        {
            return;
        }
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        transform.position += movement;
        float angle = DirectionToAngle(movement);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Aim()
    {
        aimDirection = GetAimDirection();
        
        float angle = DirectionToAngle(aimDirection);
        middle.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 GetAimDirection()
        {
            Vector3 newAimDirection = Input.GetAxis(playerMovement.aimX) * Vector3.right + Input.GetAxis(playerMovement.aimY) * Vector3.up;

            if (newAimDirection.magnitude < 0.02F)
            {
                newAimDirection = aimDirection;
            }

            return newAimDirection;
        }
    }

    public void ChangeBarrel(Barrel newBarrel)
    {
        // Destroy(barrel.gameObject);
        barrel = Instantiate(newBarrel.gameObject, connectBarrel).GetComponent<Barrel>();
        barrel.tank = this;
    }

    public float DirectionToAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            events.TakeDamage();
            projectile.Kill();
            RandomizeColors();
        //    projectile.tank.storage.AddAmmo(new Ammo(10, yetAnotherProjectile));
        }

        Crate crate = collision.gameObject.GetComponent<Crate>();
        if (crate != null)
        {
            events.OpenCrate();
            crate.OpenCrate(this);
        }
    }
}

public class Storage
{
    public Tank tank;

    public List<Ammo> storage = new List<Ammo>();
    public Ammo currentAmmo;
    public int currentAmmoIndex = 0;

    public void ChangeWeapon(Projectile projectile)
    {
        int i = 0;
        foreach(Ammo ammo in storage)
        {
            if (projectile == ammo.projectile)
            {
                SetAmmo(i);
            }
            i++;
        }
        UpdateAmmoText();
    }

    public void ChangeToRelativeWeapon(int change)
    {
        // tank.barrel.Retract();

        currentAmmoIndex += change;
        SetAmmo(currentAmmoIndex);
        UpdateAmmoText();

        tank.barrel.Extend();
    }

    public void UseAmmo()
    {
        if(currentAmmo.ammount > 0)
        {
            tank.barrel.Shoot();
            currentAmmo.ammount--;
            UpdateAmmoText();

            if(currentAmmo.ammount <= 0)
            {
                RemoveCurrentAmmo();
            }
        }
    }

//    public void Shoot()
//    {
//        tank.events.Shoot();
//
//        GameObject newProjectile = Object.Instantiate(currentAmmo.projectile.gameObject, tank.barrel.barrelEnd.position, Quaternion.identity);
//        newProjectile.GetComponent<Projectile>().tank = tank;
//
//        // tank.barrel.AnimateShot();
//    }

    public void RemoveCurrentAmmo()
    {
        storage.RemoveAt(currentAmmoIndex);
        SetAmmo(currentAmmoIndex);
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        if(currentAmmo != null)
        {
            tank.bulletsLeftText.text = currentAmmo.ammount.ToString();
        }
        else
        {
            tank.bulletsLeftText.text = "";
        }
    }

    public float NfMod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }

    public void SetAmmo(int ammoIndex)
    {
        int nrOfDifferentAmmo = storage.Count;

        if (nrOfDifferentAmmo > 0)
        {
            if(ammoIndex >= nrOfDifferentAmmo || ammoIndex < 0)
            {
                ammoIndex = (int)NfMod(ammoIndex, nrOfDifferentAmmo);

                currentAmmoIndex = ammoIndex;

            }

            currentAmmo = storage[ammoIndex];
            tank.ChangeBarrel(currentAmmo.projectile.barrel);
        }
        else {
            currentAmmo = null;
            Object.Destroy(tank.barrel);

            // Remove Barrel
        }
    }

    public void AddAmmo(Ammo toAdd)
    {
        foreach (Ammo p in storage)
        {
            if(p.projectile == toAdd.projectile)
            {
                p.ammount += toAdd.ammount;
                UpdateAmmoText();
                return;
            }
        }
        storage.Add(toAdd);
        UpdateAmmoText();
    }

    public Storage(Ammo[] storage, Tank tank)
    {
        this.tank = tank;

        foreach(Ammo ammo in storage)
        {
            AddAmmo(ammo);
        }

        currentAmmoIndex = 0;
        SetAmmo(0);
        UpdateAmmoText();
    }
}

public class Ammo
{
    public int ammount = 0;
    public Projectile projectile;

    public Ammo(int ammount, Projectile projectile)
    {
        this.ammount = ammount;
        this.projectile = projectile;
    }
}
