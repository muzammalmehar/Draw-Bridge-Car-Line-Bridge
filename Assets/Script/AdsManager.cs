using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class AdsManager : MonoBehaviour
{
   private string bannerAdId = "ca-app-pub-3940256099942544/6300978111";
   private string interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
   private InterstitialAd _interstitialAd;

   BannerView _bannerView;
   public static AdsManager instance;
   private void Awake(){

    DontDestroyOnLoad(this.gameObject);

    if(instance == null){

        instance = this;
    }
   }
   public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        });
        CreateBannerView();
        LoadInterstitialAd();
    }

    public void CreateBannerView()
  {
      Debug.Log("Creating banner view");

      // If we already have a banner, destroy the old one.
      if (_bannerView != null)
      {
          DestroyAd();
      }

      // Create a 320x50 banner at top of the screen
      _bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
  }

  public void LoadInterstitialAd()
  {
      // Clean up the old ad before loading a new one.
      if (_interstitialAd != null)
      {
            _interstitialAd.Destroy();
            _interstitialAd = null;
      }

      Debug.Log("Loading the interstitial ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      InterstitialAd.Load(interstitialAdId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              _interstitialAd = ad;
          });
  }

  public void LoadAd()
{
    // create an instance of a banner view first.
    if(_bannerView == null)
    {
        CreateBannerView();
    }

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    Debug.Log("Loading banner ad.");
    _bannerView.LoadAd(adRequest);
}

public void ShowInterstitialAd()
{
    if (_interstitialAd != null && _interstitialAd.CanShowAd())
    {
        Debug.Log("Showing interstitial ad.");
        _interstitialAd.Show();
    }
    else
    {
        Debug.LogError("Interstitial ad is not ready yet.");
    }
}

/// <summary>
/// listen to events the banner view may raise.
/// </summary>
private void ListenToAdEvents()
{
    // Raised when an ad is loaded into the banner view.
    _bannerView.OnBannerAdLoaded += () =>
    {
        Debug.Log("Banner view loaded an ad with response : "
            + _bannerView.GetResponseInfo());
    };
    // Raised when an ad fails to load into the banner view.
    _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
    {
        Debug.LogError("Banner view failed to load an ad with error : "
            + error);
    };
    // Raised when the ad is estimated to have earned money.
    _bannerView.OnAdPaid += (AdValue adValue) =>
    {
       // Debug.Log(String.Format("Banner view paid {0} {1}.",
        //    adValue.Value,
       //     adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    _bannerView.OnAdImpressionRecorded += () =>
    {
        Debug.Log("Banner view recorded an impression.");
    };
    // Raised when a click is recorded for an ad.
    _bannerView.OnAdClicked += () =>
    {
        Debug.Log("Banner view was clicked.");
    };
    // Raised when an ad opened full screen content.
    _bannerView.OnAdFullScreenContentOpened += () =>
    {
        Debug.Log("Banner view full screen content opened.");
    };
    // Raised when the ad closed full screen content.
    _bannerView.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Banner view full screen content closed.");
    };
}
/// <summary>
/// Destroys the banner view.
/// </summary>
public void DestroyAd()
{
    if (_bannerView != null)
    {
        Debug.Log("Destroying banner view.");
        _bannerView.Destroy();
        _bannerView = null;
    }
}
}
