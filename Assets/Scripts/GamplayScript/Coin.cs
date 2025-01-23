using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollectArea"))
        {
            // Add coin value to player's total money
            MoneyManager.Instance.AddCoins(coinValue);
            AudioManager.instance.Play("coin");
            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
