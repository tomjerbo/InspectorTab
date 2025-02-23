using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jerbo.Inspector {
    public class TabAttributeExample : MonoBehaviour {
        
        [Tab("Raw vals.", Tab.Color.White)] public float boyancy;
        [Tab("Raw vals.", Tab.Color.Yellow)] public int integer;
        [Tab("Raw vals.", Tab.Color.Green)] public Vector3 veccy;
        [Tab("Raw vals.", Tab.Color.Blue)] public Vector3 veccy2;

        
        [Tab("Refs", Tab.Color.Grey)] public UnityEvent dasfdsa;
        [Tab("Refs", Tab.Color.Red)] public UnityEvent[] tr;
        [Tab("Refs", Tab.Color.Purple)] public List<Vector3> game;
        [Tab("Refs", Tab.Color.Pink)] public AnimationCurve curveMiddle;
        public Animator anim;
        public Texture2D tex;

        [Space(24)] [Tab("Audio", new[] { 0.2f })]
        public AudioClip bonce;

        [Tab("Audio")] public AudioClip back;
        [Tab("Curve time")] public AnimationCurve brokenCurve;
        [Tab("ANIM time")] public Animator soloAnim;
    }
}