namespace StealFocus.MSBuild.Tasks
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.StealFocus.co.uk/MSBuild/TextFileRegexReplaceDictionary/v1.0", TypeName="TextFileRegexReplaceDictionary")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schemas.StealFocus.co.uk/MSBuild/TextFileRegexReplaceDictionary/v1.0", IsNullable=false, ElementName="TextFileRegexReplaceDictionary")]
    public partial class TextFileRegexReplaceDictionary : object, System.ComponentModel.INotifyPropertyChanged
    {
        
        private EntryCollection entryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Entry")]
        public EntryCollection Entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
                this.RaisePropertyChanged("Entry");
            }
        }

        /// <remarks />
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <remarks />
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
