using System.Drawing;
using System.Windows.Forms;
using System;

namespace Lab7CSharp
{
    partial class Form2 : Form
    {
        private PictureBox pictureBox;
        private Button loadButton;
        private Button saveButton;
        private RadioButton redRadioButton;
        private RadioButton greenRadioButton;
        private RadioButton blueRadioButton;
        private GroupBox radioGroup;

        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.redRadioButton = new System.Windows.Forms.RadioButton();
            this.greenRadioButton = new System.Windows.Forms.RadioButton();
            this.blueRadioButton = new System.Windows.Forms.RadioButton();
            this.radioGroup = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.radioGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(499, 286);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(412, 19);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load Image";
            loadButton.Click += LoadButton_Click;  // Додати обробник подій
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(412, 63);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save Image";
            saveButton.Click += SaveButton_Click;  // Додати обробник подій
            // 
            // redRadioButton
            // 
            this.redRadioButton.Checked = true;
            this.redRadioButton.Location = new System.Drawing.Point(0, 19);
            this.redRadioButton.Name = "redRadioButton";
            this.redRadioButton.Size = new System.Drawing.Size(104, 24);
            this.redRadioButton.TabIndex = 0;
            this.redRadioButton.TabStop = true;
            this.redRadioButton.Text = "Red";
            // 
            // greenRadioButton
            // 
            this.greenRadioButton.Location = new System.Drawing.Point(0, 40);
            this.greenRadioButton.Name = "greenRadioButton";
            this.greenRadioButton.Size = new System.Drawing.Size(104, 24);
            this.greenRadioButton.TabIndex = 1;
            this.greenRadioButton.Text = "Green";
            // 
            // blueRadioButton
            // 
            this.blueRadioButton.Location = new System.Drawing.Point(0, 64);
            this.blueRadioButton.Name = "blueRadioButton";
            this.blueRadioButton.Size = new System.Drawing.Size(104, 24);
            this.blueRadioButton.TabIndex = 2;
            this.blueRadioButton.Text = "Blue";
            // 
            // radioGroup
            // 
            this.radioGroup.Controls.Add(this.redRadioButton);
            this.radioGroup.Controls.Add(this.saveButton);
            this.radioGroup.Controls.Add(this.loadButton);
            this.radioGroup.Controls.Add(this.greenRadioButton);
            this.radioGroup.Controls.Add(this.blueRadioButton);
            this.radioGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radioGroup.Location = new System.Drawing.Point(0, 286);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Size = new System.Drawing.Size(499, 98);
            this.radioGroup.TabIndex = 3;
            this.radioGroup.TabStop = false;
            this.radioGroup.Text = "Color Component";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(499, 384);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.radioGroup);
            this.Name = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.radioGroup.ResumeLayout(false);
            this.Text = "Task2";
            this.ResumeLayout(false);

        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BMP Files|*.bmp|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap image = new Bitmap(openFileDialog.FileName);
                        pictureBox.Image = image;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("No image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "BMP Files|*.bmp|All Files|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap originalImage = new Bitmap(pictureBox.Image);
                        Bitmap resultImage = ExtractColorComponent(originalImage);
                        resultImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Bitmap ExtractColorComponent(Bitmap originalImage)
        {
            ColorComponent selectedComponent;

            if (redRadioButton.Checked)
                selectedComponent = ColorComponent.Red;
            else if (greenRadioButton.Checked)
                selectedComponent = ColorComponent.Green;
            else
                selectedComponent = ColorComponent.Blue;

            Bitmap resultImage = new Bitmap(originalImage);

            for (int x = 0; x < resultImage.Width; x++)
            {
                for (int y = 0; y < resultImage.Height; y++)
                {
                    Color pixel = originalImage.GetPixel(x, y);

                    Color newColor = Color.Black;

                    switch (selectedComponent)
                    {
                        case ColorComponent.Red:
                            newColor = Color.FromArgb(pixel.R, 0, 0);
                            break;
                        case ColorComponent.Green:
                            newColor = Color.FromArgb(0, pixel.G, 0);
                            break;
                        case ColorComponent.Blue:
                            newColor = Color.FromArgb(0, 0, pixel.B);
                            break;
                    }

                    resultImage.SetPixel(x, y, newColor);
                }
            }

            return resultImage;
        }
        public enum ColorComponent
        {
            Red,
            Green,
            Blue
        }
    }
}