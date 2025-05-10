using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    InventoryUIController InventoryUI;

    public PlayerActions playerAction;

    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    public void DeleteItem(){
        if(isSwapping == true){
            playerInventory.DeleteItem(tempIndex);
            isSwapping = false;
            InventoryUI.UpdateUI();
        }
    }

    public void DropItem(){
        if(isSwapping== true){
            playerInventory.DropItem(tempIndex , gameObject.transform.position + Vector3.forward);
            isSwapping = false;
            InventoryUI.UpdateUI();
        }
    }
    
    public void SwapItem(int index){
        if(isSwapping == false){
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }else if(isSwapping == true){
            playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping = false;
        }
        InventoryUI.UpdateUI();
    }
    private void Start() {
        InventoryUI = gameObject.GetComponent<InventoryUIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item")){
            if(playerInventory.AddItem(other.gameObject.GetComponent<Items>().item)){
                Destroy(other.gameObject);
                InventoryUI.UpdateUI();
            }
            
        }
        
    }

    public void CurrentItem(int index){
        
        if(playerInventory.inventorySlots[index].item){
            playerAction.SetItem(playerInventory.inventorySlots[index].item.itemPrefab); 
        }
    }
}
