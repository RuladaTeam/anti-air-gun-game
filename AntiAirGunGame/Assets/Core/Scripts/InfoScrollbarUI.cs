using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts
{
    public class InfoScrollbarUI : MonoBehaviour
    {
        private void Start()
        {
            Scrollbar scrollbar = GetComponent<Scrollbar>();
            scrollbar.value = 1;
        }
    }
}
