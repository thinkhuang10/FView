using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using HMIRunForm.Properties;
using HMIRunForm.ServerLogic;
using HMIWeb;
using ShapeRuntime;

namespace HMIRunForm;

public class RunForm : Form
{
	public struct COPYDATASTRUCT
	{
		public IntPtr dwData;

		public int cbData;

		[MarshalAs(UnmanagedType.LPStr)]
		public string lpData;
	}

	public struct COPYDATASTRUCT2
	{
		public IntPtr dwData;

		public int cbData;

		public IntPtr lpData;
	}

	public const int SWP_HIDEWINDOW = 128;

	public const int SWP_SHOWWINDOW = 64;

	public const uint SPI_SETSCREENSAVEACTIVE = 17u;

	private const int WM_COPYDATA = 74;

	private MainControl MainControl = new MainControl();

	private object inDBOperation = new object();

	private int count;

	public string sleepTime;

	private PrintDocument printdoc = new PrintDocument();

	private PrintDialog printdialog = new PrintDialog();

	private Bitmap btmaip;

	private DataRow tagdr;

	private bool needRefreshDBFactory = true;

	private DbProviderFactory factory;

	private DbConnection conn;

	private DbCommand command;

	private DbDataAdapter adapter;

	private string dbProviderName = "SqlClient Data Provider";

	private string dbConnString = "";

	private int hwnd;

	private CAuthoritySeiallize cas = new CAuthoritySeiallize();

	private IContainer components;

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

	[DllImport("User32.dll")]
	private static extern int FindWindow([MarshalAs(UnmanagedType.LPStr)] string lpClassName, [MarshalAs(UnmanagedType.LPStr)] string lpWindowName);

	[DllImport("User32.dll")]
	private static extern int SendMessageTimeout(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam, int fuFlags, int uTimeout, int lpdwResult);

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
			CAuthoritySeiallize cAuthoritySeiallize = new CAuthoritySeiallize();
			BinarySerialize<CAuthoritySeiallize> binarySerialize = new BinarySerialize<CAuthoritySeiallize>();
			cAuthoritySeiallize = binarySerialize.DeSerialize(text);
			if (cAuthoritySeiallize != null && cAuthoritySeiallize.bProjectEnd)
			{
				ProjectEndForm projectEndForm = new ProjectEndForm(cAuthoritySeiallize);
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
						refreshDBFactory();
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
						DataSet dataSet = new DataSet();
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
		hwnd = FindWindow(null, "DXP");
		if (hwnd != 0)
		{
			return;
		}
		try
		{
			new DirectoryInfo(MainControl.installpath.Replace("file:///", ""));
			Process process = new Process();
			string arguments = "";
			if (MainControl.dhp.RunEvnironment)
			{
				arguments = "ProjRun?1|";
			}
			ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + (AppDomain.CurrentDomain.BaseDirectory.EndsWith("\\") ? "..\\..\\DXP.exe" : "\\..\\..\\DXP.exe"), arguments);
			process.StartInfo = startInfo;
			process.Start();
		}
		catch (Exception)
		{
			try
			{
				Process process2 = new Process();
				string arguments2 = "";
				if (MainControl.dhp.RunEvnironment)
				{
					arguments2 = "ProjRun?1|";
				}
				ProcessStartInfo startInfo2 = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + (AppDomain.CurrentDomain.BaseDirectory.EndsWith("\\") ? "..\\..\\Bin\\DXP.exe" : "\\..\\..\\Bin\\DXP.exe"), arguments2);
				process2.StartInfo = startInfo2;
				process2.Start();
				string text = sleepTime;
				MessageBox.Show("数据将会加载" + text + "ms.请耐心等待！");
				DateTime now = DateTime.Now;
				while (now.AddSeconds(int.Parse(text)) > DateTime.Now)
				{
				}
				Timer timer = new Timer();
				timer.Interval = 1000;
				timer.Tick += timer_Tick;
				timer.Enabled = true;
			}
			catch (Exception)
			{
				MessageBox.Show("启动DXP失败,请手动启动,并加载相关工程。");
			}
		}
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		if ((hwnd = FindWindow(null, "DXP")) != 0)
		{
			COPYDATASTRUCT lParam = default;
			DirectoryInfo directoryInfo = new DirectoryInfo(MainControl.projectpath.Replace("file:///", ""));
			string text = directoryInfo.Parent.FullName + "\\" + MainControl.projectname.Replace(".dhp", "") + ".dsl";
			if (!File.Exists(text))
			{
				text = AppDomain.CurrentDomain.BaseDirectory + "\\..\\" + MainControl.projectname.Replace(".dhp", "") + ".dsl";
				if (!File.Exists(text))
				{
					((Timer)sender).Enabled = false;
					return;
				}
			}
			byte[] bytes = Encoding.Default.GetBytes(text);
			int num = bytes.Length;
			lParam.dwData = (IntPtr)3000;
			lParam.cbData = num + 1;
			lParam.lpData = text;
			SendMessageTimeout(hwnd, 74, (int)base.Handle, ref lParam, 0, 1000, 0);
			((Timer)sender).Enabled = false;
		}
		else
		{
			count++;
			if (count > 3)
			{
				((Timer)sender).Enabled = false;
				MessageBox.Show("未能打开DXP");
			}
		}
	}

	private void printdoc_PrintPage(object sender, PrintPageEventArgs e)
	{
		try
		{
			Rectangle marginBounds = e.MarginBounds;
			float num = ((Convert.ToSingle(marginBounds.Width) / Convert.ToSingle(MainControl.dhp.ProjectSize.Width) < Convert.ToSingle(marginBounds.Height) / Convert.ToSingle(MainControl.dhp.ProjectSize.Height)) ? (Convert.ToSingle(marginBounds.Width) / Convert.ToSingle(MainControl.dhp.ProjectSize.Width)) : (Convert.ToSingle(marginBounds.Height) / Convert.ToSingle(MainControl.dhp.ProjectSize.Height)));
			RectangleF rect = new RectangleF(e.MarginBounds.Location, new SizeF((float)MainControl.dhp.ProjectSize.Width * num, (float)MainControl.dhp.ProjectSize.Height * num));
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
		DBConnForm dBConnForm = new DBConnForm();
		dBConnForm.DbType = DbProviderName;
		dBConnForm.DbConnStr = DbConnString;
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
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.AddExtension = true;
		saveFileDialog.DefaultExt = ".bmp";
		saveFileDialog.Filter = "图片文件(*.jpg;*.tiff;*.bmp;*.png)|*.jpg;*.tiff;*.bmp;*.png|所有文件(*.*)|*.*";
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

	protected override void DefWndProc(ref Message m)
	{
		int msg = m.Msg;
		if (msg == 74)
		{
			Type type = default(COPYDATASTRUCT2).GetType();
			COPYDATASTRUCT2 data = (COPYDATASTRUCT2)m.GetLParam(type);
			OnCopyData(data);
		}
		else
		{
			base.DefWndProc(ref m);
		}
	}

	private void OnCopyData(COPYDATASTRUCT2 data)
	{
		try
		{
			if ((int)data.dwData != 3010)
			{
				return;
			}
			byte[] array = new byte[data.cbData];
			Marshal.Copy(data.lpData, array, 0, array.Length);
			string @string = Encoding.GetEncoding("gb2312").GetString(array, 0, array.Length);
			string[] array2 = @string.Split('|');
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != "" && array2[i].Contains("?"))
				{
					string[] array3 = array2[i].Trim().Split('?');
					dictionary.Add(array3[0], array3[1]);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void ProjectAuthorityStarter()
	{
		try
		{
			string text = MainControl.dhp.EnvironmentPath + "\\Authority.data";
			if (File.Exists(text))
			{
				BinarySerialize<CAuthoritySeiallize> binarySerialize = new BinarySerialize<CAuthoritySeiallize>();
				cas = binarySerialize.DeSerialize(text);
				if (cas != null && cas.bProjectStart)
				{
					ProjectStartForm projectStartForm = new ProjectStartForm(cas);
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

	private void refreshDBFactory()
	{
		if (conn != null)
		{
			conn.Dispose();
		}
		if (command != null)
		{
			command.Dispose();
		}
		if (adapter != null)
		{
			adapter.Dispose();
		}
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
		printdoc.PrintPage += printdoc_PrintPage;
		printdialog.Document = printdoc;
		MainControl.InitOK += MainControl_InitOK;
		MainControl.CallRuntimeDBOperation += MainControl_CallRuntimeDBOperation;
		MainControl.CallRuntimeServerLogic += MainControl_CallRuntimeServerLogic;
		MainControl.FullScreenEvent += MainControl_FullScreen;
		base.Controls.Add(MainControl);
		MainControl.BringToFront();
		TopPanel.BringToFront();
		MainControl.Dock = DockStyle.Fill;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIRunForm.RunForm));
		this.TopPanel = new System.Windows.Forms.Panel();
		this.menuStrip = new System.Windows.Forms.MenuStrip();
		this.数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.数据库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.全屏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.标准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.屏幕键盘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.计算其ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.保存到文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.menuStrip.SuspendLayout();
		base.SuspendLayout();
		this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
		this.TopPanel.Location = new System.Drawing.Point(0, 0);
		this.TopPanel.Name = "TopPanel";
		this.TopPanel.Size = new System.Drawing.Size(792, 1);
		this.TopPanel.TabIndex = 1;
		this.TopPanel.Visible = false;
		this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(TopPanel_Paint);
		this.TopPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseClick);
		this.TopPanel.MouseEnter += new System.EventHandler(TopPanel_MouseEnter);
		this.TopPanel.MouseLeave += new System.EventHandler(TopPanel_MouseLeave);
		this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.数据库ToolStripMenuItem, this.视图ToolStripMenuItem, this.工具ToolStripMenuItem, this.截图ToolStripMenuItem });
		this.menuStrip.Location = new System.Drawing.Point(0, 0);
		this.menuStrip.Name = "menuStrip";
		this.menuStrip.Size = new System.Drawing.Size(792, 25);
		this.menuStrip.TabIndex = 2;
		this.menuStrip.Text = "menuStrip";
		this.menuStrip.Visible = false;
		this.数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.数据库管理ToolStripMenuItem });
		this.数据库ToolStripMenuItem.Name = "数据库ToolStripMenuItem";
		this.数据库ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
		this.数据库ToolStripMenuItem.Text = "数据库";
		this.数据库管理ToolStripMenuItem.Name = "数据库管理ToolStripMenuItem";
		this.数据库管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.数据库管理ToolStripMenuItem.Text = "数据库管理";
		this.数据库管理ToolStripMenuItem.Click += new System.EventHandler(数据库管理ToolStripMenuItem_Click);
		this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.全屏ToolStripMenuItem, this.标准ToolStripMenuItem });
		this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
		this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
		this.视图ToolStripMenuItem.Text = "视图";
		this.全屏ToolStripMenuItem.Name = "全屏ToolStripMenuItem";
		this.全屏ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.全屏ToolStripMenuItem.Text = "全屏";
		this.全屏ToolStripMenuItem.Click += new System.EventHandler(全屏ToolStripMenuItem_Click);
		this.标准ToolStripMenuItem.Name = "标准ToolStripMenuItem";
		this.标准ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.标准ToolStripMenuItem.Text = "标准";
		this.标准ToolStripMenuItem.Click += new System.EventHandler(标准ToolStripMenuItem_Click);
		this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.屏幕键盘ToolStripMenuItem, this.计算其ToolStripMenuItem });
		this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
		this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
		this.工具ToolStripMenuItem.Text = "工具";
		this.屏幕键盘ToolStripMenuItem.Name = "屏幕键盘ToolStripMenuItem";
		this.屏幕键盘ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.屏幕键盘ToolStripMenuItem.Text = "屏幕键盘";
		this.屏幕键盘ToolStripMenuItem.Click += new System.EventHandler(屏幕键盘ToolStripMenuItem_Click);
		this.计算其ToolStripMenuItem.Name = "计算其ToolStripMenuItem";
		this.计算其ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.计算其ToolStripMenuItem.Text = "计算器";
		this.计算其ToolStripMenuItem.Click += new System.EventHandler(计算其ToolStripMenuItem_Click);
		this.截图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.保存到文件ToolStripMenuItem, this.打印ToolStripMenuItem });
		this.截图ToolStripMenuItem.Name = "截图ToolStripMenuItem";
		this.截图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
		this.截图ToolStripMenuItem.Text = "截图";
		this.截图ToolStripMenuItem.Visible = false;
		this.保存到文件ToolStripMenuItem.Name = "保存到文件ToolStripMenuItem";
		this.保存到文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.保存到文件ToolStripMenuItem.Text = "保存文件";
		this.保存到文件ToolStripMenuItem.Click += new System.EventHandler(保存到文件ToolStripMenuItem_Click);
		this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
		this.打印ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.打印ToolStripMenuItem.Text = "打印";
		this.打印ToolStripMenuItem.Click += new System.EventHandler(打印ToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.TopPanel);
		base.Controls.Add(this.menuStrip);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.MainMenuStrip = this.menuStrip;
		base.Name = "RunForm";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
		base.Load += new System.EventHandler(RunForm_Load);
		this.menuStrip.ResumeLayout(false);
		this.menuStrip.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
