using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SlotScript : MonoBehaviour, IDropHandler
{
    // ������� Inspector �����ó������ƣ���ʹ�ó�����
    [SerializeField] private string gameSceneName = "PlayScenes";

    // ���泡����֤������Ż����ܣ�
    private bool _isSceneValid;

    void Start()
    {
        // ����ʱԤ��֤������Ч��
        _isSceneValid = SceneExists(gameSceneName);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        ItemDrag item = dropped.GetComponent<ItemDrag>();
        if (item == null) return;

        HandleItemAction(item.itemType);
    }

    private void HandleItemAction(ItemDrag.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemDrag.ItemType.NewGame:
                ExecuteNewGame();
                break;

            case ItemDrag.ItemType.Continue:
                Debug.Log("����������Ϸ�¼�");
                // ������Ӵ浵�����߼�
                break;

            case ItemDrag.ItemType.Sound:
                ToggleSound();
                break;

            case ItemDrag.ItemType.Exit:
                QuitApplication();
                break;
        }
    }

    private void ExecuteNewGame()
    {
        Debug.Log("��������Ϸ�¼�");

        // �汾1��ֱ�ӳ�����ת
        if (_isSceneValid)
        {
            // ������Ϸ״̬��������ӣ�
            // PlayerPrefs.DeleteAll();
            // GameManager.Instance.ResetGame();

            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError($"��������ʧ��: {gameSceneName} �������ڹ�������");
            // ���Դ���UI������ʾ
        }

        // �汾2���첽���أ���Ҫ��ϼ��ؽ��棩
        // StartCoroutine(LoadSceneAsync());
    }

    #region �������ظ�������
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(scenePath) == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(gameSceneName);
        operation.allowSceneActivation = false;

        // ģ����ع��̣�ʵ����Ŀ��������ʵ���ȣ�
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // ���½���������Ҫ����UI�����
            // loadingBar.fillAmount = progress;

            if (progress >= 0.9f)
            {
                // �ȴ����ⰴ�����Զ�����
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    #endregion

    #region ��������ģ��
    private void ToggleSound()
    {
        Debug.Log("�������������¼�");
        // ʵ��ʵ��ʾ����
        // AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
    }

    private void QuitApplication()
    {
        Debug.Log("�����˳���Ϸ�¼�");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}