﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace AStyleExtension {
    public enum Language {
        C,
        CSharp,
        Java
    }

    public sealed partial class AStyleGeneralOptionsForm : Form {
        Dictionary<string, CheckBox> _checkboxDic;
        private Language _language;
        private const string HelpLink = "http://astyle.sourceforge.net/astyle.html";

        Dictionary<string, string> _styleDic = new Dictionary<string, string> {
            { "", "None" },
            { "--style=allman", "Allman" },
            { "--style=java", "Java" },
            { "--style=kr", "K&R" },
            { "--style=stroustrup", "Stroustrup" },
            { "--style=whitesmith", "Whitesmith" },
            { "--style=banner", "Banner" },
            { "--style=gnu", "GNU" },
            { "--style=linux", "Linux" },
            { "--style=horstmann", "Horstmann" },
            { "--style=1tbs", "1TBS" },
            { "--style=pico", "Pico" },
            { "--style=lisp", "Lisp" }
        };

        public AStyleGeneralOptionsForm(Language language) {
            InitializeComponent();
            _language = language;

            if (_language == Language.CSharp) {
                Text = "AStyle C# Settings";
            } else if (_language == Language.C) {
                Text = "AStyle C/C++ Settings";
            }

            _checkboxDic = new Dictionary<string, CheckBox> {
                { "--indent-classes", checkBoxIndentClasses },
                { "--indent-switches", checkBoxIndentSwitches },
                { "--indent-cases", checkBoxIndentCases },
                { "--indent-namespaces", checkBoxIndentNamesp },
                { "--indent-labels", checkBoxIndentLabels },
                { "--indent-preprocessor", checkBoxIndentPreproc },
                { "--indent-col1-comments", checkBoxIndentCol1Com },
                { "--break-blocks", checkBoxBreakBlocks },
                { "--break-blocks=all", checkBoxBreakBlocksAll },
                { "--pad-oper", checkBoxPadOper },
                { "--pad-paren", checkBoxPadParen },
                { "--pad-paren-out", checkBoxPadParenOut },
                { "--pad-paren-in", checkBoxPadParenIn },
                { "--pad-header", checkBoxPadHeader },
                { "--unpad-paren", checkBoxUnpadParen },
                { "--delete-empty-lines", checkBoxDelEmptyLines },
                { "--fill-empty-lines", checkBoxFillEmptyLines },
                { "--break-closing-brackets", checkBoxBreakClosingBrackets },
                { "--break-elseifs", checkBoxBreakElseIfs },
                { "--add-brackets", checkBoxAddBrackets },
                { "--add-one-line-brackets", checkBoxAddOneLineBrackets },
                { "--keep-one-line-blocks", checkBoxKeepOneLineBlocks },
                { "--keep-one-line-statements", checkBoxKeepOneLineStat },
                { "--convert-tabs", checkBoxConvertTabs }
            };

            toolTip.SetToolTip(checkBoxIndent, "Indent using spaces or tab characters.");
            toolTip.SetToolTip(checkBoxIndentClasses, "Indent 'class' and 'struct' blocks.");
            toolTip.SetToolTip(checkBoxIndentSwitches, "Indent 'switch' blocks.");
            toolTip.SetToolTip(checkBoxIndentCases, "Indent 'case X:' blocks from the 'case X:' headers.");
            toolTip.SetToolTip(checkBoxIndentNamesp, "Add extra indentation to namespace blocks.");
            toolTip.SetToolTip(checkBoxIndentLabels, "Add extra indentation to labels so they appear 1 indent less than the current indentation.");
            toolTip.SetToolTip(checkBoxIndentPreproc, "Indent multi-line preprocessor definitions ending with a backslash.");
            toolTip.SetToolTip(checkBoxIndentCol1Com, "Indent C++ comments beginning in column one.");
            toolTip.SetToolTip(checkBoxMinCondIndent, "Set the minimal indent that is added when a header is built of multiple lines.");
            toolTip.SetToolTip(checkBoxMaxInstateIndent, "Set the maximum number of spaces to indent a continuation line.");
            toolTip.SetToolTip(checkBoxBreakBlocks, "Pad empty lines around header blocks (e.g. 'if', 'for', 'while').");
            toolTip.SetToolTip(checkBoxBreakBlocksAll, "Pad empty lines around header blocks (e.g. 'if', 'for', 'while'). Treat closing header blocks (e.g. 'else', 'catch') as stand-alone blocks.");
            toolTip.SetToolTip(checkBoxPadOper, "Insert space padding around operators.");
            toolTip.SetToolTip(checkBoxPadParen, "Insert space padding around parenthesis on both the outside and the inside.");
            toolTip.SetToolTip(checkBoxPadParenOut, "Insert space padding around parenthesis on the outside only.");
            toolTip.SetToolTip(checkBoxPadParenIn, "Insert space padding around parenthesis on the inside only.");
            toolTip.SetToolTip(checkBoxPadHeader, "Insert space padding after paren headers only (e.g. 'if', 'for', 'while').");
            toolTip.SetToolTip(checkBoxUnpadParen, "Remove extra space padding around parenthesis on the inside and outside.");
            toolTip.SetToolTip(checkBoxDelEmptyLines, "Delete empty lines within a function or method. Empty lines outside of functions or methods are not deleted.");
            toolTip.SetToolTip(checkBoxFillEmptyLines, "Fill empty lines with the white space of the previous line.");
            toolTip.SetToolTip(checkBoxBreakClosingBrackets, "Breaks closing headers (e.g. 'else', 'catch') from their immediately preceding closing brackets.");
            toolTip.SetToolTip(checkBoxBreakElseIfs, "Break 'else if' header combinations into separate lines.");
            toolTip.SetToolTip(checkBoxAddBrackets, "Add brackets to unbracketed one line conditional statements (e.g. 'if', 'for', 'while').");
            toolTip.SetToolTip(checkBoxAddOneLineBrackets, "Add one line brackets to unbracketed one line conditional statements  (e.g. 'if', 'for', 'while').");
            toolTip.SetToolTip(checkBoxKeepOneLineBlocks, "Don't break one-line blocks.");
            toolTip.SetToolTip(checkBoxKeepOneLineStat, "Don't break complex statements and multiple statements residing on a single line.");
            toolTip.SetToolTip(checkBoxConvertTabs, "Converts tabs into spaces in the non-indentation part of the line.");
            toolTip.SetToolTip(checkBoxAlignPointer, "Attach a pointer or reference operator (* or &) to either the variable type or variable name, or in between.");
            toolTip.SetToolTip(checkBoxAlignReference, "Attach a reference operator (&) to either the variable type or variable name, or in between.");

            comboBoxStyle.SelectedIndex = 0;

            OnCheckBoxIndentCheckedChanged(checkBoxIndent, null);
            OnCheckBoxMinCondIndentCheckedChanged(checkBoxMinCondIndent, null);
            OnCheckBoxMaxInstateIndentCheckedChanged(checkBoxMaxInstateIndent, null);
            OnCheckBoxAlignPointerCheckedChanged(checkBoxAlignPointer, null);
            OnCheckBoxAlignReferenceCheckedChanged(checkBoxAlignReference, null);
        }

        public void SetControls(string command) {
            switch (_language) {
                case Language.CSharp:
                    comboBoxMode.SelectedIndex = 1;
                    break;
                case Language.C:
                    comboBoxMode.SelectedIndex = 0;
                    break;
            }

            if (string.IsNullOrEmpty(command)) {
                return;
            }

            var optionsList = new List<string>();
            optionsList.AddRange(command.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            foreach (string option in optionsList) {
                int pos;
                int no;
                if (option.StartsWith("--style=")) {
                    string val;
                    if (_styleDic.TryGetValue(option, out val)) {
                        comboBoxStyle.SelectedItem = val;
                    }
                } else if (option.StartsWith("--indent=spaces=") || option.StartsWith("--indent=tab=") || option.StartsWith("--indent=force-tab=")) {
                    pos = option.LastIndexOf("=", StringComparison.Ordinal);

                    if (int.TryParse(option.Substring(pos + 1), out no)) {
                        numericUpDownIndent.Value = no;
                        string[] parts = option.Split('=');
                        comboBoxIndent.SelectedItem = parts[1];
                        checkBoxIndent.Checked = true;
                    }
                } else if (option.StartsWith("--min-conditional-indent=")) {
                    pos = option.LastIndexOf("=", StringComparison.Ordinal);

                    if (int.TryParse(option.Substring(pos + 1), out no)) {
                        if (no >= 0 || no <= 4) {
                            numericUpDownMinCondIndent.Value = no;
                            checkBoxMinCondIndent.Checked = true;
                        }
                    }
                } else if (option.StartsWith("--max-instatement-indent=")) {
                    pos = option.LastIndexOf("=", StringComparison.Ordinal);

                    if (int.TryParse(option.Substring(pos + 1), out no)) {
                        if (no >= 10 || no <= 120) {
                            numericUpDownMaxInstateIndent.Value = no;
                            checkBoxMaxInstateIndent.Checked = true;
                        }
                    }
                } else if (option.StartsWith("--align-pointer=")) {
                    pos = option.LastIndexOf("=", StringComparison.Ordinal);
                    comboBoxAlignPointer.SelectedItem = option.Substring(pos + 1);
                    checkBoxAlignPointer.Checked = true;
                } else if (option.StartsWith("--align-reference=")) {
                    pos = option.LastIndexOf("=", StringComparison.Ordinal);
                    comboBoxAlignReference.SelectedItem = option.Substring(pos + 1);
                    checkBoxAlignReference.Checked = true;
                } else {
                    CheckBox cb;
                    if (_checkboxDic.TryGetValue(option, out cb)) {
                        cb.Checked = true;
                    }
                }
            }
        }

        public string GetCommandLine() {
            StringBuilder sb = new StringBuilder();

            foreach(KeyValuePair<string, string> pair in _styleDic) {
                if ((string)comboBoxStyle.SelectedItem == pair.Value) {
                    sb.Append(pair.Key).Append(" ");
                    break;
                }
            }

            if (checkBoxIndent.Checked) {
                sb.Append("--indent=").Append(comboBoxIndent.SelectedItem).Append("=").Append(numericUpDownIndent.Value).Append(" ");
            }

            if (checkBoxMinCondIndent.Checked) {
                sb.Append("--min-conditional-indent=").Append(numericUpDownMinCondIndent.Value).Append(" ");
            }

            if (checkBoxMaxInstateIndent.Checked) {
                sb.Append("--max-instatement-indent=").Append(numericUpDownMaxInstateIndent.Value).Append(" ");
            }

            if (checkBoxAlignPointer.Checked) {
                sb.Append("--align-pointer=").Append(comboBoxAlignPointer.SelectedItem).Append(" ");
            }

            if (checkBoxAlignReference.Checked) {
                sb.Append("--align-reference=").Append(comboBoxAlignReference.SelectedItem).Append(" ");
            }

            foreach (KeyValuePair<string, CheckBox> pair in _checkboxDic) {
                if (pair.Value.Checked) {
                    sb.Append(pair.Key).Append(" ");
                }
            }

            if (sb.Length > 1) {
                if (_language == Language.CSharp) {
                    sb.Append("--mode=cs");
                } else if (_language == Language.C) {
                    sb.Append("--mode=c");
                }
            }

            return sb.ToString().Trim();
        }

        private void OnLinkLabelHelpClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            var sInfo = new ProcessStartInfo(HelpLink);
            Process.Start(sInfo);
        }

        private void OnCheckBoxIndentCheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                comboBoxIndent.Enabled = true;
                numericUpDownIndent.Enabled = true;
            } else {
                comboBoxIndent.SelectedIndex = 0;
                comboBoxIndent.Enabled = false;
                numericUpDownIndent.Value = 4;
                numericUpDownIndent.Enabled = false;
            }
        }

        private void OnCheckBoxMinCondIndentCheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                numericUpDownMinCondIndent.Enabled = true;
            } else {
                numericUpDownMinCondIndent.Value = 2;
                numericUpDownMinCondIndent.Enabled = false;
            }
        }

        private void OnCheckBoxMaxInstateIndentCheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                numericUpDownMaxInstateIndent.Enabled = true;
            } else {
                numericUpDownMaxInstateIndent.Value = 40;
                numericUpDownMaxInstateIndent.Enabled = false;
            }
        }

        private void OnCheckBoxAlignPointerCheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                comboBoxAlignPointer.Enabled = true;
            } else {
                comboBoxAlignPointer.SelectedIndex = 0;
                comboBoxAlignPointer.Enabled = false;
            }
        }

        private void OnCheckBoxAlignReferenceCheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                comboBoxAlignReference.Enabled = true;
            } else {
                comboBoxAlignReference.SelectedIndex = 0;
                comboBoxAlignReference.Enabled = false;
            }
        }
    }
}