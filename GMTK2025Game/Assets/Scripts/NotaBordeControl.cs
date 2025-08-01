using UnityEngine;

public class NotaBordeControl : MonoBehaviour
{
    public float maxDistance = 5f;       
    public float maxBordeSize = 4.00f;  

    private Material matInstance;
    private Transform hitZone;

    void Awake()
    {
        
        GameObject hitZoneObj = GameObject.Find("HitZone");
        if (hitZoneObj != null)
        {
            hitZone = hitZoneObj.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto llamado 'HitZone' en la escena.");
        }

        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        matInstance = Instantiate(sr.material);
        sr.material = matInstance;
    }

    void Update()
    {
        if (hitZone == null) return;

        float distance = Mathf.Abs(transform.position.y - hitZone.position.y);
        float t = Mathf.Clamp01(1f - (distance / maxDistance));
        float bordeSize = t * maxBordeSize;

        matInstance.SetFloat("_BordeSize", bordeSize);
    }
}
