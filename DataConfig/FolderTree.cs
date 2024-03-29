using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformHumanAnFaceDetection.DataConfig
{
	public class FolderTree : IDisposable
	{
		public List<FolderTree> SubFolders { get; set; } = null!;
		public int ImageFileCount { get; set; }
		public List<string> ImageFileNames { get; set; } = null!;
		public string FolderName { get; set; } = null!; // New property to store the folder name


		public FolderTree()
		{
			SubFolders = new List<FolderTree>();
			ImageFileNames = new List<string>();
		}

		public void Populate(string folderPath, string rootPath)
		{
			FolderName = Path.GetFileName(folderPath); // Set the folder name
			string[] subdirectories = Directory.GetDirectories(folderPath);
			foreach (string subdirectory in subdirectories)
			{
				FolderTree subFolder = new FolderTree();
				subFolder.Populate(subdirectory, rootPath);
				SubFolders.Add(subFolder);
			}

			string[] imageFiles = Directory.GetFiles(folderPath).Where(file => IsImage(file)).ToArray();
			ImageFileCount = imageFiles.Length;
			ImageFileNames = imageFiles.Select(file => file.Replace(rootPath + Path.DirectorySeparatorChar, "")).ToList();
		}

		public void PopulateTreeView(TreeView treeView, FolderTree folderTree, TreeNode? parentNode = null)
		{
			string newNodeText = folderTree.FolderName + ": " + folderTree.ImageFileCount + " images";
			TreeNode newNode = new TreeNode(newNodeText);
			if (treeView.InvokeRequired)
			{
				treeView.Invoke((MethodInvoker)delegate
				{
					if (parentNode == null)
						treeView.Nodes.Add(newNode);
					else
						parentNode.Nodes.Add(newNode);
				});
			}
			else
			{
				if (parentNode == null)
					treeView.Nodes.Add(newNode);
				else
					parentNode.Nodes.Add(newNode);
			}
			foreach (FolderTree subFolder in folderTree.SubFolders)
				PopulateTreeView(treeView, subFolder, newNode);
		}

		/// <summary>
		/// Trả về từ từ trong tất cả các tệp hình ảnh trong thư mục và tất cả các thư mục con
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetImageFiles()
		{
			foreach (string imageFile in GetImageFiles(this))
				yield return imageFile;
		}
		public IEnumerable<string> GetImageFiles(string newStringParentPath)
		{
			foreach (string imageFile in GetImageFiles(this, true, newStringParentPath))
				yield return imageFile;
		}


		private IEnumerable<string> GetImageFiles(FolderTree parent, bool isCreateFolderPathNotExist = false, string newStringParentPath = null!)
		{
			foreach (string imageFile in parent.ImageFileNames)
			{
				string fullPath = Path.Combine(imageFile);
				if (isCreateFolderPathNotExist)
				{
					string directoryPath = Path.GetDirectoryName(Path.Combine(newStringParentPath, fullPath))!;
					if (directoryPath != null && !Directory.Exists(directoryPath))
						Directory.CreateDirectory(directoryPath);
				}
				yield return fullPath;
			}
			foreach (FolderTree subFolder in parent.SubFolders)
			{
				foreach (string subFolderImageFile in GetImageFiles(subFolder, isCreateFolderPathNotExist, newStringParentPath))
				{
					string fullPath = Path.Combine(subFolderImageFile);
					if (isCreateFolderPathNotExist)
					{
						string directoryPath = Path.GetDirectoryName(Path.Combine(newStringParentPath, fullPath))!;
						if (directoryPath != null && !Directory.Exists(directoryPath))
							Directory.CreateDirectory(directoryPath);
					}
					yield return fullPath;
				}
			}
		}

		public int TotalImageFiles()
		{
			int total = ImageFileCount;
			foreach (var subFolder in SubFolders)
				total += subFolder.TotalImageFiles();
			return total;
		}

		private bool IsImage(string file)
		{
			string[] extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp", ".svg" };
			return extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}

		public void Dispose()
		{
			SubFolders = new List<FolderTree>();
			ImageFileCount = 0;
			List<string> ImageFileNames = new List<string>();
		}
	}

}
