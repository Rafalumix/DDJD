using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
  public Image healthBarImage;
  public playerMovement player;
  public void UpdateHealthBar() {
    healthBarImage.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
    if (player.health == 0) {
        player.isDead = true; 
        player.AnimatorSon.SetBool("isDead", true);
    }
  }
}