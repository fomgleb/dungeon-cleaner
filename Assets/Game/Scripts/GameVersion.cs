using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class GameVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text versionText;

        private void Start()
        {
            versionText.text = $"v{Application.version}";
        }
    }
}
