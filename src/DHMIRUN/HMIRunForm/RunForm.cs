using HMIRunForm.Properties;
using HMIRunForm.ServerLogic;
using HMIWeb;
using LogHelper;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace HMIRunForm;

public partial class RunForm : Form
{
	public struct COPYDATASTRUCT2
	{
		public IntPtr dwData;

		public int cbData;

		public IntPtr lpData;
	}

	public const int SWP_HIDEWINDOW = 128;

	public const int SWP_SHOWWINDOW = 64;

	public const uint SPI_SETSCREENSAVEACTIVE = 17u;

	private readonly MainControl MainControl = new();

	private readonly object inDBOperation = new();

	public string sleepTime;

	private readonly PrintDocument printdoc = new();

	private readonly PrintDialog printdialog = new();

	private Bitmap btmaip;

	private DataRow tagdr;

	private bool needRefreshDBFactory = true;

	private DbProviderFactory factory;

	private DbConnection conn;

	private DbCommand command;

	private DbDataAdapter adapter;

	private string dbProviderName = "SqlClient Data Provider";

	private string dbConnString = "";

	private CAuthoritySeiallize cas = new();

	private Panel TopPanel;

	private MenuStrip menuStrip;

	private ToolStripMenuItem 截图ToolStripMenuItem;

	private ToolStripMenuItem 保存到文件ToolStripMenuItem;

	private ToolStripMenuItem 视图ToolStripMenuItem;

	private ToolStripMenuItem 全屏ToolStripMenuItem;

	private ToolStripMenuItem 标准ToolStripMenuItem;

	private ToolStripMenuItem 打印ToolStripMenuItem;

	private ToolStripMenuItem 工具ToolStripMenuItem;

	private ToolStripMenuItem 屏幕键盘ToolStripMenuItem;

	private ToolStripMenuItem 计算其ToolStripMenuItem;

	private ToolStripMenuItem 数据库ToolStripMenuItem;

	private ToolStripMenuItem 数据库管理ToolStripMenuItem;

	public string DbProviderName
	{
		get
		{
			return dbProviderName;
		}
		set
		{
			dbProviderName = value;
		}
	}

	public string DbConnString
	{
		get
		{
			return dbConnString;
		}
		set
		{
			dbConnString = value;
		}
	}

	[DllImport("user32.dll")]
	private static extern IntPtr FindWindowEx(int hwnd2, int hWnd2, string lpsz1, string lpsz2);

	[DllImport("user32.dll")]
	private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

	[DllImport("User32")]
	public static extern bool SystemParametersInfoW(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

	[DllImport("user32")]
	public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

	private void MainControl_FullScreen(object sender, EventArgs e)
	{
		Screen screen = Screen.FromPoint(base.Location);
		if (screen.Primary)
		{
			SetWindowPos(FindWindowEx(0, 0, "Shell_TrayWnd", "").ToInt32(), 0, 0, 0, 0, 0, 128);
		}
		SetVisibleCore(value: false);
		base.FormBorderStyle = FormBorderStyle.None;
		base.Size = screen.Bounds.Size;
		base.Location = screen.Bounds.Location;
		SetVisibleCore(value: true);
		TopPanel.Visible = true;
		menuStrip.Visible = false;
		SystemParametersInfoW(17u, 0u, IntPtr.Zero, 0u);
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			if (ProjectStartForm.bProjectStartClose)
			{
				e.Cancel = false;
				return;
			}
			string text = MainControl.dhp.EnvironmentPath + "\\Authority.data";
			if (!File.Exists(text))
			{
				return;
			}
			CAuthoritySeiallize cAuthoritySeiallize = new();
			BinarySerialize<CAuthoritySeiallize> binarySerialize = new();
			cAuthoritySeiallize = binarySerialize.DeSerialize(text);
			if (cAuthoritySeiallize != null && cAuthoritySeiallize.bProjectEnd)
			{
				ProjectEndForm projectEndForm = new(cAuthoritySeiallize);
				DialogResult dialogResult = projectEndForm.ShowDialog();
				if (dialogResult == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				if (ProjectEndForm.bProjectEndForm)
				{
					e.Cancel = false;
				}
			}
		}
		catch
		{
			MessageBox.Show("工程权限初始化出现异常！", "提示");
		}
	}

	private object MainControl_CallRuntimeDBOperation(string operationType, string sqlcmd)
	{
		lock (inDBOperation)
		{
			int num = 0;
			while (true)
			{
				try
				{
					if (needRefreshDBFactory)
					{
						RefreshDBFactory();
					}
					command.CommandText = sqlcmd;
					object result;
					if (operationType == "select")
					{
						if (conn.State != ConnectionState.Open)
						{
							conn.Open();
						}
						adapter.SelectCommand = command;
						DataSet dataSet = new();
						adapter.Fill(dataSet);
						result = dataSet;
					}
					else
					{
						if (conn.State != ConnectionState.Open)
						{
							conn.Open();
						}
						int num2 = command.ExecuteNonQuery();
						result = num2;
					}
					return result;
				}
				catch (NullReferenceException result2)
				{
					needRefreshDBFactory = true;
					if (num++ <= 3)
					{
						continue;
					}
					return result2;
				}
				catch (InvalidOperationException result3)
				{
					needRefreshDBFactory = true;
					if (num++ <= 3)
					{
						continue;
					}
					return result3;
				}
				catch (Exception result4)
				{
					return result4;
				}
			}
		}
	}

	private void MainControl_InitOK(object sender, EventArgs e)
	{
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.ClientSize = new Size(MainControl.dhp.ProjectSize.Width, MainControl.dhp.ProjectSize.Height);
		base.StartPosition = FormStartPosition.CenterScreen;
		Screen screen = Screen.FromPoint(base.Location);
		Size size = screen.Bounds.Size;
		int num = (size.Width - base.Size.Width) / 2;
		int num2 = (size.Height - base.Size.Height) / 2;
		if (num <= 0 || num2 <= 0 || MainControl.dhp.autofull)
		{
			MainControl_FullScreen(sender, e);
		}
		else
		{
			base.Location = new Point(num, num2);
		}
		Text = MainControl.Name;
		BackColor = MainControl.BackColor;
		if (MainControl.dead)
		{
			return;
		}
		ProjectAuthorityStarter();
	}

	private void Printdoc_PrintPage(object sender, PrintPageEventArgs e)
	{
		try
		{
			Rectangle marginBounds = e.MarginBounds;
			float num = ((Convert.ToSingle(marginBounds.Width) / Convert.ToSingle(MainControl.dhp.ProjectSize.Width) < Convert.ToSingle(marginBounds.Height) / Convert.ToSingle(MainControl.dhp.ProjectSize.Height)) ? (Convert.ToSingle(marginBounds.Width) / Convert.ToSingle(MainControl.dhp.ProjectSize.Width)) : (Convert.ToSingle(marginBounds.Height) / Convert.ToSingle(MainControl.dhp.ProjectSize.Height)));
			RectangleF rect = new(e.MarginBounds.Location, new SizeF((float)MainControl.dhp.ProjectSize.Width * num, (float)MainControl.dhp.ProjectSize.Height * num));
			rect.Location = new PointF((float)(e.PageBounds.Width / 2) - rect.Width / 2f, (float)(e.PageBounds.Height / 2) - rect.Height / 2f);
			e.Graphics.DrawImage(btmaip, rect);
		}
		catch
		{
			e.Cancel = true;
		}
	}

	public void CloseRunForm(object sender, EventArgs e)
	{
		Close();
	}

	private void RunForm_Load(object sender, EventArgs e)
	{
        LogUtil.Init();

        Type type = Type.GetType("LogicPage.Globle,CustomLogic");
        Activator.CreateInstance(type);
        ResourceManager resourceManager = (ResourceManager)type.GetField("rm").GetValue(null);
        string name = (string)type.GetField("projectname").GetValue(null);
        MemoryStream memoryStream = null;
        byte[] input = resourceManager.GetObject(name) as byte[];
        memoryStream = new MemoryStream(Operation.UncompressStream(input));
        IFormatter formatter = new BinaryFormatter();
        HMIProjectFile hMIProjectFile = (HMIProjectFile)formatter.Deserialize(memoryStream);
        memoryStream.Close();
        DbProviderName = hMIProjectFile.dbCfgPara.DBType;
        DbConnString = hMIProjectFile.dbCfgPara.DBConnStr;
        sleepTime = hMIProjectFile.gDXP_SleepTime;
        printdoc.PrintPage += Printdoc_PrintPage;
        printdialog.Document = printdoc;
        MainControl.InitOK += MainControl_InitOK;
        MainControl.CallRuntimeDBOperation += MainControl_CallRuntimeDBOperation;
        MainControl.CallRuntimeServerLogic += MainControl_CallRuntimeServerLogic;
        MainControl.FullScreenEvent += MainControl_FullScreen;
        base.Controls.Add(MainControl);
        MainControl.BringToFront();
        TopPanel.BringToFront();
        MainControl.Dock = DockStyle.Fill;

        MainControl.BeginInit();
	}

	private void TopPanel_MouseEnter(object sender, EventArgs e)
	{
		TopPanel.Height = 20;
		TopPanel.Invalidate();
	}

	private void TopPanel_MouseLeave(object sender, EventArgs e)
	{
		TopPanel.Height = 1;
		TopPanel.Invalidate();
	}

	private void TopPanel_Paint(object sender, PaintEventArgs e)
	{
		if (TopPanel.Height > 1)
		{
			e.Graphics.DrawImage(Resources.GroupColorModeClose, new Point(TopPanel.Width - 18, TopPanel.Height - 18));
			e.Graphics.DrawImage(Resources.ChartRefresh, new Point(TopPanel.Width - 38, TopPanel.Height - 18));
		}
	}

	private void TopPanel_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.X >= TopPanel.Width - 20 && e.X <= TopPanel.Width && e.Y >= TopPanel.Height - 20 && e.Y <= TopPanel.Height)
		{
			Close();
		}
		if (e.X >= TopPanel.Width - 40 && e.X <= TopPanel.Width - 20 && e.Y >= TopPanel.Height - 20 && e.Y <= TopPanel.Height && MessageBox.Show("是否确定退出全屏模式?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			标准ToolStripMenuItem_Click(sender, e);
		}
	}

	private void 数据库管理ToolStripMenuItem_Click(object sender, EventArgs e)
	{
        DBConnForm dBConnForm = new()
        {
            DbType = DbProviderName,
            DbConnStr = DbConnString
        };
        if (dBConnForm.ShowDialog() == DialogResult.OK)
		{
			DbProviderName = dBConnForm.DbType;
			DbConnString = dBConnForm.DbConnStr;
			needRefreshDBFactory = true;
		}
	}

	private void 保存到文件ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		btmaip = new Bitmap(MainControl.Width, MainControl.Height);
		MainControl.DrawToBitmap(btmaip, new Rectangle(0, 0, btmaip.Width, btmaip.Height));
        SaveFileDialog saveFileDialog = new()
        {
            AddExtension = true,
            DefaultExt = ".bmp",
            Filter = "图片文件(*.jpg;*.tiff;*.bmp;*.png)|*.jpg;*.tiff;*.bmp;*.png|所有文件(*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			btmaip.Save(saveFileDialog.FileName);
		}
	}

	private void 全屏ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Screen screen = Screen.FromPoint(base.Location);
		if (screen.Primary)
		{
			SetWindowPos(FindWindowEx(0, 0, "Shell_TrayWnd", "").ToInt32(), 0, 0, 0, 0, 0, 128);
		}
		SetVisibleCore(value: false);
		base.FormBorderStyle = FormBorderStyle.None;
		base.Size = screen.Bounds.Size;
		base.Location = screen.Bounds.Location;
		SetVisibleCore(value: true);
		TopPanel.Visible = true;
		menuStrip.Visible = false;
		SystemParametersInfoW(17u, 0u, IntPtr.Zero, 0u);
	}

	private void 标准ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SetWindowPos(FindWindowEx(0, 0, "Shell_TrayWnd", "").ToInt32(), 0, 200, 200, 0, 0, 64);
		SetVisibleCore(value: false);
		base.WindowState = FormWindowState.Normal;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		Size clientSize = (base.ClientSize = new Size(MainControl.dhp.ProjectSize.Width, MainControl.dhp.ProjectSize.Height + 24));
		base.ClientSize = clientSize;
		SetVisibleCore(value: true);
		TopPanel.Visible = false;
		SystemParametersInfoW(17u, 1u, IntPtr.Zero, 0u);
	}

	private void 屏幕键盘ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start(Environment.SystemDirectory + "\\osk.exe");
	}

	private void 计算其ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start(Environment.SystemDirectory + "\\calc.exe");
	}

	private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		btmaip = new Bitmap(MainControl.Width, MainControl.Height);
		MainControl.DrawToBitmap(btmaip, new Rectangle(0, 0, btmaip.Width, btmaip.Height));
		if (printdialog.ShowDialog() == DialogResult.OK)
		{
			printdoc.Print();
		}
	}

	public void ProjectAuthorityStarter()
	{
		try
		{
			string text = MainControl.dhp.EnvironmentPath + "\\Authority.data";
			if (File.Exists(text))
			{
				BinarySerialize<CAuthoritySeiallize> binarySerialize = new();
				cas = binarySerialize.DeSerialize(text);
				if (cas != null && cas.bProjectStart)
				{
					ProjectStartForm projectStartForm = new(cas);
					projectStartForm.EventCloseForm = (EventHandler)Delegate.Combine(projectStartForm.EventCloseForm, new EventHandler(CloseRunForm));
					projectStartForm.ShowDialog();
					MainControl.cas = cas;
					MainControl.strCurrentUser = projectStartForm.strCurrentUser;
				}
			}
		}
		catch
		{
			MessageBox.Show("工程权限初始化出现异常！", "提示");
		}
	}

	private void RefreshDBFactory()
	{
		conn?.Dispose();
		command?.Dispose();
		adapter?.Dispose();
		DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
		tagdr = null;
		foreach (DataRow row in factoryClasses.Rows)
		{
			if (row[0].ToString() == dbProviderName)
			{
				tagdr = row;
				break;
			}
		}
		if (tagdr == null)
		{
			throw new Exception("数据库连接错误:无法创建提供程序对数据源类的实现的实例");
		}
		factory = DbProviderFactories.GetFactory(tagdr);
		conn = factory.CreateConnection();
		conn.ConnectionString = dbConnString;
		conn.Open();
		command = factory.CreateCommand();
		command.Connection = conn;
		adapter = factory.CreateDataAdapter();
		needRefreshDBFactory = false;
	}

	private Dictionary<string, object> MainControl_CallRuntimeServerLogic(ServerLogicRequest slr)
	{
		return ServerLogicWorker.GetWorker(MainControl_CallRuntimeDBOperation).DoWork(slr.InputDict, slr.LogicItemList, slr.OutputList);
	}

	public RunForm()
	{
		InitializeComponent();
	}

	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if (width < Screen.PrimaryScreen.Bounds.Size.Width && height < Screen.PrimaryScreen.Bounds.Size.Height)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}
		else
		{
			SetWindowPos((int)base.Handle, 0, x, y, width, height, 20);
		}
	}
}
