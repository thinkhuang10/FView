using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("3A3C975E-4A0A-4452-A657-9C22ED707328")]
[ComVisible(true)]
public class CCircleRect : CShape
{
    private int cornerRadius = 5;

    [Category("外观")]
    [DisplayName("圆角半径")]
    [ReadOnly(false)]
    [Description("设定图形圆角半径。")]
    public int CornerRadius
    {
        get
        {
            return cornerRadius;
        }
        set
        {
            if (cornerRadius != value)
            {
                NeedRefreshShape = true;
            }
            cornerRadius = value;
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(ImportantPoints);
            if ((float)cornerRadius > Math.Abs(ImportantPoints[1].X - ImportantPoints[0].X) / 2f || (float)cornerRadius > Math.Abs(ImportantPoints[1].Y - ImportantPoints[0].Y) / 2f)
            {
                cornerRadius = Convert.ToInt32((Math.Abs(ImportantPoints[1].X - ImportantPoints[0].X) < Math.Abs(ImportantPoints[1].Y - ImportantPoints[0].Y)) ? Math.Abs(ImportantPoints[1].X - ImportantPoints[0].X) : Math.Abs(ImportantPoints[1].Y - ImportantPoints[0].Y)) / 2;
            }
            ref PointF reference = ref ImportantPoints[10];
            reference = ImportantPoints[0] - new SizeF(Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0), Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0));
            matrix.Invert();
            matrix.TransformPoints(ImportantPoints);
        }
    }

    protected CCircleRect(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CCircleRect info");
        }
        CCircleRect obj = new();
        FieldInfo[] fields = typeof(CCircleRect).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CCircleRect))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "cornerRadius")
            {
                cornerRadius = (int)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("cornerRadius", cornerRadius);
    }

    public CCircleRect()
    {
        ShapeName = "CircleRect" + CShape.SumLayer;
    }

    internal static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
    {
        if (cornerRadius <= 0)
        {
            cornerRadius = 1;
        }
        GraphicsPath graphicsPath = new();
        graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
        graphicsPath.AddLine(rect.X + (float)cornerRadius, rect.Y, rect.Right - (float)cornerRadius, rect.Y);
        graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
        graphicsPath.AddLine(rect.Right, rect.Y + (float)cornerRadius, rect.Right, rect.Bottom - (float)cornerRadius);
        graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y + rect.Height - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 0f, 90f);
        graphicsPath.AddLine(rect.Right - (float)cornerRadius, rect.Bottom, rect.X + (float)cornerRadius, rect.Bottom);
        graphicsPath.AddArc(rect.X, rect.Bottom - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90f, 90f);
        graphicsPath.AddLine(rect.X, rect.Bottom - (float)cornerRadius, rect.X, rect.Y + (float)cornerRadius);
        graphicsPath.CloseFigure();
        return graphicsPath;
    }

    public override List<PointF> ValuePoints()
    {
        List<PointF> list = new();
        if (ImportantPoints.Length < 10)
        {
            return list;
        }
        for (int i = 4; i < 8; i++)
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
        p00 = ImportantPoints[0];
        p11 = ImportantPoints[1];
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
            if (ImportantPoints.Length == 10)
            {
                AddPoint(ImportantPoints[0] - new SizeF(Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0), Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0)));
            }
            return false;
        }
        return true;
    }

    public override CShape Copy()
    {
        CCircleRect cCircleRect = new();
        Type type = cCircleRect.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cCircleRect, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cCircleRect, fieldInfo.GetValue(this));
            }
        }
        cCircleRect.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cCircleRect.ShapeName = ShapeName;
        new List<List<int>>();
        cCircleRect.ShapeID = ShapeID;
        cCircleRect.RotateAngel = RotateAngel;
        cCircleRect.RotateAtPoint = RotateAtPoint;
        cCircleRect.FillAngel = FillAngel;
        cCircleRect.FillAngel = FillAngel;
        cCircleRect.TranslateMatrix = TranslateMatrix.Clone();
        cCircleRect._Pen = (Pen)_Pen.Clone();
        cCircleRect._Brush = (Brush)_Brush.Clone();
        for (int j = 0; j < DelRegionShape.Count; j++)
        {
            cCircleRect.DelRegionShape.Add(DelRegionShape[j].Copy());
            cCircleRect.delimportant00points.Add(delimportant00points[j]);
            cCircleRect.delimportant11points.Add(delimportant11points[j]);
        }
        return cCircleRect;
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
        graphicsPath.AddPath(CreateRoundedRectanglePath(new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)), cornerRadius), connect: false);
        foreach (GraphicsPath item in DelRegionByShapes(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y))
        {
            graphicsPath.AddPath(item, connect: false);
        }
        graphicsPath.Transform(TranslateMatrix);
        swapgp?.Dispose();
        swapgp = graphicsPath;
        needRefreshShape = false;
        list.Add(graphicsPath);
        return list;
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
        graphicsPath.AddRectangle(new RectangleF(array[10].X - 3f, array[10].Y - 3f, 7f, 7f));
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
        if (locked)
            return false;
        
        if (!MouseOnMe(OldPoint))
            return false;
        
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
        if (locked)
            return -1;

        if (ImportantPoints.Length < 10)
            return -1;
   
        float height = Height;
        PointF location = Location;
        SizeF size = Size;
        float angel = base.Angel;
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = (PointF[])ImportantPoints.Clone();
        PointF[] array3 = new PointF[2] { OldPoint, NewPoint };
        PointF drotateatnangel = default;
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(array2);
        TranslateMatrix.TransformPoints(array3);
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        if (r == 110 || (r == -1 && array3[0].X + 5f > array2[10].X && array3[0].X - 5f < array2[10].X && array3[0].Y + 5f > array2[10].Y && array3[0].Y - 5f < array2[10].Y))
        {
            r = 110;
            float num = (array3[1].Y + array3[0].X - (array2[0].Y - array2[0].X)) / 2f;
            float num2 = (array3[1].Y + array3[0].X + (array2[0].Y - array2[0].X)) / 2f;
            float num3 = num - array2[0].X;
            float num4 = num2 - array2[0].Y;
            if (num3 < 0f && num4 < 0f)
            {
                cornerRadius = Convert.ToInt32(Math.Pow(Math.Pow(num3, 2.0) + Math.Pow(num4, 2.0), 0.5));
                if ((float)cornerRadius > Math.Abs(array2[1].X - array2[0].X) / 2f || (float)cornerRadius > Math.Abs(array2[1].Y - array2[0].Y) / 2f)
                {
                    cornerRadius = Convert.ToInt32((Math.Abs(array2[1].X - array2[0].X) < Math.Abs(array2[1].Y - array2[0].Y)) ? Math.Abs(array2[1].X - array2[0].X) : Math.Abs(array2[1].Y - array2[0].Y)) / 2;
                }
            }
            else
            {
                cornerRadius = 1;
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
        bool flag = false;
        if (r == 55 || (r == -1 && OldPoint.X + 5f > RotateAtPoint.X && OldPoint.X - 5f < RotateAtPoint.X && OldPoint.Y + 5f > RotateAtPoint.Y && OldPoint.Y - 5f < RotateAtPoint.Y))
        {
            r = 55;
            RotateAtPoint.X += NewPoint.X - OldPoint.X;
            RotateAtPoint.Y += NewPoint.Y - OldPoint.Y;
            flag = true;
        }
        if ((float)cornerRadius > Math.Abs(array2[1].X - array2[0].X) / 2f || (float)cornerRadius > Math.Abs(array2[1].Y - array2[0].Y) / 2f)
        {
            cornerRadius = Convert.ToInt32((Math.Abs(array2[1].X - array2[0].X) < Math.Abs(array2[1].Y - array2[0].Y)) ? Math.Abs(array2[1].X - array2[0].X) : Math.Abs(array2[1].Y - array2[0].Y)) / 2;
        }
        ref PointF reference = ref array2[10];
        reference = array2[0] - new SizeF(Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0), Convert.ToSingle((double)cornerRadius * Math.Pow(2.0, 0.5) / 2.0));
        ref PointF reference2 = ref array2[0];
        reference2 = new PointF(float.MaxValue, float.MaxValue);
        ref PointF reference3 = ref array2[1];
        reference3 = new PointF(float.MinValue, float.MinValue);
        for (int i = 0; i < array2.Length - 9; i++)
        {
            array2[0].X = ((array2[0].X < array2[i + 8].X) ? array2[0].X : array2[i + 8].X);
            array2[0].Y = ((array2[0].Y < array2[i + 8].Y) ? array2[0].Y : array2[i + 8].Y);
            array2[1].X = ((array2[1].X > array2[i + 8].X) ? array2[1].X : array2[i + 8].X);
            array2[1].Y = ((array2[1].Y > array2[i + 8].Y) ? array2[1].Y : array2[i + 8].Y);
        }
        ref PointF reference4 = ref array2[2];
        reference4 = new PointF(array2[1].X, array2[0].Y);
        ref PointF reference5 = ref array2[3];
        reference5 = new PointF(array2[0].X, array2[1].Y);
        ref PointF reference6 = ref array2[4];
        reference6 = new PointF(array2[0].X / 2f + array2[1].X / 2f, array2[0].Y);
        ref PointF reference7 = ref array2[5];
        reference7 = new PointF(array2[0].X / 2f + array2[1].X / 2f, array2[1].Y);
        ref PointF reference8 = ref array2[6];
        reference8 = new PointF(array2[0].X, array2[0].Y / 2f + array2[1].Y / 2f);
        ref PointF reference9 = ref array2[7];
        reference9 = new PointF(array2[1].X, array2[0].Y / 2f + array2[1].Y / 2f);
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, point);
        PointF[] array4 = array2;
        int num5 = 0;
        while (true)
        {
            if (num5 < array4.Length)
            {
                PointF pointF = array4[num5];
                if (float.IsNaN(pointF.X) || float.IsNaN(pointF.Y))
                {
                    break;
                }
                num5++;
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
