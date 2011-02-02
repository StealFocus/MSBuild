namespace StealFocus.MSBuild.Tasks
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", TypeName = "XmlNamespace")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", IsNullable = true, ElementName = "XmlNamespace")]
    public partial class XmlNamespace : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "id")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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
