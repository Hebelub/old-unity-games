using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string _name;
    public int id;
    public bool usedInRecipe; // If it already is merging with another recipe

    public Transform scale;
    public SpriteRenderer circle;
    public SpriteRenderer sprite;

    public Color circleEnterColor;
    public Color circleExitColor;

    public bool isItem = false;
    public bool moveable = false;

    public Item item;

    public AudioClip pickUpItem;

    GameManager gm;

    public bool attachedToMouse = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity collidingEntity = collision.gameObject.GetComponent<Entity>();

        if (id <= collidingEntity.id) return;

        Recipe recipe = gm.recipeMaker.FindRecipe(collidingEntity._name, _name);

        if(recipe != null && usedInRecipe == false && collidingEntity.usedInRecipe == false)
        {
            Entity firstInRecipe;
            Entity lastInRecipe;
            if (_name.ToLower() == recipe.ingredientA)
            {
                firstInRecipe = this;
                lastInRecipe = collidingEntity;
            }
            else
            {
                firstInRecipe = collidingEntity;
                lastInRecipe = this;
            }

            firstInRecipe.usedInRecipe = true;
            lastInRecipe.usedInRecipe = true;

            foreach(Entity entity in recipe.result)
            {
                firstInRecipe.DropItem(entity.gameObject, 0);
            }

            if (recipe.destroyA) Destroy(firstInRecipe.gameObject);
            else firstInRecipe.usedInRecipe = false;
            if (recipe.destroyB) Destroy(lastInRecipe.gameObject);
            else lastInRecipe.usedInRecipe = false;
        }
    }

    public void DropItem(GameObject item, float distance)
    {
        Instantiate(item, transform.position + (Vector3)Random.insideUnitCircle * distance, Quaternion.identity);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && moveable)
        {
            if (!attachedToMouse) AttachToMouse(true);
        }

        //if (Input.GetMouseButtonDown(1) && isItem)
        //{
        //    GatherItem(item);
        //}
    }

    public void AttachToMouse(bool attach)
    {
        attachedToMouse = attach;

        if (attach)
        {
            scale.transform.localScale = Vector3.one * 1.25f;
            sprite.sortingOrder = 2;
        }
        else
        {
            scale.transform.localScale = Vector3.one;
            sprite.sortingOrder = 1;
        }

        GetComponent<CircleCollider2D>().enabled = !attach;

    }

    private void OnMouseEnter()
    {
        circle.color = circleEnterColor;
    }
    private void OnMouseExit()
    {
        if (!attachedToMouse) circle.color = circleExitColor;
        // attachedToMouse = false;
    }

    public void GatherItem(Item item)
    {
        gm.audioSource.clip = pickUpItem;
        gm.audioSource.Play();

        Destroy(gameObject);
    }

    void Start()
    {
        gm = GameManager.instance;
        id = gm.idCount++;
        circle.color = circleExitColor;
        //item = GetComponent<Item>();
        //if (item != null) isItem = true; else isItem = false;
    }

    void Update()
    {

        if (attachedToMouse)
        {

            if (Input.GetMouseButtonUp(0) && attachedToMouse)
            {
                AttachToMouse(false);
            }
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        }
    }
}
