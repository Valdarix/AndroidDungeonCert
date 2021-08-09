using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AdManager : MonoBehaviour, IUnityAdsListener
{
   
    [SerializeField] Button _showAdButton;
    [SerializeField] private int _rewardValue = 100;
    private string _adUnitId = "Rewarded_Android";
    [SerializeField] private string _AndroidAdID;
    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
        if (_player == null)
            Debug.Log("Player is null");
        _showAdButton.interactable = Advertisement.IsReady(_adUnitId);
        Advertisement.AddListener(this);
        Advertisement.Initialize(_AndroidAdID, true);
    }

    public void PlayAdForReward()
    {
        Advertisement.Show(_adUnitId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _adUnitId)
            _showAdButton.interactable = true;
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads did not load");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       //Ad is playing, nothing to do 
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Finished:
                _player.AddGems(_rewardValue);
                break;
            case ShowResult.Skipped:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
        }
    }
}
