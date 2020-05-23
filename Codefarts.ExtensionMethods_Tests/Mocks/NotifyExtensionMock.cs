// <copyright file="NotifyExtensionMock.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.ExtensionMethods_Tests.Mocks
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NotifyExtensionMock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int integerProperty;
        private string stringProperty;
        private object objectProperty;

        public bool NoSetterBool { get; }
     
        public float PrivateSetterFloat { get; private set; }
      
        public float InternalSetterFloat { get; internal set; }


        public object ObjectProperty
        {
            get
            {
                return this.objectProperty;
            }

            set
            {
                var currentValue = this.objectProperty;
                if (currentValue != value)
                {
                    this.objectProperty = value;
                    this.OnPropertyChanged(nameof(this.ObjectProperty));
                }
            }
        }

        public string StringProperty
        {
            get
            {
                return this.stringProperty;
            }

            set
            {
                var currentValue = this.stringProperty;
                if (currentValue != value)
                {
                    this.stringProperty = value;
                    this.OnPropertyChanged(nameof(this.StringProperty));
                }
            }
        }

        public int IntegerProperty
        {
            get
            {
                return this.integerProperty;
            }

            set
            {
                var currentValue = this.integerProperty;
                if (currentValue != value)
                {
                    this.integerProperty = value;
                    this.OnPropertyChanged(nameof(this.IntegerProperty));
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}