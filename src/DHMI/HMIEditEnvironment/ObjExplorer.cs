using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class ObjExplorer : UserControl
{
	public CGlobal m_ObjGbl;

	public ObjExplorerSortor m_ObjSortor = new ObjExplorerSortor();

	private IContainer components;

	private ImageList m_ImgListObj;

	private ListView m_ListViewObj;

	private ColumnHeader col_0;

	public ObjExplorer()
	{
		InitializeComponent();
		m_ListViewObj.Columns[0].TextAlign = HorizontalAlignment.Center;
	}

	private void ObjExplorer_Load(object sender, EventArgs e)
	{
		OnClear();
		m_ListViewObj.ListViewItemSorter = m_ObjSortor;
	}

	private string GetImgStr(CShape sp)
	{
		Type type = sp.GetType();
		string name = type.Name;
		switch (name)
		{
		case "CString":
			return "String.png";
		case "CLine":
		case "CBezier":
			return "Lines.png";
		case "CEllipse":
			return "Ellipse.png";
		case "CRectangle":
			return "Rectangle.png";
		case "CCircleRect":
			return "crectsmall.png";
		case "CCloseLines":
			return "CloseLine.png";
		case "CVectorGraph":
			return "Memory.png";
		default:
			if (name.StartsWith("drawbitmap"))
			{
				return "Library.png";
			}
			if (name == "CControl")
			{
				return "Memory.png";
			}
			return "Memory.png";
		}
	}

	public void OnShow()
	{
		if (m_ObjGbl == null)
		{
			return;
		}
		m_ListViewObj.BeginUpdate();
		m_ListViewObj.Items.Clear();
		foreach (CShape item in m_ObjGbl.g_ListAllShowCShape)
		{
			m_ListViewObj.Items.Add(item.ShapeID.ToString(), item.Name, GetImgStr(item));
		}
		m_ListViewObj.EndUpdate();
	}

	public void OnFresh(string strGUID)
	{
		if (m_ObjGbl == null)
		{
			m_ListViewObj.Items.Clear();
		}
		else if (strGUID == null || strGUID == "")
		{
			OnShow();
		}
		else
		{
			CShape shapeByKey = m_ObjGbl.GetShapeByKey(strGUID);
			ListViewItem[] array = m_ListViewObj.Items.Find(strGUID, searchAllSubItems: true);
			if (array == null || array.Length == 0)
			{
				if (shapeByKey != null)
				{
					m_ListViewObj.Items.Add(shapeByKey.ShapeID.ToString(), shapeByKey.Name, GetImgStr(shapeByKey));
				}
			}
			else if (shapeByKey == null)
			{
				m_ListViewObj.Items.RemoveByKey(strGUID);
			}
			else
			{
				array[0].Text = shapeByKey.Name;
			}
		}
		m_ListViewObj.Sort();
		m_ListViewObj.SelectedItems.Clear();
		if (m_ListViewObj.Items.Count > 2)
		{
			m_ListViewObj.Items[2].Selected = true;
			m_ListViewObj.EnsureVisible(2);
		}
		FreshSelect(m_ObjGbl);
	}

	public void FreshSelect(CGlobal gb)
	{
		if (m_ObjGbl == null && gb == null)
		{
			return;
		}
		if (m_ObjGbl == null && gb != null)
		{
			m_ObjGbl = gb;
			OnShow();
		}
		if (m_ListViewObj.Items.Count == 0)
		{
			return;
		}
		try
		{
			m_ListViewObj.SelectedItems.Clear();
			foreach (ListViewItem item in m_ListViewObj.Items)
			{
				item.BackColor = SystemColors.Window;
				item.ForeColor = Color.Black;
			}
			foreach (CShape selectedShape in m_ObjGbl.SelectedShapeList)
			{
				ListViewItem[] array = m_ListViewObj.Items.Find(selectedShape.ShapeID.ToString(), searchAllSubItems: true);
				if (array != null && array.Length > 0)
				{
					m_ListViewObj.EnsureVisible(array[0].Index);
					array[0].BackColor = SystemColors.Highlight;
					array[0].ForeColor = Color.White;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void FreshAll()
	{
		OnShow();
		FreshSelect(m_ObjGbl);
	}

	public void OnClear()
	{
		m_ListViewObj.Items.Clear();
		m_ObjGbl = null;
	}

	private void m_ListViewObj_Resize(object sender, EventArgs e)
	{
		col_0.Width = m_ListViewObj.Size.Width - 25;
	}

	private void m_ListViewObj_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		if (e.Column == 0)
		{
			m_ObjSortor.m_bSortType = !m_ObjSortor.m_bSortType;
			m_ListViewObj.Sort();
		}
	}

	private void m_ListViewObj_Validated(object sender, EventArgs e)
	{
		FreshSelect(m_ObjGbl);
	}

	private void m_ListViewObj_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button != MouseButtons.Left || m_ListViewObj.FocusedItem == null)
		{
			return;
		}
		try
		{
			int count = m_ListViewObj.FocusedItem.SubItems.Count;
			if (count <= 0)
			{
				return;
			}
			CShape shapeByKey = m_ObjGbl.GetShapeByKey(m_ListViewObj.FocusedItem.SubItems[0].Name);
			if (shapeByKey != null)
			{
				CShape selectShapeByKey = m_ObjGbl.GetSelectShapeByKey(m_ListViewObj.FocusedItem.SubItems[0].Name);
				if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
				{
					m_ObjGbl.SelectedShapeList.Clear();
					m_ListViewObj.SelectedItems.Clear();
				}
				if (selectShapeByKey != null)
				{
					m_ObjGbl.SelectedShapeList.Remove(selectShapeByKey);
				}
				else
				{
					m_ObjGbl.SelectedShapeList.Add(shapeByKey);
				}
				m_ObjGbl.uc2.RefreshGraphics();
				m_ObjGbl.SLS_Changed(sender, null);
				FreshSelect(m_ObjGbl);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void m_ListViewObj_MouseUp(object sender, MouseEventArgs e)
	{
		e = new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X), Convert.ToInt32(e.Y), e.Delta);
		if (e.Button != MouseButtons.Right || m_ListViewObj.FocusedItem == null)
		{
			return;
		}
		if (m_ListViewObj.FocusedItem.SubItems.Count > 0)
		{
			CShape shapeByKey = m_ObjGbl.GetShapeByKey(m_ListViewObj.FocusedItem.SubItems[0].Name);
			if (shapeByKey != null)
			{
				CShape selectShapeByKey = m_ObjGbl.GetSelectShapeByKey(m_ListViewObj.FocusedItem.SubItems[0].Name);
				if (selectShapeByKey == null)
				{
					m_ObjGbl.SelectedShapeList.Clear();
					m_ListViewObj.SelectedItems.Clear();
					m_ObjGbl.SelectedShapeList.Add(shapeByKey);
					m_ObjGbl.uc2.RefreshGraphics();
					m_ObjGbl.SLS_Changed(sender, null);
					FreshSelect(m_ObjGbl);
				}
			}
		}
		if (m_ObjGbl == null || m_ObjGbl.SelectedShapeList.Count == 0)
		{
			return;
		}
		try
		{
			new Point(e.X, e.Y);
			Cursor = Cursors.Default;
			m_ObjGbl.uc2.popupMenu1.ShowPopup(Cursor.Position);
		}
		catch
		{
		}
	}

	private void m_ListViewObj_SelectedIndexChanged(object sender, EventArgs e)
	{
		m_ListViewObj.SelectedItems.Clear();
	}

	private void m_ListViewObj_KeyDown(object sender, KeyEventArgs e)
	{
		if (m_ObjGbl == null || m_ListViewObj.Items.Count == 0 || e.KeyCode < Keys.A || e.KeyCode > Keys.Z)
		{
			return;
		}
		string text = e.KeyCode.ToString().ToUpper();
		int count = m_ListViewObj.Items.Count;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < count; i++)
		{
			if (m_ListViewObj.Items[i].BackColor == SystemColors.Highlight && m_ListViewObj.Items[i].ForeColor == Color.White)
			{
				num = i + 1;
				break;
			}
		}
		while (true)
		{
			if (num < count)
			{
				string text2 = m_ListViewObj.Items[num].Text.ToUpper();
				text2 = text2[0].ToString();
				if (text2 == text)
				{
					CShape shapeByKey = m_ObjGbl.GetShapeByKey(m_ListViewObj.Items[num].Name);
					m_ObjGbl.SelectedShapeList.Clear();
					m_ObjGbl.SelectedShapeList.Add(shapeByKey);
					m_ObjGbl.uc2.RefreshGraphics();
					m_ObjGbl.SLS_Changed(sender, null);
					FreshSelect(m_ObjGbl);
					break;
				}
				num++;
			}
			else
			{
				if (num2 > 0)
				{
					break;
				}
				num2 = 1;
				num = 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.ObjExplorer));
		System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem("1", "String.png");
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("10", "PictureEffectsShadowGallery.png");
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("11", "String.png");
		System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("12", "String.png");
		System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("13", "String.png");
		System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("14", "String.png");
		System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("15", "String.png");
		System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("16", "String.png");
		System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("2", "Memory.png");
		System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("3", "Lines.png");
		System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("4", "ControlToolboxOutlook.png");
		System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("5", "crectsmall.png");
		System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("6", "Ellipse.png");
		System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("7", "CloseLine.png");
		System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("8", "Rectangle.png");
		System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("9", "Library.png");
		this.m_ImgListObj = new System.Windows.Forms.ImageList(this.components);
		this.m_ListViewObj = new System.Windows.Forms.ListView();
		this.col_0 = new System.Windows.Forms.ColumnHeader();
		base.SuspendLayout();
		this.m_ImgListObj.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_ImgListObj.ImageStream");
		this.m_ImgListObj.TransparentColor = System.Drawing.Color.Transparent;
		//this.m_ImgListObj.Images.SetKeyName(0, "String.png");
		//this.m_ImgListObj.Images.SetKeyName(1, "Memory.png");
		//this.m_ImgListObj.Images.SetKeyName(2, "Lines.png");
		//this.m_ImgListObj.Images.SetKeyName(3, "ControlToolboxOutlook.png");
		//this.m_ImgListObj.Images.SetKeyName(4, "crectsmall.png");
		//this.m_ImgListObj.Images.SetKeyName(5, "Ellipse.png");
		//this.m_ImgListObj.Images.SetKeyName(6, "CloseLine.png");
		//this.m_ImgListObj.Images.SetKeyName(7, "Rectangle.png");
		//this.m_ImgListObj.Images.SetKeyName(8, "Library.png");
		//this.m_ImgListObj.Images.SetKeyName(9, "PictureEffectsShadowGallery.png");
		this.m_ListViewObj.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.m_ListViewObj.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.col_0 });
		this.m_ListViewObj.FullRowSelect = true;
		this.m_ListViewObj.GridLines = true;
		listViewItem.UseItemStyleForSubItems = false;
		this.m_ListViewObj.Items.AddRange(new System.Windows.Forms.ListViewItem[16]
		{
			listViewItem, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6, listViewItem7, listViewItem8, listViewItem9, listViewItem10,
			listViewItem11, listViewItem12, listViewItem13, listViewItem14, listViewItem15, listViewItem16
		});
		this.m_ListViewObj.Location = new System.Drawing.Point(0, 3);
		this.m_ListViewObj.Name = "m_ListViewObj";
		this.m_ListViewObj.Size = new System.Drawing.Size(187, 281);
		this.m_ListViewObj.SmallImageList = this.m_ImgListObj;
		this.m_ListViewObj.TabIndex = 0;
		this.m_ListViewObj.UseCompatibleStateImageBehavior = false;
		this.m_ListViewObj.View = System.Windows.Forms.View.Details;
		this.m_ListViewObj.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(m_ListViewObj_ColumnClick);
		this.m_ListViewObj.SelectedIndexChanged += new System.EventHandler(m_ListViewObj_SelectedIndexChanged);
		this.m_ListViewObj.KeyDown += new System.Windows.Forms.KeyEventHandler(m_ListViewObj_KeyDown);
		this.m_ListViewObj.MouseClick += new System.Windows.Forms.MouseEventHandler(m_ListViewObj_MouseClick);
		this.m_ListViewObj.MouseUp += new System.Windows.Forms.MouseEventHandler(m_ListViewObj_MouseUp);
		this.m_ListViewObj.Resize += new System.EventHandler(m_ListViewObj_Resize);
		this.m_ListViewObj.Validated += new System.EventHandler(m_ListViewObj_Validated);
		this.col_0.Text = "页面元素";
		this.col_0.Width = 100;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.Controls.Add(this.m_ListViewObj);
		base.Name = "ObjExplorer";
		base.Size = new System.Drawing.Size(215, 316);
		base.Load += new System.EventHandler(ObjExplorer_Load);
		base.ResumeLayout(false);
	}
}
