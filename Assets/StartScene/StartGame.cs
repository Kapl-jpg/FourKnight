using UnityEngine;
using UnityEngine.UI;
namespace StartScene
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Image lighting;
        [SerializeField] private Image logo;
        [SerializeField] private Text pressStartText;
        private float _lightTimer;
        private float _logoTimer;
        private float _a = 1;
        private float _color;
        [SerializeField] [Range(0, 1)] private float rgb;
        [SerializeField] [Range(0, 1)] private float timeToEndFlash;
        private float _timerFlash;
        [SerializeField] [Range(0, 5)] private float timeToLight;
        [SerializeField] [Range(0, 5)] private float timeToEnabledLogo;
        private bool _light;
        private bool _dark;

        void Start()
        {
            lighting.enabled = false;
            _color = 0;
            logo.GetComponent<Image>().color = new Color(1, 1, 1, _color);
        }

        void Update()
        {
            if (!_light)
            {
                _timerFlash = 0;
                lighting.enabled = false;
                _lightTimer += Time.deltaTime;
            }
            else
            {
                _timerFlash += Time.deltaTime;
                if (_timerFlash >= timeToEndFlash)
                {
                    _light = false;
                }

                _color = 1;
                lighting.enabled = true;
                _lightTimer = 0;
            }

            if (_lightTimer >= timeToLight)
            {
                _light = true;
            }
            Logo();
            if (_a >= 1)
            {
                _dark = true;
            }

            if (_dark)
            {
                _a -= Time.deltaTime;
            }
            else
            {
                _a += Time.deltaTime;
            }

            if (_a <= 0)
            {
                _dark = false;
            }

            pressStartText.GetComponent<Text>().color = new Color(1, 1, 1, _a);
        }

        void Logo()
        {

            if (!_light)
            {
                if (_color >= rgb)
                {
                    _color = rgb;
                }
                else
                {
                    _color += Time.deltaTime / timeToEnabledLogo;
                }
            }

            logo.GetComponent<Image>().color = new Color(1, 1, 1, _color);
        }
    }
}
