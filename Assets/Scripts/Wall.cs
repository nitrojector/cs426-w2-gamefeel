using UnityEngine;

public class Wall : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private Material _material;
    
    void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustColorWithBallColor(Color color)
    {
        _material.color = Color.Lerp(_material.color, color, Random.Range(0.2f, 0.8f));
        _material.SetColor(EmissionColor, _material.color * 2.0f);
    }
}
