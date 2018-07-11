using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ImageProcessor;

namespace DateThem
{
    public partial class MainForm : Form
    {
        private ResourceManager rm;
        private string path;

        public MainForm()
        {
            InitializeComponent();
            SetLanguage();
            path = null;
            PrintDatesButton.Enabled = false;
        }

        private void SetLanguage()
        {
            rm = new ResourceManager(
                "DateMe.lang." + System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName,
                Assembly.GetExecutingAssembly());

            this.Text = rm.GetString("app_name");
            ChooseFolderButton.Text = rm.GetString("choose_folder_button");
            ChosenFolderLabel.Text = rm.GetString("chosen_folder_label");
            PrintDatesButton.Text = rm.GetString("print_dates_button");
        }

        private void ChooseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = rm.GetString("chosen_folder_label"),
                ShowNewFolderButton = false,
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SetPath(folderBrowserDialog.SelectedPath);
                PrintDatesButton.Enabled = true;
            }
        }

        private void SetPath(string p)
        {
            path = p;
            ChosenFolderLabel.Text = System.IO.Path.GetFileName(path);
        }

        private void PrintDatesButton_Click(object sender, EventArgs e)
        {
            ChooseFolderButton.Enabled = false;
            PrintDatesButton.Enabled = false;

            FileStream fs = null;
            Image image = null;
            byte[] source;
            ImageFactory imageFactory = new ImageFactory();
            Regex r = new Regex(":");
            int dated = 0, notDated = 0;

            string[] picturesPaths = Directory.GetFiles(@path, "*.jpg");

            ProgressBar.Maximum = picturesPaths.Length;
            ProgressBar.Step = 1;

            foreach (string p in picturesPaths)
            {
                try // case property is unset or error with reading/saving image
                {
                    // preparing image
                    fs = new FileStream(p, FileMode.Open, FileAccess.Read);
                    image = Image.FromStream(fs, false, false);
                    source = File.ReadAllBytes(p);
                    MemoryStream imageStream = new MemoryStream(source);

                    // reading neccesary properties
                    PropertyItem propertyDate = image.GetPropertyItem(0x9003);
                    PropertyItem propertyWidth = image.GetPropertyItem(0x0100);
                    PropertyItem propertyHeight = image.GetPropertyItem(0x0101);
                    int height = BitConverter.ToInt32(propertyHeight.Value, 0);

                    PropertyItem[] allProperties = image.PropertyItems;

                    // preparing date
                    DateTime date = DateTime.Parse(r.Replace(Encoding.UTF8.GetString(propertyDate.Value), "-", 2));
                    string dateStr = ConvertDate(date);

                    // preparing position
                    Point position = new Point(
                        (int)(BitConverter.ToInt32(propertyWidth.Value, 0) * .85),
                        (int)(height * .95)
                        );

                    // preparing font size
                    int fontSize = (int)(height * .03);

                    // unlocking path to the image and deleting old one
                    fs.Close();
                    File.Delete(p);

                    // printing date
                    imageFactory.Load(imageStream)
                                .Watermark(new ImageProcessor.Imaging.TextLayer()
                                {
                                    Position = position,

                                    Text = dateStr,
                                    Style = FontStyle.Bold,
                                    FontColor = Color.White,
                                    Opacity = 80,
                                    FontSize = fontSize,
                                })
                                .Save(imageStream);
                    Console.WriteLine("date: " + dateStr);

                    dated++;

                    // retrieving image propeties
                    image = Image.FromStream(imageStream, false, false);
                    foreach (PropertyItem property in allProperties) image.SetPropertyItem(property);

                    // saving image with date
                    image.Save(p);
                }
                catch
                {
                    notDated++;
                    Console.WriteLine("Error: probably image does\'t have one of properties");
                }
                finally
                {
                    // clering memory
                    fs.Dispose();
                    image.Dispose();
                    imageFactory.Dispose();

                    GC.Collect();
                }


                ProgressBar.PerformStep();

            }

            // show confirmation
            string title = rm.GetString("confirmation_title");
            string message = rm.GetString("confirmation_text_1") + dated + rm.GetString("confirmation_text_2") + notDated;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == System.Windows.Forms.DialogResult.OK) Reset();

        }

        private string ConvertDate(DateTime date)
        {
            string dateStr = "";
            int day = date.Day;
            int month = date.Month;
            if (day < 10) dateStr += "0";
            dateStr += day.ToString() + ".";
            if (month < 10) dateStr += "0";
            dateStr += month.ToString() + "." + date.Year.ToString();

            return dateStr;
        }

        private void Reset()
        {
            path = null;
            ChosenFolderLabel.Text = rm.GetString("chosen_folder_label");
            ChooseFolderButton.Enabled = true;
            PrintDatesButton.Enabled = true;
            ProgressBar.Value = 0;
        }
    }
}
