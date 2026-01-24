using UnityEngine;

public class Wall : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustColorWithBallColor(Color color)
    {
        var mat = GetComponent<Renderer>().material;
        mat.color = Color.Lerp(mat.color, color, Random.Range(0.2f, 0.8f));
        mat.SetColor(EmissionColor, color * 2.0f);
    }
}
