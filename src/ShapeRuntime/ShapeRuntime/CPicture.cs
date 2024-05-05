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
public class CPicture : CShape
{
    [NonSerialized]
    public Image Cimage;

    [NonSerialized]
    public byte[] DATA;

    private string[] _bKeys;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

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
        swapgp?.Dispose();
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
            _b[0]?.RotateFlip(RotateFlipType.RotateNoneFlipX);
            RotateAtPoint.X = pointF.X * 2f - RotateAtPoint.X;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        if (v == 1)
        {
            HMirror = !HMirror;
            _b[0]?.RotateFlip(RotateFlipType.Rotate180FlipX);
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
}
