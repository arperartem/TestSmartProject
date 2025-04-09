using System.Collections;
using UnityEngine.UI;

namespace CommonUI
{
    public class OnceVerticalLayout : VerticalLayoutGroup
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