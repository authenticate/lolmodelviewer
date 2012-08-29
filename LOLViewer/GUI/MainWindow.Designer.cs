﻿

/*
LOLViewer
Copyright 2011-2012 James Lammlein, Adrian Astley 

 

This file is part of LOLViewer.

LOLViewer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

LOLViewer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with LOLViewer.  If not, see <http://www.gnu.org/licenses/>.

*/

namespace LOLViewer.GUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.glControlMain = new OpenTK.GLControl();
            this.mainWindowMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMainWindowMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.readMainMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDirectoryMainMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMainWindowMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWindowStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainWindowStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.glControlTabControlSplitContainer = new System.Windows.Forms.SplitContainer();
            this.optionsTabControl = new System.Windows.Forms.TabControl();
            this.renderOptionsTab = new System.Windows.Forms.TabPage();
            this.fullscreenButton = new System.Windows.Forms.Button();
            this.backgroundColorButton = new System.Windows.Forms.Button();
            this.backgroundLabel = new System.Windows.Forms.Label();
            this.cameraLabel = new System.Windows.Forms.Label();
            this.resetCameraButton = new System.Windows.Forms.Button();
            this.modelScaleLabel = new System.Windows.Forms.Label();
            this.modelScaleTrackbar = new System.Windows.Forms.TrackBar();
            this.yOffsetTrackbar = new System.Windows.Forms.TrackBar();
            this.VerticalOffsetLabel = new System.Windows.Forms.Label();
            this.animationOptionsTab = new System.Windows.Forms.TabPage();
            this.playAnimationButton = new System.Windows.Forms.Button();
            this.nextKeyFrameButton = new System.Windows.Forms.Button();
            this.previousKeyFrameButton = new System.Windows.Forms.Button();
            this.keyFrameControlLabel = new System.Windows.Forms.Label();
            this.currentAnimationComboBox = new System.Windows.Forms.ComboBox();
            this.currentAnimationLabel = new System.Windows.Forms.Label();
            this.enableAnimationCheckBox = new System.Windows.Forms.CheckBox();
            this.timelineTrackBar = new System.Windows.Forms.TrackBar();
            this.glTabModelListBoxSplitContainer = new System.Windows.Forms.SplitContainer();
            this.modelListBox = new System.Windows.Forms.ListBox();
            this.modelSearchBox = new System.Windows.Forms.TextBox();
            this.modelSearchLabel = new System.Windows.Forms.Label();
            this.mainWindowMenuStrip.SuspendLayout();
            this.mainWindowStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glControlTabControlSplitContainer)).BeginInit();
            this.glControlTabControlSplitContainer.Panel1.SuspendLayout();
            this.glControlTabControlSplitContainer.Panel2.SuspendLayout();
            this.glControlTabControlSplitContainer.SuspendLayout();
            this.optionsTabControl.SuspendLayout();
            this.renderOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelScaleTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yOffsetTrackbar)).BeginInit();
            this.animationOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timelineTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glTabModelListBoxSplitContainer)).BeginInit();
            this.glTabModelListBoxSplitContainer.Panel1.SuspendLayout();
            this.glTabModelListBoxSplitContainer.Panel2.SuspendLayout();
            this.glTabModelListBoxSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControlMain
            // 
            this.glControlMain.BackColor = System.Drawing.Color.Black;
            this.glControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControlMain.Location = new System.Drawing.Point(0, 0);
            this.glControlMain.Name = "glControlMain";
            this.glControlMain.Size = new System.Drawing.Size(450, 218);
            this.glControlMain.TabIndex = 6;
            this.glControlMain.VSync = true;
            // 
            // mainWindowMenuStrip
            // 
            this.mainWindowMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMainWindowMenuStrip,
            this.aboutMainWindowMenuStrip});
            this.mainWindowMenuStrip.Location = new System.Drawing.Point(10, 10);
            this.mainWindowMenuStrip.Name = "mainWindowMenuStrip";
            this.mainWindowMenuStrip.Size = new System.Drawing.Size(604, 24);
            this.mainWindowMenuStrip.TabIndex = 7;
            this.mainWindowMenuStrip.Text = "mainWindowMenuStrip";
            // 
            // fileMainWindowMenuStrip
            // 
            this.fileMainWindowMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readMainMenuStripItem,
            this.readDirectoryMainMenuStripItem,
            this.fileToolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileMainWindowMenuStrip.Name = "fileMainWindowMenuStrip";
            this.fileMainWindowMenuStrip.Size = new System.Drawing.Size(37, 20);
            this.fileMainWindowMenuStrip.Text = "File";
            // 
            // readMainMenuStripItem
            // 
            this.readMainMenuStripItem.Name = "readMainMenuStripItem";
            this.readMainMenuStripItem.Size = new System.Drawing.Size(282, 22);
            this.readMainMenuStripItem.Text = "Read...";
            // 
            // readDirectoryMainMenuStripItem
            // 
            this.readDirectoryMainMenuStripItem.Name = "readDirectoryMainMenuStripItem";
            this.readDirectoryMainMenuStripItem.Size = new System.Drawing.Size(282, 22);
            this.readDirectoryMainMenuStripItem.Text = "Read from Default Installation Directory";
            // 
            // fileToolStripSeparator
            // 
            this.fileToolStripSeparator.Name = "fileToolStripSeparator";
            this.fileToolStripSeparator.Size = new System.Drawing.Size(279, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutMainWindowMenuStrip
            // 
            this.aboutMainWindowMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.aboutMainWindowMenuStrip.Name = "aboutMainWindowMenuStrip";
            this.aboutMainWindowMenuStrip.Size = new System.Drawing.Size(52, 20);
            this.aboutMainWindowMenuStrip.Text = "About";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.aboutToolStripMenuItem.Text = "About LOLViewer...";
            // 
            // mainWindowStatusStrip
            // 
            this.mainWindowStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainWindowStatusLabel});
            this.mainWindowStatusStrip.Location = new System.Drawing.Point(10, 412);
            this.mainWindowStatusStrip.Name = "mainWindowStatusStrip";
            this.mainWindowStatusStrip.Size = new System.Drawing.Size(604, 22);
            this.mainWindowStatusStrip.TabIndex = 8;
            this.mainWindowStatusStrip.Text = "statusStrip1";
            // 
            // mainWindowStatusLabel
            // 
            this.mainWindowStatusLabel.Name = "mainWindowStatusLabel";
            this.mainWindowStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // glControlTabControlSplitContainer
            // 
            this.glControlTabControlSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControlTabControlSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.glControlTabControlSplitContainer.Name = "glControlTabControlSplitContainer";
            this.glControlTabControlSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // glControlTabControlSplitContainer.Panel1
            // 
            this.glControlTabControlSplitContainer.Panel1.Controls.Add(this.glControlMain);
            this.glControlTabControlSplitContainer.Panel1MinSize = 200;
            // 
            // glControlTabControlSplitContainer.Panel2
            // 
            this.glControlTabControlSplitContainer.Panel2.Controls.Add(this.optionsTabControl);
            this.glControlTabControlSplitContainer.Panel2MinSize = 140;
            this.glControlTabControlSplitContainer.Size = new System.Drawing.Size(450, 378);
            this.glControlTabControlSplitContainer.SplitterDistance = 218;
            this.glControlTabControlSplitContainer.TabIndex = 9;
            // 
            // optionsTabControl
            // 
            this.optionsTabControl.Controls.Add(this.renderOptionsTab);
            this.optionsTabControl.Controls.Add(this.animationOptionsTab);
            this.optionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsTabControl.Location = new System.Drawing.Point(0, 0);
            this.optionsTabControl.Name = "optionsTabControl";
            this.optionsTabControl.SelectedIndex = 0;
            this.optionsTabControl.Size = new System.Drawing.Size(450, 156);
            this.optionsTabControl.TabIndex = 0;
            // 
            // renderOptionsTab
            // 
            this.renderOptionsTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.renderOptionsTab.Controls.Add(this.fullscreenButton);
            this.renderOptionsTab.Controls.Add(this.backgroundColorButton);
            this.renderOptionsTab.Controls.Add(this.backgroundLabel);
            this.renderOptionsTab.Controls.Add(this.cameraLabel);
            this.renderOptionsTab.Controls.Add(this.resetCameraButton);
            this.renderOptionsTab.Controls.Add(this.modelScaleLabel);
            this.renderOptionsTab.Controls.Add(this.modelScaleTrackbar);
            this.renderOptionsTab.Controls.Add(this.yOffsetTrackbar);
            this.renderOptionsTab.Controls.Add(this.VerticalOffsetLabel);
            this.renderOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.renderOptionsTab.Name = "renderOptionsTab";
            this.renderOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.renderOptionsTab.Size = new System.Drawing.Size(442, 130);
            this.renderOptionsTab.TabIndex = 0;
            this.renderOptionsTab.Text = "Rendering Options";
            // 
            // fullscreenButton
            // 
            this.fullscreenButton.Location = new System.Drawing.Point(143, 66);
            this.fullscreenButton.Name = "fullscreenButton";
            this.fullscreenButton.Size = new System.Drawing.Size(75, 23);
            this.fullscreenButton.TabIndex = 8;
            this.fullscreenButton.Text = "Fullscreen";
            this.fullscreenButton.UseVisualStyleBackColor = true;
            // 
            // backgroundColorButton
            // 
            this.backgroundColorButton.Location = new System.Drawing.Point(231, 25);
            this.backgroundColorButton.Name = "backgroundColorButton";
            this.backgroundColorButton.Size = new System.Drawing.Size(75, 23);
            this.backgroundColorButton.TabIndex = 7;
            this.backgroundColorButton.Text = "Color";
            this.backgroundColorButton.UseVisualStyleBackColor = true;
            // 
            // backgroundLabel
            // 
            this.backgroundLabel.AutoSize = true;
            this.backgroundLabel.Location = new System.Drawing.Point(236, 9);
            this.backgroundLabel.Name = "backgroundLabel";
            this.backgroundLabel.Size = new System.Drawing.Size(65, 13);
            this.backgroundLabel.TabIndex = 6;
            this.backgroundLabel.Text = "Background";
            // 
            // cameraLabel
            // 
            this.cameraLabel.AutoSize = true;
            this.cameraLabel.Location = new System.Drawing.Point(159, 9);
            this.cameraLabel.Name = "cameraLabel";
            this.cameraLabel.Size = new System.Drawing.Size(43, 13);
            this.cameraLabel.TabIndex = 5;
            this.cameraLabel.Text = "Camera";
            // 
            // resetCameraButton
            // 
            this.resetCameraButton.Location = new System.Drawing.Point(143, 25);
            this.resetCameraButton.Name = "resetCameraButton";
            this.resetCameraButton.Size = new System.Drawing.Size(75, 23);
            this.resetCameraButton.TabIndex = 4;
            this.resetCameraButton.Text = "(R)eset";
            this.resetCameraButton.UseVisualStyleBackColor = true;
            // 
            // modelScaleLabel
            // 
            this.modelScaleLabel.AutoSize = true;
            this.modelScaleLabel.Location = new System.Drawing.Point(38, 65);
            this.modelScaleLabel.Name = "modelScaleLabel";
            this.modelScaleLabel.Size = new System.Drawing.Size(66, 13);
            this.modelScaleLabel.TabIndex = 3;
            this.modelScaleLabel.Text = "Model Scale";
            // 
            // modelScaleTrackbar
            // 
            this.modelScaleTrackbar.LargeChange = 50;
            this.modelScaleTrackbar.Location = new System.Drawing.Point(6, 84);
            this.modelScaleTrackbar.Maximum = 210;
            this.modelScaleTrackbar.Minimum = 10;
            this.modelScaleTrackbar.Name = "modelScaleTrackbar";
            this.modelScaleTrackbar.Size = new System.Drawing.Size(131, 45);
            this.modelScaleTrackbar.TabIndex = 2;
            this.modelScaleTrackbar.TickFrequency = 50;
            this.modelScaleTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.modelScaleTrackbar.Value = 110;
            // 
            // yOffsetTrackbar
            // 
            this.yOffsetTrackbar.LargeChange = 25;
            this.yOffsetTrackbar.Location = new System.Drawing.Point(6, 31);
            this.yOffsetTrackbar.Maximum = 100;
            this.yOffsetTrackbar.Name = "yOffsetTrackbar";
            this.yOffsetTrackbar.Size = new System.Drawing.Size(131, 45);
            this.yOffsetTrackbar.TabIndex = 1;
            this.yOffsetTrackbar.TickFrequency = 25;
            this.yOffsetTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // VerticalOffsetLabel
            // 
            this.VerticalOffsetLabel.AutoSize = true;
            this.VerticalOffsetLabel.Location = new System.Drawing.Point(33, 15);
            this.VerticalOffsetLabel.Name = "VerticalOffsetLabel";
            this.VerticalOffsetLabel.Size = new System.Drawing.Size(77, 13);
            this.VerticalOffsetLabel.TabIndex = 0;
            this.VerticalOffsetLabel.Text = "Model Y Offset";
            // 
            // animationOptionsTab
            // 
            this.animationOptionsTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.animationOptionsTab.Controls.Add(this.playAnimationButton);
            this.animationOptionsTab.Controls.Add(this.nextKeyFrameButton);
            this.animationOptionsTab.Controls.Add(this.previousKeyFrameButton);
            this.animationOptionsTab.Controls.Add(this.keyFrameControlLabel);
            this.animationOptionsTab.Controls.Add(this.currentAnimationComboBox);
            this.animationOptionsTab.Controls.Add(this.currentAnimationLabel);
            this.animationOptionsTab.Controls.Add(this.enableAnimationCheckBox);
            this.animationOptionsTab.Controls.Add(this.timelineTrackBar);
            this.animationOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.animationOptionsTab.Name = "animationOptionsTab";
            this.animationOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.animationOptionsTab.Size = new System.Drawing.Size(442, 130);
            this.animationOptionsTab.TabIndex = 1;
            this.animationOptionsTab.Text = "Animation Options";
            // 
            // playAnimationButton
            // 
            this.playAnimationButton.Location = new System.Drawing.Point(7, 79);
            this.playAnimationButton.Name = "playAnimationButton";
            this.playAnimationButton.Size = new System.Drawing.Size(75, 23);
            this.playAnimationButton.TabIndex = 6;
            this.playAnimationButton.Text = "Play";
            this.playAnimationButton.UseVisualStyleBackColor = true;
            // 
            // nextKeyFrameButton
            // 
            this.nextKeyFrameButton.Location = new System.Drawing.Point(169, 79);
            this.nextKeyFrameButton.Name = "nextKeyFrameButton";
            this.nextKeyFrameButton.Size = new System.Drawing.Size(75, 23);
            this.nextKeyFrameButton.TabIndex = 5;
            this.nextKeyFrameButton.Text = "Next";
            this.nextKeyFrameButton.UseVisualStyleBackColor = true;
            // 
            // previousKeyFrameButton
            // 
            this.previousKeyFrameButton.Location = new System.Drawing.Point(88, 79);
            this.previousKeyFrameButton.Name = "previousKeyFrameButton";
            this.previousKeyFrameButton.Size = new System.Drawing.Size(75, 23);
            this.previousKeyFrameButton.TabIndex = 4;
            this.previousKeyFrameButton.Text = "Previous";
            this.previousKeyFrameButton.UseVisualStyleBackColor = true;
            // 
            // keyFrameControlLabel
            // 
            this.keyFrameControlLabel.AutoSize = true;
            this.keyFrameControlLabel.Location = new System.Drawing.Point(76, 37);
            this.keyFrameControlLabel.Name = "keyFrameControlLabel";
            this.keyFrameControlLabel.Size = new System.Drawing.Size(98, 13);
            this.keyFrameControlLabel.TabIndex = 3;
            this.keyFrameControlLabel.Text = "Key Frame Controls";
            // 
            // currentAnimationComboBox
            // 
            this.currentAnimationComboBox.FormattingEnabled = true;
            this.currentAnimationComboBox.Location = new System.Drawing.Point(270, 28);
            this.currentAnimationComboBox.Name = "currentAnimationComboBox";
            this.currentAnimationComboBox.Size = new System.Drawing.Size(121, 21);
            this.currentAnimationComboBox.TabIndex = 2;
            // 
            // currentAnimationLabel
            // 
            this.currentAnimationLabel.AutoSize = true;
            this.currentAnimationLabel.Location = new System.Drawing.Point(284, 12);
            this.currentAnimationLabel.Name = "currentAnimationLabel";
            this.currentAnimationLabel.Size = new System.Drawing.Size(90, 13);
            this.currentAnimationLabel.TabIndex = 1;
            this.currentAnimationLabel.Text = "Current Animation";
            // 
            // enableAnimationCheckBox
            // 
            this.enableAnimationCheckBox.AutoSize = true;
            this.enableAnimationCheckBox.Location = new System.Drawing.Point(14, 11);
            this.enableAnimationCheckBox.Name = "enableAnimationCheckBox";
            this.enableAnimationCheckBox.Size = new System.Drawing.Size(108, 17);
            this.enableAnimationCheckBox.TabIndex = 0;
            this.enableAnimationCheckBox.Text = "Enable Animation";
            this.enableAnimationCheckBox.UseVisualStyleBackColor = true;
            // 
            // timelineTrackBar
            // 
            this.timelineTrackBar.LargeChange = 10;
            this.timelineTrackBar.Location = new System.Drawing.Point(7, 53);
            this.timelineTrackBar.Maximum = 100;
            this.timelineTrackBar.Name = "timelineTrackBar";
            this.timelineTrackBar.Size = new System.Drawing.Size(237, 45);
            this.timelineTrackBar.TabIndex = 7;
            this.timelineTrackBar.TabStop = false;
            this.timelineTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // glTabModelListBoxSplitContainer
            // 
            this.glTabModelListBoxSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glTabModelListBoxSplitContainer.Location = new System.Drawing.Point(10, 34);
            this.glTabModelListBoxSplitContainer.Name = "glTabModelListBoxSplitContainer";
            // 
            // glTabModelListBoxSplitContainer.Panel1
            // 
            this.glTabModelListBoxSplitContainer.Panel1.Controls.Add(this.glControlTabControlSplitContainer);
            this.glTabModelListBoxSplitContainer.Panel1MinSize = 400;
            // 
            // glTabModelListBoxSplitContainer.Panel2
            // 
            this.glTabModelListBoxSplitContainer.Panel2.Controls.Add(this.modelListBox);
            this.glTabModelListBoxSplitContainer.Panel2.Controls.Add(this.modelSearchBox);
            this.glTabModelListBoxSplitContainer.Panel2.Controls.Add(this.modelSearchLabel);
            this.glTabModelListBoxSplitContainer.Panel2MinSize = 150;
            this.glTabModelListBoxSplitContainer.Size = new System.Drawing.Size(604, 378);
            this.glTabModelListBoxSplitContainer.SplitterDistance = 450;
            this.glTabModelListBoxSplitContainer.TabIndex = 11;
            // 
            // modelListBox
            // 
            this.modelListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelListBox.FormattingEnabled = true;
            this.modelListBox.Location = new System.Drawing.Point(0, 52);
            this.modelListBox.Name = "modelListBox";
            this.modelListBox.Size = new System.Drawing.Size(150, 329);
            this.modelListBox.TabIndex = 17;
            // 
            // modelSearchBox
            // 
            this.modelSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelSearchBox.Location = new System.Drawing.Point(0, 24);
            this.modelSearchBox.Name = "modelSearchBox";
            this.modelSearchBox.Size = new System.Drawing.Size(150, 20);
            this.modelSearchBox.TabIndex = 16;
            // 
            // modelSearchLabel
            // 
            this.modelSearchLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.modelSearchLabel.AutoSize = true;
            this.modelSearchLabel.Location = new System.Drawing.Point(52, 6);
            this.modelSearchLabel.Name = "modelSearchLabel";
            this.modelSearchLabel.Size = new System.Drawing.Size(41, 13);
            this.modelSearchLabel.TabIndex = 15;
            this.modelSearchLabel.Text = "Search";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.glTabModelListBoxSplitContainer);
            this.Controls.Add(this.mainWindowStatusStrip);
            this.Controls.Add(this.mainWindowMenuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainWindowMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "LOLViewer 1.18";
            this.mainWindowMenuStrip.ResumeLayout(false);
            this.mainWindowMenuStrip.PerformLayout();
            this.mainWindowStatusStrip.ResumeLayout(false);
            this.mainWindowStatusStrip.PerformLayout();
            this.glControlTabControlSplitContainer.Panel1.ResumeLayout(false);
            this.glControlTabControlSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glControlTabControlSplitContainer)).EndInit();
            this.glControlTabControlSplitContainer.ResumeLayout(false);
            this.optionsTabControl.ResumeLayout(false);
            this.renderOptionsTab.ResumeLayout(false);
            this.renderOptionsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelScaleTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yOffsetTrackbar)).EndInit();
            this.animationOptionsTab.ResumeLayout(false);
            this.animationOptionsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timelineTrackBar)).EndInit();
            this.glTabModelListBoxSplitContainer.Panel1.ResumeLayout(false);
            this.glTabModelListBoxSplitContainer.Panel2.ResumeLayout(false);
            this.glTabModelListBoxSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glTabModelListBoxSplitContainer)).EndInit();
            this.glTabModelListBoxSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControlMain;
        private System.Windows.Forms.MenuStrip mainWindowMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMainWindowMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMainWindowMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readDirectoryMainMenuStripItem;
        private System.Windows.Forms.StatusStrip mainWindowStatusStrip;
        private System.Windows.Forms.SplitContainer glControlTabControlSplitContainer;
        private System.Windows.Forms.TabControl optionsTabControl;
        private System.Windows.Forms.TabPage renderOptionsTab;
        private System.Windows.Forms.TabPage animationOptionsTab;
        private System.Windows.Forms.SplitContainer glTabModelListBoxSplitContainer;
        private System.Windows.Forms.ToolStripStatusLabel mainWindowStatusLabel;
        private System.Windows.Forms.TrackBar yOffsetTrackbar;
        private System.Windows.Forms.Label VerticalOffsetLabel;
        private System.Windows.Forms.TrackBar modelScaleTrackbar;
        private System.Windows.Forms.Label modelScaleLabel;
        private System.Windows.Forms.CheckBox enableAnimationCheckBox;
        private System.Windows.Forms.Label currentAnimationLabel;
        private System.Windows.Forms.ComboBox currentAnimationComboBox;
        private System.Windows.Forms.Button previousKeyFrameButton;
        private System.Windows.Forms.Label keyFrameControlLabel;
        private System.Windows.Forms.Button nextKeyFrameButton;
        private System.Windows.Forms.Button playAnimationButton;
        private System.Windows.Forms.Label cameraLabel;
        private System.Windows.Forms.Button resetCameraButton;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator;
        private System.Windows.Forms.Button backgroundColorButton;
        private System.Windows.Forms.Label backgroundLabel;
        private System.Windows.Forms.ListBox modelListBox;
        private System.Windows.Forms.TextBox modelSearchBox;
        private System.Windows.Forms.Label modelSearchLabel;
        private System.Windows.Forms.TrackBar timelineTrackBar;
        private System.Windows.Forms.Button fullscreenButton;
        private System.Windows.Forms.ToolStripMenuItem readMainMenuStripItem;
    }
}

