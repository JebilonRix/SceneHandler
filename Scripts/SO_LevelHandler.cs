using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

namespace RedPanda.LevelHandler
{
    [CreateAssetMenu(fileName = "LevelHandler", menuName = "Red Panda/Level Handler")]
    public class SO_LevelHandler : ScriptableObject
    {
        #region Fields
        [SerializeField] private LoadingScene _loadingScenePrefab;

        private string _currentSceneName;
        private bool _isSetted;
        #endregion Fields

        #region Unity Methods
        private void OnEnable()
        {
            _currentSceneName = GetSceneByBuildIndex(0).name;
            _isSetted = false;
        }
        private void OnDestroy()
        {
            _isSetted = false;
        }
        #endregion Unity Methods

        #region Public Methods
        public async void LoadScene(string _sceneName)
        {
            if (!GetSceneByName(_sceneName).IsValid())
            {
                Debug.Log(_sceneName + " named scene is not exist.");
                return;
            }

            LoadingPrefab(true);

            AsyncOperation scene = LoadSceneAsync(_sceneName);
            await LoadTheScene(scene);

            _currentSceneName = _sceneName;
        }
        public void RestartScene() => LoadScene(_currentSceneName);
        public void QuitGame() => Application.Quit();
        #endregion Public Methods

        #region Private Methods
        private async Task LoadTheScene(AsyncOperation scene)
        {
            scene.allowSceneActivation = false;

            do
            {
                await Task.Delay(100);

                if (_loadingScenePrefab != null)
                {
                    _loadingScenePrefab.ProgressBar.fillAmount = scene.progress;
                }
            }
            while (scene.progress < 0.9f);

            scene.allowSceneActivation = true;

            LoadingPrefab(false);
        }
        private void LoadingPrefab(bool activate)
        {
            if (_loadingScenePrefab == null || _loadingScenePrefab.ProgressBar == null)
            {
                return;
            }

            if (!_isSetted)
            {
                _loadingScenePrefab.ProgressBar.type = Image.Type.Filled;
                _loadingScenePrefab.ProgressBar.fillMethod = Image.FillMethod.Horizontal;
                _isSetted = true;
            }

            _loadingScenePrefab.ProgressBar.fillAmount = 0;
            _loadingScenePrefab.gameObject.SetActive(activate);
        }
        #endregion Private Methods
    }
}