using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Util;

internal static class TipPainter
{
    private const float HorizontalBorder = 2f;

    private const float VerticalBorder = 1f;

    public static Size GetTipSize(Control control, Graphics graphics, Font font, string description)
    {
        return GetTipSize(control, graphics, new TipText(graphics, font, description));
    }

    private static Rectangle GetWorkingArea(Control control)
    {
        Form form = control.FindForm();
        if (form.Owner != null)
        {
            form = form.Owner;
        }
        return Screen.GetWorkingArea(form);
    }

    public static Size GetTipSize(Control control, Graphics graphics, TipSection tipData)
    {
        Size size = Size.Empty;
        RectangleF rectangleF = GetWorkingArea(control);
        PointF pointF = control.PointToScreen(Point.Empty);
        SizeF maximumSize = new(rectangleF.Right - pointF.X - 4f, rectangleF.Bottom - pointF.Y - 2f);
        if (maximumSize.Width > 0f && maximumSize.Height > 0f)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            tipData.SetMaximumSize(maximumSize);
            SizeF empty = tipData.GetRequiredSize();
            tipData.SetAllocatedSize(empty);
            empty += new SizeF(4f, 2f);
            size = Size.Ceiling(empty);
        }
        if (control.ClientSize != size)
        {
            control.ClientSize = size;
        }
        return size;
    }

    public static Size GetLeftHandSideTipSize(Control control, Graphics graphics, TipSection tipData, Point p)
    {
        Size result = Size.Empty;
        RectangleF rectangleF = GetWorkingArea(control);
        PointF pointF = p;
        SizeF maximumSize = new(pointF.X - 4f, rectangleF.Bottom - pointF.Y - 2f);
        if (maximumSize.Width > 0f && maximumSize.Height > 0f)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            tipData.SetMaximumSize(maximumSize);
            SizeF empty = tipData.GetRequiredSize();
            tipData.SetAllocatedSize(empty);
            empty += new SizeF(4f, 2f);
            result = Size.Ceiling(empty);
        }
        return result;
    }

    public static Size DrawTip(Control control, Graphics graphics, Font font, string description)
    {
        return DrawTip(control, graphics, new TipText(graphics, font, description));
    }

    public static Size DrawTip(Control control, Graphics graphics, TipSection tipData)
    {
        Size size = Size.Empty;
        SizeF sizeF = SizeF.Empty;
        PointF pointF = control.PointToScreen(Point.Empty);
        RectangleF rectangleF = GetWorkingArea(control);
        SizeF maximumSize = new(rectangleF.Right - pointF.X - 4f, rectangleF.Bottom - pointF.Y - 2f);
        if (maximumSize.Width > 0f && maximumSize.Height > 0f)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            tipData.SetMaximumSize(maximumSize);
            sizeF = tipData.GetRequiredSize();
            tipData.SetAllocatedSize(sizeF);
            sizeF += new SizeF(4f, 2f);
            size = Size.Ceiling(sizeF);
        }
        if (control.ClientSize != size)
        {
            control.ClientSize = size;
        }
        if (size != Size.Empty)
        {
            Rectangle rect = new(Point.Empty, size - new Size(1, 1));
            new RectangleF(2f, 1f, sizeF.Width - 4f, sizeF.Height - 2f);
            graphics.DrawRectangle(SystemPens.WindowFrame, rect);
            tipData.Draw(new PointF(2f, 1f));
        }
        return size;
    }

    public static Size DrawFixedWidthTip(Control control, Graphics graphics, TipSection tipData)
    {
        Size size = Size.Empty;
        SizeF sizeF = SizeF.Empty;
        PointF pointF = control.PointToScreen(new Point(control.Width, 0));
        RectangleF rectangleF = GetWorkingArea(control);
        SizeF maximumSize = new(pointF.X - 4f, rectangleF.Bottom - pointF.Y - 2f);
        if (maximumSize.Width > 0f && maximumSize.Height > 0f)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            tipData.SetMaximumSize(maximumSize);
            sizeF = tipData.GetRequiredSize();
            tipData.SetAllocatedSize(sizeF);
            sizeF += new SizeF(4f, 2f);
            size = Size.Ceiling(sizeF);
        }
        if (control.Height != size.Height)
        {
            control.Height = size.Height;
        }
        if (size != Size.Empty)
        {
            Rectangle rect = new(Point.Empty, control.Size - new Size(1, 1));
            new RectangleF(2f, 1f, sizeF.Width - 4f, sizeF.Height - 2f);
            graphics.DrawRectangle(SystemPens.WindowFrame, rect);
            tipData.Draw(new PointF(2f, 1f));
        }
        return size;
    }
}
