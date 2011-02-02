namespace StealFocus.MSBuild.Tasks
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", TypeName = "XmlFileCheckerConfiguration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.StealFocus.co.uk/MSBuild/XmlFileCheckerConfiguration/v1.0", IsNullable = false, ElementName = "XmlFileCheckerConfiguration")]
    public partial class XmlFileCheckerConfiguration : object, System.ComponentModel.INotifyPropertyChanged
    {

        private XmlNamespaceCollection xmlNamespaceField;

        private XPathToCheckCollection xPathToCheckField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XmlNamespace")]
        public XmlNamespaceCollection XmlNamespace
        {
            get
            {
                return this.xmlNamespaceField;
            }
            set
            {
                this.xmlNamespaceField = value;
                this.RaisePropertyChanged("XmlNamespace");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XPathToCheck")]
        public XPathToCheckCollection XPathToCheck
        {
            get
            {
                return this.xPathToCheckField;
            }
            set
            {
                this.xPathToCheckField = value;
                this.RaisePropertyChanged("XPathToCheck");
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
