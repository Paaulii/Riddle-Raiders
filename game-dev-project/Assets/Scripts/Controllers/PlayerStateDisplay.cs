using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateDisplay : MonoBehaviour
{
   [SerializeField] private Image[] hearts;

   public void ResetHearts()
   {
      foreach (var heart in hearts)
      {
         heart.enabled = true;
      }
   }
   
   public void DecreaseHeartsAmount()
   {
      Image lastEnabledImage = hearts.FirstOrDefault(image => image.enabled);

      if (lastEnabledImage) {
         lastEnabledImage.enabled = false;
      }
   }
}
