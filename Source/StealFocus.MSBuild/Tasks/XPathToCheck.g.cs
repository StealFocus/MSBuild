namespace StealFocus.MSBuild.Tasks
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", TypeName = "XPathToCheck")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", IsNullable = true, ElementName = "XPathToCheck")]
    public partial class XPathToCheck : object, System.ComponentModel.INotifyPropertyChanged
    {

        private StringCollection fileExclusionField;

        private string xpathField;

        private string adviceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FileExclusion")]
        public StringCollection FileExclusion
        {
            get
            {
                return this.fileExclusionField;
            }
            set
            {
                this.fileExclusionField = value;
                this.RaisePropertyChanged("FileExclusion");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "xpath")]
        public string Xpath
        {
            get
            {
                return this.xpathField;
            }
            set
            {
                this.xpathField = value;
                this.RaisePropertyChanged("Xpath");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "advice")]
        public string Advice
        {
            get
            {
                return this.adviceField;
            }
            set
            {
                this.adviceField = value;
                this.RaisePropertyChanged("Advice");
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
