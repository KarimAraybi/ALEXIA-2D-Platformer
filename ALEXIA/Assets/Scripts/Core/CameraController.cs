using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosY = 4.07f;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, currentPosY, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + 2, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance , Time.deltaTime * cameraSpeed);
    }
    

    public void MoveToNewHeight(Transform _newHeight)
    {
        currentPosY = _newHeight.position.y;
    }
}