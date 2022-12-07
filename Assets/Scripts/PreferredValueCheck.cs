using UnityEngine;
using TMPro;
 
public class PreferredValueCheck : MonoBehaviour
{
    public TextMeshProUGUI TextComponentUI;
 
    private string m_Label_01 = "A few lines of text for testing preferred values.";
    
    private void OnValidate()
    {
        Vector2 preferredValues = TextComponentUI.GetPreferredValues(m_Label_01, 155, 0);
        Debug.Log(preferredValues.ToString("f2"));
 
        TextComponentUI.text = m_Label_01;
        Debug.Log("Width: " + TextComponentUI.preferredWidth + "   Height: " + TextComponentUI.preferredHeight);
    }
}
