using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class StageManager : MonoBehaviour
{
    public int MonsterCount;
    public int thisStage;
    public int thisPlane;
    public Plane[] planes;

    [SerializeField] List<Box> boxes = new List<Box>();
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    public GameStageDB GameStageDB;
    public GameObject MapPrefab;
    public UnityEngine.UI.Image panel;
    public Player player;
    public void Awake()
    {
        thisStage = GameManager.Instance.ThisStage;
        thisPlane = GameManager.Instance.ThisPlane;
        for (int i = 0; i < GameStageDB.stages.CubePlanes.Length; i++)
        {
            planes[i] = GameStageDB.stages.CubePlanes[i].Clone();
        }
    }


    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        MonsterCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }


    void BoxSpawn(Plane Plane)
    {

        for (int i = 0; i < Plane.boxes.Length; i++)
        {
            boxes.Add(Instantiate(Plane.boxes[i], Plane.BoxSpawnPlace[i], Quaternion.identity));
            boxes[i].isOpen = Plane.boxesOpened[i];
        }
    }

    public void CreateMap()
    {
        Fade();
        Clear();
        MapPrefab = Instantiate(planes[thisPlane - 1].Prefab);
        player = FindAnyObjectByType<Player>();
        player.transform.position = planes[thisPlane - 1].PlayerStartPoint;
        MonsterSpawn(planes[thisPlane - 1]);
        BoxSpawn(planes[thisPlane - 1]);
    }
    void MonsterSpawn(Plane Plane)
    {
        for (int i = 0; i < Plane.enemies.Length; i++)
        {
            enemies.Add(Instantiate(Plane.enemies[i], Plane.enemiesSpawnPlace[i], Quaternion.identity));
        }
    }
    void Clear()
    {
        if (MapPrefab != null)
        {
            Destroy(MapPrefab);
        }

        if (boxes.Count > 0)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                Destroy(boxes[i].gameObject);

            }
            boxes.Clear();
        }
        if (enemies.Count > 0)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                if (enemies[j] != null)
                    Destroy(enemies[j].gameObject);
            }
            enemies.Clear();
        }

    }
    public void StageSave(int Plane)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            planes[Plane].boxesOpened[i] = boxes[i].isOpen;
        }

    }


    void PlaneUp()
    {
        //다음 면 이동
        if (MonsterCount == 0)
        {
            
            Clear();
            if (planes.Length <= thisPlane)
            {   //Stage가 바뀔경우
                thisStage++;
                thisPlane = 1;
                SceneManager.LoadScene("Stage" + thisStage);

            }
            else
            {
                StageSave(thisPlane - 1);
                thisPlane++;
                CreateMap();
            }
            
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow(0.2f));
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
