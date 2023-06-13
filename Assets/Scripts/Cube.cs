using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Cube : MonoBehaviour
{
    [Header("Intensity")]
    [SerializeField] private float intensitySpeed;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationVector;

    [Header("Other")]
    [SerializeField] private PostProcessVolume postProcess;

    private MeshRenderer[] meshRenderers;
    private Bloom bloomLayer;
    private void Awake()
    {
        meshRenderers = new MeshRenderer[transform.childCount];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }

        postProcess.profile.TryGetSettings(out bloomLayer);
    }
    private void FixedUpdate()
    {
        transform.Rotate(rotationVector, rotationSpeed * Time.fixedDeltaTime);

        float currentValue = Mathf.PingPong(Time.time * intensitySpeed, maxValue - minValue) + minValue;
        bloomLayer.intensity.value = currentValue;
    }

    private void OnMouseUpAsButton()
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        }
    }
}
