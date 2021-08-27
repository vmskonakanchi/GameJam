using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float smoothSpeed;
    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;
    public Vector3 offset;
    public float cameraSizeBT;
    private float cameraSizeLR;
    private Vector3 desiredOffset;
    private Vector3 desiredPosition;
    private Vector3 finalPosition;

    private void Start()
    {
        cameraSizeLR = cameraSizeBT * 1.6f;
    }
    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) desiredOffset = new Vector3(offset.x + 2, offset.y, offset.z);
        else if (Input.GetAxisRaw("Horizontal") < 0) desiredOffset = new Vector3(offset.x - 2, offset.y, offset.z);
        else desiredOffset = offset;
        desiredPosition = target.position + desiredOffset;
        finalPosition = new Vector3(Mathf.Clamp(desiredPosition.x, leftLimit + cameraSizeLR, rightLimit - cameraSizeLR),Mathf.Clamp(desiredPosition.y, bottomLimit + cameraSizeBT, topLimit - cameraSizeBT), desiredPosition.z);
        transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
    }
}
