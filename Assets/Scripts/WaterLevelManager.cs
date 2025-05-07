using UnityEngine;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer waterSprite;   // �� ���� ���� ��������Ʈ
    [SerializeField] private Transform fillCollider;  // �ٴ� �浹 ����
    [SerializeField] private float maxHeight;   // ���� á�� ���� �ִ� y size
    [SerializeField] private float width;
    [SerializeField] private float scaleToWaterRatio = 0.1f;

    [SerializeField] private float currentHeight = 0f;

    public void AddWater(float dropScale)
    {
        float delta = dropScale * scaleToWaterRatio;
        currentHeight = Mathf.Min(currentHeight + delta, maxHeight);

        // ���� Sprite Y ������ ����
        waterSprite.size= new Vector2(width, currentHeight);

        // �ٴ� �浹 ���ص� �Բ� ���
        Vector3 colliderPos = new Vector3(fillCollider.position.x, currentHeight - fillCollider.localScale.y / 2, fillCollider.position.z);
        fillCollider.localPosition = colliderPos;
    }

    public bool IsFull()
    {
        return currentHeight >= maxHeight;
    }

    public void Init(SpriteRenderer _spriteRenderer)
    {
        waterSprite = _spriteRenderer;
        width = waterSprite.size.x;
        maxHeight = waterSprite.size.y;
    }

    private void Start()
    {
        width = waterSprite.size.x;
        maxHeight = waterSprite.size.y;
        currentHeight = 0;

        waterSprite.size = new Vector2(width, currentHeight);
    }
}
