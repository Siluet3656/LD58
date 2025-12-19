using System;
using System.Collections;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public TMPTextAnimator textAnimator1;
    public TMPTextAnimator textAnimator2;
    public TMPTextAnimator textAnimator3;
    public TMPTextAnimator textAnimator4;
    public TMPTextAnimator textAnimator5;
    public TMPTextAnimator textAnimator6;
    public TMPTextAnimator textAnimator7;
    
    public RandomPrefabFieldPlacer randomPrefabFieldPlacer;
    
    private string _endingText1;
    private string _endingText2;
    private string _endingText3;
    private string _endingText4;
    private string _endingText5;
    private string _endingText6;
    private string _endingText7;
    private string _endingText8;
    private string _endingText9;

    public bool IsLBM;

    private void Awake()
    {
        G.Ending = this;
        
        _endingText1 = LocalizationManager.Instance.Get("Ending1");
        _endingText2 = LocalizationManager.Instance.Get("Ending2");
        _endingText3 = LocalizationManager.Instance.Get("Ending3");
        
        _endingText4 = LocalizationManager.Instance.Get("Ending4");
        _endingText5 = LocalizationManager.Instance.Get("Ending5");
        _endingText6 = LocalizationManager.Instance.Get("Ending6");
        
        _endingText7 = LocalizationManager.Instance.Get("Ending7");
        _endingText8 = LocalizationManager.Instance.Get("Ending8");
        
        _endingText9 = LocalizationManager.Instance.Get("Ending9");
    }

    private void Start()
    {
        StartCoroutine(EndingRoutine());
    }

    private IEnumerator EndingRoutine()
    {
        yield return new WaitForSeconds(2f);
        IsLBM = false;

        End1();

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;
        
        End2();

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;
        
        End3();

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;
        
        End4();

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;
        
        End5();
        
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;

        End6();
        
        randomPrefabFieldPlacer.PlaceAll();
        
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;

        End7();
        
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;

        End8();
        
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => IsLBM);
        IsLBM = false;

        End9();
    }

    public void End1()
    {
        textAnimator1.StartTextAnimation($"<color=#ff0000>{_endingText1}</color>");
    }

    public void End2()
    {
        textAnimator2.StartTextAnimation($"<color=#ff0000>{_endingText2}</color>");
    }

    public void End3()
    {
        textAnimator3.StartTextAnimation($"<color=#ff0000>{_endingText3}</color>");
    }

    public void End4()
    {
        textAnimator4.StartTextAnimation($"<color=#ff0000>{_endingText5}</color>");
    }

    public void End5()
    {
        textAnimator5.StartTextAnimation($"{_endingText4}");
    }

    public void End6()
    {
        textAnimator1.StartTextAnimation("");
        textAnimator2.StartTextAnimation("");
        textAnimator3.StartTextAnimation("");
        textAnimator4.StartTextAnimation("");
        textAnimator5.StartTextAnimation("");
        
        textAnimator1.StopSound();
        
        textAnimator6.StartTextAnimation($"<color=#ff0000>{_endingText6}</color>");
    }
    
    public void End7()
    {
        textAnimator6.StartTextAnimation("");
        
        textAnimator6.StopSound();
        
        textAnimator7.StartTextAnimation($"{_endingText7}");
    }
    
    public void End8()
    {
        textAnimator7.StartTextAnimation($"{_endingText8}");
    }
    
    public void End9()
    {
        textAnimator7.StartTextAnimation($"{_endingText9}");
    }
}
