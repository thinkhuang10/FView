using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("0F602BBB-DE50-4970-9B4C-25890548056C")]
public class CBezier : CShape
{
    protected CBezier(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CBezier info");
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    public CBezier()
    {
        ShapeName = "Bezier" + CShape.SumLayer;
        _Brush = new SolidBrush(Color.Transparent);
        IsClose = false;
    }

    public override CShape Copy()
    {
        CBezier cBezier = new();
        Type type = cBezier.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cBezier, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cBezier, fieldInfo.GetValue(this));
            }
        }
        cBezier.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cBezier.ShapeName = ShapeName;
        new List<List<int>>();
        cBezier.ShapeID = ShapeID;
        cBezier.RotateAngel = RotateAngel;
        cBezier.RotateAtPoint = RotateAtPoint;
        cBezier.TranslateMatrix = TranslateMatrix.Clone();
        cBezier._Pen = (Pen)_Pen.Clone();
        cBezier._Brush = (Brush)_Brush.Clone();
        return cBezier;
    }

    public override List<PointF> ValuePoints()
    {
        List<PointF> list = new();
        if (ImportantPoints.Length < 10)
        {
            return list;
        }
        for (int i = 8; i < ImportantPoints.Length; i++)
        {
            list.Add(ImportantPoints[i]);
        }
        return list;
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
        }
        if (ImportantPoints.Length == 10)
        {
            AddPoint(new PointF(ImportantPoints[8].X + (ImportantPoints[9].X - ImportantPoints[8].X) / 3f, ImportantPoints[8].Y + (ImportantPoints[9].Y - ImportantPoints[8].Y) / 3f));
            AddPoint(new PointF(ImportantPoints[8].X + (ImportantPoints[9].X - ImportantPoints[8].X) * 2f / 3f, ImportantPoints[8].Y + (ImportantPoints[9].Y - ImportantPoints[8].Y) * 2f / 3f));
        }
        if (ImportantPoints.Length == 12)
        {
            return false;
        }
        if (ImportantPoints.Length == int.MaxValue)
        {
            return false;
        }
        return true;
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
        PointF[] array2 = new PointF[(array.Length - 8) * 2];
        for (int i = 0; i < array.Length - 8; i++)
        {
            ref PointF reference = ref array2[i];
            reference = array[i + 8];
            ref PointF reference2 = ref array2[(array.Length - 8) * 2 - 1 - i];
            reference2 = array[i + 8];
        }
        graphicsPath.AddBezier(array[8], array[10], array[11], array[9]);
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

    public override bool MouseOnMe(PointF ThePoint)
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
        graphicsPath.CloseFigure();
        graphicsPath.Transform(TranslateMatrix);
        bool flag = graphicsPath.IsVisible(ThePoint);
        graphicsPath.Reset();
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

    public override bool DrawSelect(Graphics g)
    {
        bool result = false;
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 8)
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
        graphicsPath.AddEllipse(new RectangleF(RotateAtPoint.X - 3f, RotateAtPoint.Y - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        g.FillPath(Brushes.White, graphicsPath);
        g.DrawPath(Pens.Blue, graphicsPath);
        graphicsPath.Reset();
        graphicsPath.AddRectangle(new RectangleF(array[8].X - 3f, array[8].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[9].X - 3f, array[9].Y - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        g.FillPath(Brushes.Red, graphicsPath);
        g.DrawPath(Pens.Blue, graphicsPath);
        graphicsPath.Reset();
        graphicsPath.AddRectangle(new RectangleF(array[10].X - 3f, array[10].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[11].X - 3f, array[11].Y - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        g.FillPath(Brushes.Pink, graphicsPath);
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
        for (int i = 8; i < array2.Length; i++)
        {
            if (r == 100 + i || (r == -1 && array3[0].X + 5f > array2[i].X && array3[0].X - 5f < array2[i].X && array3[0].Y + 5f > array2[i].Y && array3[0].Y - 5f < array2[i].Y))
            {
                r = 100 + i;
                array2[i].X += array3[1].X - array3[0].X;
                array2[i].Y += array3[1].Y - array3[0].Y;
                break;
            }
        }
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
        for (int j = 0; j < array2.Length - 8; j++)
        {
            array2[0].X = ((array2[0].X < array2[j + 8].X) ? array2[0].X : array2[j + 8].X);
            array2[0].Y = ((array2[0].Y < array2[j + 8].Y) ? array2[0].Y : array2[j + 8].Y);
            array2[1].X = ((array2[1].X > array2[j + 8].X) ? array2[1].X : array2[j + 8].X);
            array2[1].Y = ((array2[1].Y > array2[j + 8].Y) ? array2[1].Y : array2[j + 8].Y);
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
            if (array2.Length > 10 && (Math.Abs(array2[0].X - array2[1].X) < 2f || Math.Abs(array2[0].Y - array2[1].Y) < 2f))
            {
                break;
            }
            TranslateMatrix.TransformPoints(array2);
            if (array2.Length != 10 || !(Math.Abs(array2[8].X - array2[9].X) < 2f) || !(Math.Abs(array2[8].Y - array2[9].Y) < 2f))
            {
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
        for (int k = 0; k < array.Length; k++)
        {
            if (Point.Round(array[k]) != Point.Round(ImportantPoints[k]))
            {
                NeedRefreshShape = true;
                break;
            }
        }
        return r;
    }
}