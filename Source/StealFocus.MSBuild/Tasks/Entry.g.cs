namespace StealFocus.MSBuild.Tasks
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.StealFocus.co.uk/MSBuild/TextFileRegexReplaceDictionary/v1.0", TypeName="Entry")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schemas.StealFocus.co.uk/MSBuild/TextFileRegexReplaceDictionary/v1.0", IsNullable=true, ElementName="Entry")]
    public partial class Entry : object, System.ComponentModel.INotifyPropertyChanged
    {
        
        private string replacementValueField;
        
        private string regularExpressionField;
        
        private int expectedMatchesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ReplacementValue")]
        public string ReplacementValue
        {
            get
            {
                return this.replacementValueField;
            }
            set
            {
                this.replacementValueField = value;
                this.RaisePropertyChanged("ReplacementValue");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="regularExpression")]
        public string RegularExpression
        {
            get
            {
                return this.regularExpressionField;
            }
            set
            {
                this.regularExpressionField = value;
                this.RaisePropertyChanged("RegularExpression");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="expectedMatches")]
        public int ExpectedMatches
        {
            get
            {
                return this.expectedMatchesField;
            }
            set
            {
                this.expectedMatchesField = value;
                this.RaisePropertyChanged("ExpectedMatches");
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
