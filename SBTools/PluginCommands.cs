using System;
using System.IO;
using System.Windows.Forms.Integration;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;

namespace SBTools
{
    public class PluginCommands
    {
        private static PaletteSet _paletteSet;
        private static SBToolControl _toolControl;

        [CommandMethod("SBTOOL")]
        public static void ShowSBTool()
        {
            if (_paletteSet == null)
            {
                _paletteSet = new PaletteSet("SB Quick Tool", new Guid("12345678-ABCD-EF01-2345-6789ABCDEF01"));
                _paletteSet.Style = PaletteSetStyles.ShowPropertiesMenu |
                                    PaletteSetStyles.ShowCloseButton |
                                    PaletteSetStyles.Snappable;

                _paletteSet.MinimumSize = new System.Drawing.Size(550, 450);

                _toolControl = new SBToolControl();

                ElementHost host = new ElementHost();
                host.Dock = System.Windows.Forms.DockStyle.Fill;
                host.Child = _toolControl;

                _paletteSet.Add("Tools", host);
            }

            _paletteSet.Visible = true;
        }

        // Hàm ẩn palette được gọi từ SBToolControl khi bấm Run hoặc Close
        public static void HidePalette()
        {
            if (_paletteSet != null)
            {
                _paletteSet.Visible = false;
            }
        }
    }
}