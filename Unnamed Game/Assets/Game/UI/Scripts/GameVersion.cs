using TMPro;
using UnityEngine;

namespace UnnamedGame.UI.Scripts
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
