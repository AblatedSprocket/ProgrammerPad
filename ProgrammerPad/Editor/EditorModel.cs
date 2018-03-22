using Microsoft.Win32;
using ProgrammerPad.ColorPalette;
using ProgrammerPad.Utilities;
using System;
using System.IO;
using System.Windows;

namespace ProgrammerPad.Editor
{
    class EditorModel
    {
        #region Properties
        public Document Document { get; set; }
        public Format Format { get; set; }
        #endregion

        #region Commands
        public RelayCommand UpperCommand { get; }
        public RelayCommand LowerCommand { get; }
        public RelayCommand EnlargeCommand { get; }
        public RelayCommand ShrinkCommand { get; }
        public RelayCommand BoldCommand { get; }
        public RelayCommand ItalicCommand { get; }
        public RelayCommand SqlVariablesCommand { get; }
        public RelayCommand SqlParametersCommand { get; }
        public RelayCommand CsCommand { get; }
        public RelayCommand JsonCommand { get; }
        public RelayCommand XmlCommand { get; }
        public RelayCommand ColorPaletteCommand { get; }
        #endregion
        public EditorModel(Document document)
        {
            Document = document;
            Format = new Format();

            UpperCommand = new RelayCommand(Upper);
            LowerCommand = new RelayCommand(Lower);
            EnlargeCommand = new RelayCommand(Enlarge);
            ShrinkCommand = new RelayCommand(Shrink);
            BoldCommand = new RelayCommand(ToggleBold);
            ItalicCommand = new RelayCommand(ToggleItalic);
            SqlVariablesCommand = new RelayCommand(ConvertToSqlVariables);
            SqlParametersCommand = new RelayCommand(ConvertToSqlParameters);
            CsCommand = new RelayCommand(ConvertToCs);
            JsonCommand = new RelayCommand(FormatJson);
            XmlCommand = new RelayCommand(FormatXml);
            ColorPaletteCommand = new RelayCommand(ColorPalette);
        }

        #region File Menu Methods
        #endregion

        #region Top toolbar methods
        private void Upper()
        {
            if (Document.Text != null)
                Document.Text = Document.Text.ToUpper();
        }
        private void Lower()
        {
            if (Document.Text != null)
                Document.Text = Document.Text.ToLower();
        }
        private void Enlarge()
        {
            Format.Size += 2;
        }
        private void Shrink()
        {
            Format.Size -= 2;
        }
        private void ToggleBold()
        {
            Format.Weight = Format.Weight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
        }
        private void ToggleItalic()
        {
            Format.Style = Format.Style == FontStyles.Italic ? FontStyles.Normal : FontStyles.Italic;
        }
        private void ConvertToSqlVariables()
        {
            if (Document.Text != null)
                Document.Text = TextConverter.ConvertToSqlParams(Document.Text);
        }
        private void ConvertToSqlParameters()
        {
            if (Document.Text != null)
                Document.Text = TextConverter.ConvertToSqlCommand(Document.Text);
        }
        private void ConvertToCs()
        {
            if (Document.Text != null)
                Document.Text = TextConverter.ConvertVBtoCS(Document.Text);
        }
        private void FormatJson()
        {
            if (Document.Text != null)
                Document.Text = TextFormatter.FormatJSON(Document.Text);
        }
        private void FormatXml()
        {
            if (Document.Text != null)
                Document.Text = TextFormatter.FormatXML(Document.Text);
        }
        private void ColorPalette()
        {
            ColorPaletteDialog colorPaletteDialog = new ColorPaletteDialog();
            colorPaletteDialog.DataContext = Format;
            colorPaletteDialog.ShowDialog();
        }
        #endregion
    }
}
