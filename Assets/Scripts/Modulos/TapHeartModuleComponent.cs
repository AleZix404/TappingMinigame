using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class TapHeartModuleComponent : MonoBehaviour
{
    #region variables
    [SerializeField] PlayTappingMinigameComponent playTappingMinigameComponent;
    [SerializeField] Animator animatorModuleCanvas;
    [SerializeField] GameObject parentModule;
    [SerializeField] Slider sliderHealth;
    [SerializeField] Button btnAction;
    int timer;

    public PlayTappingMinigameComponent PlayTappingMinigameComponent { get => playTappingMinigameComponent; set => playTappingMinigameComponent = value; }
    #endregion
    #region metodos publicos
    // Start is called before the first frame update
    void Start()
    {
        OpenModule();
        AddListenerAction(btnAction, delegate 
        {
            StartCoroutine(GoAction());
        });
    }
    #endregion
    #region metodos publicos
    void AddListenerAction(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    void OpenModule()
    {
        //instanciar segun dificultad
        StartCoroutine(Cronometro());
    }
    public IEnumerator GoAction()
    {
        if (timer <= PlayTappingMinigameComponent.ModuleTapTimer)
        {
            //suma slider
            sliderHealth.value = sliderHealth.value <= PlayTappingMinigameComponent.TapHeartPtsMax ? sliderHealth.value += PlayTappingMinigameComponent.TapHeartPtsAddition : PlayTappingMinigameComponent.TapHeartPtsMax;

            if (sliderHealth.value == PlayTappingMinigameComponent.TapHeartPtsMax)
            {
                PlayTappingMinigameComponent.HealthHUB(true);
                parentModule.SetActive(false);
            }
        }
        
        yield return null;
    }
    #region metodos privados
    private IEnumerator Cronometro()
    {
        yield return new WaitForSeconds(1f);
        if (PlayTappingMinigameComponent.ModuleTapTimer == timer)
        {
            if (timer == PlayTappingMinigameComponent.ModuleTapTimer)
            {
                PlayTappingMinigameComponent.HealthHUB(false);
                parentModule.SetActive(false);
            }
            StopCoroutine(Cronometro());
        }
        else
        {
            timer += 1;
            Debug.Log("tp es: " + timer);
            StartCoroutine(Cronometro());
        }
    }
    #endregion
    #endregion
}
