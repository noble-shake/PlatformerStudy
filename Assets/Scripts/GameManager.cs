using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public enum SceneNums
{
    MainScene,
    PlayScene,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("적기생성")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;//3?
    List<GameObject> listSpawnEnemy = new List<GameObject>();

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f;//스폰타이머 
    Transform trsSpawnPoint;

    [Header("적기생성 카메라리밋")]
    [SerializeField] Vector2 vecCamMinMax;//기획자가 설정하는 위치값, 카메라로부터
    Vector2 vecSpawnLimit;//월드 포지션 기준 생성 리밋 위치값, 월드포지션

    Camera mainCam;

    [Header("Item Drop Rate")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;
    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    [SerializeField] GameObject objPlayer;

    [Header("gauge")]
    [SerializeField] Slider slider;
    [SerializeField] Image sliderFill;
    [SerializeField] TMP_Text textTimer;

    float bossSpawnTime = 60f;
    [SerializeField] float gameTime = 0f;
    bool spawnBoss = false;
    float colorRatio = 0f;
    Color colorBossHp;

    [SerializeField] int killCountBossSpawn = 60;
    [SerializeField] int killCount = 0;

    [Header("Score")]
    [SerializeField] TMP_Text textScore;
    int score;

    [Header("Game Over Menu")]

    [SerializeField] GameObject objGameoverMenu;
    [SerializeField] TMP_Text textGameover;
    [SerializeField] TMP_InputField iFGameover;
    [SerializeField] Button btnGameover;
    int rank = -1;
    string keyRankData = "rankData";

    public class cRank {
        public int score = 0;
        public string name = "";
    }
    List<cRank> listRanks = new List<cRank>(); // 0 ~ 9 ranks


    private void Awake()
    {
        if (Tool.IsEnterFirstScene == false) {
            SceneManager.LoadScene((int)SceneNums.MainScene);
            return;
        }

        // SingleTon ~ GameManger is Only One Object
        if (Instance == null)
        {
            Instance = this;
        }
        else { 
            Destroy(this);
        }

        initGameoverMenu();
    }

    private void initGameoverMenu() {
        if (objGameoverMenu.activeSelf == true) {
            objGameoverMenu.SetActive(false);
        }
        btnGameover.onClick.AddListener(saveAndNextScene); // lambda or onclick function
        //btnGameover.onClick.AddListener(Test1);
        //btnGameover.onClick.AddListener(delegate { Test2(1); });
        //btnGameover.onClick.AddListener(() => Test2(1));
    }

    private void initRank()
    {
        // JSON
        // JsonConvert // In Unity, this Class will convert unity variables to TEXTT
        // jsonutilty (unity official) 는 list 내부에 있는 내용을 파싱 할 수 없는 문제가 있음.
        // 퍼포먼스는 unity 께 더 좋긴 함.
        //string value = JsonConvert.SerializeObject(listRanks);
        //listRanks = JsonConvert.DeserializeObject<List<cRank>>(value);
        string rankValue = PlayerPrefs.GetString(keyRankData, string.Empty); // key, default
        if (rankValue == string.Empty)
        {
            int count = 10;
            for (int i = 0; i < count; i++)
            {
                listRanks.Add(new cRank()); 
            }
            rankValue = JsonConvert.SerializeObject(listRanks);
            PlayerPrefs.SetString(keyRankData, rankValue);

        }
        else {
            listRanks = JsonConvert.DeserializeObject<List<cRank>>(rankValue);
        }

    }

    // If Player Dead, current rank check
    private int GetRank() {
        int _score = score;
        int count = listRanks.Count;
        for (int iNum = 0; iNum < count; iNum++) { 
            cRank rank = listRanks[iNum];
            if (rank.score < _score) {
                return iNum + 1;
            }
        }
        return -1;
    }

    private void saveAndNextScene() {
        // If In rank, name / score 를 rankList에 저장해두고 11등은 삭제
        // not in rnak, 다른씬으로 이동 (메인 씬)
                                    
        int currentRank = GetRank();

        if (currentRank != -1)
        {
            cRank newRank = new cRank() { score = score, name = iFGameover.text };
            Debug.Log(newRank.score);
            Debug.Log(newRank.name);
            listRanks.Insert(currentRank - 1, newRank);
            int count = listRanks.Count;
            listRanks.RemoveAt(count - 1);

            string saveValue = JsonConvert.SerializeObject(listRanks);
            PlayerPrefs.SetString(keyRankData, saveValue);
        }

        //int count = listRanks.Count;
        //for (int iNum = 0; iNum < count; iNum++)
        //{
        //    cRank rank = listRanks[iNum];
        //    if (rank.score < score)
        //    {
        //        cRank newRank = new cRank() { score = score, name = textGameover.text };
        //        listRanks.Insert(iNum + 1, newRank);
        //        listRanks.RemoveAt(count);
        //        break;
        //    }
        //}

        //SceneManager.LoadScene // 한 프레임 내에 다른 씬을 로드한다. - 이제 아무도 안씀
        // LoadSceneAsync // 로드가 완료될떄까지 진행 
        // Scene Number  / Scene Name (not recommended)

        SceneManager.LoadSceneAsync((int)SceneNums.MainScene); 


    }


    private void Start()
    {
        initSlider();
        mainCam = Camera.main;
        trsSpawnPoint = transform.Find("SpawnPoint");

        vecSpawnLimit.x = mainCam.ViewportToWorldPoint(
            new Vector3(vecCamMinMax.x, 0f)
            ).x;

        vecSpawnLimit.y = mainCam.ViewportToWorldPoint(
            new Vector3(vecCamMinMax.y, 0f)
            ).x;

    }

    void Update()
    {
        checkSpawn();
        checkTime();
        // checkKillCount(); no need to check each frame in update
    }

    private void checkKillCount()
    {
        if (spawnBoss == false && killCountBossSpawn == killCount) {
            clearEnemy();
            createBoss();
            spawnBoss = true;
            
        }
        modifySlider();
    }

    private void checkSpawn()//적기를 소환해도 되는지 체크
    {
        if (isSpawn == false || spawnBoss == true) return;

        sTimer += Time.deltaTime;
        if (sTimer >= spawnTime && gameTime < bossSpawnTime - 1)
        {
            sTimer = 0.0f;

            spawnEnemy();//적기생산
        }
    }

    private void checkTime() {
        if (spawnBoss == true)
        {
            if (sliderFill.color != colorBossHp) {
                if (colorRatio != 1.0f) {
                    colorRatio += Time.deltaTime;
                    if (colorRatio > 1.0f) {
                        colorRatio = 1.0f;
                    }
                }
                
            }
            if (sliderFill.color != Color.red) {


                sliderFill.color = Color.red;
            }
        }
        else {
            if (sliderFill.color == Color.red) {
                sliderFill.color = new Color(0.3f, 0.7f, 1f, 1f);
            }
            gameTime += Time.deltaTime;


            if (gameTime > bossSpawnTime)
            {
                gameTime = bossSpawnTime;
                spawnBoss = true;
                isSpawn = false;
                clearEnemy();
                createBoss();
            }

            modifySlider();
        }


    }

    public void BossKill() {
        spawnBoss = false;
        isSpawn = true;
        initTimer();
    }

    public void HitBoss(int _curHp, int _maxHp) {
        textTimer.text = $"{_curHp.ToString("D2")} / {_maxHp.ToString("D4")}";
        slider.maxValue = _maxHp;
        slider.value = _curHp;
    }

    private void initTimer() {
        gameTime = 0f;
        bossSpawnTime += 60;

        if (spawnTime != 0.1f) {
            spawnTime -= 0.1f;
        }
        initSlider();
    }

    private void modifySlider()
    {
        slider.value = gameTime;
        // ToString's parameters
        // ( ) => "0010"
        // D3 ~ 세자리 수까지 : 10 -> 010
        // N2 ..소숫점 수까지
        textTimer.text = $"{((int)gameTime).ToString("D2")} / {((int)slider.maxValue).ToString("D2")}";
        // textTimer.text = $"{(int)gameTime} / {slider.maxValue}";
        // System.DateTime.Now.ToString(); // os time. never use!!
        // server synchronize needs datetime

        // killcount system.
        //slider.value = killCount;
        //textTimer.text = $"{killCount} / {killCountBossSpawn}";
    }

    public void addKillCount() {

        return;

        killCount++;
        if (killCount > killCountBossSpawn) {
            killCount = killCountBossSpawn;
            spawnBoss = true;
        }

        checkKillCount();
    }

    private void initSlider() {
        slider.maxValue = bossSpawnTime;
        //slider.maxValue = killCountBossSpawn;
        slider.value = 0f;
        //textTimer.text = $"{slider.value} / {slider.maxValue}";
        textTimer.text = $"{slider.value} / {slider.maxValue}";
    }

    private void clearEnemy() {
        int count = listSpawnEnemy.Count;
        for (int iNum = count - 1; iNum > -1; --iNum) {
            Destroy(listSpawnEnemy[iNum]);
        }
        // listSpawnEnemy.Clear();
    }

    private void createBoss()
    {
        // GameObject go = Instantiate(listEnemy[3], newPos, Quaternion.identity);
        GameObject go = listEnemy[listEnemy.Count - 1];
        Instantiate(go, trsSpawnPoint.position, Quaternion.identity);
        
    }

    private void spawnEnemy()//적기를 생산합니다.
    {
        float rand = UnityEngine.Random.Range(0.0f, 100.0f);
        GameObject objEnemy = listEnemy[0];
        if (rand < 50.0f)
        {
            objEnemy = listEnemy[0];
        }
        else if (rand < 75.0f)
        {
            objEnemy = listEnemy[1];
        }
        else
        {
            objEnemy = listEnemy[2];
        }

        Vector3 newPos = trsSpawnPoint.position;
        newPos.x = UnityEngine.Random.Range(vecSpawnLimit.x, vecSpawnLimit.y);
        GameObject go = Instantiate(objEnemy, newPos, Quaternion.identity);

        // listSpawnEnemy.Add(go);
        AddSpawnEnemyList(go);

        float rate = UnityEngine.Random.Range(0.0f, 100.0f);
        if (rate <= itemDropRate) {
            Enemy gosc = go.GetComponent<Enemy>();
            gosc.setHaveItem();
        }
    }

    public void dropItem(Vector3 _pos) {
        int raniNum = UnityEngine.Random.Range(0, itemList.Count);
        Instantiate(itemList[raniNum], _pos, Quaternion.identity);
    }

    public Transform GetPlayerTransform() {
        if (objPlayer == null)
        {
            return null;
        }
        else {
            return objPlayer.transform;
        }
    }

    public void AddSpawnEnemyList(GameObject _value) {
        // 중복 처리
        //if (listSpawnEnemy.Exists((x) => x == _value) == false)
        //{
        //    listSpawnEnemy.Add(_value);
        //}
        listSpawnEnemy.Add(_value);
    }

    public void RemoveSpawnEnemyList(GameObject _value) {
        listSpawnEnemy.Remove(_value);
    }

    public void DestroyEnemy(Enemy.enumEnemy _value)
    {
        switch (_value) {
            case Enemy.enumEnemy.EnemyA:
                score += 1;
                break;
            case Enemy.enumEnemy.EnemyB:
                score += 2;
                break;
            case Enemy.enumEnemy.EnemyC:
                score += 3;
                break;
            case Enemy.enumEnemy.Boss:
                score += 4;
                break;
        }

        //textScore.text = score.ToString("D8");
        textScore.text = score.ToString("D2");

    }

    public void GameOver() {
        int rank = GetRank();
        if (rank != -1)
        {
            textGameover.text = $"{rank} 등 달성!";
            iFGameover.gameObject.SetActive(true);

        }
        else {
            textGameover.text = "순위 권 내에 들지 못했습니다.";
            iFGameover.gameObject.SetActive(false);                                                                                                                                                                     
        }

        objGameoverMenu.gameObject.SetActive(true);
        iFGameover.Select();
    }


    // on click button -> 변수 2개 이상 사용이 안됨, 참조도 안됨, 코드로 하는게 좋다.
    public void Test1() {
        Debug.Log("Test1");
    }

    public void Test2(int _value)
    {
        Debug.Log($"Test2 {_value}");
    }

    public void Test3(float _value)
    {
        Debug.Log("Test3");
    }

    public void Test4(bool _value)
    {
        Debug.Log("Test4");
    }
}
