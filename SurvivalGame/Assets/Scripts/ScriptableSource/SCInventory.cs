using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory" , menuName = "Scriptable/Inventory")]
public class SCInventory : ScriptableObject
{

    public List<Slot> inventorySlots = new List<Slot>();
    int stackLimit = 4;
    public bool AddItem(SCitem item){
        foreach (Slot slot in inventorySlots){
            if(slot.item == item ){
                if(slot.item.canStackable){
                    if(slot.ItemCount<stackLimit){
                        slot.ItemCount++;
                        if(slot.ItemCount >= stackLimit){
                            slot.isFull = true;
                        }
                        return true;
                    }
                }
                
            }else if(slot.ItemCount == 0 ){
                slot.AddItemToSlot(item);
                return true;
            }        
        }
        return false;
    }
    

    public void DeleteItem(int index){
        inventorySlots[index].isFull = false;
        inventorySlots[index].ItemCount = 0;
        inventorySlots[index].item = null;
    }

    public void DropItem(int index , Vector3 position){
        for (int i = 0; i < inventorySlots[index].ItemCount; i++)
        {
            GameObject tempItem = Instantiate(inventorySlots[index].item.itemPrefab);
        tempItem.transform.position = position+new Vector3(i,0,0);
        }
        
        DeleteItem(index);

    }
}




[System.Serializable]

public class Slot{
    public bool isFull;
    public int ItemCount;
    public SCitem item;

    public void AddItemToSlot(SCitem item){
        this.item = item;
        if(item.canStackable == false){
            isFull = true;
        }
        ItemCount++;
    }
}
