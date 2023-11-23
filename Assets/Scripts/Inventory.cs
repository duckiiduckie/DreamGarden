using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string type;
        public int count;
        public int maxAllowed;
        public Sprite icon;
        public Slot()
        {
            type = "";
            count = 0;
            maxAllowed = 99;
        }


        public bool CanAddItem()
        {
            if (count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(string type, Sprite sprite)
        {
            icon = sprite;
            this.type = type;
            count++;
        }
    }

    public List<Slot> slots = new List<Slot>();


    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(string typeToAdd, Sprite sprite)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == typeToAdd && slot.CanAddItem())
            {
                slot.AddItem(typeToAdd, sprite);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == "")
            {
                slot.AddItem(typeToAdd, sprite);
                return;
            }
        }
    }
}
