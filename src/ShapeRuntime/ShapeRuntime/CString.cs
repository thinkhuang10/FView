using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("3568C2EF-D0D3-4790-9E8B-06D07C8637BD")]
public class CString : CShape, ISupportHtml5
{
    public string str;

    public Font _font = new("新宋体", 19f);

    [ReadOnly(false)]
    [DisplayName("显示字符")]
    [DHMICtrlProperty]
    [Description("设定显示内容。")]
    public string DisplayStr
    {
        get
        {
            return str;
        }
        set
        {
            if (value != DisplayStr)
            {
                NeedRefreshShape = true;
            }
            str = value;
        }
    }

    [ReadOnly(false)]
    [Description("设定显示字体。")]
    [DisplayName("字体样式")]
    public Font StringFont
    {
        get
        {
            return _font;
        }
        set
        {
            if (value != StringFont)
            {
                NeedRefreshShape = true;
            }
            _font = value;
        }
    }

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    protected CString(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CString info");
        }
        CString obj = new();
        FieldInfo[] fields = typeof(CString).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CString))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "_font")
            {
                _font = (Font)enumerator.Value;
            }
            else if (enumerator.Name == "str")
            {
                str = (string)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_font", _font);
        info.AddValue("str", str);
    }

    public CString()
    {
        Color1 = Color.Black;
    }

    public CString(string _str)
    {
        str = _str;
        Color1 = Color.Black;
    }

    public override bool AddPoint(PointF NewPoint)
    {
        needRefreshShape = false;
        List<PointF> list = new()
        {
            new PointF(float.MaxValue, float.MaxValue),
            new PointF(float.MinValue, float.MinValue),
            default,
            default,
            default,
            default,
            default,
            default
        };
        for (int i = 0; i < ImportantPoints.Length - 8; i++)
        {
            list.Add(ImportantPoints[i + 8]);
        }
        list.Add(NewPoint);
        ImportantPoints = new PointF[list.Count];
        ImportantPoints = (PointF[])list.ToArray().Clone();
        for (int j = 0; j < ImportantPoints.Length - 8; j++)
        {
            ImportantPoints[0].X = ((ImportantPoints[0].X < ImportantPoints[j + 8].X) ? ImportantPoints[0].X : ImportantPoints[j + 8].X);
            ImportantPoints[0].Y = ((ImportantPoints[0].Y < ImportantPoints[j + 8].Y) ? ImportantPoints[0].Y : ImportantPoints[j + 8].Y);
            ImportantPoints[1].X = ((ImportantPoints[1].X > ImportantPoints[j + 8].X) ? ImportantPoints[1].X : ImportantPoints[j + 8].X);
            ImportantPoints[1].Y = ((ImportantPoints[1].Y > ImportantPoints[j + 8].Y) ? ImportantPoints[1].Y : ImportantPoints[j + 8].Y);
        }
        if (ImportantPoints.Length >= 10)
        {
            RotateAtPoint.X = ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f;
            RotateAtPoint.Y = ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f;
            ref PointF reference = ref ImportantPoints[2];
            reference = new PointF(ImportantPoints[1].X, ImportantPoints[0].Y);
            ref PointF reference2 = ref ImportantPoints[3];
            reference2 = new PointF(ImportantPoints[0].X, ImportantPoints[1].Y);
            ref PointF reference3 = ref ImportantPoints[4];
            reference3 = new PointF(RotateAtPoint.X, ImportantPoints[0].Y);
            ref PointF reference4 = ref ImportantPoints[5];
            reference4 = new PointF(RotateAtPoint.X, ImportantPoints[1].Y);
            ref PointF reference5 = ref ImportantPoints[6];
            reference5 = new PointF(ImportantPoints[0].X, RotateAtPoint.Y);
            ref PointF reference6 = ref ImportantPoints[7];
            reference6 = new PointF(ImportantPoints[1].X, RotateAtPoint.Y);
            return false;
        }
        return true;
    }

    public override CShape Copy()
    {
        CString cString = new();
        Type type = cString.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cString, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cString, fieldInfo.GetValue(this));
            }
        }
        cString.str = str;
        cString._font = _font;
        cString.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cString.ShapeName = ShapeName;
        new List<List<int>>();
        cString.ShapeID = ShapeID;
        cString.RotateAngel = RotateAngel;
        cString.FillAngel = FillAngel;
        cString.FillAngel = FillAngel;
        cString.RotateAtPoint = RotateAtPoint;
        cString.TranslateMatrix = TranslateMatrix.Clone();
        cString._Pen = (Pen)_Pen.Clone();
        cString._Brush = (Brush)_Brush.Clone();
        return cString;
    }

    public override bool MouseOnMe(PointF ThePoint)
    {
        bool result = false;
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 10)
        {
            return result;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X) - 2f, Math.Min(array[0].Y, array[1].Y) - 2f, Math.Abs(array[1].X - array[0].X) + 4f, Math.Abs(array[1].Y - array[0].Y) + 4f));
        graphicsPath.CloseFigure();
        graphicsPath.Transform(TranslateMatrix);
        bool flag = graphicsPath.IsVisible(ThePoint);
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[1].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[1].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[1].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        bool flag2 = graphicsPath.IsVisible(ThePoint);
        if (flag || flag2)
        {
            result = true;
        }
        return result;
    }

    public override List<GraphicsPath> HowToDraw()
    {
        List<GraphicsPath> list = new();
        if (swapgp != null && !needRefreshShape)
        {
            list.Add(swapgp);
            return list;
        }
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 10)
        {
            return list;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        FontFamily fontFamily = _font.FontFamily;
        int style = (int)_font.Style;
        float size = _font.Size;
        StringFormat genericDefault = StringFormat.GenericDefault;
        graphicsPath.AddString(str, fontFamily, style, size, new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)), genericDefault);
        graphicsPath.CloseFigure();
        graphicsPath.Transform(TranslateMatrix);
        if (swapgp != null)
        {
            swapgp.Dispose();
        }
        swapgp = graphicsPath;
        needRefreshShape = false;
        list.Add(graphicsPath);
        return list;
    }

    public override bool DrawSelect(Graphics g)
    {
        bool result = false;
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 10)
        {
            return result;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X) - 2f, Math.Min(array[0].Y, array[1].Y) - 2f, Math.Abs(array[1].X - array[0].X) + 4f, Math.Abs(array[1].Y - array[0].Y) + 4f));
        graphicsPath.Transform(TranslateMatrix);
        Pen pen = new(Color.White, 0f);
        g.DrawPath(pen, graphicsPath);
        pen = new Pen(Color.SkyBlue, 0f);
        float[] dashPattern = new float[2] { 3f, 4f };
        pen.DashPattern = dashPattern;
        g.DrawPath(pen, graphicsPath);
        graphicsPath.Reset();
        graphicsPath.AddEllipse(new RectangleF(array[0].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[0].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[1].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[1].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[1].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(RotateAtPoint.X - 3f, RotateAtPoint.Y - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        g.FillPath(Brushes.White, graphicsPath);
        g.DrawPath(Pens.Blue, graphicsPath);
        graphicsPath.Reset();
        graphicsPath.AddEllipse(new RectangleF(RotateAtPoint.X - 3f + (Height / 2f + 35f) * (float)Math.Cos((double)(RotateAngel - 90f) * Math.PI / 180.0), RotateAtPoint.Y - 3f + (Height / 2f + 35f) * (float)Math.Sin((double)(RotateAngel - 90f) * Math.PI / 180.0), 7f, 7f));
        g.FillPath(Brushes.YellowGreen, graphicsPath);
        g.DrawPath(Pens.Blue, graphicsPath);
        return result;
    }

    public override bool EditLocation(PointF OldPoint, PointF NewPoint)
    {
        if (locked && Operation.bEditEnvironment)
        {
            return false;
        }
        if (!MouseOnMe(OldPoint))
        {
            return false;
        }
        PointF location = Location;
        SizeF size = Size;
        float angel = base.Angel;
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[2] { OldPoint, NewPoint };
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(array);
        TranslateMatrix.TransformPoints(array2);
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        for (int i = 0; i < array.Length; i++)
        {
            array[i].X += array2[1].X - array2[0].X;
            array[i].Y += array2[1].Y - array2[0].Y;
        }
        RotateAtPoint.X += NewPoint.X - OldPoint.X;
        RotateAtPoint.Y += NewPoint.Y - OldPoint.Y;
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, point);
        TranslateMatrix.TransformPoints(array);
        ImportantPoints = (PointF[])array.Clone();
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        if (Point.Round(Location) != Point.Round(location) || System.Drawing.Size.Round(Size) != System.Drawing.Size.Round(size) || base.Angel != angel)
        {
            NeedRefreshShape = true;
        }
        return true;
    }

    public override int EditPoint(PointF OldPoint, PointF NewPoint, int r)
    {
        if (locked && Operation.bEditEnvironment)
        {
            return -1;
        }
        if (ImportantPoints.Length < 10)
        {
            return -1;
        }
        float height = Height;
        PointF location = Location;
        SizeF size = Size;
        float angel = base.Angel;
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = (PointF[])ImportantPoints.Clone();
        PointF[] array3 = new PointF[2] { OldPoint, NewPoint };
        PointF drotateatnangel = default;
        bool flag = false;
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(array2);
        TranslateMatrix.TransformPoints(array3);
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        PartEditPoint(ref r, array2, array3, ref drotateatnangel);
        if (r == 557 || (r == -1 && OldPoint.X + 5f > RotateAtPoint.X + (height / 2f + 35f) * (float)Math.Cos((double)(RotateAngel - 90f) * Math.PI / 180.0) && OldPoint.X - 5f < RotateAtPoint.X + (height / 2f + 35f) * (float)Math.Cos((double)(RotateAngel - 90f) * Math.PI / 180.0) && OldPoint.Y + 5f > RotateAtPoint.Y + (height / 2f + 35f) * (float)Math.Sin((double)(RotateAngel - 90f) * Math.PI / 180.0) && OldPoint.Y - 5f < RotateAtPoint.Y + (height / 2f + 35f) * (float)Math.Sin((double)(RotateAngel - 90f) * Math.PI / 180.0)))
        {
            r = 557;
            if (OldPoint != NewPoint)
            {
                RotateAngel = 90f + (float)(Math.Atan2(NewPoint.Y - RotateAtPoint.Y, NewPoint.X - RotateAtPoint.X) * 180.0 / Math.PI);
            }
            TranslateMatrix.TransformPoints(ImportantPoints);
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
            TranslateMatrix.TransformPoints(ImportantPoints);
            if (Point.Round(Location) != Point.Round(location) || System.Drawing.Size.Round(Size) != System.Drawing.Size.Round(size) || base.Angel != angel)
            {
                NeedRefreshShape = true;
            }
            return r;
        }
        if (r == 55 || (r == -1 && OldPoint.X + 5f > RotateAtPoint.X && OldPoint.X - 5f < RotateAtPoint.X && OldPoint.Y + 5f > RotateAtPoint.Y && OldPoint.Y - 5f < RotateAtPoint.Y))
        {
            r = 55;
            RotateAtPoint.X += NewPoint.X - OldPoint.X;
            RotateAtPoint.Y += NewPoint.Y - OldPoint.Y;
            flag = true;
        }
        ref PointF reference = ref array2[0];
        reference = new PointF(float.MaxValue, float.MaxValue);
        ref PointF reference2 = ref array2[1];
        reference2 = new PointF(float.MinValue, float.MinValue);
        for (int i = 0; i < array2.Length - 8; i++)
        {
            array2[0].X = ((array2[0].X < array2[i + 8].X) ? array2[0].X : array2[i + 8].X);
            array2[0].Y = ((array2[0].Y < array2[i + 8].Y) ? array2[0].Y : array2[i + 8].Y);
            array2[1].X = ((array2[1].X > array2[i + 8].X) ? array2[1].X : array2[i + 8].X);
            array2[1].Y = ((array2[1].Y > array2[i + 8].Y) ? array2[1].Y : array2[i + 8].Y);
        }
        ref PointF reference3 = ref array2[2];
        reference3 = new PointF(array2[1].X, array2[0].Y);
        ref PointF reference4 = ref array2[3];
        reference4 = new PointF(array2[0].X, array2[1].Y);
        ref PointF reference5 = ref array2[4];
        reference5 = new PointF(array2[0].X / 2f + array2[1].X / 2f, array2[0].Y);
        ref PointF reference6 = ref array2[5];
        reference6 = new PointF(array2[0].X / 2f + array2[1].X / 2f, array2[1].Y);
        ref PointF reference7 = ref array2[6];
        reference7 = new PointF(array2[0].X, array2[0].Y / 2f + array2[1].Y / 2f);
        ref PointF reference8 = ref array2[7];
        reference8 = new PointF(array2[1].X, array2[0].Y / 2f + array2[1].Y / 2f);
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, point);
        PointF[] array4 = array2;
        int num = 0;
        while (true)
        {
            if (num < array4.Length)
            {
                PointF pointF = array4[num];
                if (float.IsNaN(pointF.X) || float.IsNaN(pointF.Y))
                {
                    break;
                }
                num++;
                continue;
            }
            if (!(Math.Abs(array2[0].X - array2[1].X) < 2f) && !(Math.Abs(array2[0].Y - array2[1].Y) < 2f))
            {
                TranslateMatrix.TransformPoints(array2);
                ImportantPoints = (PointF[])array2.Clone();
                if (!flag)
                {
                    PointF[] array5 = new PointF[1]
                    {
                        new(drotateatnangel.X + RotateAtPoint.X, drotateatnangel.Y + RotateAtPoint.Y)
                    };
                    TranslateMatrix.TransformPoints(array5);
                    RotateAtPoint = array5[0];
                }
            }
            break;
        }
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        if (Point.Round(Location) != Point.Round(location) || System.Drawing.Size.Round(Size) != System.Drawing.Size.Round(size) || base.Angel != angel)
        {
            NeedRefreshShape = true;
        }
        for (int j = 0; j < array.Length; j++)
        {
            if (Point.Round(array[j]) != Point.Round(ImportantPoints[j]))
            {
                NeedRefreshShape = true;
                break;
            }
        }
        return r;
    }

    public string makeHTML()
    {
        bool bold = StringFont.Bold;
        string text = ((!bold) ? "normal" : "bold");
        bool italic = StringFont.Italic;
        string text2 = ((!italic) ? "normal" : "Italic");
        bool underline = StringFont.Underline;
        string text3 = ((!underline) ? "none" : "underline");
        StringBuilder stringBuilder = new();
        stringBuilder.Append("<div id=\"" + ShapeName + "\" onclick=\"_onclick('" + ShapeName + "');\" style=\"z-index:" + base.Lay + ";display:inline; position:absolute; left:" + Location.X + "px; top:" + Location.Y + "px;width:" + Width.ToString() + "px;height:" + Height.ToString() + "px;color:" + ColorTranslator.ToHtml(PenColor) + ";font-Size:" + StringFont.Size + "pt; font-Style:" + text2 + "; font-family:" + StringFont.Name + "; font-Weight:" + text + ";text-decoration:" + text3 + ";\" width=\"" + Width.ToString() + "px\" height=\"" + Height.ToString() + "px\" >" + DisplayStr + "</div>");
        return stringBuilder.ToString();
    }

    public string makeCycleScript()
    {
        StringBuilder stringBuilder = new();
        Regex regex = new("\\[.*?\\]");
        if (ao)
        {
            string text = aobianliang.Replace("System.", "parent.");
            foreach (Match item in regex.Matches(text))
            {
                text = text.Replace(item.Value, "parent.VarOperation.GetValueByName(\"" + item.Value + "\")");
            }
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Text\")(" + text + ");");
        }
        if (doo)
        {
            string text2 = dobianlaing.Replace("System.", "parent.");
            foreach (Match item2 in regex.Matches(text2))
            {
                text2 = text2.Replace(item2.Value, "parent.VarOperation.GetValueByName(\"" + item2.Value + "\")");
            }
            stringBuilder.AppendLine("if(" + text2 + "==\"True\")");
            stringBuilder.AppendLine("\t$(\"#" + ShapeName + "\").data(\"set_Text\")(\"" + dotishion + "\");");
            stringBuilder.AppendLine("else");
            stringBuilder.AppendLine("\t$(\"#" + ShapeName + "\").data(\"set_Text\")(\"" + dotishioff + "\");");
        }
        if (zfcsc)
        {
            string text3 = zfcscbianliang.Replace("System.", "parent.");
            foreach (Match item3 in regex.Matches(text3))
            {
                text3 = text3.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
            }
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Text\")(" + text3 + ");");
        }
        if (czyd)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_czyd_Y\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_czyd_Y\",get_Y(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text4 = czydbianliang.Replace("System.", "parent.");
            foreach (Match item4 in regex.Matches(text4))
            {
                text4 = text4.Replace(item4.Value, "parent.VarOperation.GetValueByName(\"" + item4.Value + "\")");
            }
            stringBuilder.AppendLine("if (" + czydzhimin + "<" + text4 + " && " + text4 + "<" + czydzhimax + ")");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_czyd_Y\")");
            stringBuilder.AppendLine(" _Y += " + text4);
            stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\",_Y);");
            stringBuilder.AppendLine("}");
        }
        if (spyd)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_spyd_X\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_spyd_X\",get_X(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text5 = spydbianliang.Replace("System.", "parent.");
            foreach (Match item5 in regex.Matches(text5))
            {
                text5 = text5.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
            }
            stringBuilder.AppendLine("if (" + spydzhimin + "<" + text5 + " && " + text5 + "<" + spydzhimax + ")");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_spyd_X\")");
            stringBuilder.AppendLine(" _Y += " + text5);
            stringBuilder.AppendLine("set_X(\"" + ShapeName + "\",_Y);");
            stringBuilder.AppendLine("}");
        }
        if (gdbh)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\",get_Height(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text6 = gdbhbianliang.Replace("System.", "parent.");
            foreach (Match item6 in regex.Matches(text6))
            {
                text6 = text6.Replace(item6.Value, "parent.VarOperation.GetValueByName(\"" + item6.Value + "\")");
            }
            if (gdbhcankao == 1)
            {
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text6 + " && " + text6 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")");
                stringBuilder.AppendLine(" _Y += " + text6);
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
            }
            else
            {
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbhY\")==null)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_gdbhY\",get_Y(\"" + ShapeName + "\"));");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text6 + " && " + text6 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbhY\")");
                stringBuilder.AppendLine(" _Y -= " + text6);
                stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text6 + " && " + text6 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")");
                stringBuilder.AppendLine(" _Y += " + text6);
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbhY\")+$(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\") < get_Y(\"" + ShapeName + "\")+get_Height(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")+$(\"#" + ShapeName + "\").data(\"set_gdbhY\")));");
                stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("}");
            }
        }
        if (kdbh)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_kdbh_Width\",get_Width(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text7 = kdbhbianliang.Replace("System.", "parent.");
            foreach (Match item7 in regex.Matches(text7))
            {
                text7 = text7.Replace(item7.Value, "parent.VarOperation.GetValueByName(\"" + item7.Value + "\")");
            }
            if (kdbhcankao == 1)
            {
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text7 + " && " + text7 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")");
                stringBuilder.AppendLine(" _Y += " + text7);
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
            }
            else
            {
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")==null)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\",get_X(\"" + ShapeName + "\"));");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text7 + " && " + text7 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")");
                stringBuilder.AppendLine(" _Y -= " + text7);
                stringBuilder.AppendLine("set_X(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text7 + " && " + text7 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")");
                stringBuilder.AppendLine(" _Y += " + text7);
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\") < get_X(\"" + ShapeName + "\")+get_Width(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")));");
                stringBuilder.AppendLine("set_X(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("}");
            }
        }
        if (txyc)
        {
            string text8 = txycbianliang.Replace("System.", "parent.");
            foreach (Match item8 in regex.Matches(text8))
            {
                text8 = text8.Replace(item8.Value, "parent.VarOperation.GetValueByName(\"" + item8.Value + "\")");
            }
            if (txycnotbianliang)
            {
                stringBuilder.AppendLine("if (" + text8 + " == 0  || " + text8 + " == false)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Visible(\"" + ShapeName + "\", true)");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("else");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Visible(\"" + ShapeName + "\", false)");
                stringBuilder.AppendLine("}");
            }
            else
            {
                stringBuilder.AppendLine("if (" + text8 + " == 0  || " + text8 + " == false)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Visible(\"" + ShapeName + "\", false)");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("else");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Visible(\"" + ShapeName + "\", true)");
                stringBuilder.AppendLine("}");
            }
        }
        if (this.requestPropertyBindData != null)
        {
            DataTable dataTable = this.requestPropertyBindData();
            foreach (DataRow row in dataTable.Rows)
            {
                stringBuilder.AppendLine("parent.VarOperation.SetValueByName(\"[\"+pagename+\"." + ShapeName + "." + row["PropertyName"].ToString() + "]\",parent.VarOperation.GetValueByName(\"[" + row["Bind"].ToString() + "]\"));");
            }
        }
        return stringBuilder.ToString();
    }

    public string makeStyle()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("");
        return stringBuilder.ToString();
    }

    public string makeScript()
    {
        StringBuilder stringBuilder = new();
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Clable_value(\"" + ShapeName + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Text\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {get_Clable_value(\"" + ShapeName + "\")}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Text\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Clable_value(\"" + ShapeName + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_DisplayStr\"," + text + ")");
        bool flag = false;
        if (this.requestEventBindDict != null)
        {
            Dictionary<string, List<EventSetItem>> dictionary = this.requestEventBindDict();
            foreach (string key in dictionary.Keys)
            {
                switch (key)
                {
                    case "Click":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, dictionary, key);
                        makeCommonClickJS(stringBuilder);
                        MakeScriptOfDBOperation(stringBuilder);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        flag = true;
                        break;
                    case "MouseEnter":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, dictionary, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "MouseLeave":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, dictionary, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "DBOperationOK":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, dictionary, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "DBOperationErr":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, dictionary, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                }
            }
        }
        if (!flag)
        {
            stringBuilder.AppendLine("var " + ShapeName + "Value =parent.VarOperation.GetValueByName(\"" + dibianlaing + "\");");
            stringBuilder.AppendLine("function " + ShapeName + "_event_Click(){");
            makeCommonClickJS(stringBuilder);
            MakeScriptOfDBOperation(stringBuilder);
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Click\"," + ShapeName + "_event_Click)");
        }
        return stringBuilder.ToString();
    }

    private void makeCommonClickJS(StringBuilder sb)
    {
        if (ai)
        {
            sb.AppendLine("temp=window.prompt(\"" + aitishi + "\",parent.VarOperation.GetValueByName(\"" + aibianliang + "\"));");
            sb.AppendLine("if(temp!=null){");
            sb.AppendLine("\ttemp=Number(temp);");
            sb.AppendLine("\tif(temp==NaN||temp>=" + aimax + "||temp<=" + aimin + ")");
            sb.AppendLine("\t\talert(\"您输入的数据有误！\\r\\n输入范围：[" + aimin + "," + aimax + "]\");");
            sb.AppendLine("\telse");
            sb.AppendLine("\t\tparent.VarOperation.SetValueByName(\"" + aibianliang + "\",temp);");
            sb.AppendLine("}");
        }
        if (di)
        {
            sb.AppendLine(ShapeName + "Value =!" + ShapeName + "Value;");
            sb.AppendLine("temp=" + ShapeName + "Value;");
            sb.AppendLine("parent.VarOperation.SetValueByName(\"" + dibianlaing + "\",temp);");
        }
        if (zfcsr)
        {
            sb.AppendLine("temp=window.prompt(\"" + zfcsrtishi + "\",parent.VarOperation.GetValueByName(\"" + zfcsrbianliang + "\"));");
            sb.AppendLine("if(temp!=null){");
            sb.AppendLine("\tparent.VarOperation.SetValueByName(\"" + zfcsrbianliang + "\",temp);");
            sb.AppendLine("}");
        }
        if (ymqh)
        {
            string[] array = ymqhxianshi;
            foreach (string text in array)
            {
                sb.AppendLine("\tparent.SetPageVisible(\"" + text + "\",true);");
            }
            string[] array2 = ymqhyincang;
            foreach (string text2 in array2)
            {
                sb.AppendLine("\tparent.SetPageVisible(\"" + text2 + "\",false);");
            }
        }
    }

    private void MakeScriptOfDBOperation(StringBuilder sb)
    {
        if (dbselect)
        {
            sb.AppendLine("$.ajax({");
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/SelectToTable\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + base.DbselectSQL + "\")),");
            sb.AppendLine("success: function(result) {");
            string[] array = dbselectTO.Split(',');
            new List<string>();
            new List<string>();
            new Dictionary<string, string>();
            int num = -1;
            string[] array2 = array;
            foreach (string text in array2)
            {
                num++;
                if (text.Contains("."))
                {
                    Regex regex = new("\\.\\w{1,40}");
                    foreach (Match item in regex.Matches(text))
                    {
                        if (item.Value.Contains("."))
                        {
                            string text2 = item.Value.Replace(".", "");
                            string text3 = text.Replace(text2, "");
                            string text4 = text3.Substring(1, text3.Length - 3);
                            sb.AppendLine("parent.GetPage(\"" + text4 + "\")(\"#" + text2 + "\").data(\"setSelectResult\")(result," + num + ");");
                        }
                    }
                }
                else if (!(text == "{权限管理字段}") && text.Contains("["))
                {
                    string text5 = text.Substring(1, text.Length - 2);
                    sb.AppendLine("parent.VarOperation.SetValueByName(\"" + text5 + "\", $(result).children().eq(0).children().eq(0).children().eq(" + num + ").text());");
                }
            }
            sb.AppendLine("_onDBOperationOK(\"" + ShapeName + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + ShapeName + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbupdate)
        {
            sb.AppendLine("$.ajax({");
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/SelectToTable\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbupdateSQL + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("_onDBOperationOK(\"" + ShapeName + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + ShapeName + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbdelete)
        {
            sb.AppendLine("$.ajax({");
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/SelectToTable\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbdeleteSQL + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("_onDBOperationOK(\"" + ShapeName + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + ShapeName + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbinsert)
        {
            sb.AppendLine("$.ajax({");
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/SelectToTable\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbinsertSQL + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("_onDBOperationOK(\"" + ShapeName + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + ShapeName + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
    }

    private void MakeEvent(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
    {
        int num = 0;
        try
        {
            num = eventBindDict[eventName].Count;
        }
        catch (Exception)
        {
        }
        if (num == 0)
        {
            return;
        }
        int num2 = 0;
        sb.AppendLine("\tvar step=\"0\";");
        sb.AppendLine("\tlabelFinish:");
        sb.AppendLine("\twhile(true)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tswitch(step) {");
        sb.AppendLine("\t\tcase \"0\":");
        Regex regex = new("\\[.*?\\]");
        Regex regex2 = new("(\\b\\w+)\\.(\\b\\w+)\\.(\\b\\w+)\\((.*)\\)");
        foreach (EventSetItem item in eventBindDict[eventName])
        {
            string text = item.Condition;
            if (text == null)
            {
                text = "true";
            }
            else
            {
                List<string[]> replaceFunction = new();
                CShape.GetReplaceJSFunStr(regex2, text, ref replaceFunction);
                foreach (string[] item2 in replaceFunction)
                {
                    text = text.Replace(item2[0], "parent.GetPage(\"" + item2[1] + "\")(\"#" + item2[2] + "\").data(\"" + item2[3] + "\")(" + item2[4] + ")");
                }
                text = text.Replace("System.", "parent.");
                foreach (Match item3 in regex.Matches(text))
                {
                    text = text.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
                }
            }
            if (item.OperationType == "定义标签")
            {
                sb.AppendLine("\t\tcase \"" + item.FromObject + "\":");
            }
            else if (item.OperationType == "跳转标签")
            {
                sb.AppendLine("\t\tcase \"" + num2++ + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("\t\t\t\tstep=\"" + item.FromObject + "\";");
                sb.AppendLine("\t\t\t\tbreak;");
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "变量赋值")
            {
                int num3 = ++num2;
                sb.AppendLine("\t\tcase \"" + num3 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                string text2 = item.FromObject;
                List<string[]> replaceFunction2 = new();
                CShape.GetReplaceJSFunStr(regex2, text2, ref replaceFunction2);
                foreach (string[] item4 in replaceFunction2)
                {
                    text2 = text2.Replace(item4[0], "parent.GetPage(\"" + item4[1] + "\")(\"#" + item4[2] + "\").data(\"" + item4[3] + "\")(" + item4[4] + ")");
                }
                text2 = text2.Replace("System.", "parent.");
                foreach (Match item5 in regex.Matches(text2))
                {
                    text2 = text2.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
                }
                if (item.ToObject.Key != "")
                {
                    sb.AppendLine("parent.VarOperation.SetValueByName(\"[" + item.ToObject.Key + "]\"," + text2 + ")");
                }
                else
                {
                    sb.AppendLine("\t\t\t\t" + text2);
                }
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "属性赋值")
            {
                int num4 = ++num2;
                sb.AppendLine("\t\tcase \"" + num4 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                string text3 = item.FromObject;
                List<string[]> replaceFunction3 = new();
                CShape.GetReplaceJSFunStr(regex2, text3, ref replaceFunction3);
                foreach (string[] item6 in replaceFunction3)
                {
                    text3 = text3.Replace(item6[0], "parent.GetPage(\"" + item6[1] + "\")(\"#" + item6[2] + "\").data(\"" + item6[3] + "\")(" + item6[4] + ")");
                }
                text3 = text3.Replace("System.", "parent.");
                foreach (Match item7 in regex.Matches(text3))
                {
                    text3 = text3.Replace(item7.Value, "parent.VarOperation.GetValueByName(\"" + item7.Value + "\")");
                }
                string[] array = item.ToObject.Key.Split('.');
                sb.AppendLine("\t\t\t\tparent.GetPage(\"" + array[0] + "\")(\"#" + array[1] + "\").data(\"set_" + array[2] + "\")(" + text3 + ");");
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "服务器逻辑")
            {
                ServerLogicRequest serverLogicRequest = item.Tag as ServerLogicRequest;
                serverLogicRequest.Id = Guid.NewGuid().ToString();
                Operation.ServerLogicDict.Add(serverLogicRequest.Id, serverLogicRequest);
                int num5 = ++num2;
                sb.AppendLine("\t\tcase \"" + num5 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("var inputData=\"<Input>\";");
                foreach (string key in serverLogicRequest.InputDict.Keys)
                {
                    sb.AppendLine("inputData+=\"<InputItem Id=\\\"" + key + "\\\" Type=\\\"\"+(typeof parent.VarOperation.GetValueByName(\"" + key + "\"))+\"\\\">\"+parent.VarOperation.GetValueByName(\"" + key + "\")+\"</InputItem>\";");
                }
                sb.AppendLine("inputData+=\"</Input>\";");
                sb.AppendLine("var callsl = new parent.ServerLogic();");
                sb.AppendLine("callsl.ExcuteServerLogic(\"" + serverLogicRequest.Id + "\", inputData);");
                sb.AppendLine("\t\t\t}");
            }
            else
            {
                if (!(item.OperationType == "方法调用"))
                {
                    continue;
                }
                int num6 = ++num2;
                sb.AppendLine("\t\tcase \"" + num6 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("\t\t\t");
                if (item.ToObject.Key == "")
                {
                    string[] array2 = item.FromObject.Split('.');
                    StringBuilder stringBuilder = new();
                    foreach (KVPart<string, string> para in item.Paras)
                    {
                        string text4 = para.Key.Replace("System.", "parent.");
                        foreach (Match item8 in regex.Matches(text4))
                        {
                            text4 = text4.Replace(item8.Value, "parent.VarOperation.GetValueByName(\"" + item8.Value + "\")");
                        }
                        stringBuilder.Append("," + text4);
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(0, 1);
                    }
                    sb.AppendLine("parent.GetPage(\"" + array2[0] + "\")(\"#" + array2[1] + "\").data(\"" + array2[2] + "\")(" + stringBuilder.ToString() + ");");
                }
                else
                {
                    string[] array3 = item.FromObject.Split('.');
                    StringBuilder stringBuilder2 = new();
                    foreach (KVPart<string, string> para2 in item.Paras)
                    {
                        string text5 = para2.Key.Replace("System.", "parent.");
                        foreach (Match item9 in regex.Matches(text5))
                        {
                            text5 = text5.Replace(item9.Value, "parent.VarOperation.GetValueByName(\"" + item9.Value + "\")");
                        }
                        stringBuilder2.Append("," + text5);
                    }
                    if (stringBuilder2.Length > 0)
                    {
                        stringBuilder2.Remove(0, 1);
                    }
                    sb.AppendLine(string.Concat("parent.VarOperation.SetValueByName(\"[", item.ToObject, "]\",parent.GetPage(\"", array3[0], "\")(\"#", array3[1], "\").data(\"", array3[2], "\")(", stringBuilder2.ToString(), "));"));
                }
                sb.AppendLine("\t\t\t}");
            }
        }
        sb.AppendLine("\t\t\tbreak labelFinish;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
    }
}
