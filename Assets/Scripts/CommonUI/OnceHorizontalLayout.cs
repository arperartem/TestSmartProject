using System.Collections;
using UnityEngine.UI;

namespace CommonUI
{
    public class OnceHorizontalLayout : HorizontalLayoutGroup
    {
        public void ReBuild()
        {
            enabled = true;
            StartCoroutine(InternalUpdate());
        }

        private IEnumerator InternalUpdate()
        {
            yield return null;
            enabled = false;
        }
    }
}