using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a single flower with nectar
/// </summary>
public class Flower : MonoBehaviour
{
    [Tooltip("The Color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, .3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowerColor = new Color(.5f, 0f, 1f);

    /// <summary>
    /// The trigger collider representing the nectar
    /// </summary>
    [HideInInspector]
    public Collider nectarCollider;

    // The Solid collider representing flower petals
    private Collider flowerCollider;

    // The flowers material
    private Material flowerMaterial;

    /// <summary>
    /// A Vector Pointing straight out of the flower
    /// </summary>
    public Vector3 FlowerUpVector
    {
        get
        {
            return nectarCollider.transform.up;
        }
    }

    /// <summary>
    /// The Center position of the nectar collider
    /// </summary>
    public Vector3 FlowerCenterPosition
    {
        get
        {
            return nectarCollider.transform.position;
        }
    }

    /// <summary>
    /// The amount of nectar remaining in the flower
    /// </summary>
    public float NectarAmount { get; private set; }

    public bool HasNectar
    {
        get
        {
            return NectarAmount > 0f;
        }
    }

    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">The amount of nectar to remove</param>
    /// <returns>The actual amount of nectar successfully removed</returns>
    public float Feed(float amount)
    {
        //Track how much was successfully taken (never more than available)
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);

        //subtract nectar

        NectarAmount -= amount;
        if (NectarAmount <= 0)
        {
            //no nectar remaining
            NectarAmount = 0;

            //disable flower collider
            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            //change flower color
            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);


        }
        //return amount of nectar taken

        return nectarTaken;
    }

    /// <summary>
    /// Resets the flower
    /// </summary>
    public void ResetFlower()
    {
        //no nectar remaining
        NectarAmount = 1f;

        //disable flower collider
        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);

        //change flower color
        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);

    }

    /// <summary>
    /// Called when flower wakes up
    /// </summary>
    private void Awake()
    {
        //find the flowers mesh renderer and get main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        //find flower and nectar collider
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();


    }
}
