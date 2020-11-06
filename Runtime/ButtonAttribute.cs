using UnityEngine;

namespace NDY.Attribute
{
    public class ButtonAttribute : PropertyAttribute
    {
        public string[] Methods;

        public ButtonAttribute(params string[] methods)
        {
            Methods = methods;
        }
    }
}
