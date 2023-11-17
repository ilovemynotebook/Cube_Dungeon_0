using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StageManager : MonoBehaviour
{
    public int MonsterCount;
    public int thisStage;
    public int thisPlane;
    public Plane plane;
    public UnityEngine.UI.Image panel;
    [SerializeField] List<Box> boxes = new List<Box>();
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    public GameStageDB GameStageDB;

    public void Awake()
    {
        thisStage = GameManager.Instance.ThisStage;
        thisPlane = GameManager.Instance.ThisPlane;
    }


    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        MonsterCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    void PlaneUp()
    {

    }
    IEnumerator FadeFlow(float F_time)
    {
        panel.gameObject.SetActive(true);
        float time = 0f;
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(0.2f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
}
