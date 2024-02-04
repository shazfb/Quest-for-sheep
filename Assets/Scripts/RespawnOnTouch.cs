using UnityEngine;

public class RespawnOnTouch : MonoBehaviour
{
    private Vector3 lastGroundedPosition;
    public PlayerMovement2 playerMovement;

    public void Start()
    {
        lastGroundedPosition = transform.position;
    }

    public void Update()
    {
        
        if (IsGrounded())
        {
            lastGroundedPosition = transform.position;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Fog"))
        {
            Respawn();
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float raycastDistance = 0.1f;

        return Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance) &&
               hit.collider.CompareTag("Ground");
    }


    public void Respawn()
    {
       
        playerMovement.characterController.enabled = false;
        transform.position = lastGroundedPosition;
        playerMovement.characterController.enabled = true;
        Debug.Log("Respawned at " + lastGroundedPosition);
    }
}
