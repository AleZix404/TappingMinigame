using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayTappingMinigameComponent : MonoBehaviour
{
    #region variables
    [Header("Manager Data")]
    [SerializeField] int cantModulos;
    [SerializeField] difficulty selectedDifficulty = difficulty.Easy;
    [SerializeField] modulos selectedmodule = modulos.tapRepetido;
    private minigameMode selectedMode = minigameMode.Single;

    [Header("Texto de introduccion")]
    [SerializeField] private Text txtCountGO;
    [SerializeField] private float delayCountGO;

    [Header("Slider Healt")]
    [SerializeField] Slider sliderHUB;
    [SerializeField] int sliderPtsMax;
    [SerializeField] int sliderPtsMin;

    [Header("Tap Heart Module")]
    [SerializeField] float moduleTapTimer;
    float tapHeartPtsMax;
    float tapHeartPtsMin;
    float tapHeartPtsSustract;
    float tapHeartPtsAddition;

    [Header("Tap Prolonged Module")]
    [SerializeField] float moduleTapProTimer;
    float tapProPtsMax;
    float tapProPtsMin;
    float tapProPtsSustract;
    float tapProPtsAddition;

    public float TapHeartPtsMax { get => tapHeartPtsMax; set => tapHeartPtsMax = value; }
    public float TapHeartPtsMin { get => tapHeartPtsMin; set => tapHeartPtsMin = value; }
    public float TapHeartPtsSustract { get => tapHeartPtsSustract; set => tapHeartPtsSustract = value; }
    public float TapHeartPtsAddition { get => tapHeartPtsAddition; set => tapHeartPtsAddition = value; }
    public float ModuleTapTimer { get => moduleTapTimer; set => moduleTapTimer = value; }
    public float TapProPtsSustract { get => tapProPtsSustract; set => tapProPtsSustract = value; }
    public float TapProPtsAddition { get => tapProPtsAddition; set => tapProPtsAddition = value; }
    public float TapProPtsMin { get => tapProPtsMin; set => tapProPtsMin = value; }
    public float TapProPtsMax { get => tapProPtsMax; set => tapProPtsMax = value; }
    public float ModuleTapProTimer { get => moduleTapProTimer; set => moduleTapProTimer = value; }
    #endregion

    #region metodos publicos
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textGO());
        ApplyDifficulty();
    }
    #endregion

    #region metodos privados
    IEnumerator textGO()
    {
        yield return new WaitForSecondsRealtime(delayCountGO);
        txtCountGO.text = "3";
        yield return new WaitForSecondsRealtime(delayCountGO);
        txtCountGO.text = "2";
        yield return new WaitForSecondsRealtime(delayCountGO);
        txtCountGO.text = "1";
        yield return new WaitForSecondsRealtime(delayCountGO);
        txtCountGO.text = "";
    }

    void ApplyDifficulty()
    {
        switch (selectedDifficulty)
        {
            case difficulty.VeryEasy:
                selectedMode = minigameMode.Single;
                selectedmodule = modulos.tapRepetido;
                selectedmodule = modulos.tapProlongado;
                GetModuleData();
                break;
            case difficulty.Easy:
                selectedMode = minigameMode.Single;
                break;
            case difficulty.Medium:
                selectedMode = minigameMode.Alternating;
                break;
            case difficulty.Hard:
                selectedMode = minigameMode.Random;
                break;
            case difficulty.VeryHard:
                selectedMode = minigameMode.RandomWithPenalty;
                break;
            case difficulty.Tutorial:
                selectedMode = minigameMode.Alternating;
                break;
            case difficulty.TutorialAdvanced:
                selectedMode = minigameMode.RandomWithPenalty;
                break;
        }
    }
    //debe invocarse en la dificultad no en start
    void GetModuleData() 
    {
        switch (selectedmodule)
        {
            case modulos.tapRepetido:
                tapHeartPtsMax = 1f;
                tapHeartPtsMin = 0;
                tapHeartPtsSustract = .1f;
                tapHeartPtsAddition = .1f;
                PtsHUB = .2f;
                ModuleTapTimer = 3f;
                break;
            case modulos.tapProlongado:
                TapProPtsMax = 1f;
                TapProPtsMin = 0;
                TapProPtsSustract = .1f;
                TapProPtsAddition = .1f;
                PtsHUB = .2f;
                ModuleTapProTimer = 100f;
                break;
            case modulos.tapIntermitente:
                Debug.Log("");
                break;
            case modulos.tapArrastrar:
                Debug.Log("");
                break;
        }
    }
    public float PtsHUB;
    public void HealthHUB(bool addition) 
    {
        if (addition)
        {
            sliderHUB.value += PtsHUB;
            Debug.Log("Mejorar sprite SWITCH");
            //Mejorar sprite SWITCH
        }
        else
        {
            if (sliderHUB.value == sliderPtsMin)
            {
                Debug.Log("SOLO deteriorar sprite");
            }
            else
            {
                sliderHUB.value -= PtsHUB;
                Debug.Log("Deteriorar sprite");
                //Deteriorar sprite
            }
        }
    }
    public enum modulos
    {
        tapRepetido,
        tapProlongado,
        tapIntermitente,
        tapArrastrar
    }
    public enum difficulty
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard,
        Tutorial,
        TutorialAdvanced
    }
    public enum minigameMode
    {
        Single,
        Alternating,
        Random,
        RandomWithPenalty,
    }
    #endregion
}
