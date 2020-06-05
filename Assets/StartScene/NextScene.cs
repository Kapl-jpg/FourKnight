using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace StartScene
{
    public class NextScene : MonoBehaviour,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
