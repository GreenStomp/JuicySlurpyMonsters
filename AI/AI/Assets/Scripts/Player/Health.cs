using UnityEngine;
public class Health : MonoBehaviour
{
    public int NumberOfLives { get { return numberOfLives; } }
    public int MaxNumberOfLives { get { return maxNumberOfLives; } }
    [SerializeField]
    private int maxNumberOfLives;
    [SerializeField]
    private int numberOfLives;
    private void Update()
    {
        if (numberOfLives == 0)
        {
            GameOver();
        }
    }
    public void DecreaseLife(int lives)
    {
        numberOfLives = Mathf.Max(numberOfLives - lives, 0);
    }
    public void IncreaseLife(int lives)
    {
        numberOfLives = Mathf.Min(numberOfLives + lives, maxNumberOfLives);
    }
    public void GameOver()
    {
        //Caricare scena nuova o resettare il livello
        this.enabled = false;
    }
}
