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
[Guid("0F48C10C-5118-40d5-8A43-F7CFD1406C4D")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class CCloseLines : CShape, ISupportHtml5
{
    [NonSerialized]
    private GraphicsPath swapgp0;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    protected CCloseLines(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CCloseLines info");
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    public CCloseLines()
    {
        ShapeName = "CloseLines" + CShape.SumLayer;
    }

    public override CShape Copy()
    {
        CCloseLines cCloseLines = new();
        Type type = cCloseLines.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                object value = fieldInfo.GetValue(this);
                if (value != null)
                {
                    fieldInfo.SetValue(cCloseLines, fieldInfo.FieldType.GetMethod("Clone").Invoke(fieldInfo.GetValue(this), null));
                }
            }
            else
            {
                fieldInfo.SetValue(cCloseLines, fieldInfo.GetValue(this));
            }
        }
        cCloseLines.FillAngel = FillAngel;
        cCloseLines.ImportantPoints = (PointF[])ImportantPoints.Clone();
        cCloseLines.ShapeName = ShapeName;
        cCloseLines.ShapeID = ShapeID;
        cCloseLines.RotateAngel = RotateAngel;
        cCloseLines.RotateAtPoint = RotateAtPoint;
        cCloseLines.TranslateMatrix = TranslateMatrix.Clone();
        cCloseLines._Pen = (Pen)_Pen.Clone();
        cCloseLines._Brush = (Brush)_Brush.Clone();
        cCloseLines.FillAngel = FillAngel;
        for (int j = 0; j < DelRegionShape.Count; j++)
        {
            cCloseLines.DelRegionShape.Add(DelRegionShape[j].Copy());
            cCloseLines.delimportant00points.Add(delimportant00points[j]);
            cCloseLines.delimportant11points.Add(delimportant11points[j]);
        }
        return cCloseLines;
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
            swapgp0 = swapgp;
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
        PointF[] array2 = new PointF[array.Length - 8];
        for (int i = 0; i < array2.Length; i++)
        {
            ref PointF reference = ref array2[i];
            reference = array[i + 8];
        }
        graphicsPath.AddLines(array2);
        graphicsPath.CloseFigure();
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
        swapgp0 = swapgp;
        needRefreshShape = false;
        list.Add(graphicsPath);
        return list;
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
        for (int i = 8; i < array.Length; i++)
        {
            graphicsPath.AddRectangle(new RectangleF(array[i].X - 3f, array[i].Y - 3f, 7f, 7f));
        }
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
        if (Location != location || Size != size || base.Angel != angel)
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
        for (int k = 0; k < array.Length; k++)
        {
            if (array[k] != ImportantPoints[k])
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
        stringBuilder.Append("<canvas id=\"" + ShapeName + "\" onclick=\"_onclick('" + ShapeName + "');\" style=\"z-index:" + base.Lay + ";display:inline; position:absolute; left:" + Location.X + "px; top:" + Location.Y + "px;\" width=\"" + (double)Width * 1.005 + "px\" height=\"" + (double)Height * 1.001 + "px\" ></canvas>");
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
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
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
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_gdbhY\")+$(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\") < get_Y(\"" + ShapeName + "\")+get_Height(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Height(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_gdbh_Heigth\")+$(\"#" + ShapeName + "\").data(\"set_gdbhY\")));");
                stringBuilder.AppendLine("set_Y(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
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
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
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
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("if($(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\") < get_X(\"" + ShapeName + "\")+get_Width(\"" + ShapeName + "\"))");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("set_Width(\"" + ShapeName + "\", ($(\"#" + ShapeName + "\").data(\"set_kdbhX_Width\")+$(\"#" + ShapeName + "\").data(\"set_kdbh_Width\")));");
                stringBuilder.AppendLine("set_X(\"" + ShapeName + "\", 0);");
                stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
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

    public string makeScript()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Color1\",\"" + ColorTranslator.ToHtml(Color1) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Color2\",\"" + ColorTranslator.ToHtml(Color2) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"FillStyle\",\"" + BrushStyle.ToString() + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"LineThick\",\"" + _Pen.Width + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"LineColor\",\"" + ColorTranslator.ToHtml(PenColor) + "\");");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_LineColor\",function () {return get_line_LineColor(\"" + ShapeName + "\");});");
        if (BrushStyle == _BrushStyle.线性渐变填充)
        {
            stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"FillAngle\"," + FillAngel + ");");
        }
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_X\",function () {return get_line_X(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Y\",function () {return get_line_Y(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Color1\",function () {return get_Ellipse_Color1(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Color2\",function () {return get_Ellipse_Color2(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Width\",function () {return get_Width(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Height\",function () {return get_Height(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"get_Visible\",function () {return get_line_Visible(\"" + ShapeName + "\");});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_LineColor\",function (value) {set_line_LineColor(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Color1\",function (value) {set_line_Color1(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Color2\",function (value) {set_line_Color2(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_X\",function (value) {set_line_X(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Y\",function (value) {set_line_Y(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Width\",function (value) {set_line_Width(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Height\",function (value) {set_line_Height(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Visible\",function (value) {set_line_Visible(\"" + ShapeName + "\",value);});");
        stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"set_Visible\",function (value) {set_line_Visible(\"" + ShapeName + "\",value);});");
        Pass_ClosePionts(stringBuilder);
        stringBuilder.AppendLine("Closelines_Draw(\"" + ShapeName + "\"," + ShapeName + "_Point);");
        if (eventBindDict != null)
        {
            stringBuilder.AppendLine(MakeScriptForShape());
        }
        return stringBuilder.ToString();
    }

    public string makeStyle()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("");
        return stringBuilder.ToString();
    }

    public void Pass_ClosePionts(StringBuilder sb)
    {
        List<GraphicsPath> list = new();
        if (swapgp != null && !needRefreshShape)
        {
            list.Add(swapgp);
            swapgp0 = swapgp;
        }
        else
        {
            GraphicsPath graphicsPath = new();
            if (ImportantPoints.Length < 10)
            {
                graphicsPath.AddLine(ImportantPoints[8], ImportantPoints[9]);
            }
            list.Add(graphicsPath);
            swapgp0 = graphicsPath;
        }
        sb.AppendLine("var  " + ShapeName + "_Point = new Array ()");
        if (swapgp0 != null)
        {
            for (int i = 0; i < swapgp0.PathData.Points.Length; i++)
            {
                sb.AppendLine(ShapeName + "_Point[" + i + "]= new  Array() ;");
                sb.AppendLine(ShapeName + "_Point[" + i + "][0]=" + swapgp0.PathData.Points[i].X);
                sb.AppendLine(ShapeName + "_Point[" + i + "][1]=" + swapgp0.PathData.Points[i].Y);
            }
        }
    }
}
