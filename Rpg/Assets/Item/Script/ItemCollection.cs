using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rpg.Character;
using UnityEngine.Events;
using System;

namespace Rpg.Item
{
    public class ItemCollection : TriggerGenericAction
    {
        [Header("--- Item Collection Options ---")]
        public float onCollectDelay;

        [Tooltip("Immediately equip the item ignoring the Equip animation")]
        public bool immediate = false;

        [Header("---Items List Data---")]
        public ItemListData itemListData;

        [Header("---Items Filter---")]
        public List<ItemType> itemsFilter = new List<ItemType>() { 0 };

        [HideInInspector]
        public List<ItemReference> items = new List<ItemReference>();

        protected override void Start()
        {
            base.Start();
            if (destroyAfter && destroyDelay < onCollectDelay)
            {
                destroyDelay = onCollectDelay + 0.25f;
            }
        }

        public void CollectItems(ItemManager itemManager)
        {
            if (items.Count > 0)
            {
                StartCoroutine(SetItemsToItemManager(itemManager));
            }
        }

        IEnumerator SetItemsToItemManager(ItemManager itemManager)
        {
            yield return new WaitForSeconds(onCollectDelay);

            itemManager.CollectItem(items, immediate);
            items.Clear();
        }
    }
}

