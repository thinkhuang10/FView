using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("F23E8E0F-DBCF-4f96-8E95-6A98C3A15E6E")]
public class CPixieControl : CShape
{
    public byte[] DATA;

    public string _dllfile = "";

    public string[] _files = new string[0];

    [NonSerialized]
    public bool isRunning;

    [NonSerialized]
    private object[] evdata;

    public string type = "";

    [Browsable(false)]
    [DHMIHideProperty]
    public override Color PenColor
    {
        get
        {
            return base.PenColor;
        }
        set
        {
            base.PenColor = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public override DashStyle PenStyle
    {
        get
        {
            return base.PenStyle;
        }
        set
        {
            base.PenStyle = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public override float PenWidth
    {
        get
        {
            return base.PenWidth;
        }
        set
        {
            base.PenWidth = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public override float BrushBFB
    {
        get
        {
            return base.BrushBFB;
        }
        set
        {
            base.BrushBFB = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public override _BrushStyle BrushStyle
    {
        get
        {
            return base.BrushStyle;
        }
        set
        {
            base.BrushStyle = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public override Color Color1
    {
        get
        {
            return base.Color1;
        }
        set
        {
            base.Color1 = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public override Color Color2
    {
        get
        {
            return base.Color1;
        }
        set
        {
            base.Color1 = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public override float FillAngel
    {
        get
        {
            return base.FillAngel;
        }
        set
        {
            base.FillAngel = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public override HatchStyle HatchStyle
    {
        get
        {
            return base.HatchStyle;
        }
        set
        {
            base.HatchStyle = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public override int Opacity
    {
        get
        {
            return base.Opacity;
        }
        set
        {
            base.Opacity = value;
        }
    }

    public event ShapeRuntimeGetVarTable OnGetVarTable;

    public event ValidateVarName ValidateVar;

    public event ShapeGetValue GetValueEvent;

    public event ShapeSetValue SetValueEvent;

    protected CPixieControl(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CPixieControl info");
        }
        CPixieControl obj = new();
        FieldInfo[] fields = typeof(CPixieControl).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CPixieControl))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "_dllfile")
            {
                _dllfile = (string)enumerator.Value;
            }
            else if (enumerator.Name == "_files")
            {
                _files = (string[])enumerator.Value;
            }
            else if (enumerator.Name == "DATA")
            {
                DATA = (byte[])enumerator.Value;
            }
            else if (enumerator.Name == "type")
            {
                type = (string)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_dllfile", _dllfile);
        info.AddValue("_files", _files);
        info.AddValue("DATA", DATA);
        info.AddValue("type", type);
    }

    public string GetVarNames()
    {
        if (OnGetVarTable != null)
        {
            return OnGetVarTable("");
        }
        return null;
    }

    public bool ValidateVarName(string varName)
    {
        if (ValidateVar != null)
        {
            return ValidateVar(varName);
        }
        return false;
    }

    public virtual void RefreshControl()
    {
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

    public void OnMouseDown(object sender, MouseEventArgs e)
    {
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[1] { e.Location };
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        matrix.TransformPoints(array2);
        matrix.Dispose();
        MouseEventArgs e2 = new(e.Button, e.Clicks, Math.Abs(Convert.ToInt32(array2[0].X - array[0].X)), Math.Abs(Convert.ToInt32(array2[0].Y - array[0].Y)), e.Delta);
        ManageMouseDown(e2);
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[1] { e.Location };
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        matrix.TransformPoints(array2);
        matrix.Dispose();
        MouseEventArgs e2 = new(e.Button, e.Clicks, Math.Abs(Convert.ToInt32(array2[0].X - array[0].X)), Math.Abs(Convert.ToInt32(array2[0].Y - array[0].Y)), e.Delta);
        ManageMouseMove(e2);
    }

    public override List<Bitmap> GetImages()
    {
        RefreshControl();
        if (_b[0] != null && _b[0] is Bitmap)
        {
            List<Bitmap> list = new()
            {
                _b[0] as Bitmap
            };
            return list;
        }
        return new List<Bitmap>();
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[1] { e.Location };
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        matrix.TransformPoints(array2);
        matrix.Dispose();
        MouseEventArgs e2 = new(e.Button, e.Clicks, Math.Abs(Convert.ToInt32(array2[0].X - array[0].X)), Math.Abs(Convert.ToInt32(array2[0].Y - array[0].Y)), e.Delta);
        ManageMouseUp(e2);
    }

    public virtual void ManageMouseDown(MouseEventArgs e)
    {
    }

    public virtual void ManageMouseMove(MouseEventArgs e)
    {
    }

    public virtual void ManageMouseUp(MouseEventArgs e)
    {
    }

    public void ShowPropertyDialog()
    {
        if (!isRunning)
        {
            ShowDialog();
        }
    }

    public virtual void ShowDialog()
    {
    }

    public virtual byte[] Serialize()
    {
        return null;
    }

    public virtual void Deserialize(byte[] data)
    {
    }

    public object GetValue(string name)
    {
        if (!isRunning)
        {
            return null;
        }
        if (GetValueEvent != null)
        {
            return GetValueEvent(name);
        }
        return null;
    }

    public void SetValue(string name, object value)
    {
        if (isRunning && SetValueEvent != null)
        {
            SetValueEvent(name, value);
        }
    }

    public void FinishRefresh(Bitmap b)
    {
        _b[0]?.Dispose();
        _b[0] = b;
        if (VMirror)
        {
            _b[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
        }
        if (HMirror)
        {
            _b[0].RotateFlip(RotateFlipType.Rotate180FlipX);
        }
    }

    public void DllCopyTo(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string[] files = _files;
        foreach (string text in files)
        {
            try
            {
                FileInfo fileInfo = new(text.Replace("file:///", ""));
                fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileInfo.Name);
                File.Copy(fileInfo.FullName, dir + fileInfo.Name, overwrite: true);
            }
            catch
            {
            }
        }
        try
        {
            FileInfo fileInfo2 = new(_dllfile.Replace("file:///", ""));
            fileInfo2 = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileInfo2.Name);
            if (fileInfo2.Name.ToLower() != "system.windows.forms.dll" && fileInfo2.Name.ToLower() != "shaperuntime.dll")
            {
                File.Copy(fileInfo2.FullName, dir + fileInfo2.Name, overwrite: true);
            }
        }
        catch
        {
        }
    }

    public CPixieControl()
    {
        ShapeName = "PixieControl" + CShape.SumLayer;
        _b = new Bitmap[1];
    }

    public void clearEvent()
    {
        OnGetVarTable = null;
        ValidateVar = null;
    }

    public void backupEvent()
    {
        evdata = new object[2];
        evdata[0] = OnGetVarTable;
        evdata[1] = ValidateVar;
        OnGetVarTable = null;
        ValidateVar = null;
    }

    public override void BeforeSaveMe()
    {
        DATA = Serialize();
        base.BeforeSaveMe();
    }

    public void resumeevent()
    {
        if (evdata != null && evdata.Length >= 2)
        {
            OnGetVarTable = (ShapeRuntimeGetVarTable)evdata[0];
            ValidateVar = (ValidateVarName)evdata[1];
            evdata[0] = null;
            evdata[1] = null;
        }
    }

    public override void AfterLoadMe()
    {
        base.AfterLoadMe();
        Deserialize(DATA);
    }

    public override CShape Copy()
    {
        BeforeSaveMe();
        AfterSaveMe();
        Type type = GetType();
        CPixieControl cPixieControl = (CPixieControl)Activator.CreateInstance(type);
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cPixieControl, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cPixieControl, fieldInfo.GetValue(this));
            }
        }
        cPixieControl._dllfile = _dllfile;
        cPixieControl.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cPixieControl.ShapeName = ShapeName;
        new List<List<int>>();
        cPixieControl.ShapeID = ShapeID;
        cPixieControl.RotateAngel = RotateAngel;
        cPixieControl.RotateAtPoint = RotateAtPoint;
        cPixieControl.TranslateMatrix = TranslateMatrix.Clone();
        cPixieControl._Pen = (Pen)_Pen.Clone();
        cPixieControl._Brush = (Brush)_Brush.Clone();
        cPixieControl.type = this.type;
        cPixieControl._b = (Bitmap[])_b.Clone();
        cPixieControl.OnGetVarTable += OnGetVarTable;
        cPixieControl.ValidateVar += ValidateVar;
        cPixieControl.AfterLoadMe();
        return cPixieControl;
    }

    public override bool AddPoint(PointF NewPoint)
    {
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
            RefreshControl();
            float num = 200f;
            float num2 = 200f;
            float num3 = 1f;
            if (_b[0] != null)
            {
                num3 = 200f / (float)_b[0].Width;
                num = _b[0].Width;
                num2 = _b[0].Height;
            }
            AddPoint(new PointF(ImportantPoints[8].X + num3 * num, ImportantPoints[8].Y + num3 * num2));
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
            UpdateSVG();
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
        return list;
    }

    public override bool DrawMe(Graphics g)
    {
        try
        {
            new GraphicsPath();
            if (ImportantPoints.Length < 2)
            {
                return false;
            }
            RefreshControl();
            PointF[] destPoints = new PointF[3]
            {
                ImportantPoints[0],
                ImportantPoints[2],
                ImportantPoints[3]
            };
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
        new GraphicsPath();
        if (ImportantPoints.Length < 2)
        {
            return false;
        }
        PointF[] destPoints = new PointF[3]
        {
            ImportantPoints[0],
            ImportantPoints[2],
            ImportantPoints[3]
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
        return true;
    }

    private void UpdateSVG()
    {
        _ = ImportantPoints.Length;
        _ = 2;
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
        UpdateSVG();
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[2] { OldPoint, NewPoint };
        PointF drotateatnangel = default;
        bool flag = false;
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(array);
        TranslateMatrix.TransformPoints(array2);
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        PartEditPoint(ref r, array, array2, ref drotateatnangel);
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
            return r;
        }
        if (r == 55 || (r == -1 && OldPoint.X + 5f > RotateAtPoint.X && OldPoint.X - 5f < RotateAtPoint.X && OldPoint.Y + 5f > RotateAtPoint.Y && OldPoint.Y - 5f < RotateAtPoint.Y))
        {
            r = 55;
            RotateAtPoint.X += NewPoint.X - OldPoint.X;
            RotateAtPoint.Y += NewPoint.Y - OldPoint.Y;
            flag = true;
        }
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
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, point);
        PointF[] array3 = array;
        int num = 0;
        while (true)
        {
            if (num < array3.Length)
            {
                PointF pointF = array3[num];
                if (float.IsNaN(pointF.X) || float.IsNaN(pointF.Y))
                {
                    break;
                }
                num++;
                continue;
            }
            if (!(Math.Abs(array[0].X - array[1].X) < 2f) && !(Math.Abs(array[0].Y - array[1].Y) < 2f))
            {
                TranslateMatrix.TransformPoints(array);
                ImportantPoints = (PointF[])array.Clone();
                if (!flag)
                {
                    PointF[] array4 = new PointF[1]
                    {
                        new(drotateatnangel.X + RotateAtPoint.X, drotateatnangel.Y + RotateAtPoint.Y)
                    };
                    TranslateMatrix.TransformPoints(array4);
                    RotateAtPoint = array4[0];
                }
            }
            break;
        }
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        return r;
    }
}
