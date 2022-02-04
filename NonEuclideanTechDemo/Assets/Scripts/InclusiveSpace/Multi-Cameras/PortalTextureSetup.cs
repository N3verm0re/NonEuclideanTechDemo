using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;
    public Material cameraAMat;
    public Material cameraBMat;

    public bool playerIsInOtherWorld;
    public GameObject FrontPanel1;
    public GameObject FrontPanel2;
    public GameObject BackPanel1;
    public GameObject BackPanel2;

    //probably should stop using singletons in the future, otherwise multicamera trick is limited to one per scene
    //regardless, keeping it for now to maintain consistency of textures
    #region Singleton
    private static PortalTextureSetup _textureManager = null;
    public static PortalTextureSetup TextureManager
    {
        get
        {
            return _textureManager;
        }
    }

    private void Awake()
    {
        if (_textureManager != null && _textureManager != this)
        {
            Destroy(this);
        }
        else
        {
            _textureManager = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion
    private void Start()
    {
        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraAMat.mainTexture = cameraA.targetTexture;

        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraBMat.mainTexture = cameraB.targetTexture;

        if (!playerIsInOtherWorld)
        {
            FrontPanel1.SetActive(true);
            BackPanel1.SetActive(true);
            FrontPanel2.SetActive(false);
            BackPanel2.SetActive(false);
        }
        else
        {
            FrontPanel1.SetActive(false);
            BackPanel1.SetActive(false);
            FrontPanel2.SetActive(true);
            BackPanel2.SetActive(true);
        }
        //playerIsInOtherWorld = false;
    }

    public void SwapPlayerWorld()
    {
        if (!playerIsInOtherWorld)
        {
            FrontPanel1.SetActive(false);
            BackPanel1.SetActive(false);
            FrontPanel2.SetActive(true);
            BackPanel2.SetActive(true);
            playerIsInOtherWorld = true;
        }
        else
        {
            FrontPanel1.SetActive(true);
            BackPanel1.SetActive(true);
            FrontPanel2.SetActive(false);
            BackPanel2.SetActive(false);
            playerIsInOtherWorld = false;
        }
    }
}
