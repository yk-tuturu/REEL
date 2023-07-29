using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class transitions : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject panelPrefab;
    public GameObject finalRect;
    public GameObject blackOutPanel;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void transitionToBossFight()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        var shopScreen = GameObject.Find("ShopScreen");
        if (shopScreen != null)
        {
            shopScreen.GetComponent<ShopScreen>().Close();
        }
        
        // plays movement 9, a slight time buffer is placed between the param change and the beginning of the animation
        bgmScript.instance.SetParameter(9);

        yield return new WaitForSeconds(2f);

        // double making sure the file has been saved
        SaveSystem.instance.Save(0);

        // transition begins
        iTween.ShakePosition(gameCamera, new Vector3(20, 20, 20), 6f);

        yield return new WaitForSeconds(1f);

        GameObject panel = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        var rect = panel.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, 0, 0);

        finalRect = panel;

        iTween.ValueTo(panel, iTween.Hash("from", 0.1f, "to", 0.95f, "time", 0.85f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1f);

        iTween.ValueTo(panel, iTween.Hash("from", 0.95f, "to", 0f, "time", 0.85f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1f);

        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 0.95f, "time", 0.85f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1f);

        iTween.ValueTo(panel, iTween.Hash("from", 0.95f, "to", 0f, "time", 0.85f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1f);

        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 1f, "time", 1.2f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "changeScene", "oncompletetarget", this.gameObject));
    }

    public void updateColor(float val)
    {
        Debug.Log(val.ToString());
        Image image = finalRect.GetComponent<Image>();
        image.color = new Color(1, 1, 1, val);
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Scenes/fishOverlord");
    }

    public void FadeIn()
    {
        iTween.ValueTo(blackOutPanel, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "updateColorBlack", "onupdatetarget", this.gameObject));
    }

    void updateColorBlack(float val)
    {
        Debug.Log(val.ToString());
        Image image = blackOutPanel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, val);
    }

    public void FadeToBlack()
    {
        iTween.ValueTo(blackOutPanel, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "updateColorBlack", "onupdatetarget", this.gameObject, "oncomplete", "reloadScene", "oncompletetarget", this.gameObject));
    }
}
