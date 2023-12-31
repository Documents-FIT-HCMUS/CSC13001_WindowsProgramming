using RuleHandler;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Text.Json;
using System.Windows.Threading;
using System.Reflection;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private BindingList<IRuleHandler> rules;
        private BindingList<IRuleHandler> chosenRules;
        private BindingList<string> itemTypes;
        private BindingList<IRuleHandler> conflictActions;
        private BindingList<Filename> filenames;
        private BindingList<Foldername> foldernames;

        private string currentProjectName = "Unsaved Project";
        private string currentProjectPath = "autosave.proj";
        private string currentPresetPath = "";

        private Dictionary<string, List<Filename>> conflictFiles = new Dictionary<string, List<Filename>>();
        private Dictionary<string, List<Foldername>> conflictFolders = new Dictionary<string, List<Foldername>>();

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            Title = "Unsaved Project";
        }

        private bool loadProject(string location, string name)
        {
            string path = location;
            string content = File.ReadAllText(path);

            ProjectFormat projectData = new ProjectFormat();
            try
            {
                projectData = JsonSerializer.Deserialize<ProjectFormat>(content);
            }
            catch (JsonException exception)
            {
                MessageBox.Show("Cannot parse data from chosen file due to incorrect format! Check the file again!", "Error");
                return false;
            }

            this.chosenRules.Clear();
            foreach (RuleJsonFormat ruleJson in projectData.rules)
            {
                IRuleHandler item = rules.SingleOrDefault(Item => Item.GetRuleType().Equals(ruleJson.RuleType));
                if (item != null)
                {
                    IRuleHandler target = item.Clone();
                    target.SetParameter(new RuleParameter
                    {
                        InputStrings = ruleJson.InputStrings,
                        OutputStrings = ruleJson.OutputStrings,
                        Counter = ruleJson.Counter,
                    });
                    this.chosenRules.Add(target);
                }
            }

            filenames.Clear();
            foreach (FileFormat fileFormat in projectData.files)
            {
                filenames.Add(new Filename
                {
                    CurrentName = fileFormat.CurrentName,
                    NewName = fileFormat.NewName,
                    Path = fileFormat.Path,
                    Result = fileFormat.result
                });
            }

            foldernames.Clear();
            foreach (FolderFormat folderFormat in projectData.folders)
            {
                foldernames.Add(new Foldername
                {
                    CurrentName = folderFormat.CurrentName,
                    NewName = folderFormat.NewName,
                    Path = folderFormat.Path,
                    Result = folderFormat.result
                });
            }

            ItemListView.ItemsSource = projectData.files.Count > 0 ? filenames : foldernames;
            typeComboBox.SelectedItem = projectData.files.Count > 0 ? "File" : "Folder";

            this.currentProjectPath = location;
            this.currentProjectName = name;
            Title = currentProjectName;

            return true;
        }

        private bool saveProject(string location, string name)
        {
            string path = location;
            StreamWriter output;
            try
            {
                output = new StreamWriter(path);

                List<RuleJsonFormat> rules = new List<RuleJsonFormat>();
                List<FileFormat> files = new List<FileFormat>();
                List<FolderFormat> folders = new List<FolderFormat>();

                foreach (IRuleHandler item in this.chosenRules)
                {
                    rules.Add(new RuleJsonFormat
                    {
                        RuleType = item.GetRuleType(),
                        InputStrings = item.GetParameter().InputStrings,
                        OutputStrings = item.GetParameter().OutputStrings,
                        Counter = item.GetParameter().Counter,
                    });
                }

                foreach (Filename filename in filenames)
                {
                    files.Add(new FileFormat
                    {
                        CurrentName = filename.CurrentName,
                        NewName = filename.NewName,
                        Path = filename.Path,
                        result = filename.Result
                    });
                }

                foreach (Foldername foldername in foldernames)
                {
                    folders.Add(new FolderFormat
                    {
                        CurrentName = foldername.CurrentName,
                        NewName = foldername.NewName,
                        Path = foldername.Path,
                        result = foldername.Result
                    });
                }

                ProjectFormat projectFormat = new ProjectFormat
                {
                    rules = rules,
                    files = files,
                    folders = folders
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                string data = JsonSerializer.Serialize(projectFormat, options);
                output.Write(data);
                output.Close();

                this.currentProjectPath = path;
                this.currentProjectName = name;

                return true;
            }
            catch (IOException ioe)
            {
                return false;
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            string message = "Do you want to close the application?";
            if (File.Exists("autosave.proj"))
            {
                message = "You have unsaved work/project. Do you want to save it before closing?";
                var result = MessageBox.Show(message, "Batch Rename", MessageBoxButton.YesNoCancel,
                     MessageBoxImage.Question, MessageBoxResult.Yes);

                if (MessageBoxResult.Yes == result)
                {
                    // TODO: save project here
                    string path;
                    if (this.currentProjectPath == "")
                    {
                        // Handle save dialog here
                        var dialog = new SaveFileDialog();
                        dialog.Filter = "PROJ (*.proj)|*.proj";
                        if (dialog.ShowDialog() == false)
                            return;
                        path = dialog.FileName;
                        // Get the projectName from path
                    }
                    else
                    {
                        path = this.currentProjectPath;
                    }

                    if (saveProject(path, ""))
                    {
                        if (File.Exists("autosave.proj"))
                            File.Delete("autosave.proj");
                        Environment.Exit(0);
                    }
                }
                else if (MessageBoxResult.No == result)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                var result = MessageBox.Show(message, "Batch Rename", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.No);

                if (MessageBoxResult.Yes == result)
                {
                    saveProject(this.currentProjectPath, this.currentProjectName);
                    Environment.Exit(0);
                }
            }

        }

        private void PerformAutosave()
        {
            saveProject(this.currentProjectPath, this.currentProjectName);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Perform Autosave every 15 seconds
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (s, ev) => PerformAutosave();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();

            // Initiate Data Lists/Collections
            List<IRuleHandler> ruleHandlers = new List<IRuleHandler>();

            var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            var dlls = new DirectoryInfo(exeFolder).GetFiles("rules/*.dll");

            foreach (var dll in dlls)
            {
                var assembly = Assembly.LoadFile(dll.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass)
                    {
                        if (typeof(IRuleHandler).IsAssignableFrom(type))
                        {
                            var temp_rule = Activator.CreateInstance(type) as IRuleHandler;
                            ruleHandlers.Add(temp_rule);
                        }
                    }
                }
            }

            rules = new BindingList<IRuleHandler>();

            ruleHandlers.ForEach(E =>
            {
                rules.Add(E);
            });

            //itemTypes
            itemTypes = new BindingList<string>()
            {
                "File", "Folder"
            };
            chosenRules = new BindingList<IRuleHandler>();
            conflictActions = new BindingList<IRuleHandler>();

            int index = ruleHandlers.FindIndex(e => e.GetRuleType() == "AddSuffixCounterRule");
            if (index != -1)
                conflictActions.Add(ruleHandlers[index]);

            index = ruleHandlers.FindIndex(e => e.GetRuleType() == "AddPrefixCounterRule");
            if (index != -1)
                conflictActions.Add(ruleHandlers[index]);


            filenames = new BindingList<Filename>();
            foldernames = new BindingList<Foldername>();

            // Bind UI with lists/collections
            rulesComboxBox.ItemsSource = rules;
            typeComboBox.ItemsSource = itemTypes;
            conflictComboBox.ItemsSource = conflictActions;

            chosenRulesListView.ItemsSource = chosenRules;
        }

        private void AddRules(object sender, RoutedEventArgs e)
        {
            int index = rulesComboxBox.SelectedIndex;
            if (index != -1)
            {
                chosenRules.Add(rules[index].Clone());
            }
        }
        private void EditChosenRule(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Invalid rule.");
                return;
            }

            IRuleHandler rule = chosenRules[index];
            if (rule.IsEditable())
            {
                IRuleEditor editWindow = rule.ParamsEditorWindow();
                if (editWindow.ShowDialog() == true)
                    chosenRules[index].SetParameter(editWindow.GetParameter());
            }
            else
            {
                MessageBox.Show("This rule does not have any parameter to edit", "Error");
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(chosenRules);
            view.Refresh();

        }
        private void ChosenRule_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Invalid rule.");
                return;
            }

            IRuleHandler rule = chosenRules[index];
            if (rule.IsEditable())
            {
                IRuleEditor editWindow = rule.ParamsEditorWindow();
                if (editWindow.ShowDialog() == true)
                    chosenRules[index].SetParameter(editWindow.GetParameter());
            }
            else
            {
                MessageBox.Show("This rule does not have any parameter to edit", "Error");
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(chosenRules);
            view.Refresh();
        }
        private void RemoveChosenRule(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index != -1)
                chosenRules.RemoveAt(index);
        }
        private void RemoveChosenItem(object sender, RoutedEventArgs e)
        {
            string chosenType = itemTypes[typeComboBox.SelectedIndex];
            switch (chosenType)
            {
                case "File":
                    filenames.RemoveAt(ItemListView.SelectedIndex);
                    break;
                case "Folder":
                    foldernames.RemoveAt(ItemListView.SelectedIndex);
                    break;
                default:
                    break;
            }
        }
        private void ResetChosenRules(object sender, RoutedEventArgs e)
        {
            chosenRules.Clear();
            conflictFiles.Clear();
            conflictFolders.Clear();
        }

        #region rule ListView position buttons event handler
        private void MoveRuleToTop(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index != -1)
            {
                IRuleHandler temp = chosenRules[index];
                for (int i = index; i > 0; --i)
                    chosenRules[i] = chosenRules[i - 1];
                chosenRules[0] = temp;
            }
        }

        private void MoveRuleToPrev(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index != -1 && index != 0)
            {
                IRuleHandler temp = chosenRules[index - 1];
                chosenRules[index - 1] = chosenRules[index];
                chosenRules[index] = temp;
            }
        }

        private void MoveRuleToNext(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index != -1 && index != chosenRules.Count - 1)
            {
                IRuleHandler temp = chosenRules[index + 1];
                chosenRules[index + 1] = chosenRules[index];
                chosenRules[index] = temp;
            }
        }

        private void MoveRuleToBottom(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index != -1)
            {
                IRuleHandler temp = chosenRules[index];
                for (int i = index; i < chosenRules.Count - 1; ++i)
                    chosenRules[i] = chosenRules[i + 1];
                chosenRules[chosenRules.Count - 1] = temp;
            }
        }
        #endregion

        private void AddItems(object sender, RoutedEventArgs e)
        {
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose item type (files or folders)", "Error");
                return;
            }
            if (typeComboBox.SelectedItem.ToString() == "File")
            {
                ItemListView.ItemsSource = filenames;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "All files (*.*)|*.*";
                int counter = 0;
                if (openFileDialog.ShowDialog() == true)
                {
                    string[] files = openFileDialog.FileNames;
                    List<Filename> newFilenames = new List<Filename>();

                    foreach (string file in files)
                    {
                        bool isExisted = false;
                        string currentName = Path.GetFileName(file);
                        string directoryPath = Path.GetDirectoryName(file);

                        foreach (var filename in filenames)
                        {
                            if (filename.CurrentName == currentName && filename.Path == directoryPath)
                            {
                                isExisted = true;
                                break;
                            }
                        }

                        if (!isExisted)
                        {
                            newFilenames.Add(new Filename() { CurrentName = currentName, Path = directoryPath });
                            counter++;
                        }
                    }

                    foreach (var newFilename in newFilenames)
                        filenames.Add(newFilename);
                }

                MessageBox.Show(counter + " file(s) Added Successfully", "Success");
            }
            else if (typeComboBox.SelectedItem.ToString() == "Folder")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {
                    ItemListView.ItemsSource = foldernames;

                    string path = dialog.SelectedPath + "\\";
                    string[] folders = Directory.GetDirectories(path);
                    List<Foldername> newFoldernames = new List<Foldername>();

                    foreach (var folder in folders)
                    {
                        bool isExisted = false;
                        string currentName = folder.Remove(0, path.Length);

                        foreach (var foldername in foldernames)
                        {
                            if (foldername.CurrentName == currentName && foldername.Path == path)
                            {
                                isExisted = true;
                                break;
                            }
                        }

                        if (!isExisted)
                        {
                            newFoldernames.Add(new Foldername() { CurrentName = currentName, Path = path });
                            counter++;
                        }
                    }
                    foreach (var newFoldername in newFoldernames)
                        foldernames.Add(newFoldername);

                    MessageBox.Show(counter + " folder(s) Added Successfully", "Success");
                }

            }
        }

        private void ResetAddedItems(object sender, RoutedEventArgs e)
        {
            filenames.Clear();
            foldernames.Clear();
            conflictFiles.Clear();
            conflictFolders.Clear();
        }

        #region process preview and start batch
        private void StartProcess(object sender, RoutedEventArgs e)
        {
            if (chosenRules.Count == 0)
            {
                MessageBox.Show("Process skipped because there is no rule set", "Process aborted");
                return;
            }

            if (filenames.Count == 0 && foldernames.Count == 0)
            {
                MessageBox.Show("Process skipped because there is chosed file(s)/folder(s)", "Process aborted");
                return;
            }

            string targetPath = "";
            bool moveMode = false;

            if (moveToNew.IsChecked == true)
            {
                // Probably show a a dialog, let user choose where to move new files to
                // What will happen if we rename folders, do we copy all files and subfolders inside?
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                string path = "";
                if (System.Windows.Forms.DialogResult.OK == result)
                {
                    path = dialog.SelectedPath;
                    moveMode = true;
                }

                targetPath = path;
            }

            this.conflictFiles.Clear();
            this.conflictFolders.Clear();

            List<IRuleHandler> ruleSetForFiles = new List<IRuleHandler>();
            List<IRuleHandler> ruleSetForFolders = new List<IRuleHandler>();

            foreach (IRuleHandler rule in this.chosenRules)
            {
                ruleSetForFiles.Add(rule.Clone());
                ruleSetForFolders.Add(rule.Clone());
            }

            foreach (Filename file in filenames)
            {
                file.NewName = file.CurrentName;
                file.Result = "";

                string tempPath = file.Path;
                if (moveMode)
                    tempPath = targetPath;

                foreach (IRuleHandler handler in ruleSetForFiles)
                {
                    file.NewName = handler.Process(file.NewName);
                }

                if (!this.conflictFiles.ContainsKey(tempPath + "/" + file.NewName))
                    this.conflictFiles.Add(tempPath + "/" + file.NewName, new List<Filename> { file });
                else
                    this.conflictFiles[tempPath + "/" + file.NewName].Add(file);

                string validCheck = Util.CheckValid(file.NewName);
                if (validCheck != "")
                {
                    file.Result = validCheck;
                }
            }

            foreach (Foldername folder in foldernames)
            {
                folder.NewName = folder.CurrentName;
                folder.Result = "";
                foreach (IRuleHandler handler in ruleSetForFolders)
                {
                    folder.NewName = handler.Process(folder.NewName, false);
                }

                string tempPath = folder.Path;
                if (moveMode)
                    tempPath = targetPath;

                if (!this.conflictFolders.ContainsKey(targetPath + "/" + folder.NewName))
                    this.conflictFolders.Add(targetPath + "/" + folder.NewName, new List<Foldername> { folder });
                else
                    this.conflictFolders[targetPath + "/" + folder.NewName].Add(folder);

                string validCheck = Util.CheckValid(folder.NewName);
                if (validCheck != "")
                {
                    folder.Result = validCheck;
                }
            }

            bool isOccurConflict = false;

            foreach (var (key, value) in this.conflictFiles)
            {
                if (value.Count > 1)
                {
                    isOccurConflict = true;
                    value.ForEach(e =>
                    {
                        e.Result = "Conflicted";
                    });
                }
            }

            foreach (var (key, value) in this.conflictFolders)
            {
                if (value.Count > 1)
                {
                    isOccurConflict = true;
                    value.ForEach(e =>
                    {
                        e.Result = "Conflicted";
                    });
                }
            }

            int index = this.conflictComboBox.SelectedIndex;

            if (isOccurConflict && index == -1)
            {
                MessageBox.Show("There will be some files/folders have the same name at the end of the process, consider to add conflict resolver or change rule set and try again", "Process aborted");
                return;
            }

            //resolve conflict if selected a conflict resolver
            foreach (var (key, value) in this.conflictFiles)
            {
                if (value.Count > 1)
                {
                    IRuleHandler resolver = conflictActions[index].Clone();
                    value.ForEach(e =>
                    {
                        e.NewName = resolver.Process(e.NewName);
                    });
                }
            }

            foreach (var (key, value) in this.conflictFolders)
            {
                if (value.Count > 1)
                {
                    IRuleHandler resolver = conflictActions[index].Clone();
                    value.ForEach(e =>
                    {
                        e.NewName = resolver.Process(e.NewName);
                    });
                }
            }

            //process

            int folderCounter = 0;
            int fileCounter = 0;
            bool haveInvalidName = false;

            foreach (Filename file in filenames)
            {
                string validCheck = Util.CheckValid(file.NewName);
                if (validCheck != "")
                {
                    file.Result = "Aborted " + validCheck;
                    haveInvalidName = true;
                    continue;
                }
                try
                {
                    if (!moveMode)
                        File.Move(file.Path + "/" + file.CurrentName, file.Path + "/" + file.NewName);
                    else
                        File.Copy(file.Path + "/" + file.CurrentName, targetPath + "/" + file.NewName);
                    file.CurrentName = file.NewName;
                }
                catch (FileNotFoundException exception)
                {
                    file.Result = "Source file not exist";
                    continue;
                }
                fileCounter++;
                file.Result = "Success";
            }

            foreach (Foldername folder in foldernames)
            {
                string validCheck = Util.CheckValid(folder.NewName);
                if (validCheck != "")
                {
                    folder.Result = "Aborted " + validCheck;
                    haveInvalidName = true;
                    continue;
                }

                try
                {
                    Directory.Move(folder.Path + "/" + folder.CurrentName, folder.Path + "/" + folder.NewName);
                    folder.CurrentName = folder.NewName;
                }
                catch (DirectoryNotFoundException exception)
                {
                    folder.Result = "Source directory not found";
                    continue;
                }
                folderCounter++;
                folder.Result = "Success";
            }

            MessageBox.Show($"Result\n   Type file: {fileCounter}/{filenames.Count} success\n   Type folder: {folderCounter}/{foldernames.Count} success", "Process done");
        }
        private void PreviewProcess(object sender, RoutedEventArgs e)
        {
            if (chosenRules.Count == 0)
            {
                MessageBox.Show("Process skipped because there is no rule set", "Process aborted");
                return;
            }

            if (filenames.Count == 0 && foldernames.Count == 0)
            {
                MessageBox.Show("Process skipped because there is no selected file(s)/folder(s)", "Process aborted");
                return;
            }

            this.conflictFiles.Clear();
            this.conflictFolders.Clear();
            bool haveInvalidName = false;

            List<IRuleHandler> ruleSetForFiles = new List<IRuleHandler>();
            List<IRuleHandler> ruleSetForFolders = new List<IRuleHandler>();

            foreach (IRuleHandler rule in this.chosenRules)
            {
                ruleSetForFiles.Add(rule.Clone());
                ruleSetForFolders.Add(rule.Clone());
            }

            foreach (Filename file in filenames)
            {
                file.NewName = file.CurrentName;
                file.Result = "";

                if (!File.Exists(file.Path + "/" + file.CurrentName))
                {
                    file.Result = "Source file not exist";
                    continue;
                }


                foreach (IRuleHandler handler in ruleSetForFiles)
                {
                    file.NewName = handler.Process(file.NewName);
                }

                if (!this.conflictFiles.ContainsKey(file.Path + "/" + file.NewName))
                    this.conflictFiles.Add(file.Path + "/" + file.NewName, new List<Filename> { file });
                else
                    this.conflictFiles[file.Path + "/" + file.NewName].Add(file);

                string validCheck = Util.CheckValid(file.NewName);
                if (validCheck != "")
                {
                    file.Result = validCheck;
                    haveInvalidName = true;
                }
            }

            foreach (Foldername folder in foldernames)
            {
                folder.NewName = folder.CurrentName;
                folder.Result = "";

                if (!Directory.Exists(folder.Path + "/" + folder.CurrentName))
                {
                    folder.Result = "Directory not exist";
                    continue;
                }

                foreach (IRuleHandler handler in ruleSetForFolders)
                {
                    folder.NewName = handler.Process(folder.NewName, false);
                }

                if (!this.conflictFolders.ContainsKey(folder.Path + "/" + folder.NewName))
                    this.conflictFolders.Add(folder.Path + "/" + folder.NewName, new List<Foldername> { folder });
                else
                    this.conflictFolders[folder.Path + "/" + folder.NewName].Add(folder);

                string validCheck = Util.CheckValid(folder.NewName);
                if (validCheck != "")
                {
                    folder.Result = validCheck;
                    haveInvalidName = true;
                }
            }

            bool isOccurConflict = false;

            foreach (var (key, value) in this.conflictFiles)
            {
                if (value.Count > 1)
                {
                    isOccurConflict = true;
                    value.ForEach(e =>
                    {
                        e.Result += " | Duplicate name";
                    });
                }
            }

            foreach (var (key, value) in this.conflictFolders)
            {
                if (value.Count > 1)
                {
                    isOccurConflict = true;
                    value.ForEach(e =>
                    {
                        e.Result += " | Duplicate name";
                    });
                }
            }

            if (isOccurConflict || haveInvalidName)
            {
                MessageBox.Show("There are some files/folders have name conflict, consider to add conflict resolver or change rule set", "Caution");
            }
        }
        #endregion

        private void SaveRulesToJson(object sender, RoutedEventArgs e)
        {
            if (chosenRules.Count == 0)
            {
                MessageBox.Show("There are no selected rule to save.");
                return;
            }

            string path;
            if (this.currentPresetPath == "")
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "JSON (*.json)|*.json";
                if (dialog.ShowDialog() == false)
                    return;
                path = dialog.FileName;
            }
            else
                path = this.currentPresetPath;

            StreamWriter output;
            try
            {

                List<RuleJsonFormat> ruleJsonFormats = new List<RuleJsonFormat>();

                foreach (var rule in chosenRules)
                {
                    ruleJsonFormats.Add(new RuleJsonFormat
                    {
                        InputStrings = rule.GetParameter().InputStrings,
                        OutputStrings = rule.GetParameter().OutputStrings,
                        Counter = rule.GetParameter().Counter,
                        RuleType = rule.GetRuleType(),
                    });
                }

                output = new StreamWriter(path);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string data = JsonSerializer.Serialize(ruleJsonFormats, options);
                output.Write(data);
                output.Close();
                MessageBox.Show($"Preset Saved Successfully!\nPath: {path}", "Save preset");
                this.currentPresetPath = path;
            }
            catch (IOException ioe)
            {
                MessageBox.Show("Cannot Save Preset!", "Error");
                return;
            }
        }
        private void LoadRulesFromJson(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "JSON (*.json)|*.json";

            if (dialog.ShowDialog() == false)
                return;

            string preset = dialog.FileName;
            string content = File.ReadAllText(preset);

            List<RuleJsonFormat> ruleJsons = new List<RuleJsonFormat>();

            try
            {
                ruleJsons = JsonSerializer.Deserialize<List<RuleJsonFormat>>(content);
            }
            catch (JsonException exception)
            {
                MessageBox.Show("Cannot parse data from the file, check the file again", "Error");
                return;
            }

            this.chosenRules.Clear();

            foreach (RuleJsonFormat ruleJson in ruleJsons)
            {
                IRuleHandler item = rules.SingleOrDefault(Item => Item.GetRuleType().Equals(ruleJson.RuleType));
                if (item != null)
                {
                    IRuleHandler target = item.Clone();
                    target.SetParameter(new RuleParameter
                    {
                        InputStrings = ruleJson.InputStrings,
                        OutputStrings = ruleJson.OutputStrings,
                        Counter = ruleJson.Counter,
                    });
                    this.chosenRules.Add(target);
                }
            }

            MessageBox.Show("Loaded successfully!", "Load preset");
            this.currentPresetPath = preset;
        }

        #region save and load project
        private void SaveProject(object sender, RoutedEventArgs e)
        {
            string path;
            string projectName = "";
            if (this.currentProjectPath == "" || this.currentProjectPath == "autosave.proj")
            {
                // Handle save dialog here
                var dialog = new SaveFileDialog();
                dialog.Filter = "PROJ (*.proj)|*.proj";

                if (dialog.ShowDialog() == false)
                    return;
                path = dialog.FileName;

                // Get the projectName from path
                projectName = path.Substring(path.LastIndexOf('\\') + 1).Split('.')[0];
            }
            else
            {
                path = this.currentProjectPath;
            }

            if (saveProject(path, projectName))
            {
                Title = currentProjectName;
            }
        }
        private void LoadProject(object sender, RoutedEventArgs e)
        {
            // path is loaded project's path
            // if user cancel, it's still empty

            string path = "";
            var dialog = new OpenFileDialog();
            dialog.Filter = "PROJ (*.proj)|*.proj";

            if (dialog.ShowDialog() == false)
                return;

            path = dialog.FileName;
            string name = path.Substring(path.LastIndexOf('\\') + 1).Split('.')[0];

            if (loadProject(path, name))
            {
                MessageBox.Show("Project Loaded Successfully!", "Load Project");
                if (File.Exists("autosave.proj"))
                    File.Delete("autosave.proj");
                Title = currentProjectName;
            }
        }
        private void NewProject(object sender, RoutedEventArgs e)
        {
            // If autosave exists, meaning not opening any project, ask want to save or not
            if (this.currentProjectPath == "" || this.currentProjectPath == "autosave.proj")
            {
                String path, projectName;
                var result = MessageBox.Show("Do you want save your current work before starting a new project?",
                    "Unsaved Changes Detected", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (MessageBoxResult.Yes == result)
                {
                    var dialog = new SaveFileDialog();
                    dialog.Filter = "PROJ (*.proj)|*.proj";

                    if (dialog.ShowDialog() == false)
                        return;
                    path = dialog.FileName;

                    // Get the projectName from path
                    projectName = path.Substring(path.LastIndexOf('\\') + 1).Split('.')[0];
                    if (saveProject(path, projectName))
                    {
                        Title = currentProjectName;
                        MessageBox.Show("Project Saved Successfully!", "Success");
                    }
                }
                else if (MessageBoxResult.No == result)
                {
                }
                else if (MessageBoxResult.Cancel == result)
                    return;
            }
            // If autosave doesn't exists and you're opening a project already, ask want to create new or not
            // If yes, then save user's works to current project
            else
            {
                var result = MessageBox.Show("Do you want save your work to the current project?",
                   "Unsaved Changes Detected", MessageBoxButton.YesNoCancel);
                if (MessageBoxResult.Yes == result)
                {
                    string path = this.currentProjectPath;
                    string projectName = path.Substring(path.LastIndexOf('\\') + 1).Split('.')[0];
                    if (saveProject(path, projectName))
                    {
                        Title = currentProjectName;
                        MessageBox.Show("Project Saved Successfully!", "Success");
                    }
                }
                else if (MessageBoxResult.No == result)
                {
                }
                else if (MessageBoxResult.Cancel == result)
                    return;
            }
            this.currentProjectName = "Unsaved Project";
            this.currentProjectPath = "autosave.proj";
            this.currentPresetPath = "";
            chosenRules.Clear();
            filenames.Clear();
            foldernames.Clear();
            // If autosave doesn't exists and you're not opening any project, just ask only
        }
        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (File.Exists("autosave.proj"))
            {
                var result = MessageBox.Show("Do you want to recover your project using autosave?", "Autosave Detected", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes);

                if (MessageBoxResult.Yes == result)
                {
                    loadProject("autosave.proj", "Unsaved Project");
                }
            }
        }

        private void typeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedItem == null)
                return;
            if (typeComboBox.SelectedItem == "File")
                ItemListView.ItemsSource = filenames;
            if (typeComboBox.SelectedItem == "Folder")
                ItemListView.ItemsSource = foldernames;
        }

        private void ItemsDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length <= 0)
                    return;

                foreach (string file in files)
                {
                    string path = file;
                    string name = path.Substring(file.LastIndexOf('\\') + 1);
                    bool isExisted = false;

                    if (File.Exists(file))
                    {
                        foreach (var filename in filenames)
                            if (filename.CurrentName == name && filename.Path == path)
                            {
                                isExisted = true;
                                break;
                            }

                        if (!isExisted)
                            filenames.Add(new Filename()
                            {
                                CurrentName = name,
                                NewName = "",
                                Path = path,
                                Result = ""
                            });
                    }
                    else
                    {
                        foreach (var foldername in foldernames)
                            if (foldername.CurrentName == name && foldername.Path == path)
                            {
                                isExisted = true;
                                break;
                            }
                        if (!isExisted)
                            foldernames.Add(new Foldername()
                            {
                                CurrentName = name,
                                NewName = "",
                                Path = path,
                                Result = ""
                            });
                    }
                }

                ItemListView.ItemsSource = filenames.Count > 0 ? filenames : foldernames;
                typeComboBox.SelectedItem = filenames.Count > 0 ? "File" : "Folder";
            }
        }
    }
}
