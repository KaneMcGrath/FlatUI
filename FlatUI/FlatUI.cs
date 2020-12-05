using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlatUI
{

	public class ColorScheme
	{

		public ColorScheme()
		{
			this.windowInsideColor = new Color(0.3f, 0.3f, 0.3f);
			this.windowOutsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideColor = new Color(0.5f, 0.5f, 0.5f);
			this.outsideColor = new Color(0.2f, 0.2f, 0.2f);
		}


		public ColorScheme(Color wIn, Color wOut, Color cIn, Color cOut)
		{
			this.windowInsideColor = wIn;
			this.windowOutsideColor = wOut;
			this.insideColor = cIn;
			this.outsideColor = cOut;
		}


		public Color[] toColors()
		{
			return new Color[]
			{
				this.windowInsideColor,
				this.windowOutsideColor,
				this.insideColor,
				this.outsideColor
			};
		}

	
		public Color color(int i)
		{
			if (i == 0)
			{
				return this.windowInsideColor;
			}
			if (i == 1)
			{
				return this.windowOutsideColor;
			}
			if (i == 2)
			{
				return this.insideColor;
			}
			if (i == 3)
			{
				return this.outsideColor;
			}
			return this.windowInsideColor;
		}

		
		public Color insideColor;

	
		public Color outsideColor;

		
		public Color windowInsideColor;

		
		public Color windowOutsideColor;
	}

	public static class ColorTypeConverter
	{
		
		public static string ToRGBHex(Color c)
		{
			string result;
			try
			{
				result = string.Format("[{0:X2}{1:X2}{2:X2}]", ColorTypeConverter.ToByte(c.r), ColorTypeConverter.ToByte(c.g), ColorTypeConverter.ToByte(c.b));
			}
			catch (FormatException)
			{
				result = "yo shitBroke";
			}
			return result;
		}

		private static byte ToByte(float f)
		{
			f = Mathf.Clamp01(f);
			return (byte)(f * 255f);
		}
	}

	public class FlatUIButton
	{
		public void draw()
		{
			GUI.DrawTexture(this.rect, this.outsideTex);
			GUI.DrawTexture(new Rect(this.offset().x + (float)this.thickness, this.offset().y + (float)this.thickness, this.offset().width - (float)(this.thickness * 2), this.offset().height - (float)(this.thickness * 2)), this.insideTex);
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleCenter;
			bool flag = this.visible;
			if (GUI.Button(this.offset(), "-", guistyle))
			{
				this.state = !this.state;
			}
		}
		public bool getState()
		{
			return this.state;
		}
		public FlatUIButton()
		{
			this.buttonText = "";
			this.thickness = 2;
		}
		public FlatUIButton(Rect rect)
		{
			this.buttonText = "";
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public Rect offset()
		{
			return this.rect;
		}
		public void draw(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			if (this.onPressedAnimTimer > 0f)
			{
				GUI.DrawTexture(position, this.outsideTex);
				GUI.DrawTexture(this.pressedAnim(position), this.insideTex);
				this.onPressedAnimTimer -= Time.deltaTime;
			}
			else
			{
				GUI.DrawTexture(position, this.outsideTex);
				GUI.DrawTexture(new Rect(position.x + (float)this.thickness, position.y + (float)this.thickness, position.width - (float)this.thickness * 2f, position.height - (float)this.thickness * 2f), this.insideTex);
				this.onPressedAnimTimer -= Time.deltaTime;
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleCenter;
			bool flag = this.visible;
			GUI.Label(position, this.buttonText, guistyle);
		}
		public void update(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			if (FlatUITools.isMouseInRect(position))
			{
				this.thickness = 5;
			}
			else
			{
				this.thickness = 2;
			}
			if (GUI.Button(position, "", new GUIStyle
			{
				border = new RectOffset(1, 1, 1, 1),
				alignment = TextAnchor.MiddleCenter,
				normal =
				{
					textColor = this.outsideColor
				}
			}))
			{
				this.state = true;
				this.onButtonPressed();
				return;
			}
			this.state = false;
		}
		public void setColors(Color inside, Color outside)
		{
			this.insideColor = inside;
			this.outsideColor = outside;
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
		}
		public FlatUIButton(Rect rect, string buttonText)
		{
			this.buttonText = "";
			this.thickness = 2;
			this.buttonText = buttonText;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		private void onButtonPressed()
		{
			this.onPressedAnimTimer = this.animationTime;
		}
		private Rect pressedAnim(Rect position)
		{
			float num = 1f - this.onPressedAnimTimer / this.animationTime;
			float num2 = position.x + (float)this.thickness;
			float num3 = position.y + (float)this.thickness;
			float num4 = position.width - (float)this.thickness * 2f;
			float num5 = position.height - (float)this.thickness * 2f;
			return new Rect(num2 + num4 / 2f - num4 / 2f * num, num3 + num5 / 2f - num5 / 2f * num, num4 * num, num5 * num);
		}
		public Color insideColor;
		public Color outsideColor;
		public int thickness;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		public bool state;
		public string buttonText;
		private bool onPressed;
		private float onPressedAnimTimer;
		public float animationTime = 0.1f;
	}

	public class FlatUICheckBox
	{
		public bool getState()
		{
			return this.state;
		}
		public FlatUICheckBox()
		{
			this.thickness = 2;
		}
		public FlatUICheckBox(Rect rect)
		{
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.IsC = this.insideTex;
			this.OsC = this.outsideTex;
			this.visible = true;
		}
		public Rect offset()
		{
			return this.rect;
		}
		public void draw(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			Rect position2 = new Rect(position.x + (position.width - 30f), position.y, 30f, 30f);
			if (this.useBackPlate)
			{
				GUI.DrawTexture(position, this.IsC);
			}
			GUI.DrawTexture(position2, this.outsideTex);
			GUI.DrawTexture(new Rect(position2.x + (float)this.thickness, position2.y + (float)this.thickness, position2.width - (float)(this.thickness * 2), position2.height - (float)(this.thickness * 2)), this.insideTex);
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleLeft;
			bool flag = this.visible;
			GUI.Label(new Rect(position.x + 5f, position.y, position.width - 30f, position.height), this.label, guistyle);
		}
		public void update(Rect Parent)
		{
			Rect rect = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			Rect position = new Rect(rect.x + (rect.width - 30f), rect.y, 30f, 30f);
			if (FlatUITools.isMouseInRect(rect))
			{
				this.thickness = 4;
			}
			else
			{
				this.thickness = 2;
			}
			if (GUI.Button(position, "", new GUIStyle
			{
				border = new RectOffset(1, 1, 1, 1),
				alignment = TextAnchor.MiddleCenter,
				normal =
				{
					textColor = this.outsideColor
				}
			}))
			{
				if (this.state)
				{
					this.setColors(this.insideColor, this.outsideColor, true);
					this.state = false;
					return;
				}
				this.setColors(this.outsideColor, this.insideColor, true);
				this.state = true;
			}
		}
		public void setColors(Color inside, Color outside)
		{
			this.insideColor = inside;
			this.outsideColor = outside;
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
		}
		public FlatUICheckBox(Rect rect, string label)
		{
			this.thickness = 2;
			this.label = label;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.IsC = this.insideTex;
			this.OsC = this.outsideTex;
			this.visible = true;
		}
		public void setColors(Color inside, Color outside, bool texOnly)
		{
			if (!texOnly)
			{
				this.insideColor = inside;
				this.outsideColor = outside;
			}
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				inside
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				outside
			});
			this.outsideTex.Apply();
		}
		public Color insideColor;
		public Color outsideColor;
		public int thickness;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		public bool state;
		public string label = "";
		public bool useBackPlate;
		public Texture2D IsC;
		public Texture2D OsC;
	}

	public class FlatUIColorChooser
	{
		public Color getColor()
		{
			float r = this.red.value();
			float g = this.green.value();
			float b = this.blue.value();
			return new Color(r, g, b);
		}
		public void Kill()
		{
			FlatUIManager.remove(this);
		}
		public FlatUIColorChooser(Rect rect, string title)
		{
			this.window = new FlatUIWindow(rect, title);
			this.window.canExit = true;
			this.preview = new FlatUIButton(this.window.indexToRect(0, 3));
			this.red = new FlatUISlider(this.window.indexToRect(3));
			this.red.setColors(this.red.insideColor, this.red.outsideColor, new Color(1f, 0f, 0f), this.red.sliderOutsideColor);
			this.green = new FlatUISlider(this.window.indexToRect(4));
			this.green.setColors(this.green.insideColor, this.green.outsideColor, new Color(0f, 1f, 0f), this.green.sliderOutsideColor);
			this.blue = new FlatUISlider(this.window.indexToRect(5));
			this.blue.setColors(this.blue.insideColor, this.blue.outsideColor, new Color(0f, 0f, 1f), this.blue.sliderOutsideColor);
			this.window.contents.Add(this.preview);
			this.window.contents.Add(this.red);
			this.window.contents.Add(this.green);
			this.window.contents.Add(this.blue);
		}
		public void Update2()
		{
			this.preview.setColors(this.getColor(), this.preview.outsideColor);
		}
		public FlatUIWindow GetWindow()
		{
			return this.window;
		}
		public float r()
		{
			return this.red.value();
		}
		public float g()
		{
			return this.green.value();
		}
		public float b()
		{
			return this.blue.value();
		}
		public void setColor(Color color)
		{
			this.red.setValue(color.r);
			this.green.setValue(color.g);
			this.blue.setValue(color.b);
		}
		private FlatUIButton preview;
		private FlatUISlider red;
		private FlatUISlider green;
		private FlatUISlider blue;
		public FlatUIWindow window;
		private FlatUIButton hex;
	}

	public class FlatUIColorSelector
	{
		public Color getColor()
		{
			float r = this.red.value();
			float g = this.green.value();
			float b = this.blue.value();
			return new Color(r, g, b);
		}
		public void Kill()
		{
			FlatUIManager.remove(this);
		}
		public FlatUIColorSelector(Rect rect, FlatUIWindow parent)
		{
			this.canChooseWindow = true;
			this.rect = rect;
			this.parent = parent;
			this.preview = new FlatUIButton(this.indexToRect(rect, 4, 3));
			this.red = new FlatUISlider(this.indexToRect(rect, 4, 0));
			this.red.setColors(this.red.insideColor, this.red.outsideColor, new Color(1f, 0f, 0f), this.red.sliderOutsideColor);
			this.green = new FlatUISlider(this.indexToRect(rect, 4, 1));
			this.green.setColors(this.green.insideColor, this.green.outsideColor, new Color(0f, 1f, 0f), this.green.sliderOutsideColor);
			this.blue = new FlatUISlider(this.indexToRect(rect, 4, 2));
			this.blue.setColors(this.blue.insideColor, this.blue.outsideColor, new Color(0f, 0f, 1f), this.blue.sliderOutsideColor);
			this.parent.contents.Add(this.preview);
			this.parent.contents.Add(this.red);
			this.parent.contents.Add(this.green);
			this.parent.contents.Add(this.blue);
			this.colorWindow = new FlatUIColorChooser(new Rect(parent.rect.x + parent.rect.width, parent.rect.y, 300f, 214f), "Select a color");
			this.colorWindow.window.canExit = true;
		}
		public void Update2()
		{
			this.preview.setColors(this.getColor(), this.preview.outsideColor);
			if (this.canChooseWindow && this.preview.state)
			{
				if (!FlatUIManager.hasObject(this.colorWindow.GetWindow()))
				{
					this.colorWindow.setColor(this.getColor());
					this.colorWindow.GetWindow().moveTo(this.parent.rect.x + this.parent.rect.width, this.parent.rect.y + this.rect.y);
					FlatUIManager.current.Add(this.colorWindow.GetWindow());
				}
				else
				{
					FlatUIManager.makeTop(this.colorWindow.GetWindow());
				}
			}
			if (FlatUIManager.hasObject(this.colorWindow.GetWindow()))
			{
				this.setColor(this.colorWindow.getColor());
				this.colorWindow.Update2();
			}
		}
		public FlatUIWindow GetWindow()
		{
			return this.parent;
		}
		private Rect indexToRect(Rect rect, int divisions, int j)
		{
			float width = rect.width;
			if (divisions < 1)
			{
				divisions = 1;
			}
			return new Rect((float)this.parent.thickness + rect.x + width / (float)divisions * (float)j, rect.y, width / (float)divisions, 30f);
		}
		public void setColor(Color color)
		{
			this.red.setValue(color.r);
			this.green.setValue(color.g);
			this.blue.setValue(color.b);
		}
		public bool isChanging()
		{
			return this.red.isChanging() || this.green.isChanging() || this.blue.isChanging() || FlatUIManager.hasObject(this.colorWindow.GetWindow());
		}
		public FlatUIColorSelector(Rect rect, FlatUIWindow parent, Color defaultColor)
		{
			this.canChooseWindow = true;
			this.rect = rect;
			this.parent = parent;
			this.preview = new FlatUIButton(this.indexToRect(rect, 4, 3));
			this.red = new FlatUISlider(this.indexToRect(rect, 4, 0));
			this.red.setColors(this.red.insideColor, this.red.outsideColor, new Color(1f, 0f, 0f), this.red.sliderOutsideColor);
			this.green = new FlatUISlider(this.indexToRect(rect, 4, 1));
			this.green.setColors(this.green.insideColor, this.green.outsideColor, new Color(0f, 1f, 0f), this.green.sliderOutsideColor);
			this.blue = new FlatUISlider(this.indexToRect(rect, 4, 2));
			this.blue.setColors(this.blue.insideColor, this.blue.outsideColor, new Color(0f, 0f, 1f), this.blue.sliderOutsideColor);
			this.setColor(defaultColor);
			this.parent.contents.Add(this.preview);
			this.parent.contents.Add(this.red);
			this.parent.contents.Add(this.green);
			this.parent.contents.Add(this.blue);
			this.colorWindow = new FlatUIColorChooser(new Rect(parent.rect.x + parent.rect.width, parent.rect.y, 300f, 214f), "Select a color");
			this.colorWindow.window.canExit = true;
		}
		private FlatUIButton preview;
		private FlatUISlider red;
		private FlatUISlider green;
		private FlatUISlider blue;
		public FlatUIWindow parent;
		public bool canChooseWindow;
		private FlatUIColorChooser colorWindow;
		private Rect rect;
	}

	public class FlatUIInput
	{
		public FlatUIInput()
		{
			this.thickness = 2;
		}
		public FlatUIInput(Rect rect)
		{
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.5f, 0.5f, 0.5f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public Rect offset()
		{
			return this.rect;
		}
		public void draw(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			GUI.DrawTexture(position, this.outsideTex);
			GUI.DrawTexture(new Rect(position.x + (float)this.thickness, position.y + (float)this.thickness, position.width - (float)(this.thickness * 2), position.height - (float)(this.thickness * 2)), this.insideTex);
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(position.x + 5f, position.y, position.width, position.height), this.text, guistyle);
		}
		public void update(Rect Parent)
		{
			Rect rect = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			new Rect(rect.x + (rect.width - 30f), rect.y, 30f, 30f);
			if (FlatUITools.isMouseInRect(rect))
			{
				this.thickness = 4;
			}
			else
			{
				this.thickness = 2;
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleLeft;
			guistyle.normal.textColor = new Color32(0, 0, 0, 0);
			this.text = GUI.TextField(new Rect(rect.x + 5f, rect.y, rect.width, rect.height), this.text, guistyle);
		}
		public void setColors(Color inside, Color outside)
		{
			this.insideColor = inside;
			this.outsideColor = outside;
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
		}
		public FlatUIInput(Rect rect, string text)
		{
			this.thickness = 2;
			this.text = text;
			this.rect = rect;
			this.insideColor = new Color(0.5f, 0.5f, 0.5f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public void setColors(Color inside, Color outside, bool texOnly)
		{
			if (!texOnly)
			{
				this.insideColor = inside;
				this.outsideColor = outside;
			}
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				inside
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				outside
			});
			this.outsideTex.Apply();
		}
		public Color insideColor;
		public Color outsideColor;
		public int thickness;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		public bool state;
		public string text = "";
	}

	public class FlatUILabel
	{
		public FlatUILabel()
		{
			this.guistyle.border = new RectOffset(1, 1, 1, 1);
			this.guistyle.alignment = TextAnchor.MiddleCenter;
			this.alignSide = 5;
			this.textAnchor();
			this.animationTime = 0.1f;
			this.labelText = "";
			this.thickness = 2;
		}
		public FlatUILabel(Rect rect)
		{
			this.guistyle.border = new RectOffset(1, 1, 1, 1);
			this.animationTime = 0.1f;
			this.alignSide = 5;
			this.textAnchor();
			this.labelText = "";
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public Rect offset()
		{
			return this.rect;
		}
		public void draw(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			if (this.useBackPlate)
			{
				GUI.DrawTexture(position, this.insideTex);
			}
			GUI.DrawTexture(this.alignCalc(position, true), this.outsideTex);
			GUI.DrawTexture(this.alignCalc(position, false), this.insideTex);
			bool flag = this.visible;
			GUI.Label(this.alignCalc(position, false), this.labelText, this.guistyle);
		}
		public void update(Rect Parent)
		{
		}
		public void setColors(Color inside, Color outside)
		{
			this.insideColor = inside;
			this.outsideColor = outside;
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
		}
		public FlatUILabel(Rect rect, string labelText)
		{
			this.guistyle.border = new RectOffset(1, 1, 1, 1);
			this.animationTime = 0.1f;
			this.alignSide = 5;
			this.textAnchor();
			this.labelText = labelText;
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public FlatUILabel(Rect rect, string labelText, int alignSide)
		{
			this.guistyle.border = new RectOffset(1, 1, 1, 1);
			this.animationTime = 0.1f;
			this.alignSide = alignSide;
			this.textAnchor();
			this.labelText = labelText;
			this.thickness = 2;
			this.rect = rect;
			this.insideColor = new Color(0.3f, 0.3f, 0.3f);
			this.outsideColor = new Color(0.1f, 0.1f, 0.1f);
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.visible = true;
		}
		public Rect alignCalc(Rect position, bool Outstide)
		{
			Rect result = position;
			float left = result.x;
			float top = result.y;
			float width = result.width;
			float height = result.height;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int num5 = this.alignSide;
			float num6 = 10f;
			float num7 = (float)this.labelText.Length * 8f;
			float num8 = 20f;
			if (num5 == 3 || num5 == 6 || num5 == 9)
			{
				left = result.x + result.width - num6 - num7;
			}
			else if (num5 == 8 || num5 == 5 || num5 == 2)
			{
				left = result.x + result.width / 2f - num7 / 2f;
			}
			else if (num5 == 1 || num5 == 7)
			{
				left = result.x + num6;
			}
			else if (num5 == 4)
			{
				left = result.x;
				num = 2f;
				num3 = 2f;
			}
			if (num5 == 1 || num5 == 4 || num5 == 7)
			{
				width = num7 + num6;
			}
			else if (num5 == 8 || num5 == 5 || num5 == 2)
			{
				width = num7;
			}
			else if (num5 == 3 || num5 == 9)
			{
				width = num7;
			}
			else if (num5 == 6)
			{
				width = num7 + num6;
				num3 = 2f;
			}
			if (num5 == 7 || num5 == 8 || num5 == 9)
			{
				top = result.y;
				num2 = 2f;
				num4 = 2f;
			}
			else if (num5 == 4 || num5 == 5 || num5 == 6)
			{
				top = result.y + result.height / 2f - num8 / 2f;
			}
			else if (num5 == 1 || num5 == 2 || num5 == 3)
			{
				top = result.y + result.height - num8;
			}
			if (num5 == 7 || num5 == 8 || num5 == 9)
			{
				height = num8;
			}
			else if (num5 == 4 || num5 == 5 || num5 == 6)
			{
				height = num8;
			}
			else if (num5 == 1 || num5 == 2 || num5 == 3)
			{
				height = num8;
				num4 = 2f;
			}
			result = new Rect(left, top, width, height);
			if (Outstide)
			{
				return result;
			}
			return new Rect(result.x + (float)this.thickness - num, result.y + (float)this.thickness - num2, result.width - (float)this.thickness * 2f + num3, result.height - (float)this.thickness * 2f + num4);
		}
		private void textAnchor()
		{
			if (this.alignSide == 1)
			{
				this.guistyle.alignment = TextAnchor.LowerLeft;
				return;
			}
			if (this.alignSide == 2)
			{
				this.guistyle.alignment = TextAnchor.LowerCenter;
				return;
			}
			if (this.alignSide == 3)
			{
				this.guistyle.alignment = TextAnchor.LowerRight;
				return;
			}
			if (this.alignSide == 4)
			{
				this.guistyle.alignment = TextAnchor.MiddleRight;
				return;
			}
			if (this.alignSide == 5)
			{
				this.guistyle.alignment = TextAnchor.MiddleCenter;
				return;
			}
			if (this.alignSide == 6)
			{
				this.guistyle.alignment = TextAnchor.MiddleRight;
				return;
			}
			if (this.alignSide == 7)
			{
				this.guistyle.alignment = TextAnchor.UpperLeft;
				return;
			}
			if (this.alignSide == 8)
			{
				this.guistyle.alignment = TextAnchor.UpperCenter;
				return;
			}
			if (this.alignSide == 9)
			{
				this.guistyle.alignment = TextAnchor.UpperRight;
				return;
			}
			this.guistyle.alignment = TextAnchor.MiddleCenter;
		}
		public Color insideColor;
		public Color outsideColor;
		public int thickness;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		public string labelText;
		private float onPressedAnimTimer;
		public float animationTime;
		public int alignSide;
		public GUIStyle guistyle = new GUIStyle();
		public bool useBackPlate;
	}

	public class FlatUIManager
	{
		public static void draw()
		{
			List<object> list = FlatUIManager.current;
			if (list.Count != 0)
			{
				FlatUIManager.hasMouse = null;
				for (int i = 0; i <= list.Count - 1; i++)
				{
					if (list[i] is FlatUIWindow && !((FlatUIWindow)list[i]).hide)
					{
						((FlatUIWindow)list[i]).draw();
					}
				}
				for (int j = list.Count - 1; j >= 0; j--)
				{
					if (list[j] is FlatUIWindow)
					{
						((FlatUIWindow)list[j]).update();
					}
				}
			}
		}
		public static void makeTop(object window)
		{
			for (int i = 0; i <= FlatUIManager.current.Count - 1; i++)
			{
				if (FlatUIManager.current[i] == window)
				{
					object item = FlatUIManager.current[i];
					FlatUIManager.current.RemoveAt(i);
					FlatUIManager.current.Add(item);
				}
			}
		}
		public static void remove(object window)
		{
			for (int i = 0; i <= FlatUIManager.current.Count - 1; i++)
			{
				if (FlatUIManager.current[i] == window)
				{
					object obj = FlatUIManager.current[i];
					FlatUIManager.current.RemoveAt(i);
				}
			}
		}
		public static bool hasObject(object window)
		{
			for (int i = 0; i <= FlatUIManager.current.Count - 1; i++)
			{
				if (FlatUIManager.current[i] == window)
				{
					return true;
				}
			}
			return false;
		}
		static FlatUIManager()
		{
			FlatUIManager.hasMouse = null;
		}
		public static List<object> current = new List<object>();
		public static object hasMouse;
		public static List<object> Menu = new List<object>();
	}

	public class FlatUIMinMaxSlider
	{
		public void Kill()
		{
			FlatUIManager.remove(this);
		}
		public FlatUIMinMaxSlider(Rect rect, FlatUIWindow parent)
		{
			this.rect = rect;
			this.parent = parent;
			this.labels = true;
			this.add(new FlatUILabel(this.indexToRect(rect, 0), "Min", 6));
			this.minInput = new FlatUIInput(this.indexToRect(rect, 1), "60");
			this.add(this.minInput);
			this.flatUISlider = new FlatUISlider(this.indexToRect(rect, 2));
			this.add(this.flatUISlider);
			this.maxInput = new FlatUIInput(this.indexToRect(rect, 3), "60");
			this.add(this.maxInput);
			this.add(new FlatUILabel(this.indexToRect(rect, 4), "Max", 4));
		}
		private void add(object o)
		{
			this.parent.contents.Add(o);
		}
		public void Update2()
		{
			if (this.parent.hide)
			{
				return;
			}
			float min;
			if (float.TryParse(this.minInput.text, out min))
			{
				this.flatUISlider.min = min;
			}
			float max;
			if (float.TryParse(this.maxInput.text, out max))
			{
				this.flatUISlider.max = max;
			}
		}
		public FlatUIWindow GetWindow()
		{
			return this.parent;
		}
		private Rect indexToRect(Rect rect, int part)
		{
			float width = rect.width;
			if (part == 0)
			{
				return new Rect((float)this.parent.thickness + rect.x, rect.y, this.sideWidth, 30f);
			}
			if (part == 1)
			{
				if (!this.labels)
				{
					return new Rect((float)this.parent.thickness + rect.x + this.sideWidth, rect.y, width - this.sideWidth * 2f, 30f);
				}
				return new Rect((float)this.parent.thickness + rect.x + this.sideWidth, rect.y, this.sideWidth, 30f);
			}
			else if (part == 2)
			{
				if (!this.labels)
				{
					return new Rect((float)this.parent.thickness + rect.x + width - this.sideWidth, rect.y, this.sideWidth, 30f);
				}
				return new Rect((float)this.parent.thickness + rect.x + this.sideWidth * 2f, rect.y, width - this.sideWidth * 4f, 30f);
			}
			else
			{
				if (part == 3)
				{
					return new Rect((float)this.parent.thickness + rect.x + width - this.sideWidth * 2f, rect.y, this.sideWidth, 30f);
				}
				return new Rect((float)this.parent.thickness + rect.x + width - this.sideWidth, rect.y, this.sideWidth, 30f);
			}
		}
		public FlatUIMinMaxSlider(Rect rect, FlatUIWindow parent, bool labels, float min, float max)
		{
			this.parent = parent;
			this.rect = rect;
			this.labels = labels;
			this.add(new FlatUIButton(rect, "test"));
			if (labels)
			{
				this.add(new FlatUILabel(this.indexToRect(rect, 0), "Min", 6));
				this.minInput = new FlatUIInput(this.indexToRect(rect, 1), min.ToString());
				this.add(this.minInput);
				this.flatUISlider = new FlatUISlider(this.indexToRect(rect, 2));
				this.add(this.flatUISlider);
				this.maxInput = new FlatUIInput(this.indexToRect(rect, 3), max.ToString());
				this.add(this.maxInput);
				this.add(new FlatUILabel(this.indexToRect(rect, 4), "Max", 4));
				return;
			}
			this.minInput = new FlatUIInput(this.indexToRect(rect, 0), min.ToString());
			this.add(this.minInput);
			this.flatUISlider = new FlatUISlider(this.indexToRect(rect, 1));
			this.add(this.flatUISlider);
			this.maxInput = new FlatUIInput(this.indexToRect(rect, 2), max.ToString());
			this.add(this.maxInput);
		}
		public bool isChanging()
		{
			return this.flatUISlider.isChanging();
		}
		public FlatUIWindow parent;
		public bool canChooseMin;
		public bool canChooseMax;
		public FlatUISlider flatUISlider;
		private Rect rect;
		public float sideWidth = 35f;
		public FlatUIInput minInput;
		public FlatUIInput maxInput;
		private bool labels;
	}

	public class FlatUISlider
	{
		public FlatUISlider()
		{
			this.thickness = 2;
		}
		public FlatUISlider(Rect rect)
		{
			this.thickness = 2;
			this.rect = rect;
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.sliderInsideTex = new Texture2D(1, 1);
			this.sliderOutsideTex = new Texture2D(1, 1);
			this.setColors(new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f), new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f));
			this.visible = true;
		}
		public Rect offset(Rect position)
		{
			return new Rect(position.x + (float)this.thickness, position.y + (float)this.thickness, position.width - (float)this.thickness * 2f, position.height - (float)this.thickness * 2f);
		}
		public void draw(Rect Parent)
		{
			Rect position = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			Rect position2 = new Rect(position.x + this.sliderX, position.y, 10f, 30f);
			GUI.DrawTexture(position, this.outsideTex);
			GUI.DrawTexture(this.offset(position), this.insideTex);
			GUI.DrawTexture(position2, this.sliderOutsideTex);
			GUI.DrawTexture(new Rect(position2.x + (float)this.sliderThickness, position2.y + (float)this.sliderThickness, position2.width - (float)this.sliderThickness * 2f, position2.height - (float)this.sliderThickness * 2f), this.sliderInsideTex);
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleCenter;
			bool flag = this.visible;
			if (this.dragging)
			{
				Rect position3 = new Rect(position.x + this.sliderX, position.y - 20f, 75f, 20f);
				Rect position4 = new Rect(position3.x + 2f, position3.y + 2f, position3.width - 4f, position3.height - 4f);
				GUI.DrawTexture(position3, this.outsideTex);
				GUI.DrawTexture(position4, this.insideTex);
				GUI.Label(new Rect(position3.x + 2f, position3.y, 100f, 30f), this.value().ToString());
			}
		}
		public void update(Rect Parent)
		{
			Rect rect = new Rect(Parent.x + this.rect.x, Parent.y + this.rect.y, this.rect.width, this.rect.height);
			Rect rect2 = new Rect(rect.x + this.sliderX, rect.y, 10f, 30f);
			if (FlatUITools.isMouseInRect(rect))
			{
				this.thickness = 4;
			}
			else
			{
				this.thickness = 2;
			}
			if (FlatUITools.isMouseInRect(rect2))
			{
				this.sliderThickness = 4;
			}
			else
			{
				this.sliderThickness = 2;
			}
			if (Input.GetMouseButtonDown(0) && FlatUITools.isMouseInRect(rect2) && !FlatUIWindow.isDragging)
			{
				this.dragging = true;
				FlatUIWindow.isDragging = true;
				this.mouseDragPointx = Input.mousePosition.x - rect.x - this.sliderX;
				this.holdControl = true;
			}
			else if (Input.GetMouseButtonDown(0) && !FlatUITools.isMouseInRect(rect2) && FlatUITools.isMouseInRect(rect))
			{
				float num = Input.mousePosition.x - rect.x;
				if (num < 0f)
				{
					num = 0f;
				}
				if (num > rect.width - 10f)
				{
					num = rect.width - 10f;
				}
				this.sliderX = num;
			}
			if (Input.GetMouseButtonUp(0))
			{
				FlatUIWindow.isDragging = false;
				this.dragging = false;
				this.holdControl = false;
			}
			if (this.dragging)
			{
				float num2 = Input.mousePosition.x - this.mouseDragPointx - rect.x;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				if (num2 > rect.width - 10f)
				{
					num2 = rect.width - 10f;
				}
				this.sliderX = num2;
			}
		}
		public void setColors(Color inside, Color outside)
		{
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
		}
		public float value()
		{
			float num = this.rect.width - 10f;
			float num2 = this.max - this.min;
			float num3 = this.sliderX / num;
			return this.min + num3 * num2;
		}
		public void setColors(Color inside, Color outside, Color sliderInsideColor, Color sliderOutsideColor)
		{
			this.insideColor = inside;
			this.outsideColor = outside;
			this.sliderInsideColor = sliderInsideColor;
			this.sliderOutsideColor = sliderOutsideColor;
			this.insideTex.SetPixels(new Color[]
			{
				this.insideColor
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.outsideColor
			});
			this.outsideTex.Apply();
			this.sliderInsideTex.SetPixels(new Color[]
			{
				this.sliderInsideColor
			});
			this.sliderInsideTex.Apply();
			this.sliderOutsideTex.SetPixels(new Color[]
			{
				this.sliderOutsideColor
			});
			this.sliderOutsideTex.Apply();
		}
		public void setColors(Color inside)
		{
			this.insideTex.SetPixels(new Color[]
			{
				inside
			});
			this.insideTex.Apply();
		}
		public void setValue(float newValue)
		{
			float num = this.rect.width - 10f;
			float num2 = this.max - this.min;
			float num3 = newValue * num * num2;
			this.sliderX = num3;
		}
		public bool isChanging()
		{
			return this.dragging;
		}
		public FlatUISlider(Rect rect, float defaultValue)
		{
			this.defaultValue = defaultValue;
			this.thickness = 2;
			this.rect = rect;
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.sliderInsideTex = new Texture2D(1, 1);
			this.sliderOutsideTex = new Texture2D(1, 1);
			this.setColors(new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f), new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f));
			this.visible = true;
		}
		public Color insideColor;
		private float mouseDragPointx;
		private float mouseDragPointy;
		private bool dragging;
		public Color outsideColor;
		private int thickness;
		public float min;
		public float max = 1f;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		public float sliderX;
		public int sliderThickness;
		public Color sliderInsideColor;
		public Color sliderOutsideColor;
		public Texture2D sliderInsideTex;
		public Texture2D sliderOutsideTex;
		public bool holdControl;
		private float defaultValue;
	}

	public static class FlatUITools
	{
		public static bool isMouseInRect(Rect rect)
		{
			return Input.mousePosition.x > rect.x && Input.mousePosition.x < rect.x + rect.width && (float)Screen.height - Input.mousePosition.y > rect.y && (float)Screen.height - Input.mousePosition.y < rect.y + rect.height;
		}
	}

	public class FlatUIWindow
	{
		public void draw()
		{
			if (this.hide)
			{
				return;
			}
			Rect position = new Rect(this.rect.x, this.rect.y, this.rect.width, 30f);
			Rect position2 = new Rect(this.rect.x, this.rect.y + 30f - (float)this.thickness, this.rect.width, this.rect.height - 30f);
			Rect position3 = new Rect(position.x + position.width - 30f, position.y, 30f, 30f);
			Rect position4 = new Rect(this.rect.x, this.rect.y, this.rect.width, 30f);
			GUI.DrawTexture(position, this.outsideTex);
			GUI.DrawTexture(new Rect(position.x + (float)this.thickness, position.y + (float)this.thickness, position.width - (float)(this.thickness * 2), position.height - (float)(this.thickness * 2)), this.insideTex);
			GUI.DrawTexture(position3, this.outsideTex);
			GUI.DrawTexture(new Rect(position3.x + (float)this.thickness, position3.y + (float)this.thickness, position3.width - (float)(this.thickness * 2), position3.height - (float)(this.thickness * 2)), this.insideTex);
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleCenter;
			guistyle.normal.textColor = this.colorScheme.color(1);
			GUI.Label(position4, this.title, guistyle);
			if (this.canExit)
			{
				Rect position5 = new Rect(position.x + position.width - 60f, position.y, 30f, 30f);
				GUI.DrawTexture(position5, this.outsideTex);
				GUI.DrawTexture(new Rect(position5.x + (float)this.thickness, position5.y + (float)this.thickness, position5.width - (float)(this.thickness * 2), position5.height - (float)(this.thickness * 2)), this.insideTex);
				GUI.Label(new Rect(position.x + position.width - 30f, position.y, 30f, 30f), "x", guistyle);
				GUI.Label(new Rect(position.x + position.width - 60f, position.y, 30f, 30f), "-", guistyle);
			}
			else
			{
				GUI.Label(new Rect(position.x + position.width - 30f, position.y, 30f, 30f), "-", guistyle);
			}
			if (this.visible)
			{
				GUI.DrawTexture(position2, this.outsideTex);
				GUI.DrawTexture(new Rect(position2.x + (float)this.thickness, position2.y + (float)this.thickness, position2.width - (float)(this.thickness * 2), position2.height - (float)(this.thickness * 2)), this.insideTex);
			}
			if (this.visible)
			{
				if (FlatUITools.isMouseInRect(this.rect))
				{
					FlatUIManager.hasMouse = this;
				}
			}
			else if (FlatUITools.isMouseInRect(new Rect(this.rect.x, this.rect.y, this.rect.width, 30f)))
			{
				FlatUIManager.hasMouse = this;
			}
			if (this.contents.Count != 0)
			{
				this.windowContentDraw();
			}
		}
		public FlatUIWindow()
		{
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
		}
		public FlatUIWindow(Rect rect)
		{
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.rect = rect;
			this.setColors(new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f));
			this.visible = true;
			this.title = "";
		}
		public FlatUIWindow(Rect rect, string title)
		{
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.rect = rect;
			this.setColors(new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f));
			this.visible = true;
			this.title = title;
		}
		public void update()
		{
			if (this.hide)
			{
				return;
			}
			this.hasMouse = false;
			if (FlatUIManager.hasMouse == this || this.holdControl)
			{
				this.hasMouse = true;
			}
			Rect rect = new Rect(this.rect.x, this.rect.y, this.rect.width - 30f, 30f);
			bool flag = false;
			if (Input.GetMouseButtonDown(0) && FlatUITools.isMouseInRect(this.rect) && FlatUIManager.hasMouse == this)
			{
				flag = true;
			}
			if (Input.GetMouseButtonDown(0) && FlatUITools.isMouseInRect(rect) && !FlatUIWindow.isDragging && this.hasMouse)
			{
				this.dragging = true;
				FlatUIWindow.isDragging = true;
				this.mouseDragPointx = Input.mousePosition.x - this.rect.x;
				this.mouseDragPointy = (float)Screen.height - Input.mousePosition.y - this.rect.y;
			}
			if (Input.GetMouseButtonUp(0))
			{
				FlatUIWindow.isDragging = false;
				this.dragging = false;
			}
			if (this.dragging)
			{
				this.rect.x = Input.mousePosition.x - this.mouseDragPointx;
				this.rect.y = (float)Screen.height - Input.mousePosition.y - this.mouseDragPointy;
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.border = new RectOffset(1, 1, 1, 1);
			guistyle.alignment = TextAnchor.MiddleCenter;
			guistyle.normal.textColor = this.colorScheme.color(1);
			if (this.canExit)
			{
				if (GUI.Button(new Rect(rect.x + rect.width - 30f, rect.y, 30f, 30f), "", guistyle) && this.hasMouse)
				{
					this.visible = !this.visible;
				}
				if (GUI.Button(new Rect(rect.x + rect.width, rect.y, 30f, 30f), "", guistyle) && this.hasMouse)
				{
					FlatUIManager.remove(this);
				}
			}
			else if (GUI.Button(new Rect(rect.x + rect.width, rect.y, 30f, 30f), "", guistyle) && this.hasMouse)
			{
				this.visible = !this.visible;
			}
			if (this.contents.Count != 0 && this.hasMouse)
			{
				this.windowContentUpdate();
			}
			if (flag)
			{
				FlatUIManager.makeTop(this);
			}
			if (FlatUIWindow.useTitleDebug)
			{
				this.title = FlatUIWindow.isDragging.ToString() + ":" + this.hasMouse.ToString();
				if (FlatUIManager.hasMouse == this)
				{
					this.title += " :x";
				}
				if (FlatUIManager.current[FlatUIManager.current.Count - 1] == this)
				{
					this.title += " :T";
				}
				if (this.holdControl)
				{
					this.title += " :H";
				}
			}
		}
		public void setColors(Color inside, Color outside)
		{
			this.setColors(new ColorScheme(inside, outside, inside, outside));
		}
		public Rect indexToRect(int i)
		{
			return new Rect((float)this.thickness, (float)i * 30f, this.rect.width - (float)this.thickness * 2f, 30f);
		}
		public void windowContentDraw()
		{
			if (this.visible)
			{
				for (int i = 0; i <= this.contents.Count - 1; i++)
				{
					if (this.contents[i] is FlatUIButton)
					{
						((FlatUIButton)this.contents[i]).draw(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUICheckBox)
					{
						((FlatUICheckBox)this.contents[i]).draw(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUIInput)
					{
						((FlatUIInput)this.contents[i]).draw(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUISlider)
					{
						((FlatUISlider)this.contents[i]).draw(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUILabel)
					{
						((FlatUILabel)this.contents[i]).draw(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
				}
			}
		}
		public void windowContentUpdate()
		{
			if (this.visible)
			{
				this.holdControl = false;
				for (int i = this.contents.Count - 1; i >= 0; i--)
				{
					if (this.contents[i] is FlatUIButton)
					{
						((FlatUIButton)this.contents[i]).update(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUICheckBox)
					{
						((FlatUICheckBox)this.contents[i]).update(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUIInput)
					{
						((FlatUIInput)this.contents[i]).update(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
					}
					else if (this.contents[i] is FlatUISlider)
					{
						((FlatUISlider)this.contents[i]).update(new Rect(this.rect.x, this.rect.y + 30f, this.rect.width, 30f));
						bool flag = ((FlatUISlider)this.contents[i]).holdControl;
						if (flag)
						{
							this.holdControl = flag;
						}
					}
				}
			}
		}
		public Rect indexToRect(int i, int divisions, int j)
		{
			float num = this.rect.width - (float)this.thickness * 2f;
			if (divisions < 1)
			{
				divisions = 1;
			}
			return new Rect((float)this.thickness + num / (float)divisions * (float)j, (float)i * 30f, num / (float)divisions, 30f);
		}
		public Rect indexToRect(int i, int divisions, int j, int width)
		{
			float num = this.rect.width - (float)this.thickness * 2f;
			if (divisions < 1)
			{
				divisions = 1;
			}
			return new Rect((float)this.thickness + num / (float)divisions * (float)j, (float)i * 30f, num / (float)divisions * (float)width, 30f);
		}
		public Rect indexToRect(int i, int n)
		{
			return new Rect((float)this.thickness, (float)i * 30f, this.rect.width - (float)this.thickness * 2f, 30f * (float)n);
		}
		public void setColor(Color color, bool inside)
		{
			if (inside)
			{
				this.setColors(new ColorScheme(color, this.colorScheme.color(1), this.colorScheme.color(2), this.colorScheme.color(3)));
				return;
			}
			this.setColors(new ColorScheme(this.colorScheme.color(0), color, this.colorScheme.color(2), this.colorScheme.color(3)));
		}
		public void moveTo(float x, float y)
		{
			Rect rect = this.rect;
			this.rect = new Rect(x, y, rect.width, rect.height);
		}
		public FlatUIWindow(Rect rect, string title, ColorScheme scheme)
		{
			this.insideTex = new Texture2D(1, 1);
			this.outsideTex = new Texture2D(1, 1);
			this.rect = rect;
			this.setColors(new Color(0.3f, 0.3f, 0.3f), new Color(0.1f, 0.1f, 0.1f));
			this.visible = true;
			this.title = title;
		}
		public void setColors(ColorScheme scheme)
		{
			this.colorScheme = scheme;
			this.insideTex.SetPixels(new Color[]
			{
				this.colorScheme.color(0)
			});
			this.insideTex.Apply();
			this.outsideTex.SetPixels(new Color[]
			{
				this.colorScheme.color(1)
			});
			this.outsideTex.Apply();
			for (int i = this.contents.Count - 1; i >= 0; i--)
			{
				if (this.contents[i] is FlatUIButton)
				{
					((FlatUIButton)this.contents[i]).setColors(scheme.color(2), scheme.color(3));
				}
				else if (this.contents[i] is FlatUICheckBox)
				{
					((FlatUICheckBox)this.contents[i]).setColors(scheme.color(2), scheme.color(3));
				}
				else if (this.contents[i] is FlatUIInput)
				{
					((FlatUIInput)this.contents[i]).setColors(scheme.color(2), scheme.color(3));
				}
				else if (this.contents[i] is FlatUISlider)
				{
					((FlatUISlider)this.contents[i]).setColors(scheme.color(2), scheme.color(3));
				}
			}
		}
		public int thickness = 2;
		public Rect rect;
		public bool visible;
		public Texture2D insideTex;
		public Texture2D outsideTex;
		private bool dragging;
		private float mouseDragPointx;
		private float mouseDragPointy;
		public static bool isDragging;
		public string title;
		public List<object> contents = new List<object>();
		public bool hasMouse;
		public bool canExit;
		public bool holdControl;
		private static bool useTitleDebug;
		public bool hide;
		public ColorScheme colorScheme;
	}
}
