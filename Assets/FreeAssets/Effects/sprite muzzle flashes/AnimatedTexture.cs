using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour{
    [SerializeField] private MeshRenderer rendererMy = null;
    public                   float        fps        = 30.0f;
    public                   Texture2D[]  frames;

    private int frameIndex;

    private void Start(){
        NextFrame();
        // InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    public void NextFrame(){
        rendererMy.sharedMaterial.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }
}