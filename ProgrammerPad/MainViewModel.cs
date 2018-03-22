using ProgrammerPad.Editor;
using ProgrammerPad.FileMenu;

namespace ProgrammerPad
{
    class MainViewModel
    {
        Document _document;
        public EditorModel Editor { get; set; }
        public FileMenuModel File { get; set; }
        public MainViewModel()
        {
            _document = new Document();
            Editor = new EditorModel(_document);
            File = new FileMenuModel(_document);
        }
    }
}
