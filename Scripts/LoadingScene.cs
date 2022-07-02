using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.LevelHandler
{
    public class LoadingScene : MonoBehaviour
    {
        #region Fields And Properties
        private static LoadingScene _instance;

        [SerializeField] private Image _progressBarFill;

        public Image ProgressBar { get => _progressBarFill; }
        #endregion Fields And Properties

        #region Unity Methods
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion Unity Methods
    }
}