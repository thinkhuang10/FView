using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ShapeRuntime;

public class ClassForUsers
{
	public static void MakeUserTypeTree(TreeView tv, ComboBox cb, List<UserType> userTypes, List<HMIUser> HMIusers)
	{
		tv.Nodes.Clear();
		cb.Items.Clear();
		List<HMIUser> list = new List<HMIUser>();
		foreach (HMIUser HMIuser in HMIusers)
		{
			list.Add(HMIuser);
		}
		foreach (UserType userType in userTypes)
		{
			TreeNode treeNode = tv.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			cb.Items.Add(userType);
			HMIUser[] array = list.ToArray();
			foreach (HMIUser current in array)
			{
				if (current.type == userType.Id)
				{
					treeNode.Nodes.Add(current.name, current.name, "Snap1.bmp", "Snap1.bmp");
					list.Remove(current);
				}
			}
			if (userType.SubUserTypes != null)
			{
				MakeUserTypeTree(treeNode, cb, userType.SubUserTypes, list);
			}
		}
	}

	public static List<UserType> GetUserTypeList(List<UserType> Begin)
	{
		List<UserType> list = new List<UserType>();
		makeUserTypeList(list, Begin);
		return list;
	}

	private static void makeUserTypeList(List<UserType> lut, List<UserType> Begin)
	{
		foreach (UserType item in Begin)
		{
			lut.Add(item);
			if (item.SubUserTypes != null)
			{
				makeUserTypeList(lut, item.SubUserTypes);
			}
		}
	}

	public static UserType FindUserType(string name, List<UserType> Begin)
	{
		foreach (UserType item in Begin)
		{
			if (item.UserTypeName == name)
			{
				return item;
			}
			if (item.SubUserTypes != null)
			{
				UserType userType = FindUserType(name, item.SubUserTypes);
				if (userType != null)
				{
					return userType;
				}
			}
		}
		return null;
	}

	public static bool DelUserType(string name, List<UserType> Begin)
	{
		UserType[] array = Begin.ToArray();
		foreach (UserType userType in array)
		{
			if (userType.UserTypeName == name)
			{
				Begin.Remove(userType);
				return true;
			}
			if (userType.SubUserTypes != null && DelUserType(name, userType.SubUserTypes))
			{
				return true;
			}
		}
		return false;
	}

	public static UserType FindUserTypeByID(int ID, List<UserType> Begin)
	{
		foreach (UserType item in Begin)
		{
			if (item.Id == ID)
			{
				return item;
			}
			if (item.SubUserTypes != null)
			{
				UserType userType = FindUserTypeByID(ID, item.SubUserTypes);
				if (userType != null)
				{
					return userType;
				}
			}
		}
		return null;
	}

	public static bool UserTypeInUseByID(int ID, List<UserType> Begin)
	{
		foreach (UserType item in Begin)
		{
			if (item.Id == ID)
			{
				return true;
			}
			if (item.SubUserTypes != null)
			{
				UserType userType = FindUserTypeByID(ID, item.SubUserTypes);
				if (userType != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	private static void MakeUserTypeTree(TreeNode tv, ComboBox cb, List<UserType> userTypes, List<HMIUser> HMIusers)
	{
		foreach (UserType userType in userTypes)
		{
			TreeNode treeNode = tv.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			cb.Items.Add(userType);
			HMIUser[] array = HMIusers.ToArray();
			foreach (HMIUser hMIUser in array)
			{
				if (hMIUser.type == userType.Id)
				{
					treeNode.Nodes.Add(hMIUser.name, hMIUser.name, "Snap1.bmp", "Snap1.bmp");
					HMIusers.Remove(hMIUser);
				}
			}
			if (userType.SubUserTypes != null)
			{
				MakeUserTypeTree(treeNode, cb, userType.SubUserTypes, HMIusers);
			}
		}
	}

	public static void MakeUserTypeTree(TreeView tv, List<UserType> userTypes, List<HMIUser> HMIusers)
	{
		tv.Nodes.Clear();
		List<HMIUser> list = new List<HMIUser>();
		foreach (HMIUser HMIuser in HMIusers)
		{
			list.Add(HMIuser);
		}
		foreach (UserType userType in userTypes)
		{
			TreeNode tv2 = tv.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			if (userType.SubUserTypes != null)
			{
				MakeUserTypeTree(tv2, userType.SubUserTypes, HMIusers);
			}
		}
	}

	private static void MakeUserTypeTree(TreeNode tv, List<UserType> userTypes, List<HMIUser> HMIusers)
	{
		foreach (UserType userType in userTypes)
		{
			TreeNode tv2 = tv.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			HMIUser[] array = HMIusers.ToArray();
			foreach (HMIUser hMIUser in array)
			{
			}
			if (userType.SubUserTypes != null)
			{
				MakeUserTypeTree(tv2, userType.SubUserTypes, HMIusers);
			}
		}
	}

	public static string getMd5Hash(string input)
	{
		MD5 mD = MD5.Create();
		byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input));
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	public static HMIUser MakeNewHMIUserByUserType(UserType ut)
	{
		HMIUser hMIUser = new HMIUser();
		foreach (int region in ut.Regions)
		{
			hMIUser.Regions.Add(region);
		}
		return hMIUser;
	}

	public static void BeforeDelUserType(UserType ut, List<HMIUser> lus)
	{
		HMIUser[] array = lus.ToArray();
		foreach (HMIUser hMIUser in array)
		{
			if (hMIUser.type == ut.Id)
			{
				lus.Remove(hMIUser);
			}
		}
	}
}
