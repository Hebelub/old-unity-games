using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    protected GameManager gm;
    protected MeshRenderer mr;
    protected Upgrades upgrades;

    protected void Start()
    {
        gm = GameManager.instance;
        mr = GetComponent<MeshRenderer>();
        upgrades = gm.upgrades;

        Color.RGBToHSV(mr.material.color, out float h, out float s, out float v);
        frailty = v;
    }

    public float frailty = 0;

    private float health = 1;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            health = value;

            if(value <= 0)
            {
                Drop();
            }
            else
            {
                //Color.RGBToHSV(mr.material.color, out float h, out float s, out float v);
                //mr.material.color = Color.HSVToRGB(h, s, 1 - value);
            }
        }
    }

    public bool isShaking = false;
    public IEnumerator IShake()
    {
        Transform shakeTransform = Instantiate(gm.centerOfCube, transform.position, Quaternion.identity, transform).transform;
        shakeTransform.GetComponent<MeshRenderer>().material.color = mr.material.color;
        Material originalMaterial = mr.material;
        mr.material = gm.transparentCube;
        mr.material.color = new Color(originalMaterial.color.r, originalMaterial.color.g, originalMaterial.color.b, 0.75F);

        Vector3 originalPosition = transform.position;

        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;

        while (Health < 1)
        {
            shakeTransform.localScale = new Vector3(
                (x - ((1 - Health) * 2) / 2.0F) / x, 
                (y - ((1 - Health) * 2) / 2.0F) / y, 
                (z - ((1 - Health) * 2) / 2.0F) / z);
            shakeTransform.position = originalPosition + Random.insideUnitSphere * (1 - Health) / 2.0F;

            yield return null;

            Health += 0.001f;
        }

        mr.material = originalMaterial;

        Destroy(shakeTransform.gameObject);
        transform.position = originalPosition;
        isShaking = false;
    }

    public void Hit(float damage)
    {
        Health -= damage * frailty + damage * 0.25f;
        if(!isShaking)
        {
            StartCoroutine(IShake());
            isShaking = true;
        }
    }

    public void Drop()
    {
        int x = Mathf.FloorToInt(transform.localScale.x);
        int y = Mathf.FloorToInt(transform.localScale.y);
        int z = Mathf.FloorToInt(transform.localScale.z);

        int quantity = x * y * z;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    Vector3 position = transform.position + new Vector3(
                            (transform.localScale.x / x) * i - transform.localScale.x / 2 + 0.5f,
                            (transform.localScale.y / y) * j - transform.localScale.y / 2 + 0.5f,
                            (transform.localScale.z / z) * k - transform.localScale.z / 2 + 0.5f);
                    if (i == 0 || j == 0 || k == 0 || i == x - 1 || j == y - 1 || k == z - 1)
                    {
                        if (Physics.OverlapSphere(position, 0f).Length <= 1)
                        {
                            GameObject coinBlock = Instantiate(gm.cubeCoin, position, Quaternion.identity, gm.cubeCoins);

                            coinBlock.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
                        }
                    }
                }
            }
        }

        AddMoney(quantity);

        if (transform.parent.childCount <= 1)
        {
            gm.NumberOfObstacles += 1;
            gm.GenerateLevel();
        }
        Destroy(gameObject);
    }

    public void AddMoney(float money)
    {
        gm.Money += money;
    }
}
