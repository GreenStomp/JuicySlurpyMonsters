using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO;

public class MonsterEat : MonoBehaviour
{
    public LayerMask Layer;

    private PlayerController playerControler;

    [SerializeField]
    private ReferenceFloat IncrementSpeed;

    private float minDistance = 5f;

    // Use this for initialization
    void Start()
    {
        playerControler = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CanPickUpHuman();
    }

    private void CanPickUpHuman()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 35f, Color.magenta);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 35f))
        {
            if (Layer.value == 1 << hit.collider.gameObject.layer)
            {
                playerControler.MovementSpeed.Value += IncrementSpeed.Value * Time.deltaTime;
                HumanReached(hit.collider.transform);
            }
        }

        else
        {
            if (playerControler.MovementSpeed.Value > 15)
                playerControler.MovementSpeed.Value -= IncrementSpeed.Value * Time.deltaTime;
        }
    }

    private void HumanReached(Transform human)
    {
        if (Vector3.Distance(human.position, this.transform.position) <= minDistance)
            human.gameObject.SetActive(false);
    }
}
