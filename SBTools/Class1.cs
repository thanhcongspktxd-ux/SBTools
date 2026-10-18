using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autodesk.AutoCAD.ApplicationServices;

// Định danh tường minh tránh xung đột UserControl với WinForms
using WpfUserControl = System.Windows.Controls.UserControl;

namespace SBTools
{
    public class CommandModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Shortcut { get; set; }
        public string Category { get; set; }
        public string VideoPath { get; set; }
    }

    public partial class SBToolControl : WpfUserControl
    {
        private List<CommandModel> _allCommands;

        public SBToolControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _allCommands = new List<CommandModel>
            {
                // ========================================
                // LAYER
                // ========================================
                new CommandModel { Name = "FNDS - North Florida Fndn", Title = "ENG S.1 North Florida => N1", Desc1 = "Switch current layer to ENG S.1", Desc2 = "for Foundation Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "FNDS", Category = "Layer" },
                new CommandModel { Name = "LIS - North Florida Line/IS", Title = "ENG S.2 North Florida => N2", Desc1 = "Switch current layer to ENG S.2", Desc2 = "for Lintel Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "LIS", Category = "Layer" },
                new CommandModel { Name = "ROS - North Florida ROS", Title = "ENG S.3 North Florida => N3", Desc1 = "Switch current layer to ENG S.3", Desc2 = "for Roof Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "ROS", Category = "Layer" },
                new CommandModel { Name = "FNDSS - South Florida Fndn", Title = "ENG S.1 South Florida => S1", Desc1 = "Switch current layer to ENG S.1 South Florida", Desc2 = "for Foundation Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "FNDSS", Category = "Layer" },
                new CommandModel { Name = "LISS - South Florida Line/IS", Title = "ENG S.2 South Florida => S2", Desc1 = "Switch current layer to ENG S.2 South Florida", Desc2 = "for Lintel Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "LISS", Category = "Layer" },
                new CommandModel { Name = "ROSS - South Florida ROS", Title = "ENG S.3 South Florida => S3", Desc1 = "Switch current layer to ENG S.3 South Florida", Desc2 = "for Roof Plan drafting.", Desc3 = "Automates layer switching.", Shortcut = "ROSS", Category = "Layer" },
                new CommandModel { Name = "SPE - Slab Penetration", Title = "TM Slab Penetration => W1", Desc1 = "Switch current layer to TM Slab Penetration", Desc2 = "for slab penetration details.", Desc3 = "Layer state management.", Shortcut = "SPE", Category = "Layer" },
                new CommandModel { Name = "FLN - Floor Plan Notes", Title = "TM Floor Notes => W2", Desc1 = "Switch current layer to TM Floor Notes", Desc2 = "for floor annotations.", Desc3 = "Layer state management.", Shortcut = "FLN", Category = "Layer" },
                new CommandModel { Name = "FLD - Floor Plan Dims", Title = "TM Floor Dimensions => W3", Desc1 = "Switch current layer to TM Floor Dimensions", Desc2 = "for dimensioning.", Desc3 = "Layer state management.", Shortcut = "FLD", Category = "Layer" },
                new CommandModel { Name = "FLE - Floor Electrical Plan", Title = "TM Floor Electrical => W4", Desc1 = "Switch current layer to TM Floor Electrical", Desc2 = "for electrical layout.", Desc3 = "Layer state management.", Shortcut = "FLE", Category = "Layer" },
                new CommandModel { Name = "SPELN - Lennar Slab Pen", Title = "Lennar Slab Penetration => R1", Desc1 = "Switch current layer to", Desc2 = "Lennar Slab Penetration.", Desc3 = "Project specific layer.", Shortcut = "SPELN", Category = "Layer" },
                new CommandModel { Name = "FLNLN - Lennar Notes/Dims", Title = "Lennar Floor Notes => R2", Desc1 = "Switch current layer to", Desc2 = "Lennar Floor Notes.", Desc3 = "Project specific layer.", Shortcut = "FLNLN", Category = "Layer" },
                new CommandModel { Name = "FLELN - Lennar Electrical", Title = "Lennar Electrical => R3", Desc1 = "Switch current layer to", Desc2 = "Lennar Electrical.", Desc3 = "Project specific layer.", Shortcut = "FLELN", Category = "Layer" },

                // ========================================
                // BLOCK
                // ========================================
                new CommandModel { Name = "EB - Edit Block", Title = "Edit Block", Desc1 = "Open Block Editor to modify", Desc2 = "the selected block definition.", Desc3 = "Quick editing tool.", Shortcut = "EB", Category = "Block" },
                new CommandModel { Name = "RN - Rename Block", Title = "Rename Block", Desc1 = "Rename an existing block definition", Desc2 = "while keeping all references.", Desc3 = "Block management.", Shortcut = "RN", Category = "Block" },
                new CommandModel { Name = "SB - Select Blocks", Title = "Select Blocks", Desc1 = "Select every block reference", Desc2 = "that has the same block name.", Desc3 = "Quick selection.", Shortcut = "SB", Category = "Block" },
                new CommandModel { Name = "BR - Change Base Point", Title = "Change Base Point", Desc1 = "Change the insertion point", Desc2 = "of one selected block.", Desc3 = "Block utility.", Shortcut = "BR", Category = "Block" },
                new CommandModel { Name = "BRR - Change Base Point Multiple", Title = "Change Base Point Multiple", Desc1 = "Change insertion points for", Desc2 = "multiple selected block definitions.", Desc3 = "Batch block utility.", Shortcut = "BRR", Category = "Block" },
                new CommandModel { Name = "HB - Hide Blocks", Title = "Hide Blocks", Desc1 = "Temporarily hide selected block", Desc2 = "references from the drawing.", Desc3 = "Visibility toggle.", Shortcut = "HB", Category = "Block" },
                new CommandModel { Name = "EE - Update Elevation Block", Title = "Update quick elevation block", Desc1 = "Update the quick elevation block", Desc2 = "with the current drawing elevation.", Desc3 = "Elevation tool.", Shortcut = "EE", Category = "Block" },

                // ========================================
                // TEXT
                // ========================================
                new CommandModel { Name = "TA - Text Align", Title = "Text Align", Desc1 = "Align multiple Text or MText objects", Desc2 = "to a common position.", Desc3 = "Text formatting helper.", Shortcut = "TA", Category = "Text" },
                new CommandModel { Name = "ET - Edit Text", Title = "Edit Text", Desc1 = "Quickly edit the contents of Text", Desc2 = "and MText objects.", Desc3 = "Fast text editing.", Shortcut = "ET", Category = "Text" },
                new CommandModel { Name = "BG - Background Mask", Title = "Background Mask", Desc1 = "Create or remove Background Mask", Desc2 = "for selected text objects.", Desc3 = "Text visual enhancement.", Shortcut = "BG", Category = "Text" },

                // ========================================
                // DIMENSION
                // ========================================
                new CommandModel { Name = "SD - Split Dimension", Title = "Split Dimension", Desc1 = "Split one linear dimension into", Desc2 = "two connected dimensions.", Desc3 = "Dimension utility.", Shortcut = "SD", Category = "Dimension" },
                new CommandModel { Name = "MD - Merge Dimension", Title = "Merge Dimension", Desc1 = "Merge multiple adjacent dimensions", Desc2 = "into one dimension.", Desc3 = "Dimension utility.", Shortcut = "MD", Category = "Dimension" },

                // ========================================
                // ZOOM
                // ========================================
                new CommandModel { Name = "ZE - Zoom Extents", Title = "Zoom Extents", Desc1 = "Zoom to display all visible objects", Desc2 = "in the current space.", Desc3 = "Navigation tool.", Shortcut = "ZE", Category = "Zoom" },
                new CommandModel { Name = "ZEA - Zoom Extents All", Title = "Zoom Extents All", Desc1 = "Zoom Extents for Model Space", Desc2 = "and every Layout.", Desc3 = "Full view navigation.", Shortcut = "ZEA", Category = "Zoom" },

                // ========================================
                // OTHER
                // ========================================
                new CommandModel { Name = "QS - Quick Select", Title = "Quick Select", Desc1 = "Launch Quick Select to filter", Desc2 = "drawing objects by properties.", Desc3 = "Selection utility.", Shortcut = "QS", Category = "Other" },
                new CommandModel { Name = "BINDALL - Bind All Xrefs", Title = "Bind All Xrefs", Desc1 = "Automatically bind every attached", Desc2 = "Xref into the current drawing.", Desc3 = "Drawing cleanup.", Shortcut = "BINDALL", Category = "Other" },
                new CommandModel { Name = "PF - Export Product Fit", Title = "Export Product Fit", Desc1 = "Export the selected Product Fit", Desc2 = "layout into a clean DWG file.", Desc3 = "Export utility.", Shortcut = "PF", Category = "Other" },
                new CommandModel { Name = "PBS - Save + Publish", Title = "Save + Publish", Desc1 = "Save the drawing and publish all", Desc2 = "layouts automatically.", Desc3 = "Batch output tool.", Shortcut = "PBS", Category = "Other" },
                new CommandModel { Name = "FT - Filter Text", Title = "Filter Text", Desc1 = "Search, filter and locate Text or MText", Desc2 = "objects by content.", Desc3 = "Search utility.", Shortcut = "FT", Category = "Other" },
                new CommandModel { Name = "SENDTOBACK - Send To Back", Title = "Send To Back", Desc1 = "Move selected objects behind all", Desc2 = "other overlapping objects.", Desc3 = "Draw order utility.", Shortcut = "SENDTOBACK", Category = "Other" },
                new CommandModel { Name = "BRINGTOFRONT - Bring To Front", Title = "Bring To Front", Desc1 = "Move selected objects above all", Desc2 = "overlapping objects.", Desc3 = "Draw order utility.", Shortcut = "BRINGTOFRONT", Category = "Other" },
                new CommandModel { Name = "MH - Merge Hatches", Title = "Merge Hatches", Desc1 = "Merge separate hatches into one hatch.", Desc2 = "Hatch management tool.", Desc3 = "Efficiency booster.", Shortcut = "MH", Category = "Other" },
                new CommandModel { Name = "BF - Smart Draw Order", Title = "Smart Draw Order", Desc1 = "Automatically reorder Hatch, Wipeout,", Desc2 = "Lines and Blocks in the correct order.", Desc3 = "Automated cleanup.", Shortcut = "BF", Category = "Other" },
                new CommandModel { Name = "PV - Paste Layout to Layout", Title = "Paste from Layout to Layout", Desc1 = "Paste objects from one", Desc2 = "layout to another.", Desc3 = "Layout utility.", Shortcut = "PV", Category = "Other" }
            };

            CbCategory.Items.Clear();
            CbCategory.Items.Add("All Categories");
            var categories = _allCommands.Select(c => c.Category).Distinct();
            foreach (var cat in categories)
            {
                CbCategory.Items.Add(cat);
            }
            CbCategory.SelectedIndex = 0;

            UpdateCommandList(_allCommands);
        }

        private void UpdateCommandList(IEnumerable<CommandModel> commands)
        {
            var list = commands.ToList();
            LbCommands.ItemsSource = list;

            if (TxtTotal != null)
            {
                TxtTotal.Text = $"Total: {list.Count} Commands";
            }

            if (list.Count > 0)
            {
                LbCommands.SelectedIndex = 0;
            }
        }

        private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbCategory.SelectedItem is string selectedCategory)
            {
                if (selectedCategory == "All Categories")
                {
                    UpdateCommandList(_allCommands);
                }
                else
                {
                    var filtered = _allCommands.Where(c => c.Category == selectedCategory).ToList();
                    UpdateCommandList(filtered);
                }
            }
        }

        /// <summary>
        /// Logic tự động định tuyến đường dẫn video:
        /// 1. Tìm theo mã phím tắt riêng (VD: ZE.mp4, ET.mp4, EB.mp4)
        /// 2. Nếu là 3 lệnh Lennar thì dùng chung Layer.mp4
        /// </summary>
        private string GetResolvedVideoPath(CommandModel cmd)
        {
            string videoFolder = @"C:\Users\Admin\Desktop\Tool Video\";

            // 1. Đường dẫn file video riêng theo tên lệnh (VD: EB.mp4, ZE.mp4, ET.mp4...)
            string specificVideoPath = Path.Combine(videoFolder, $"{cmd.Shortcut}.mp4");

            // 2. Đường dẫn video dùng chung cho nhóm Engineering North/South Florida (LayEng.mp4)
            string layEngVideoPath = Path.Combine(videoFolder, "LayEng.mp4");

            // 3. Đường dẫn video dùng chung cho nhóm TM Layer (LayTM.mp4)
            string layTMVideoPath = Path.Combine(videoFolder, "LayTM.mp4");

            // 4. Đường dẫn video dùng chung cho nhóm Lennar (Layer.mp4)
            string layerVideoPath = Path.Combine(videoFolder, "Layer.mp4");

            // Ưu tiên 1: Nếu có file video riêng trùng tên Shortcut thì dùng file riêng
            if (File.Exists(specificVideoPath))
            {
                return specificVideoPath;
            }
            // Ưu tiên 2: 6 lệnh Engineering dùng chung LayEng.mp4
            else if ((cmd.Shortcut == "FNDS" || cmd.Shortcut == "LIS" || cmd.Shortcut == "ROS" ||
                      cmd.Shortcut == "FNDSS" || cmd.Shortcut == "LISS" || cmd.Shortcut == "ROSS")
                     && File.Exists(layEngVideoPath))
            {
                return layEngVideoPath;
            }
            // Ưu tiên 3: 4 lệnh TM Layer dùng chung LayTM.mp4
            else if ((cmd.Shortcut == "SPE" || cmd.Shortcut == "FLN" || cmd.Shortcut == "FLD" || cmd.Shortcut == "FLE")
                     && File.Exists(layTMVideoPath))
            {
                return layTMVideoPath;
            }
            // Ưu tiên 4: 3 lệnh Lennar dùng chung Layer.mp4
            else if ((cmd.Shortcut == "SPELN" || cmd.Shortcut == "FLNLN" || cmd.Shortcut == "FLELN")
                     && File.Exists(layerVideoPath))
            {
                return layerVideoPath;
            }

            return string.Empty;
        }
        private void LbCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbCommands.SelectedItem is CommandModel cmd)
            {
                TxtTitle.Text = cmd.Title;
                TxtDesc1.Text = cmd.Desc1;
                TxtDesc2.Text = cmd.Desc2;
                TxtDesc3.Text = cmd.Desc3;
                TxtShortcut.Text = cmd.Shortcut;
                TxtCatDetail.Text = cmd.Category;

                string finalVideoPath = GetResolvedVideoPath(cmd);

                if (!string.IsNullOrEmpty(finalVideoPath) && File.Exists(finalVideoPath))
                {
                    CmdVideo.Source = new Uri(finalVideoPath);
                    CmdVideo.Play();
                    CmdVideo.Visibility = Visibility.Visible;
                    TxtNoVideo.Visibility = Visibility.Collapsed;
                    BtnWatchVideo.IsEnabled = true;
                }
                else
                {
                    CmdVideo.Stop();
                    CmdVideo.Source = null;
                    CmdVideo.Visibility = Visibility.Collapsed;
                    TxtNoVideo.Text = $"No video ({cmd.Shortcut}.mp4)";
                    TxtNoVideo.Visibility = Visibility.Visible;
                    BtnWatchVideo.IsEnabled = false;
                }
            }
        }

        // Click nút "Watch Video" mở file video bằng phần mềm mặc định của hệ thống
        private void BtnWatchVideo_Click(object sender, RoutedEventArgs e)
        {
            if (LbCommands.SelectedItem is CommandModel cmd)
            {
                string videoPath = GetResolvedVideoPath(cmd);
                if (!string.IsNullOrEmpty(videoPath) && File.Exists(videoPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = videoPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"Chưa tìm thấy file video cho lệnh {cmd.Shortcut}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Bắt sự kiện nhấp đúp chuột (ClickCount == 2) để mở cửa sổ phóng to
        private void CmdVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && LbCommands.SelectedItem is CommandModel cmd)
            {
                string finalVideoPath = GetResolvedVideoPath(cmd);

                if (!string.IsNullOrEmpty(finalVideoPath) && File.Exists(finalVideoPath))
                {
                    CmdVideo.Pause();

                    VideoPlayerWindow playerWin = new VideoPlayerWindow(finalVideoPath, cmd.Title);
                    playerWin.ShowDialog();

                    CmdVideo.Play();
                }
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            if (LbCommands.SelectedItem is CommandModel cmd)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                if (doc != null && !string.IsNullOrEmpty(cmd.Shortcut))
                {
                    doc.SendStringToExecute(cmd.Shortcut + " ", true, false, true);
                }
            }

            PluginCommands.HidePalette();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            PluginCommands.HidePalette();
        }
    }
}