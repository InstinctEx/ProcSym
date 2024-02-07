using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ProcSym
{
    public partial class Main : Form
    {
        private bool _originalFiles = true;
        private bool _isChecked;
        private string[] _updatedFileNames;
        private readonly Dictionary<string, string> _fileNamesMap = new Dictionary<string, string>();
        private static readonly Random Random = new Random();

        public Main()
        {
            InitializeComponent();
            CheckForAdminPrivileges();
        }

        private static void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void CheckForAdminPrivileges()
        {
            if (!IsAdministrator())
            {
                DisplayMessage("Please run this program as Administrator!");
                Close();
            }
        }

        private static bool IsAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            var itemsToRemove = listBox1.SelectedItems.Cast<string>().ToList();
            foreach (var item in itemsToRemove)
            {
                listBox1.Items.Remove(item);
            }
        }

        private void OpenFolderBtn_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog
            {
                Filter = "Executable Files (*.exe)|*.exe",
                Multiselect = true
            })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                //listBox1.Items.Clear();
                listBox1.Items.AddRange(openFileDialog.FileNames);
            }
        }

        private static Process StartProcess(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show($"File does not exist: {path}");
                return null;
            }
            var process = Process.Start(path);
            process.EnableRaisingEvents = true;
            return process;
        }

        private static void KillProcesses(IEnumerable<string> processNames)
        {
            var processesToKill = Process.GetProcesses()
                .Where(x => processNames.Any(p => x.ProcessName.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            foreach (var process in processesToKill)
            {
                process.Kill();
            }
        }

        private void CreateSymlinks(string path, IEnumerable<string> upfileNames, IEnumerable<string> fileNames)
        {
            foreach (var fileName in upfileNames)
            {
                try
                {
                    foreach (var file in fileNames)
                    {
                        var symlinkName = file;
                        File.CreateSymbolicLink(Path.Combine(path, symlinkName), Path.Combine(path, fileName));
                    }

                    _originalFiles = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private static void DeleteFiles(string path, IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                try
                {
                    File.Delete(Path.Combine(path, fileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at Deleting Files: " + ex.Message);
                }
            }
        }

        public void Wait(int milliseconds)
        {
            if (milliseconds <= 0) return;
            var timer1 = new System.Windows.Forms.Timer { Interval = milliseconds };
            timer1.Start();
            timer1.Tick += (sender, args) =>
            {
                timer1.Stop();
                timer1.Dispose();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private string[] RenameFiles(string path, string[] fileNames)
        {
            var newFileNames = new List<string>();

            foreach (var fileName in fileNames)
            {
                try
                {
                    var newFileName = "rre_" + RandomString(15) + ".exe";
                    File.Move(Path.Combine(path, fileName), Path.Combine(path, newFileName));
                    newFileNames.Add(newFileName);

                    // Add the new name and original name to the dictionary
                    _fileNamesMap[newFileName] = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return newFileNames.ToArray();
        }

        private void Runrbtn_Click(object sender, EventArgs e)
        {
            var paths = listBox1.Items.Cast<string>().ToArray();
            var fileNames = listBox1.Items.Cast<string>().Select(Path.GetFileName).ToArray();

            foreach (var path in paths)
            {
                var directoryName = Path.GetDirectoryName(path);

                if (!_originalFiles)
                {
                    MessageBox.Show("SymProc is already running, please restart the application");
                    return;
                }
                _updatedFileNames = RenameFiles(directoryName, fileNames);
                CreateSymlinks(directoryName, _updatedFileNames, fileNames);
                StartProcess(Path.Combine(directoryName, _updatedFileNames[0])); // Pass the full path including the renamed file name
                Wait(3000);
                DeleteFiles(directoryName, fileNames);
                return;
            }
        }

        private void Main_Close(object sender, FormClosingEventArgs e)
        {
            var directories = listBox1.Items.Cast<string>().Select(Path.GetDirectoryName).ToArray();
            var fileNames = listBox1.Items.Cast<string>().Select(Path.GetFileName).ToArray();
            var KillablefileNames = listBox1.Items.Cast<string>().Select(Path.GetFileNameWithoutExtension).ToArray();

            if (!_originalFiles)
            {
                var renamedFileNames = _updatedFileNames.Select(Path.GetFileNameWithoutExtension).ToArray();
                var processNamesToKill = KillablefileNames.Concat(renamedFileNames);

                foreach (var directory in directories)
                {

                    KillProcesses(processNamesToKill);
                    RenameToOriginalNames(directory);
                    MessageBox.Show("Cleaning Temp Data.");
                }
            }
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private void RenameToOriginalNames(string path)
        {
            foreach (var entry in _fileNamesMap)
            {
                try
                {
                    File.Move(Path.Combine(path, entry.Key), Path.Combine(path, entry.Value));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
