using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace XisoForgeGUI.View
{
    public partial class MainWindow : Window
    {
        string isoPath = "";
        string targetDir = "";
        string mode = "-x"; // Valeur par défaut pour l'extraction

        private Process currentProcess;
        private string extractXisoPath;

        public MainWindow()
        {
            try
            {
                // Initialize UI components first
                InitializeComponent();

                // Initialize log once components are ready
                if (TxtLog != null && TxtLog.Document == null)
                {
                    TxtLog.Document = new FlowDocument();
                }

                // Define the path to extract-xiso.exe (in the same folder as the application)
                extractXisoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "extract-xiso.exe");

                // Check if extract-xiso.exe exists
                if (!File.Exists(extractXisoPath))
                {
                    AppendLog("ERROR: The file extract-xiso.exe was not found in the application folder.", Brushes.Red);
                }
                ApplyModeSettings("Extract"); // Appel direct avec le Tag par défaut "Extract"
                if (CmbMode.Items.Count > 0)
                {
                    foreach (ComboBoxItem item in CmbMode.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString() == "Extract")
                        {
                            CmbMode.SelectedItem = item;
                            break;
                        }
                    }
                    // Fallback au cas où l'élément "Extract" ne serait pas trouvé (bien qu'il devrait l'être)
                    if (CmbMode.SelectedItem == null)
                    {
                        CmbMode.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle startup exception - use MessageBox directly as TxtLog might not be initialized
                MessageBox.Show($"Initialization Error: {ex.Message}\n\nDetails: {ex.InnerException?.Message}",
                               "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Allow window dragging by clicking the header - separate from the main try/catch block
            try
            {
                this.MouseLeftButtonDown += (s, e) =>
                {
                    if (e.ButtonState == MouseButtonState.Pressed)
                    {
                        this.DragMove();
                    }
                };
            }
            catch (Exception ex)
            {
                // Ignore this exception, it's not critical
                Debug.WriteLine($"Exception during DragMove setup: {ex.Message}");
            }
        }

        // Method to add colored text to the RichTextBox
        private void AppendLog(string text, Brush color = null)
        {
            try
            {
                if (color == null)
                    color = Brushes.White; // Default color

                if (TxtLog == null)
                    return;

                // Ensure the document exists
                if (TxtLog.Document == null)
                {
                    TxtLog.Document = new FlowDocument();
                }

                var paragraph = TxtLog.Document.Blocks.LastBlock as Paragraph;
                if (paragraph == null) // Create a new paragraph if one doesn't exist or the last block isn't a paragraph
                {
                    paragraph = new Paragraph();
                    TxtLog.Document.Blocks.Add(paragraph);
                }

                paragraph.Inlines.Add(new Run(text) { Foreground = color });
                paragraph.Inlines.Add(new LineBreak());
                TxtLog.ScrollToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while appending to log: {ex.Message}");
                // Do not rethrow to avoid error loops
            }
        }

        // To clear the log
        private void ClearLog()
        {
            try
            {
                if (TxtLog != null && TxtLog.Document != null)
                    TxtLog.Document.Blocks.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while clearing log: {ex.Message}");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnChooseIso_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "ISO files (*.iso)|*.iso",
                Title = "Select an ISO file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                isoPath = openFileDialog.FileName;
                TxtIsoPath.Text = isoPath;
            }
        }

        private void BtnChooseOutput_Click(object sender, RoutedEventArgs e)
        {
            // Ce bouton sera désactivé pour le mode 'Extract' via ApplyModeSettings
            var openFolderDialog = new Microsoft.Win32.OpenFolderDialog
            {
                Title = "Choose the destination folder"
            };

            if (openFolderDialog.ShowDialog() == true)
            {
                targetDir = openFolderDialog.FolderName;
                TxtOutputDir.Text = targetDir;
            }
        }

        // Méthode centrale pour appliquer les paramètres de l'UI en fonction du mode sélectionné (via son Tag)
        private void ApplyModeSettings(string selectedModeTag)
        {
            switch (selectedModeTag)
            {
                case "Extract":
                    mode = "-x";
                    BtnChooseIso.IsEnabled = true;
                    TxtIsoPath.IsEnabled = true;
                    BtnChooseOutput.IsEnabled = false;
                    TxtOutputDir.IsEnabled = false;
                    targetDir = AppDomain.CurrentDomain.BaseDirectory;
                    TxtOutputDir.Text = targetDir;
                    AppendLog("Note: For 'Extract' mode, the output folder is set to the application directory.", Brushes.LightYellow);
                    break;

                case "Create":
                    mode = "-c";
                    BtnChooseIso.IsEnabled = false;
                    TxtIsoPath.IsEnabled = false;
                    BtnChooseOutput.IsEnabled = true;
                    TxtOutputDir.IsEnabled = true;
                    if (TxtOutputDir.Text == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        TxtOutputDir.Text = "";
                        targetDir = "";
                    }
                    AppendLog("Mode 'Create': Please select a source folder to convert into ISO.", Brushes.LightBlue);
                    break;

                case "List":
                    mode = "-l";
                    BtnChooseIso.IsEnabled = true;
                    TxtIsoPath.IsEnabled = true;
                    BtnChooseOutput.IsEnabled = false;
                    TxtOutputDir.IsEnabled = false;
                    if (TxtOutputDir.Text == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        TxtOutputDir.Text = "";
                        targetDir = "";
                    }
                    AppendLog("Mode 'List': Please select an ISO file.", Brushes.LightBlue);
                    break;

                case "Rewrite":
                    mode = "-r";
                    BtnChooseIso.IsEnabled = true;
                    TxtIsoPath.IsEnabled = true;
                    BtnChooseOutput.IsEnabled = false;
                    TxtOutputDir.IsEnabled = false;
                    if (TxtOutputDir.Text == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        TxtOutputDir.Text = "";
                        targetDir = "";
                    }
                    AppendLog("Mode 'Rewrite': Please select an ISO file.", Brushes.LightBlue);
                    break;

                default:
                    ShowError($"Unknown mode detected: {selectedModeTag}");
                    break;
            }
        }

        private void CmbMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            if (CmbMode.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag != null)
            {
                ClearLog(); // Clear log on mode change
                string selectedModeTag = selectedItem.Tag.ToString();
                ApplyModeSettings(selectedModeTag);
            }
        }


        private async void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            ClearLog();
            AppendLog("Starting process...");

            if (mode != "-c" && string.IsNullOrWhiteSpace(isoPath))
            {
                ShowError("Veuillez sélectionner un fichier ISO.");
                return;
            }

            if (mode == "-c" && string.IsNullOrWhiteSpace(targetDir))
            {
                ShowError("Veuillez sélectionner le dossier source à convertir en ISO.");
                return;
            }

            if (mode == "-x")
            {
                if (!Directory.Exists(targetDir))
                {
                    try
                    {
                        Directory.CreateDirectory(targetDir);
                        AppendLog($"Création du dossier '{targetDir}'");
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Impossible de créer le dossier cible : {ex.Message}");
                        return;
                    }
                }
            }
            else if (mode == "-c")
            {
                if (!Directory.Exists(targetDir))
                {
                    ShowError($"Le dossier source '{targetDir}' n'existe pas.");
                    return;
                }
            }

            string arguments = BuildArguments();
            if (string.IsNullOrEmpty(arguments))
            {
                return;
            }

            // Debug logs
            AppendLog($"DEBUG: ISO Path (isoPath): \"{isoPath}\"", Brushes.Cyan);
            AppendLog($"DEBUG: Target Directory (targetDir): \"{targetDir}\"", Brushes.Cyan);
            AppendLog($"DEBUG: Mode: \"{mode}\"", Brushes.Cyan);
            AppendLog($"DEBUG: Arguments for extract-xiso: {arguments}", Brushes.Cyan);

            try
            {
                AppendLog($"Executing: extract-xiso {arguments}");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = extractXisoPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                currentProcess = new Process
                {
                    StartInfo = startInfo,
                    EnableRaisingEvents = true
                };

                var processCompletionTask = new TaskCompletionSource<bool>();

                currentProcess.OutputDataReceived += (s, ea) =>
                {
                    if (!string.IsNullOrEmpty(ea.Data))
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke(() => AppendLog(ea.Data));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error receiving output data: {ex.Message}");
                        }
                    }
                };

                currentProcess.ErrorDataReceived += (s, ea) =>
                {
                    if (!string.IsNullOrEmpty(ea.Data))
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke(() => AppendLog("ERROR: " + ea.Data, Brushes.Red));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error receiving error data: {ex.Message}");
                        }
                    }
                };

                currentProcess.Exited += (s, ea) =>
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                int exitCode = currentProcess.ExitCode;
                                if (exitCode == 0)
                                {
                                    // ✅ Succès général
                                    //AppendLog("Process completed successfully.", Brushes.LightGreen);

                                    // ✅ Message spécifique par mode
                                    switch (mode)
                                    {
                                        case "-x":
                                            AppendLog("Extraction completed successfully.", Brushes.LightGreen);
                                            try
                                            {
                                                int fileCount = Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories).Length;
                                                AppendLog($"{fileCount} file(s) found in: {targetDir}", Brushes.LightYellow);
                                            }
                                            catch (Exception ex)
                                            {
                                                AppendLog($"Could not verify extracted files: {ex.Message}", Brushes.Orange);
                                            }
                                            break;
                                        case "-c":
                                            AppendLog("ISO creation completed successfully.", Brushes.LightGreen);
                                            break;
                                        case "-l":
                                            AppendLog("Listing completed successfully.", Brushes.LightGreen);
                                            break;
                                        case "-r":
                                            AppendLog("Rewrite completed successfully.", Brushes.LightGreen);
                                            break;
                                        default:
                                            AppendLog("Operation completed successfully.", Brushes.LightGreen);
                                            break;
                                    }
                                }
                                else
                                {
                                    AppendLog($"Process finished with error code: {exitCode}", Brushes.Red);
                                }
                            }
                            catch (InvalidOperationException ex)
                            {
                                AppendLog($"Error retrieving process exit details (possibly cancelled): {ex.Message}", Brushes.Orange);
                            }
                            catch (Exception ex)
                            {
                                AppendLog($"Error during process finalization: {ex.Message}", Brushes.Red);
                            }
                            finally
                            {
                                BtnRun.IsEnabled = true;
                                BtnCancel.IsEnabled = false;
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error in Exited event dispatcher: {ex.Message}");
                    }
                    finally
                    {
                        processCompletionTask.TrySetResult(true);
                    }
                };

                BtnRun.IsEnabled = false;
                BtnCancel.IsEnabled = true;

                currentProcess.Start();
                currentProcess.BeginOutputReadLine();
                currentProcess.BeginErrorReadLine();

                await processCompletionTask.Task; // Attendre la fin du processus
            }
            catch (Exception ex)
            {
                ShowError($"Failed to run process: {ex.Message}");
                BtnRun.IsEnabled = true;
                BtnCancel.IsEnabled = false;
            }
        }
        private string BuildArguments()
        {
            // Pour les modes qui ne nécessitent pas de dossier cible (comme -x, -l, -r)
            if (mode == "-x" || mode == "-l" || mode == "-r")
            {
                return $"{mode} \"{isoPath}\"";
            }

            // Pour la création d'ISO (par exemple avec -c)
            if (mode == "-c")
            {
                return $"{mode} \"{targetDir}\"";
            }

            // Fallback (si jamais)
            return $"{mode} \"{isoPath}\"";
        }

        //private string BuildArguments()
        //{
        //    string arguments = "";

        //    switch (mode) // -x, -c, -l, -r
        //    {
        //        case "-x": // Extraction
        //            if (string.IsNullOrWhiteSpace(isoPath))
        //            {
        //                ShowError("Le chemin ISO doit être spécifié pour l'extraction.");
        //                return null;
        //            }

        //            string finalExtractDir = string.IsNullOrWhiteSpace(targetDir)
        //                ? Path.Combine(Path.GetDirectoryName(isoPath), Path.GetFileNameWithoutExtension(isoPath))
        //                : targetDir;

        //            // CORRECT ORDER : ISO first, then -d <dir>
        //            arguments = $"\"{isoPath}\" -d \"{finalExtractDir}\"";
        //            break;


        //        case "-c": // Création ISO
        //            if (string.IsNullOrWhiteSpace(targetDir))
        //            {
        //                ShowError("Le dossier source doit être spécifié pour la création d'ISO.");
        //                return null;
        //            }

        //            string sourceFolderForIso = targetDir;
        //            string outputIsoName = Path.GetFileName(sourceFolderForIso) + ".iso";
        //            string outputIsoPath = Path.Combine(Path.GetDirectoryName(sourceFolderForIso), outputIsoName);

        //            arguments = $"-c \"{sourceFolderForIso}\" \"{outputIsoPath}\"";
        //            break;

        //        case "-l": // Lister le contenu ISO
        //            if (string.IsNullOrWhiteSpace(isoPath))
        //            {
        //                ShowError("Le chemin ISO doit être spécifié pour lister le contenu.");
        //                return null;
        //            }

        //            arguments = $"-l \"{isoPath}\"";
        //            break;

        //        case "-r": // Réécriture
        //            if (string.IsNullOrWhiteSpace(isoPath))
        //            {
        //                ShowError("Le chemin ISO doit être spécifié pour la réécriture.");
        //                return null;
        //            }

        //            arguments = $"-r \"{isoPath}\"";
        //            break;

        //        default:
        //            ShowError($"Mode inconnu : {mode}");
        //            return null;
        //    }

        //    return arguments;
        //}

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (currentProcess != null && !currentProcess.HasExited)
            {
                try
                {
                    currentProcess.Kill(); // Forcefully stop the process
                    currentProcess.WaitForExit(1000); // Give it a moment to register as exited
                    AppendLog("Process cancelled by user.", Brushes.Orange);
                }
                catch (InvalidOperationException ex) // Process may have already exited
                {
                    AppendLog($"Notice: Process was already exiting or could not be killed: {ex.Message}", Brushes.Yellow);
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to cancel process: {ex.Message}");
                }
            }
            else
            {
                AppendLog("No active process to cancel.", Brushes.Yellow);
            }
            // Ensure UI state is reset correctly
            BtnRun.IsEnabled = true;
            BtnCancel.IsEnabled = false;
        }

        private void ShowError(string message)
        {
            AppendLog("ERROR: " + message, Brushes.Red);
        }
    }
}