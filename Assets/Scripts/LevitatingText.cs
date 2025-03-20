using UnityEngine;
using TMPro;

public class FloatingScore : MonoBehaviour
{
    public float floatSpeed = 1f;    // Speed at which the prefab floats upwards
    public float fadeSpeed = 1f;     // Speed at which the prefab fades out

    private TextMeshPro textMeshPro;
    private Color initialColor;

    void Start()
    {
        // Get the TextMeshPro component
        textMeshPro = GetComponent<TextMeshPro>();
        // Store the initial color
        initialColor = textMeshPro.color;
    }

    void Update()
    {
        // Move the prefab upwards
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // Decrease the alpha value of the color to fade out
        initialColor.a -= fadeSpeed * Time.deltaTime;
        textMeshPro.color = initialColor;

        // Check if the prefab is fully transparent
        if (initialColor.a <= 0f)
        {
            // Destroy the prefab when it's no longer visible
            Destroy(gameObject);
        }
    }
}
