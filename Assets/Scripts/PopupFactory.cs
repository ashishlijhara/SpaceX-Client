using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupFactory : MonoBehaviour, IPopupFactory
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Appear()
    {
        gameObject.SetActive(true);
    }

    //! TODO: Fade In to Appear
    public virtual void FadeAppear()
    {

    }

    //! TODO: Fade Out before dispose
    public virtual void FadeDispose()
    {

    }

    public virtual void Dispose()
    {
        gameObject.SetActive(false);
    }

    public virtual void DisposeDestroy()
    {
        Destroy(gameObject);
    }
}
