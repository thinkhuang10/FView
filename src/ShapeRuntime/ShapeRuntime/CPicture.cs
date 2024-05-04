using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
[Guid("334AD5D9-923A-4269-9F41-11C5D4AEC2AC")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CPicture : CShape, ISupportHtml5
{
    [NonSerialized]
    public Image Cimage;

    [NonSerialized]
    public byte[] DATA;

    private string[] _bKeys;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    protected CPicture(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CPicture info");
        }
        CPicture obj = new();
        FieldInfo[] fields = typeof(CPicture).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CPicture))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "_bKeys")
            {
                _bKeys = (string[])enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_bKeys", _bKeys);
    }

    public CPicture()
    {
        ShapeName = "Picture" + CShape.SumLayer;
        _b = new Bitmap[1];
    }

    public string GetImageGUID()
    {
        if (_bKeys != null && !string.IsNullOrEmpty(_bKeys[0]))
        {
            return _bKeys[0];
        }
        return null;
    }

    public override void BeforeSaveMe()
    {
        if (_bKeys == null)
        {
            _bKeys = new string[1];
            _bKeys[0] = DHMIImageManage.SaveImage(_b[0]);
            _b[0].Tag = _bKeys[0];
        }
        else if (_b[0] != null)
        {
            _bKeys = new string[1];
            _bKeys[0] = _b[0].Tag.ToString();
        }
        base.BeforeSaveMe();
    }

    public override void AfterLoadMe()
    {
        if (DATA == null && _bKeys != null)
        {
            Image image = DHMIImageManage.LoadImage(_bKeys[0]);
            if (image != null)
            {
                Image image2 = image;
                image2.Tag = image.Tag;
                _b[0] = image2;
            }
        }
        else if (DATA != null || _bKeys != null)
        {
            MemoryStream stream = new(DATA);
            Bitmap bitmap = new(stream);
            float num = 200f / (float)bitmap.Width;
            _b[0] = bitmap;
            AddPoint(new PointF(ImportantPoints[8].X + num * (float)bitmap.Width, ImportantPoints[8].Y + num * (float)bitmap.Height));
        }
        base.AfterLoadMe();
    }

    public override CShape Copy()
    {
        CPicture cPicture = new();
        Type type = cPicture.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cPicture, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cPicture, fieldInfo.GetValue(this));
            }
        }
        cPicture.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cPicture.ShapeName = ShapeName;
        new List<List<int>>();
        cPicture.ShapeID = ShapeID;
        cPicture.RotateAngel = RotateAngel;
        cPicture.RotateAtPoint = RotateAtPoint;
        cPicture.TranslateMatrix = TranslateMatrix.Clone();
        cPicture._Pen = (Pen)_Pen.Clone();
        cPicture._Brush = (Brush)_Brush.Clone();
        cPicture.DATA = DATA;
        cPicture._bKeys = _bKeys;
        cPicture._b = (Image[])_b.Clone();
        return cPicture;
    }

    public void SetFile(string _file)
    {
    }

    public override bool AddPoint(PointF NewPoint)
    {
        NeedRefreshShape = true;
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
        if (ImportantPoints.Length == 9)
        {
            if (DATA == null)
            {
                Bitmap bitmap = new(Cimage)
                {
                    Tag = Cimage.Tag
                };
                _bKeys = new string[1];
                _bKeys[0] = bitmap.Tag.ToString();
                float num = 200f / (float)bitmap.Width;
                _b[0] = bitmap;
                AddPoint(new PointF(ImportantPoints[8].X + num * (float)bitmap.Width, ImportantPoints[8].Y + num * (float)bitmap.Height));
            }
            else
            {
                MemoryStream stream = new(DATA);
                Bitmap bitmap2 = new(stream);
                float num2 = 200f / (float)bitmap2.Width;
                _b[0] = bitmap2;
                AddPoint(new PointF(ImportantPoints[8].X + num2 * (float)bitmap2.Width, ImportantPoints[8].Y + num2 * (float)bitmap2.Height));
            }
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

    public override List<Brush> HowToFill()
    {
        List<Brush> list = new()
        {
            null
        };
        return list;
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
        if (ImportantPoints.Length < 2)
        {
            return list;
        }
        graphicsPath.AddLines(new PointF[6]
        {
            ImportantPoints[0],
            ImportantPoints[2],
            ImportantPoints[3],
            ImportantPoints[2],
            ImportantPoints[1],
            ImportantPoints[3]
        });
        if (swapgp != null)
        {
            swapgp.Dispose();
        }
        swapgp = graphicsPath;
        needRefreshShape = false;
        list.Add(graphicsPath);
        return list;
    }

    public override bool Mirror(int v)
    {
        PointF pointF = new(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);
        if (v == 0)
        {
            VMirror = !VMirror;
            if (_b[0] != null)
            {
                _b[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            RotateAtPoint.X = pointF.X * 2f - RotateAtPoint.X;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        if (v == 1)
        {
            HMirror = !HMirror;
            if (_b[0] != null)
            {
                _b[0].RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            RotateAtPoint.Y = pointF.Y * 2f - RotateAtPoint.Y;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        ref PointF reference = ref array[0];
        reference = new PointF(float.MaxValue, float.MaxValue);
        ref PointF reference2 = ref array[1];
        reference2 = new PointF(float.MinValue, float.MinValue);
        for (int i = 0; i < array.Length - 8; i++)
        {
            array[0].X = ((array[0].X < array[i + 8].X) ? array[0].X : array[i + 8].X);
            array[0].Y = ((array[0].Y < array[i + 8].Y) ? array[0].Y : array[i + 8].Y);
            array[1].X = ((array[1].X > array[i + 8].X) ? array[1].X : array[i + 8].X);
            array[1].Y = ((array[1].Y > array[i + 8].Y) ? array[1].Y : array[i + 8].Y);
        }
        ref PointF reference3 = ref array[2];
        reference3 = new PointF(array[1].X, array[0].Y);
        ref PointF reference4 = ref array[3];
        reference4 = new PointF(array[0].X, array[1].Y);
        ref PointF reference5 = ref array[4];
        reference5 = new PointF(array[0].X / 2f + array[1].X / 2f, array[0].Y);
        ref PointF reference6 = ref array[5];
        reference6 = new PointF(array[0].X / 2f + array[1].X / 2f, array[1].Y);
        ref PointF reference7 = ref array[6];
        reference7 = new PointF(array[0].X, array[0].Y / 2f + array[1].Y / 2f);
        ref PointF reference8 = ref array[7];
        reference8 = new PointF(array[1].X, array[0].Y / 2f + array[1].Y / 2f);
        matrix.Reset();
        matrix.RotateAt(RotateAngel, point);
        TranslateMatrix.TransformPoints(array);
        ImportantPoints = (PointF[])array.Clone();
        NeedRefreshShape = true;
        return true;
    }

    public override bool DrawMe(Graphics g)
    {
        if (ImportantPoints.Length < 2)
        {
            return false;
        }
        GraphicsPath graphicsPath = HowToDraw()[0];
        PointF[] destPoints = new PointF[3]
        {
            graphicsPath.PathPoints[0],
            graphicsPath.PathPoints[1],
            graphicsPath.PathPoints[2]
        };
        try
        {
            if (_b[0] != null)
            {
                g.DrawImage(_b[0], destPoints);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override bool DrawMe(Graphics g, bool trueorfalse)
    {
        if (!visible)
        {
            return false;
        }
        if (ImportantPoints.Length < 2)
        {
            return false;
        }
        GraphicsPath graphicsPath = HowToDraw()[0];
        PointF[] destPoints = new PointF[3]
        {
            graphicsPath.PathPoints[0],
            graphicsPath.PathPoints[1],
            graphicsPath.PathPoints[2]
        };
        if (_b[0] != null)
        {
            g.DrawImage(_b[0], destPoints);
        }
        DrawRectLine(g, trueorfalse);
        return true;
    }

    public override bool DrawSelect(Graphics g)
    {
        bool result = false;
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 2)
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
        StringBuilder stringBuilder = new();
        string text = (base.Visible ? "visible" : "hidden");
        string text2 = _bKeys[0] != null && !(_bKeys[0] == "") ? "\"/Resources/" + _bKeys[0] + "\"" : "\"\"";
        stringBuilder.Append("<img id=\"" + ShapeName + "\" src=" + text2 + " onclick=\"_onclick('" + ShapeName + "');\" style=\"z-index:" + base.Lay + ";visibility:" + text + "; display:inline; position:absolute;left:" + Location.X + "px; top:" + Location.Y + "px;width:" + Width.ToString() + "px;height:" + Height.ToString() + "px;;\" width=\"" + Width.ToString() + "px\" height=\"" + Height.ToString() + "px\" ></input>");
        return stringBuilder.ToString();
    }

    public string makeCycleScript()
    {
        StringBuilder stringBuilder = new();
        Regex regex = new("\\[.*?\\]");
        if (czyd)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_czyd_Y\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_czyd_Y\",get_Y(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text = czydbianliang.Replace("System.", "parent.");
            foreach (Match item in regex.Matches(text))
            {
                text = text.Replace(item.Value, "parent.VarOperation.GetValueByName(\"" + item.Value + "\")");
            }
            stringBuilder.AppendLine("if (" + czydzhimin + "<" + text + " && " + text + "<" + czydzhimax + ")");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_czyd_Y\")");
            stringBuilder.AppendLine(" _Y += " + text);
            stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\",_Y);");
            stringBuilder.AppendLine("}");
        }
        if (spyd)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_spyd_X\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_spyd_X\",get_X(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text2 = spydbianliang.Replace("System.", "parent.");
            foreach (Match item2 in regex.Matches(text2))
            {
                text2 = text2.Replace(item2.Value, "parent.VarOperation.GetValueByName(\"" + item2.Value + "\")");
            }
            stringBuilder.AppendLine("if (" + spydzhimin + "<" + text2 + " && " + text2 + "<" + spydzhimax + ")");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_spyd_X\")");
            stringBuilder.AppendLine(" _Y += " + text2);
            stringBuilder.AppendLine("set_X(\"" + ShapeName + "\",_Y);");
            stringBuilder.AppendLine("}");
        }
        if (gdbh)
        {
            stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")==null)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\",get_Height(\"" + ShapeName + "\"));");
            stringBuilder.AppendLine("}");
            string text3 = gdbhbianliang.Replace("System.", "parent.");
            foreach (Match item3 in regex.Matches(text3))
            {
                text3 = text3.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
            }
            if (gdbhcankao == 1)
            {
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text3 + " && " + text3 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")");
                stringBuilder.AppendLine(" _Y += " + text3);
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
            }
            else
            {
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbhY\")==null)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_gdbhY\",get_Y(\"" + ShapeName + "\"));");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text3 + " && " + text3 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbhY\")");
                stringBuilder.AppendLine(" _Y -= " + text3);
                stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + gdbhzhimin + "<" + text3 + " && " + text3 + "<" + gdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")");
                stringBuilder.AppendLine(" _Y += " + text3);
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
            string text4 = kdbhbianliang.Replace("System.", "parent.");
            foreach (Match item4 in regex.Matches(text4))
            {
                text4 = text4.Replace(item4.Value, "parent.VarOperation.GetValueByName(\"" + item4.Value + "\")");
            }
            if (kdbhcankao == 1)
            {
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text4 + " && " + text4 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")");
                stringBuilder.AppendLine(" _Y += " + text4);
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
            }
            else
            {
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")==null)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\",get_X(\"" + ShapeName + "\"));");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text4 + " && " + text4 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")");
                stringBuilder.AppendLine(" _Y -= " + text4);
                stringBuilder.AppendLine("set_X(\"" + ShapeName + "\",_Y);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if (" + kdbhzhimin + "<" + text4 + " && " + text4 + "<" + kdbhzhimax + ")");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("var _Y =  $(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")");
                stringBuilder.AppendLine(" _Y += " + text4);
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
            string text5 = txycbianliang.Replace("System.", "parent.");
            foreach (Match item5 in regex.Matches(text5))
            {
                text5 = text5.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
            }
            if (txycnotbianliang)
            {
                stringBuilder.AppendLine("if (" + text5 + " == 0  || " + text5 + " == false)");
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
                stringBuilder.AppendLine("if (" + text5 + " == 0  || " + text5 + " == false)");
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
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    _ = row;
                }
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
        if (eventBindDict != null)
        {
            foreach (string key in eventBindDict.Keys)
            {
                switch (key)
                {
                    case "Click":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "CheckedChanged":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "MouseEnter":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "MouseLeave":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "DoubleClick":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "DBOperationOK":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                    case "DBOperationErr":
                        stringBuilder.AppendLine("function " + ShapeName + "_event_" + key + "(){");
                        MakeEvent(stringBuilder, eventBindDict, key);
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"" + key + "\"," + ShapeName + "_event_" + key + ")");
                        break;
                }
            }
        }
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Visible(\"" + ShapeName + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Visible\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Visible(\"" + ShapeName + "\")}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Visible\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Height\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Height(\"" + ShapeName + "\")}");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Height\"," + text + ")");
        return stringBuilder.ToString();
    }

    private void MakeEvent(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
    {
        try
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
                    List<string> list = new();
                    foreach (Match item3 in regex.Matches(text))
                    {
                        if (!list.Contains(item3.Value))
                        {
                            list.Add(item3.Value);
                            text = text.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
                        }
                    }
                    list.Clear();
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
                    List<string> list2 = new();
                    foreach (Match item5 in regex.Matches(text2))
                    {
                        if (!list2.Contains(item5.Value))
                        {
                            list2.Add(item5.Value);
                            text2 = text2.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
                        }
                    }
                    list2.Clear();
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
                    List<string> list3 = new();
                    foreach (Match item7 in regex.Matches(text3))
                    {
                        if (!list3.Contains(item7.Value))
                        {
                            list3.Add(item7.Value);
                            text3 = text3.Replace(item7.Value, "parent.VarOperation.GetValueByName(\"" + item7.Value + "\")");
                        }
                    }
                    list3.Clear();
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
                            List<string> list4 = new();
                            foreach (Match item8 in regex.Matches(text4))
                            {
                                if (!list4.Contains(item8.Value))
                                {
                                    list4.Add(item8.Value);
                                    text4 = text4.Replace(item8.Value, "parent.VarOperation.GetValueByName(\"" + item8.Value + "\")");
                                }
                            }
                            list4.Clear();
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
                            List<string> list5 = new();
                            foreach (Match item9 in regex.Matches(text5))
                            {
                                if (!list5.Contains(item9.Value))
                                {
                                    list5.Add(item9.Value);
                                    text5 = text5.Replace(item9.Value, "parent.VarOperation.GetValueByName(\"" + item9.Value + "\")");
                                }
                            }
                            list5.Clear();
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
        catch
        {
        }
    }
}
