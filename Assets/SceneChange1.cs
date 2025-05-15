using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange1 : MonoBehaviour
{

    [Header("��������")]
    public Image whiteFadeImage;  // ȫ����ɫImage
    public float fadeDuration = 1f;  // ����ʱ��
    public void LoadSceneWithWhiteFade(string sceneName)
    {
        StartCoroutine(WhiteFadeTransition(sceneName));
    }

    IEnumerator WhiteFadeTransition(string sceneName)
    {
        // �������Image
        whiteFadeImage.gameObject.SetActive(true);

        // ��������
        float timer = 0f;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            whiteFadeImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // ȷ����ȫ��ɫ
        whiteFadeImage.color = Color.white;

        // �첽�����³���
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // �����ؽ��ȴﵽ90%ʱ�����
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // ��������
        timer = 0f;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            whiteFadeImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // ���ذ���
        whiteFadeImage.gameObject.SetActive(false);
    }
}
