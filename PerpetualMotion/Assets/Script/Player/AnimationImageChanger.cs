using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationImageChanger : MonoBehaviour
{
    [SerializeField] Sprite[] _sprits;
    int _index = 0;
    [SerializeField] float _count = 0.25f;
    float _counter = 0;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void SpriteChange()
    {
        sr.sprite = _sprits[_index];
        _index = (_index + 1) % _sprits.Length;
    }
    void Update()
    {
        if (_counter >= _count)
        {
            SpriteChange();
            _counter = 0;
        }
        else
        {
            _counter += Time.deltaTime;
        }
    }
}
