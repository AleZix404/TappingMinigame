using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapProlongedModuleComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region variables
    [SerializeField] PlayTappingMinigameComponent playTappingMinigameComponent;
    [SerializeField] Animator animatorModuleCanvas;
    [SerializeField] Slider sliderHealth;
    int timer;

    public PlayTappingMinigameComponent PlayTappingMinigameComponent { get => playTappingMinigameComponent; set => playTappingMinigameComponent = value; }
    #endregion
    #region metodos publicos
    // Start is called before the first frame update
    void Start()
    {
        OpenModule();
    }
    #endregion
    #region metodos privados
    private void OpenModule()
    {
        //instanciar segun dificultad
        StartCoroutine(Cronometro());
    }
    private IEnumerator Cronometro()
    {
        yield return new WaitForSeconds(1f);
        if (timer == PlayTappingMinigameComponent.ModuleTapProTimer)
        {
            PlayTappingMinigameComponent.HealthHUB(false);
            gameObject.SetActive(false);
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
    #region metodos publicos
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(GoAction(true));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(GoAction(false));
    }
    public IEnumerator GoAction(bool addition)
    {
        while (timer <= PlayTappingMinigameComponent.ModuleTapProTimer)
        {
            if (addition)
            {
                if (sliderHealth.value >= playTappingMinigameComponent.TapProPtsMax)
                {
                    //StopCoroutine(GoAction(true));
                    break;
                }
                else 
                {
                    sliderHealth.value += (playTappingMinigameComponent.TapProPtsAddition / 50);
                }
               // if (sliderHealth.value == //PlayTappingMinigameComponent.TapProPtsMax)
               // {
               //     PlayTappingMinigameComponent.HealthHUB(true);
               //     gameObject.SetActive(false);
               // }
                Debug.Log("+=");
            }
            else
            {
                
                Debug.Log("-=");
                if (sliderHealth.value <= playTappingMinigameComponent.TapProPtsMin)
                {
                    //StopCoroutine(GoAction(false));
                    break;
                }
                else 
                {
                    sliderHealth.value -= (playTappingMinigameComponent.TapProPtsSustract * 0.2f);
                    Debug.Log("0.2f");
                }
            }
            yield return null;
        }
    }
    #endregion
}
