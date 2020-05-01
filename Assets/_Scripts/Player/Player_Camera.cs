using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //Player Camera variables
    public float cameraHeight = 20f;
    public Vector2 cameraDistance = new Vector2(0, 2f);
    public Camera playerCamera;

    private Vector3 cameraSmoothVelocity;

    public float cameraSmoothTime = 0.3f;
    public float playerRotationSmoothing = 0.5f;
    public float panZoomFactor = 3;

    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 cursorOffsetVector;

    private void Start()
    {
        Vector3 cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        playerCamera.transform.position = transform.position + cameraOffset;
        playerCamera.transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position, Vector3.up);
    }

    void LateUpdate()
    {
        //Setup camera offset
        Vector3 cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        cursorOffsetVector = cursorPosition - playerPosOnScreen;

        Vector2 panZoom = new Vector2(cursorOffsetVector.x * panZoomFactor, cursorOffsetVector.y * panZoomFactor);

        cameraOffset += new Vector3(0, Mathf.Sqrt(panZoom.x * panZoom.x + panZoom.y * panZoom.y), 0);

        //Camera Follow
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, transform.position + cameraOffset, ref cameraSmoothVelocity, cameraSmoothTime);
        playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, Quaternion.LookRotation(transform.position - playerCamera.transform.position, Vector3.up), playerRotationSmoothing * Time.deltaTime);

        /*//Setup camera offset
        Vector3 cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        cursorOffsetVector = cursorPosition - playerPosOnScreen;

        Vector2 mouseOffset = new Vector2(cursorOffsetVector.x * mouseLookSmoothing.x, cursorOffsetVector.y * mouseLookSmoothing.y);

        cameraOffset += new Vector3(0, (Mathf.Abs(mouseOffset.x) + Mathf.Abs(mouseOffset.y)), 0);

        Vector3 screenCenter = playerCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) * 0.5f);
        screenCenter.y = 0;

        //Camera Follow
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, transform.position + cameraOffset, ref cameraSmoothVelocity, cameraSmoothTime);
        playerCamera.transform.LookAt(transform.position + new Vector3(2 * mouseOffset.x, 0, 2 * mouseOffset.y));*/
    }
}