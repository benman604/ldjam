using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AnimationEntry {
    public string animationName;
    public RuntimeAnimatorController controller;
    public float speed = 1f;
}

public class AnimationSwitcher : MonoBehaviour
{
    public string currentAnimation;
    public List<AnimationEntry> animationEntries;
    private Dictionary<string, RuntimeAnimatorController> animators = new Dictionary<string, RuntimeAnimatorController>();
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        foreach (AnimationEntry entry in animationEntries) {
            animators.Add(entry.animationName, entry.controller);
        }
    }

    public void SetAnimation(string animationName) {
        if (!animators.ContainsKey(animationName)) {
            Debug.LogError("Invalid animation index");
            return;
        }

        currentAnimation = animationName;
        animator.runtimeAnimatorController = animators[currentAnimation];
        animator.speed = animationEntries.Find(entry => entry.animationName == currentAnimation).speed;
    }
}
