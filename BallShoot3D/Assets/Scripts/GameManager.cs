using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("----Ball Settings")]
    public GameObject[] balls;
    public GameObject FirePoint;
    [SerializeField] private float ballForce;
    int activeBallIndex;
    public Animator ballShoot;
    public ParticleSystem ballShootEffect;
    public ParticleSystem[] ballEffects;
    int activeEffectIndex;
    public AudioSource[] ballVoice;
    int activeBallVoiceIndex;


    [Header("----Level Settings")]
    [SerializeField] private int targetBallCount;
    [SerializeField] private int availableBallCount;
    int enterBallCount;
    public Slider levelSlider;
    public TextMeshProUGUI remainBallCount_text;

    [Header("----UI Settings")]
    public GameObject[] panels;
    public TextMeshProUGUI starCount;
    public TextMeshProUGUI winLevelNumber;
    public TextMeshProUGUI loseLevelNumber;

    [Header("----Other Settings")]
    public Renderer basketRend;
    float basketStartValue;
    float basketStepValue;
    public AudioSource[] otherAudio;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        activeBallVoiceIndex = 0;
        activeBallIndex = 0;
        basketStartValue = 0.5f; // tiling buna göre ayarlı diye
        basketStepValue = 0.25f / targetBallCount;  // kovadaki slider mantığının matematik hesabı
        levelSlider.maxValue = targetBallCount;
        remainBallCount_text.text = availableBallCount.ToString();

    }

    public void InsideBall()
    {
        enterBallCount++;
        levelSlider.value = enterBallCount;

        basketStartValue -= basketStepValue;
        basketRend.material.SetTextureScale("_MainTex", new Vector2(1f, basketStartValue)); // tiling değerine ulaşmak için kullanılan yöntem (meshrenderer içinde)

        ballVoice[activeBallVoiceIndex].Play();
        activeBallVoiceIndex++;
        if (activeBallVoiceIndex == ballVoice.Length - 1)   // başa döndüğünü anlamak için
        {
            activeBallVoiceIndex = 0;
        }

        if (enterBallCount == targetBallCount)
        {
            otherAudio[0].Play();
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 15); // bölüm sonu verilen yıldız sayısı
            starCount.text = PlayerPrefs.GetInt("Star").ToString();
            winLevelNumber.text = "Level" + SceneManager.GetActiveScene().name; // level bla bla yazdırmak için
            panels[1].SetActive(true);
        }
        if (availableBallCount == 0 && enterBallCount != targetBallCount)
        {
            otherAudio[1].Play();
            loseLevelNumber.text = "Level" + SceneManager.GetActiveScene().name;
            panels[2].SetActive(true);
        }
        if ((availableBallCount + enterBallCount) < targetBallCount)
        {
            otherAudio[1].Play();
            loseLevelNumber.text = "Level" + SceneManager.GetActiveScene().name;
            panels[2].SetActive(true);
        }

    }
    public void OutsideBall()
    {


        if (availableBallCount == 0)
        {
            otherAudio[1].Play();
            loseLevelNumber.text = "Level" + SceneManager.GetActiveScene().name;
            panels[2].SetActive(true);
        }
        if ((availableBallCount + enterBallCount) < targetBallCount)
        {
            otherAudio[1].Play();
            loseLevelNumber.text = "Level" + SceneManager.GetActiveScene().name;
            panels[2].SetActive(true);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            availableBallCount--;
            remainBallCount_text.text = availableBallCount.ToString();
            ballShoot.Play("BallShoot");
            ballShootEffect.Play();
            otherAudio[2].Play();
            balls[activeBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            balls[activeBallIndex].SetActive(true);
            balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(balls[activeBallIndex].transform.TransformDirection(90, 90, 0)
             * ballForce, ForceMode.Force);


            if (balls.Length - 1 == activeBallIndex)
            {
                activeBallIndex = 0;
            }
            else
            {
                activeBallIndex++;
            }
        }

    }

    public void GameStop()
    {
        panels[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void PanelButtons(string index)
    {
        switch (index)
        {
            case "Resume":
                Time.timeScale = 1;
                panels[0].SetActive(false);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Settings":
                break;
            case "Restart":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }

    }

    public void ParticleEffect(Vector3 pos, Color color) // amaç top ile effectin rengini aynı yapmak
    {
        ballEffects[activeBallIndex].transform.position = pos;
        var main = ballEffects[activeBallIndex].main;   // kullanımı böyle 
        main.startColor = color;
        ballEffects[activeBallIndex].gameObject.SetActive(true);
        activeBallIndex++;
        if (activeBallIndex == ballEffects.Length - 1)   // başa döndüğünü anlamak için
        {
            activeBallIndex = 0;
        }


    }
}
