using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("1CF13D7A-826D-4bfe-A2D4-F91784B4AD9A")]
public class CGraphicsPath : CShape
{
    [NonSerialized]
    private List<GraphicsPath> lgp;

    [NonSerialized]
    private List<Brush> lbr;

    [NonSerialized]
    private List<Pen> lpen;

    [NonSerialized]
    private List<GraphicsPath> templp;

    [NonSerialized]
    private List<Pen> templpen;

    [NonSerialized]
    private List<Brush> templb;

    public override bool NeedRefreshShape
    {
        get
        {
            return NeedRefresh;
        }
        set
        {
            NeedRefresh = value;
            if (value)
            {
                needRefreshShape = true;
                needRefreshBrush = true;
                CShape[] shapes = Shapes;
                foreach (CShape cShape in shapes)
                {
                    cShape.NeedRefreshShape = true;
                }
            }
        }
    }

    protected CGraphicsPath(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CGraphicsPath info");
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    public CGraphicsPath()
    {
        ShapeName = "GraphicsPath" + CShape.SumLayer;
    }

    public void DllCopyTo(string dir)
    {
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            if (cShape is CControl)
            {
                ((CControl)cShape).DllCopyTo(dir);
            }
            else if (cShape is CPixieControl)
            {
                ((CPixieControl)cShape).DllCopyTo(dir);
            }
            else if (cShape is CGraphicsPath)
            {
                ((CGraphicsPath)cShape).DllCopyTo(dir);
            }
        }
    }

    public override CShape Copy()
    {
        CGraphicsPath cGraphicsPath = new();
        Type type = cGraphicsPath.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cGraphicsPath, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cGraphicsPath, fieldInfo.GetValue(this));
            }
        }
        cGraphicsPath.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cGraphicsPath.ShapeName = ShapeName;
        new List<List<int>>();
        cGraphicsPath.ShapeID = ShapeID;
        cGraphicsPath.RotateAngel = RotateAngel;
        cGraphicsPath.RotateAtPoint = RotateAtPoint;
        cGraphicsPath.TranslateMatrix = TranslateMatrix.Clone();
        cGraphicsPath._Pen = (Pen)_Pen.Clone();
        cGraphicsPath._Brush = (Brush)_Brush.Clone();
        cGraphicsPath.Shapes = (CShape[])Shapes.Clone();
        cGraphicsPath._b = (Bitmap[])_b.Clone();
        for (int j = 0; j < Shapes.Length; j++)
        {
            cGraphicsPath.Shapes[j] = new CShape();
            cGraphicsPath.Shapes[j] = Shapes[j].Copy();
            cGraphicsPath.Shapes[j].ShapeID = Guid.NewGuid();
        }
        cGraphicsPath.p00 = p00;
        cGraphicsPath.p11 = p11;
        return cGraphicsPath;
    }

    public bool AddPath(CShape TheShape)
    {
        NeedRefreshShape = true;
        List<CShape> list = new();
        for (int i = 0; i < Shapes.Length; i++)
        {
            list.Add(Shapes[i]);
        }
        list.Add(TheShape);
        Shapes = new CShape[list.Count];
        Shapes = (CShape[])list.ToArray().Clone();
        List<PointF> list2 = new()
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
        for (int j = 0; j < ImportantPoints.Length - 8; j++)
        {
            list2.Add(ImportantPoints[j + 8]);
        }
        list2.Add(new PointF(TheShape.ImportantPoints[0].X / 2f + TheShape.ImportantPoints[1].X / 2f, TheShape.ImportantPoints[0].Y / 2f + TheShape.ImportantPoints[1].Y / 2f));
        ImportantPoints = new PointF[list2.Count];
        ImportantPoints = (PointF[])list2.ToArray().Clone();
        ImportantPoints[0].X = float.MaxValue;
        ImportantPoints[0].Y = float.MaxValue;
        ImportantPoints[1].X = float.MinValue;
        ImportantPoints[1].Y = float.MinValue;
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            PointF[] array = cShape.ValuePoints().ToArray();
            for (int l = 0; l < array.Length; l++)
            {
                PointF pointF = array[l];
                ImportantPoints[0].X = ((ImportantPoints[0].X < pointF.X) ? ImportantPoints[0].X : pointF.X);
                ImportantPoints[0].Y = ((ImportantPoints[0].Y < pointF.Y) ? ImportantPoints[0].Y : pointF.Y);
                ImportantPoints[1].X = ((ImportantPoints[1].X > pointF.X) ? ImportantPoints[1].X : pointF.X);
                ImportantPoints[1].Y = ((ImportantPoints[1].Y > pointF.Y) ? ImportantPoints[1].Y : pointF.Y);
            }
        }
        p00 = ImportantPoints[0];
        p11 = ImportantPoints[1];
        ref PointF reference = ref ImportantPoints[2];
        reference = new PointF(ImportantPoints[1].X, ImportantPoints[0].Y);
        ref PointF reference2 = ref ImportantPoints[3];
        reference2 = new PointF(ImportantPoints[0].X, ImportantPoints[1].Y);
        ref PointF reference3 = ref ImportantPoints[4];
        reference3 = new PointF(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y);
        ref PointF reference4 = ref ImportantPoints[5];
        reference4 = new PointF(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[1].Y);
        ref PointF reference5 = ref ImportantPoints[6];
        reference5 = new PointF(ImportantPoints[0].X, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);
        ref PointF reference6 = ref ImportantPoints[7];
        reference6 = new PointF(ImportantPoints[1].X, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);
        RotateAtPoint.X = ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f;
        RotateAtPoint.Y = ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f;
        if (ImportantPoints.Length == int.MaxValue)
        {
            return false;
        }
        return true;
    }

    public override List<Bitmap> GetImages()
    {
        List<Bitmap> list = new();
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            foreach (Bitmap image in cShape.GetImages())
            {
                list.Add(image);
            }
        }
        return list;
    }

    public List<GraphicsPath> DrawShapesBySize(PointF _p00, float Width, float Height)
    {
        Matrix matrix = new();
        if (Width > 0f)
        {
            if (Height > 0f)
            {
                matrix.Translate(0f - p00.X, 0f - p00.Y, MatrixOrder.Append);
                matrix.Scale((0f - Width) / (p00.X - p11.X), (0f - Height) / (p00.Y - p11.Y), MatrixOrder.Append);
                matrix.Translate(_p00.X, _p00.Y, MatrixOrder.Append);
            }
            else
            {
                matrix.Translate(0f - p00.X, 0f - p00.Y, MatrixOrder.Append);
                matrix.Scale((0f - Width) / (p00.X - p11.X), (0f - Height) / (p00.Y - p11.Y), MatrixOrder.Append);
                matrix.Translate(_p00.X, _p00.Y - Height, MatrixOrder.Append);
            }
        }
        else if (Height > 0f)
        {
            matrix.Translate(0f - p00.X, 0f - p00.Y, MatrixOrder.Append);
            matrix.Scale((0f - Width) / (p00.X - p11.X), (0f - Height) / (p00.Y - p11.Y), MatrixOrder.Append);
            matrix.Translate(_p00.X - Width, _p00.Y, MatrixOrder.Append);
        }
        else
        {
            matrix.Translate(0f - p00.X, 0f - p00.Y, MatrixOrder.Append);
            matrix.Scale((0f - Width) / (p00.X - p11.X), (0f - Height) / (p00.Y - p11.Y), MatrixOrder.Append);
            matrix.Translate(_p00.X - Width, _p00.Y - Height, MatrixOrder.Append);
        }
        while (true)
        {
            List<GraphicsPath> list = new();
            CShape[] shapes = Shapes;
            int num = 0;
            while (true)
            {
                if (num < shapes.Length)
                {
                    CShape cShape = shapes[num];
                    GraphicsPath[] array = cShape.HowToDraw().ToArray();
                    foreach (GraphicsPath addingPath in array)
                    {
                        try
                        {
                            GraphicsPath graphicsPath = new();
                            graphicsPath.AddPath(addingPath, connect: false);
                            graphicsPath.Transform(matrix);
                            list.Add(graphicsPath);
                        }
                        catch (Exception)
                        {
                            List<CShape> list2 = new(Shapes);
                            list2.Remove(cShape);
                            Shapes = list2.ToArray();
                            goto end_IL_027e;
                        }
                    }
                    num++;
                    continue;
                }
                return list;

            end_IL_027e:
                break;
            }
        }
    }

    public override List<GraphicsPath> HowToDraw()
    {
        if (lgp != null && !needRefreshShape)
        {
            return lgp;
        }
        lgp = new List<GraphicsPath>();
        if (ImportantPoints.Length < 2)
        {
            return lgp;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        GraphicsPath[] array2 = DrawShapesBySize(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y).ToArray();
        foreach (GraphicsPath graphicsPath in array2)
        {
            graphicsPath.Transform(TranslateMatrix);
            lgp.Add(graphicsPath);
        }
        needRefreshShape = false;
        return lgp;
    }

    public override List<Brush> HowToFill()
    {
        if (lbr != null && !needRefreshShape)
        {
            return lbr;
        }
        lbr = new List<Brush>();
        if (ImportantPoints.Length < 2)
        {
            return lbr;
        }
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            foreach (Brush item in cShape.HowToFill())
            {
                lbr.Add(item);
            }
        }
        return lbr;
    }

    public override List<Pen> HowToDrawLine()
    {
        if (lpen != null && !needRefreshShape)
        {
            return lpen;
        }
        lpen = new List<Pen>();
        if (ImportantPoints.Length < 2)
        {
            return lpen;
        }
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            foreach (Pen item in cShape.HowToDrawLine())
            {
                lpen.Add(item);
            }
        }
        return lpen;
    }

    public override bool DrawMe(Graphics g)
    {
        try
        {
            if (ImportantPoints.Length < 2)
            {
                return false;
            }
            bool flag = false;
            if (templp == null || templpen == null || templb == null || needRefreshShape || needRefreshBrush)
            {
                flag = true;
            }
            HowToDraw();
            PointF[] array = (PointF[])ImportantPoints.Clone();
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(array);
            templp = DrawShapesBySize(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y);
            templpen = HowToDrawLine();
            templb = HowToFill();
            List<Bitmap> images = GetImages();
            if (flag)
            {
                for (int i = 0; i < templp.Count; i++)
                {
                    GraphicsPath graphicsPath = templp[i];
                    graphicsPath.Transform(TranslateMatrix);
                    if (templb[i] != null)
                    {
                        if (templb[i] is PathGradientBrush pathGradientBrush)
                        {
                            Color centerColor = pathGradientBrush.CenterColor;
                            Color color = pathGradientBrush.SurroundColors[0];
                            pathGradientBrush = new PathGradientBrush(graphicsPath)
                            {
                                CenterColor = centerColor
                            };
                            Color[] array2 = (Color[])pathGradientBrush.SurroundColors.Clone();
                            for (int j = 0; j < pathGradientBrush.SurroundColors.Length; j++)
                            {
                                array2[j] = color;
                            }
                            pathGradientBrush.SurroundColors = array2;
                            g.FillPath(pathGradientBrush, graphicsPath);
                            templb[i] = pathGradientBrush;
                        }
                        else if (templb[i] is LinearGradientBrush linearGradientBrush)
                        {
                            float angle = Convert.ToSingle(Math.Acos(linearGradientBrush.Transform.Elements[0] / Convert.ToSingle(Math.Pow(Math.Pow(linearGradientBrush.Transform.Elements[0], 2.0) + Math.Pow(linearGradientBrush.Transform.Elements[2], 2.0), 0.5))) / Math.PI * 180.0);
                            float[] factors = (float[])linearGradientBrush.Blend.Factors.Clone();
                            float[] positions = (float[])linearGradientBrush.Blend.Positions.Clone();
                            linearGradientBrush = new LinearGradientBrush(graphicsPath.GetBounds(), linearGradientBrush.LinearColors[0], linearGradientBrush.LinearColors[1], angle, isAngleScaleable: true);
                            Blend blend = new(4)
                            {
                                Factors = factors,
                                Positions = positions
                            };
                            linearGradientBrush.Blend = blend;
                            g.FillPath(linearGradientBrush, graphicsPath);
                            templb[i] = linearGradientBrush;
                        }
                        else
                        {
                            g.FillPath(templb[i], graphicsPath);
                        }
                        g.DrawPath(templpen[i], graphicsPath);
                    }
                    else if (images[i] != null)
                    {
                        g.DrawImage(images[i], new PointF[3]
                        {
                            graphicsPath.PathPoints[0],
                            graphicsPath.PathPoints[1],
                            graphicsPath.PathPoints[2]
                        });
                    }
                }
                needRefreshBrush = false;
            }
            else
            {
                for (int k = 0; k < templp.Count; k++)
                {
                    GraphicsPath graphicsPath2 = templp[k];
                    graphicsPath2.Transform(TranslateMatrix);
                    if (templb[k] != null)
                    {
                        g.FillPath(templb[k], graphicsPath2);
                        g.DrawPath(templpen[k], graphicsPath2);
                    }
                    else if (images[k] != null)
                    {
                        g.DrawImage(images[k], new PointF[3]
                        {
                            graphicsPath2.PathPoints[0],
                            graphicsPath2.PathPoints[1],
                            graphicsPath2.PathPoints[2]
                        });
                    }
                }
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
        bool flag = false;
        if (templp == null || templpen == null || templb == null || needRefreshShape || needRefreshBrush)
        {
            flag = true;
        }
        HowToDraw();
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        templp = DrawShapesBySize(new PointF(Math.Min(array[0].X, array[1].X), Math.Min(array[0].Y, array[1].Y)), array[1].X - array[0].X, array[1].Y - array[0].Y);
        templpen = HowToDrawLine();
        templb = HowToFill();
        List<Bitmap> images = GetImages();
        if (flag)
        {
            for (int i = 0; i < templp.Count; i++)
            {
                GraphicsPath graphicsPath = templp.ToArray()[i];
                graphicsPath.Transform(TranslateMatrix);
                if (templb[i] != null)
                {
                    if (templb[i] is PathGradientBrush pathGradientBrush)
                    {
                        Color centerColor = pathGradientBrush.CenterColor;
                        Color color = pathGradientBrush.SurroundColors[0];
                        pathGradientBrush = new PathGradientBrush(graphicsPath)
                        {
                            CenterColor = centerColor
                        };
                        Color[] array2 = (Color[])pathGradientBrush.SurroundColors.Clone();
                        for (int j = 0; j < pathGradientBrush.SurroundColors.Length; j++)
                        {
                            array2[j] = color;
                        }
                        pathGradientBrush.SurroundColors = array2;
                        g.FillPath(pathGradientBrush, graphicsPath);
                        templb[i] = pathGradientBrush;
                    }
                    else if (templb[i] is LinearGradientBrush linearGradientBrush)
                    {
                        float angle = Convert.ToSingle(Math.Acos(linearGradientBrush.Transform.Elements[0] / Convert.ToSingle(Math.Pow(Math.Pow(linearGradientBrush.Transform.Elements[0], 2.0) + Math.Pow(linearGradientBrush.Transform.Elements[2], 2.0), 0.5))) / Math.PI * 180.0);
                        float[] factors = (float[])linearGradientBrush.Blend.Factors.Clone();
                        float[] positions = (float[])linearGradientBrush.Blend.Positions.Clone();
                        linearGradientBrush = new LinearGradientBrush(graphicsPath.GetBounds(), linearGradientBrush.LinearColors[0], linearGradientBrush.LinearColors[1], angle, isAngleScaleable: true);
                        Blend blend = new(4)
                        {
                            Factors = factors,
                            Positions = positions
                        };
                        linearGradientBrush.Blend = blend;
                        g.FillPath(linearGradientBrush, graphicsPath);
                        templb[i] = linearGradientBrush;
                    }
                    else
                    {
                        g.FillPath(templb[i], graphicsPath);
                    }
                    g.DrawPath(templpen[i], graphicsPath);
                }
                else if (images[i] != null)
                {
                    g.DrawImage(images[i], new PointF[3]
                    {
                        graphicsPath.PathPoints[0],
                        graphicsPath.PathPoints[1],
                        graphicsPath.PathPoints[2]
                    });
                }
            }
            needRefreshBrush = false;
        }
        else
        {
            for (int k = 0; k < templp.Count; k++)
            {
                GraphicsPath graphicsPath2 = templp[k];
                graphicsPath2.Transform(TranslateMatrix);
                if (templb[k] != null)
                {
                    g.FillPath(templb[k], graphicsPath2);
                    g.DrawPath(templpen[k], graphicsPath2);
                }
                else if (images[k] != null)
                {
                    g.DrawImage(images[k], new PointF[3]
                    {
                        graphicsPath2.PathPoints[0],
                        graphicsPath2.PathPoints[1],
                        graphicsPath2.PathPoints[2]
                    });
                }
            }
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

    public override bool DrawRectLine(Graphics g, bool trueorfalse)
    {
        bool result = false;
        GraphicsPath graphicsPath = new();
        if (ImportantPoints.Length < 2 || !trueorfalse)
        {
            return result;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X) - 2f, Math.Min(array[0].Y, array[1].Y) - 2f, Math.Abs(array[1].X - array[0].X) + 4f, Math.Abs(array[1].Y - array[0].Y) + 4f));
        graphicsPath.Transform(TranslateMatrix);
        Pen pen = new(Color.White, 5f);
        g.DrawPath(pen, graphicsPath);
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
        
        if (ImportantPoints.Length < 9)
            return -1;
        
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
        for (int i = 0; i < array.Length; i++)
        {
            if (Point.Round(array[i]) != Point.Round(ImportantPoints[i]))
            {
                NeedRefreshShape = true;
                break;
            }
        }
        return r;
    }

    public override bool Mirror(int v)
    {
        PointF pointF = new(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);
        if (v == 0)
        {
            VMirror = !VMirror;
            for (int i = 0; i < ImportantPoints.Length; i++)
            {
                ImportantPoints[i].X = pointF.X * 2f - ImportantPoints[i].X;
            }
            RotateAtPoint.X = pointF.X * 2f - RotateAtPoint.X;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        if (v == 1)
        {
            HMirror = !HMirror;
            for (int j = 0; j < ImportantPoints.Length; j++)
            {
                ImportantPoints[j].Y = pointF.Y * 2f - ImportantPoints[j].Y;
            }
            RotateAtPoint.Y = pointF.Y * 2f - RotateAtPoint.Y;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            cShape.Mirror(v);
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
        for (int l = 0; l < array.Length - 8; l++)
        {
            array[0].X = ((array[0].X < array[l + 8].X) ? array[0].X : array[l + 8].X);
            array[0].Y = ((array[0].Y < array[l + 8].Y) ? array[0].Y : array[l + 8].Y);
            array[1].X = ((array[1].X > array[l + 8].X) ? array[1].X : array[l + 8].X);
            array[1].Y = ((array[1].Y > array[l + 8].Y) ? array[1].Y : array[l + 8].Y);
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
}
