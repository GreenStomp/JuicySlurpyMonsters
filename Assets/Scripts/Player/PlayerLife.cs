using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int NumberLives { get { return numberLives; } }
    public int MaxNumberLives { get { return maxNumberLives; } }

    [SerializeField]
    private int numberLives = 3;
    [SerializeField]
    private int maxNumberLives = 3;
   
    private PlayerController playerController;
    private Animator animator;
    public bool Immune;
    public LayerMask Layer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Layer = 1 << 10;
    }

    // Use this for initialization
    public void IncreaseLife(int lives)
    {
        numberLives = Mathf.Min(numberLives + lives, maxNumberLives);
    }

    // Update is called once per frame
    public void DecreaseLife(int lives)
    {
        numberLives = Mathf.Max(numberLives - lives, 0);
    }
     
    private void OnCollisionEnter(Collision collision)
    {
        if (Layer.value  == 1 << collision.collider.gameObject.layer && !Immune)
        {
            //Debug.Log("Collision");
            DecreaseLife(1);
            return;
        }
    }
    private void GameOver()
    {

    }
}
