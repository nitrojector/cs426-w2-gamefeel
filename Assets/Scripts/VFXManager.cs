using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public Camera mainCamera;
    
    public static VFXManager Instance { get; private set; }

    private readonly List<Shake> activeShakes = new List<Shake>();
    private Vector3 lastPosOffset = Vector3.zero;
    private float lastRotOffset = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    
    // Public: enqueue a new shake (addable). The actual per-frame execution happens in FixedUpdate.
    public void ScreenShake(Vector2 dir, float rot, float intensity, float duration)
    {
        if (duration <= 0f || intensity <= 0f)
            return;

        var s = new Shake
        {
            dir = dir,
            rot = rot,
            intensity = intensity,
            duration = duration,
            elapsed = 0f
        };

        activeShakes.Add(s);
    }

    private void FixedUpdate()
    {
        if (mainCamera == null)
            return;

        // Unapply last frame's offsets so we don't accumulate them over time.
        if (lastPosOffset != Vector3.zero)
            mainCamera.transform.localPosition -= lastPosOffset;
        if (Mathf.Abs(lastRotOffset) > 0.0001f)
            mainCamera.transform.localRotation *= Quaternion.Euler(0f, 0f, -lastRotOffset);

        Vector3 totalPosOffset = Vector3.zero;
        float totalRotOffset = 0f;

        // Advance shakes and compute contributions.
        for (int i = activeShakes.Count - 1; i >= 0; --i)
        {
            var sh = activeShakes[i];
            sh.elapsed += Time.fixedDeltaTime;

            if (sh.elapsed >= sh.duration)
            {
                activeShakes.RemoveAt(i);
                continue;
            }

            float remaining = 1f - (sh.elapsed / sh.duration); // 1 -> 0
            // Use eased decay (quadratic) for smoother falloff: stronger at start, softer at end
            float decay = remaining * remaining;
            float contribution = sh.intensity * decay;

            // Positional contribution: direction-scaled jitter + random noise
            Vector2 noise = Random.insideUnitCircle;
            Vector3 dir3 = new Vector3(sh.dir.x, sh.dir.y, 0f);
            Vector3 posContribution = new Vector3(dir3.x * noise.x, dir3.y * noise.y, 0f) * contribution;

            // Rotational contribution around Z axis
            float rotContribution = sh.rot * (Random.Range(-1f, 1f)) * decay;

            totalPosOffset += posContribution;
            totalRotOffset += rotContribution;

            // write back elapsed (struct copy protection)
            activeShakes[i] = sh;
        }

        // Apply new offsets and remember them for next FixedUpdate
        mainCamera.transform.localPosition += totalPosOffset;
        mainCamera.transform.localRotation *= Quaternion.Euler(0f, 0f, totalRotOffset);

        lastPosOffset = totalPosOffset;
        lastRotOffset = totalRotOffset;
    }

    // Internal shake representation
    private struct Shake
    {
        public Vector2 dir;   // preferred direction of shake in local XY
        public float rot;     // rotation amplitude (degrees)
        public float intensity;
        public float duration;
        public float elapsed;
    }
}