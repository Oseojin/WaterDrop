using UnityEngine;

public class WaterDropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterDropPrefab;

    private WaterDrop currentDrop;
    private Camera mainCamera;
    private bool isHolding = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            StartHolding(mouseWorldPos);
        }
        else if (Input.GetMouseButton(0) && currentDrop != null)
        {
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentDrop.FollowMouse(mouseWorldPos);
            currentDrop.Grow();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseDrop();
        }
    }

    void StartHolding(Vector2 position)
    {
        GameObject dropObj = Instantiate(waterDropPrefab, position, Quaternion.identity);
        currentDrop = dropObj.GetComponent<WaterDrop>();
        isHolding = true;
    }

    void ReleaseDrop()
    {
        if (currentDrop != null)
        {
            currentDrop.Release();
            currentDrop = null;
        }
        isHolding = false;
    }
}
