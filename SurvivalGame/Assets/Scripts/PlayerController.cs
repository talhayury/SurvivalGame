using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sensivity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private GameObject userInventory;
    public Camera fpsCam;
    Rigidbody rb;
    CapsuleCollider capsuleColl;
    public LayerMask groundLayer;    
    float mouseX , mouseY ;
    float rotX , rotY;
    bool isInventoryOpen;

    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleColl = gameObject.GetComponent<CapsuleCollider>();
       rb = GetComponent<Rigidbody>(); 
       userInventory.SetActive(false);
       //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab)){
            if(isInventoryOpen == false){
                userInventory.SetActive(true);
                isInventoryOpen = true;
            }else if(isInventoryOpen == true){
                userInventory.SetActive(false);
                isInventoryOpen = false;
            }
        }
        if(Input.GetButtonDown("Jump")){
            if(OnGround() == true){
                Jump();
            }
        }
        mouseX = Input.GetAxis("Mouse X")*Time.deltaTime*_sensivity;
        mouseY = Input.GetAxis("Mouse Y")*Time.deltaTime*_sensivity;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX,-90,90);

        rotY += mouseX;
        transform.localRotation = Quaternion.Euler(0 , rotY , 0);
        
    }

    private void LateUpdate() {
        fpsCam.transform.localRotation = Quaternion.Euler(rotX,0,0);
    }

    private void FixedUpdate() {
        
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        direction = new Vector3(hor , 0 , ver);
        rb.MovePosition(transform.position + transform.TransformDirection(direction*Time.fixedDeltaTime*_movementSpeed));
    }

    private void Jump(){
        rb.AddForce(Vector3.up*_jumpPower);
    }

    private bool OnGround(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,capsuleColl.bounds.size.y /2 ,groundLayer)){
            return true;
        }else{
            return false;
        }
    }
}
