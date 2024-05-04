using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonSnappableTypes;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using HMIEditEnvironment.Properties;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class UserShapeEditControl : UserControl
{
	public List<DataFile> dfs;

	private Rectangle dragDropRect = new Rectangle(1, 1, 1, 1);

	private Rectangle dragRightDropRect = new Rectangle(1, 1, 1, 1);

	public Size OldSize;

	private Bitmap graphicsBuffer;

	private Point pDrawStartPoint = default;

	public bool backgroundReDraw;

	private Bitmap backgroundImageResult;

	public CGlobal theglobal;

	private Point BeginDrawShapeFirstPoint;

	private SolidBrush otherBrush = new SolidBrush(Color.FromArgb(100, 192, 255, 255));

	private Pen otherPen0 = new Pen(Color.White, 0f);

	private Pen otherPen1 = new Pen(Color.SkyBlue, 0f);

	private IContainer components;

	public PopupMenu popupMenu1;

	private BarButtonItem barButtonItem1;

	private BarButtonItem barButtonItem3;

	private BarButtonItem barButtonItem4;

	private BarButtonItem barButtonItem5;

	private BarSubItem barSubItem1;

	private BarEditItem barEditItemControlName;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private BarSubItem barSubItem2;

	private BarSubItem barSubItem3;

	private BarButtonItem barButtonItem10;

	private BarButtonItem barButtonItem11;

	private BarManager barManager1;

	private BarDockControl barDockControlTop;

	private BarDockControl barDockControlBottom;

	private BarDockControl barDockControlLeft;

	private BarDockControl barDockControlRight;

	private BarButtonItem barButtonItem6;

	private BarButtonItem barButtonItem7;

	private BarButtonItem barButtonItem12;

	private BarEditItem barEditItem2;

	private RepositoryItemColorEdit repositoryItemColorEdit1;

	private BarEditItem barEditItem3;

	private RepositoryItemColorEdit repositoryItemColorEdit2;

	private BarButtonItem barButtonItem13;

	private BarButtonItem barButtonItem14;

	private BarButtonItem barButtonItem15;

	private BarButtonItem barButtonItem16;

	private BarButtonItem barButtonItem17;

	private BarButtonItem barButtonItem18;

	private BarButtonItem barButtonItem19;

	private BarButtonItem barButtonItem20;

	private BarButtonItem barButtonItem21;

	private BarButtonItem barButtonItem22;

	private BarButtonItem barButtonItem23;

	private BarSubItem barSubItem5;

	private BarButtonItem barButtonItem24;

	private BarButtonItem barButtonItem25;

	private BarSubItem barSubItem6;

	private BarButtonItem barButtonItem27;

	private BarButtonItem barButtonItem28;

	private BarButtonItem barButtonItem29;

	private BarButtonItem barButtonItem30;

	private ColorEditControl colorEditControl1;

	private BarButtonItem barButtonItem8;

	private BarButtonItem barButtonItem_Delete;

	private void UserControl2_Load(object sender, EventArgs e)
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		UpdateStyles();
		float[] dashPattern = new float[2] { 3f, 4f };
		otherPen1.DashPattern = dashPattern;
		if (!theglobal.locked)
		{
			theglobal.uc1.ReFreshEnable();
			OldSize = base.Size;
			RefreshGraphics();
		}
	}

	protected override void OnPaintBackground(PaintEventArgs e)
	{
		try
		{
			if (backgroundImageResult == null)
			{
				backgroundImageResult = new Bitmap(base.Width, base.Height);
				PaintEventArgs e2 = new PaintEventArgs(Graphics.FromImage(backgroundImageResult), new Rectangle(0, 0, base.Width, base.Height));
				base.OnPaintBackground(e2);
			}
			else if (backgroundReDraw || backgroundImageResult.Width != base.Width || backgroundImageResult.Height != base.Height)
			{
				backgroundImageResult.Dispose();
				backgroundImageResult = new Bitmap(base.Width, base.Height);
				PaintEventArgs e3 = new PaintEventArgs(Graphics.FromImage(backgroundImageResult), e.ClipRectangle);
				base.OnPaintBackground(e3);
				backgroundReDraw = false;
			}
		}
		catch
		{
			MessageBox.Show("背景窗体绘制出现异常！", "提示");
		}
	}

	protected override bool IsInputKey(Keys keyData)
	{
		if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
		{
			return true;
		}
		return false;
	}

	private void UserControl2_Paint(object sender, PaintEventArgs e)
	{
		if (graphicsBuffer == null)
		{
			RefreshGraphics();
		}
		if (backgroundImageResult != null)
		{
			e.Graphics.DrawImageUnscaled(backgroundImageResult, Point.Empty);
		}
		e.Graphics.DrawImageUnscaled(graphicsBuffer, Point.Empty);
		e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
		e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
		if (!theglobal.locked)
		{
			foreach (CShape selectedShape in theglobal.SelectedShapeList)
			{
				selectedShape.DrawMe(e.Graphics);
				selectedShape.DrawSelect(e.Graphics);
			}
			if (theglobal.str_IMDoingWhat == "BeginDrawShape" && BeginDrawShapeFirstPoint != Point.Empty)
			{
				Pen pen = new Pen(Color.Gray, 1.5f);
				float[] dashPattern = new float[2] { 1f, 2f };
				pen.DashPattern = dashPattern;
				if (theglobal.g_ListAllShowCShape[theglobal.g_ListAllShowCShape.Count - 1] is CLine)
				{
					e.Graphics.DrawLine(pen, pDrawStartPoint, BeginDrawShapeFirstPoint);
				}
				else
				{
					e.Graphics.DrawRectangle(pen, Math.Min(pDrawStartPoint.X, BeginDrawShapeFirstPoint.X), Math.Min(pDrawStartPoint.Y, BeginDrawShapeFirstPoint.Y), Math.Abs(BeginDrawShapeFirstPoint.X - pDrawStartPoint.X), Math.Abs(BeginDrawShapeFirstPoint.Y - pDrawStartPoint.Y));
				}
			}
			if (theglobal.str_IMDoingWhat == "DragDrop")
			{
				e.Graphics.FillRectangle(otherBrush, dragDropRect.X + 2, dragDropRect.Y + 2, dragDropRect.Width - 4, dragDropRect.Height - 4);
				e.Graphics.DrawRectangle(otherPen0, dragDropRect);
				e.Graphics.DrawRectangle(otherPen1, dragDropRect);
			}
		}
		if (theglobal.str_IMDoingWhat == "MSelect")
		{
			otherBrush = new SolidBrush(Color.FromArgb(100, 192, 255, 255));
			e.Graphics.FillRectangle(otherBrush, Math.Min(theglobal.lastmouseeventargs.X, theglobal.mselectfirstp.X) + 2f, Math.Min(theglobal.lastmouseeventargs.Y, theglobal.mselectfirstp.Y) + 2f, Math.Abs(theglobal.mselectfirstp.X - (float)theglobal.lastmouseeventargs.X) - 4f, Math.Abs(theglobal.mselectfirstp.Y - (float)theglobal.lastmouseeventargs.Y) - 4f);
			e.Graphics.DrawRectangle(otherPen0, Math.Min(theglobal.lastmouseeventargs.X, theglobal.mselectfirstp.X), Math.Min(theglobal.lastmouseeventargs.Y, theglobal.mselectfirstp.Y), Math.Abs(theglobal.mselectfirstp.X - (float)theglobal.lastmouseeventargs.X), Math.Abs(theglobal.mselectfirstp.Y - (float)theglobal.lastmouseeventargs.Y));
			e.Graphics.DrawRectangle(otherPen1, Math.Min(theglobal.lastmouseeventargs.X, theglobal.mselectfirstp.X), Math.Min(theglobal.lastmouseeventargs.Y, theglobal.mselectfirstp.Y), Math.Abs(theglobal.mselectfirstp.X - (float)theglobal.lastmouseeventargs.X), Math.Abs(theglobal.mselectfirstp.Y - (float)theglobal.lastmouseeventargs.Y));
		}
		e.Dispose();
	}

	private void UserControl2_DoubleClick(object sender, EventArgs e)
	{
		if (theglobal.locked || theglobal.SelectedShapeList.Count == 0)
		{
			return;
		}
		CShape[] array = theglobal.SelectedShapeList.ToArray();
		foreach (CShape cShape in array)
		{
			if (cShape is CControl)
			{
				CControl cControl = (CControl)cShape;
				string[] array2 = cControl.type.Split('.');
				if ("ShapeRuntime" == array2[0])
				{
					barButtonItem1_ItemClick(null, null);
					continue;
				}
				cControl._c.Enabled = true;
				theglobal.OldShape = cControl;
				theglobal.str_IMDoingWhat = "ControlTrue";
			}
			else if (cShape is CPixieControl)
			{
				barButtonItem8_ItemClick(null, null);
			}
			else if (cShape is CPicture)
			{
				barButtonItem1_ItemClick(null, null);
			}
			else
			{
				barButtonItem1_ItemClick(null, null);
			}
		}
	}

	private void UserControl2_KeyDown(object sender, KeyEventArgs e)
	{
		new List<CShape>(theglobal.SelectedShapeList.ToArray());
		new List<CShape>();
		if (!theglobal.locked)
		{
			if (e.KeyCode == Keys.Up)
			{
				KeyUpFunc();
			}
			if (e.KeyCode == Keys.Down)
			{
				KeyDownFunc();
			}
			if (e.KeyCode == Keys.Left)
			{
				KeyLeftFunc();
			}
			if (e.KeyCode == Keys.Right)
			{
				KeyRightFunc();
			}
			if (theglobal.theform is ChildForm)
			{
				CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
			}
			else if (theglobal.SelectedShapeList.Count != 0)
			{
				CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
			}
			theglobal.uc1.ReFreshEnable();
		}
	}

	private void UserControl2_MouseDown(object sender, MouseEventArgs e)
	{
		CEditEnvironmentGlobal.UnSelectAllShapesIfNotSender(this);
		e = new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X), Convert.ToInt32(e.Y), e.Delta);
		theglobal.lastmouseeventargs = e;
		theglobal.theform.BringToFront();
		CEditEnvironmentGlobal.mdiparent.activeform(theglobal.theform);
		if (theglobal.str_IMDoingWhat == "BeginDrawShape")
		{
			CShape cShape = theglobal.g_ListAllShowCShape[theglobal.g_ListAllShowCShape.Count - 1];
			if (cShape is CRectangle || cShape is CEllipse || cShape is CLine || cShape is CString || cShape is CCircleRect)
			{
				BeginDrawShapeFirstPoint = e.Location;
				cShape.AddPoint(e.Location);
				CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
			}
		}
		if (!(theglobal.str_IMDoingWhat == "Select"))
		{
			return;
		}
		foreach (CShape item in theglobal.g_ListAllShowCShape)
		{
			if ((!item.locked && item.MouseOnMe(e.Location)) || item.EditPoint(e.Location, e.Location, -1) != -1 || item.EditLocation(e.Location, e.Location))
			{
				return;
			}
		}
		theglobal.SelectedShapeList.Clear();
		RefreshGraphics();
		theglobal.str_IMDoingWhat = "MSelect";
		theglobal.mselectfirstp = e.Location;
		CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
	}

	public void UserControl2_MouseMove(object sender, MouseEventArgs e)
	{
		if (theglobal.locked || e.Button == MouseButtons.Right || !MouseMove_RecordMousePoint(e) || !MouseMove_ShowCursorsStatus(e))
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			SuspendLayout();
			if (!MouseMove_OperateEditPoint(e) || !MouseMove_BeforeDragSelected(e) || !MouseMove_EditPosition(e))
			{
				return;
			}
			ResumeLayout();
			Invalidate(invalidateChildren: false);
			Update();
		}
		theglobal.lastmouseeventargs = e;
	}

	private void UserControl2_MouseUp(object sender, MouseEventArgs e)
	{
		if (theglobal.locked)
		{
			return;
		}
		e = new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X), Convert.ToInt32(e.Y), e.Delta);
		theglobal.lastmouseeventargs = e;
		if (e.Button == MouseButtons.Right)
		{
			bool flag = false;
			foreach (CShape selectedShape in theglobal.SelectedShapeList)
			{
				if (selectedShape.MouseOnMe(e.Location))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				theglobal.SelectedShapeList.Clear();
				CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
			}
			if (theglobal.str_IMDoingWhat == "Select" || theglobal.str_IMDoingWhat == "MSelect")
			{
				Cursor = Cursors.Default;
				popupMenu1.ShowPopup(Cursor.Position);
				theglobal.IMContorlWhatPoint = -1;
			}
			else
			{
				if (theglobal.str_IMDoingWhat == "BeginDrawShape")
				{
					UserCommandControl2.GiveName(theglobal.g_ListAllShowCShape[theglobal.g_ListAllShowCShape.Count - 1]);
					CShape cShape = theglobal.g_ListAllShowCShape[theglobal.g_ListAllShowCShape.Count - 1].Copy();
					List<CShape> list = new List<CShape>
                    {
                        cShape
                    };
					theglobal.ForUndo(list, null);
					CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
					Trace.WriteLine("Draw Shape Finished: " + cShape.ShapeName);
					CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
				}
				theglobal.str_IMDoingWhat = "Select";
				theglobal.IMContorlWhatPoint = -1;
			}
		}
		if (e.Button == MouseButtons.Left)
		{
			if (theglobal.str_IMDoingWhat == "MSelect")
			{
				if ((float)e.Location.X != theglobal.mselectfirstp.X && (float)e.Location.Y != theglobal.mselectfirstp.Y)
				{
					foreach (CShape item4 in theglobal.g_ListAllShowCShape)
					{
						float num = float.MaxValue;
						float num2 = float.MaxValue;
						float num3 = float.MinValue;
						float num4 = float.MinValue;
						try
						{
							for (int i = 0; i < 8; i++)
							{
								if (num > item4.ImportantPoints[i].X)
								{
									num = item4.ImportantPoints[i].X;
								}
								if (num2 > item4.ImportantPoints[i].Y)
								{
									num2 = item4.ImportantPoints[i].Y;
								}
								if (num3 < item4.ImportantPoints[i].X)
								{
									num3 = item4.ImportantPoints[i].X;
								}
								if (num4 < item4.ImportantPoints[i].Y)
								{
									num4 = item4.ImportantPoints[i].Y;
								}
							}
							List<CShape> list2 = new List<CShape>();
							bool flag2 = false;
							if (num > Math.Min(e.Location.X, theglobal.mselectfirstp.X) && num < Math.Max(e.Location.X, theglobal.mselectfirstp.X) && num2 > Math.Min(e.Location.Y, theglobal.mselectfirstp.Y) && num2 < Math.Max(e.Location.Y, theglobal.mselectfirstp.Y))
							{
								flag2 = true;
							}
							if (num3 > Math.Min(e.Location.X, theglobal.mselectfirstp.X) && num3 < Math.Max(e.Location.X, theglobal.mselectfirstp.X) && num2 > Math.Min(e.Location.Y, theglobal.mselectfirstp.Y) && num2 < Math.Max(e.Location.Y, theglobal.mselectfirstp.Y))
							{
								flag2 = true;
							}
							if (num > Math.Min(e.Location.X, theglobal.mselectfirstp.X) && num < Math.Max(e.Location.X, theglobal.mselectfirstp.X) && num4 > Math.Min(e.Location.Y, theglobal.mselectfirstp.Y) && num4 < Math.Max(e.Location.Y, theglobal.mselectfirstp.Y))
							{
								flag2 = true;
							}
							if (num3 > Math.Min(e.Location.X, theglobal.mselectfirstp.X) && num3 < Math.Max(e.Location.X, theglobal.mselectfirstp.X) && num4 > Math.Min(e.Location.Y, theglobal.mselectfirstp.Y) && num4 < Math.Max(e.Location.Y, theglobal.mselectfirstp.Y))
							{
								flag2 = true;
							}
							if (flag2)
							{
								list2.Add(item4);
							}
							theglobal.SelectedShapeList.AddRange(list2);
						}
						catch (Exception)
						{
							break;
						}
					}
					goto IL_10ae;
				}
				theglobal.str_IMDoingWhat = "Select";
			}
			if (theglobal.str_IMDoingWhat == "Select")
			{
				bool flag3 = false;
				theglobal.CSLS.Clear();
				for (int num5 = theglobal.g_ListAllShowCShape.Count - 1; num5 >= 0; num5--)
				{
					if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
					{
						theglobal.SelectedShapeList.Clear();
					}
					if (theglobal.g_ListAllShowCShape[num5].MouseOnMe(e.Location))
					{
						theglobal.CSLS.Add(theglobal.g_ListAllShowCShape[num5]);
					}
				}
				if (theglobal.OCSLS.Count == theglobal.CSLS.Count)
				{
					for (int j = 0; j < theglobal.OCSLS.Count && !(theglobal.OCSLS[j].ShapeName != theglobal.CSLS[j].ShapeName); j++)
					{
						flag3 = true;
					}
				}
				if (theglobal.CSLS.Count > 0)
				{
					if (!flag3)
					{
						theglobal.SelectInt = 0;
					}
					bool flag4 = true;
					foreach (CShape cSL in theglobal.CSLS)
					{
						if (!cSL.locked)
						{
							flag4 = false;
						}
					}
					while (!flag4 && theglobal.CSLS.Count != 1 && theglobal.CSLS[theglobal.SelectInt % theglobal.CSLS.Count].locked)
					{
						theglobal.SelectInt++;
					}
					theglobal.SelectedShapeList.Add(theglobal.CSLS[theglobal.SelectInt % theglobal.CSLS.Count]);
					theglobal.SelectInt++;
					theglobal.OCSLS.Clear();
					CShape[] array = theglobal.CSLS.ToArray();
					foreach (CShape item in array)
					{
						theglobal.OCSLS.Add(item);
					}
				}
				else
				{
					theglobal.OCSLS.Clear();
				}
			}
			if (theglobal.str_IMDoingWhat == "BeginDrawShape")
			{
				CShape cShape2 = theglobal.g_ListAllShowCShape[theglobal.g_ListAllShowCShape.Count - 1];
				if (!cShape2.AddPoint(e.Location))
				{
					BeginDrawShapeFirstPoint = Point.Empty;
					UserCommandControl2.GiveName(cShape2);
					CShape item2 = cShape2.Copy();
					List<CShape> list3 = new List<CShape>
                    {
                        item2
                    };
					theglobal.ForUndo(list3, null);
					theglobal.str_IMDoingWhat = "Select";
					if (cShape2.ImportantPoints.Length >= 10 && cShape2.ImportantPoints[9].X == cShape2.ImportantPoints[8].X && cShape2.ImportantPoints[9].Y == cShape2.ImportantPoints[8].Y && (cShape2 is CRectangle || cShape2 is CEllipse || cShape2 is CLine || cShape2 is CString || cShape2 is CCircleRect))
					{
						theglobal.g_ListAllShowCShape.Remove(cShape2);
						theglobal.SelectedShapeList.Remove(cShape2);
						CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
					}
					CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
					Trace.WriteLine("Draw Shape Finished: " + cShape2.ShapeName);
					CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
				}
			}
		}
		if (theglobal.str_IMDoingWhat == "EditPoint" || theglobal.str_IMDoingWhat == "EditLocation" || theglobal.str_IMDoingWhat == "EndEditStr" || theglobal.str_IMDoingWhat == "CopyMove")
		{
			CShape[] array = theglobal.g_ListAllShowCShape.ToArray();
			foreach (CShape cShape3 in array)
			{
				if (theglobal.str_IMDoingWhat == "EditLocation")
				{
					if (theglobal.OldShapes == null || theglobal.OldShapes.Count == 0)
					{
						continue;
					}
					List<CShape> list4 = new List<CShape>();
					List<CShape> list5 = new List<CShape>();
					foreach (CShape oldShape in theglobal.OldShapes)
					{
						foreach (CShape item5 in theglobal.g_ListAllShowCShape)
						{
							if (item5.ShapeID == oldShape.ShapeID)
							{
								list4.Add(item5);
								list5.Add(oldShape);
							}
						}
					}
					theglobal.ForUndo(list4, list5);
					theglobal.OldShapes.Clear();
					theglobal.OldShape = null;
				}
				else if (theglobal.str_IMDoingWhat == "CopyMove")
				{
					if (theglobal.OldShapes != null && theglobal.OldShapes.Count != 0)
					{
						List<CShape> list6 = new List<CShape>();
						List<CShape> list7 = new List<CShape>();
						foreach (CShape oldShape2 in theglobal.OldShapes)
						{
							foreach (CShape item6 in theglobal.g_ListAllShowCShape)
							{
								if (item6.ShapeID == oldShape2.ShapeID)
								{
									list6.Add(item6);
									list7.Add(oldShape2);
								}
							}
						}
						theglobal.ForUndo(list6, null);
						theglobal.OldShapes.Clear();
						theglobal.OldShape = null;
					}
					Cursor = Cursors.Default;
				}
				else if (theglobal.str_IMDoingWhat == "EditPoint")
				{
					if (theglobal.OldShapes == null || theglobal.OldShapes.Count == 0)
					{
						continue;
					}
					List<CShape> list8 = new List<CShape>();
					List<CShape> list9 = new List<CShape>();
					foreach (CShape oldShape3 in theglobal.OldShapes)
					{
						foreach (CShape item7 in theglobal.g_ListAllShowCShape)
						{
							if (item7.ShapeID == oldShape3.ShapeID)
							{
								list8.Add(item7);
								list9.Add(oldShape3);
							}
						}
					}
					theglobal.ForUndo(list8, list9);
					theglobal.OldShapes.Clear();
					theglobal.OldShape = null;
				}
				else if (theglobal.OldShape != null && cShape3.ShapeID == theglobal.OldShape.ShapeID)
				{
					if (theglobal.str_IMDoingWhat == "EndEditStr")
					{
						CString item3 = (CString)cShape3;
						Focus();
						theglobal.SelectedShapeList.Remove(item3);
					}
					List<CShape> list10 = new List<CShape>();
					List<CShape> list11 = new List<CShape>();
					list10.Add(cShape3);
					list11.Add(theglobal.OldShape);
					theglobal.ForUndo(list10, list11);
					theglobal.OldShape = null;
				}
			}
			theglobal.str_IMDoingWhat = "Select";
			theglobal.IMContorlWhatPoint = -1;
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
		if (theglobal.str_IMDoingWhat == "EndControlTrue")
		{
			CShape[] array = theglobal.g_ListAllShowCShape.ToArray();
			foreach (CShape cShape4 in array)
			{
				if (theglobal.OldShape != null && cShape4.ShapeID == theglobal.OldShape.ShapeID)
				{
					if (theglobal.str_IMDoingWhat == "EndControlTrue")
					{
						CControl cControl = (CControl)cShape4;
						cControl._c.Enabled = false;
					}
					theglobal.OldShape = null;
				}
			}
			theglobal.str_IMDoingWhat = "Select";
		}
		if (theglobal.str_IMDoingWhat == "ControlTrue")
		{
			theglobal.str_IMDoingWhat = "EndControlTrue";
		}
		if (theglobal.str_IMDoingWhat == "EditStr")
		{
			theglobal.str_IMDoingWhat = "EndEditStr";
		}
		goto IL_10ae;
		IL_10ae:
		if (theglobal.str_IMDoingWhat == "MSelect")
		{
			theglobal.str_IMDoingWhat = "Select";
			theglobal.mselectfirstp = PointF.Empty;
		}
		RefreshDHLJDataGridViewItems();
		theglobal.uc1.ReFreshEnable();
		CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
		RefreshGraphics();
		CEditEnvironmentGlobal.mdiparent.barCheckItem10.Tag = false;
		CEditEnvironmentGlobal.mdiparent.barCheckItem10.Checked = false;
		foreach (CShape selectedShape2 in theglobal.SelectedShapeList)
		{
			if (selectedShape2.locked)
			{
				CEditEnvironmentGlobal.mdiparent.barCheckItem10.Checked = true;
				break;
			}
		}
		CEditEnvironmentGlobal.mdiparent.barCheckItem10.Tag = null;
		theglobal.SLS_Changed(sender, null);
	}

	private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button19_Click(sender, e);
	}

	private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button20_Click(sender, e);
	}

	private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.SameWidth();
	}

	private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.SameHeight();
	}

	private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button27_Click(sender, e);
	}

	private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button16_Click(sender, e);
	}

	private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button15_Click(sender, e);
	}

	private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button14_Click(sender, e);
	}

	private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button11_Click(sender, e);
	}

	private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button12_Click(sender, e);
	}

	private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button13_Click(sender, e);
	}

	private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button7_Click(sender, e);
	}

	private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button8_Click(sender, e);
	}

	private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button9_Click(sender, e);
	}

	private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button10_Click(sender, e);
	}

	private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button17_Click(sender, e);
	}

	private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.uc1.button18_Click(sender, e);
	}

	private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
	{
		CEditEnvironmentGlobal.CLS.Clear();
		CShape[] array = theglobal.SelectedShapeList.ToArray();
		foreach (CShape cShape in array)
		{
			CEditEnvironmentGlobal.CLS.Add(cShape.clone());
		}
	}

	private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
	{
		List<CShape> list = new List<CShape>();
		CEditEnvironmentGlobal.CLS.Clear();
		CShape[] array = theglobal.SelectedShapeList.ToArray();
		foreach (CShape cShape in array)
		{
			List<string> list2 = CheckIOExists.ShapeInUse(theglobal.df.name + "." + cShape.Name);
			if (list2.Count != 0)
			{
				delPage delPage2 = new delPage(list2, "该图形正在被引用,是否仍继续操作.");
				if (delPage2.ShowDialog() != DialogResult.Yes)
				{
					return;
				}
			}
			if (cShape is CControl)
			{
				theglobal.uc2.Controls.Remove(((CControl)cShape)._c);
			}
			theglobal.g_ListAllShowCShape.Remove(cShape);
			CShape item = cShape;
			CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
			CEditEnvironmentGlobal.CLS.Add(item);
			list.Add(item);
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
		theglobal.ForUndo(null, list);
		theglobal.SelectedShapeList.Clear();
		CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
		RefreshGraphics();
	}

	private void barButtonItem_Delete_ItemClick(object sender, ItemClickEventArgs e)
	{
		List<CShape> list = new List<CShape>();
		CShape[] array = theglobal.SelectedShapeList.ToArray();
		foreach (CShape cShape in array)
		{
			List<string> list2 = CheckIOExists.ShapeInUse(theglobal.df.name + "." + cShape.Name);
			if (list2.Count != 0)
			{
				delPage delPage2 = new delPage(list2, "该图形正在被引用,是否仍继续操作.");
				if (delPage2.ShowDialog() != DialogResult.Yes)
				{
					return;
				}
			}
			if (cShape is CControl)
			{
				theglobal.uc2.Controls.Remove(((CControl)cShape)._c);
			}
			theglobal.g_ListAllShowCShape.Remove(cShape);
			CShape item = cShape;
			CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
			list.Add(item);
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
		theglobal.ForUndo(null, list);
		theglobal.SelectedShapeList.Clear();
		CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
		RefreshGraphics();
	}

	private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
	{
		List<CShape> list = new List<CShape>();
		theglobal.SelectedShapeList.Clear();
		CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
		CEditEnvironmentGlobal.CLS.Sort(CEditEnvironmentGlobal.CompareByLayer);
		CShape[] array = CEditEnvironmentGlobal.CLS.ToArray();
		foreach (CShape cShape in array)
		{
			CShape cShape2 = cShape.clone();
			while (true)
			{
				IL_0062:
				foreach (CShape item in theglobal.g_ListAllShowCShape)
				{
					if (item.Size == cShape2.Size && item.Location == cShape2.Location)
					{
						cShape2.Location = new Point(Convert.ToInt32(cShape2.Location.X) + 10, Convert.ToInt32(cShape2.Location.Y) + 10);
						goto IL_0062;
					}
				}
				break;
			}
			if (cShape is CControl)
			{
				((CControl)cShape2).initLocationErr = true;
				theglobal.uc2.Controls.Add(((CControl)cShape2)._c);
				((CControl)cShape2).initLocationErr = false;
				((CControl)cShape2)._c.Enabled = false;
				((CControl)cShape2)._c.BringToFront();
				if (((CControl)cShape)._c is IDCCEControl)
				{
					((IDCCEControl)((CControl)cShape2)._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
				}
			}
			cShape2.ShapeID = Guid.NewGuid();
			UserCommandControl2.GiveName(cShape2);
			theglobal.g_ListAllShowCShape.Add(cShape2);
			theglobal.SelectedShapeList.Add(cShape2);
			list.Add(cShape2);
			CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
		}
		theglobal.ForUndo(list, null);
		RefreshGraphics();
		theglobal.SLS_Changed(sender, null);
		CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
	}

	private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (theglobal.SelectedShapeList.Count != 0)
		{
			dhljForm dhljForm2 = new dhljForm();
			dhljForm2.theglobal = theglobal;
			dhljForm2.dfs = dfs;
			dhljForm2.ShowDialog();
		}
	}

	private void UserShapeEditControl_DragEnter(object sender, DragEventArgs e)
	{
		try
		{
			e.Effect = DragDropEffects.Copy;
			string text = (string)e.Data.GetData(typeof(string));
			if (!text.StartsWith("AddShape:"))
			{
				return;
			}
			if (text.Substring(9, 2) == "0:")
			{
				theglobal.str_IMDoingWhat = "DragDrop";
				if (text.Substring(11, 4) == "变量文字")
				{
					dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - 75), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - 50)), new Size(50, 20));
				}
				else
				{
					dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - 75), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - 50)), new Size(150, 100));
				}
			}
			else if (text.Substring(9, 2) == "1:")
			{
				string type = text.Substring(11);
				CVectorGraph cVectorGraph = new CVectorGraph();
				cVectorGraph.type = type;
				cVectorGraph.AddPoint(PointF.Empty);
				theglobal.str_IMDoingWhat = "DragDrop";
				dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Size.Ceiling(cVectorGraph.Size).Width / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Size.Ceiling(cVectorGraph.Size).Height / 2)), Size.Ceiling(cVectorGraph.Size));
			}
			else if (text.Substring(9, 2) == "2:")
			{
				string text2 = text.Substring(11);
				CControl cControl = new CControl();
				switch (text2)
				{
				case "按钮":
					cControl.type = "ShapeRuntime.CButton";
					break;
				case "多选框":
					cControl.type = "ShapeRuntime.CCheckBox";
					break;
				case "标签":
					cControl.type = "ShapeRuntime.CLabel";
					break;
				case "文本框":
					cControl.type = "ShapeRuntime.CTextBox";
					break;
				case "下拉列表":
					cControl.type = "ShapeRuntime.CComboBox";
					break;
				case "日历控件":
					cControl.type = "ShapeRuntime.CDateTimePicker";
					break;
				case "图片框":
					cControl.type = "ShapeRuntime.CPictureBox";
					break;
				case "分组框":
					cControl.type = "ShapeRuntime.CGroupBox";
					break;
				case "数据视图":
					cControl.type = "ShapeRuntime.CDataGridView";
					break;
				case "列表框":
					cControl.type = "ShapeRuntime.CListBox";
					break;
				case "定时器":
					cControl.type = "ShapeRuntime.CTimer";
					break;
				case "HTML5上传控件":
					cControl.type = "ShapeRuntime.HTML5_Controls.FileUpLoad";
					break;
				case "Excel 导入控件":
					cControl.type = "ShapeRuntime.HTML5_Controls.ExcelIn";
					break;
				}
				cControl._dllfile = AppDomain.CurrentDomain.BaseDirectory + "ShapeRuntime.dll";
				cControl.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)));
				theglobal.str_IMDoingWhat = "DragDrop";
				dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Convert.ToInt32(cControl.Size.Width) / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Convert.ToInt32(cControl.Size.Height) / 2)), new Size(Convert.ToInt32(cControl.Size.Width), Convert.ToInt32(cControl.Size.Height)));
				dragRightDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Convert.ToInt32(cControl.Size.Width) / 2) + Convert.ToInt32(cControl.Size.Width), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Convert.ToInt32(cControl.Size.Height) / 2) - 5), new Size(5, 5));
			}
			else if (text.Substring(9, 2) == "3:")
			{
				if (text.EndsWith("|"))
				{
					text = text.Substring(0, text.Length - 1);
				}
				string[] array = text.Substring(11).Split('|');
				string type2 = array[0];
				string dllfile = array[1];
				CControl cControl2 = new CControl();
				cControl2.type = type2;
				cControl2._dllfile = dllfile;
				List<string> list = new List<string>();
				for (int i = 2; i < array.Length; i++)
				{
					list.Add(array[i]);
				}
				cControl2._files = list.ToArray();
				cControl2.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)));
				theglobal.str_IMDoingWhat = "DragDrop";
				dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Convert.ToInt32(cControl2.Size.Width) / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Convert.ToInt32(cControl2.Size.Height) / 2)), new Size(Convert.ToInt32(cControl2.Size.Width), Convert.ToInt32(cControl2.Size.Height)));
			}
			else if (text.Substring(9, 2) == "4:")
			{
				string[] array2 = text.Substring(11).Split('|');
				string type3 = array2[0];
				string dllfile2 = array2[1];
				CControl cControl3 = new CControl();
				cControl3.type = type3;
				cControl3._dllfile = dllfile2;
				List<string> list2 = new List<string>();
				for (int j = 2; j < array2.Length; j++)
				{
					list2.Add(array2[j]);
				}
				cControl3._files = list2.ToArray();
				cControl3.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)));
				theglobal.str_IMDoingWhat = "DragDrop";
			}
			else if (text.Substring(9, 2) == "5:")
			{
				string[] array3 = text.Substring(11).Split('|');
				theglobal.str_IMDoingWhat = "DragDrop";
				dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Convert.ToInt32(array3[1]) / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Convert.ToInt32(array3[2]) / 2)), new Size(Convert.ToInt32(array3[1]), Convert.ToInt32(array3[2])));
			}
			else if (text.Substring(9, 2) == "6:")
			{
				if (text.EndsWith("|"))
				{
					text = text.Substring(0, text.Length - 1);
				}
				string[] array4 = text.Substring(11).Split('|');
				string type4 = array4[0];
				string dllfile3 = array4[1];
				CControl cControl4 = new CControl();
				cControl4.type = type4;
				cControl4._dllfile = dllfile3;
				List<string> list3 = new List<string>();
				for (int k = 2; k < array4.Length; k++)
				{
					list3.Add(array4[k]);
				}
				cControl4._files = list3.ToArray();
				cControl4.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)));
				theglobal.str_IMDoingWhat = "DragDrop";
				dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - Convert.ToInt32(cControl4.Size.Width) / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - Convert.ToInt32(cControl4.Size.Height) / 2)), new Size(Convert.ToInt32(cControl4.Size.Width), Convert.ToInt32(cControl4.Size.Height)));
			}
			Invalidate(invalidateChildren: false);
			Update();
		}
		catch (Exception)
		{
		}
	}

	private void UserShapeEditControl_DragOver(object sender, DragEventArgs e)
	{
		dragDropRect = new Rectangle(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X - dragDropRect.Size.Width / 2), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y - dragDropRect.Size.Height / 2)), dragDropRect.Size);
		Invalidate(invalidateChildren: false);
		Update();
	}

	private void UserShapeEditControl_DragLeave(object sender, EventArgs e)
	{
		theglobal.str_IMDoingWhat = "Select";
		Invalidate(invalidateChildren: false);
		Update();
	}

	private void UserShapeEditControl_DragDrop(object sender, DragEventArgs e)
	{
		theglobal.str_IMDoingWhat = "Select";
		string text = (string)e.Data.GetData(typeof(string));
		if (!text.StartsWith("AddShape:"))
		{
			return;
		}
		CShape cShape = null;
		if (text.Substring(9, 2) == "0:")
		{
			switch (text.Substring(11))
			{
			case "直线":
			{
				CLine cLine = new CLine();
				UserCommandControl2.GiveName(cLine);
				cLine.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cLine.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cShape = cLine;
				break;
			}
			case "椭圆":
			{
				CEllipse cEllipse = new CEllipse();
				UserCommandControl2.GiveName(cEllipse);
				cEllipse.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cEllipse.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cShape = cEllipse;
				break;
			}
			case "矩形":
			{
				CRectangle cRectangle = new CRectangle();
				UserCommandControl2.GiveName(cRectangle);
				cRectangle.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cRectangle.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cShape = cRectangle;
				break;
			}
			case "圆角矩形":
			{
				CCircleRect cCircleRect = new CCircleRect();
				UserCommandControl2.GiveName(cCircleRect);
				cCircleRect.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cCircleRect.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cShape = cCircleRect;
				break;
			}
			case "贝塞尔曲线":
			{
				CBezier cBezier = new CBezier();
				UserCommandControl2.GiveName(cBezier);
				cBezier.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cBezier.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cShape = cBezier;
				break;
			}
			case "三角形":
			{
				CCloseLines cCloseLines = new CCloseLines();
				UserCommandControl2.GiveName(cCloseLines);
				cCloseLines.AddPoint(PointToClient(new Point(e.X, e.Y - 50)));
				cCloseLines.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cCloseLines.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(-75, 50));
				cShape = cCloseLines;
				break;
			}
			case "文字":
			{
				CString cString2 = new CString();
				UserCommandControl2.GiveName(cString2);
				cString2.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(75, 50));
				cString2.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(75, 50));
				cString2.DisplayStr = "#####";
				cShape = cString2;
				break;
			}
			default:
			{
				CString cString = new CString();
				UserCommandControl2.GiveName(cString);
				cString.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(30, 10));
				cString.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) + new Size(30, 10));
				cString.DisplayStr = "#####";
				cShape = cString;
				theglobal.SelectedShapeList.Clear();
				theglobal.SelectedShapeList.Add(cString);
				string text2 = text.Substring(text.LastIndexOf(':') + 1);
				theglobal.SelectedShapeList[0].ao = true;
				theglobal.SelectedShapeList[0].aojingdu = 0;
				theglobal.SelectedShapeList[0].aobianliang = "[" + text2 + "]";
				break;
			}
			}
		}
		else if (text.Substring(9, 2) == "1:")
		{
			string type = text.Substring(11);
			CVectorGraph cVectorGraph = new CVectorGraph();
			UserCommandControl2.GiveName(cVectorGraph);
			cVectorGraph.type = type;
			if (cVectorGraph.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2))))
			{
				UserCommandControl2.GiveName(cVectorGraph);
				cVectorGraph.Location = new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2));
				cShape = cVectorGraph;
			}
		}
		else if (text.Substring(9, 2) == "2:")
		{
			string text3 = text.Substring(11);
			CControl cControl = new CControl();
			cControl.IsHTML5_Control = false;

			switch (text3)
			{
			case "按钮":
				cControl.type = "ShapeRuntime.CButton";
				break;
			case "多选框":
				cControl.type = "ShapeRuntime.CCheckBox";
				break;
			case "标签":
				cControl.type = "ShapeRuntime.CLabel";
				break;
			case "文本框":
				cControl.type = "ShapeRuntime.CTextBox";
				break;
			case "下拉列表":
				cControl.type = "ShapeRuntime.CComboBox";
				break;
			case "日历控件":
				cControl.type = "ShapeRuntime.CDateTimePicker";
				break;
			case "图片框":
				cControl.type = "ShapeRuntime.CPictureBox";
				break;
			case "分组框":
				cControl.type = "ShapeRuntime.CGroupBox";
				break;
			case "数据视图":
				cControl.type = "ShapeRuntime.CDataGridView";
				break;
			case "列表框":
				cControl.type = "ShapeRuntime.CListBox";
				break;
			case "定时器":
				cControl.type = "ShapeRuntime.CTimer";
				break;
			case "HTML5上传控件":
				cControl.type = "ShapeRuntime.HTML5_Controls.FileUpLoad";
				break;
			case "Excel导入控件":
				cControl.type = "ShapeRuntime.HTML5_Controls.ExcelIn";
				break;
			}
			cControl._dllfile = AppDomain.CurrentDomain.BaseDirectory + "ShapeRuntime.dll";
			cControl.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2)));
			UserCommandControl2.GiveName(cControl);
			cControl._c.Enabled = false;
			if (cControl._c is IDCCEControl)
			{
				((IDCCEControl)cControl._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
			}
			cShape = cControl;
		}
		else if (text.Substring(9, 2) == "3:")
		{
			if (text.EndsWith("|"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			string[] array = text.Substring(11).Split('|');
			string type2 = array[0];
			string dllfile = array[1];
			CControl cControl2 = new CControl();
			cControl2.type = type2;
			cControl2._dllfile = dllfile;
			List<string> list = new List<string>();
			for (int i = 2; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			cControl2._files = list.ToArray();
			cControl2.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2)));
			UserCommandControl2.GiveName(cControl2);
			if (cControl2._c is IDCCEControl)
			{
				((IDCCEControl)cControl2._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
			}
			cControl2._c.Enabled = false;
			cShape = cControl2;
		}
		else if (text.Substring(9, 2) == "4:")
		{
			string[] array2 = text.Substring(11).Split('|');
			string type3 = array2[0];
			string dllfile2 = array2[1];
			CControl cControl3 = new CControl();
			cControl3.type = type3;
			cControl3._dllfile = dllfile2;
			List<string> list2 = new List<string>();
			for (int j = 2; j < array2.Length; j++)
			{
				list2.Add(array2[j]);
			}
			cControl3._files = list2.ToArray();
			cControl3.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2)));
			UserCommandControl2.GiveName(cControl3);
			cControl3.Location = new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2));
			cControl3._c.Enabled = false;
			cShape = cControl3;
		}
		else if (text.Substring(9, 2) == "5:")
		{
			string[] array3 = text.Substring(11).Split('|');
			string text4 = array3[0];
			string text5 = array3[3];
			Assembly assembly = Assembly.LoadFrom(text5);
			Type type4 = assembly.GetType(text4);
			object obj = Activator.CreateInstance(type4);
			CPixieControl cPixieControl = obj as CPixieControl;
			cPixieControl.OnGetVarTable += CForDCCEControl.GetVarTableEvent;
			cPixieControl.ValidateVar += CForDCCEControl.ValidateVarEvent;
			cPixieControl.type = text4;
			cPixieControl._dllfile = text5;
			List<string> list3 = new List<string>();
			for (int k = 4; k < array3.Length; k++)
			{
				list3.Add(array3[k]);
			}
			cPixieControl._files = list3.ToArray();
			cPixieControl.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2)));
			UserCommandControl2.GiveName(cPixieControl);
			cPixieControl.Location = new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2));
			cShape = cPixieControl;
		}
		else if (text.Substring(9, 2) == "6:")
		{
			CControl cControl4 = new CControl();
			cControl4.IsHTML5_Control = false;

			if (text.EndsWith("|"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			string[] array4 = text.Substring(11).Split('|');
			string type5 = array4[0];
			string dllfile3 = array4[1];
			cControl4.type = type5;
			cControl4._dllfile = dllfile3;
			List<string> list4 = new List<string>();
			for (int l = 2; l < array4.Length; l++)
			{
				list4.Add(array4[l]);
			}
			cControl4._files = list4.ToArray();
			cControl4.AddPoint(new Point(Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).X), Convert.ToInt32(PointToClient(new Point(e.X, e.Y)).Y)) - new Size(Convert.ToInt32(dragDropRect.Size.Width / 2), Convert.ToInt32(dragDropRect.Size.Height / 2)));
			UserCommandControl2.GiveName(cControl4);
			if (cControl4._c is IDCCEControl)
			{
				((IDCCEControl)cControl4._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
			}
			cControl4._c.Enabled = false;
			cShape = cControl4;
		}
		if (cShape != null)
		{
			if (cShape is CControl)
			{
				((CControl)cShape).initLocationErr = true;
				theglobal.uc2.Controls.Add(((CControl)cShape)._c);
				((CControl)cShape).initLocationErr = false;
			}
			theglobal.g_ListAllShowCShape.Add(cShape);
			theglobal.SelectedShapeList.Clear();
			theglobal.SelectedShapeList.Add(cShape);
			CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
			List<CShape> list5 = new List<CShape>
            {
                cShape
            };
			theglobal.ForUndo(list5, null);
		}
		RefreshGraphics();
		CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
	}

	private void barSubItem1_Popup(object sender, EventArgs e)
	{
		CEditEnvironmentGlobal.NotEditValue = true;
		if (theglobal.SelectedShapeList.Count == 1)
		{
			barButtonItem12.Enabled = true;
			barEditItem2.Enabled = true;
			barEditItem3.Enabled = true;
			barEditItem2.EditValue = theglobal.SelectedShapeList[0].Color1;
			barEditItem3.EditValue = theglobal.SelectedShapeList[0].PenColor;
		}
		else
		{
			barEditItem2.EditValue = Color.Transparent;
			barEditItem3.EditValue = Color.Transparent;
			barButtonItem12.Enabled = false;
			barEditItem2.Enabled = false;
			barEditItem3.Enabled = false;
		}
		CEditEnvironmentGlobal.NotEditValue = false;
	}

	private void barEditItemControlName_EditValueChanged(object sender, EventArgs e)
	{
		if (theglobal.SelectedShapeList.Count != 1 || CEditEnvironmentGlobal.NotEditValue)
		{
			return;
		}
		List<string> list = CheckIOExists.ShapeInUse(CEditEnvironmentGlobal.childform.theglobal.df.name + "." + theglobal.SelectedShapeList[0].ShapeName);
		if (list.Count != 0)
		{
			delPage delPage2 = new delPage(list, "该图形正在被引用,是否仍继续操作.");
			if (delPage2.ShowDialog(this) != DialogResult.Yes)
			{
				return;
			}
		}
		Regex regex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$");
		if (!regex.IsMatch((string)barEditItemControlName.EditValue))
		{
			MessageBox.Show("名称中包含非法字符.", "错误");
			return;
		}
		foreach (CShape item in theglobal.g_ListAllShowCShape)
		{
			if (item != theglobal.SelectedShapeList[0] && item.Name == (string)barEditItemControlName.EditValue)
			{
				MessageBox.Show("页面中已存在名称为\"" + (string)barEditItemControlName.EditValue + "\"的元素.", "错误");
				return;
			}
		}
		string[] keystr = CheckScript.Keystr;
		foreach (string text in keystr)
		{
			if ((string)barEditItemControlName.EditValue == text)
			{
				MessageBox.Show("名称中含有关键字将会导致错误。");
				return;
			}
		}
		theglobal.OldShape = theglobal.SelectedShapeList[0].Copy();
		theglobal.SelectedShapeList[0].Name = (string)barEditItemControlName.EditValue;
		List<CShape> list2 = new List<CShape>();
		List<CShape> list3 = new List<CShape>();
		list2.Add(theglobal.SelectedShapeList[0]);
		list3.Add(theglobal.OldShape);
		theglobal.ForUndo(list2, list3);
		CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(theglobal.SelectedShapeList[0].ShapeID.ToString());
		CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
	}

	private void barEditItem2_EditValueChanged(object sender, EventArgs e)
	{
		if (!CEditEnvironmentGlobal.NotEditValue)
		{
			theglobal.OldShape = theglobal.SelectedShapeList[0].Copy();
			theglobal.SelectedShapeList[0].Color1 = (Color)barEditItem2.EditValue;
			List<CShape> list = new List<CShape>();
			List<CShape> list2 = new List<CShape>();
			list.Add(theglobal.SelectedShapeList[0]);
			list2.Add(theglobal.OldShape);
			theglobal.ForUndo(list, list2);
			RefreshGraphics();
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
	}

	private void barEditItem3_EditValueChanged(object sender, EventArgs e)
	{
		if (!CEditEnvironmentGlobal.NotEditValue)
		{
			theglobal.OldShape = theglobal.SelectedShapeList[0].Copy();
			theglobal.SelectedShapeList[0].PenColor = (Color)barEditItem3.EditValue;
			List<CShape> list = new List<CShape>();
			List<CShape> list2 = new List<CShape>();
			list.Add(theglobal.SelectedShapeList[0]);
			list2.Add(theglobal.OldShape);
			theglobal.ForUndo(list, list2);
			RefreshGraphics();
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
	}

	private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
	{
		theglobal.pg.SelectedObject = theglobal.pageProp;
		PagePropertyForm pagePropertyForm = new PagePropertyForm();
		if (pagePropertyForm.ShowDialog() == DialogResult.OK)
		{
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
	}

	private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
	{
		EventBindForm eventBindForm = new EventBindForm(theglobal.df);
		if (eventBindForm.ShowDialog() == DialogResult.OK)
		{
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
	}

	private void popupMenu1_BeforePopup(object sender, CancelEventArgs e)
	{
		if (theglobal.SelectedShapeList.Count == 0)
		{
			barButtonItem8.Visibility = BarItemVisibility.Never;
			barButtonItem1.Visibility = BarItemVisibility.Never;
			barEditItemControlName.Visibility = BarItemVisibility.Never;
			barSubItem1.Visibility = BarItemVisibility.Never;
			barSubItem5.Visibility = BarItemVisibility.Never;
			barSubItem2.Visibility = BarItemVisibility.Never;
			barSubItem3.Visibility = BarItemVisibility.Never;
			barSubItem6.Visibility = BarItemVisibility.Never;
			barButtonItem8.Enabled = false;
			barButtonItem1.Enabled = false;
			barButtonItem3.Enabled = false;
			barButtonItem4.Enabled = false;
			if (CEditEnvironmentGlobal.CLS.Count != 0)
			{
				barButtonItem5.Enabled = true;
			}
			else
			{
				barButtonItem5.Enabled = false;
			}
			barSubItem1.Enabled = false;
			barButtonItem24.Enabled = false;
			barButtonItem25.Enabled = false;
			if (barButtonItem24.Enabled || barButtonItem25.Enabled)
			{
				barSubItem5.Enabled = true;
			}
			barSubItem2.Enabled = false;
			barSubItem3.Enabled = false;
			barSubItem6.Enabled = false;
			return;
		}
		if (theglobal.SelectedShapeList.Count == 1)
		{
			if (theglobal.SelectedShapeList[0] is CPixieControl)
			{
				barButtonItem8.Enabled = true;
				barButtonItem8.Visibility = BarItemVisibility.Always;
			}
			else
			{
				barButtonItem8.Enabled = false;
				barButtonItem8.Visibility = BarItemVisibility.Never;
			}
			barButtonItem1.Visibility = BarItemVisibility.Always;
			barSubItem1.Visibility = BarItemVisibility.Always;
			barSubItem5.Visibility = BarItemVisibility.Always;
			barSubItem2.Visibility = BarItemVisibility.Always;
			barSubItem3.Visibility = BarItemVisibility.Always;
			barSubItem6.Visibility = BarItemVisibility.Always;
			barEditItemControlName.Visibility = BarItemVisibility.Always;
			barEditItemControlName.Enabled = true;
			barEditItemControlName.EditValueChanged -= barEditItemControlName_EditValueChanged;
			barEditItemControlName.EditValue = theglobal.SelectedShapeList[0].Name;
			barEditItemControlName.EditValueChanged += barEditItemControlName_EditValueChanged;
			barButtonItem1.Enabled = true;
			barButtonItem3.Enabled = true;
			barButtonItem4.Enabled = true;
			if (CEditEnvironmentGlobal.CLS.Count != 0)
			{
				barButtonItem5.Enabled = true;
			}
			else
			{
				barButtonItem5.Enabled = false;
			}
			barSubItem1.Enabled = true;
			barButtonItem24.Enabled = false;
			if (theglobal.SelectedShapeList[0] is CGraphicsPath)
			{
				barButtonItem25.Enabled = true;
			}
			else
			{
				barButtonItem25.Enabled = false;
			}
			if (barButtonItem24.Enabled || barButtonItem25.Enabled)
			{
				barSubItem5.Enabled = true;
			}
			barSubItem2.Enabled = false;
			barSubItem3.Enabled = false;
			barSubItem6.Enabled = true;
			if (theglobal.SelectedShapeList[0] is CControl || theglobal.SelectedShapeList[0] is CPixieControl || theglobal.SelectedShapeList[0] is CVectorGraph || theglobal.SelectedShapeList[0] is CPicture)
			{
				barButtonItem12.Enabled = false;
				barEditItem2.Enabled = false;
				barEditItem3.Enabled = false;
			}
			return;
		}
		if (theglobal.SelectedShapeList[0] is CPixieControl)
		{
			barButtonItem8.Enabled = true;
			barButtonItem8.Visibility = BarItemVisibility.Always;
		}
		else
		{
			barButtonItem8.Enabled = false;
			barButtonItem8.Visibility = BarItemVisibility.Never;
		}
		barButtonItem1.Visibility = BarItemVisibility.Always;
		barSubItem1.Visibility = BarItemVisibility.Always;
		barSubItem5.Visibility = BarItemVisibility.Always;
		barSubItem2.Visibility = BarItemVisibility.Always;
		barSubItem3.Visibility = BarItemVisibility.Always;
		barSubItem6.Visibility = BarItemVisibility.Always;
		barEditItemControlName.Visibility = BarItemVisibility.Always;
		barEditItemControlName.Enabled = false;
		barEditItemControlName.EditValue = "";
		barButtonItem1.Enabled = false;
		barButtonItem3.Enabled = true;
		barButtonItem4.Enabled = true;
		if (CEditEnvironmentGlobal.CLS.Count != 0)
		{
			barButtonItem5.Enabled = true;
		}
		else
		{
			barButtonItem5.Enabled = false;
		}
		barSubItem1.Enabled = false;
		barButtonItem24.Enabled = false;
		int num = 0;
		foreach (CShape selectedShape in theglobal.SelectedShapeList)
		{
			_ = selectedShape;
			if (!(theglobal.SelectedShapeList[0] is CControl))
			{
				num++;
				if (num >= 2)
				{
					barButtonItem24.Enabled = true;
					break;
				}
			}
		}
		barButtonItem25.Enabled = false;
		foreach (CShape selectedShape2 in theglobal.SelectedShapeList)
		{
			_ = selectedShape2;
			if (theglobal.SelectedShapeList[0] is CGraphicsPath)
			{
				barButtonItem25.Enabled = true;
				break;
			}
		}
		if (barButtonItem24.Enabled || barButtonItem25.Enabled)
		{
			barSubItem5.Enabled = true;
		}
		barSubItem2.Enabled = true;
		barSubItem3.Enabled = true;
		barSubItem6.Enabled = true;
		barButtonItem12.Enabled = false;
		barEditItem2.Enabled = false;
		barEditItem3.Enabled = false;
		bool flag = false;
		foreach (CShape selectedShape3 in theglobal.SelectedShapeList)
		{
			if (!(selectedShape3 is CControl) && !(selectedShape3 is CPixieControl) && !(selectedShape3 is CVectorGraph) && !(selectedShape3 is CPicture))
			{
				flag = true;
			}
		}
		if (flag)
		{
			barButtonItem12.Enabled = true;
			barEditItem2.Enabled = true;
			barEditItem3.Enabled = true;
		}
	}

	private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (theglobal.SelectedShapeList.Count != 0 && theglobal.SelectedShapeList[0] is CPixieControl)
		{
			(theglobal.SelectedShapeList[0] as CPixieControl).ShowPropertyDialog();
			Invalidate(invalidateChildren: false);
			Update();
			CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
		}
	}

	public bool MouseMove_RecordMousePoint(MouseEventArgs e)
	{
		try
		{
			e = new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X), Convert.ToInt32(e.Y), e.Delta);
			pDrawStartPoint = e.Location;
			CEditEnvironmentGlobal.mdiparent.BarStaticItem_Status.Caption = "状态 鼠标 (" + e.X + "," + e.Y + ")";
			return true;
		}
		catch
		{
			MessageBox.Show("画图起始点及记录鼠标状态出现异常！", "提示");
			return false;
		}
	}

	public bool MouseMove_ShowCursorsStatus(MouseEventArgs e)
	{
		try
		{
			switch (theglobal.str_IMDoingWhat)
			{
			case "BeginDrawShape":
				Cursor = Cursors.Cross;
				break;
			case "EditPoint":
			{
				Cursor = Cursors.Cross;
				int iMContorlWhatPoint = theglobal.IMContorlWhatPoint;
				if (iMContorlWhatPoint == 55)
				{
					Cursor = Cursors.UpArrow;
				}
				break;
			}
			case "EditLocation":
				Cursor = Cursors.Hand;
				break;
			case "CopyMove":
				Cursor = new Cursor(new MemoryStream(Resources.dragging));
				break;
			}
			if (theglobal.str_IMDoingWhat == "Select")
			{
				foreach (CShape selectedShape in theglobal.SelectedShapeList)
				{
					if (-1 != selectedShape.EditPoint(e.Location, e.Location, -1) && !selectedShape.locked)
					{
						Cursor = Cursors.Cross;
						return true;
					}
				}
				foreach (CShape item in theglobal.g_ListAllShowCShape)
				{
					if (item.MouseOnMe(e.Location) && !item.locked)
					{
						Cursor = Cursors.Hand;
						return true;
					}
					Cursor = Cursors.Default;
				}
			}
			return true;
		}
		catch
		{
			MessageBox.Show("鼠标状态的指针形状出现异常！", "提示");
			return false;
		}
	}

	public bool MouseMove_OperateEditPoint(MouseEventArgs e)
	{
		try
		{
			if (theglobal.str_IMDoingWhat == "Select" || theglobal.str_IMDoingWhat == "EditPoint")
			{
				for (int num = theglobal.SelectedShapeList.Count - 1; num >= 0; num--)
				{
					bool flag = false;
					if (theglobal.str_IMDoingWhat == "Select" && theglobal.SelectedShapeList[num].EditPoint(e.Location, e.Location, -1) != -1 && !flag)
					{
						theglobal.OldShapes.Clear();
						flag = true;
						foreach (CShape selectedShape in theglobal.SelectedShapeList)
						{
							theglobal.OldShapes.Add(selectedShape.Copy());
						}
					}
					if (theglobal.IMContorlWhatPoint == -1)
					{
						theglobal.IMContorlWhatPoint = theglobal.SelectedShapeList[num].EditPoint(theglobal.lastmouseeventargs.Location, e.Location, theglobal.IMContorlWhatPoint);
					}
					else
					{
						theglobal.SelectedShapeList[num].EditPoint(theglobal.lastmouseeventargs.Location, e.Location, theglobal.IMContorlWhatPoint);
					}
					if (theglobal.IMContorlWhatPoint != -1)
					{
						theglobal.str_IMDoingWhat = "EditPoint";
					}
				}
			}
			return true;
		}
		catch
		{
			MessageBox.Show("操作编辑点出现异常！", "提示");
			return false;
		}
	}

	public bool MouseMove_BeforeDragSelected(MouseEventArgs e)
	{
		try
		{
			if (theglobal.str_IMDoingWhat == "Select")
			{
				bool flag = false;
				bool flag2 = false;
				CShape[] array = theglobal.SelectedShapeList.ToArray();
				foreach (CShape cShape in array)
				{
					if (cShape.MouseOnMe(e.Location) || cShape.EditPoint(e.Location, e.Location, -1) != -1 || cShape.EditLocation(e.Location, e.Location))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					bool flag3 = false;
					for (int num = theglobal.g_ListAllShowCShape.Count - 1; num >= 0; num--)
					{
						if (theglobal.g_ListAllShowCShape[num].MouseOnMe(e.Location) || theglobal.g_ListAllShowCShape[num].EditLocation(e.Location, e.Location))
						{
							flag3 = true;
							theglobal.SelectedShapeList.Clear();
							theglobal.SelectedShapeList.Add(theglobal.g_ListAllShowCShape[num]);
							CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
							theglobal.OldShapes.Clear();
							foreach (CShape selectedShape in theglobal.SelectedShapeList)
							{
								theglobal.OldShapes.Add(selectedShape.Copy());
							}
							theglobal.str_IMDoingWhat = "EditLocation";
							flag = true;
							theglobal.mousedownp = PointF.Empty;
							break;
						}
					}
					if (!flag3)
					{
						theglobal.mousedownp = e.Location;
					}
					CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
				}
				else
				{
					theglobal.mousedownp = PointF.Empty;
				}
				if (flag)
				{
					RefreshGraphics();
				}
			}
			return true;
		}
		catch
		{
			MessageBox.Show("控件拖拽前选择出现异常！", "提示");
			return false;
		}
	}

	public bool MouseMove_EditPosition(MouseEventArgs e)
	{
		try
		{
			if (theglobal.str_IMDoingWhat == "Select" || theglobal.str_IMDoingWhat == "EditLocation")
			{
				if (theglobal.SelectedShapeList.Count == 0)
				{
					for (int num = theglobal.SelectedShapeList.Count - 1; num >= 0; num--)
					{
						bool flag = false;
						if (theglobal.SelectedShapeList[num].EditLocation(e.Location, e.Location))
						{
							flag = true;
						}
						bool flag2 = false;
						flag2 = theglobal.SelectedShapeList[num].EditLocation(theglobal.lastmouseeventargs.Location, e.Location);
						if (flag2)
						{
							theglobal.str_IMDoingWhat = "EditLocation";
							for (int i = 0; i < theglobal.SelectedShapeList.Count; i++)
							{
								if (i != num)
								{
									theglobal.SelectedShapeList[i].X += e.Location.X - theglobal.lastmouseeventargs.X;
									theglobal.SelectedShapeList[i].Y += e.Location.Y - theglobal.lastmouseeventargs.Y;
								}
							}
						}
						if (flag2)
						{
							theglobal.str_IMDoingWhat = "EditLocation";
							if (theglobal.OldShapes.Count != 0 || !flag)
							{
								break;
							}
							theglobal.OldShapes.Clear();
							foreach (CShape selectedShape in theglobal.SelectedShapeList)
							{
								theglobal.OldShapes.Add(selectedShape.Copy());
							}
							break;
						}
					}
				}
				else
				{
					for (int num2 = theglobal.SelectedShapeList.Count - 1; num2 >= 0; num2--)
					{
						bool flag3 = false;
						if (theglobal.SelectedShapeList[num2].EditLocation(e.Location, e.Location))
						{
							flag3 = true;
						}
						bool flag4 = false;
						flag4 = theglobal.SelectedShapeList[num2].EditLocation(theglobal.lastmouseeventargs.Location, e.Location);
						if (flag4)
						{
							theglobal.str_IMDoingWhat = "EditLocation";
							if (theglobal.OldShapes.Count == 0 && flag3 && theglobal.SelectedShapeList.Count > 0)
							{
								theglobal.OldShapes.Clear();
								foreach (CShape selectedShape2 in theglobal.SelectedShapeList)
								{
									theglobal.OldShapes.Add(selectedShape2.Copy());
								}
							}
							for (int j = 0; j < theglobal.SelectedShapeList.Count; j++)
							{
								if (j != num2)
								{
									theglobal.SelectedShapeList[j].X += e.Location.X - theglobal.lastmouseeventargs.X;
									theglobal.SelectedShapeList[j].Y += e.Location.Y - theglobal.lastmouseeventargs.Y;
								}
							}
						}
						if (flag4)
						{
							break;
						}
					}
				}
			}
			else if (theglobal.str_IMDoingWhat == "CopyMove")
			{
				bool flag5 = false;
				flag5 = true;
				bool flag6 = false;
				flag6 = true;
				if (flag6)
				{
					theglobal.str_IMDoingWhat = "CopyMove";
					for (int k = 0; k < theglobal.SelectedShapeList.Count; k++)
					{
						theglobal.SelectedShapeList[k].X += e.Location.X - theglobal.lastmouseeventargs.X;
						theglobal.SelectedShapeList[k].Y += e.Location.Y - theglobal.lastmouseeventargs.Y;
					}
				}
				if (flag6)
				{
					theglobal.str_IMDoingWhat = "CopyMove";
					if (theglobal.OldShapes.Count == 0 && flag5)
					{
						theglobal.OldShapes.Clear();
						foreach (CShape selectedShape3 in theglobal.SelectedShapeList)
						{
							theglobal.OldShapes.Add(selectedShape3.Copy());
						}
					}
				}
			}
			return true;
		}
		catch
		{
			MessageBox.Show("鼠标移动，编辑控件位置！", "提示");
			return false;
		}
	}

	public UserShapeEditControl()
	{
		InitializeComponent();
	}

	public void KeyUpFunc()
	{
		try
		{
			List<CShape> oldshapelist = new List<CShape>(theglobal.SelectedShapeList.ToArray());
			List<CShape> list = new List<CShape>();
			CShape[] array = theglobal.SelectedShapeList.ToArray();
			foreach (CShape cShape in array)
			{
				cShape.Y += -1f;
				list.Add(cShape.Copy());
			}
			theglobal.ForUndo(list, oldshapelist);
			theglobal.pg.Refresh();
			RefreshGraphics();
		}
		catch
		{
			MessageBox.Show("鼠标上移方法出现异常！", "提示");
		}
	}

	public void KeyDownFunc()
	{
		try
		{
			List<CShape> oldshapelist = new List<CShape>(theglobal.SelectedShapeList.ToArray());
			List<CShape> list = new List<CShape>();
			CShape[] array = theglobal.SelectedShapeList.ToArray();
			foreach (CShape cShape in array)
			{
				cShape.Y += 1f;
				list.Add(cShape.Copy());
			}
			theglobal.ForUndo(list, oldshapelist);
			theglobal.pg.Refresh();
			RefreshGraphics();
		}
		catch
		{
			MessageBox.Show("鼠标下移方法出现异常！", "提示");
		}
	}

	public void KeyLeftFunc()
	{
		try
		{
			List<CShape> oldshapelist = new List<CShape>(theglobal.SelectedShapeList.ToArray());
			List<CShape> list = new List<CShape>();
			CShape[] array = theglobal.SelectedShapeList.ToArray();
			foreach (CShape cShape in array)
			{
				cShape.X += -1f;
				list.Add(cShape.Copy());
			}
			theglobal.ForUndo(list, oldshapelist);
			theglobal.pg.Refresh();
			RefreshGraphics();
		}
		catch
		{
			MessageBox.Show("鼠标左移方法出现异常！", "提示");
		}
	}

	public void KeyRightFunc()
	{
		try
		{
			List<CShape> oldshapelist = new List<CShape>(theglobal.SelectedShapeList.ToArray());
			List<CShape> list = new List<CShape>();
			CShape[] array = theglobal.SelectedShapeList.ToArray();
			foreach (CShape cShape in array)
			{
				cShape.X += 1f;
				list.Add(cShape.Copy());
			}
			theglobal.ForUndo(list, oldshapelist);
			theglobal.pg.Refresh();
			RefreshGraphics();
		}
		catch
		{
			MessageBox.Show("鼠标右移方法出现异常！", "提示");
		}
	}

	public void RefreshGraphics()
	{
		try
		{
			if (graphicsBuffer == null)
			{
				graphicsBuffer = new Bitmap(base.Width, base.Height);
			}
			else if (graphicsBuffer.Width != base.Width || graphicsBuffer.Height != base.Height)
			{
				graphicsBuffer.Dispose();
				if (base.Width == 0 || base.Height == 0)
				{
					graphicsBuffer = new Bitmap(1, 1);
				}
				else
				{
					graphicsBuffer = new Bitmap(base.Width, base.Height);
				}
			}
			Graphics graphics = Graphics.FromImage(graphicsBuffer);
			graphics.Clear(Color.Transparent);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			CShape[] array = theglobal.g_ListAllShowCShape.ToArray();
			foreach (CShape cShape in array)
			{
				if (!theglobal.SelectedShapeList.Contains(cShape))
				{
					cShape.DrawMe(graphics);
				}
			}
			graphics.DrawRectangle(Pens.Black, 0, 0, theglobal.df.size.Width - 1, theglobal.df.size.Height - 1);
			if (theglobal.df.IsWindow)
			{
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, theglobal.df.size.Width - 1, 22), SystemColors.GradientActiveCaption, SystemColors.ActiveCaption, 0f);
				graphics.FillRectangle(brush, new Rectangle(0, 0, theglobal.df.size.Width - 1, 22));
				ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, Convert.ToInt32(theglobal.df.size.Width - 1), 22), Border3DStyle.Bump);
				ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, Convert.ToInt32(theglobal.df.size.Width - 1), Convert.ToInt32(theglobal.df.size.Height - 1)), Border3DStyle.Bump);
				graphics.DrawString(theglobal.df.pageName, SystemFonts.DefaultFont, Brushes.DarkGray, new PointF(3f, 3f));
				graphics.DrawString(theglobal.df.pageName, SystemFonts.DefaultFont, Brushes.Black, new PointF(4f, 4f));
				ControlPaint.DrawCaptionButton(graphics, new Rectangle(theglobal.df.size.Width - 23, 5, 12, 12), CaptionButton.Close, ButtonState.Normal);
			}
			graphics.Dispose();
			Refresh();
		}
		catch
		{
			MessageBox.Show("重新绘制图形出现异常！", "提示");
		}
	}

	private void RefreshDHLJDataGridViewItems()
	{
		if (theglobal.SelectedShapeList.Count != 0)
		{
			if (theglobal.SelectedShapeList[0].ai)
			{
				theglobal.dataGridView.Rows[0].Cells[2].Value = theglobal.dataGridView.Rows[0].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[0].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].di)
			{
				theglobal.dataGridView.Rows[1].Cells[2].Value = theglobal.dataGridView.Rows[1].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[1].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].zfcsr)
			{
				theglobal.dataGridView.Rows[2].Cells[2].Value = theglobal.dataGridView.Rows[2].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[2].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].ao)
			{
				theglobal.dataGridView.Rows[3].Cells[2].Value = theglobal.dataGridView.Rows[3].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[3].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].doo)
			{
				theglobal.dataGridView.Rows[4].Cells[2].Value = theglobal.dataGridView.Rows[4].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[4].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].zfcsc)
			{
				theglobal.dataGridView.Rows[5].Cells[2].Value = theglobal.dataGridView.Rows[5].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[5].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].bxysbh)
			{
				theglobal.dataGridView.Rows[6].Cells[2].Value = theglobal.dataGridView.Rows[6].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[6].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].tcs1ysbh)
			{
				theglobal.dataGridView.Rows[7].Cells[2].Value = theglobal.dataGridView.Rows[7].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[7].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].tcs2ysbh)
			{
				theglobal.dataGridView.Rows[8].Cells[2].Value = theglobal.dataGridView.Rows[8].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[8].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].czbfb)
			{
				theglobal.dataGridView.Rows[9].Cells[2].Value = theglobal.dataGridView.Rows[9].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[9].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].spbfb)
			{
				theglobal.dataGridView.Rows[10].Cells[2].Value = theglobal.dataGridView.Rows[10].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[10].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].czyd)
			{
				theglobal.dataGridView.Rows[11].Cells[2].Value = theglobal.dataGridView.Rows[11].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[11].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].spyd)
			{
				theglobal.dataGridView.Rows[12].Cells[2].Value = theglobal.dataGridView.Rows[12].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[12].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].mbxz)
			{
				theglobal.dataGridView.Rows[13].Cells[2].Value = theglobal.dataGridView.Rows[13].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[13].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].kdbh)
			{
				theglobal.dataGridView.Rows[14].Cells[2].Value = theglobal.dataGridView.Rows[14].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[14].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].gdbh)
			{
				theglobal.dataGridView.Rows[15].Cells[2].Value = theglobal.dataGridView.Rows[15].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[15].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].txyc)
			{
				theglobal.dataGridView.Rows[16].Cells[2].Value = theglobal.dataGridView.Rows[16].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[16].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].ymqh)
			{
				theglobal.dataGridView.Rows[17].Cells[2].Value = theglobal.dataGridView.Rows[17].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[17].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].sptz)
			{
				theglobal.dataGridView.Rows[18].Cells[2].Value = theglobal.dataGridView.Rows[18].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[18].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].cztz)
			{
				theglobal.dataGridView.Rows[19].Cells[2].Value = theglobal.dataGridView.Rows[19].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[19].Cells[2].Value = "";
			}
			if (theglobal.SelectedShapeList[0].sbsj)
			{
				theglobal.dataGridView.Rows[20].Cells[2].Value = theglobal.dataGridView.Rows[20].Cells[0].Value;
			}
			else
			{
				theglobal.dataGridView.Rows[20].Cells[2].Value = "";
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
		this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem_Delete = new DevExpress.XtraBars.BarButtonItem();
		this.barEditItemControlName = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
		this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
		this.colorEditControl1 = new HMIEditEnvironment.ColorEditControl();
		this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
		this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
		this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
		this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem15 = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
		this.barButtonItem16 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem17 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem18 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem19 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem20 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem21 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem22 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem23 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
		this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemColorEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
		this.barButtonItem24 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem25 = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
		this.barButtonItem27 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem28 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem29 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem30 = new DevExpress.XtraBars.BarButtonItem();
		((System.ComponentModel.ISupportInitialize)this.popupMenu1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.colorEditControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit2).BeginInit();
		base.SuspendLayout();
		this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[14]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Delete),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEditItemControlName, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem5),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
			new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem6, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem10, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11)
		});
		this.popupMenu1.Manager = this.barManager1;
		this.popupMenu1.Name = "popupMenu1";
		this.popupMenu1.BeforePopup += new System.ComponentModel.CancelEventHandler(popupMenu1_BeforePopup);
		this.barButtonItem8.Caption = "精灵面板(&P)";
		this.barButtonItem8.Id = 56;
		this.barButtonItem8.Name = "barButtonItem8";
		this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem8_ItemClick);
		this.barButtonItem1.Caption = "对象动画(&A)";
		this.barButtonItem1.Id = 0;
		this.barButtonItem1.Name = "barButtonItem1";
		this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
		this.barButtonItem3.Caption = "复制(&C)";
		this.barButtonItem3.Id = 2;
		this.barButtonItem3.Name = "barButtonItem3";
		this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
		this.barButtonItem4.Caption = "剪切(&T)";
		this.barButtonItem4.Id = 3;
		this.barButtonItem4.Name = "barButtonItem4";
		this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem4_ItemClick);
		this.barButtonItem5.Caption = "粘贴(&P)";
		this.barButtonItem5.Id = 4;
		this.barButtonItem5.Name = "barButtonItem5";
		this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem5_ItemClick);
		this.barButtonItem_Delete.Caption = "删除(&D)";
		this.barButtonItem_Delete.Id = 62;
		this.barButtonItem_Delete.Name = "barButtonItem_Delete";
		this.barButtonItem_Delete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem_Delete_ItemClick);
		this.barEditItemControlName.Edit = this.repositoryItemTextEdit1;
		this.barEditItemControlName.Id = 15;
		this.barEditItemControlName.Name = "barEditItemControlName";
		this.barEditItemControlName.Width = 120;
		this.barEditItemControlName.EditValueChanged += new System.EventHandler(barEditItemControlName_EditValueChanged);
		this.repositoryItemTextEdit1.AutoHeight = false;
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		this.barSubItem1.Caption = "对象属性(&R)";
		this.barSubItem1.Id = 9;
		this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem12),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItem2, "", false, true, true, 30),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItem3, "", false, true, true, 50)
		});
		this.barSubItem1.Name = "barSubItem1";
		this.barSubItem1.Popup += new System.EventHandler(barSubItem1_Popup);
		this.barButtonItem12.ActAsDropDown = true;
		this.barButtonItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.barButtonItem12.Caption = "填充样式";
		this.barButtonItem12.DropDownControl = this.colorEditControl1;
		this.barButtonItem12.Id = 16;
		this.barButtonItem12.Name = "barButtonItem12";
		this.colorEditControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
		this.colorEditControl1.Appearance.Options.UseBackColor = true;
		this.colorEditControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
		this.colorEditControl1.Location = new System.Drawing.Point(4, 60);
		this.colorEditControl1.Manager = this.barManager1;
		this.colorEditControl1.Name = "colorEditControl1";
		this.colorEditControl1.Size = new System.Drawing.Size(150, 230);
		this.colorEditControl1.TabIndex = 6;
		this.colorEditControl1.Visible = false;
		this.barManager1.DockControls.Add(this.barDockControlTop);
		this.barManager1.DockControls.Add(this.barDockControlBottom);
		this.barManager1.DockControls.Add(this.barDockControlLeft);
		this.barManager1.DockControls.Add(this.barDockControlRight);
		this.barManager1.Form = this;
		this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[36]
		{
			this.barButtonItem1, this.barButtonItem3, this.barButtonItem4, this.barButtonItem5, this.barButtonItem6, this.barButtonItem7, this.barSubItem1, this.barSubItem2, this.barSubItem3, this.barButtonItem10,
			this.barButtonItem11, this.barEditItemControlName, this.barButtonItem12, this.barEditItem2, this.barEditItem3, this.barButtonItem13, this.barButtonItem14, this.barButtonItem15, this.barButtonItem16, this.barButtonItem17,
			this.barButtonItem18, this.barButtonItem19, this.barButtonItem20, this.barButtonItem21, this.barButtonItem22, this.barButtonItem23, this.barSubItem5, this.barButtonItem24, this.barButtonItem25, this.barSubItem6,
			this.barButtonItem27, this.barButtonItem28, this.barButtonItem29, this.barButtonItem30, this.barButtonItem8, this.barButtonItem_Delete
		});
		this.barManager1.MaxItemId = 69;
		this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[3] { this.repositoryItemTextEdit1, this.repositoryItemColorEdit1, this.repositoryItemColorEdit2 });
		this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
		this.barDockControlTop.Size = new System.Drawing.Size(463, 0);
		this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.barDockControlBottom.Location = new System.Drawing.Point(0, 302);
		this.barDockControlBottom.Size = new System.Drawing.Size(463, 0);
		this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
		this.barDockControlLeft.Size = new System.Drawing.Size(0, 302);
		this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
		this.barDockControlRight.Location = new System.Drawing.Point(463, 0);
		this.barDockControlRight.Size = new System.Drawing.Size(0, 302);
		this.barButtonItem6.Caption = "大小调整";
		this.barButtonItem6.Id = 5;
		this.barButtonItem6.Name = "barButtonItem6";
		this.barButtonItem7.Caption = "排列对齐";
		this.barButtonItem7.Id = 6;
		this.barButtonItem7.Name = "barButtonItem7";
		this.barSubItem2.Caption = "大小调整(&S)";
		this.barSubItem2.Id = 10;
		this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem13),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem14),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem15)
		});
		this.barSubItem2.Name = "barSubItem2";
		this.barButtonItem13.Caption = "相同宽度(&W)";
		this.barButtonItem13.Id = 19;
		this.barButtonItem13.Name = "barButtonItem13";
		this.barButtonItem13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem13_ItemClick);
		this.barButtonItem14.Caption = "相同高度(&H)";
		this.barButtonItem14.Id = 20;
		this.barButtonItem14.Name = "barButtonItem14";
		this.barButtonItem14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem14_ItemClick);
		this.barButtonItem15.Caption = "相同大小(&S)";
		this.barButtonItem15.Id = 21;
		this.barButtonItem15.Name = "barButtonItem15";
		this.barButtonItem15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem15_ItemClick);
		this.barSubItem3.Caption = "排列对齐(&N)";
		this.barSubItem3.Id = 11;
		this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[8]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem16),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem17),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem18),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem19),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem20),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem21),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem22, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem23)
		});
		this.barSubItem3.Name = "barSubItem3";
		this.barButtonItem16.Caption = "上对齐(&T)";
		this.barButtonItem16.Id = 22;
		this.barButtonItem16.Name = "barButtonItem16";
		this.barButtonItem16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem16_ItemClick);
		this.barButtonItem17.Caption = "水平对齐(&V)";
		this.barButtonItem17.Id = 23;
		this.barButtonItem17.Name = "barButtonItem17";
		this.barButtonItem17.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem17_ItemClick);
		this.barButtonItem18.Caption = "下对齐(&B)";
		this.barButtonItem18.Id = 24;
		this.barButtonItem18.Name = "barButtonItem18";
		this.barButtonItem18.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem18_ItemClick);
		this.barButtonItem19.Caption = "左对齐(&L)";
		this.barButtonItem19.Id = 25;
		this.barButtonItem19.Name = "barButtonItem19";
		this.barButtonItem19.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem19_ItemClick);
		this.barButtonItem20.Caption = "垂直对齐(&H)";
		this.barButtonItem20.Id = 26;
		this.barButtonItem20.Name = "barButtonItem20";
		this.barButtonItem20.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem20_ItemClick);
		this.barButtonItem21.Caption = "右对齐(&R)";
		this.barButtonItem21.Id = 27;
		this.barButtonItem21.Name = "barButtonItem21";
		this.barButtonItem21.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem21_ItemClick);
		this.barButtonItem22.Caption = "水平等间距";
		this.barButtonItem22.Id = 28;
		this.barButtonItem22.Name = "barButtonItem22";
		this.barButtonItem22.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem22_ItemClick);
		this.barButtonItem23.Caption = "垂直等间距";
		this.barButtonItem23.Id = 29;
		this.barButtonItem23.Name = "barButtonItem23";
		this.barButtonItem23.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem23_ItemClick);
		this.barButtonItem10.Caption = "页面事件(&E)";
		this.barButtonItem10.Id = 13;
		this.barButtonItem10.Name = "barButtonItem10";
		this.barButtonItem10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem10_ItemClick);
		this.barButtonItem11.Caption = "页面属性(&R)";
		this.barButtonItem11.Id = 14;
		this.barButtonItem11.Name = "barButtonItem11";
		this.barButtonItem11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem11_ItemClick);
		this.barEditItem2.Caption = "填充色  ";
		this.barEditItem2.Edit = this.repositoryItemColorEdit1;
		this.barEditItem2.Id = 17;
		this.barEditItem2.Name = "barEditItem2";
		this.barEditItem2.Width = 75;
		this.barEditItem2.EditValueChanged += new System.EventHandler(barEditItem2_EditValueChanged);
		this.repositoryItemColorEdit1.AutoHeight = false;
		this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
		this.barEditItem3.Caption = "边线颜色";
		this.barEditItem3.Edit = this.repositoryItemColorEdit2;
		this.barEditItem3.Id = 18;
		this.barEditItem3.Name = "barEditItem3";
		this.barEditItem3.Width = 75;
		this.barEditItem3.EditValueChanged += new System.EventHandler(barEditItem3_EditValueChanged);
		this.repositoryItemColorEdit2.AutoHeight = false;
		this.repositoryItemColorEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit2.Name = "repositoryItemColorEdit2";
		this.barSubItem5.Caption = "组合拆解(&G)";
		this.barSubItem5.Id = 30;
		this.barSubItem5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem24),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem25)
		});
		this.barSubItem5.Name = "barSubItem5";
		this.barButtonItem24.Caption = "组合(&G)";
		this.barButtonItem24.Id = 31;
		this.barButtonItem24.Name = "barButtonItem24";
		this.barButtonItem24.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem24_ItemClick);
		this.barButtonItem25.Caption = "拆解(&U)";
		this.barButtonItem25.Id = 32;
		this.barButtonItem25.Name = "barButtonItem25";
		this.barButtonItem25.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem25_ItemClick);
		this.barSubItem6.Caption = "层次与翻转(&L)";
		this.barSubItem6.Id = 34;
		this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem27),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem28),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem29, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem30)
		});
		this.barSubItem6.Name = "barSubItem6";
		this.barButtonItem27.Caption = "移动至顶层(&F)";
		this.barButtonItem27.Id = 35;
		this.barButtonItem27.Name = "barButtonItem27";
		this.barButtonItem27.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem27_ItemClick);
		this.barButtonItem28.Caption = "移动至底层(&B)";
		this.barButtonItem28.Id = 36;
		this.barButtonItem28.Name = "barButtonItem28";
		this.barButtonItem28.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem28_ItemClick);
		this.barButtonItem29.Caption = "水平翻转(&H)";
		this.barButtonItem29.Id = 37;
		this.barButtonItem29.Name = "barButtonItem29";
		this.barButtonItem29.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem29_ItemClick);
		this.barButtonItem30.Caption = "垂直翻转(&V)";
		this.barButtonItem30.Id = 38;
		this.barButtonItem30.Name = "barButtonItem30";
		this.barButtonItem30.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem30_ItemClick);
		this.AllowDrop = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Controls.Add(this.colorEditControl1);
		base.Controls.Add(this.barDockControlLeft);
		base.Controls.Add(this.barDockControlRight);
		base.Controls.Add(this.barDockControlBottom);
		base.Controls.Add(this.barDockControlTop);
		base.Name = "UserShapeEditControl";
		base.Size = new System.Drawing.Size(463, 302);
		base.Load += new System.EventHandler(UserControl2_Load);
		base.DragDrop += new System.Windows.Forms.DragEventHandler(UserShapeEditControl_DragDrop);
		base.DragEnter += new System.Windows.Forms.DragEventHandler(UserShapeEditControl_DragEnter);
		base.DragOver += new System.Windows.Forms.DragEventHandler(UserShapeEditControl_DragOver);
		base.DragLeave += new System.EventHandler(UserShapeEditControl_DragLeave);
		base.Paint += new System.Windows.Forms.PaintEventHandler(UserControl2_Paint);
		base.DoubleClick += new System.EventHandler(UserControl2_DoubleClick);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(UserControl2_KeyDown);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(UserControl2_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(UserControl2_MouseMove);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(UserControl2_MouseUp);
		((System.ComponentModel.ISupportInitialize)this.popupMenu1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.colorEditControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit2).EndInit();
		base.ResumeLayout(false);
	}
}