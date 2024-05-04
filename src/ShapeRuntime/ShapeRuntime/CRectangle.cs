using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("04234775-9B57-4be8-8B3C-7198B0DC09EC")]
public class CRectangle : CShape, ISupportHtml5
{
    [OptionalField]
    public bool ld;

    [OptionalField]
    public int ldkuandu = 30;

    [OptionalField]
    public int ldgaodu = 10;

    [OptionalField]
    public int ldjianxi = 10;

    [OptionalField]
    public string ldbianliang = "";

    [OptionalField]
    public Color ldcolor1 = Color.White;

    [OptionalField]
    public Color ldcolor2 = Color.White;

    [OptionalField]
    public int ldfangxiang = 1;

    [OptionalField]
    public float ldstep;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    protected CRectangle(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CRectangle info");
        }
        CRectangle obj = new();
        FieldInfo[] fields = typeof(CRectangle).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CRectangle))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "ld")
            {
                ld = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "ldkuandu")
            {
                ldkuandu = (int)enumerator.Value;
            }
            else if (enumerator.Name == "ldgaodu")
            {
                ldgaodu = (int)enumerator.Value;
            }
            else if (enumerator.Name == "ldjianxi")
            {
                ldjianxi = (int)enumerator.Value;
            }
            else if (enumerator.Name == "ldbianliang")
            {
                ldbianliang = (string)enumerator.Value;
            }
            else if (enumerator.Name == "ldcolor1")
            {
                ldcolor1 = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "ldcolor2")
            {
                ldcolor2 = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "ldfangxiang")
            {
                ldfangxiang = (int)enumerator.Value;
            }
            else if (enumerator.Name == "ldstep")
            {
                ldstep = (float)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        if (ld)
        {
            info.AddValue("ld", ld);
            info.AddValue("ldkuandu", ldkuandu);
            info.AddValue("ldgaodu", ldgaodu);
            info.AddValue("ldjianxi", ldjianxi);
            info.AddValue("ldbianliang", ldbianliang);
            info.AddValue("ldcolor1", ldcolor1);
            info.AddValue("ldcolor2", ldcolor2);
            info.AddValue("ldfangxiang", ldfangxiang);
            info.AddValue("ldstep", ldstep);
        }
    }

    public CRectangle()
    {
        ShapeName = "Rectangle" + CShape.SumLayer;
    }

    public override void AfterLoadMe()
    {
        base.AfterLoadMe();
        if (ldkuandu == 0)
        {
            ld = false;
            ldkuandu = 30;
            ldbianliang = "";
            ldgaodu = 10;
            ldjianxi = 10;
            ldcolor1 = Color.White;
            ldcolor2 = Color.White;
            ldfangxiang = 1;
            ldstep = 0f;
        }
        else if (ldgaodu == 0)
        {
            ldgaodu = 10;
            ldjianxi = 10;
        }
    }

    public override CShape Copy()
    {
        CRectangle cRectangle = new();
        Type type = cRectangle.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cRectangle, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cRectangle, fieldInfo.GetValue(this));
            }
        }
        cRectangle.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cRectangle.ShapeName = ShapeName;
        new List<List<int>>();
        cRectangle.ShapeID = ShapeID;
        cRectangle.RotateAngel = RotateAngel;
        cRectangle.FillAngel = FillAngel;
        cRectangle.RotateAtPoint = RotateAtPoint;
        cRectangle.TranslateMatrix = TranslateMatrix.Clone();
        cRectangle.FillAngel = FillAngel;
        cRectangle._Pen = (Pen)_Pen.Clone();
        cRectangle._Brush = (Brush)_Brush.Clone();
        cRectangle.ld = ld;
        cRectangle.ldkuandu = ldkuandu;
        cRectangle.ldgaodu = ldgaodu;
        cRectangle.ldjianxi = ldjianxi;
        cRectangle.ldbianliang = ldbianliang;
        cRectangle.ldcolor1 = ldcolor1;
        cRectangle.ldcolor2 = ldcolor2;
        cRectangle.ldfangxiang = ldfangxiang;
        cRectangle.ldstep = ldstep;
        for (int j = 0; j < DelRegionShape.Count; j++)
        {
            cRectangle.DelRegionShape.Add(DelRegionShape[j].Copy());
            cRectangle.delimportant00points.Add(delimportant00points[j]);
            cRectangle.delimportant11points.Add(delimportant11points[j]);
        }
        return cRectangle;
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
            return false;
        }
        return true;
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
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)));
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
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)));
        foreach (GraphicsPath item in DelRegionByShapes(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y))
        {
            graphicsPath.AddPath(item, connect: false);
        }
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

    public override bool DrawMe(Graphics g)
    {
        if (ld)
        {
            GraphicsPath graphicsPath = new();
            if (ImportantPoints.Length < 10)
            {
                return false;
            }
            PointF[] array = (PointF[])ImportantPoints.Clone();
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(array);
            graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)));
            foreach (GraphicsPath item in DelRegionByShapes(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y))
            {
                graphicsPath.AddPath(item, connect: false);
            }
            graphicsPath.Transform(TranslateMatrix);
            if (swapgp != null)
            {
                swapgp.Dispose();
            }
            swapgp = graphicsPath;
            RefreshBrush();
            g.FillPath(_Brush, graphicsPath);
            g.DrawPath(_Pen, graphicsPath);
            GraphicsPath graphicsPath2 = new();
            Pen pen = new(Color.Black);
            if (ldfangxiang == 1)
            {
                PointF pointF = array[4];
                PointF pointF2 = array[5];
                LinearGradientBrush linearGradientBrush = new(pointF, pointF2, ldcolor1, ldcolor2);
                linearGradientBrush.MultiplyTransform(TranslateMatrix);
                pen = new Pen(linearGradientBrush)
                {
                    Width = ldgaodu
                };
                float num = pointF2.Y - pointF.Y;
                float num2 = (float)(ldkuandu + ldjianxi) * (1f - ldstep);
                if (num2 > (float)ldjianxi)
                {
                    graphicsPath2.AddLine(pointF, new PointF(pointF.X, pointF.Y + num2 - (float)ldjianxi));
                    graphicsPath2.CloseFigure();
                }
                for (; num2 + (float)ldkuandu + (float)ldjianxi <= num; num2 += (float)(ldkuandu + ldjianxi))
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), new PointF(pointF.X, pointF.Y + num2 + (float)ldkuandu));
                    graphicsPath2.CloseFigure();
                }
                if (num - num2 > (float)ldkuandu)
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), new PointF(pointF.X, pointF.Y + num2 + (float)ldkuandu));
                    graphicsPath2.CloseFigure();
                }
                else
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), pointF2);
                    graphicsPath2.CloseFigure();
                }
            }
            else if (ldfangxiang == -1)
            {
                PointF pointF = array[6];
                PointF pointF2 = array[7];
                LinearGradientBrush linearGradientBrush2 = new(pointF, pointF2, ldcolor1, ldcolor2);
                linearGradientBrush2.MultiplyTransform(TranslateMatrix);
                pen = new Pen(linearGradientBrush2)
                {
                    Width = ldgaodu
                };
                float num3 = pointF2.X - pointF.X;
                float num4 = (float)(ldkuandu + ldjianxi) * (1f - ldstep);
                if (num4 > (float)ldjianxi)
                {
                    graphicsPath2.AddLine(pointF, new PointF(pointF.X + num4 - (float)ldjianxi, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                for (; num4 + (float)ldkuandu + (float)ldjianxi <= num3; num4 += (float)(ldkuandu + ldjianxi))
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), new PointF(pointF.X + num4 + (float)ldkuandu, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                if (num3 - num4 > (float)ldkuandu)
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), new PointF(pointF.X + num4 + (float)ldkuandu, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                else
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), pointF2);
                    graphicsPath2.CloseFigure();
                }
            }
            graphicsPath2.Transform(TranslateMatrix);
            g.DrawPath(pen, graphicsPath2);
            return true;
        }
        return base.DrawMe(g);
    }

    public void flow()
    {
        ldstep += 0.2f;
        ldstep %= 1f;
        NeedRefreshShape = true;
    }

    public override bool DrawMe(Graphics g, bool trueorfalse)
    {
        if (ld)
        {
            if (!visible)
            {
                return false;
            }
            GraphicsPath graphicsPath = new();
            if (ImportantPoints.Length < 2)
            {
                return false;
            }
            PointF[] array = (PointF[])ImportantPoints.Clone();
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(array);
            graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y), Math.Abs(array[1].X - array[0].X), Math.Abs(array[1].Y - array[0].Y)));
            foreach (GraphicsPath item in DelRegionByShapes(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y))
            {
                graphicsPath.AddPath(item, connect: false);
            }
            graphicsPath.Transform(TranslateMatrix);
            if (swapgp != null)
            {
                swapgp.Dispose();
            }
            swapgp = graphicsPath;
            RefreshBrush();
            g.FillPath(_Brush, graphicsPath);
            g.DrawPath(_Pen, graphicsPath);
            GraphicsPath graphicsPath2 = new();
            Pen pen = Pens.Black;
            if (ldfangxiang == 1)
            {
                PointF pointF = array[4];
                PointF pointF2 = array[5];
                LinearGradientBrush linearGradientBrush = new(pointF, pointF2, ldcolor1, ldcolor2);
                linearGradientBrush.MultiplyTransform(TranslateMatrix);
                pen = new Pen(linearGradientBrush)
                {
                    Width = ldgaodu
                };
                float num = pointF2.Y - pointF.Y;
                float num2 = (float)(ldkuandu + ldjianxi) * (1f - ldstep);
                if (num2 > (float)ldjianxi)
                {
                    graphicsPath2.AddLine(pointF, new PointF(pointF.X, pointF.Y + num2 - (float)ldjianxi));
                    graphicsPath2.CloseFigure();
                }
                for (; num2 + (float)ldkuandu + (float)ldjianxi <= num; num2 += (float)(ldkuandu + ldjianxi))
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), new PointF(pointF.X, pointF.Y + num2 + (float)ldkuandu));
                    graphicsPath2.CloseFigure();
                }
                if (num - num2 > (float)ldkuandu)
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), new PointF(pointF.X, pointF.Y + num2 + (float)ldkuandu));
                    graphicsPath2.CloseFigure();
                }
                else
                {
                    graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num2), pointF2);
                    graphicsPath2.CloseFigure();
                }
            }
            else if (ldfangxiang == -1)
            {
                PointF pointF = array[6];
                PointF pointF2 = array[7];
                LinearGradientBrush linearGradientBrush2 = new(pointF, pointF2, ldcolor1, ldcolor2);
                linearGradientBrush2.MultiplyTransform(TranslateMatrix);
                pen = new Pen(linearGradientBrush2)
                {
                    Width = ldgaodu
                };
                float num3 = pointF2.X - pointF.X;
                float num4 = (float)(ldkuandu + ldjianxi) * (1f - ldstep);
                if (num4 > (float)ldjianxi)
                {
                    graphicsPath2.AddLine(pointF, new PointF(pointF.X + num4 - (float)ldjianxi, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                for (; num4 + (float)ldkuandu + (float)ldjianxi <= num3; num4 += (float)(ldkuandu + ldjianxi))
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), new PointF(pointF.X + num4 + (float)ldkuandu, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                if (num3 - num4 > (float)ldkuandu)
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), new PointF(pointF.X + num4 + (float)ldkuandu, pointF.Y));
                    graphicsPath2.CloseFigure();
                }
                else
                {
                    graphicsPath2.AddLine(new PointF(pointF.X + num4, pointF.Y), pointF2);
                    graphicsPath2.CloseFigure();
                }
            }
            graphicsPath2.Transform(TranslateMatrix);
            g.DrawPath(pen, graphicsPath2);
            DrawRectLine(g, trueorfalse);
            return true;
        }
        return base.DrawMe(g, trueorfalse);
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
        stringBuilder.Append("<canvas id=\"" + ShapeName + "\" onclick=\"_onclick('" + ShapeName + "');\" style=\"z-index:" + base.Lay + ";display:inline; position:absolute; left:" + Location.X + "px; top:" + Location.Y + "px;\" width=\"" + Width.ToString() + "px\" height=\"" + Height.ToString() + "px\" ></canvas>");
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
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
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
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbhY\")+$(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\") < get_Y(\"" + ShapeName + "\")+get_Height(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")+$(\"#" + ShapeName + "\").data(\"set_gdbhY\")));");
                stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
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
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
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
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\") < get_X(\"" + ShapeName + "\")+get_Width(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")));");
                stringBuilder.AppendLine("set_X(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\")");
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
                    stringBuilder.AppendLine("parent.VarOperation.SetValueByName(\"[\"+pagename+\"." + ShapeName + "." + row["PropertyName"].ToString() + "]\",parent.VarOperation.GetValueByName(\"[" + row["Bind"].ToString() + "]\"));");
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
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"LineColor\",\"" + ColorTranslator.ToHtml(PenColor) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Color1\",\"" + ColorTranslator.ToHtml(Color1) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Color2\",\"" + ColorTranslator.ToHtml(Color2) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"FillStyle\",\"" + BrushStyle.ToString() + "\");");
        if (BrushStyle == _BrushStyle.线性渐变填充)
        {
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"FillAngle\"," + FillAngel + ");");
        }
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_LineColor\",function () {return get_LineColor(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Color1\",function () {return get_Color1(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Color2\",function () {return get_Color2(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_X\",function () {return get_X(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Y\",function () {return get_Y(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Width\",function () {return get_Width(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Height\",function () {return get_Height(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Visible\",function () {return get_Visible(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_LineColor\",function (value) {set_LineColor(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Color1\",function (value) {set_Color1(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Color2\",function (value) {set_Color2(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_X\",function (value) {set_X(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Y\",function (value) {set_Y(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Width\",function (value) {set_Width(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Height\",function (value) {set_Height(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Visible\",function (value) {set_Visible(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("rectangle_Draw(\"" + ShapeName + "\");");
        if (eventBindDict != null)
        {
            stringBuilder.AppendLine(MakeScriptForShape());
        }
        return stringBuilder.ToString();
    }
}
