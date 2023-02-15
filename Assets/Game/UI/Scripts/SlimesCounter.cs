using TMPro;
using UnityEngine;

namespace Game.UI.Scripts
{
    public class SlimesCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void ShowEnemiesCount(int count)
        {
            text.text = count.ToString();
        }
    }
}
