using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
//using VectorGraphProvider;

namespace ShapeRuntime;

[Serializable]
[Guid("334DD5D9-953A-4669-9F01-21C5D4AEC2CC")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CVectorGraph : CShape
{
    public string type = "";

    //public string dllfile = "VectorGraphProvider.dll";

    [NonSerialized]
    private readonly object svgObject;

    protected CVectorGraph(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CVectorGraph info");
        }
        CVectorGraph obj = new();
        FieldInfo[] fields = typeof(CVectorGraph).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CVectorGraph))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "type")
            {
                type = (string)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("type", type);
    }

    public CVectorGraph()
    {
        ShapeName = "VectorGraph" + CShape.SumLayer;
        _b = new Bitmap[1];
    }

    public override CShape Copy()
    {
        CVectorGraph cVectorGraph = new();
        Type type = cVectorGraph.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cVectorGraph, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cVectorGraph, fieldInfo.GetValue(this));
            }
        }
        cVectorGraph.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cVectorGraph.ShapeName = ShapeName;
        List<List<int>> list = new();
        foreach (List<int> userRegion in UserRegionList)
        {
            list.Add(new List<int>(userRegion));
        }
        cVectorGraph.UserRegionList = list;
        cVectorGraph.ShapeID = ShapeID;
        cVectorGraph.RotateAngel = RotateAngel;
        cVectorGraph.RotateAtPoint = RotateAtPoint;
        cVectorGraph.TranslateMatrix = TranslateMatrix.Clone();
        cVectorGraph._Pen = (Pen)_Pen.Clone();
        cVectorGraph._Brush = (Brush)_Brush.Clone();
        cVectorGraph.type = this.type;
        cVectorGraph._b = (Bitmap[])_b.Clone();
        return cVectorGraph;
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
        //if (ImportantPoints.Length == 9)
        //{
        //	Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + dllfile);
        //	Type type = assembly.GetType(this.type);
        //	if (type == null)
        //	{
        //		return false;
        //	}
        //	object obj = Activator.CreateInstance(type);
        //	VectorGraph vectorGraph = obj as VectorGraph;
        //	float num = 200f / vectorGraph.OriginalSize.Width;
        //	AddPoint(new PointF(ImportantPoints[8].X + num * vectorGraph.OriginalSize.Width, ImportantPoints[8].Y + num * vectorGraph.OriginalSize.Height));
        //}
        //if (ImportantPoints.Length >= 10)
        //{
        //	RotateAtPoint.X = ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f;
        //	RotateAtPoint.Y = ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f;
        //	ref PointF reference = ref ImportantPoints[2];
        //	reference = new PointF(ImportantPoints[1].X, ImportantPoints[0].Y);
        //	ref PointF reference2 = ref ImportantPoints[3];
        //	reference2 = new PointF(ImportantPoints[0].X, ImportantPoints[1].Y);
        //	ref PointF reference3 = ref ImportantPoints[4];
        //	reference3 = new PointF(RotateAtPoint.X, ImportantPoints[0].Y);
        //	ref PointF reference4 = ref ImportantPoints[5];
        //	reference4 = new PointF(RotateAtPoint.X, ImportantPoints[1].Y);
        //	ref PointF reference5 = ref ImportantPoints[6];
        //	reference5 = new PointF(ImportantPoints[0].X, RotateAtPoint.Y);
        //	ref PointF reference6 = ref ImportantPoints[7];
        //	reference6 = new PointF(ImportantPoints[1].X, RotateAtPoint.Y);
        //	UpdateSVG();
        //	return true;
        //}
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
        list.Add(graphicsPath);
        if (swapgp != null)
        {
            swapgp.Dispose();
        }
        swapgp = graphicsPath;
        needRefreshShape = false;
        return list;
    }

    public override bool DrawMe(Graphics g)
    {
        new GraphicsPath();
        if (ImportantPoints.Length < 2)
        {
            return false;
        }
        HowToDraw();
        PointF[] destPoints = new PointF[3]
        {
            ImportantPoints[0],
            ImportantPoints[2],
            ImportantPoints[3]
        };
        if (_b[0] != null)
        {
            UpdateSVG();
            g.DrawImage(_b[0], destPoints);
        }
        else
        {
            UpdateSVG();
            if (_b[0] != null)
            {
                g.DrawImage(_b[0], destPoints);
            }
        }
        return true;
    }

    public override bool DrawMe(Graphics g, bool trueorfalse)
    {
        if (!visible)
        {
            return false;
        }
        new GraphicsPath();
        if (ImportantPoints.Length < 2)
        {
            return false;
        }
        HowToDraw();
        PointF[] destPoints = new PointF[3]
        {
            ImportantPoints[0],
            ImportantPoints[2],
            ImportantPoints[3]
        };
        if (_b[0] != null)
        {
            UpdateSVG();
            g.DrawImage(_b[0], destPoints);
        }
        else
        {
            UpdateSVG();
            if (_b[0] != null)
            {
                g.DrawImage(_b[0], destPoints);
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

    public override List<Bitmap> GetImages()
    {
        if (_b == null || _b.Length == 0 || _b[0] == null)
        {
            UpdateSVG();
        }
        List<Bitmap> list = new()
        {
            _b[0] as Bitmap
        };
        return list;
    }

    private void UpdateSVG()
    {
        if (ImportantPoints.Length < 2)
        {
            return;
        }
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        Bitmap bitmap = new(((int)Math.Abs(array[1].X - array[0].X) == 0) ? 1 : ((int)Math.Abs(array[1].X - array[0].X)), ((int)Math.Abs(array[1].Y - array[0].Y) == 0) ? 1 : ((int)Math.Abs(array[1].Y - array[0].Y)));
        Graphics g = Graphics.FromImage(bitmap);
        if (svgObject == null)
        {
            //Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + dllfile);
            //Type type = assembly.GetType(this.type);
            //if (type == null)
            //{
            //	return;
            //}
            //obj = (svgObject = Activator.CreateInstance(type));
        }
        else
        {
        }
        //if (obj != null)
        //{
        //	VectorGraph vectorGraph = obj as VectorGraph;
        //	vectorGraph.DisplaySize = new SizeF((int)Math.Abs(array[1].X - array[0].X), (int)Math.Abs(array[1].Y - array[0].Y));
        //	Scale(g, vectorGraph);
        //	vectorGraph.Paint(g);
        //}
        if (_b[0] != null)
        {
            _b[0].Dispose();
        }
        _b[0] = bitmap;
        if (VMirror)
        {
            _b[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
        }
        if (HMirror)
        {
            _b[0].RotateFlip(RotateFlipType.Rotate180FlipX);
        }
    }

    //private void Scale(Graphics g, VectorGraph vg)
    //{
    //	vg.RateX = vg.DisplaySize.Width / vg.OriginalSize.Width;
    //	vg.RateY = vg.DisplaySize.Height / vg.OriginalSize.Height;
    //	g.ScaleTransform((vg.RateX == 0f) ? 1f : vg.RateX, (vg.RateY == 0f) ? 1f : vg.RateY);
    //}

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
        UpdateSVG();
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
