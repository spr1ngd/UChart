
namespace UChart
{
	public interface IPoint
	{
		void SetSize(float size);
		void SetTexture(UnityEngine.Texture2D texture);		
		void SetColor(UnityEngine.Color color);
		void SetColor(UnityEngine.Color32 color32);
	    void SetAlpha(float alpha);
	}
}