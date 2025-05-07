using UnityEngine;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer waterSprite;   // 컵 내부 수면 스프라이트
    [SerializeField] private Transform fillCollider;  // 바닥 충돌 영역
    [SerializeField] private float maxHeight;   // 물이 찼을 때의 최대 y size
    [SerializeField] private float width;
    [SerializeField] private float scaleToWaterRatio = 0.1f;

    [SerializeField] private float currentHeight = 0f;

    public void AddWater(float dropScale)
    {
        float delta = dropScale * scaleToWaterRatio;
        currentHeight = Mathf.Min(currentHeight + delta, maxHeight);

        // 수면 Sprite Y 스케일 증가
        waterSprite.size= new Vector2(width, currentHeight);

        // 바닥 충돌 기준도 함께 상승
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
