using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] public int maximumNumberOfDrops;
    [SerializeField] public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()
    {
        List<Loot> droppedItems = new List<Loot>();
        int numberOfDrops = Random.Range(1, maximumNumberOfDrops+1);
        for (int i = 0; i < numberOfDrops; i++)
        {
            int randomNumber = Random.Range(0, 101);
            List<Loot> possibleItems = new List<Loot>();
            foreach (Loot item in lootList)
            {
                if (randomNumber <= item.dropChance)
                {
                    possibleItems.Add(item);
                }
            }
            if (possibleItems.Count > 0)
            {
                Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
                Debug.Log("Drop anythings");
                droppedItems.Add(droppedItem);
            }
        }
        if (droppedItems.Count > 0)
        {
            Debug.Log("Dropped " + droppedItems.Count + " items.");
            return droppedItems;
        }
        Debug.Log("No loot droped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnerPosition)
    {
        List<Loot> droppedItems = GetDroppedItems(); 
        if (droppedItems != null && droppedItems.Count > 0)
        {
            foreach (Loot droppedItem in droppedItems)
            {
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnerPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
                lootGameObject.tag = droppedItem.lootName;
                float dropForce = 30f;
                Vector2 dropDirection = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
        }
    }

}
