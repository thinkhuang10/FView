using CommonSnappableTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
[Guid("14B6FDFA-A3BA-4701-AA70-E455A45A89CB")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CControl : CShape
{
    private delegate Control CallCreatControlDele(Assembly ExtraAssembly);

    private Dictionary<string, object> propertySerializeDict;

    [NonSerialized]
    public static Control UIThreadControl = new();

    [NonSerialized]
    public Control _c = new();

    [NonSerialized]
    public double zoom = 1.0;

    [OptionalField]
    public float DefaultFontSize;

    public byte[] _data;

    public string _dllfile = RuntimeEnvironment.GetRuntimeDirectory() + "System.Windows.Forms.dll";

    public string _RegFile = "";

    public string[] _files = new string[0];

    [NonSerialized]
    public bool error;

    [NonSerialized]
    private bool fackZoom;

    [NonSerialized]
    public bool initLocationErr;

    public string type = "";

    public override PointF Location
    {
        get
        {
            return base.Location;
        }
        set
        {
            base.Location = value;
            UpdateControl();
        }
    }

    public override SizeF Size
    {
        get
        {
            return base.Size;
        }
        set
        {
            base.Size = value;
            UpdateControl();
        }
    }

    public override string Name
    {
        get
        {
            return base.Name;
        }
        set
        {
            base.Name = value;
            if (_c is IControlShape && ((IControlShape)_c).ID != ShapeName)
            {
                ((IControlShape)_c).ID = ShapeName;
            }
        }
    }

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    protected CControl(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CControl info");
        }
        CControl obj = new();
        FieldInfo[] fields = typeof(CControl).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CControl))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "_data")
            {
                _data = (byte[])enumerator.Value;
            }
            else if (enumerator.Name == "_dllfile")
            {
                _dllfile = (string)enumerator.Value;
            }
            else if (enumerator.Name == "_files")
            {
                _files = (string[])enumerator.Value;
            }
            else if (enumerator.Name == "_RegFile")
            {
                _RegFile = (string)enumerator.Value;
            }
            else if (enumerator.Name == "DefaultFontSize")
            {
                DefaultFontSize = (float)enumerator.Value;
            }
            else if (enumerator.Name == "propertySerializeDict")
            {
                propertySerializeDict = (Dictionary<string, object>)enumerator.Value;
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
        info.AddValue("_data", _data);
        info.AddValue("_dllfile", _dllfile);
        info.AddValue("_files", _files);
        info.AddValue("_RegFile", _RegFile);
        info.AddValue("DefaultFontSize", DefaultFontSize);
        info.AddValue("propertySerializeDict", propertySerializeDict);
        info.AddValue("type", type);
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

    public override void BeforeSaveMe()
    {
        fackZoom = true;
        _c.Size = new Size(Convert.ToInt32(Size.Width), Convert.ToInt32(Size.Height));
        _c.Location = new Point(Convert.ToInt32(Location.X), Convert.ToInt32(Location.Y));
        if (_c is IDCCEControl)
        {
            byte[] array = ((IDCCEControl)_c).Serialize();
            if (array != null)
            {
                _data = array;
            }
        }
        else if (_c is AxHost)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new();
            formatter.Serialize(memoryStream, ((AxHost)_c).OcxState);
            memoryStream.Close();
            _data = memoryStream.ToArray();
        }
        DefaultFontSize = _c.Font.Size;
        base.BeforeSaveMe();
        propertySerializeDict = new Dictionary<string, object>();
        Type type = _c.GetType();
        PropertyInfo[] properties = type.GetProperties();
        PropertyInfo[] array2 = properties;
        foreach (PropertyInfo propertyInfo in array2)
        {
            if (!propertyInfo.CanRead || !propertyInfo.CanWrite || !Attribute.IsDefined(propertyInfo.PropertyType, typeof(SerializableAttribute)) || !(propertyInfo.Name != "Instance"))
            {
                continue;
            }
            if (propertyInfo.Name == "Image")
            {
                if (_c is PictureBox)
                {
                    ((PictureBox)_c).Image = null;
                }
                if (_c is Button)
                {
                    ((Button)_c).Image = null;
                }
            }
            try
            {
                if (!Attribute.IsDefined(propertyInfo, typeof(DHMINeedSerPropertyAttribute)) && (!Attribute.IsDefined(propertyInfo, typeof(BrowsableAttribute)) || ((BrowsableAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(BrowsableAttribute))).Browsable) && !propertySerializeDict.ContainsKey(propertyInfo.Name))
                {
                    propertySerializeDict.Add(propertyInfo.Name, propertyInfo.GetValue(_c, null));
                }
            }
            catch (Exception)
            {
            }
        }
        UpdateControl();
        fackZoom = false;
    }

    public override void AfterSaveMe()
    {
        base.AfterSaveMe();
        Type type = _c.GetType();
        PropertyInfo[] properties = type.GetProperties();
        PropertyInfo[] array = properties;
        foreach (PropertyInfo propertyInfo in array)
        {
            if (propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.Name == "Image")
            {
                if (_c is CPictureBox)
                {
                    ((PictureBox)_c).Image = ((CPictureBox)_c).Image.img;
                }
                if (_c is CButton)
                {
                    ((Button)_c).Image = ((CButton)_c).Image.img;
                }
            }
        }
    }

    public void ReLifeMe()
    {
        try
        {
            Assembly assembly;
            try
            {
                string text = _dllfile.Replace("/", "\\");
                if (text.Contains("System.Windows.Forms.dll"))
                {
                    _dllfile = RuntimeEnvironment.GetRuntimeDirectory() + "System.Windows.Forms.dll";
                    assembly = Assembly.Load("System.Windows.Forms");
                }
                else if (text.ToLower().Contains("shaperuntime.dll"))
                {
                    assembly = Assembly.Load("ShapeRuntime");
                }
                else
                {
                    if (!File.Exists(text.Replace("file:///", "")) && File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DHMI.exe"))
                    {
                        _dllfile = AppDomain.CurrentDomain.BaseDirectory + text.Substring((text.IndexOf("UserControl") >= 0) ? text.IndexOf("UserControl") : 0);
                        if (!File.Exists(_dllfile.Replace("file:///", "")))
                        {
                            _dllfile = AppDomain.CurrentDomain.BaseDirectory + text.Substring(text.LastIndexOf("\\") + 1);
                        }
                    }
                    FileInfo fileInfo = new(_dllfile.Replace("file:///", ""));
                    assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + fileInfo.Name);
                }
            }
            catch (Exception)
            {
                assembly = Assembly.LoadFrom(_dllfile.Replace("file:///", ""));
            }
            if (UIThreadControl.InvokeRequired)
            {
                _c = (Control)UIThreadControl.Invoke(new CallCreatControlDele(CallCreatControl), assembly);
            }
            else
            {
                _c = CallCreatControl(assembly);
            }
        }
        catch (Exception)
        {
            error = true;
        }
        UpdateControl();
    }

    private void OnControlSizeChanged(object sender, EventArgs e)
    {
        if (initLocationErr)
        {
            fackZoom = true;
            _c.Size = new Size(Convert.ToInt32((double)Size.Width * zoom), Convert.ToInt32((double)Size.Height * zoom));
            fackZoom = false;
        }
        else if (!fackZoom)
        {
            Size = new Size(Convert.ToInt32((double)_c.Size.Width / zoom), Convert.ToInt32((double)_c.Size.Height / zoom));
        }
    }

    private void OnControlLocationChanged(object sender, EventArgs e)
    {
        if (initLocationErr)
        {
            fackZoom = true;
            _c.Location = new Point(Convert.ToInt32((double)Location.X * zoom), Convert.ToInt32((double)Location.Y * zoom));
            fackZoom = false;
        }
        else if (!fackZoom)
        {
            Location = new Point(Convert.ToInt32((double)_c.Location.X / zoom), Convert.ToInt32((double)_c.Location.Y / zoom));
        }
    }

    private Control CallCreatControl(Assembly ExtraAssembly)
    {
        Type type = ExtraAssembly.GetType(this.type);
        object obj = Activator.CreateInstance(type, BindingFlags.Default, null, null, new CultureInfo(""));
        Control control = obj as Control;
        if (propertySerializeDict == null)
        {
            propertySerializeDict = new Dictionary<string, object>();
        }
        Type type2 = control.GetType();
        PropertyInfo[] properties = type2.GetProperties();
        PropertyInfo[] array = properties;
        foreach (PropertyInfo propertyInfo in array)
        {
            if (!propertyInfo.CanWrite || !propertySerializeDict.ContainsKey(propertyInfo.Name) || propertySerializeDict[propertyInfo.Name] == null || !propertyInfo.PropertyType.IsInstanceOfType(propertySerializeDict[propertyInfo.Name]))
            {
                continue;
            }
            try
            {
                if (!(type2.Name == "CButton") || !(propertyInfo.Name == "BackgroundImage") || propertySerializeDict["BackgroundImage"] is not Image)
                {
                    goto IL_016d;
                }
                if (propertySerializeDict.ContainsKey("Image") && propertySerializeDict["Image"] != null)
                {
                    propertySerializeDict["BackgroundImage"] = null;
                    continue;
                }
                Image value = propertySerializeDict["BackgroundImage"] as Image;
                propertySerializeDict["Image"] = value;
                propertySerializeDict["BackgroundImage"] = null;
                goto IL_016d;
            IL_016d:
                propertyInfo.SetValue(control, propertySerializeDict[propertyInfo.Name], null);
            }
            catch
            {
            }
        }
        if (control is IDCCEControl)
        {
            ((IDCCEControl)control).DeSerialize(_data);
        }
        else if (control is AxHost)
        {
            BinaryFormatter binaryFormatter = new();
            byte[] data = _data;
            Stream stream = new MemoryStream(data);
            ((AxHost)control).BeginInit();
            ((AxHost)control).OcxState = (AxHost.State)binaryFormatter.UnsafeDeserialize(stream, null);
            ((AxHost)control).EndInit();
            stream.Close();
        }
        control.LocationChanged += OnControlLocationChanged;
        control.SizeChanged += OnControlSizeChanged;
        if (control is IControlShape)
        {
            IControlShape controlShape = control as IControlShape;
            controlShape.ID = ShapeName;
            controlShape.IDChanged += OnControlIDChanged;
        }
        return control;
    }

    private void OnControlIDChanged(object sender, EventArgs e)
    {
        if (sender is IControlShape)
        {
            IControlShape controlShape = sender as IControlShape;
            Name = controlShape.ID;
        }
    }

    public override void AfterLoadMe()
    {
        if (new FileInfo(_dllfile).Name.ToLower() == "shaperuntime.dll")
        {
            _dllfile = AppDomain.CurrentDomain.BaseDirectory + "ShapeRuntime.dll";
        }
        if (zoom == 0.0)
        {
            zoom = 1.0;
        }
        base.AfterLoadMe();
        ReLifeMe();
        _Pen = new Pen(Color.Transparent);
        _Brush = new SolidBrush(Color.Transparent);
    }

    public CControl()
    {
        ShapeName = "Control" + CShape.SumLayer;
        _Pen = new Pen(Color.Transparent);
        _Brush = new SolidBrush(Color.Transparent);
    }

    public override CShape Copy()
    {
        CControl cControl = new();
        Type type = cControl.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cControl, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cControl, fieldInfo.GetValue(this));
            }
        }
        cControl.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cControl.ShapeName = ShapeName;
        new List<List<int>>();
        cControl.ShapeID = ShapeID;
        cControl.RotateAngel = RotateAngel;
        cControl.RotateAtPoint = RotateAtPoint;
        cControl.TranslateMatrix = TranslateMatrix.Clone();
        cControl._Pen = (Pen)_Pen.Clone();
        cControl._Brush = (Brush)_Brush.Clone();
        cControl.type = this.type;
        cControl._b = (Bitmap[])_b.Clone();
        cControl._dllfile = _dllfile;
        cControl._c = _c;
        cControl.zoom = zoom;
        return cControl;
    }

    public override CShape clone()
    {
        CControl cControl = new();
        Type type = cControl.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cControl, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cControl, fieldInfo.GetValue(this));
            }
        }
        cControl.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cControl.ShapeName = ShapeName;
        new List<List<int>>();
        cControl.ShapeID = ShapeID;
        cControl.RotateAngel = RotateAngel;
        cControl.RotateAtPoint = RotateAtPoint;
        cControl.TranslateMatrix = TranslateMatrix.Clone();
        cControl._Pen = (Pen)_Pen.Clone();
        cControl._Brush = (Brush)_Brush.Clone();
        cControl.type = this.type;
        cControl._b = (Bitmap[])_b.Clone();
        cControl._dllfile = _dllfile;
        cControl.zoom = zoom;
        cControl.Layer = CShape.SumLayer++;
        cControl.locked = false;
        BeforeSaveMe();
        AfterSaveMe();
        cControl._data = _data;
        cControl.ReLifeMe();
        return cControl;
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
            string text = _dllfile.Replace("/", "\\");
            if (!File.Exists(text.Replace("file:///", "")) && File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DHMI.exe"))
            {
                _dllfile = AppDomain.CurrentDomain.BaseDirectory + text.Substring(text.IndexOf("UserControl"));
                if (!File.Exists(text.Replace("file:///", "")))
                {
                    _dllfile = AppDomain.CurrentDomain.BaseDirectory + text.Substring(text.IndexOf("UserControl"));
                }
                if (!File.Exists(text.Replace("file:///", "")))
                {
                    _dllfile = AppDomain.CurrentDomain.BaseDirectory + text.Substring(text.IndexOf("UserControl"));
                }
            }
            Assembly assembly = Assembly.LoadFrom(_dllfile);
            Type type = assembly.GetType(this.type);
            object obj = Activator.CreateInstance(type, BindingFlags.Default, null, null, new CultureInfo(""));
            _c = obj as Control;
            _c.LocationChanged += OnControlLocationChanged;
            _c.SizeChanged += OnControlSizeChanged;
            if (_c is IControlShape)
            {
                IControlShape controlShape = _c as IControlShape;
                controlShape.ID = ShapeName;
                controlShape.IDChanged += OnControlIDChanged;
            }
            float num = ((_c.Size.Width > 1024) ? 1024 : _c.Size.Width);
            float num2 = num / (float)_c.Size.Width;
            AddPoint(new PointF(ImportantPoints[8].X + num2 * (float)_c.Size.Width, ImportantPoints[8].Y + num2 * (float)_c.Size.Height));
            if (_c is ListBox)
            {
                ((ListBox)_c).IntegralHeight = false;
            }
            return true;
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
            UpdateControl();
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
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        graphicsPath.AddRectangle(new RectangleF(Math.Min(array[0].X, array[1].X) - 2f, Math.Min(array[0].Y, array[1].Y) - 2f, Math.Abs(array[1].X - array[0].X) + 4f, Math.Abs(array[1].Y - array[0].Y) + 4f));
        graphicsPath.Transform(TranslateMatrix);
        swapgp = graphicsPath;
        needRefreshShape = false;
        UpdateControl();
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
        graphicsPath.AddEllipse(new RectangleF(array[0].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[0].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[1].X - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddEllipse(new RectangleF(array[1].X - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[1].Y - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[1].X - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.AddRectangle(new RectangleF(array[0].X / 2f + array[1].X / 2f - 3f, array[0].Y / 2f + array[1].Y / 2f - 3f, 7f, 7f));
        graphicsPath.Transform(TranslateMatrix);
        g.FillPath(Brushes.White, graphicsPath);
        g.DrawPath(Pens.Blue, graphicsPath);
        return result;
    }

    public override bool EditLocation(PointF OldPoint, PointF NewPoint)
    {
        if (locked && Operation.bEditEnvironment)
        {
            return false;
        }
        PointF location = Location;
        SizeF size = Size;
        float angel = base.Angel;
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
        if (Point.Round(Location) != Point.Round(location) || System.Drawing.Size.Round(Size) != System.Drawing.Size.Round(size) || base.Angel != angel)
        {
            NeedRefreshShape = true;
        }
        return true;
    }

    public void UpdateControl()
    {
        if (_c != null)
        {
            fackZoom = true;
            _c.Size = new Size(Convert.ToInt32((double)Size.Width * zoom), Convert.ToInt32((double)Size.Height * zoom));
            _c.Location = new Point(Convert.ToInt32((double)Location.X * zoom), Convert.ToInt32((double)Location.Y * zoom));
            fackZoom = false;
        }
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
